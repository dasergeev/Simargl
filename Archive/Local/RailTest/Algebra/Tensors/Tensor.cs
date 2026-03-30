using RailTest.Algebra.Tensors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Algebra.Tensors
{
    /// <summary>
    /// Представляет тензор.
    /// </summary>
    public abstract class Tensor
    {
        /// <summary>
        /// Поле для хранения ядра тензора.
        /// </summary>
        private readonly TensorCore _Core;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="core">
        /// Ядро тензора.
        /// </param>
        internal Tensor(TensorCore core)
        {
            _Core = core;
        }

        ///// <summary>
        ///// Выполняет преобразование
        ///// </summary>
        ///// <param name="value">
        ///// Значение (0, 0) тензора.
        ///// </param>
        //public static implicit operator Tensor(double value)
        //{

        //    return null;
        //}
    }

    ///// <summary>
    ///// Представляет тензор.
    ///// </summary>
    ///// <typeparam name="T">
    ///// Тип компонент тензора.
    ///// </typeparam>
    //public abstract class Tensor<T> : Tensor
    //{

    //}
}
