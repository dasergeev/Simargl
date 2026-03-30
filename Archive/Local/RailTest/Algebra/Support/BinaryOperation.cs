using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Algebra.Support
{
    /// <summary>
    /// Представляет базовый класс для всех бинарных операций.
    /// </summary>
    /// <typeparam name="TLeft">
    /// Тип левого операнда.
    /// </typeparam>
    /// <typeparam name="TRight">
    /// Тип правого операнда.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// Тип результата.
    /// </typeparam>
    abstract class BinaryOperation<TLeft, TRight, TResult>
    {
        /// <summary>
        /// Выполняет операцию.
        /// </summary>
        /// <param name="left">
        /// Левый операнд.
        /// </param>
        /// <param name="right">
        /// Правый операнд.
        /// </param>
        /// <returns>
        /// Результат операции.
        /// </returns>
        public abstract TResult Invoke(TLeft left, TRight right);
    }
}
