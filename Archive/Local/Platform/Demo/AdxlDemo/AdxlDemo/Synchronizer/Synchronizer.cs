namespace Apeiron.Platform.Demo.AdxlDemo;

/// <summary>
/// Представляет синхронизатор.
/// </summary>
public sealed class Synchronizer :
    Element
{
    /// <summary>
    /// Происходит при достижении очередной отметки времени.
    /// </summary>
    public event EventHandler<SynchronizerEventArgs>? Tick;

    /// <summary>
    /// Поле для хранения критического объекта.
    /// </summary>
    private readonly object _SyncRoot;

    /// <summary>
    /// Поле для хранения значения, определяющего запущен ли синхронизатор.
    /// </summary>
    private bool _IsStart;

    /// <summary>
    /// Поле для хранения основного потока.
    /// </summary>
    private Thread? _Thread;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public Synchronizer(Engine engine) :
        base(engine)
    {
        //  Создание критического объекта.
        _SyncRoot = new();

        //  Установка значения, определяющего запущен ли синхронизатор.
        _IsStart = false;

        //  Установка основного потока.
        _Thread = null;
    }

    /// <summary>
    /// Запускает синхронизатор.
    /// </summary>
    public void Start()
    {
        //  Блокировка критического объекта.
        lock (_SyncRoot)
        {
            //  Установка значения, определяющего запущен ли синхронизатор.
            _IsStart = true;

            //  Проверка работы основного потока.
            ValidationThread();
        }
    }

    /// <summary>
    /// Останавливает синхронизатор.
    /// </summary>
    public void Stop()
    {
        //  Блокировка критического объекта.
        lock (_SyncRoot)
        {
            //  Установка значения, определяющего запущен ли синхронизатор.
            _IsStart = false;

            //  Проверка работы основного потока.
            ValidationThread();
        }
    }

    /// <summary>
    /// Выполняет проверку работы основного потока.
    /// </summary>
    private void ValidationThread()
    {
        //  Блокировка критического объекта.
        lock (_SyncRoot)
        {
            //  Проверка запуска синхронизатора.
            if (_IsStart)
            {
                //  Проверка потока.
                if (_Thread is null)
                {
                    //  Создание потока.
                    _Thread = new(ThreadEntry);

                    //  Запуск потока.
                    _Thread.Start();
                }
            }
            else
            {
                //  Проверка потока.
                if (_Thread is not null)
                {
                    //  Сброс ссылки на поток.
                    _Thread = null;
                }
            }
        }
    }

    /// <summary>
    /// Представляет точку входа основного потока.
    /// </summary>
    private void ThreadEntry()
    {
        //  Получение шага времени.
        TimeSpan step = TimeSpan.FromMilliseconds(Engine.Application.Settings.SynchronizerStep);

        //  Последняя метка времени.
        DateTime lastTime = DateTime.Now;

        //  Корректировка последней метки времени.
        lastTime = new((lastTime.Ticks / step.Ticks) * step.Ticks);

        //  Основной цикл потока.
        while (_IsStart)
        {
            //  Безопасное выполнение.
            Invoker.Critical(delegate
            {
                //  Получение текущего времени.
                DateTime time = DateTime.Now;

                //  Проверка текущего времени.
                while ((time - lastTime) > step)
                {
                    //  Корректировка последней метки времени.
                    lastTime += step;

                    //  Вызов события.
                    Tick?.Invoke(this, new(lastTime));
                }
            });

            //  Ожидание перед следующим циклом.
            Thread.Sleep(1);
        }
    }
}
