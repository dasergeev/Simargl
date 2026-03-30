using System.Linq;
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
/// <param name="spectrum">
/// Спектр.
/// </param>
[CLSCompliant(false)]
public sealed class SpectrumChart(Core core, Chart chart, AccelEth3TSpectrum spectrum) :
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

        //  Получение данных спектра.
        double[] y = spectrum.Spectrum.Select(value => value.Magnitude).ToArray();

        //  Определение шага времени.
        double dX = spectrum.Spectrum.FrequencyStep;

        //  Определение длины данных.
        int length = y.Length;

        //  Создание массива точек.
        DataPoint[] points = new DataPoint[length];
        DataPoint[] min = new DataPoint[length];
        DataPoint[] max = new DataPoint[length];

        //  Перебор точек.
        for (int i = 0; i < length; i++)
        {
            //  Установка точки.
            points[i] = new(i * dX, y[i]);
            min[i] = new(i * dX, spectrum.StoreMin[i]);
            max[i] = new(i * dX, spectrum.StoreMax[i]);
        }

        //  Отправка данных на график.
        await SendAsync(0, points, cancellationToken).ConfigureAwait(false);
        await SendAsync(1, min, cancellationToken).ConfigureAwait(false);
        await SendAsync(2, max, cancellationToken).ConfigureAwait(false);
    }
}
