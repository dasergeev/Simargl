using Apeiron.Analysis.Transforms;
using Apeiron.Frames;

namespace Apeiron.Platform.Server.Services.Microservices;

/// <summary>
/// Представляет микрослужбу, выполняющую базовую обработку.
/// </summary>
internal class BaseProcessor :
    ServerMicroservice<BaseProcessor>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public BaseProcessor(ILogger<BaseProcessor> logger) :
        base(logger)
    {

    }

    /// <summary>
    /// Асинхронно выполняет шаг работы микрослужбы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая шаг работы микрослужбы.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override sealed async ValueTask MakeStepAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Работа с корневым каталогом.
        await WorkAsync(new DirectoryInfo(@"\\railtest.ru\Data\06-НТО\RawData"), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с каталогом.
    /// </summary>
    /// <param name="directory">
    /// Информация о каталоге.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с каталогом.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkAsync(DirectoryInfo directory, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение подкаталогов.
            DirectoryInfo[] subDirectories = directory.GetDirectories();

            //  Асинхронная работа со всеми каталогами.
            await Parallel.ForEachAsync(
                subDirectories,
                new ParallelOptions
                {
                    CancellationToken = cancellationToken,
                },
                WorkAsync).ConfigureAwait(false);

            //  Получение файлов.
            FileInfo[] files = directory.GetFiles();

            //  Асинхронная работа со всеми файлами.
            await Parallel.ForEachAsync(
                files,
                new ParallelOptions
                {
                    CancellationToken = cancellationToken,
                },
                WorkAsync).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с файлом.
    /// </summary>
    /// <param name="file">
    /// Информация о файле.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с файлом.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkAsync(FileInfo file, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Перехват всех исключений.
            try
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Открытие кадра.
                Frame frame = new(file.FullName);

                //  Перебор всех каналов.
                foreach (Channel channel in frame.Channels)
                {
                    //  Цикл по фильтрам.
                    for (int cutoff = 1000; cutoff > 0; cutoff--)
                    {
                        //  Создание фильтра.
                        SincFilter transform = new(cutoff);

                        //  Фильтрация канала.
                        transform.Invoke(channel, channel);
                    }
                }
            }
            catch
            {

            }
        }, cancellationToken).ConfigureAwait(false);
    }
}
