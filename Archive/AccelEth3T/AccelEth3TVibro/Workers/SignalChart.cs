using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет объект, обновляющий данные графиков.
/// </summary>
/// <param name="core">
/// Ядро.
/// </param>
/// <param name="chart">
/// Элемент управления, отображающий график.
/// </param>
/// <param name="signal">
/// Сигнал.
/// </param>
[CLSCompliant(false)]
public sealed class SignalChart(Core core, Chart chart, AccelEth3TSignal signal) :
    ChartWorker(core, chart)
{
    /// <summary>
    /// Асинхронно обновляет график.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обновление графика.
    /// </returns>
    protected override async Task UpdateAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение данных сигнала.
        double[] y = (double[])signal.Signal.Items.Clone();

        //  Определение шага времени.
        double dX = 1.0 / signal.Signal.Sampling;

        //  Определение длины данных.
        int length = y.Length;

        //  Определение смещения.
        int offset = 1 - length;

        //  Создание массива точек.
        DataPoint[] points = new DataPoint[length];

        //  Перебор точек.
        for (int i = 0; i < length; i++)
        {
            //  Установка точки.
            points[i] = new((i + offset) * dX, y[i]);
        }

        //  Отправка данных на график.
        await SendAsync(0, points, cancellationToken).ConfigureAwait(false);
        await SendAsync(1, [new(-Settings.SignalDuration, signal.StoreMin), new(0, signal.StoreMin)], cancellationToken).ConfigureAwait(false);
        await SendAsync(2, [new(-Settings.SignalDuration, signal.StoreMax), new(0, signal.StoreMax)], cancellationToken).ConfigureAwait(false);


    }
}
