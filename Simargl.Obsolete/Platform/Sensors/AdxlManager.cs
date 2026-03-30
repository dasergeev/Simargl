//using System.Collections.ObjectModel;
//using System.Net;
//using System.Net.NetworkInformation;
//using System.Net.Sockets;
//using Simargl.Designing;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using Simargl.Support;
//using System.Linq;
//using static Simargl.Designing.Verify;

//namespace Simargl.Platform.Sensors;

///// <summary>
///// Представляет класс организующий работу с датчиками Adxl
///// </summary>
//public class AdxlManager :
//    IDisposable
//{
//    /// <summary>
//    /// Представляет объект синхронизации.
//    /// </summary>
//    private readonly object _SyncRoot = new();

//    /// <summary>
//    /// Представляет флаг освобождения ресурсов.
//    /// </summary>
//    private bool _Disposed = false;

//    /// <summary>
//    /// Представляет список серверов.
//    /// </summary>
//    public List<AdxlServer> ServersList { get; } = new();

//    /// <summary>
//    /// Представляет список датчиков.
//    /// </summary>
//    public List<AdxlSensor> AdxlSensors { get; } = new();

//    /// <summary>
//    /// Возвращает список найденых устройств.
//    /// </summary>
//    public ObservableCollection<AdxlModbus> ScanedList { get; } = new();

//    /// <summary>
//    /// Представляет токен отмены приложения.
//    /// </summary>
//    private readonly CancellationToken _ServiceToken;

//    /// <summary>
//    /// Инициализирует объект класса.
//    /// </summary>
//    public AdxlManager([NoVerify] CancellationToken serviceToken)
//    {
//        //  Присвоение токена отмены.
//        _ServiceToken = serviceToken;
//    }


//    /// <summary>
//    /// Представляет фунцкию добавления датчика в список.
//    /// </summary>
//    /// <param name="sensor">Датчик для добавления.</param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="sensor"/> передана пустая ссылка.
//    /// </exception>
//    public async Task AddAsync(AdxlSensor sensor)
//    {
//        //  Проверка ссылки.
//        IsNotNull(sensor, nameof(sensor));

//        while(!_ServiceToken.IsCancellationRequested)
//        {
//            try
//            {
//                //  Чтение данных.
//                await sensor.Modbus.LoadAsync(_ServiceToken).ConfigureAwait(false);
//            }
//            catch (Exception ex)
//            {
//                //  Проверка исключения
//                if (ex.IsCritical())
//                {
//                    //  Переброс исключения
//                    throw;
//                }
//                //  Ожидание.
//                await Task.Delay(1000, _ServiceToken).ConfigureAwait(false);

//                //  Продолжение цикла.
//                continue;
//            }

//            break;
//        }

//        //  Проверка токена 
//        if(_ServiceToken.IsCancellationRequested)
//        {
//            //  Возврат из функции.
//            return;
//        }

//        //  Вход в критическую секцию
//        lock (_SyncRoot)
//        {
//            //  Добавление в список.
//            AdxlSensors.Add(sensor);

//            //  Поиск подходящего сервера.
//            var server = ServersList.FirstOrDefault(x => x.Port.Equals(sensor.Modbus.TcpPort));

//            //  Проверка найден ли
//            if (server == default)
//            {
//                //  Создание сервера.
//                server = new(sensor.Modbus.TcpPort);

//                //  Запуск сервера.
//                server.Start(_ServiceToken);

//                //  Добавление сервиса в список.
//                ServersList.Add(server);
//            }

//            //  Добавление датчика.
//            server.Add(sensor);

//        }

//        //  Остановка датчика. (решает проблему повторного запуска)
//        await sensor.Modbus.StopAsync(_ServiceToken).ConfigureAwait(false);

//        //  Запуск датчика.
//        await sensor.Modbus.StartAsync(_ServiceToken).ConfigureAwait(false);

//        //  Перезагрузка датчика.
//        await sensor.Modbus.ResetAsync(_ServiceToken).ConfigureAwait(false);
//    }


//    /// <summary>
//    /// Представляет фунцкию добавления датчика в список.
//    /// </summary>
//    /// <param name="sensor">Датчик для добавления.</param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="sensor"/> передана пустая ссылка.
//    /// </exception>
//    public async Task RemoveAsync(AdxlSensor sensor)
//    {
//        //  Проверка ссылки.
//        IsNotNull(sensor, nameof(sensor));

//        //  Чтение данных.
//        await sensor.Modbus.LoadAsync(_ServiceToken).ConfigureAwait(false);

//        //  Вход в критическую секцию
//        lock (_SyncRoot)
//        {
//            //  Добавление в список.
//            AdxlSensors.Remove(sensor);

//            //  Цикл по всем сервера
//            foreach(var server in ServersList)
//            {
//                //  Удаление датчика
//                server.Remove(sensor);

//                //  Если у сервера нет клиентов
//                if (server.ListenSensor.Count == 0)
//                {
//                    //  Удаление сервера.
//                    ServersList.Remove(server);

//                    //  Освобождение ресурсов.
//                    server.Dispose();

//                    //  Выход из цикла.
//                    break;
//                }
//            }
//        }

//        //  Запуск датчика.
//        await sensor.Modbus.StopAsync(_ServiceToken).ConfigureAwait(false);
//    }


//    /// <summary>
//    /// Проверяет IP адрес на доступность, и тип устройства - датчик.
//    /// </summary>
//    /// <param name="address">
//    /// IP адрес.
//    /// </param>
//    private async Task PingAndTestIPAddressAsync(IPAddress address)
//    {
//        //  Функция пинга
//        static bool PingIP(IPAddress address)
//        {
//            try
//            {
//                //  Создание отправителя
//                Ping pingSender = new();

//                //  Получение ответа
//                PingReply reply = pingSender.Send(address, 100);

//                //  Проверка ответ
//                if (reply.Status == IPStatus.Success)
//                {
//                    //  Возврат результата
//                    return true;
//                }
//                //  Возврат результата
//                return false;
//            }
//            catch (Exception ex)
//            {
//                //  Проверка исключения.
//                if (ex.IsSystem())
//                {
//                    //  Выброс исключения.
//                    throw;
//                }
//                //  Возврат результата
//                return false;
//            }

//        }

//        //  Получения устройства из списка по IP
//        AdxlModbus? device = ScanedList.FirstOrDefault(x=>x.SessionIP.Equals(address));

//        //  Пинг IP адреса
//        if (PingIP(address))
//        {


//            //  Проверка что устройства не найдено в списке
//            if (device == default)
//            {
//                //  Создание нового устройства
//                device = new(address,502);

//                //  Чтение регистров
//                await device.LoadAsync(default);

//                //Блокировка критической секции
//                lock (_SyncRoot)
//                {
//                    //  Добавление в список
//                    ScanedList.Add(device);
//                }
//            }
//            else
//            {
//                try
//                {
//                    //  Чтение регистров
//                    await device.LoadAsync(default);

//                }
//                catch (Exception error) when ((error is InvalidOperationException) || (error is SocketException))
//                {

//                    //  Блокировка критической секции
//                    lock (_SyncRoot)
//                    {
//                        //  Удаление из списка т.к. устройство не доступно для Modbus
//                        ScanedList.Remove(device);
//                    }
//                }
//            }

//        }
//        else
//        {
//            //  Проверка что устройство в списке
//            if (device != default)
//            {
//                //  Блокировка критической секции
//                lock (_SyncRoot)
//                {
//                    //  Удаление из списка т.к. устройство не доступно для Modbus
//                    ScanedList.Remove(device);
//                }
//            }
//        }
//    }



//    /// <summary>
//    /// Сканирует сеть и возвращает IP и индекс датчика.
//    /// </summary>
//    public async Task ScanAsync(string treeByteIPstring)
//    {
//        //  Очистка предыдущей операции сканирования.
//        ScanedList.Clear();

//        //Очистка списка задач.
//        List<Task> taskList = new(); 

//        //Цикл создания задач для каждого IP  в подсети.
//        for (int i = 1; i < 254; i++)
//        {
//            //Инициализация переменных
//            string ipString = $"{treeByteIPstring}{(byte)i}";

//            //  Инициализация IP
//            IPAddress address = IPAddress.Parse(ipString);

//            //Запуск задачи
//            Task? task = Task.Run(async () => await PingAndTestIPAddressAsync(address));

//            //Добавление задачи в список.
//            taskList.Add(task);
//        }

//        while (true)
//        {

//            //  Флаг завершения всех задач.
//            bool isAllTaskComplited = true;

//            //Проверка и анализ выполнения предыдущей операции
//            foreach (var one in taskList)
//            {
//                if (one is null)
//                {
//                    continue;
//                }
//                if (!one.IsCompleted)
//                {
//                    isAllTaskComplited = false;
//                }
//            }

//            //  Проверка что все задачи завершены.
//            if (isAllTaskComplited)
//            {
//                break;
//            }

//            //  Ожидание
//            await Task.Delay(50).ConfigureAwait(false);
//        }
//    }

//    /// <summary>
//    /// Представляет функцию освобождения ресурсов.
//    /// </summary>
//    public void Dispose()
//    {
//        Dispose(disposing: true);
//        GC.SuppressFinalize(this);
//    }

//    /// <summary>
//    /// Представляет функци освобождения ресурсов.
//    /// </summary>
//    /// <param name="disposing">Флаг завершения ресурсов.</param>
//    protected virtual void Dispose(bool disposing)
//    {
//        //  Проверка флага
//        if (_Disposed)
//        {
//            return;
//        }

//        //  Проверка параметра
//        if (disposing)
//        {
//            //  Цикл по всем серверам
//            foreach (var one in ServersList)
//            {
//                //  Освобождение ресурсов.
//                one.Dispose();
//            }

//            //  Очистка списка
//            ServersList.Clear();

//            //  Очистка списка
//            AdxlSensors.Clear();
//        }

//        //  Установка флага
//        _Disposed = true;
//    }
//}
