using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Text;

namespace Simargl.Analysis.Peaks;

/// <summary>
/// Представляет хранилище пиков.
/// </summary>
public sealed class PeakStorage :
    INotifyPropertyChanged,
    ICloneable
{
    /// <summary>
    /// Постоянная, определяющая первую часть сигнатуры файла.
    /// </summary>
    const ulong FirstSignature = 0xC8A27F50AA514AA6;

    /// <summary>
    /// Постоянная, определяющая вторую часть сигнатуры файла.
    /// </summary>
    const ulong SecondSignature = 0xBE218572AAB2EDA3;

    /// <summary>
    /// Происходит при изменении свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="TestTime"/>.
    /// </summary>
    public event EventHandler? TestTimeChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="FormationTime"/>.
    /// </summary>
    public event EventHandler? FormationTimeChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="TestObject"/>.
    /// </summary>
    public event EventHandler? TestObjectChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="Location"/>.
    /// </summary>
    public event EventHandler? LocationChanged;

    /// <summary>
    /// Поле для хранения времени проведения испытаний.
    /// </summary>
    DateTime _TestTime;

    /// <summary>
    /// Поле для хранения времени формирования хранилища.
    /// </summary>
    DateTime _FormationTime;

    /// <summary>
    /// Поле для хранения названия объекта испытаний.
    /// </summary>
    string _TestObject;

    /// <summary>
    /// Поле для хранения названия места проведения испытаний.
    /// </summary>
    string _Location;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public PeakStorage() :
        this(new PeakInfoCollection())
    {

        
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="peaks">
    /// Коллекция информации о пиках.
    /// </param>
    PeakStorage(PeakInfoCollection peaks)
    {
        _TestTime = DateTime.Now;
        _FormationTime = DateTime.Now;
        _TestObject = string.Empty;
        _Location = string.Empty;
        Peaks = peaks;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая сслыка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="path"/> передано значение,
    /// которое представляет собой пустую строку (""),
    /// содержащую только пробел или хотя бы один недопустимый символ.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="path"/> передано значение,
    /// которое ссылается на устройство нефайлового типа,
    /// например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// В параметре <paramref name="path"/> передано значение,
    /// которое ссылается на устройство нефайлового типа,
    /// например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="FileNotFoundException">
    /// Не удается найти файл, например когда заданный параметром <paramref name="path"/> путь, не существует.
    /// </exception>
    /// <exception cref="IOException">
    /// Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="SecurityException">
    /// У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="DirectoryNotFoundException">
    /// Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="UnauthorizedAccessException">
    /// Запрошенный доступ не поддерживается операционной системой для указанного пути,
    /// например когда для файла или каталога установлен доступ только для чтения.
    /// </exception>
    /// <exception cref="PathTooLongException">
    /// Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    /// </exception>
    /// <exception cref="FormatException">
    /// Неизвестный формат файла.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// Версия формата файла не поддерживается.
    /// </exception>
    public PeakStorage(string path) :
        this()
    {
        if (path is null)
        {
            throw new ArgumentNullException(nameof(path), "Передана пустая сслыка.");
        }

        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var reader = new BinaryReader(stream, Encoding.UTF8, true);
        try
        {
            //  Сигнатура.
            if (reader.ReadUInt64() != FirstSignature & reader.ReadUInt64() != SecondSignature)
            {
                throw new FormatException("Неизвестный формат файла.");
            }

            //  Версия.
            if (reader.ReadUInt16() != 1)
            {
                throw new NotSupportedException("Версия формата файла не поддерживается.");
            }

            //  Заголовок файла.
            _TestTime = DateTime.FromBinary(reader.ReadInt64());
            _FormationTime = DateTime.FromBinary(reader.ReadInt64());
            _TestObject = new string(reader.ReadChars(reader.ReadInt32()));
            _Location = new string(reader.ReadChars(reader.ReadInt32()));

            int countPeaks = reader.ReadInt32();
            if (countPeaks < 0)
            {
                throw new FormatException("Неизвестный формат файла.");
            }

            //  Элементы.
            for (int i = 0; i != countPeaks; ++i)
            {
                Peaks.Add(new PeakInfo
                {
                    FileName = new string(reader.ReadChars(reader.ReadInt32())),
                    Region = new string(reader.ReadChars(reader.ReadInt32())),
                    Speed = reader.ReadDouble(),
                    Direction = (Direction)reader.ReadInt32(),
                    Rail = (Rail)reader.ReadInt32(),
                    Section = reader.ReadInt32(),
                    Axis = reader.ReadInt32(),
                    ChannelGroup = (ChannelGroup)reader.ReadInt32(),
                    Value = reader.ReadDouble()
                });
            }
        }
        catch (ArgumentException)
        {
            throw new FormatException("Неизвестный формат файла.");
        }
        catch (EndOfStreamException)
        {
            throw new FormatException("Неизвестный формат файла.");
        }
    }

    /// <summary>
    /// Возвращает или задаёт время проведения испытаний.
    /// </summary>
    public DateTime TestTime
    {
        get => _TestTime;
        set
        {
            if (_TestTime != value)
            {
                _TestTime = value;
                OnTestTimeChanged(EventArgs.Empty);
            }    
        }
    }

    /// <summary>
    /// Возвращает или задаёт время формирования хранилища.
    /// </summary>
    public DateTime FormationTime
    {
        get => _FormationTime;
        set
        {
            if (_FormationTime != value)
            {
                _FormationTime = value;
                OnFormationTimeChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт название объекта испытаний.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    public string TestObject
    {
        get => _TestObject;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Передана пустая ссылка");
            }
            if (_TestObject != value)
            {
                _TestObject = value;
                OnTestObjectChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт название места проведения испытаний.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    public string Location
    {
        get => _Location;
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Передана пустая ссылка");
            }
            if (_Location != value)
            {
                _Location = value;
                OnLocationChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает коллекцию информации о пиках.
    /// </summary>
    public PeakInfoCollection Peaks { get; }

    /// <summary>
    /// Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Новый объект, являющийся копией этого экземпляра.
    /// </returns>
    public PeakStorage Clone()
    {
        return new PeakStorage(Peaks.Clone())
        {
            TestTime = _TestTime,
            FormationTime = _FormationTime,
            TestObject = _TestObject,
            Location = _Location,
        };
    }

    /// <summary>
    /// Сохраняет объект в файл.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая сслыка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="path"/> передано значение,
    /// которое представляет собой пустую строку (""),
    /// содержащую только пробел или хотя бы один недопустимый символ.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="path"/> передано значение,
    /// которое ссылается на устройство нефайлового типа,
    /// например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// В параметре <paramref name="path"/> передано значение,
    /// которое ссылается на устройство нефайлового типа,
    /// например "con:", "com1:", "lpt1:" и т. д., в среде NTFS.
    /// </exception>
    /// <exception cref="IOException">
    /// Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="SecurityException">
    /// У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    /// <exception cref="DirectoryNotFoundException">
    /// Указан недопустимый путь (например, ведущий на несопоставленный диск).
    /// </exception>
    /// <exception cref="UnauthorizedAccessException">
    /// Запрошенный доступ не поддерживается операционной системой для указанного пути,
    /// например когда для файла или каталога установлен доступ только для чтения.
    /// </exception>
    /// <exception cref="PathTooLongException">
    /// Указанный путь, имя файла или оба значения превышают максимальную длину, заданную в системе.
    /// </exception>
    public void Save(string path)
    {
        if (path is null)
        {
            throw new ArgumentNullException(nameof(path), "Передана пустая сслыка.");
        }
        using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        using var writer = new BinaryWriter(stream, Encoding.UTF8, true);
        //  Сигнатура.
        writer.Write(FirstSignature);
        writer.Write(SecondSignature);

        //  Версия.
        writer.Write((ushort)1);

        //  Заголовок файла.
        writer.Write(_TestTime.ToBinary());
        writer.Write(_FormationTime.ToBinary());
        writer.Write(_TestObject.Length);
        writer.Write(_TestObject.ToCharArray());
        writer.Write(_Location.Length);
        writer.Write(_Location.ToCharArray());
        writer.Write(Peaks.Count);

        //  Элементы.
        foreach (var peak in Peaks)
        {
            writer.Write(peak.FileName.Length);
            writer.Write(peak.FileName.ToCharArray());
            writer.Write(peak.Region.Length);
            writer.Write(peak.Region.ToCharArray());
            writer.Write(peak.Speed);
            writer.Write((int)peak.Direction);
            writer.Write((int)peak.Rail);
            writer.Write(peak.Section);
            writer.Write(peak.Axis);
            writer.Write((int)peak.ChannelGroup);
            writer.Write(peak.Value);
        }
    }

    /// <summary>
    /// Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Новый объект, являющийся копией этого экземпляра.
    /// </returns>
    object ICloneable.Clone()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="TestTimeChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnTestTimeChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(TestTime)));
        TestTimeChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="FormationTimeChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnFormationTimeChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(FormationTime)));
        FormationTimeChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="TestObjectChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnTestObjectChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(TestObject)));
        TestObjectChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="LocationChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnLocationChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Location)));
        LocationChanged?.Invoke(this, e);
    }
}
