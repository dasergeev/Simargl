//using Simargl.Platform.Journals;
//using System;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Simargl.Platform.Transmitters;

///// <summary>
///// Представляет класс передачи данных в бинарном виде.
///// </summary>
//public sealed class BinaryTransmitter : TransmittersCollection
//{

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
//    public BinaryTransmitter(Journal journal, TransmittersOptions options)
//       : base(journal,options)
//    {
//    }


//    /// <summary>
//    /// Представляет функцию отправки массива.
//    /// </summary>
//    /// <param name="data">
//    /// Массив
//    /// </param>
//    public void SendTransparent(byte[] data)
//    {
//        Send(data);
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
//    public async Task SendTransparentAsync(byte[] data, CancellationToken token)
//    {
//        await SendAsync(data,token);
//    }


//    /// <summary>
//    /// Представляет функцию отправки массива.
//    /// </summary>
//    /// <param name="message">
//    /// Сообщение
//    /// </param>
//    public void SendTransparent(string message)
//    {
//        var data = Encoding.UTF8.GetBytes(message);

//        Send(data);
//    }

//    /// <summary>
//    /// Представляет функцию пересылки массива асинхронно.
//    /// </summary>
//    /// <param name="message">
//    /// Сообщение
//    /// </param>
//    /// <param name="token">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача.
//    /// </returns>
//    public async Task SendTransparentAsync(string message,  CancellationToken token)
//    {
//        var data = Encoding.UTF8.GetBytes(message);

//        await SendAsync(data, token);
//    }


//    /// <summary>
//    /// Представляет функцию отправки массива c заголовком.
//    /// </summary>
//    /// <param name="data">
//    /// Массив
//    /// </param>
//    /// <param name="time">
//    /// Время установленное в заголовке.
//    /// </param>
//    /// <param name="identifier">
//    /// Идентификато установленный в заголовке
//    /// </param>
//    public void SendHeadered(byte[] data, DateTime time, long identifier)
//    {
//        TransmitterBinPackage package = new(identifier, time, data);

//        Send(package.ToArray());
//    }

//    /// <summary>
//    /// Представляет функцию пересылки массива асинхронно.
//    /// </summary>
//    /// <param name="data">
//    /// Массив
//    /// </param>
//    /// <param name="time">
//    /// Время установленное в заголовке.
//    /// </param>
//    /// <param name="identifier">
//    /// Идентификато установленный в заголовке
//    /// </param>
//    /// <param name="token">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача.
//    /// </returns>
//    public async Task SendHeaderedAsync(long identifier, DateTime time, byte[] data, CancellationToken token)
//    {
//        TransmitterBinPackage package = new(identifier, time, data);

//        await SendAsync(package.ToArray(), token);
//    }

//    /// <summary>
//    /// Представляет функцию отправки массива c заголовком.
//    /// </summary>
//    /// <param name="identifier">
//    /// Идентификато установленный в заголовке
//    /// </param>
//    /// <param name="time">
//    /// Время установленное в заголовке.
//    /// </param>
//    /// <param name="message">
//    /// Сообщение.
//    /// </param>
//    public void SendHeadered(string identifier, DateTime time, string message)
//    {
//        TransmitterTextPackage package = new(identifier, time, message);

//        Send(package.ToArray());
//    }

//    /// <summary>
//    /// Представляет функцию пересылки массива асинхронно.
//    /// </summary>
//    /// <param name="identifier">
//    /// Идентификато установленный в заголовке
//    /// </param>
//    /// <param name="time">
//    /// Время установленное в заголовке.
//    /// </param>
//    /// <param name="message">
//    /// Сообщение.
//    /// </param>
//    /// <param name="token">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача.
//    /// </returns>
//    public async Task SendHeaderedAsync(string identifier, DateTime time, string message, CancellationToken token)
//    {
//        TransmitterTextPackage package = new(identifier, time, message);

//        await SendAsync(package.ToArray(), token).ConfigureAwait(false);
//    }
//}
