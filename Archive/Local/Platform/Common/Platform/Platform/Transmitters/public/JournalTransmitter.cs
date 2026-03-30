using Apeiron.Platform.Journals;
using System.Text;

namespace Apeiron.Platform.Transmitters;

/// <summary>
/// Представляет класс-передатчик журнала приложения.
/// </summary>
public class JournalTransmitter:Journal
{
    /// <summary>
    /// Представляет интерфейс логирования.
    /// </summary>
    private readonly Journal _Journal;

    /// <summary>
    /// Представляет класс передатчика.
    /// </summary>
    private readonly BinaryTransmitter _Transmitter;

    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="journal">
    /// Журнал.
    /// </param>
    /// <param name="options">
    /// Конфигурация.
    /// </param>
    /// <param name="encoder">
    /// Кодировщик
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметр <paramref name="journal"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметр <paramref name="options"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметр <paramref name="encoder"/> передана пустая ссылка.
    /// </exception>
    public JournalTransmitter(Journal journal, TransmittersOptions options, Encoding encoder)
    {
        //  Инициализация интерфейса логирования
        _Journal = Check.IsNotNull(journal, nameof(journal));

        //   Инициализация передатчика.
        _Transmitter = new BinaryTransmitter(journal,options);   
    }


    /// <summary>
    /// Представляет функцию логирования в журнал и в передатчик.
    /// </summary>
    /// <param name="level">Уровень логирования</param>
    /// <param name="message">Сообщение лога.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задачу.</returns>
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
    public override async Task LogAsync(JournalLevel level, string message, CancellationToken cancellationToken)
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

                //  Передача сообщения журнала
                _Transmitter.SendTransparent($"{DateTime.Now:dd.MM.yyyy HH.mm.ss};Trace;{message}\r\n");
                
                //  Вывод в журнал.
                await _Journal.LogTraceAsync($"{message}", cancellationToken).ConfigureAwait(false);
                break;
            case JournalLevel.Debug:
                //  Передача сообщения журнала
                _Transmitter.SendTransparent($"{DateTime.Now:dd.MM.yyyy HH.mm.ss};Debug;{message}\r\n");

                //  Вывод в журнал.
                await _Journal.LogDebugAsync($"{message}", cancellationToken).ConfigureAwait(false);
                break;
            case JournalLevel.Information:
                //  Передача сообщения журнала
                _Transmitter.SendTransparent($"{DateTime.Now:dd.MM.yyyy HH.mm.ss};Information;{message}\r\n");

                //  Вывод в журнал.
                await _Journal.LogInformationAsync($"{message}", cancellationToken).ConfigureAwait(false);
                break;
            case JournalLevel.Warning:
                //  Передача сообщения журнала
                _Transmitter.SendTransparent($"{DateTime.Now:dd.MM.yyyy HH.mm.ss};Warning;{message}\r\n");

                //  Вывод в журнал.
                await _Journal.LogWarningAsync($"{message}", cancellationToken).ConfigureAwait(false);
                break;
            case JournalLevel.Error:
                //  Передача сообщения журнала
                _Transmitter.SendTransparent($"{DateTime.Now:dd.MM.yyyy HH.mm.ss};Error;{message}\r\n");

                //  Вывод в журнал.
                await _Journal.LogErrorAsync($"{message}", cancellationToken).ConfigureAwait(false);
                break;
            case JournalLevel.Critical://  Передача сообщения журнала
                _Transmitter.SendTransparent($"{DateTime.Now:dd.MM.yyyy HH.mm.ss};Critical;{message}\r\n");

                //  Вывод в журнал.
                await _Journal.LogCriticalAsync($"{message}", cancellationToken).ConfigureAwait(false);
                break;
            default:
                break;
        }
    }
}
