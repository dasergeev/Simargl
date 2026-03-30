using Simargl.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Synergy.Transferring;

/// <summary>
/// Представляет подтверждение получения данных файла.
/// </summary>
public sealed class FileDataConfirmation :
    Message
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="path">
    /// Локальный путь к файлу.
    /// </param>
    public FileDataConfirmation(string path) :
        base(MessageFormat.FileDataConfirmation)
    {
        //  Установка значений свойств.
        Path = IsNotNull(path);

        //  Для анализатора.
        _ = this;
    }

    /// <summary>
    /// Возвращает локальный путь к файлу.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Асинхронно сохраняет сообщение в поток.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, сохраняющая данные в поток.
    /// </returns>
    protected override sealed async Task SaveAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        //  Запись пути.
        await spreader.WriteStringAsync(Path, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно загружает сообщение из потока.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая данные из потока.
    /// </returns>
    internal static async Task<FileDataConfirmation> LoadAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        //  Загрузка пути.
        string path = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

        //  Возврат сообщения.
        return new(path);
    }
}
