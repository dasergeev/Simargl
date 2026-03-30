// <copyright file="MulticastMessageReceiver.cs" company="Hottinger Baldwin Messtechnik GmbH">
//
// SharpScan, a library for scanning and configuring HBM devices.
//
// The MIT License (MIT)
//
// Copyright (C) Hottinger Baldwin Messtechnik GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
// BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN
// ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Hbm.Devices.Scan
{
    /// <summary>
    /// 
    /// </summary>
    public class MulticastMessageReceiver : IDisposable
    {
        private readonly Socket socket;
        private readonly IPAddress multicastIP;
        private readonly byte[] receiveBuffer;

        private readonly Dictionary<int, NetworkInterface> interfaceMap;
        private readonly List<IPAddress> multicastInterfaces;
        private readonly MulticastMessageEventArgs eventArgs;

        private EndPoint endPoint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multicastIP"></param>
        /// <param name="port"></param>
        public MulticastMessageReceiver(string multicastIP, int port)
            : this(IPAddress.Parse(multicastIP), port)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public MulticastMessageReceiver(IPAddress address, int port)
        {
            multicastIP = address;
            interfaceMap = new Dictionary<int, NetworkInterface>();
            multicastInterfaces = new List<IPAddress>();

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            eventArgs = new MulticastMessageEventArgs();
            try
            {
                socket.ReceiveBufferSize = 128000;
                IPEndPoint ipep = new(IPAddress.Any, port);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback, 0);
                socket.Bind(ipep);

                receiveBuffer = new byte[65536];
                IPEndPoint sender = new(IPAddress.Any, 0);
                endPoint = (EndPoint)sender;

                NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(AddressChangedCallback);

                AddMulticastMembership();

                socket.BeginReceiveMessageFrom(
                    receiveBuffer,
                    0,
                    receiveBuffer.Length,
                    SocketFlags.None,
                    ref endPoint,
                    new AsyncCallback(MessageComplete),
                    null);
            }
            catch
            {
                socket.Dispose();
                throw;
            }
        }

        ///
        ~MulticastMessageReceiver()
        {
            NetworkChange.NetworkAddressChanged -= AddressChangedCallback;
            if (socket != null)
            {
                Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MulticastMessageEventArgs>? HandleMessage;

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            DropMulticastMembership();
            socket.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        private static NetworkInterface? GetInterface(int index)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                try
                {
                    IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                    IPv4InterfaceProperties ip4 = adapterProperties.GetIPv4Properties();
                    if ((ip4 != null) && (ip4.Index == index))
                    {
                        return adapter;
                    }

                    IPv6InterfaceProperties ip6 = adapterProperties.GetIPv6Properties();
                    if ((ip6 != null) && (ip6.Index == index))
                    {
                        return adapter;
                    }
                }
                catch (NetworkInformationException e)
                {
                    if (e.NativeErrorCode != (int)SocketError.ProtocolNotSupported)
                    {
                        throw;
                    }
                }
            }

            return null;
        }

        private void AddressChangedCallback(object? sender, EventArgs e)
        {
            DropMulticastMembership();
            AddMulticastMembership();
        }

        private void DropMulticastMembership()
        {
            lock (multicastInterfaces)
            {
                foreach (IPAddress address in multicastInterfaces)
                {
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, new MulticastOption(multicastIP, address));
                }

                multicastInterfaces.Clear();
                interfaceMap.Clear();
            }
        }

        private void AddMulticastMembership()
        {
            lock (multicastInterfaces)
            {
                multicastInterfaces.Clear();
                interfaceMap.Clear();

                NetworkInterface[] ifaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in ifaces)
                {
                    if (ni.SupportsMulticast &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        (ni.OperationalStatus == OperationalStatus.Up || ni.OperationalStatus == OperationalStatus.Unknown))
                    {
                        UnicastIPAddressInformationCollection addresses = ni.GetIPProperties().UnicastAddresses;
                        foreach (UnicastIPAddressInformation address in addresses)
                        {
                            IPAddress addr = address.Address;
                            if (addr.AddressFamily == AddressFamily.InterNetwork)
                            {
                                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastIP, addr));
                                multicastInterfaces.Add(addr);
                            }
                        }
                    }
                }
            }
        }

        private void MessageComplete(IAsyncResult result)
        {
            var flags = SocketFlags.None;
            try
            {
                int received = socket.EndReceiveMessageFrom(result, ref flags, ref endPoint, out IPPacketInformation packetInformation);
                if (received > 0)
                {
                    NetworkInterface? incomingIF = LookupIncomingInterface(packetInformation.Interface);
                    string message = System.Text.Encoding.ASCII.GetString(receiveBuffer, 0, received);

                    if (HandleMessage != null)
                    {
                        eventArgs.IncomingInterface = incomingIF;
                        eventArgs.JsonString = message;
                        HandleMessage(this, eventArgs);
                    }
                }

                socket.BeginReceiveMessageFrom(
                    receiveBuffer,
                    0,
                    receiveBuffer.Length,
                    SocketFlags.None,
                    ref endPoint,
                    new AsyncCallback(MessageComplete),
                    null);
            }
            catch (ObjectDisposedException)
            {
                // Deliberatly ignored. This exception shows that the socket was closed.
                return;
            }
        }

        private NetworkInterface? LookupIncomingInterface(int index)
        {
            NetworkInterface? iface;
            if (interfaceMap.ContainsKey(index))
            {
                iface = interfaceMap[index];
            }
            else
            {
                iface = GetInterface(index);
                if (iface != null)
                {
                    interfaceMap[index] = iface;
                }
            }

            return iface;
        }
    }
}
