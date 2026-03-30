using System;

namespace Simargl.Frames.Simple;

/// <summary>
/// Представляет заголовок канала в формате <see cref="StorageFormat.Simple"/>.
/// </summary>
/// <param name="name">
/// Имя канала.
/// </param>
/// <param name="unit">
/// Единица измерения.
/// </param>
/// <param name="cutoff">
/// Частота среза фильтра.
/// </param>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="name"/> передана пустая ссылка.
/// </exception>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="unit"/> передана пустая ссылка.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="cutoff"/> передано нечисловое значение.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="cutoff"/> передано бесконечное значение.
/// </exception>
public sealed class SimpleChannelHeader(string name, string unit, double cutoff) :
    ChannelHeader(StorageFormat.Simple, name, unit, cutoff)
{
    /// <summary>
    /// Создаёт копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public override ChannelHeader Clone()
    {
        //  Создание и возврат копии заголовка.
        return new SimpleChannelHeader(Name, Unit, Cutoff);
    }
}
