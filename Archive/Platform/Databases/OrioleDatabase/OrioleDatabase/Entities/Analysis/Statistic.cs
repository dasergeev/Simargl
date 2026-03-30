using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет статистические данные.
/// </summary>
public class Statistic :
    Entity
{
    /// <summary>
    /// Постоянная, определяющая имя столбца таблиц базы данных,
    /// содержащего метку времени получения данных кадра.
    /// </summary>
    private const string _TimestampName = @"Timestamp";

    /// <summary>
    /// Поле для хранения идентификатора канала.
    /// </summary>
    private int _ChannelId;

    /// <summary>
    /// Поле для хранения идентификатора регистратора.
    /// </summary>
    private int _RegistrarId;

    /// <summary>
    /// Поле для хранения метки времени получения данных кадра.
    /// </summary>
    public long _Timestamp;

    /// <summary>
    /// Поле для хранения частоты среза фильтра.
    /// </summary>
    private double _Cutoff;

    /// <summary>
    /// Поле для хранения регистратора.
    /// </summary>
    private readonly Registrar _Registrar = null!;

    /// <summary>
    /// Поле для хранения канала.
    /// </summary>
    private readonly Channel _Channel = null!;

    /// <summary>
    /// Поле для хранения кадра.
    /// </summary>
    private readonly Frame _Frame = null!;

    /// <summary>
    /// Поле для хранения фильтра.
    /// </summary>
    private readonly Filter _Filter = null!;

    /// <summary>
    /// Поле для хранения широты.
    /// </summary>
    private double _Latitude;

    /// <summary>
    /// Поле для хранения долготы.
    /// </summary>
    private double _Longitude;

    /// <summary>
    /// Поле для хранения местоположения.
    /// </summary>
    private readonly Location _Location = null!;

    /// <summary>
    /// Поле для хранения скорости.
    /// </summary>
    private double _Speed;

    /// <summary>
    /// Поле для хранения количества значений.
    /// </summary>
    private int _Count;

    /// <summary>
    /// Поле для хранения минимального значения.
    /// </summary>
    private double _Min;

    /// <summary>
    /// Поле для хранения максимального значения.
    /// </summary>
    private double _Max;

    /// <summary>
    /// Поле для хранения среднего значения.
    /// </summary>
    private double _Average;

    /// <summary>
    /// Поле для хранения среднеквадратического отклонения.
    /// </summary>
    private double _Deviation;

    /// <summary>
    /// Поле для хранения суммы значений.
    /// </summary>
    private double _Sum;

    /// <summary>
    /// Поле для хранения суммы квадратов значений.
    /// </summary>
    private double _SquaresSum;

    /// <summary>
    /// Поле для хранения минимального значения по модулю.
    /// </summary>
    private double _MinModulo;

    /// <summary>
    /// Поле для хранения максимального значения по модулю.
    /// </summary>
    private double _MaxModulo;

    /// <summary>
    /// Поле для хранения среднего значения по модулю.
    /// </summary>
    private double _AverageModulo;

    /// <summary>
    /// Поле для хранения среднеквадратического отклонения по модулю.
    /// </summary>
    private double _DeviationModulo;

    /// <summary>
    /// Поле для хранения суммы значений по модулю.
    /// </summary>
    private double _SumModulo;

    /// <summary>
    /// Возвращает или задаёт идентификатор канала.
    /// </summary>
    public int ChannelId
    {
        get => _ChannelId;
        set => SetProperty(nameof(ChannelId), ref _ChannelId, value);
    }

    /// <summary>
    /// Возвращает или задаёт идентификатор регистратора.
    /// </summary>
    public int RegistrarId
    {
        get => _RegistrarId;
        set => SetProperty(nameof(RegistrarId), ref _RegistrarId, value);
    }

    /// <summary>
    /// Возвращает или задаёт время получения данных кадра.
    /// </summary>
    public DateTime FrameTime
    {
        get => Frame.FromTimestamp(_Timestamp);
        set => SetProperty(_TimestampName, ref _Timestamp, Frame.ToTimestamp(value));
    }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public double Cutoff
    {
        get => _Cutoff;
        set => SetProperty(nameof(Cutoff), ref _Cutoff, value);
    }

    /// <summary>
    /// Возвращает или задаёт широту в градусах.
    /// </summary>
    public double Latitude
    {
        get => _Latitude;
        set => SetProperty(nameof(Latitude), ref _Latitude, value);
    }

    /// <summary>
    /// Возвращает или задаёт долготу в градусах.
    /// </summary>
    public double Longitude
    {
        get => _Longitude;
        set => SetProperty(nameof(Longitude), ref _Longitude, value);
    }

    /// <summary>
    /// Возвращает или задаёт скорость.
    /// </summary>
    public double Speed
    {
        get => _Speed;
        set => SetProperty(nameof(Speed), ref _Speed, value);
    }

    /// <summary>
    /// Возвращает или задаёт количество значений.
    /// </summary>
    public int Count
    {
        get => _Count;
        set => SetProperty(nameof(Count), ref _Count, value);
    }

    /// <summary>
    /// Возвращает или задаёт минимальное значение.
    /// </summary>
    public double Min
    {
        get => _Min;
        set => SetProperty(nameof(Min), ref _Min, value);
    }

    /// <summary>
    /// Возвращает или задаёт максимальное значение.
    /// </summary>
    public double Max
    {
        get => _Max;
        set => SetProperty(nameof(Max), ref _Max, value);
    }

    /// <summary>
    /// Возвращает или задаёт среднее значение.
    /// </summary>
    public double Average
    {
        get => _Average;
        set => SetProperty(nameof(Average), ref _Average, value);
    }

    /// <summary>
    /// Возвращает или задаёт среднеквадратическое отклонение.
    /// </summary>
    public double Deviation
    {
        get => _Deviation;
        set => SetProperty(nameof(Deviation), ref _Deviation, value);
    }

    /// <summary>
    /// Возвращает или задаёт сумму значений.
    /// </summary>
    public double Sum
    {
        get => _Sum;
        set => SetProperty(nameof(Sum), ref _Sum, value);
    }

    /// <summary>
    /// Возвращает или задаёт сумму квадратов значений.
    /// </summary>
    public double SquaresSum
    {
        get => _SquaresSum;
        set => SetProperty(nameof(SquaresSum), ref _SquaresSum, value);
    }

    /// <summary>
    /// Возвращает или задаёт минимальное значение по модулю.
    /// </summary>
    public double MinModulo
    {
        get => _MinModulo;
        set => SetProperty(nameof(MinModulo), ref _MinModulo, value);
    }

    /// <summary>
    /// Возвращает или задаёт максимальное значение по модулю.
    /// </summary>
    public double MaxModulo
    {
        get => _MaxModulo;
        set => SetProperty(nameof(MaxModulo), ref _MaxModulo, value);
    }

    /// <summary>
    /// Возвращает или задаёт среднее значение по модулю.
    /// </summary>
    public double AverageModulo
    {
        get => _AverageModulo;
        set => SetProperty(nameof(AverageModulo), ref _AverageModulo, value);
    }

    /// <summary>
    /// Возвращает или задаёт среднеквадратическое отклонение по модулю.
    /// </summary>
    public double DeviationModulo
    {
        get => _DeviationModulo;
        set => SetProperty(nameof(DeviationModulo), ref _DeviationModulo, value);
    }

    /// <summary>
    /// Возвращает или задаёт сумму значений по модулю.
    /// </summary>
    public double SumModulo
    {
        get => _SumModulo;
        set => SetProperty(nameof(SumModulo), ref _SumModulo, value);
    }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<Statistic> typeBuilder)
    {
        //  Базовая настройка.
        Entity.BuildAction(typeBuilder);

        //  Настройка идентификатора канала.
        typeBuilder.Property(statistic => statistic.ChannelId);
        typeBuilder.HasIndex(statistic => statistic.ChannelId);

        //  Настройка идентификатора регистратора.
        typeBuilder.Property(statistic => statistic.RegistrarId);
        typeBuilder.HasIndex(statistic => statistic.RegistrarId);

        //  Настройка метки времени получения данных кадра.
        typeBuilder.Property(statistic => statistic._Timestamp)
            .HasColumnName(_TimestampName);
        typeBuilder.HasIndex(statistic => statistic._Timestamp);
        typeBuilder.Ignore(statistic => statistic.FrameTime);

        //  Настройка частоты среза фильтра.
        typeBuilder.Property(statistic => statistic.Cutoff);
        typeBuilder.HasIndex(statistic => statistic.Cutoff);

        //  Настройка альтернативного ключа.
        typeBuilder.HasAlternateKey(statistic => new
        {
            statistic.ChannelId,
            statistic.RegistrarId,
            Timestamp = statistic._Timestamp,
            statistic.Cutoff
        });

        //  Настройка регистратора.
        typeBuilder.HasOne(statistic => statistic._Registrar)
            .WithMany(registrar => registrar.Statistics)
            .HasForeignKey(statistic => statistic.RegistrarId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Statistics_Registrars");

        //  Настройка канала.
        typeBuilder.HasOne(statistic => statistic._Channel)
            .WithMany(channel => channel.Statistics)
            .HasForeignKey(statistic => statistic.ChannelId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Statistics_Channels");

        //  Настройка кадра.
        typeBuilder.HasOne(statistic => statistic._Frame)
            .WithMany(frame => frame.Statistics)
            .HasForeignKey(statistic => new
            {
                statistic.RegistrarId,
                Timestamp = statistic._Timestamp,
            })
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Statistics_Frames");

        //  Настройка фильтра.
        typeBuilder.HasOne(statistic => statistic._Filter)
            .WithMany(filter => filter.Statistics)
            .HasForeignKey(statistic => statistic.Cutoff)
            .HasPrincipalKey(filter => filter.Cutoff)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Statistics_Filters");

        //  Настройка широты.
        typeBuilder.Property(statistic => statistic.Latitude);
        typeBuilder.HasIndex(statistic => statistic.Latitude);

        //  Настройка долготы.
        typeBuilder.Property(statistic => statistic.Longitude);
        typeBuilder.HasIndex(statistic => statistic.Longitude);

        //  Настройка местоположения.
        typeBuilder.HasOne(statistic => statistic._Location)
            .WithMany(location => location.Statistics)
            .HasForeignKey(statistic => new
            {
                statistic.Latitude,
                statistic.Longitude,
            })
            .HasPrincipalKey(location => new
            {
                location.Latitude,
                location.Longitude,
            })
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Statistics_Locations");

        //  Настройка скорости.
        typeBuilder.Property(statistic => statistic.Speed);
        typeBuilder.HasIndex(statistic => statistic.Speed);

        //  Настройка количества значений.
        typeBuilder.Property(statistic => statistic.Count);
        typeBuilder.HasIndex(statistic => statistic.Count);

        //  Настройка минимального значения.
        typeBuilder.Property(statistic => statistic.Min);
        typeBuilder.HasIndex(statistic => statistic.Min);

        //  Настройка максимального значения.
        typeBuilder.Property(statistic => statistic.Max);
        typeBuilder.HasIndex(statistic => statistic.Max);

        //  Настройка среднего значения.
        typeBuilder.Property(statistic => statistic.Average);
        typeBuilder.HasIndex(statistic => statistic.Average);

        //  Настройка среднеквадратического отклонения.
        typeBuilder.Property(statistic => statistic.Deviation);
        typeBuilder.HasIndex(statistic => statistic.Deviation);

        //  Настройка суммы значений.
        typeBuilder.Property(statistic => statistic.Sum);
        typeBuilder.HasIndex(statistic => statistic.Sum);

        //  Настройка суммы квадратов значений.
        typeBuilder.Property(statistic => statistic.SquaresSum);
        typeBuilder.HasIndex(statistic => statistic.SquaresSum);

        //  Настройка минимального значения по модулю.
        typeBuilder.Property(statistic => statistic.MinModulo);
        typeBuilder.HasIndex(statistic => statistic.MinModulo);

        //  Настройка максимального значения по модулю.
        typeBuilder.Property(statistic => statistic.MaxModulo);
        typeBuilder.HasIndex(statistic => statistic.MaxModulo);

        //  Настройка среднего значения по модулю.
        typeBuilder.Property(statistic => statistic.AverageModulo);
        typeBuilder.HasIndex(statistic => statistic.AverageModulo);

        //  Настройка среднеквадратического отклонения по модулю.
        typeBuilder.Property(statistic => statistic.DeviationModulo);
        typeBuilder.HasIndex(statistic => statistic.DeviationModulo);

        //  Настройка суммы значений по модулю.
        typeBuilder.Property(statistic => statistic.SumModulo);
        typeBuilder.HasIndex(statistic => statistic.SumModulo);
    }
}
