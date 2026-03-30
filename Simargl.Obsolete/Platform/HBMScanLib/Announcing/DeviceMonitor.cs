// <copyright file="DeviceMonitor.cs" company="Hottinger Baldwin Messtechnik GmbH">
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

namespace Hbm.Devices.Scan.Announcing
{
    using System;
    using System.Collections.Generic;
    using System.Timers;

    /// <summary>
    /// 
    /// </summary>
    public class DeviceMonitor
    {
        private readonly IDictionary<string, AnnounceTimer> deviceMap;

        private bool stopped;

        /// <summary>
        /// 
        /// </summary>
        public DeviceMonitor()
        {
            stopped = false;
            deviceMap = new Dictionary<string, AnnounceTimer>();
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NewDeviceEventArgs>? HandleNewDevice;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<UpdateDeviceEventArgs>? HandleUpdateDevice;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<RemoveDeviceEventArgs>? HandleRemoveDevice;

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            lock (deviceMap)
            {
                stopped = true;
                var keysToRemove = new List<string>();
                foreach (KeyValuePair<string, AnnounceTimer> entry in deviceMap)
                {
                    AnnounceTimer timer = deviceMap[entry.Key];
                    timer.Stop();
                    timer.Close();
                    keysToRemove.Add(entry.Key);
                }

                foreach (var key in keysToRemove)
                {
                    deviceMap.Remove(key);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsClosed()
        {
            lock (deviceMap)
            {
                return stopped;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void HandleEvent(object sender, AnnounceEventArgs args)
        {
            if (args != null)
            {
                Announce? announce = args.Announce;
                if(announce != null)
                    ArmTimer(announce);
            }
        }

        private void ArmTimer(Announce announce)
        {
            string path = announce.Path;
            int expriationMs = GetExpirationMilliSeconds(announce);
            lock (deviceMap)
            {
                if (!stopped)
                {
                    if (deviceMap.TryGetValue(path, out AnnounceTimer? value))
                    {
                        AnnounceTimer timer = value;
                        timer.Stop();
                        Announce oldAnnounce = timer.Announce;
                        if (!oldAnnounce.Equals(announce))
                        {
                            timer.Announce = announce;
                            if (HandleUpdateDevice != null)
                            {
                                UpdateDeviceEventArgs updateDeviceEvent = new()
                                {
                                    NewAnnounce = announce,
                                    OldAnnounce = oldAnnounce
                                };
                                HandleUpdateDevice(this, updateDeviceEvent);
                            }
                        }

                        timer.Interval = expriationMs;
                        timer.Start();
                    }
                    else
                    {
                        AnnounceTimer timer = new(expriationMs, announce)
                        {
                            AutoReset = false
                        };
                        timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        deviceMap.Add(path, timer);
                        timer.Start();
                        if (HandleNewDevice != null)
                        {
                            NewDeviceEventArgs newDeviceEvent = new()
                            {
                                Announce = announce
                            };
                            HandleNewDevice(this, newDeviceEvent);
                        }
                    }
                }
            }
        }

        private static int GetExpirationMilliSeconds(Announce announce)
        {
            return announce.Parameters != null ? announce.Parameters.Expiration * 1000 : 0;
        }

        private void OnTimedEvent(object? source, ElapsedEventArgs e)
        {
            if(source == null)
                return;
            AnnounceTimer timer = (AnnounceTimer)source;
            timer.Stop();
            string path = timer.Announce.Path;
            lock (deviceMap)
            {
                deviceMap.Remove(path);
            }

            if (HandleRemoveDevice != null)
            {
                RemoveDeviceEventArgs removeDeviceEvent = new()
                {
                    Announce = timer.Announce
                };
                HandleRemoveDevice(this, removeDeviceEvent);
            }
        }

        private class AnnounceTimer : Timer
        {
            internal AnnounceTimer(double expire, Announce announce)
                : base(expire)
            {
                Announce = announce;
            }

            internal Announce Announce { get; set; }
        }
    }
}
