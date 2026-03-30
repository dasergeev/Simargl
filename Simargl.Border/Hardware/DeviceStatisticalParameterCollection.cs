
namespace Simargl.Border.Hardware;

/// <summary>
/// Представляет коллекцию статистических параметро устройства.
/// </summary>
public sealed class DeviceStatisticalParameterCollection :
    IEnumerable<DeviceStatisticalParameter>
{
    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    private readonly DeviceStatisticalParameter[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public DeviceStatisticalParameterCollection()
    {
        //  Создание элементов коллекции.
        _Items = [TimeContinuous, DataContinuous, LastSynchromarker, SynchromarkerContinuous];
    }

    /// <summary>
    /// Возвращает время непрерывной работы.
    /// </summary>
    public DeviceStatisticalParameter TimeContinuous { get; } = new()
    {
        Name = "Время непрерывной работы",
        Unit = "-"
    };

    /// <summary>
    /// Возвращает размер непрерывно переданных данных в МБ.
    /// </summary>
    public DeviceStatisticalParameter DataContinuous { get; } = new()
    {
        Name = "Размер непрерывных данных",
        Unit = "МБ"
    };

    /// <summary>
    /// Возвращает последний синхромаркер.
    /// </summary>
    public DeviceStatisticalParameter LastSynchromarker { get; } = new()
    {
        Name = "Последний синхромаркер",
        Unit = "-"
    };

    /// <summary>
    /// Возвращает количество непрерывных синхромаркеров.
    /// </summary>
    public DeviceStatisticalParameter SynchromarkerContinuous { get; } = new()
    {
        Name = "Непрерывных синхромаркеров",
        Unit = "-"
    };

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<DeviceStatisticalParameter> GetEnumerator()
    {
        return ((IEnumerable<DeviceStatisticalParameter>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _Items.GetEnumerator();
    }
}
