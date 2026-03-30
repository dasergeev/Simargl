// <copyright file="ConfigurationInterface.cs" company="Hottinger Baldwin Messtechnik GmbH">
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

namespace Hbm.Devices.Scan.Configure
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ConfigurationInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationInterface"></param>
        /// <param name="address"></param>
        /// <param name="mask"></param>
        public ConfigurationInterface(string configurationInterface, string address, string mask)
            : this(configurationInterface)
        {
            InternetProtocolV4 = new ManualInternetProtocolV4Address
            {
                ManualAddress = address,
                ManualNetMask = mask
            };
            ConfigurationMethod = ConfigurationInterface.Method.Manual;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationInterface"></param>
        /// <param name="mode"></param>
        public ConfigurationInterface(string configurationInterface, Method mode)
            : this(configurationInterface)
        {
            if (mode != Method.Dhcp)
            {
                throw new ArgumentException(null, nameof(mode));
            }

            ConfigurationMethod = mode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iface"></param>
        private ConfigurationInterface(string iface)
        {
            if (string.IsNullOrEmpty(iface))
            {
                throw new ArgumentException(null, nameof(iface));
            }

            Name = iface;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum Method
        {
            /// <summary>
            /// 
            /// </summary>
            /// <value></value>
            Manual,
            /// <summary>
            /// 
            /// </summary>
            /// <value></value>
            Dhcp
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "ipv4", EmitDefaultValue = false)]
        public ManualInternetProtocolV4Address? InternetProtocolV4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Method ConfigurationMethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "configurationMethod")]
        public string ConfigurationMethodString
        {
            get
            {
                if (ConfigurationMethod == Method.Dhcp)
                {
                    return "dhcp";
                }
                else
                {
                    return "manual";
                }
            }

            private set
            {
                if ("manual".Equals(value))
                {
                    ConfigurationMethod = Method.Manual;
                }
                else if ("dhcp".Equals(value))
                {
                    ConfigurationMethod = Method.Dhcp;
                }
                else
                {
                    throw new ArgumentException("ConfigurationMethodString");
                }
            }
        }
    }
}
