using System.Collections.ObjectModel;
using System.Text;

namespace Apeiron.Platform.Demo.AdxlDemo.Logging;

/// <summary>
/// Представляет средство ведения журнала.
/// </summary>
public sealed class Logger :
    Primary
{
    /// <summary>
    /// Постоянная, определяющая максимальное количество сообщений в журнале.
    /// </summary>
    private const int _MaxMessages = 1000;

    /// <summary>
    /// Поле для хранения коллекции сообщений журнала.
    /// </summary>
    private readonly ObservableCollection<LogMessage> _LogMessages;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="logMessages">
    /// Коллекция сообщений журнала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logMessages"/> передана пустая ссылка.
    /// </exception>
    public Logger(Engine engine, ObservableCollection<LogMessage> logMessages) :
        base(engine)
    {
        //  Установка коллекции сообщений журнала.
        _LogMessages = IsNotNull(logMessages, nameof(logMessages));
    }

    /// <summary>
    /// Регистрирует сообщение в журнале.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо зарегистрировать в журнале.
    /// </param>
    /// <remarks>
    /// Метод должен выполняться в основном потоке.
    /// </remarks>
    private void PrimaryLog([ParameterNoChecks] LogMessage message)
    {
        //  Добавление элемента в коллекцию.
        _LogMessages.Add(message);

        //  Корректировка количества сообщений.
        while (_LogMessages.Count > _MaxMessages)
        {
            //  Удаление первого сообщения.
            _LogMessages.RemoveAt(0);
        }
    }

    /// <summary>
    /// Регистрирует исключение в журнале.
    /// </summary>
    /// <param name="exception">
    /// Исключение, которое необходимо зарегистрировать в журнале.
    /// </param>
    public void Log(Exception? exception)
    {
        //  Построитель текста исключения.
        StringBuilder text = new("Произошло исключение");

        //  Проверка ссылки на исключение.
        if (exception is not null)
        {
            //  Добавление текста исключения.
            text.Append(exception.Message);
            text.Append(':');
            text.AppendLine();

            //  Добавление информации об исключении.
            text.Append(exception.ToString());
        }

        //  Регистрация сообщения в журнале.
        Log(text.ToString());
    }

    /// <summary>
    /// Регистрирует сообщение в журнале.
    /// </summary>
    /// <param name="text">
    /// Текст сообщения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    public void Log(string text)
    {
        //  Создание сообщения.
        LogMessage message = new(text);

        //  Добавление действия, регистрирующего сообщение.
        Invoker.AddAction(delegate
        {
            //  Регистрация сообщения.
            PrimaryLog(message);
        });
    }

    /// <summary>
    /// Асинхронно регистрирует сообщение в журнале.
    /// </summary>
    /// <param name="text">
    /// Текст сообщения.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая регистрацию сообщения в журнале.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task LogAsync(string text, CancellationToken cancellationToken)
    {
        //  Создание сообщения.
        LogMessage message = new(text);

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Добавление действия, регистрирующего сообщение.
        await Invoker.AddActionAsync(delegate
        {
            //  Регистрация сообщения.
            PrimaryLog(message);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Регистрирует исключение в журнале.
    /// </summary>
    /// <param name="exception">
    /// Исключение, которое необходимо зарегистрировать в журнале.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая регистрацию исключения в журнале.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task LogAsync(Exception? exception, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Асинхронное выполнение.
        await Task.Run(delegate
        {
            //  Добавление исключения в журнал.
            Log(exception);
        }, cancellationToken).ConfigureAwait(false);
    }
}
