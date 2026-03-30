using Simargl.Designing;
using System;

namespace Simargl.Frames.Mera;

/// <summary>
/// Представляет информацию о порции записи при прерывистой записи.
/// </summary>
public sealed class MeraPortionInfo
{
    /// <summary>
    /// Поле для хранения смещения данных порции записи.
    /// </summary>
    private int _Offset = 0;

    /// <summary>
    /// Возвращает или задаёт время начала порции записи.
    /// </summary>
    public TimeSpan Begin { get; set; }

    /// <summary>
    /// Возвращает или задаёт смещение данных порции записи.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    public int Offset
    {
        get => _Offset;
        set => _Offset = IsNotNegative(value, nameof(Offset));
    }
}
