namespace Simargl.Hardware.Recorder.Measurement;

/// <summary>
/// Представляет индикатор измерения.
/// </summary>
/// <param name="number">
/// Номер индикатора.
/// </param>
/// <param name="type">
/// Тип источника индикатора.
/// </param>
/// <param name="source">
/// Источник.
/// </param>
/// <param name="serial">
/// Серийный номер источника.
/// </param>
/// <param name="index">
/// Индекс индикатора.
/// </param>
/// <param name="name">
/// Имя индикатора.
/// </param>
/// <param name="unit">
/// Единица измерения.
/// </param>
/// <param name="scale">
/// Масштаб.
/// </param>
public sealed class MeasurementIndicator(
    int number, string type, string source, string serial, int index,
    string name, string unit, double scale)
{
    /// <summary>
    /// Поле для хранения последнего значения.
    /// </summary>
    private float _LastValue;

    /// <summary>
    /// Поле для хранения времени получения последнего значения.
    /// </summary>
    private DateTime _LastTime = DateTime.MinValue;

    /// <summary>
    /// Возвращает номер индикатора.
    /// </summary>
    public int Number { get; } = number;

    /// <summary>
    /// Возвращает тип источника индикатора.
    /// </summary>
    public string Type { get; } = IsNotNull(type);

    /// <summary>
    /// Возвращает источник индикатора.
    /// </summary>
    public string Source { get; } = IsNotNull(source);

    /// <summary>
    /// Возвращает серийный номер источника.
    /// </summary>
    public string Serial { get; } = IsNotNull(serial);

    /// <summary>
    /// Возвращает индекс индикатора.
    /// </summary>
    public int Index { get; } = index;

    /// <summary>
    /// Возвращает имя индикатора.
    /// </summary>
    public string Name { get; } = IsNotNull(name);

    /// <summary>
    /// Возвращает единицу измерения.
    /// </summary>
    public string Unit { get; } = IsNotNull(unit);

    /// <summary>
    /// Возвращает масштаб.
    /// </summary>
    public double Scale { get; } = scale;

    /// <summary>
    /// Возвращает текущее значение.
    /// </summary>
    public string Value { get; private set; } = "-";

    /// <summary>
    /// Асинхронно обновляет значения.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, обновляющая значения.
    /// </returns>
    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмеы.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка времени.
        if (DateTime.Now - _LastTime < TimeSpan.FromSeconds(10))
        {
            //  Проверка сигнала тензомодуля.
            if (type == "Strain" && Math.Abs(_LastValue - 4) < 0.001)
            {
                //  Установка последнего значения.
                Value = "нет сигнала";
            }
            else
            {
                //  Установка последнего значения.
                Value = (_LastValue * Scale).ToString("0.0000");
            }
        }
        else
        {
            //  Отсутствуют данные.
            Value = "-";
        }
    }

    /// <summary>
    /// Добавляет значения.
    /// </summary>
    /// <param name="values">
    /// Добавляемые значения.
    /// </param>
    public void AddValues(float[] values)
    {
        //  Получение последнего значения.
        _LastValue = values.Average();

        //  Обновление времени получения последнего значения.
        _LastTime = DateTime.Now;
    }
}
