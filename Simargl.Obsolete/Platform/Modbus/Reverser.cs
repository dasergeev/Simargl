using System;
using System.Runtime.CompilerServices;

namespace Simargl.Platform.Modbus;

/// <summary>
/// Предоставляет методы для преобразования значений из байтового формата архитектуры компьютера в сетевой порядок и обратно.
/// </summary>
public static class Reverser
{
    /// <summary>
    /// Преобразует целое значение.
    /// </summary>
    /// <param name="value">
    /// Значение для преобразования.
    /// </param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    public static short Reverse(short value)
    {
        //  Проверка необходимости выполнения преобразования.
        if (BitConverter.IsLittleEndian)
        {
            //  Преобразование значения.
            return unchecked((short)ReverseCore(unchecked((ushort)value)));
        }
        else
        {
            //  Возврат исходного значения.
            return value;
        }
    }

    /// <summary>
    /// Преобразует целое значение.
    /// </summary>
    /// <param name="value">
    /// Значение для преобразования.
    /// </param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    [CLSCompliant(false)]
    public static ushort Reverse(ushort value)
    {
        //  Проверка необходимости выполнения преобразования.
        if (BitConverter.IsLittleEndian)
        {
            //  Преобразование значения.
            return ReverseCore(value);
        }
        else
        {
            //  Возврат исходного значения.
            return value;
        }
    }

    /// <summary>
    /// Преобразует целое значение без проверки архитектуры компьютера.
    /// </summary>
    /// <param name="value">
    /// Значение для преобразования.
    /// </param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static ushort ReverseCore(ushort value)
    {
        //  Изменение порядка следования байт.
        return unchecked((ushort)(((value & 0xFF) << 8) | (value >> 8) & 0xFF));
    }

    /// <summary>
    /// Преобразует целое значение без проверки архитектуры компьютера.
    /// </summary>
    /// <param name="value">
    /// Значение для преобразования.
    /// </param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static uint ReverseCore(uint value)
    {
        //  Изменение порядка следования байт.
        return unchecked((uint)(((value & 0xFFFF) << 16) | (value >> 16) & 0xFFFF));
    }

    /// <summary>
    /// Преобразует целое значение без проверки архитектуры компьютера.
    /// </summary>
    /// <param name="value">
    /// Значение для преобразования.
    /// </param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static int ReverseCore(int value)
    {
        //  Изменение порядка следования байт.
        return unchecked((int)(((value & 0xFFFF) << 16) | (value >> 16) & 0xFFFF));
    }

    /// <summary>
    /// Преобразует целое значение без проверки архитектуры компьютера.
    /// </summary>
    /// <param name="value">
    /// Значение для преобразования.
    /// </param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static float ReverseCore(float value)
    {
        //  Преобразование в Uint
        var temp = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);

        //  Перестановка.
        temp = ReverseCore(temp);

        //  Изменение порядка следования байт.
        return BitConverter.ToSingle(BitConverter.GetBytes(temp),0);
    }

    /// <summary>
    /// Преобразует массив как массив ushort.
    /// </summary>
    /// <param name="array">
    /// Источник.
    /// </param>
    /// <returns>
    /// Результат
    /// </returns>
    public static byte[] Reverse(byte[] array)
    {
        //  Проверка порядка байт на машине
        if (BitConverter.IsLittleEndian == false)
        {
            //  Вовзрат данных
            return array;
        }

        //  Выделение памяти
        var data = new byte[array.Length];

        //  Цикл массива
        for (int i = 0; i < array.Length; i += 2)
        {
            //  Преобразование
            data[i] = array[i + 1];
            data[i + 1] = array[i];
        }

        //  Возврат полученного массива.
        return data;
    }
}
