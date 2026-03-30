using System;

namespace RailTest.Algebra;

/// <summary>
/// Представляет операции в кольце вычетов по модулю.
/// </summary>
public class ModuloIntegers
{
    /// <summary>
    /// Поле для хранения модуля.
    /// </summary>
    private readonly long _Module;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="module">
    /// Модуль кольца.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="module"/> передано отрицательное или равное нулю значение.
    /// </exception>
    public ModuloIntegers(int module)
    {
        if (module <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(module), "Передано отрицательное или равное нулю значение.");
        }
        _Module = module;
    }

    /// <summary>
    /// Возвращает модуль кольца.
    /// </summary>
    public int Module => (int)_Module;

    /// <summary>
    /// Выполняет нормализацию значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо нормализовать.
    /// </param>
    /// <returns>
    /// Нормализованное значение.
    /// </returns>
    public int Normalize(long value)
    {
        return (int)(((value % _Module) + _Module) % _Module);
    }

    /// <summary>
    /// Выполняет нормализацию значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо нормализовать.
    /// </param>
    /// <returns>
    /// Нормализованное значение.
    /// </returns>
    public int Normalize(int value)
    {
        return (int)(((value % _Module) + _Module) % _Module);
    }

    /// <summary>
    /// Выполняет нормализацию значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо нормализовать.
    /// </param>
    /// <returns>
    /// Нормализованное значение.
    /// </returns>
    public int Normalize(short value)
    {
        return (int)(((value % _Module) + _Module) % _Module);
    }

    /// <summary>
    /// Выполняет нормализацию значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо нормализовать.
    /// </param>
    /// <returns>
    /// Нормализованное значение.
    /// </returns>
    [CLSCompliant(false)]
    public int Normalize(sbyte value)
    {
        return (int)(((value % _Module) + _Module) % _Module);
    }

    /// <summary>
    /// Выполняет нормализацию значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо нормализовать.
    /// </param>
    /// <returns>
    /// Нормализованное значение.
    /// </returns>
    [CLSCompliant(false)]
    public int Normalize(ulong value)
    {
        return (int)(value % (ulong)_Module);
    }

    /// <summary>
    /// Выполняет нормализацию значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо нормализовать.
    /// </param>
    /// <returns>
    /// Нормализованное значение.
    /// </returns>
    [CLSCompliant(false)]
    public int Normalize(uint value)
    {
        return (int)(value % (uint)_Module);
    }

    /// <summary>
    /// Выполняет нормализацию значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо нормализовать.
    /// </param>
    /// <returns>
    /// Нормализованное значение.
    /// </returns>
    [CLSCompliant(false)]
    public int Normalize(ushort value)
    {
        return (int)(value % (uint)_Module);
    }

    /// <summary>
    /// Выполняет нормализацию значения.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо нормализовать.
    /// </param>
    /// <returns>
    /// Нормализованное значение.
    /// </returns>
    public int Normalize(byte value)
    {
        return (int)(value % (uint)_Module);
    }

    /// <summary>
    /// Выполняет операцию сложения двух чисел по модулю.
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
    public int Add(int left, int right)
    {
        return (int)((((left + right) % _Module) + _Module) % _Module);
    }

    /// <summary>
    /// Выполняет операцию вычитания двух чисел по модулю.
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
    public int Subtract(int left, int right)
    {
        return (int)((((left - right) % _Module) + _Module) % _Module);
    }
}
