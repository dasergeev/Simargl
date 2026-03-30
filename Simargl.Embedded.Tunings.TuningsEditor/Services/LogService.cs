using Simargl.Embedded.Tunings.TuningsEditor.Core;

namespace Simargl.Embedded.Tunings.TuningsEditor.Services;

/// <summary>
/// Представляет службу работы с журналом.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public class LogService(ILogger<LogService> logger, Heart heart) :
    BackgroundService
{
    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Ожидание инициализации служб.
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        logger.LogInformation("Начало работы службы ведения журнала.");

        //  Список сообщений.
        List<string> messages = [];

        //  Основной цикл поддержки работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Проверка очистки журнала.
            if (heart.IsLogClear)
            {
                //  Очистка журнала.
                messages.Clear();

                //  установка текста журнала.
                await heart.SetLogTextAsync(string.Empty, cancellationToken).ConfigureAwait(false);

                //  Сброс флага.
                heart.IsLogClear = false;
            }

            //  Проверка очереди и работы журнала.
            if (!heart.LogQueue.IsEmpty &&
                heart.IsLogEnable)
            {
                //  Извлечение из очереди.
                while (heart.LogQueue.TryDequeue(out string? message) &&
                    !cancellationToken.IsCancellationRequested)
                {
                    //  Проверка сообщения.
                    if (message is not null)
                    {
                        //  Добавление в список сообщений.
                        messages.Insert(0, message);
                    }
                }

                //  Усечение списка.
                while (messages.Count > 512)
                {
                    //  Удаление сообщения.
                    messages.RemoveAt(messages.Count - 1);
                }

                //  Получение текста журнала.
                string text = string.Join(Environment.NewLine, messages);

                //  установка текста журнала.
                await heart.SetLogTextAsync(text, cancellationToken).ConfigureAwait(false);
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(500, cancellationToken).ConfigureAwait(false);
        }
    }
}
