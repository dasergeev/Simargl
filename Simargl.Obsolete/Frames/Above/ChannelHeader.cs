using Simargl.Designing;
using System.Runtime.CompilerServices;

namespace Simargl.Frames;

/// <summary>
/// Представляет заголовок канала.
/// </summary>
public abstract class ChannelHeader :
    ICloneable
{
    /// <summary>
    /// Поле для хранения имени канала.
    /// </summary>
    private string _Name;

    /// <summary>
    /// Поле для хранения единицы измерения.
    /// </summary>
    private string _Unit;

    /// <summary>
    /// Поле для хранения частоты среза фильтра.
    /// </summary>
    private double _Cutoff;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="format">
    /// Значение, определяющее формат хранения элементов кадра регистрации.
    /// </param>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ChannelHeader([NoVerify] StorageFormat format, string name, string unit, double cutoff)
    {
        //  Установка значения, определяющего формат хранения элементов кадра регистрации.
        Format = format;

        //  Установка имени канала.
        _Name = IsNotNull(name, nameof(name));

        //  Установка единицы измерения.
        _Unit = IsNotNull(unit, nameof(unit));

        //  Установка частоты среза фильтра.
        _Cutoff = IsFinite(cutoff, nameof(cutoff));
    }

    /// <summary>
    /// Возвращает значение, определяющее формат хранения элементов кадра регистрации.
    /// </summary>
    public StorageFormat Format { get; }

    /// <summary>
    /// Возвращает или задаёт имя канала.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Name
    {
        get => _Name;
        set => _Name = IsNotNull(value, nameof(Name));
    }

    /// <summary>
    /// Возвращает или задаёт единицу измерения.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Unit
    {
        get => _Unit;
        set => _Unit = IsNotNull(value, nameof(Unit));
    }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    public double Cutoff
    {
        get => _Cutoff;
        set => _Cutoff = IsFinite(value, nameof(Cutoff));
    }

    /// <summary>
    /// Создаёт копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public abstract ChannelHeader Clone();

    /// <summary>
    /// Возвращает копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    object ICloneable.Clone() => Clone();
}
