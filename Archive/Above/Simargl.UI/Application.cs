using Simargl.Engine;
using System.Collections.Concurrent;
using System.Windows.Threading;

namespace Simargl.UI;

/// <summary>
/// Представляет приложение.
/// </summary>
public class Application :
    System.Windows.Application
{
    /// <summary>
    /// Поле для хранения очереди действий, которые необходимо выполнить в основном потоке.
    /// </summary>
    private readonly ConcurrentQueue<Action> _Actions;
    
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Application()
    {
        //  Создание очереди действий, которые необходимо выполнить в основном потоке.
        _Actions = [];

        //  Создание контекста входа в приложение.
        EntryContext context = new(_Actions.Enqueue);

        //  Блок перехвата всех исключений.
        try
        {
            //  Создание таймера для выполнения задач в основном потоке.
            DispatcherTimer timer = new()
            {
                Interval = context.KeeperPeriod,
            };

            //  Добавление обработчика события таймера.
            timer.Tick += delegate (object? sender, EventArgs e)
            {
                //  Извлечение действий из очереди.
                while (_Actions.TryDequeue(out Action? action))
                {
                    //  Проверка ссылки на действие.
                    if (action is not null)
                    {
                        //  Выполнение действия.
                        DefyCritical(action);
                    }
                }
            };

            //  Запуск таймера.
            timer.Start();

            //  Добавление обработчика события выхода из приложения.
            Exit += delegate (object sender, System.Windows.ExitEventArgs e)
            {
                //  Разрушение контекста входа в приложение.
                DefyCritical(context.Dispose);

                //  Остановка таймера.
                DefyCritical(timer.Stop);
            };

            //  Создание точки входа в приложение.
            Entry entry = new(context);

            //  Вывод сообщения в журнал.
            entry.Journal.Add("Приложение запущено.");
        }
        catch
        {
            //  Разрушение контекста входа в приложение.
            DefyCritical(context.Dispose);

            //  Повторный выброс исключения.
            throw;
        }
    }
}
