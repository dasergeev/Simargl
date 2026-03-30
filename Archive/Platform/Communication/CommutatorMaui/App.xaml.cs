using Apeiron.Platform.Communication;
using Apeiron.Support;
using System.Collections.Specialized;

namespace CommutatorMaui;

/// <summary>
/// Представляет приложение.
/// </summary>
public sealed partial class App :
    Application,
    IDisposable
{
    /// <summary>
    /// Поле для хранения источника основного токена отмены.
    /// </summary>
    private readonly CancellationTokenSource _TokenSource;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public App()
    {
        //Инициализация основных компонентов.
        InitializeComponent();

        //  Создание источника основного токена отмены.
        _TokenSource = new();

        //  Установка основного токена отмены.
        CancellationToken = _TokenSource.Token;

        ////  Создание основных настроек приложения.
        Setting = new("Андрей", "123QWEasd");

        Invoker = new(SafeInvokeInMainThread);

        //Создание коммуникатора с серверным узлом.
        Communicator = new(Setting.CommunicatorOptions, Invoker);

        //  Добавление обработчика события изменения коллекции диалогов.
        Communicator.Dialogs.CollectionChanged += collectionChanged;

        //Создание главной страницы.
        MainPage = new NavigationPage(new ChatsPage());



        //  Обработчик события изменения коллекции диалогов.
        void collectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            //  Получение первого диалога.
            if (Communicator.Dialogs.Count > 0)
            {
                MainPage = new NavigationPage(new ChatsPage(Communicator));

                //  Удаление обработчика события.
                Communicator.Dialogs.CollectionChanged -= collectionChanged;
            }
        }
    }

    /// <summary>
    /// Происходит при запуске приложения.
    /// </summary>
    protected override void OnStart()
    {
        //  Вызов метода базового класса.
        base.OnStart();

        //  Асинхронное выполнение.
        _ = Task.Run(async delegate
        {
            //  Основной цикл поддержки.
            while (!CancellationToken.IsCancellationRequested)
            {
                //  Блок перехвата всех некритических исключений.
                try
                {
                    //  Обновление коллекции диалогов.
                    await Communicator.Dialogs.UpdateAsync(default, CancellationToken).ConfigureAwait(false);

                    //  Перебор диалогов.
                    foreach (Dialog dialog in Communicator.Dialogs)
                    {
                        //  Обновление диалога.
                        await dialog.UpdateAsync(default, CancellationToken).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    //  Проверка критического исключения.
                    if (ex.IsCritical())
                    {
                        //  Повторный выброс исключения.
                        throw;
                    }
                }

                //  Ожидание перед повторным запуском.
                await Task.Delay(1000, CancellationToken).ConfigureAwait(false);
            }
        });
    }

    /// <summary>
    /// Возвращает основной токен отмены.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Возвращает основные настройки приложения.
    /// </summary>
    public Setting Setting { get; }

    /// <summary>
    /// Возвращает средство вызова методов.
    /// </summary>
    public Invoker Invoker { get; }

    /// <summary>
    /// Возвращает коммуникатор с серверным узлом.
    /// </summary>
    public Communicator Communicator { get; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    public static void SafeInvokeInMainThread(Action action)
    {
        //if (DeviceInfo.Platform == DevicePlatform.WinUI )
        //{
        //    Dispatcher.Dispatch(action);
        //}
        //else
        //{
            MainThread.InvokeOnMainThreadAsync(action).Wait();
        //}
    }

    //  Разрушает объект.
    public void Dispose()
    {
        //  Разрушение источника токена отмены.
        ((IDisposable)_TokenSource).Dispose();
    }
}
