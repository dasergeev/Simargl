// <copyright file="Receiver.cs" company="Hottinger Baldwin Messtechnik GmbH">
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
using Hbm.Devices.Scan.Announcing;

namespace Apeiron.QuantumX;

public class HBMScanner
{
    public static void Main()
    {
        AnnounceReceiver ar = new();
        AnnounceDeserializer parser = new(5);
        ar.HandleMessage += parser.HandleEvent;
        DeviceMonitor monitor = new();
        parser.HandleMessage += monitor.HandleEvent;
        HBMScanner receiver = new();
        monitor.HandleNewDevice += receiver.HandleNewDeviceEvent;
        monitor.HandleRemoveDevice += receiver.HandleRemoveDeviceEvent;
        monitor.HandleUpdateDevice += receiver.HandleUpdateDeviceEvent;
        try
        {
            Console.WriteLine("Press \"Enter\" - to exit.");
            Console.ReadLine();
        }
        finally
        {
            ar.Close();
        }
    }

    public void HandleNewDeviceEvent(object sender, NewDeviceEventArgs args)
    {
        if (sender != null)
        {
            Console.Out.WriteLine("New Device:");
            LogAnnounce(args.Announce);
        }
    }

    public void HandleRemoveDeviceEvent(object sender, RemoveDeviceEventArgs args)
    {
        if (sender != null)
        {
            Console.Out.WriteLine("Remove Device:");
            LogAnnounce(args.Announce);
        }
    }

    public void HandleUpdateDeviceEvent(object sender, UpdateDeviceEventArgs args)
    {
        if (sender != null)
        {
            Console.Out.WriteLine("Update Device:");
            LogAnnounce(args.NewAnnounce);
        }
    }

    private static void LogAnnounce(Announce announce)
    {
        Console.Out.WriteLine("api version: " + announce.Parameters.ApiVersion);
        LogDevice(announce.Parameters.Device);
        LogIpAddresses(announce.Parameters.NetSettings.Interface);
        LogServices(announce.Parameters.Services);
        Console.Out.WriteLine();
    }

    private static void LogDevice(AnnouncedDevice device)
    {
        Console.Out.WriteLine("Device:");
        Console.Out.WriteLine("  UUID: " + device.Uuid);
        Console.Out.WriteLine("  name: " + device.Name);
        Console.Out.WriteLine("  family: " + device.FamilyType);
        Console.Out.WriteLine("  type: " + device.DeviceType);
        Console.Out.WriteLine("  label: " + device.Label);
        Console.Out.WriteLine("  firmware version: " + device.FirmwareVersion);
        Console.Out.WriteLine("  is router: " + device.IsRouter);
    }

    private static void LogIpAddresses(NetInterface iface)
    {
        Console.Out.WriteLine("  IP adresses:");
        Console.Out.WriteLine("    interface name: " + iface.Name);
        foreach (InternetProtocolV4Address address in iface.InternetProtocolV4)
        {
            Console.Out.WriteLine("    " + address.Address + '/' + address.NetMask);
        }

        foreach (InternetProtocolV6Address address in iface.InternetProtocolV6)
        {
            Console.Out.WriteLine("    " + address.Address + '/' + address.Prefix);
        }
    }

    private static void LogServices(IList<ServiceEntry> services)
    {
        if ((services == null) || (services.Count == 0))
        {
            Console.Out.WriteLine("  No services announced!");
        }
        else
        {
            Console.Out.WriteLine("  Services:");
            foreach (ServiceEntry entry in services)
            {
                Console.Out.WriteLine("    " + entry.ServiceType + " : " + entry.Port);
            }
        }
    }
}
