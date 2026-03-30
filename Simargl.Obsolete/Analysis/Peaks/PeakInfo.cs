using System.ComponentModel;

namespace Simargl.Analysis.Peaks;

/// <summary>
/// Представляет информацию о пике.
/// </summary>
public sealed class PeakInfo :
    INotifyPropertyChanged,
    ICloneable
{
    /// <summary>
    /// Происходит при изменении свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="FileName"/>.
    /// </summary>
    public event EventHandler? FileNameChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="Region"/>.
    /// </summary>
    public event EventHandler? RegionChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="Speed"/>.
    /// </summary>
    public event EventHandler? SpeedChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="Direction"/>.
    /// </summary>
    public event EventHandler? DirectionChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="Rail"/>.
    /// </summary>
    public event EventHandler? RailChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="Section"/>.
    /// </summary>
    public event EventHandler? SectionChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="Axis"/>.
    /// </summary>
    public event EventHandler? AxisChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="ChannelGroup"/>.
    /// </summary>
    public event EventHandler? ChannelGroupChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="Value"/>.
    /// </summary>
    public event EventHandler? ValueChanged;

    /// <summary>
    /// Поле для хранения имени файла.
    /// </summary>
    string _FileName;

    /// <summary>
    /// Поле для хранения названия участка.
    /// </summary>
    string _Region;

    /// <summary>
    /// Поле для хранения скорости движения.
    /// </summary>
    double _Speed;

    /// <summary>
    /// Поле для хранения значения, определяющего направление движения.
    /// </summary>
    Direction _Direction;

    /// <summary>
    /// Поле для хранения значения, определяющего рельс.
    /// </summary>
    Rail _Rail;

    /// <summary>
    /// Поле для хранения номера сечения.
    /// </summary>
    int _Section;

    /// <summary>
    /// Поле для хранения номера оси.
    /// </summary>
    int _Axis;

    /// <summary>
    /// Поле для хранения значения, определяющего группу каналов.
    /// </summary>
    ChannelGroup _ChannelGroup;

    /// <summary>
    /// Поле для хранения значения пика.
    /// </summary>
    double _Value;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public PeakInfo()
    {
        _FileName = string.Empty;
        _Region = string.Empty;
        _Speed = 0;
        _Direction = Direction.Direct;
        _Rail = Rail.Inner;
        _Section = 0;
        _Axis = 0;
        _ChannelGroup = ChannelGroup.Unknown;
        _Value = 0;
    }

    /// <summary>
    /// Возвращает или задаёт имя файла.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    public string FileName
    {
        get => _FileName;
        set
        {
            IsNotNull(value, nameof(FileName));
            if (_FileName != value)
            {
                _FileName = value;
                OnFileNameChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт название участка.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    public string Region
    {
        get => _Region;
        set
        {
            IsNotNull(value, nameof(Region));
            if (_Region != value)
            {
                _Region = value;
                OnRegionChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт скорость движения.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    public double Speed
    {
        get => _Speed;
        set
        {
            IsNotNegative(value, nameof(Speed));
            if (_Speed != value)
            {
                _Speed = value;
                OnSpeedChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее направление движения.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение, которое не содержится в перечислении <see cref="Direction"/>.
    /// </exception>
    public Direction Direction
    {
        get => _Direction;
        set
        {
            IsDefined(value, nameof(Direction));
            if (_Direction != value)
            {
                _Direction = value;
                OnDirectionChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее рельс.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение, которое не содержится в перечислении <see cref="Rail"/>.
    /// </exception>
    public Rail Rail
    {
        get => _Rail;
        set
        {
            IsDefined(value, nameof(Rail));
            if (_Rail != value)
            {
                _Rail = value;
                OnRailChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт номер сечения.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    public int Section
    {
        get => _Section;
        set
        {
            IsNotNegative(value, nameof(Section));
            if (_Section != value)
            {
                _Section = value;
                OnSectionChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт номер сечения.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    public int Axis
    {
        get => _Axis;
        set
        {
            IsNotNegative(value, nameof(Axis));
            if (_Axis != value)
            {
                _Axis = value;
                OnAxisChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее группу каналов.
    /// </summary>
    public ChannelGroup ChannelGroup
    {
        get => _ChannelGroup;
        set
        {
            if (_ChannelGroup != value)
            {
                _ChannelGroup = value;
                OnChannelGroupChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение пика.
    /// </summary>
    public double Value
    {
        get => _Value;
        set
        {
            if (_Value != value)
            {
                _Value = value;
                OnValueChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Выполняет операцию проверки на равенство.
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    public static bool operator ==(PeakInfo left, PeakInfo right)
    {
        IsNotNull(left, nameof(left));
        IsNotNull(right, nameof(right));
        return left._FileName == right._FileName &&
            left._Region == right._Region &&
            left._Speed == right._Speed &&
            left._Direction == right._Direction &&
            left._Rail == right._Rail &&
            left._Section == right._Section &&
            left._Axis == right._Axis &&
            left._ChannelGroup == right._ChannelGroup &&
            left._Value == right._Value;
    }

    /// <summary>
    /// Выполняет операцию проверки на неравенство.
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    public static bool operator !=(PeakInfo left, PeakInfo right)
    {
        IsNotNull(left, nameof(left));
        IsNotNull(right, nameof(right));
        return left._FileName != right._FileName ||
            left._Region != right._Region ||
            left._Speed != right._Speed ||
            left._Direction != right._Direction ||
            left._Rail != right._Rail ||
            left._Section != right._Section ||
            left._Axis != right._Axis ||
            left._ChannelGroup != right._ChannelGroup ||
            left._Value != right._Value;
    }

    /// <summary>
    /// Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Новый объект, являющийся копией этого экземпляра.
    /// </returns>
    public PeakInfo Clone()
    {
        return new PeakInfo
        {
            FileName = _FileName,
            Region = _Region,
            Speed = _Speed,
            Direction = _Direction,
            Rail = _Rail,
            Section = _Section,
            Axis = _Axis,
            ChannelGroup = _ChannelGroup,
            Value = _Value,
        };
    }

    /// <summary>
    /// Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Новый объект, являющийся копией этого экземпляра.
    /// </returns>
    object ICloneable.Clone()
    {
        return Clone();
    }

    /// <summary>
    /// Определяет, равен ли заданный объект текущему объекту.
    /// </summary>
    /// <param name="obj">
    /// Объект, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj is PeakInfo peakInfo)
        {
            return _FileName == peakInfo._FileName &&
                _Region == peakInfo._Region &&
                _Speed == peakInfo._Speed &&
                _Direction == peakInfo._Direction &&
                _Rail == peakInfo._Rail &&
                _Section == peakInfo._Section &&
                _Axis == peakInfo._Axis &&
                _ChannelGroup == peakInfo._ChannelGroup &&
                _Value == peakInfo._Value;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Возвращает строку, представляющую текущий объект.
    /// </summary>
    /// <returns>
    /// Строка, представляющая текущий объект.
    /// </returns>
    public override string ToString()
    {
        return $"{_FileName} V{_Speed:0.0}";
    }

    /// <summary>
    /// Служит хэш-функцией по умолчанию.
    /// </summary>
    /// <returns>
    /// Хэш-код для текущего объекта.
    /// </returns>
    public override int GetHashCode()
    {
        return _Value.GetHashCode();
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
    /// Вызывает событие <see cref="FileNameChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnFileNameChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(FileName)));
        FileNameChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="RegionChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnRegionChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Region)));
        RegionChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="SpeedChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnSpeedChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Speed)));
        SpeedChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="DirectionChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnDirectionChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Direction)));
        DirectionChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="RailChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnRailChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Rail)));
        RailChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="SectionChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnSectionChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Section)));
        SectionChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="AxisChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnAxisChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Axis)));
        AxisChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="ChannelGroupChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnChannelGroupChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(ChannelGroup)));
        ChannelGroupChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="ValueChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnValueChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
        ValueChanged?.Invoke(this, e);
    }
}
