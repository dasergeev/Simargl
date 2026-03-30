using System.Collections;
using System.Collections.Generic;
using System;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет коллекцию параметров датчика.
/// </summary>
public sealed class AdxlDeviceParameterCollection :
    IEnumerable<AdxlDeviceParameter>
{
    /// <summary>
    /// Поле для хранения устройства.
    /// </summary>
    private readonly AdxlDevice _Device;

    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    private readonly AdxlDeviceParameter[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="device">
    /// Устройство.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="device"/> передана пустая ссылка.
    /// </exception>
    public AdxlDeviceParameterCollection(AdxlDevice device)
    {
        //  Установка устройства.
        _Device = IsNotNull(device, nameof(device));

        //  Создание параметров.
        AdxlDeviceParameter sampling = new(
            _Device, 1000, "Частота", "Гц",
            @"Частота дискретизации.
Может принимать следующие значения:
· 125 Гц
· 250 Гц
· 500 Гц
· 1000 Гц
· 2000 Гц
· 4000 Гц",
            false,
            () => _Device.Sampling,
            () => 125,
            () => 4000,
            () => new double[] { 125, 250, 500, 1000, 2000, 4000 },
            () => new string[] { "125 Гц", "250 Гц", "500 Гц", "1000 Гц", "2000 Гц", "4000 Гц" },
            (value, index) => _Device.Connect.WriteSampling((ushort)value));

        AdxlDeviceParameter highPassFilter = new(
            _Device, 0, "Фильтр", "Гц",
            @"Частота фильтра низких частот, значение HighPass фильтра.
Может принимать следующие значения:
· 0 Гц - фильтр отключён
· 0,247 % от частоты дискретизации
· 0,062084 % от частоты дискретизации
· 0,015545 % от частоты дискретизации
· 0,003862 % от частоты дискретизации
· 0,000954 % от частоты дискретизации
· 0,000238 % от частоты дискретизации",
            false,
            () => _Device.HighPassFilter,
            () => 0,
            () => 0.247 * sampling.Value,
            () => new double[] { 0, 0.00247 * sampling.Value, 0.00062084 * sampling.Value, 0.00015545 * sampling.Value, 0.00003862 * sampling.Value, 0.00000954 * sampling.Value, 0.00000238 * sampling.Value },
            () => new string[] { $"{0} Гц", $"{0.00247 * sampling.Value} Гц", $"{0.00062084 * sampling.Value} Гц", $"{0.00015545 * sampling.Value} Гц", $"{0.00003862 * sampling.Value} Гц", $"{0.00000954 * sampling.Value} Гц", $"{0.00000238 * sampling.Value} Гц" },
            (value, index) => _Device.Connect.WriteHighPassFilter((ushort)index));

        AdxlDeviceParameter measuringRange = new(
            _Device, 40, "Диапазон", "g",
            @"Диапазон измерения датчика.
Может принимать следующие значения:
· 10 g
· 20 g
· 40 g",
            false,
            () => _Device.MeasuringRange,
            () => 10,
            () => 40,
            () => new double[] { 10, 20, 40 },
            () => new string[] { "10 g", "20 g", "40 g" },
            (value, index) => _Device.Connect.WriteMeasuringRange((ushort)value));

        AdxlDeviceParameter xOffset = new(
            _Device, 0, "Смещение Ox", "g",
            @"Смещение сигнала по оси Ox.
Значение должно быть в пределах текущего диапазона измерения датчика.",
            true,
            () => _Device.XOffset,
            () => -measuringRange.Value,
            () => measuringRange.Value,
            () => Array.Empty<double>(),
            () => Array.Empty<string>(),
            (value, index) => _Device.Connect.WriteXOffset((float)value));

        AdxlDeviceParameter yOffset = new(
            _Device, 0, "Смещение Oy", "g",
            @"Смещение сигнала по оси Oy.
Значение должно быть в пределах текущего диапазона измерения датчика.",
            true,
            () => _Device.YOffset,
            () => -measuringRange.Value,
            () => measuringRange.Value,
            () => Array.Empty<double>(),
            () => Array.Empty<string>(),
            (value, index) => _Device.Connect.WriteYOffset((float)value));

        AdxlDeviceParameter zOffset = new(
            _Device, 0, "Смещение Oz", "g",
            @"Смещение сигнала по оси Oz.
Значение должно быть в пределах текущего диапазона измерения датчика.",
            true,
            () => _Device.ZOffset,
            () => -measuringRange.Value,
            () => measuringRange.Value,
            () => Array.Empty<double>(),
            () => Array.Empty<string>(),
            (value, index) => _Device.Connect.WriteZOffset((float)value));

        //  Создание хранилища элементов.
        _Items = new AdxlDeviceParameter[]
        {
            sampling,
            highPassFilter,
            measuringRange,
            xOffset,
            yOffset,
            zOffset,
        };

        //  Перебор параметров.
        foreach (AdxlDeviceParameter parameter in _Items)
        {
            //  Добавление обработчика события.
            parameter.PropertyChanged += propertyChanged;
        }

        //  Обрабатывает событие изменения значения свойства параметра.
        void propertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //  Перебор параметров.
            foreach (AdxlDeviceParameter parameter in _Items)
            {
                //  Обновление параметра.
                parameter.Update();
            }
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<AdxlDeviceParameter> GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов.
        return ((IEnumerable<AdxlDeviceParameter>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов.
        return _Items.GetEnumerator();
    }
}
