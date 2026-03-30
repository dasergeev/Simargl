using System.Net;

namespace Simargl.Hardware.Recorder.Measurement;

/// <summary>
/// Представляет коллекцию индикаторов измерения.
/// </summary>
public sealed class MeasurementIndicatorCollection :
    IEnumerable<MeasurementIndicator>
{
    /// <summary>
    /// Поле для хранения массива индикаторов измерения.
    /// </summary>
    private readonly MeasurementIndicator[] _Items;

    /// <summary>
    /// Поле для хранения карты индикаторов тензометрических модулей.
    /// </summary>
    private readonly Dictionary<string, Dictionary<int, MeasurementIndicator>> _StrainItems;

    /// <summary>
    /// Поле для хранения карты индикаторов датчиков ускорений.
    /// </summary>
    private readonly Dictionary<string, Dictionary<int, MeasurementIndicator>> _AdxlItems;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="options">
    /// Настройки.
    /// </param>
    public MeasurementIndicatorCollection(MeasurementOptions options)
    {
        //  Создание списка индикаторов измерения.
        List<MeasurementIndicator> items = [];

        //  Номер индикатора.
        int number = 1;

        //  Перебор источников.
        foreach (MeasurementSource source in options.Sources)
        {
            //  Перебор имён.
            for (int i = 0; i != source.Names.Count; i++)
            {
                //  Добавление индикатора.
                items.Add(new(
                    number, source.Type, source.Source, source.Serial, i,
                    source.Names[i], source.Units[i], source.Scales[i]));

                //  Переход к следующему номеру.
                number++;
            }
        }

        //  Получение массива индикаторов измерения.
        _Items = [.. items];

        //  Получение карты индикаторов тензометрических модулей.
        _StrainItems = items
            .Where(x => x.Type == "Strain")
            .GroupBy(x => x.Serial)
            .Select(x => new
            {
                x.Key,
                Values = x.ToDictionary(x => x.Index)
            })
            .ToDictionary(x => x.Key, x => x.Values);

        //  Получение карты индикаторов датчиков ускорений.
        _AdxlItems = items
            .Where(x => x.Type == "Adxl")
            .GroupBy(x => x.Source)
            .Select(x => new
            {
                x.Key,
                Values = x.ToDictionary(x => x.Index)
            })
            .ToDictionary(x => x.Key, x => x.Values);
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Length;

    /// <summary>
    /// Возвращает элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Элемент с указанным индексом.
    /// </returns>
    public MeasurementIndicator this[int index] => _Items[index];

    /// <summary>
    /// Добавляет данные тензомодуля.
    /// </summary>
    /// <param name="address">
    /// Адрес датчика.
    /// </param>
    /// <param name="data">
    /// Данные.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public bool TryAddAdxl(IPAddress address, float[][] data)
    {
        //  Поиск в карте модуля.
        if (!_AdxlItems.TryGetValue(address.ToString(), out Dictionary<int, MeasurementIndicator>? map) ||
            map is null)
        {
            //  Модуль не найден.
            return false;
        }

        //  Перебор индексов.
        for (int index = 0; index < 4; index++)
        {
            //  Поиск индикатора.
            if (map.TryGetValue(index, out MeasurementIndicator? indicator) &&
                indicator is not null)
            {
                //  Добавление значений.
                indicator.AddValues(data[index]);
            }
        }

        //  Модуль найден.
        return true;
    }

    /// <summary>
    /// Добавляет данные тензомодуля.
    /// </summary>
    /// <param name="serialNumber">
    /// Серийный номер.
    /// </param>
    /// <param name="data">
    /// Данные.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public bool TryAddStrain(uint serialNumber, float[][] data)
    {
        //  Поиск в карте модуля.
        if (!_StrainItems.TryGetValue($"{serialNumber:X8}", out Dictionary<int, MeasurementIndicator>? map) ||
            map is null)
        {
            //  Модуль не найден.
            return false;
        }

        //  Перебор индексов.
        for (int index = 0; index < 4; index++)
        {
            //  Поиск индикатора.
            if (map.TryGetValue(index, out MeasurementIndicator? indicator) &&
                indicator is not null)
            {
                //  Добавление значений.
                indicator.AddValues(data[index]);
            }
        }

        //  Модуль найден.
        return true;
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<MeasurementIndicator> GetEnumerator()
    {
        //  Возврат перечислителя массива элементов.
        return ((IEnumerable<MeasurementIndicator>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя массива элементов.
        return _Items.GetEnumerator();
    }
}
