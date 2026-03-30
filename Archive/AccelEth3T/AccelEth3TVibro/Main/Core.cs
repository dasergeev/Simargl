using Simargl.Concurrent;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет ядро приложения.
/// </summary>
public sealed class Core
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="mainForm">
    /// Главное окно приложения.
    /// </param>
    /// <param name="timer">
    /// Таймер.
    /// </param>
    /// <param name="richTextBox">
    /// Элемент управления, отображающий текст.
    /// </param>
    public Core(MainForm mainForm, Timer timer, RichTextBox richTextBox)
    {
        //  Установка главного окна приложения.
        MainForm = mainForm;

        //  Создание средства вызова методов в основном потоке.
        Invoker = new(timer);

        //  Создание источника основного токена отмены.
        CancellationTokenSource mainTokenSource = new();

        //  Установка основного токена отмены всех задач.
        MainCancellationToken = mainTokenSource.Token;

        //  Добавлнение обработчика события закрытия окна.
        MainForm.FormClosing += delegate (object? sender, FormClosingEventArgs e)
        {
            //  Блок перехвата всех исключений.
            try {
                //  Отправка запроса на завершение задач.
                mainTokenSource.Cancel();
            } catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение источника токена отмены.
                mainTokenSource.Dispose();
            }
            catch { }
        };

        //  Создание средства ведения журнала.
        Journal = new(this, richTextBox);

        //  Вывод сообщения в журнал.
        Journal.Add("Запуск работы ядра.");

        //  Создание коллекции устройств.
        Devices = new(this);

        //  Создание объекта, отслеживающего состояние.
        Status = new(this);

        //  Создание объекта, выполняющего прислушивание сети.
        Listener = new(this);

        //  Создание объекта, сохраняющего историю.
        Store = new(this);

        //  Создание объекта, контролирующего состояние.
        Controller = new(this);
    }

    /// <summary>
    /// Возвращает главное окно приложения.
    /// </summary>
    public MainForm MainForm { get; }

    /// <summary>
    /// Возвращает средство вызова методов в основном потоке.
    /// </summary>
    public Invoker Invoker { get; }

    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    public Journal Journal { get; }

    /// <summary>
    /// Возвращает основной токен отмены всех задач.
    /// </summary>
    public CancellationToken MainCancellationToken { get; }

    /// <summary>
    /// Возвращает коллекцию устройств.
    /// </summary>
    public DeviceCollection Devices { get; }

    /// <summary>
    /// Возвращает объект, отслеживающий состояние.
    /// </summary>
    public Status Status { get; }

    /// <summary>
    /// Возвращает объект, выполняющий прислушивание сети.
    /// </summary>
    public Listener Listener { get; }

    /// <summary>
    /// Возвращает объект, сохранающий историю.
    /// </summary>
    public Store Store { get; }

    /// <summary>
    /// Возвращает объект, контролирующий состояние.
    /// </summary>
    public Controller Controller { get; }

    /// <summary>
    /// Выполняет действие с поддержкой.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо поддерживать.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие.
    /// </returns>
    public static async Task SafeAsync(AsyncAction action, CancellationToken cancellationToken)
    {
        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение действия.
                await action(cancellationToken).ConfigureAwait(false);

                //  Действие успешно завершено.
                return;
            } catch { }

            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Ожидание перед следующей попыткой.
                await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
