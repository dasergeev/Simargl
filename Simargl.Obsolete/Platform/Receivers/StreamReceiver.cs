//using Simargl.EventsArgs;
//using Simargl.IO;
//using Simargl.Platform.Journals;
//using Simargl.Platform.Transmitters;
//using Simargl.Designing;
//using System;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Simargl.Platform.TcpClientReceiver;

//internal class StreamReceiver
//{

//    /// <summary>
//    /// Представляет буфер промежуточного копирования.
//    /// </summary>
//    private readonly byte[] _Portion;

//    /// <summary>
//    /// Представляет передатчик класса.
//    /// </summary>
//    private readonly BinaryTransmitter _Transmitter;

//    /// <summary>
//    /// Представляет событие получения данных.
//    /// </summary>
//    public event EventHandler<ByteArrayEventArgs>? Receive;

//    /// <summary>
//    /// Инициализирует объект класса.
//    /// </summary>
//    /// <param name="journal">Журнал</param>
//    /// <param name="transmittersOptions">Опции передатчика.</param>
//    /// <param name="portionSize">Размер порции данных.</param>
//    /// <exception cref="ArgumentNullException">Передан параметр вне диапазона (отрацательный).</exception>
//    public StreamReceiver(Journal journal, int portionSize, TransmittersOptions transmittersOptions)
//    {
//        //  Проверка параметра
//        Verify.IsNotLess(portionSize, 1, nameof(portionSize));

//        //  Инициализация буфера.
//        _Portion = new byte[portionSize];

//        //  Установка передатчика.
//        _Transmitter = new(journal, transmittersOptions);
//    }


//    /// <summary>
//    /// Представляет функцию получения буфера данных.
//    /// </summary>
//    /// <param name="stream">Поток.</param>
//    /// <param name="token">Токен отмены.</param>
//    public async Task ReceiveBufferAsync(NetworkStream stream, CancellationToken token)
//    {
//        //  Время получения первого пакета в файле.
//        DateTime receiveTime = DateTime.Now;

//        //  Создание читателя.
//        Spreader reader = new (stream,Encoding.UTF8);
        
//        //  Выполнять пока есть данные и интервал не истек.
//        while (token.IsCancellationRequested == false)
//        {
        
//            //  Чтение порции данных из потока соединения.
//            await reader.ReadBytesAsync(_Portion,0,_Portion.Length,token).ConfigureAwait(false);

//            //  Передача и запись данных.
//            _Transmitter.SendTransparent(_Portion);

//            // Вызов обработчика события.
//            Receive?.Invoke(this, new(_Portion));
//        }
//        /*
//        //  Выполнять пока есть данные и интервал не истек.
//        while (token.IsCancellationRequested == false)
//        {
//            if (stream.DataAvailable)
//            {
//                //  Чтение порции данных из потока соединения.
//                var count = await stream.ReadAsync(_Portion, token).ConfigureAwait(false);

//                //  Передача и запись данных.
//                _Transmitter.SendTransparent(_Portion);

//                // Вызов обработчика события.
//                Receive?.Invoke(this, new(_Portion));

//                //  Продолжение цикла.
//                continue;
//            }

//            //  Ожидание.
//            await Task.Delay(50, token).ConfigureAwait(false);
//        }
//        */
//    }
//}
