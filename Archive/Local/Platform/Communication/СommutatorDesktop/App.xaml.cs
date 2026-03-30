using Apeiron.Platform.Communication.СommutatorDesktop.Logging;

namespace Apeiron.Platform.Communication.СommutatorDesktop;

/// <summary>
/// Представляет приложение.
/// </summary>
public partial class App :
    Application
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
        //  Создание источника основного токена отмены.
        _TokenSource = new();

        //  Установка основного токена отмены.
        CancellationToken = _TokenSource.Token;

        //  Создание основных настроек приложения.
        Setting = new();

        //  Создание средства ведения журнала.
        Logger = new(Setting.LogMessages);

        //  Создание средства вызова методов.
        Invoker = new(Dispatcher);

        //  Создание коммуникатора с серверным узлом.
        Communicator = new(Setting.CommunicatorOptions, Invoker);

        //  Инициализация основных компонентов.
        InitializeComponent();

        _ = Task.Run(async delegate
        {
            try
            {
                Logger.Log("Начало обновления.");

                DateTime beginTime = DateTime.Now;

                //  Обновление диалогов.
                await Communicator.Dialogs.UpdateAsync(default, default);

                Logger.Log($"Завершение обновления. {DateTime.Now - beginTime}");

                foreach (Dialog dialog in Communicator.Dialogs)
                {
                    Logger.Log($"Найден диалог с пользователем {dialog.Companion.Name}");

                    ////  Отправка сообщения.
                    //await dialog.SendMessageAsync("Двойной привет!", default).ConfigureAwait(false);
                }

                ////  Запрос диалогов.
                //Dialog[] dialogs = await Communicator.GetAllDialogsAsync(default).ConfigureAwait(false);

                    //foreach (Dialog dialog in dialogs)
                    //{
                    //    Logger.Log($"Найден диалог с {dialog.Сompanion.Name}");

                    //    ////  Отправка сообщения.
                    //    //var info = await dialog.SendMessageAsync($"Привет, {dialog.Сompanion.Name}!", default).ConfigureAwait(false);

                    //    //Logger.Log($"ID сообщения {info.ID}");
                    //}

                    //RemoteMethod<(string Name, string Password), long> method = new();
                    //Type type = method.GetType();
                    //Logger.Log($"Type: {type}");

                    //foreach (var item in type.GetGenericArguments())
                    //{
                    //    Logger.Log($"GenericArgument: {item.GetGenericTypeDefinition()}");
                    //}

            }
            catch (Exception ex)
            {
                Logger.Log($"Исключение {ex}");
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
    /// Возвращает средство ведения журнала.
    /// </summary>
    public Logger Logger { get; }

    /// <summary>
    /// Возвращает средство вызова методов.
    /// </summary>
    public Invoker Invoker { get; }

    /// <summary>
    /// Возвращает коммуникатор с серверным узлом.
    /// </summary>
    public Communicator Communicator { get; }

    /// <summary>
    /// Обрабатывает событие завершения приложения.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override void OnExit(ExitEventArgs e)
    {
        //  Вызов метода базового класса.
        base.OnExit(e);

        //  Отправка запроса на отмену задач.
        _TokenSource.Cancel();

        //  Разрушение коммуникатора с серверным узлом.
        ((IDisposable)Communicator).Dispose();
    }
}
