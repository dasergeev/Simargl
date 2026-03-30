using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Compute.Cuda
{
    /// <summary>
    /// Представляет исключение, которое произошло при обращении к ядрку CUDA.
    /// </summary>
    public class CudaException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="code">
        /// Код ошибки.
        /// </param>
        /// <param name="message">
        /// Сообщение, описывающее ошибку.
        /// </param>
        public CudaException(CudaError code, string message) :
            base(message)
        {
            Code = code;
        }

        /// <summary>
        /// Возвращает код ошибки.
        /// </summary>
        public CudaError Code { get; }

        /// <summary>
        /// Преобразовывает код ошибки в исключение.
        /// </summary>
        /// <param name="code">
        /// Код ошибки.
        /// </param>
        /// <param name="message">
        /// Сообщение, описывающее ошибку.
        /// </param>
        /// <returns>
        /// Исключение.
        /// </returns>
        internal static CudaException FromCode(CudaError code, string message)
        {
            return new CudaException(code, message);
        }
    }
}
