// <copyright file="AnnounceParameters.cs" company="Hottinger Baldwin Messtechnik GmbH">
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class AnnounceParameters
    {
#pragma warning disable 0649
        [DataMember(Name = "expiration")]
        private int expiration;

        [DataMember(Name = "apiVersion")]

        private string apiVersion = string.Empty;

        [DataMember(Name = "device")]
        private AnnouncedDevice? device;

        [DataMember(Name = "router")]
        private Router? router;

        [DataMember(Name = "services")]
        private IList<ServiceEntry>? services;

        [DataMember(Name = "netSettings")]
        private NetSettings? netSettings;
#pragma warning restore 0649

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public int Expiration
        {
            get
            {
                return expiration;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string ApiVersion
        {
            get
            {
                return apiVersion;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public AnnouncedDevice? Device
        {
            get
            {
                return device;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Router? Router
        {
            get
            {
                return router;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public IList<ServiceEntry> Services
        {
            get
            {
                if (services != null)
                {
                    return new ReadOnlyCollection<ServiceEntry>(services);
                }
                else
                {
                    return new List<ServiceEntry>();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public NetSettings? NetSettings
        {
            get
            {
                return netSettings;
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext c)
        {
            /*
             * Ensure that we always have an expiration time greater zero.
             * Later handling of Announce objects (i.e. DeviceMonitor) can rely on a valid expiration time.
             */
            if (expiration < 0)
            {
                throw new SerializationException("negative expiration times are not allowed");
            }

            if (expiration == 0)
            {
                expiration = int.Parse(ScanConstants.defaultExpirationInSeconds, CultureInfo.InvariantCulture);
            }
        }
    }
}
