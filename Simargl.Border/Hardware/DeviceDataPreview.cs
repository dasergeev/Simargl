using Simargl.Border.Processing;
using System.Drawing;

namespace Simargl.Border.Hardware;

/// <summary>
/// Представляет предварительный просмотр данных устройства.
/// </summary>
public sealed class DeviceDataPreview(Processor processor) :
    ProcessorUnit(processor)
{
    /// <summary>
    /// Поле для хранения точек.
    /// </summary>
    private readonly ConcurrentQueue<(Synchromarker X, float Y)>[] _Points = [[], [], []];

    /// <summary>
    /// Асинхронно регистрирует полученные данные.
    /// </summary>
    /// <param name="package">
    /// Пакет данных от модуля Т.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, регистрирующая данные.
    /// </returns>
    internal async Task RegisterAsync(DevicePackage package, CancellationToken cancellationToken)
    {
        //  Постоянная, определяющая размер списка точек.
        const int pointsCount = (int)(-BasisConstants.PointsXMin * 1.1 * 40);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Добавление точек.
        _Points[0].Enqueue((package.Synchromarker, (float)package.Data0.Average()));
        _Points[1].Enqueue((package.Synchromarker, (float)package.Data1.Average()));
        _Points[2].Enqueue((package.Synchromarker, (float)package.Data2.Average()));

        //  Удаление лишних точек.
        foreach (var points in _Points)
            while (points.Count > pointsCount)
                points.TryDequeue(out _);
    }

    /// <summary>
    /// Возвращает точки.
    /// </summary>
    /// <param name="index">
    /// Индекс последовательности.
    /// </param>
    /// <returns>
    /// Коллекция точек.
    /// </returns>
    public IReadOnlyList<PointF> GetPoints(int index)
    {
        //  Получение текущего времени.
        DateTime now = DateTime.Now;

        //  Возврат точек для графика.
        return [.. _Points[index].ToArray()
            .Select(x => new PointF(
                (float)(Processor.Synchronizer.Convert(x.X) - now).TotalSeconds, x.Y))
            .Where(x => x.X >= BasisConstants.PointsXMin && x.X <= 0)];
    }
}
