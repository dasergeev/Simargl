//using Simargl.Platform.Journals;
//using Simargl.Designing;
//using Simargl.Support;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Threading;
//using System.Threading.Tasks;
//using static Simargl.Designing.Verify;

//namespace Simargl.Platform.Transmitters;

///// <summary>
///// Представляет класс рассылки данных серии адресатов
///// </summary>
//internal class UdpTransmitter: ITransmitter
//{
//    /// <summary>
//    /// Представляет интерфейс логирования.
//    /// </summary>
//    private readonly Journal _Journal;

//    /// <summary>
//    /// Представляет список конечных получателей.
//    /// </summary>
//    private IReadOnlyList<TransmitterEndPoint> EthernetPoints { get; set; }

//    /// <summary>
//    /// Инциализирует экзепляр класса.
//    /// </summary>
//    /// <param name="journal">
//    /// Журнал.
//    /// </param>
//    /// <param name="ethernetPoints">
//    /// Конечные получатели.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметр <paramref name="journal"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметр <paramref name="ethernetPoints"/> передана пустая ссылка.
//    /// </exception>
//    internal UdpTransmitter(Journal journal,IReadOnlyList<TransmitterEndPoint> ethernetPoints)
//    {
//        //  Установка интерфейса логирования.
//        _Journal = IsNotNull(journal,nameof(journal));

//        //  Установка списка.
//        EthernetPoints = IsNotNull(ethernetPoints,nameof(ethernetPoints));
//    }

//    /// <summary>
//    /// Рассылает сообщение серии адресатов.
//    /// </summary>
//    /// <param name="data">
//    /// Данные.
//    /// </param>
//    /// <param name="token">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача.
//    /// </returns>
//    public async Task SendAsync(byte[] data, CancellationToken token)
//    {
//        //  Проверка ссылки на данные
//        if (data is not null)
//        {
//            foreach (var point in EthernetPoints)
//            {
//                //  Пересылка данных
//                try
//                {
//                    //  Получение строки IP адреса сервера NMEA
//                    string ipStr = IsNotNull(point.IP, nameof(point.IP));

//                    //  Получение IP адреса сервера NMEA
//                    IPAddress ipAddressSendServer = IPAddress.Parse(ipStr);

//                    //  Получение конечной точки сервера NMEA
//                    IPEndPoint pointResend = new(ipAddressSendServer, point.Port);

//                    //  Создание UDP-клиента.
//                    using UdpClient udpResender = new();

//                    //  Создание токена отмены
//                    using var source = new CancellationTokenSource();

//                    //  Отмена задачи пересылки в случае приостановки 
//                    source.CancelAfter(200);

//                    //  Пересылка данных
//                    await udpResender.SendAsync(data, pointResend, source.Token).ConfigureAwait(false);
//                }
//                catch (Exception ex)
//                {
//                    //  Проверка исключения
//                    if (ex.IsSystem())
//                    {
//                        //  Перенаправление исключения
//                        throw;
//                    }

//                    //  Логирование исключения.
//                    await _Journal.LogErrorAsync($"UdpTransmitter:{ex.Message}", token).ConfigureAwait(false);
//                }
//            }
//        }
//    }
    
//    /// <summary>
//    /// Представляет функцию отправки массива.
//    /// </summary>
//    /// <param name="data">
//    /// Массив.
//    /// </param>
//    public void Send(byte[] data)
//    {
//        //  Проверка ссылки на данные
//        if (data is not null)
//        {
//            foreach (var point in EthernetPoints)
//            {
//                //  Пересылка данных
//                try
//                {
//                    //  Получение строки IP адреса сервера NMEA
//                    string ipStr = IsNotNull(point.IP, nameof(point.IP));

//                    //  Получение IP адреса сервера NMEA
//                    IPAddress ipAddressSendServer = IPAddress.Parse(ipStr);

//                    //  Получение конечной точки сервера NMEA
//                    IPEndPoint pointResend = new(ipAddressSendServer, point.Port);

//                    //  Создание UDP-клиента.
//                    using UdpClient udpResender = new();

//                    //  Создание токена отмены
//                    using var source = new CancellationTokenSource();

//                    //  Отмена задачи пересылки в случае приостановки 
//                    source.CancelAfter(200);

//                    //  Пересылка данных
//                    udpResender.Send(data, pointResend);
//                }
//                catch (Exception ex)
//                {
//                    //  Проверка исключения
//                    if (ex.IsSystem())
//                    {
//                        //  Перенаправление исключения
//                        throw;
//                    }
//                }
//            }
//        }
//    }
//}
