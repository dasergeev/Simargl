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
/// <param name="power">
/// Сигнал.
/// </param>
[CLSCompliant(false)]
public sealed class PowerChart(Core core, Chart chart, AccelEth3TPower power) :
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

        //  Отправка данных на график.
        await SendAsync(0, power.Points, cancellationToken).ConfigureAwait(false);
        await SendAsync(1, [new(-Settings.SignalDuration, power.StoreMin), new(0, power.StoreMin)], cancellationToken).ConfigureAwait(false);
        await SendAsync(2, [new(-Settings.SignalDuration, power.StoreMax), new(0, power.StoreMax)], cancellationToken).ConfigureAwait(false);
    }
}
