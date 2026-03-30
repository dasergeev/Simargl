using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Compute.Cuda.Core
{
    /// <summary>
    /// The libraryPropertyType data type is an enumeration of library property types. (ie. CUDA version X.Y.Z would yield MAJOR_VERSION=X, MINOR_VERSION=Y, PATCH_LEVEL=Z)
    /// </summary>
    public enum LibraryPropertyType
    {
        /// <summary>
        /// Major version.
        /// </summary>
        MAJOR_VERSION,

        /// <summary>
        /// Minor version.
        /// </summary>
	    MINOR_VERSION,

        /// <summary>
        /// Patch level.
        /// </summary>
	    PATCH_LEVEL
    }
}
