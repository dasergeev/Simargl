namespace Simargl.Frames.Catman;

/// <summary>
/// Представляет заголовок кадра в формате <see cref="StorageFormat.Catman"/>.
/// </summary>
public class CatmanFrameHeader :
    FrameHeader
{
    /// <summary>
    /// Поле для хранения комментария.
    /// </summary>
    private string _Comment;

    /// <summary>
    /// Поле для хранения зарезервированных строк.
    /// </summary>
    private string[] _Reserve = new string[32];

    /// <summary>
    /// Поле для хранения максимальной длины канала.
    /// </summary>
    private int _MaxChannelLength;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public CatmanFrameHeader() :
        base(StorageFormat.Catman)
    {
        _Comment = string.Empty;

        for (int i = 0; i != 32; ++i)
        {
            _Reserve[i] = string.Empty;
        }
        _MaxChannelLength = 0;
    }

    /// <summary>
    /// Возвращает или задаёт комментарий.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Comment
    {
        get => _Comment;
        set => _Comment = IsNotNull(value, nameof(Comment));
    }

    /// <summary>
    /// Возвращает или задаёт максимальную длину канала.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    public int MaxChannelLength
    {
        get => _MaxChannelLength;
        set => _MaxChannelLength = IsNotNegative(value, nameof(MaxChannelLength));
    }

    /// <summary>
    /// Возвращает массив зарезервированных строк.
    /// </summary>
    internal string[] Reserve => _Reserve;

    /// <summary>
    /// Создаёт копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public override FrameHeader Clone()
    {
        CatmanFrameHeader duplicate = new()
        {
            Comment = Comment,
            _Reserve = (string[])_Reserve.Clone(),
            MaxChannelLength = MaxChannelLength,
        };
        return duplicate;
    }
}
