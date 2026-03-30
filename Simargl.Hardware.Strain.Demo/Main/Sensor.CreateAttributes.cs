using Simargl.Hardware.Strain.Demo.Main.Attributes;

namespace Simargl.Hardware.Strain.Demo.Main;

partial class Sensor
{
    /// <summary>
    /// Создаёт атрибуты.
    /// </summary>
    private void CreateAttributes()
    {
        Information.Add("Серийный номер", $"{Serial:X8}");
        Information.Add("Порт Modbus", $"{DefaultPort}");
        Information.Add("Адрес датчика", x => AddressChanged += x, () => $"{Address}");
        Information.Add(Properties.SensorType, SensorAttributeFormat.Readable);
        Information.Add(Properties.FirmwareVersion, SensorAttributeFormat.Readable);
        Information.Add(Properties.FirmwareDate, SensorAttributeFormat.Readable);
        Information.Add(Properties.ChannelsCount, SensorAttributeFormat.Readable);

        Condition.Add("Обновление адреса", this, nameof(AddressUpdateTime), "HH:mm dd.MM.yyyy");
        Condition.Add("Modbus-подключение", x => IsConnectedChanged += x, () => IsConnected ? "активно" : "неактивно");
        Condition.Add("Регистрация данных", this, nameof(IsRegistrationText));
        Condition.Add("Длительность Modbus-подключения", this, nameof(ConnectionDuration), "hh\\:mm\\:ss");
        Condition.Add(Properties.MaxTemperature, SensorAttributeFormat.Resettable);
        Condition.Add(Properties.MinTemperature, SensorAttributeFormat.Resettable);
        Condition.Add(Properties.MaxVoltage, SensorAttributeFormat.Resettable);
        Condition.Add(Properties.MinVoltage, SensorAttributeFormat.Resettable);
        Condition.Add(Properties.TimeOffset, SensorAttributeFormat.Readable);
        Condition.Add(Properties.Chip0ErrorСodes, SensorAttributeFormat.Resettable, codesConverter);
        Condition.Add(Properties.Chip1ErrorСodes, SensorAttributeFormat.Resettable, codesConverter);
        Condition.Add(Properties.Chip2ErrorСodes, SensorAttributeFormat.Resettable, codesConverter);
        Condition.Add(Properties.Chip3ErrorСodes, SensorAttributeFormat.Resettable, codesConverter);
        Condition.Add(Properties.Status, SensorAttributeFormat.Readable, statusConverter);
        Condition.Add("Время получения последнего пакета", this, nameof(ReceivingTime), "HH:mm:ss.ffff dd.MM.yyyy");
        Condition.Add("SyncFlag", this, nameof(SyncFlag));

        Condition.Add("TimeUnix", this, nameof(TimeUnix));
        Condition.Add("TimeNano", this, nameof(TimeNano));
        Condition.Add("CpuTemp", this, nameof(CpuTemp));
        Condition.Add("SensorTemp", this, nameof(SensorTemp));
        Condition.Add("CpuPower", this, nameof(CpuPower));

        Settings.Add(Properties.UdpPortIdentification, SensorAttributeFormat.Writable);
        Settings.Add(Properties.UdpPortLogging, SensorAttributeFormat.Writable);
        Settings.Add(Properties.TCPServerPort, SensorAttributeFormat.Writable);
        Settings.Add(Properties.NetworkAddress, SensorAttributeFormat.Writable);
        Settings.Add(Properties.SubnetMask, SensorAttributeFormat.Writable);
        Settings.Add(Properties.ServerAddress, SensorAttributeFormat.Writable);
        Settings.Add(Properties.DesiredSampling, SensorAttributeFormat.Writable);
        var att = Settings.Add(Properties.RealSampling, SensorAttributeFormat.Readable);
        att.ValueChanged += delegate (object? sender, EventArgs e)
        {
            if (float.TryParse(att.Value, out float sampling))
            {
                _Sampling = sampling;
            }
        };

        Settings.Add(Properties.RealBandwidth, SensorAttributeFormat.Readable);
        Settings.Add(Properties.FilteringMode, ["Быстро меняющийся процесс", "Медленно меняющийся процесс", "Неизвестное значение"]);
        Settings.Add(Properties.AutoZeroMode, ["Нет", "Запись нулей при старте", "Неизвестное значение"]);

        static string codesConverter(ushort value)
        {
            //  Создание посторителя строки.
            StringBuilder builder = new($"{Convert.ToString(value, 2).PadLeft(16, '0')}");

            //  Проверка значения.
            if (value == 0)
            {
                builder.AppendLine();
                builder.Append("Нет ошибок");
            }
            else
            {
                //  Перебор битов.
                for (int i = 0; i < 16; i++)
                {
                    //  Проверка бита.
                    if ((value & 1 << i) != 0)
                    {
                        builder.AppendLine();
                        builder.Append($"Бит {i}: ");
                        builder.Append(i switch
                        {
                            0 => "Отсутствует питание или сигнал сенсорных линий (NOREF)",
                            1 => "Обрыв сигнальных линий (OPEN_CIRCUIT)",
                            2 => "Короткое замыкание сигнальных линий (SHORT_CIRCUIT)",
                            3 => "Ошибка обмена с чипом (COMM_ERROR)",
                            4 => "Чип не найден (CHIP_NOT_FOUND)",
                            _ => "Неизвестная ошибка",
                        });

                    }
                }
            }

            //  Возврат строки.
            return builder.ToString();
        }

        static string statusConverter(ushort value)
        {
            return value switch
            {
                0 => "Простаивает (нет соединения с сервером)",
                1 => "Идет регистрация и передача на сервер",
                _ => $"Неизвестное состояние ({value})",
            };
        }
    }
}
