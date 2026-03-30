using Apeiron.Oriole.Server.Performance;
using Apeiron.Platform.Databases.OrioleDatabase;

namespace Apeiron.Oriole.Server.Workers.Common;

/// <summary>
/// Представляет фоновый процесс службы, выполняющий информаирование о сотоянии системы.
/// </summary>
public class Informing :
    Worker<Informing>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public Informing(ILogger<Informing> logger) :
        base(logger)
    {

    }

    /// <summary>
    /// Асинхронно выполняет фоновую работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Задержка для инициализации консоли и выдачи служебных сообщений.
        await Task.Delay(50, cancellationToken).ConfigureAwait(false);

        //  Текущие показатели эффективности.
        Indicators indicators = default;

        //  Получение контекста сеанса работы с базой данных.
        using (OrioleDatabaseContext database = new())
        {
            //  Обновление показателей эффективности.
            indicators = new(database, indicators);
        }

        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Получение контекста сеанса работы с базой данных.
            using (OrioleDatabaseContext database = new())
            {
                //  Обновление показателей эффективности.
                indicators = new(database, indicators);
            }

            //  Вывод информации в журнал о состоянии исполнителя.
            Logger.LogInformation("{indicators}", indicators);

            ////  Освобождение ресурсов.
            //GC.Collect(int.MaxValue, GCCollectionMode.Forced, true);

            //  Ожидание.
            await Task.Delay(300000, cancellationToken).ConfigureAwait(false);
        }
    }
}
