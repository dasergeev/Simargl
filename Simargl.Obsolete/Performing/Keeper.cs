using Simargl.Designing.Utilities;
using Simargl.Concurrent;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Simargl.Performing;

/// <summary>
/// Представляет средство поддержки задач.
/// </summary>
public sealed class Keeper :
    Performer
{
    /// <summary>
    /// Возвращает минимальный период поддержки.
    /// </summary>
    public static TimeSpan MinPeriod { get; } = TimeSpan.FromMilliseconds(1);

    /// <summary>
    /// Возвращает максимальный период поддержки.
    /// </summary>
    public static TimeSpan MaxPeriod { get; } = TimeSpan.FromMilliseconds(1000);

    /// <summary>
    /// Возвращает период поддержки по умолчанию.
    /// </summary>
    public static TimeSpan DefaultPeriod { get; } = TimeSpan.FromMilliseconds(10);

    /// <summary>
    /// Поле для хранения периода поддержки.
    /// </summary>
    private readonly TimeSpan _Period;

    /// <summary>
    /// Поле для хранения очереди действий.
    /// </summary>
    private ConcurrentQueue<AsyncAction>? _Actions;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="period">
    /// Период поддержки.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="period"/> передано значение,
    /// которое меньше значения <see cref="MinPeriod"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="period"/> передано значение,
    /// которое превышает значение <see cref="MaxPeriod"/>.
    /// </exception>
    public Keeper(TimeSpan period, CancellationToken cancellationToken) :
        base(cancellationToken)
    {
        //  Проверка периода поддержки.
        IsNotLess(period, MinPeriod);
        IsNotLarger(period, MaxPeriod);

        //  Замена токена отмены.
        cancellationToken = GetCancellationToken();

        //  Установка периода поддержки.
        _Period = period;

        //  Создание очереди действий.
        _Actions = [];

        //  Запуск задачи, выполняющей основную работу.
        _ = Task.Run(async delegate
        {
            //  Выполнение основной работы.
            await InvokeAsync(cancellationToken);
        }, cancellationToken);
    }

    /// <summary>
    /// Добавляет действие в механизм поддержки.
    /// </summary>
    /// <param name="action">
    /// Действие, добавляемое в механизм поддержки.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    public void Add(AsyncAction action)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action);

        //  Проверка объекта.
        ConcurrentQueue<AsyncAction> actions = IsActual();

        //  Добавление действия в очередь.
        actions.Enqueue(action);
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    /// <param name="disposing">
    /// Значение, определяющее требуется ли освободить управляемое состояние.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        //  Подавление некритических исключений.
        DefyCritical(delegate
        {
            //  Получение очереди действий.
            ConcurrentQueue<AsyncAction>? actions = Interlocked.Exchange(ref _Actions, null);

            //  Очистка очереди действий.
            actions?.Clear();
        });

        //  Вызов метода базового класса.
        base.Dispose(disposing);
    }

    /// <summary>
    /// Выполняет проверку объекта.
    /// </summary>
    /// <returns>
    /// Очередь действий.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConcurrentQueue<AsyncAction> IsActual()
    {
        //  Получение очереди действий.
        ConcurrentQueue<AsyncAction> actions =
            Volatile.Read(ref _Actions) ??
            throw ExceptionsCreator.OperationObjectDisposed(nameof(Keeper));

        //  Возврат очереди действий.
        return actions;
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Подавление некритических исключений.
            DefyCritical(delegate
            {
                //  Получение очереди действий.
                if (Volatile.Read(ref _Actions) is ConcurrentQueue<AsyncAction> actions)
                {
                    //  Извлечение действий из очереди.
                    while (actions.TryDequeue(out AsyncAction? action))
                    {
                        //  Проверка действия.
                        if (action is not null)
                        {
                            //  Асинхронное выполнение.
                            _ = Task.Run(async delegate
                            {
                                //  Основной поддержки.
                                while (!cancellationToken.IsCancellationRequested)
                                {
                                    //  Блок перехвата всех исключений.
                                    try
                                    {
                                        //  Выполнение действия.
                                        await action(cancellationToken).ConfigureAwait(false);

                                        //  Завершение поддержки.
                                        return;
                                    }
                                    catch (Exception ex)
                                    {
                                        //  Проверка критического исключения.
                                        if (ex.IsCritical())
                                        {
                                            //  Повторный выброс исключения.
                                            throw;
                                        }

                                        //  Проверка токена отмены.
                                        if (cancellationToken.IsCancellationRequested)
                                        {
                                            //  Завершение поддержки.
                                            return;
                                        }
                                    }

                                    //  Ожидание перед следующим проходом.
                                    await Task.Delay(_Period, cancellationToken).ConfigureAwait(false);
                                }
                            }, cancellationToken);
                        }
                    }
                }
            });

            //  Ожидание перед следующим проходом.
            await Task.Delay(_Period, cancellationToken).ConfigureAwait(false);
        }
    }
}
