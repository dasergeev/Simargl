using Apeiron.Platform.Communication.СommutatorDesktop.Logging;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Apeiron.Platform.Communication.СommutatorDesktop.UserInterface;

/// <summary>
/// Представляет элемент управления, отображающий журнал.
/// </summary>
partial class LogView
{
    /// <summary>
    /// Поле для хранения коллекции сообщений журнала.
    /// </summary>
    private readonly ObservableCollection<LogMessage> _Messages;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public LogView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Создание коллекции сообщений журнала.
        _Messages = new();

        //  Привязка коллекции сообщений к списку.
        _ListView.ItemsSource = _Messages;

        //  Создание таймера.
        DispatcherTimer timer = new(DispatcherPriority.Background, Dispatcher)
        {
            Interval = Setting.LogViewUpdateInterval,
        };

        //  Добавление обработчика события таймера.
        timer.Tick += Timer_Tick;

        //  Запуск таймера.
        timer.Start();
    }

    /// <summary>
    /// Обрабатывает событие таймера.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Timer_Tick(object? sender, EventArgs e)
    {
        //  Извлечение очередного сообщения из очереди.
        while (Setting.LogMessages.TryDequeue(out LogMessage? message))
        {
            //  Добавление сообщения в журнал.
            _Messages.Insert(0, message);
        }
    }
}
