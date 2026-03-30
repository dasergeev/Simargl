using Apeiron.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Apeiron.Platform.Expanse.Core;

/// <summary>
/// Представляет подключение к серверному пространству.
/// </summary>
internal sealed class ExpanseConnection :
    IDisposable
{
    /// <summary>
    /// Поле для хранения подключения.
    /// </summary>
    private readonly TcpClient _Client;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public ExpanseConnection()
    {
        //  Создание подключения.
        _Client = new();
    }

    /// <summary>
    /// Асинхронно выполняет подключение к серверному пространству.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая подключение к серверному пространству.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Подключение клиента.
        await _Client.ConnectAsync(
            GlobalSettings.ExpanseAddress,
            GlobalSettings.ExpansePort,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет метод в серверном пространстве.
    /// </summary>
    /// <param name="method">
    /// Идентификатор метода.
    /// </param>
    /// <param name="argumentsProvider">
    /// Поставщик аргументов метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод в серверном пространстве.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task SingleInvokeAsync(
        [ParameterNoChecks] ExpanseMethodId method,
        [ParameterNoChecks] Action<BinaryWriter> argumentsProvider,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание подключения к серверному пространству.
        using ExpanseConnection connection = new();

        //  Подключение к серверному пространству.
        await connection.ConnectAsync(cancellationToken).ConfigureAwait(false);

        //  Выполнение метода.
        await connection.InvokeAsync(method, argumentsProvider,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет метод в серверном пространстве.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата метода.
    /// </typeparam>
    /// <param name="method">
    /// Идентификатор метода.
    /// </param>
    /// <param name="argumentsProvider">
    /// Поставщик аргументов метода.
    /// </param>
    /// <param name="resultParser">
    /// Анализатор результатов метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод в серверном пространстве.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<TResult> SingleInvokeAsync<TResult>(
        [ParameterNoChecks] ExpanseMethodId method,
        [ParameterNoChecks] Action<BinaryWriter> argumentsProvider,
        [ParameterNoChecks] Func<BinaryReader, TResult> resultParser,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание подключения к серверному пространству.
        using ExpanseConnection connection = new();

        //  Подключение к серверному пространству.
        await connection.ConnectAsync(cancellationToken).ConfigureAwait(false);

        //  Выполнение метода.
        return await connection.InvokeAsync(method, argumentsProvider, resultParser,
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет метод в серверном пространстве.
    /// </summary>
    /// <param name="method">
    /// Идентификатор метода.
    /// </param>
    /// <param name="argumentsProvider">
    /// Поставщик аргументов метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод в серверном пространстве.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task InvokeAsync(
        [ParameterNoChecks] ExpanseMethodId method,
        [ParameterNoChecks] Action<BinaryWriter> argumentsProvider,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание потока для записи аргументов.
        using MemoryStream argumentsStream = new();

        //  Создание средства записи аргументов.
        using BinaryWriter writer = new(argumentsStream, Encoding.UTF8, false);

        //  Запись аргументов.
        argumentsProvider(writer);

        //  Сброс данных в поток.
        writer.Flush();
        await argumentsStream.FlushAsync(cancellationToken).ConfigureAwait(false);

        //  Получение массива аргументов.
        byte[] arguments = argumentsStream.ToArray();

        //  Вызов метода.
        _ = await InvokeAsync(method, arguments, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет метод в серверном пространстве.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата метода.
    /// </typeparam>
    /// <param name="method">
    /// Идентификатор метода.
    /// </param>
    /// <param name="argumentsProvider">
    /// Поставщик аргументов метода.
    /// </param>
    /// <param name="resultParser">
    /// Анализатор результатов метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод в серверном пространстве.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<TResult> InvokeAsync<TResult>(
        [ParameterNoChecks] ExpanseMethodId method,
        [ParameterNoChecks] Action<BinaryWriter> argumentsProvider,
        [ParameterNoChecks] Func<BinaryReader, TResult> resultParser,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание потока для записи аргументов.
        using MemoryStream argumentsStream = new();

        //  Создание средства записи аргументов.
        using BinaryWriter writer = new(argumentsStream, Encoding.UTF8, false);

        //  Запись аргументов.
        argumentsProvider(writer);

        //  Сброс данных в поток.
        writer.Flush();
        await argumentsStream.FlushAsync(cancellationToken).ConfigureAwait(false);

        //  Получение массива аргументов.
        byte[] arguments = argumentsStream.ToArray();

        //  Вызов метода.
        byte[] result = await InvokeAsync(method, arguments, cancellationToken).ConfigureAwait(false);

        //  Создание потока для чтения результата.
        using MemoryStream resultStream = new(result);

        //  Создание средства чтения результатов.
        using BinaryReader reader = new(resultStream, Encoding.UTF8, false);

        //  Возврат результата.
        return resultParser(reader);
    }

    /// <summary>
    /// Асинхронно выполняет метод в серверном пространстве.
    /// </summary>
    /// <param name="method">
    /// Идентификатор метода.
    /// </param>
    /// <param name="arguments">
    /// Аргументы метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод в серверном пространстве.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<byte[]> InvokeAsync(
        [ParameterNoChecks] ExpanseMethodId method,
        [ParameterNoChecks] byte[] arguments,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение потока.
        Stream stream = _Client.GetStream();

        //  Создание средства записи в поток.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Запись идентификатора метода.
        await spreader.WriteInt32Async((int)method, cancellationToken).ConfigureAwait(false);

        //  Запись размера аргументов.
        await spreader.WriteInt32Async(arguments.Length, cancellationToken).ConfigureAwait(false);

        //  Запись аргументов метода.
        await spreader.WriteBytesAsync(arguments, cancellationToken).ConfigureAwait(false);

        //  Сброс данных в поток.
        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение формата результата.
        ExpanseResultFormat format = (ExpanseResultFormat)await spreader
            .ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Проверка пустого результата.
        if (format == ExpanseResultFormat.Void)
        {
            //  Возврат пустого массива.
            return Array.Empty<byte>();
        }

        //  Чтение размера результатов метода.
        int length = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Чтение результатов метода.
        byte[] result = await spreader.ReadBytesAsync(length, cancellationToken).ConfigureAwait(false);

        //  Проверка исключения.
        if (format == ExpanseResultFormat.Exception)
        {
            //  Создание потока для чтения данных об исключении.
            using MemoryStream resultStream = new(result);

            //  Средство для десериализации.
            BinaryFormatter formatter = new();

#pragma warning disable SYSLIB0011 // Тип или член устарел
            //  Десериализация исключения из потока.
            Exception ex = (Exception)formatter.Deserialize(resultStream);
#pragma warning restore SYSLIB0011 // Тип или член устарел

            //  Генерация исключения.
            throw new InvalidOperationException("Не удалось выполнить метод в серверном пространстве.", ex);
        }

        //  Возврат результатов метода.
        return result;
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    void IDisposable.Dispose()
    {
        //  Получение потока.
        Stream stream = _Client.GetStream();

        //  Создание средства записи в поток.
        using BinaryWriter writer = new(stream, Encoding.UTF8, true);

        //  Запись идентификатора метода.
        writer.Write((int)ExpanseMethodId.Shutdown);

        //  Разрушение подключения.
        ((IDisposable)_Client).Dispose();
    }
}
