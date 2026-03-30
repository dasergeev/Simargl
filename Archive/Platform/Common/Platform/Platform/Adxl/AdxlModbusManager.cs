using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;

namespace Apeiron.Platform.Adxl;


/// <summary>
/// Представляет класс настройки Adxl датчиков по Mobdus
/// </summary>
public class AdxlModbusManager
{


    /// <summary>
    /// Префикс IP адреса сканируемой подсети
    /// </summary>
    private const string _MaskOfNetForSearch = "192.168.1.";

    /// <summary>
    /// Список найденых устройств
    /// </summary>
    private readonly List<Task> ListOfSearchDeviceTask = new();

    /// <summary>
    /// Возвращает список датчиков.
    /// </summary>
    public ObservableCollection<AdxlModbusSensor> Sensors { get; } = new ObservableCollection<AdxlModbusSensor>();

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    public AdxlModbusManager()
    {

    }


    /// <summary>
    /// Загружает данные в свойства.
    /// </summary>
    /// <param name="index">
    /// Индекс
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Возвращает задачу.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано не допустимое значение.
    /// </exception>
    public async Task LoadAsync(int index,CancellationToken token)
    {
        //  Проверка на отрицательность.
        Check.IsNotNegative(index, nameof(index));

        //  Проверка на превышение индекса.
        Check.IsNotLarger(index, Sensors.Count - 1, nameof(index));

        //  Загружает данные
        await Sensors[index].LoadAsync(token).ConfigureAwait(false);
    }


    /// <summary>
    /// Записывает свойства в устройства.
    /// </summary>
    /// <param name="index">
    /// Индекс
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Возвращает задачу.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано не допустимое значение.
    /// </exception>
    public async Task UpdateAsync(int index, CancellationToken token)
    {
        //  Проверка на отрицательность.
        Check.IsNotNegative(index, nameof(index));

        //  Проверка на превышение индекса.
        Check.IsNotLarger(index, Sensors.Count - 1, nameof(index));

        //  Загружает данные
        await Sensors[index].UpdateAsync(token).ConfigureAwait(false);
    }

    /// <summary>
    /// Возвращает значение свойства
    /// </summary>
    /// <param name="index">
    /// Индекс
    /// </param>
    /// <param name="property">
    /// Имя свойства.
    /// </param>
    /// <returns>
    /// Возвращает значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="property"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Ошибка получения свойства или значения свойства.
    /// </exception>
    public string GetValue(int index, string property)
    {
        //  Проверка на отрицательность.
        Check.IsNotNegative(index, nameof(index));

        //  Проверка на превышение индекса.
        Check.IsNotLarger(index,Sensors.Count - 1, nameof(index));    

        //  Проверка что передана не пустая ссылка.
        Check.IsNotNull(property, nameof(property));


        try
        {
            //  Получение типа.
            Type type = typeof(AdxlModbusSensor);   

            //  Получение свойства.
            PropertyInfo? info = type.GetProperty(property);

            //  Проверка ссылки.
            info = Check.IsNotNull(info, nameof(property)); 

            //  Получение значения.
            var value = info.GetValue(Sensors[index]);

            //  Проверка ссылки.
            value = Check.IsNotNull(value, nameof(property)).ToString();

            //  Проверка результата.
            string result = Check.IsNotNull(value!.ToString(), nameof(property));    

            //  Возврат результата.
            return result;    
        }
        catch (Exception ex)
        {
            //  Проверка исключения.
            if(ex.IsSystem())
            {
                //  Выброс исключения
                throw;
            }

            //  Выброс исключения.
            throw new ArgumentException("Ошибка получения свойства или получения значения.", ex);
        }

    }


    /// <summary>
    /// Устанавливает значение свойства.
    /// </summary>
    /// <param name="index">
    /// Индекс
    /// </param>
    /// <param name="property">
    /// Имя свойства.
    /// </param>
    /// <param name="value">
    /// Новое значение свойства.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="property"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Ошибка получения свойства или значения свойства.
    /// </exception>
    public void SetValue(int index, string property, string value)
    {
        //  Проверка на отрицательность.
        Check.IsNotNegative(index, nameof(index));

        //  Проверка на превышение индекса.
        Check.IsNotLarger(index, Sensors.Count - 1, nameof(index));

        //  Проверка что передана не пустая ссылка.
        Check.IsNotNull(property, nameof(property));


        try
        {
            //  Получение типа.
            Type type = typeof(AdxlModbusSensor);

            //  Получение свойства.
            PropertyInfo? info = type.GetProperty(property);

            //  Проверка ссылки.
            info = Check.IsNotNull(info, nameof(property));

            //  Проверка, что тип значения IP адресс
            if (info.PropertyType == typeof(IPAddress))
            {
                //  Установка значения.
                info.SetValue(Sensors[index], IPAddress.Parse(value));
            }
            else
            {
                //  Получение конвертера.
                var converter = TypeDescriptor.GetConverter(info.PropertyType);

                //  Получение значения.
                var result = converter.ConvertFrom(value);

                //  Установка значения.
                info.SetValue(Sensors[index], result);
            }
        }
        catch (Exception ex)
        {
            //  Проверка исключения.
            if (ex.IsSystem())
            {
                //  Выброс исключения
                throw;
            }

            //  Выброс исключения.
            throw new ArgumentException("Ошибка получения свойства или установки значения.",ex);
        }

    }

    /// <summary>
    /// Вовзращает имена и типы всех свойств AdxlModbusSensor.
    /// </summary>
    /// <returns>Имена и типы всех свойств</returns>
    public static PropertyInfo[] GetAllPropertyNameAndType()
    {

        //  Получение типа.
        Type typeClass = typeof(AdxlModbusSensor);

        //  Получение свойства.
        PropertyInfo[] infos = typeClass.GetProperties();

        //  Возврат значений.
        return infos;
    }



    /// <summary>
    /// Сканирует сеть и возвращает IP и индекс датчика.
    /// </summary>
    /// <returns>Возвращает IP и индекс датчика.</returns>
    public async Task<string> ScanAsync()
    {
        //Очистка списка задач.
        ListOfSearchDeviceTask.Clear();

        //Цикл создания задач для каждого IP  в подсети.
        for (int i = 1; i < 254; i++)
        {
            //Инициализация переменных
            string ipString = $"{_MaskOfNetForSearch}{(byte)i}";

            //  Инициализация IP
            IPAddress address = IPAddress.Parse(ipString);

            //Запуск задачи
            Task? task = Task.Run(async () => await PingAndTestIPAddressAsync(address));

            //Добавление задачи в список.
            ListOfSearchDeviceTask.Add(task);
        }

        while(true)
        {

            //  Флаг завершения всех задач.
            bool isAllTaskComplited = true;

            //Проверка и анализ выполнения предыдущей операции
            foreach (var one in ListOfSearchDeviceTask)
            {
                if (one is null)
                {
                    continue;
                }
                if (!one.IsCompleted)
                {
                    isAllTaskComplited = false;
                }
            }

            //  Проверка что все задачи завершены.
            if (isAllTaskComplited)
            {
                break;
            }

            //  Ожидание
            await Task.Delay(50).ConfigureAwait(false);
        }


    //  Создание писателя:
    StringWriter sw = new ();

        //  Инициализация индекса
        int index = 0;

        //  Цикл по всем датчикам
        foreach (var one in Sensors)
        {
            //  Запись данных
            await sw.WriteLineAsync($"{index}:{one.ModbusMaster.IP}").ConfigureAwait(false);

            //  Инкремент индекса
            index++;
        }

        //  Возврат значения.
        return sw.ToString();
    }


    /// <summary>
    /// Представляет функцию остановки измерений
    /// </summary>
    /// <param name="index">
    /// Индекс
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Удаленный узел недоступен.
    /// </exception>
    /// <exception cref="IOException">
    /// Ошибка при выполнении запроса.
    /// </exception>
    public async Task StopAsync(int index, CancellationToken cancellationToken)
    {
        //  Проверка на отрицательность.
        Check.IsNotNegative(index, nameof(index));

        //  Проверка на превышение индекса.
        Check.IsNotLarger(index, Sensors.Count - 1, nameof(index));

        //  Подача команды.
        await Sensors[index].StopAsync(cancellationToken).ConfigureAwait(false);
    }



    /// <summary>
    /// Представляет функцию очистки статистики.
    /// </summary>
    /// <param name="index">
    /// Индекс
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Удаленный узел недоступен.
    /// </exception>
    /// <exception cref="IOException">
    /// Ошибка при выполнении запроса.
    /// </exception>
    public async Task ClearStatisticAsync(int index, CancellationToken cancellationToken)
    {
        //  Проверка на отрицательность.
        Check.IsNotNegative(index, nameof(index));

        //  Проверка на превышение индекса.
        Check.IsNotLarger(index, Sensors.Count - 1, nameof(index));

        //  Подача команды очистки температуры.
        await Sensors[index].ClearTempAsync(cancellationToken).ConfigureAwait(false);

        //  Подача команды очистки напряжения.
        await Sensors[index].ClearVoltageAsync(cancellationToken).ConfigureAwait(false);

        //  Подача команды очистки ошибок.
        await Sensors[index].ClearErrorAsync(cancellationToken).ConfigureAwait(false);
    }





    /// <summary>
    /// Представляет функцию старта измерений.
    /// </summary>
    /// <param name="index">
    /// Индекс
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Удаленный узел недоступен.
    /// </exception>
    /// <exception cref="IOException">
    /// Ошибка при выполнении запроса.
    /// </exception>
    public async Task StartAsync(int index, CancellationToken cancellationToken)
    {
        //  Проверка на отрицательность.
        Check.IsNotNegative(index, nameof(index));

        //  Проверка на превышение индекса.
        Check.IsNotLarger(index, Sensors.Count - 1, nameof(index));

        //  Подача команды.
        await Sensors[index].StartAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Представляет перезагрузки датчика.
    /// </summary>
    /// <param name="index">
    /// Индекс
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Удаленный узел недоступен.
    /// </exception>
    /// <exception cref="IOException">
    /// Ошибка при выполнении запроса.
    /// </exception>
    public async Task ResetAsync(int index,CancellationToken cancellationToken)
    {
        //  Проверка на отрицательность.
        Check.IsNotNegative(index, nameof(index));

        //  Проверка на превышение индекса.
        Check.IsNotLarger(index, Sensors.Count - 1, nameof(index));

        try
        {
            //  Подача команды.
            await Sensors[index].ResetAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            //  Проверка исключения.
            if (ex.IsSystem())
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Выгружает значение всех свойств.
    /// </summary>
    /// <param name="index">
    /// Индекс
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано не допустимое значение.
    /// </exception>
    public string PrintAllValue(int index)
    {
        //  Проверка на отрицательность.
        Check.IsNotNegative(index, nameof(index));

        //  Проверка на превышение индекса.
        Check.IsNotLarger(index, Sensors.Count - 1, nameof(index));

        //  Получение свойства.
        var infos = GetAllPropertyNameAndType().OrderBy(x => x.Name);

        //  Проверка ссылки.
        infos = Check.IsNotNull(infos, nameof(infos));

        //  Создание писателя:
        StringWriter sw = new();

        //  Цикл по всем датчикам
        foreach (var info in infos)
        {
            //  Запись данных.
            sw.WriteLine($"{info.Name}: {GetValue(index,info.Name)}");
        }

        return sw.ToString();

    }


    /// <summary>
    /// Проверяет IP адрес на доступность, и тип устройства - датчик.
    /// </summary>
    /// <param name="address">
    /// IP адрес.
    /// </param>
    private async Task PingAndTestIPAddressAsync(IPAddress address)
    {
        //  Функция пинга
        static bool PingIP(IPAddress address)
        {
            try
            {
                //  Создание отправителя
                Ping pingSender = new();

                //  Получение ответа
                PingReply reply = pingSender.Send(address, 100);

                //  Проверка ответ
                if (reply.Status == IPStatus.Success)
                {
                    //  Возврат результата
                    return true;
                }
                //  Возврат результата
                return false;
            }
            catch(Exception ex)
            {
                //  Проверка исключения.
                if (ex.IsSystem())
                {
                    //  Выброс исключения.
                    throw;
                }
                //  Возврат результата
                return false;
            }
            
        }

        //  Получения устройства из списка по IP
        AdxlModbusSensor? device = GetSensorByIP(address);

        //  Пинг IP адреса
        if (PingIP(address))
        {


            //  Проверка что устройства не найдено в списке
            if (device == default)
            {
                //  Создание нового устройства
                device = new(address, AdxlSettings.AdxlModbusPort);

                //  Чтение регистров
                await device.LoadAsync(default);

                //Блокировка критической секции
                lock (Sensors)
                {
                    //  Добавление в список
                    Sensors.Add(device);
                }
            }
            else
            {
                try
                {
                    //  Чтение регистров
                    await device.LoadAsync(default);

                }
                catch (Exception error) when ((error is InvalidOperationException) || (error is SocketException))
                {

                    //  Блокировка критической секции
                    lock (Sensors)
                    {
                        //  Удаление из списка т.к. устройство не доступно для Modbus
                        Sensors.Remove(device);
                    }
                }
            }

        }
        else
        {
            //  Проверка что устройство в списке
            if (device != default)
            {
                //  Блокировка критической секции
                lock (Sensors)
                {
                    //  Удаление устройства
                    Sensors.Remove(device);
                }
            }
        }
    }


    /// <summary>
    /// Возвращает интерфейс датчика из списка известных.
    /// </summary>
    /// <param name="address">
    /// IP адрес
    /// </param>
    /// <returns>
    /// Интерфейс датчика.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="address"/> передана пустая ссылка.
    /// </exception>
    private AdxlModbusSensor? GetSensorByIP(IPAddress address)
    {
        //  Проверка
        address = Check.IsNotNull(address, nameof(address));

        //  Блокировка критической секции
        lock (Sensors)
        {
            //  Поиск
            AdxlModbusSensor? device = Sensors.FirstOrDefault(x => x.ModbusMaster.IP.Equals(address));
            return device;
        }
    }

}
