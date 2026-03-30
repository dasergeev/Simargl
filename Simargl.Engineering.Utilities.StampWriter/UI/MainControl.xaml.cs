using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Simargl.Engineering.Utilities.StampWriter.UI;

/// <summary>
/// Представляет главный элемент управления.
/// </summary>
partial class MainControl
{
    /// <summary>
    /// Поле для хранения источника завершения основной задачи.
    /// </summary>
    private volatile TaskCompletionSource? _TaskCompletionSource;

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private volatile CancellationTokenSource? _CancellationTokenSource;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public MainControl()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Проверка режима разработки.
        if (!DesignerProperties.GetIsInDesignMode(this))
        {
            //  Установка данных контекста.
            DataContext = Heart;
        }
    }

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    public static Heart Heart => ((App)Application.Current).Heart;

    /// <summary>
    /// Запускает работу приложения.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        //  Создание потока.
        Thread thread = new(Entry)
        {
            //  Установка основного потока.
            IsBackground = false,
        };

        //  Настройка STA-потока.
        thread.SetApartmentState(ApartmentState.STA);

        //  Создание источника завершения основной задачи.
        _TaskCompletionSource = new();

        //  Создание источника токена отмены.
        _CancellationTokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(Heart.CancellationToken);

        //  Очистка сообщений.
        Heart.Messages.Clear();

        //  Запуск потока.
        thread.Start();

        //  Настройка интерфейса.
        Heart.IsStoped = false;
        Heart.IsStarted = true;
        Heart.StampViewVisibility = Visibility.Collapsed;
        Heart.JournalViewVisibility = Visibility.Visible;

        //  Вывод сообщения в журнал.
        Heart.Log("Запуск работы.");

        //  Запуск асинхронной задачи.
        _ = Task.Run(async delegate
        {
            //  Получение источника завершения задачи.
            if (_TaskCompletionSource is TaskCompletionSource taskCompletionSource)
            {
                //  Ожидание завершения задачи.
                await taskCompletionSource.Task.ConfigureAwait(false);
            }

            //  Ожидание для отображения журнала.
            await Task.Delay(10000, CancellationToken.None).ConfigureAwait(false);

            //  Выполнение в основном потоке.
            await Dispatcher.InvokeAsync(delegate
            {
                //  Настройка интерфейса.
                Heart.IsStoped = true;
                Heart.IsStarted = false;
                Heart.StampViewVisibility = Visibility.Visible;
                Heart.JournalViewVisibility = Visibility.Collapsed;
            });
        });
    }

    /// <summary>
    /// Останавливает работу приложения.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        //  Получение источника токена отмены.
        if (_CancellationTokenSource is CancellationTokenSource cancellationTokenSource)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Запрос на отмену.
                cancellationTokenSource.Cancel();
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение источника токена отмены..
                cancellationTokenSource.Dispose();
            }
            catch { }
        }
    }

    /// <summary>
    /// Точка входа потока.
    /// </summary>
    private void Entry()
    {
        //  Блок с гарантированным завершением.
        try
        {
            //  Получение источника токена отмены.
            if (_CancellationTokenSource is not CancellationTokenSource cancellationTokenSource)
            {
                //  Завершение работы.
                return;
            }

            //  Блок перехвата всех исключений.
            try
            {
                //  Создание исполнителя.
                using Performer performer = new(cancellationTokenSource.Token);

                //  Выполнение основной работы.
                performer.Invoke();
            }
            catch (Exception ex)
            {
                //  Вывод сообщения в журнал.
                Heart.Log(ex.ToString());
            }
        }
        finally
        {
            //  Завершение задачи.
            _TaskCompletionSource?.SetResult();
        }
    }
}
