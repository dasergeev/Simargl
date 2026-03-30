// <copyright file="DefaultGateway.cs" company="Hottinger Baldwin Messtechnik GmbH">
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
    using System.Runtime.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class DefaultGateway
    {
#pragma warning disable 0649
        [DataMember(Name = "ipv4Address")]
        private string ipv4Address = string.Empty;

        [DataMember(Name = "ipv6Address")]
        private string ipv6Address = string.Empty;
#pragma warning restore 0649

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string InternetProtocolV4Address
        {
            get
            {
                return ipv4Address;
            }

            set
            {
                ipv4Address = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string InternetProtocolV6Address
        {
            get
            {
                return ipv6Address;
            }

            set
            {
                ipv6Address = value;
            }
        }
    }
}
