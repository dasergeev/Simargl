namespace Apeiron.Platform.MediatorLibrary.Messages;

/// <summary>
/// Представляет базовый класс для всех сообщений.
/// </summary>
public abstract class GeneralMessage
{
    /// <summary>
    /// Последовательность идентифицирующая Host.
    /// </summary>
    public abstract long IdSequence { get; init; }

    /// <summary>
    /// Возвращает формат пакета.
    /// </summary>
    public abstract byte Type { get; init; }

    /// <summary>
    /// Представляет идентификатор хоста в строковом формате.
    /// </summary>
    public abstract string HostId { get; init; }


    /// <summary>
    /// Сохраняет пакет в сетевой поток.
    /// </summary>
    /// <param name="stream">Сетевой поток.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public abstract Task SavePackageAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Загружает данные из сетевого потока.
    /// </summary>
    /// <param name="stream">Сетевой поток.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public abstract Task<GeneralMessage> LoadPackageAsync(Stream stream, CancellationToken cancellationToken);
}
