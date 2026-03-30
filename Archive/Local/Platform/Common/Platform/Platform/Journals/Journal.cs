using Microsoft.Extensions.Logging;

namespace Apeiron.Platform.Journals;

/// <summary>
/// Представляет журнал.
/// </summary>
public abstract class Journal
{
    /// <summary>
    /// Асинхронно записывает в журнал сообщение с указанным уровнем.
    /// </summary>
    /// <param name="level">
    /// Уровень сообщения.
    /// </param>
    /// <param name="message">
    /// Сообщение, которое необходимо записать в журнал.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись в журнал.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="level"/> передано значение,
    /// которое не содержится в перечислении <see cref="JournalLevel"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public abstract Task LogAsync(JournalLevel level, string message, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно записывает в журнал подробные сведения о ходе работы приложения.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо записать в журнал.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись в журнал.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task LogTraceAsync(string message, CancellationToken cancellationToken)
    {
        //  Запись в журнал.
        await LogAsync(JournalLevel.Trace, message, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно записывает в журнал сообщение об отладке.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо записать в журнал.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись в журнал.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task LogDebugAsync(string message, CancellationToken cancellationToken)
    {
        //  Запись в журнал.
        await LogAsync(JournalLevel.Debug, message, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно записывает в журнал информационное сообщение.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо записать в журнал.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись в журнал.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task LogInformationAsync(string message, CancellationToken cancellationToken)
    {
        //  Запись в журнал.
        await LogAsync(JournalLevel.Information, message, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно записывает в журнал сообщение с предупреждением.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо записать в журнал.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись в журнал.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task LogWarningAsync(string message, CancellationToken cancellationToken)
    {
        //  Запись в журнал.
        await LogAsync(JournalLevel.Warning, message, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно записывает в журнал сообщение об ошибке.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо записать в журнал.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись в журнал.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task LogErrorAsync(string message, CancellationToken cancellationToken)
    {
        //  Запись в журнал.
        await LogAsync(JournalLevel.Error, message, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно записывает в журнал критическое сообщение.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо записать в журнал.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись в журнал.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task LogCriticalAsync(string message, CancellationToken cancellationToken)
    {
        //  Запись в журнал.
        await LogAsync(JournalLevel.Critical, message, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Создаёт журнал-обёртку.
    /// </summary>
    /// <param name="logger">
    /// Исходное средство для ведения журнала.
    /// </param>
    /// <returns>
    /// Журнал.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public static Journal FromLogger(ILogger logger)
    {
        //  Создание и возврат журнала.
        return new Logger(logger);
    }

    /// <summary>
    /// Представляет журнал-обёртку.
    /// </summary>
    private sealed class Logger :
        Journal
    {
        /// <summary>
        /// Поле для хранения исходного средства ведения журнала.
        /// </summary>
        private readonly ILogger _Logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="logger">
        /// Исходное средство для ведения журнала.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="logger"/> передана пустая ссылка.
        /// </exception>
        public Logger(ILogger logger)
        {
            //  Установка средства для ведения журнала.
            _Logger = Check.IsNotNull(logger, nameof(logger));
        }

        /// <summary>
        /// Асинхронно записывает в журнал сообщение с указанным уровнем.
        /// </summary>
        /// <param name="level">
        /// Уровень сообщения.
        /// </param>
        /// <param name="message">
        /// Сообщение, которое необходимо записать в журнал.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая запись в журнал.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="level"/> передано значение,
        /// которое не содержится в перечислении <see cref="JournalLevel"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="message"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public override sealed async Task LogAsync(JournalLevel level, string message, CancellationToken cancellationToken)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка уровня сообщения.
            level = Check.IsDefined(level, nameof(level));

            //  Проверка сообщения.
            message = Check.IsNotNull(message, nameof(message));

            //  Разбор уровня сообщения.
            switch (level)
            {
                case JournalLevel.Trace:
                    //  Вывод в журнал.
                    _Logger.LogTrace("{message}", message);
                    break;
                case JournalLevel.Debug:
                    //  Вывод в журнал.
                    _Logger.LogDebug("{message}", message);
                    break;
                case JournalLevel.Information:
                    //  Вывод в журнал.
                    _Logger.LogInformation("{message}", message);
                    break;
                case JournalLevel.Warning:
                    //  Вывод в журнал.
                    _Logger.LogWarning("{message}", message);
                    break;
                case JournalLevel.Error:
                    //  Вывод в журнал.
                    _Logger.LogError("{message}", message);
                    break;
                case JournalLevel.Critical:
                    //  Вывод в журнал.
                    _Logger.LogCritical("{message}", message);
                    break;
                default:
                    break;
            }
        }
    }
}
