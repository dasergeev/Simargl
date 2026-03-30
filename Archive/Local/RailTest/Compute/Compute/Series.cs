using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Compute
{
    /// <summary>
    /// Представляет механизмы обработки последовательностей.
    /// </summary>
    public class Series : Ancestor
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public Series() :
            this(ComputeBase.Cpu)
        {

        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="base">
        /// Вычислительная база.
        /// </param>
        public Series(ComputeBase @base)
        {
            Base = @base;
        }

        /// <summary>
        /// Возвращает вычислительную базу.
        /// </summary>
        public ComputeBase Base { get; }


    }
}
