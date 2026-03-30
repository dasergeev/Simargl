//using Simargl.EventsArgs;
//using Simargl.Platform.Journals;
//using Simargl.Platform.TcpClientReceiver;
//using Simargl.Platform.Transmitters;
//using System.Net;
//using System.Net.Sockets;
//using System.Reflection;
//using System;
//using Simargl.Designing;
//using System.Threading.Tasks;
//using System.Threading;
//using System.IO;
//using Simargl.Support;
//using static Simargl.Designing.Verify;

//namespace Simargl.Platform.Sensors;


///// <summary>
///// Представляет класс датчика ADXL.
///// </summary>
//public class AdxlSensor
//{
//    /// <summary>
//    /// Константа определяющая размер пакета данных от датчика.
//    /// </summary>
//    public const int AdxlPackageSize = 636;

//    /// <summary>
//    /// Константа определяющая порт интерфейса Modbus.
//    /// </summary>
//    public const int ModbusPort = 502;

//    /// <summary>
//    /// Представляет конфигурацию передатчика.
//    /// </summary>
//    private readonly TransmittersOptions _TransmitterOption;

//    /// <summary>
//    /// Представляет средство логирования.
//    /// </summary>
//    private readonly Journal _Journal;

//    /// <summary>
//    /// Представляет инструмент получения и сохранения данных.
//    /// </summary>
//    private readonly StreamReceiver _Receiver;

//    /// <summary>
//    /// Возвращает интерфейс Modbus датчика.
//    /// </summary>
//    public AdxlModbus Modbus { get; private set; }

//    /// <summary>
//    /// Представляет событие получения данных.
//    /// </summary>
//    public event EventHandler<ByteArrayEventArgs>? Receive;

//    /// <summary>
//    /// Инициализирует объект класса.
//    /// </summary>
//    /// <param name="journal">Журнал</param>
//    /// <param name="address">IP адресс датчика.</param>
//    /// <param name="options">Опции передатчика.</param>
//    /// <exception cref="ArgumentNullException">В параметре передана пустая ссылка.</exception>
//    public AdxlSensor(Journal journal, IPAddress address, TransmittersOptions options)
//    {
//        //  Установка средства логирования.
//        _Journal = IsNotNull(journal, nameof(journal));

//        //  Установка настроек.
//        _TransmitterOption = IsNotNull(options, nameof(options));

//        //  Установка интерфейса модбас.
//        Modbus = new AdxlModbus(IsNotNull(address, nameof(address)), ModbusPort);

//        //  Создание получателя
//        _Receiver = new(_Journal, AdxlPackageSize, _TransmitterOption);

//        //  Подпись на событие
//        _Receiver.Receive += OnData;
//    }


//    /// <summary>
//    /// Асинхронно выполняет работу с подключением датчика Adxl.
//    /// </summary>
//    /// <param name="tcpClient">
//    /// TCP-клиент датчика.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая работу с подключением датчика.
//    /// </returns>
//    internal async Task StreamRoutineAsync(TcpClient tcpClient, CancellationToken cancellationToken)
//    {

//        //  Установка токена отмены.
//        using var clientToken = new CancellationTokenSource();

//        //  Связывание токенов.
//        using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, clientToken.Token);

//        //  Получение токена отмены.
//        var token = tokenSource.Token;

//        //  Клиент для работы с датчиком.
//        using TcpClient client = tcpClient;

//        //  Проверка соединения.
//        if (client.Client.RemoteEndPoint is not IPEndPoint point)
//        {
//            //  Выброс исключения.
//            throw new IOException($"Подключилось неизвестное устройство на порт Adxl {client.Client.RemoteEndPoint}.");
//        }

//        //  Получение массива байт.
//        IPAddress address = point.Address;

//        //  Получение идентификатора.
//        long identifier = BitConverter.ToInt32(address.GetAddressBytes());

//        //  Вывод информации в журнал.
//        await _Journal.LogInformationAsync($"Подключен датчик Adxl: {address}.", cancellationToken);

//        try
//        {
//            //  Поток для чтения данных.
//            using NetworkStream stream = client.GetStream();

//            //  Время получения последнего пакета.
//            DateTime lastTime = DateTime.Now;

//            //  Получение данных 
//            await _Receiver.ReceiveBufferAsync(stream, token).ConfigureAwait(false);
//        }
//        catch (Exception e)
//        {  
//            //  Вывод информации в журнал.
//            await _Journal.LogInformationAsync($"Датчик Adxl = ({address}) выбросил исключение:{e.Message}", cancellationToken);

//            //  Преверка исключения
//            if (e.IsCritical())
//            {
//                //  Переброс исключения.
//                throw;
//            }
//        } 
//    }




//    /// <summary>
//    /// Вовзращает имена и типы всех свойств AdxlModbusSensor.
//    /// </summary>
//    /// <returns>Имена и типы всех свойств</returns>
//    public static PropertyInfo[] GetAllProperty()
//    {

//        //  Получение типа.
//        Type typeClass = typeof(AdxlModbus);

//        //  Получение свойства.
//        PropertyInfo[] infos = typeClass.GetProperties();

//        //  Возврат значений.
//        return infos;
//    }

//    /// <summary>
//    /// Представляет функцию обработки события.
//    /// </summary>
//    /// <param name="obj">Объект.</param>
//    /// <param name="e">Аргумент.</param>
//    private void OnData(object? obj, ByteArrayEventArgs e)
//    {
//        //  Переброс события.
//        Receive?.Invoke(this, e);    
//    }
//}
