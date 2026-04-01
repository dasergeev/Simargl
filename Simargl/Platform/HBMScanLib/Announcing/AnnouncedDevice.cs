// <copyright file="AnnouncedDevice.cs" company="Hottinger Baldwin Messtechnik GmbH">
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
    public class AnnouncedDevice
    {
#pragma warning disable 0649
        [DataMember(Name = "uuid")]
        private string uuid = string.Empty;

        [DataMember(Name = "name")]
        private string name = string.Empty;

        [DataMember(Name = "type")]
        private string deviceType = string.Empty;

        [DataMember(Name = "label")]
        private string label = string.Empty;

        [DataMember(Name = "familyType")]
        private string familyType = string.Empty;

        [DataMember(Name = "hardwareId")]
        private string hardwareId = string.Empty;

        [DataMember(Name = "firmwareVersion")]
        private string firmwareVersion = string.Empty;

        [DataMember(Name = "isRouter")]
        private bool isRouter = false;
#pragma warning restore 0649

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Uuid
        {
            get
            {
                return uuid;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string DeviceType
        {
            get
            {
                return deviceType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Label
        {
            get
            {
                return label;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string FamilyType
        {
            get
            {
                return familyType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string HardwareId
        {
            get
            {
                return hardwareId;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string FirmwareVersion
        {
            get
            {
                return firmwareVersion;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public bool IsRouter
        {
            get
            {
                return isRouter;
            }
        }
    }
}
