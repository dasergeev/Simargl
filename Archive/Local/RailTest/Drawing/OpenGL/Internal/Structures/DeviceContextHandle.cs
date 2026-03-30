using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет дескриптор контекста устройства.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal unsafe struct DeviceContextHandle
    {
        /// <summary>
        /// Поле для хранения значения дескриптора.
        /// </summary>
        private readonly IntPtr _Value;

        /// <summary>
        /// Инициализирует новый экземпляр сруктуры.
        /// </summary>
        /// <param name="value">
        /// Значение дескриптора.
        /// </param>
        private DeviceContextHandle(IntPtr value)
        {
            _Value = value;
        }

        /// <summary>
        /// Возвращает недействительный дескриптор.
        /// </summary>
        public static DeviceContextHandle Invalid { get; } = new DeviceContextHandle(IntPtr.Zero);

        /// <summary>
        /// Выполняет операцию проверки на равенство.
        /// </summary>
        /// <param name="left">
        /// Левый операнд.
        /// </param>
        /// <param name="rigth">
        /// Правый операнд.
        /// </param>
        /// <returns>
        /// Результат операции.
        /// </returns>
        public static bool operator == (DeviceContextHandle left, DeviceContextHandle rigth)
        {
            return left._Value == rigth._Value;
        }

        /// <summary>
        /// Выполняет операцию проверки на неравенство.
        /// </summary>
        /// <param name="left">
        /// Левый операнд.
        /// </param>
        /// <param name="rigth">
        /// Правый операнд.
        /// </param>
        /// <returns>
        /// Результат операции.
        /// </returns>
        public static bool operator !=(DeviceContextHandle left, DeviceContextHandle rigth)
        {
            return left._Value != rigth._Value;
        }

        /// <summary>
        /// Указывает, равен ли этот экземпляр заданному объекту.
        /// </summary>
        /// <param name="obj">
        /// Объект для сравнения с текущим экземпляром.
        /// </param>
        /// <returns>
        /// Значение true, если <paramref name="obj"/> и данный экземпляр относятся к одному типу и представляют одинаковые значения;
        /// в противном случае - значение false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is DeviceContextHandle handle)
            {
                return this == handle;
            }
            return false;
        }

        /// <summary>
        /// Возвращает хэш-код данного экземпляра.
        /// </summary>
        /// <returns>
        /// 32-разрядное целое число со знаком, являющееся хэш-кодом для данного экземпляра.
        /// </returns>
        public override int GetHashCode()
        {
            return _Value.GetHashCode();
        }

        /// <summary>
        /// Возвращает текстовое представление объекта.
        /// </summary>
        /// <returns>
        /// Текстовое представление объекта.
        /// </returns>
        public override string ToString()
        {
            return _Value.ToString();
        }
    }
}
