using Microsoft.Win32;
using Simargl.Engineering.Utilities.CertificateSheet.Core;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Simargl.Engineering.Utilities.CertificateSheet;

/// <summary>
/// Представляет сердце приложения.
/// </summary>
/// <param name="invoker">
/// Метод, выполняющий действие в основном потоке.
/// </param>
public sealed class Heart(Func<Action, Task> invoker) :
    INotifyPropertyChanged,
    IDisposable
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения критического объекта.
    /// </summary>
    private readonly Lock _SyncLock = new();

    /// <summary>
    /// Поле для хранения значения, определяющего разрушен ли объект.
    /// </summary>
    private bool _IsDisposed;

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private CancellationTokenSource? _CancellationTokenSource;

    /// <summary>
    /// Поле для хранения стека объектов.
    /// </summary>
    private readonly Stack<dynamic> _Stack = [];

    /// <summary>
    /// Поле для хранения значения прогресса выполнения.
    /// </summary>
    private double _Progress;

    /// <summary>
    /// Поле для хранения значения, определяющего активна ли кнопка запуска.
    /// </summary>
    private bool _StartButtonIsEnabled = true;

    /// <summary>
    /// Поле для хранения значения, определяющего активна ли кнопка остановки.
    /// </summary>
    private bool _StopButtonIsEnabled;

    /// <summary>
    /// Возвращает коллекцию удостоверяющих листов.
    /// </summary>
    public ObservableCollection<Certificate> Certificates { get; } = [];

    /// <summary>
    /// Возвращает метод, выполняющий действие в основном потоке.
    /// </summary>
    public Func<Action, Task> Invoker { get; } = invoker;

    /// <summary>
    /// Возвращает или задаёт значение прогресса выполнения.
    /// </summary>
    public double Progress
    {
        get => _Progress;
        set
        {
            //  Проверка изменения значения.
            if (_Progress != value)
            {
                //  Установка нового значения.
                _Progress = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(Progress)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее активна ли кнопка запуска.
    /// </summary>
    public bool StartButtonIsEnabled
    {
        get => _StartButtonIsEnabled;
        set
        {
            //  Проверка изменения значения.
            if (_StartButtonIsEnabled != value)
            {
                //  Установка нового значения.
                _StartButtonIsEnabled = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(StartButtonIsEnabled)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее активна ли кнопка остановки.
    /// </summary>
    public bool StopButtonIsEnabled
    {
        get => _StopButtonIsEnabled;
        set
        {
            //  Проверка изменения значения.
            if (_StopButtonIsEnabled != value)
            {
                //  Установка нового значения.
                _StopButtonIsEnabled = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(StopButtonIsEnabled)));
            }
        }
    }

    /// <summary>
    /// Запускает работу.
    /// </summary>
    public void Start()
    {
        //  Блокировка критического объекта.
        lock (_SyncLock)
        {
            //  Проверка значения, определяющего разрушен ли объект.
            if (_IsDisposed)
            {
                //  Невозможно запустить работу.
                return;
            }

            //  Создание диалога.
            OpenFileDialog dialog = new()
            {
                Title = "Открыть файл",
                Filter = "Excel (*.xlsx)|*.xlsx|Все файлы (*.*)|*.*",
            };

            //  Вызов диалогового окна.
            bool? result = dialog.ShowDialog();

            //  Проверка результата.
            if (result != true)
            {
                //  Завершение работы.
                return;
            }

            //  Получение пути.
            string path = dialog.FileName;

            //  Остановка работы.
            StopCore();

            //  Подготовка данных.
            Certificates.Clear();
            Progress = 0;

            //  Создание источника токена отмены.
            _CancellationTokenSource = new();

            //  Получение токена отмены.
            CancellationToken cancellationToken = _CancellationTokenSource.Token;

            //  Запуск асинхронной задачи.
            _ = Task.Run(async delegate
            {
                //  Выполнение основной задачи.
                await InvokeAsync(path, cancellationToken).ConfigureAwait(false);
            }, CancellationToken.None);

            //  Настройка кнопок.
            StartButtonIsEnabled = false;
            StopButtonIsEnabled = true;
        }
    }

    /// <summary>
    /// Останавливает работу.
    /// </summary>
    public void Stop()
    {
        //  Блокировка критического объекта.
        lock (_SyncLock)
        {
            //  Остановка работы.
            StopCore();

            //  Настройка кнопок.
            StartButtonIsEnabled = true;
            StopButtonIsEnabled = false;
        }
    }

    /// <summary>
    /// Останавливает работу.
    /// </summary>
    private void StopCore()
    {
        //  Сброс источника токена отмены.
        if (Interlocked.Exchange(ref _CancellationTokenSource, null) is CancellationTokenSource cancellationTokenSource)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Отправка запроса на отмену.
                cancellationTokenSource.Cancel();
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение источника токена отмены.
                cancellationTokenSource.Dispose();
            }
            catch { }
        }

        //  Извлечение объектов из стека.
        while (_Stack.TryPop(out dynamic? obj))
        {
            //  Проверка объекта.
            if (obj is not null)
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Попытка закрытия.
                    obj.Quit();
                }
                catch { }

                //  Блок перехвата всех исключений.
                try
                {
                    //  Освобождение объекта.
                    Marshal.ReleaseComObject(obj);
                }
                catch { }
            }
        }

        //  Сборка мусора.
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    /// <summary>
    /// Присоединяет объект.
    /// </summary>
    /// <param name="obj">
    /// Присоединяемый объект.
    /// </param>
    public void Attach(dynamic? obj)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка объекта.
            if (Marshal.IsComObject(obj))
            {
                //  Блокировка критического объекта.
                lock (_SyncLock)
                {
                    //  Проверка значения, определяющего разрушен ли объект.
                    if (_IsDisposed)
                    {
                        //  Блок перехвата всех исключений.
                        try
                        {
                            //  Освобождение объекта.
                            Marshal.ReleaseComObject(obj);
                        }
                        catch { }
                    }
                    else
                    {
                        //  Добавление в стек.
                        _Stack.Push(obj);
                    }
                }
            }
        }
        catch { }
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    void IDisposable.Dispose()
    {
        //  Блокировка критического объекта.
        lock (_SyncLock)
        {
            //  Остановка работы.
            StopCore();

            //  Установка значения, определяющего разрушен ли объект.
            _IsDisposed = true;
        }
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    private async Task InvokeAsync(string path, CancellationToken cancellationToken)
    {
        //  Блок с гарантированным завершением.
        try
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Загрузка данных.
                await Loader.LoadAsync(path, cancellationToken).ConfigureAwait(false);

                //  Ссылка на приложение.
                dynamic? application = null;

                //  Блок с гарантированным завершением.
                try
                {
                    //  Выполнение в основном потоке.
                    await Invoker(delegate
                    {
                        //  Получение типа приложения Word.
                        Type type = Type.GetTypeFromProgID("Word.Application") ??
                            throw new InvalidOperationException("Не удалось найти программный идентификатор приложения Word.");

                        //  Запуск приложения.
                        application = Activator.CreateInstance(type) ??
                            throw new InvalidOperationException("Не удалось запустить приложение Word.");
                    });

                    //  Присоединение приложения.
                    Attach(application);

                    ////  Проверка ссылки на приложение.
                    //if (application is not null)
                    //{
                    //    //  Выполнение в основном потоке.
                    //    await Invoker(
                    //        delegate
                    //        {
                    //            application.Visible = true;
                    //            while (Certificates.Count > 1)
                    //            {
                    //                Certificates.RemoveAt(0);
                    //            }
                    //        });
                    //}

                    //  Определение количества удостоверяющих листов.
                    int totalCount = Certificates.Count;

                    //  Проверка количества удостоверяющих листов.
                    if (totalCount == 0)
                    {
                        //  Выполнение в основном потоке.
                        await Invoker(delegate
                        {
                            //  Установка прогресса.
                            Progress = 100;
                        });
                    }
                    else
                    {
                        //  Количество отработанных удостоверяющих листов.
                        int count = 0;

                        //  Параллельная работа с удостоверяющими листами.
                        await Parallel.ForEachAsync(
                            Certificates,
                            new ParallelOptions()
                            {
                                CancellationToken = cancellationToken,
                                MaxDegreeOfParallelism = 1,
                            },
                            async delegate (Certificate certificate, CancellationToken cancellationToken)
                            {
                                //  Выполнение работы с удостоверяющим листом.
                                await certificate.InvokeAsync(application, cancellationToken).ConfigureAwait(false);

                                //  Корректировка счётчика.
                                int currentCount = Interlocked.Increment(ref count);

                                //  Определение прогресса.
                                double progress = 100.0 * currentCount / totalCount;

                                //  Выполнение в основном потоке.
                                await Invoker(delegate
                                {
                                    //  Проверка прогресса.
                                    if (Progress < progress)
                                    {
                                        //  Установка прогресса.
                                        Progress = progress;
                                    }
                                });
                            }).ConfigureAwait(false);
                    }

                    //await Task.Delay(-1, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    //  Проверка объекта.
                    if (application is not null)
                    {
                        //  Выполнение в основном потоке.
                        await Invoker(delegate
                        {
                            //  Блок перехвата всех исключений.
                            try
                            {
                                //  Закрытие приложения.
                                application?.Quit();
                            }
                            catch { }

                            //  Блок перехвата всех исключений.
                            try
                            {
                                //  Освобождение объекта.
                                Marshal.ReleaseComObject(application);
                            }
                            catch { }
                        });
                    }

                    //  Сборка мусора.
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Выполнение в основном потоке.
                    await Invoker(delegate
                    {
                        //  Вывод сообщения на экран.
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }).ConfigureAwait(false);
                }
            }
        }
        finally
        {
            //  Выполнение в основном потоке.
            await Invoker(Stop).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref PropertyChanged)?.Invoke(this, e);
    }
}
