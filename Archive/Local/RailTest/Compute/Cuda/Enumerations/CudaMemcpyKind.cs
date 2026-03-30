using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Compute.Cuda
{
    /// <summary>
    /// CUDA memory copy types
    /// </summary>
    public enum CudaMemcpyKind : int
    {
        /// <summary>
        /// Host -> Host.
        /// </summary>
        HostToHost = 0,

        /// <summary>
        /// Host -> Device.
        /// </summary>
        HostToDevice = 1,

        /// <summary>
        /// Device -> Host.
        /// </summary>
        DeviceToHost = 2,

        /// <summary>
        /// Device -> Device.
        /// </summary>
        DeviceToDevice = 3,

        /// <summary>
        /// Direction of the transfer is inferred from the pointer values. Requires unified virtual addressing
        /// </summary>
        Default = 4
    }
}
