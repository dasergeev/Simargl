using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет объект, работающий с графиком.
/// </summary>
/// <param name="core">
/// Ядро.
/// </param>
/// <param name="chart">
/// Элемент управления, отображающий график.
/// </param>
[CLSCompliant(false)]
public abstract class ChartWorker(Core core, Chart chart) :
    Worker(core)
{
    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private readonly CancellationTokenSource _CancellationTokenSource = new();

    /// <summary>
    /// Останавливает работу объекта.
    /// </summary>
    public void Stop()
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Отправка запроса на отмену.
            _CancellationTokenSource.Cancel();
        }
        catch { }
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
    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Создание связанного источника токена отмены.
        using CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            _CancellationTokenSource.Token, cancellationToken);

        //  Замена токена отмены.
        cancellationToken = linkedTokenSource.Token;

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Основной цикл обновления.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Обновляет график.
                await UpdateAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        catch
        {
            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Повторный выброс исключения.
                throw;
            }
        }
    }

    /// <summary>
    /// Асинхронно обновляет график.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обновление графика.
    /// </returns>
    protected abstract Task UpdateAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно отправляет данные на график.
    /// </summary>
    /// <param name="index">
    /// Индекс графика.
    /// </param>
    /// <param name="points">
    /// Массив точек графика.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, отправляющая данные на график.
    /// </returns>
    protected async Task SendAsync(int index, DataPoint[] points, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение коллекции точек последовательности.
        DataPointCollection chartPoints = chart.Series[index].Points;

        //  Выполнение в основном потоке.
        await Core.Invoker.InvokeAsync(delegate
        {
            //  Приостановка размещения элементов.
            chart.SuspendLayout();

            //  Блок с гарантированным завершением.
            try
            {
                //  Очистка коллекции точек.
                chartPoints.Clear();

                //  Перебор точек последовательности.
                for (int i = 0; i < points.Length; i++)
                {
                    //  Добавление точки.
                    chartPoints.Add(points[i]);
                }
            }
            finally
            {
                //  Восстановление размещения элементов.
                chart.ResumeLayout(false);
                chart.PerformLayout();
            }
        }, cancellationToken).ConfigureAwait(false);
    }
}
