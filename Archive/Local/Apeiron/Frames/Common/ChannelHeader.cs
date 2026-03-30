using Apeiron.Frames.Catman;
using Apeiron.Frames.TestLab;
using System.Runtime.CompilerServices;

namespace Apeiron.Frames;

/// <summary>
/// Представляет заголовок канала.
/// </summary>
public class ChannelHeader
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
    public ChannelHeader() :
        this(StorageFormat.Simple)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
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
    internal ChannelHeader(string name, string unit, double cutoff) :
        this(StorageFormat.Simple, name, unit, cutoff)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="format">
    /// Формат кадра.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="format"/> передано значение,
    /// которое не содержится в перечислении <see cref="StorageFormat"/>.
    /// </exception>
    internal ChannelHeader(StorageFormat format) :
        this(format, string.Empty, string.Empty, 0)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="format">
    /// Формат кадра.
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
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="format"/> передано значение,
    /// которое не содержится в перечислении <see cref="StorageFormat"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="unit"/> передана пустая ссылка.
    /// </exception>
    internal ChannelHeader(StorageFormat format, string name, string unit, double cutoff)
    {
        //  Установка формата.
        Format = IsDefined(format, nameof(format));

        //  Установка имени канала.
        _Name = IsNotNull(name, nameof(name));

        //  Установка единицы измерения.
        _Unit = IsNotNull(unit, nameof(unit));
        
        //  Установка частоты среза фильтра.
        _Cutoff = cutoff;
    }

    /// <summary>
    /// Возвращает формат кадра.
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
    public double Cutoff
    {
        get => _Cutoff;
        set => _Cutoff = value;
    }

    /// <summary>
    /// Создаёт копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public virtual ChannelHeader Clone()
    {
        if (Format != StorageFormat.Simple)
        {
            throw new NotSupportedException("При попытке создать копию заголовка канала не была найдена функция, реализующая копирование.");
        }
        return new ChannelHeader(Name, Unit, Cutoff);
    }

    /// <summary>
    /// Возвращает заголовок кадра в другом формате.
    /// </summary>
    /// <param name="format">
    /// Формат заголовка канала, в который необходимо преобразовать текущий объект.
    /// </param>
    /// <returns>
    /// Преобразованный объект.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Произошла попытка преобразовать заголовок канала в неизвестный формат.
    /// </exception>
    internal virtual ChannelHeader Convert(StorageFormat format)
    {
        if (format == Format)
        {
            return Clone();
        }
        switch (format)
        {
            case StorageFormat.Simple:
                return new ChannelHeader(Name, Unit, Cutoff);
            case StorageFormat.TestLab:
                {
                    return new TestLabChannelHeader()
                    {
                        Name = Name,
                        Unit = Unit,
                        Cutoff = Cutoff,
                    };
                }
            case StorageFormat.Catman:
                {
                    return new CatmanChannelHeader()
                    {
                        Name = Name,
                        Unit = Unit,
                        Cutoff = Cutoff,
                    };
                }
            default:
                throw new ArgumentOutOfRangeException(nameof(format),
                    "Произошла попытка преобразовать заголовок канала в неизвестный формат.");
        }
    }
}
