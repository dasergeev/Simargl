//using Simargl.Platform.Journals;
//using Simargl.Designing;
//using System.Threading.Tasks;
//using System.Threading;
//using System;
//using System.Collections.Generic;
//using static Simargl.Designing.Verify;

//namespace Simargl.Platform.Transmitters;

///// <summary>
///// Представляет класс коллекции передатчиков.
///// </summary>
//public abstract class TransmittersCollection
//{
//    /// <summary>
//    /// Представляет интерфейс логирования.
//    /// </summary>
//    internal Journal _Journal;

//    /// <summary>
//    /// Представляет конфигурацию передатчика.
//    /// </summary>
//    private readonly TransmittersOptions _Options;

//    /// <summary>
//    /// Представляет механизм синхронизации
//    /// </summary>
//    private readonly SemaphoreSlim _Semaphore = new(1, 1);

//    /// <summary>
//    /// Представляет коллекцию передатчиков.
//    /// </summary>
//    private readonly List<ITransmitter> _Transmitters = new();

//    /// <summary>
//    /// Инициализирует экземпляр класса.
//    /// </summary>
//    /// <param name="journal">
//    /// Журнал.
//    /// </param>
//    /// <param name="options">
//    /// Конфигурация.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметр <paramref name="journal"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметр <paramref name="options"/> передана пустая ссылка.
//    /// </exception>
//    protected TransmittersCollection(Journal journal, TransmittersOptions options)
//    {
//        //  Установка интерфейса логирования.
//        _Journal = IsNotNull(journal, nameof(journal));

//        //  Проверка и установка кофигурации
//        _Options = IsNotNull(options,nameof(options));

//        //  Проверка настроек файлового передатчика
//        if (_Options.FileExtension is not null && _Options.RootPath is not null && _Options.FolderName is not null)
//        {
//            //  Добавление файлового передатчика.
//            Add(new FileTransmitter(_Journal,_Options.RootPath, _Options.FolderName, _Options.FileExtension)); 
//        }

//        //  Проверка настроек Tcp передатчика
//        if (_Options.TcpServerPort > 0 && _Options.TcpServerPort < ushort.MaxValue)
//        {
//            //  Добавление Tcp передатчика
//            Add(new TcpServerTransmitter(_Journal,_Options.TcpServerPort));
//        }

//        //  Проверка настроек Udp передатчика
//        if (_Options.UdpEndPoint is not null)
//        {
//            //  Добавление Udp передатчика.
//            Add(new UdpTransmitter(_Journal,_Options.UdpEndPoint));
//        }
//    }

//    /// <summary>
//    /// Представляет функцию добавления передатчика.
//    /// </summary>
//    /// <param name="transmitter"></param>
//    private void Add(ITransmitter transmitter)
//    {
//        //  Ожидание семафора.
//        _Semaphore.Wait();

//        //  Добавление интерфейса
//        _Transmitters.Add(transmitter);

//        //  Освобождение  семафора.
//        _Semaphore.Release();
//    }



//    /// <summary>
//    /// Представляет функцию отправки массива.
//    /// </summary>
//    /// <param name="data">
//    /// Массив
//    /// </param>
//    protected virtual void Send(byte[] data)
//    { 
//        //  Ожидание семафора.
//        _Semaphore.Wait();

//        //  Цикл по всем передатчикам.
//        foreach (ITransmitter transmitter in _Transmitters)
//        {
//            //  Отправка данных
//            transmitter.Send(data);
//        }

//        //  Освобождение  семафора.
//        _Semaphore.Release();

//    }

//    /// <summary>
//    /// Представляет функцию пересылки массива асинхронно.
//    /// </summary>
//    /// <param name="data">
//    /// Массив
//    /// </param>
//    /// <param name="token">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача.
//    /// </returns>
//    protected virtual async Task SendAsync(byte[] data, CancellationToken token)
//    {
//        //  Ожидание семафора.
//        await _Semaphore.WaitAsync(token).ConfigureAwait(false);

//        //  Цикл по всем передатчикам.
//        foreach (ITransmitter transmitter in _Transmitters)
//        {
//            //  Отправка данных
//            await transmitter.SendAsync(data,token).ConfigureAwait(false);
//        }

//        //  Освобождение  семафора.
//        _Semaphore.Release();
//    }
//}
