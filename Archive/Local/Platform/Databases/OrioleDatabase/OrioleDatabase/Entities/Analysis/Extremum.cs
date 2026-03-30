using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет информацию об экстремуме.
/// </summary>
public sealed class Extremum
{
    /// <summary>
    /// Возвращает или инициализирует идентификатор информации.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Возвращает или задаёт идентификатор регистратора.
    /// </summary>
    public int RegistrarId { get; set; }

    /// <summary>
    /// Возвращает или задаёт метку времени получения данных кадра.
    /// </summary>
    public long FrameTimestamp { get; set; }

    /// <summary>
    /// Возвращает или инициализирует идентификатор канала.
    /// </summary>
    public int ChannelId { get; init; }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public double Cutoff { get; set; }

    /// <summary>
    /// Возвращает или задаёт идентификатор местоположения.
    /// </summary>
    public long LocationId { get; set; }

    /// <summary>
    /// Возвращает или задаёт путь к кадру регистрации.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт скорость движения.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsMin { get; set; }

    /// <summary>
    /// Индекс экстремального значения в канале.
    /// </summary>
    public int ExtremumIndex { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double ExtremumValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uxb1Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uyb1Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uzb1Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uxb2Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uyb2Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uzb2Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double UxrValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double UyrValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double UzrValue { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uxk1Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uyk1Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uzk1Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uxk2Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uyk2Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Uzk2Value { get; set; }

    /// <summary>
    /// Возвращает или задаёт регистратор.
    /// </summary>
    public Registrar Registrar { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт кадр регистрации.
    /// </summary>
    public Frame Frame { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт канал регистрации.
    /// </summary>
    public Channel Channel { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт фильтр.
    /// </summary>
    public Filter Filter { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт местоположение.
    /// </summary>
    public Location Location { get; set; } = null!;

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<Extremum> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
        typeBuilder.Property(entity => entity.Id)
            .ValueGeneratedOnAdd();
        typeBuilder.HasIndex(entity => entity.Id);

        //  Настройка идентификатора регистратора.
        typeBuilder.Property(entity => entity.RegistrarId);
        typeBuilder.HasIndex(entity => entity.RegistrarId);

        //  Настройка метки времени получения данных кадра.
        typeBuilder.Property(entity => entity.FrameTimestamp);
        typeBuilder.HasIndex(entity => entity.FrameTimestamp);

        //  Настройка идентификатора канала.
        typeBuilder.Property(entity => entity.ChannelId);
        typeBuilder.HasIndex(entity => entity.ChannelId);

        //  Настройка частоты среза фильтра.
        typeBuilder.Property(entity => entity.Cutoff);
        typeBuilder.HasIndex(entity => entity.Cutoff);

        //  Настройка идентификатора местоположения.
        typeBuilder.Property(entity => entity.LocationId);
        typeBuilder.HasIndex(entity => entity.LocationId);

        //  Настройка регистратора.
        typeBuilder.HasOne(entity => entity.Registrar)
            .WithMany(registrar => registrar.Extremums)
            .HasForeignKey(entity => entity.RegistrarId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Extremums_Registrars");

        //  Настройка кадра.
        typeBuilder.HasOne(entity => entity.Frame)
            .WithMany(frame => frame.Extremums)
            .HasForeignKey(entity => new
            {
                entity.RegistrarId,
                Timestamp = entity.FrameTimestamp,
            })
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Extremums_Frames");

        //  Настройка канала.
        typeBuilder.HasOne(entity => entity.Channel)
            .WithMany(channel => channel.Extremums)
            .HasForeignKey(entity => entity.ChannelId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Extremums_Channels");

        //  Настройка фильтра.
        typeBuilder.HasOne(entity => entity.Filter)
            .WithMany(filter => filter.Extremums)
            .HasForeignKey(entity => entity.Cutoff)
            .HasPrincipalKey(filter => filter.Cutoff)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Extremums_Filters");

        //  Настройка местоположения.
        typeBuilder.HasOne(entity => entity.Location)
            .WithMany(registrar => registrar.Extremums)
            .HasForeignKey(entity => entity.LocationId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Extremums_Locations");
    }
}
