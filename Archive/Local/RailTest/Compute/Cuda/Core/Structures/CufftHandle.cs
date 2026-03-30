using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Compute.Cuda.Core
{
    /// <summary>
    /// Тип ручки, используемый для хранения и доступа к планам cuFFT.
    /// Пользователь получает дескриптор после создания плана cuFFT и использует этот дескриптор для выполнения плана.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 4)]
    public struct CufftHandle
    {
        /// <summary>
        /// Поле для хранения значения.
        /// </summary>
        private readonly int _Value;
    }
}
