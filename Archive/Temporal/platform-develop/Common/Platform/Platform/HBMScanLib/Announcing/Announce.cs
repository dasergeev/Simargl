// <copyright file="Announce.cs" company="Hottinger Baldwin Messtechnik GmbH">
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
    using System.Net.NetworkInformation;
    using System.Runtime.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Announce : JsonRpc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name = "params")]
        public AnnounceParameters? Parameters { get; private set; }

        internal string Path { get; set; } = string.Empty;

        internal NetworkInterface? IncomingInterface { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Announce other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            return string.Compare(JsonString, other.JsonString, StringComparison.Ordinal) == 0;
        }

        internal void IdentifyCommunicationPath(NetworkInterface incomingIF)
        {
            if (Parameters != null && Parameters.Device != null && Parameters.NetSettings != null && Parameters.NetSettings.Interface != null)
            {
                Path = Parameters.Device.Uuid + incomingIF.Id + Parameters.NetSettings.Interface.Name;
                if (Parameters.Router != null)
                {
                    Path = Path + Parameters.Router.Uuid;
                }
            }

            IncomingInterface = incomingIF;
        }
    }
}
