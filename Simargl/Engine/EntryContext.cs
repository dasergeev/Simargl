using Simargl.Performing;

namespace Simargl.Engine;

/// <summary>
/// Представляет контекст входа в приложение.
/// </summary>
public sealed class EntryContext :
    Performer
{
    /// <summary>
    /// Поле для хранения периода поддержки задач.
    /// </summary>
    private TimeSpan _KeeperPeriod = Keeper.DefaultPeriod;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="sender">
    /// Метод, устанавливающий действие в очередь основного потока.
    /// </param>
    public EntryContext(Sender sender) :
        base(CancellationToken.None)
    {
        //  Установка основного токена отмены.
        CancellationToken = GetCancellationToken();

        //  Установка метода, устанавливающего действие в очередь основного потока.
        PrimarySender = sender;
    }

    /// <summary>
    /// Возвращает основной токен отмены.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Возвращает метод, устанавливающий действие в очередь основного потока.
    /// </summary>
    public Sender PrimarySender { get; }

    /// <summary>
    /// Возвращает или инициализирует период поддержки задач.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение,
    /// которое меньше значения <see cref="Keeper.MinPeriod"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение,
    /// которое превышает значение <see cref="Keeper.MaxPeriod"/>.
    /// </exception>
    public TimeSpan KeeperPeriod
    {
        get => _KeeperPeriod;
        init
        {
            //  Проверка периода поддержки.
            IsNotLess(value, Keeper.MinPeriod, nameof(KeeperPeriod));
            IsNotLarger(value, Keeper.MaxPeriod, nameof(KeeperPeriod));

            //  Установка периода поддержки.
            _KeeperPeriod = value;
        }
    }
}
