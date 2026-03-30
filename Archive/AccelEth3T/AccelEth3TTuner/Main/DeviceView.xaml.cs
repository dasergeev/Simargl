using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет элемент управления, отображающий информацию о датчике.
/// </summary>
partial class DeviceView
{
    /// <summary>
    /// Поле для хранения устройства.
    /// </summary>
    private AdxlDevice? _Device;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public DeviceView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// Устанавливает устройство.
    /// </summary>
    /// <param name="device">
    /// Устройство.
    /// </param>
    public void SetDeivce(AdxlDevice? device)
    {
        //  Установка настроек.
        _Device = device;

        //  Установка нового источника настроек.
        _ListView.ItemsSource = device?.Parameters;
    }

    /// <summary>
    /// Обрабатывает событие загрузки текущих настроек.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Load(object sender, RoutedEventArgs e)
    {
        //  Получение устройства.
        if (_Device is AdxlDevice device)
        {
            //  Обновление значений.
            _ = Task.Run(async () => await device.UpdateAsync(default));

            //  Перебор параметров.
            foreach (AdxlDeviceParameter parameter in device.Parameters)
            {
                //  Установка текущего свойства.
                parameter.Value = parameter.ActiveValue;
            }
        }
    }

    /// <summary>
    /// Обрабатывает событие установки текущих настроек.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Save(object sender, RoutedEventArgs e)
    {
        //  Получение устройства.
        if (_Device is AdxlDevice device)
        {
            //  Запуск асинхронной задачи.
            Task.Run(async delegate
            {
                //  Безопасное выполнение.
                try
                {
                    CancellationToken cancellationToken = default;

                    //  Перебор параметров.
                    foreach (AdxlDeviceParameter parameter in device.Parameters)
                    {
                        //  Безопасное выполнение.
                        try
                        {
                            //  Сохранение значения в датчик.
                            parameter.Save();
                        }
                        catch { }
                    }

                    try
                    {
                        await device.Connect.WriteAddressAsync(device.Address, cancellationToken);
                    }
                    catch { }

                    try
                    {
                        await device.Connect.WriteMaskAsync(device.Mask, cancellationToken);
                    }
                    catch { }

                    try
                    {
                        await device.Connect.WriteGatewayAsync(device.Gateway, cancellationToken);
                    }
                    catch { }

                    try
                    {
                        await device.Connect.WriteServerAsync(device.Server, cancellationToken);
                    }
                    catch { }

                    try
                    {
                        await device.Connect.WriteMaxTemperatureAsync(0, cancellationToken);
                    }
                    catch { }

                    try
                    {
                        await device.Connect.WriteMinTemperatureAsync(0, cancellationToken);
                    }
                    catch { }

                    try
                    {
                        await device.Connect.WriteMaxVoltageAsync(0, cancellationToken);
                    }
                    catch { }

                    try
                    {
                        await device.Connect.WriteMinVoltageAsync(0, cancellationToken);
                    }
                    catch { }

                    try
                    {
                        await device.Connect.ResetErrorCodesAsync(cancellationToken);
                    }
                    catch { }


                    /*


                   


    public void WriteUseDhcp(bool value)
    public void WriteUdpPort(ushort value)
    public void WriteTcpPort(ushort value)
    public void WriteMeasuringRange(ushort value)
    public void WriteHighPassFilter(ushort value)
    public void WriteSampling(ushort value)
    public void WriteXOffset(float value)
    public void WriteYOffset(float value)
    public void WriteZOffset(float value)
    public void WriteAddress(IPv4Address value)
    public void WriteMask(IPv4Address value)
    public void WriteGateway(IPv4Address value)
    public void WriteServer(IPv4Address value)
    public void WriteMaxTemperature(float value)
    public void WriteMinTemperature(float value)
    public void WriteMaxVoltage(float value)
    public void WriteMinVoltage(float value)
    public void WriteState(ushort value)

                    */


                    //  Перезагрузка датчика.
                    await device.RebootAsync(default).ConfigureAwait(false);
                }
                catch { }
            }).GetAwaiter().GetResult();
        }
    }
}
