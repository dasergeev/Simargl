using Apeiron.IO;
using Apeiron.Platform.Expanse.Coordinators;
using Apeiron.Platform.Expanse.Core;
using Apeiron.Platform.Journals;
using Apeiron.Platform.Performers;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Apeiron.Platform.Expanse;

/// <summary>
/// Представляет исполнителя, выполняющего координацию серверного пространства.
/// </summary>
internal sealed class ExpansePerformer :
    Performer
{
    /// <summary>
    /// Поле для хранения координатора файловых хранилищ.
    /// </summary>
    private readonly FileStoragesCoordinator _FileStoragesCoordinator;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="journal">
    /// Журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="journal"/> передана пустая ссылка.
    /// </exception>
    public ExpansePerformer(Journal journal) :
        base(journal)
    {
        //  Создание координатора файловых хранилищ.
        _FileStoragesCoordinator = new();
    }

    /// <summary>
    /// Асинхронно выполняет работу исполнителя.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу исполнителя.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public override sealed async Task PerformAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Поддержка выполения.
        await KeepAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание средства прослушивания сети.
            TcpListener listener = new(IPAddress.Any, GlobalSettings.ExpansePort);

            //  Блок с гарантированным завершением.
            try
            {
                //  Запуск прослушивания сети.
                listener.Start();

                //  Основной цикл службы.
                while (!cancellationToken.IsCancellationRequested)
                {
                    //  Асинхронный приём запроса ожидающего подключения.
                    TcpClient client = await listener
                        .AcceptTcpClientAsync(cancellationToken)
                        .ConfigureAwait(false);

                    //  Запуск асинхронной работы с поделючением.
                    _ = Task.Run(async () => await InvokeClientAsync(
                        client, cancellationToken).ConfigureAwait(false),
                        cancellationToken);
                }
            }
            finally
            {
                //  Остановка прослушивания сети.
                listener.Stop();
            }
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с клиентом.
    /// </summary>
    /// <param name="tcpClient">
    /// Клиентское подключение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с клиентом.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task InvokeClientAsync(
        [ParameterNoChecks] TcpClient tcpClient, CancellationToken cancellationToken)
    {
        //  Для гарантированного освобождения TCP-клиента.
        using TcpClient client = tcpClient;

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Вывод информации о подключении.
        await Journal.LogInformationAsync(
            $"Новое подключение: {client.Client.RemoteEndPoint}",
            cancellationToken).ConfigureAwait(false);

        //  Получение потока клиента.
        using Stream stream = client.GetStream();

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Основной работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Чтение идентификатора метода.
            ExpanseMethodId method = (ExpanseMethodId)await spreader
                .ReadInt32Async(cancellationToken).ConfigureAwait(false);

            //  Проверка завершения работы.
            if (method == ExpanseMethodId.Shutdown)
            {
                //  Запись формата результата.
                await spreader.WriteInt32Async((int)ExpanseResultFormat.Void, cancellationToken)
                    .ConfigureAwait(false);

                //  Завершение работы.
                break;
            }

            //  Чтение размера аргументов.
            int length = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

            //  Чтение аргументов метода.
            byte[] arguments = await spreader
                .ReadBytesAsync(length, cancellationToken).ConfigureAwait(false);

            //  Результат вызова.
            byte[] result;

            //  Формат результата.
            ExpanseResultFormat format;

            //  Блок перехвата исключений.
            try
            {
                //  Вызов метода.
                result = await InvokeMethodAsync(method, arguments, cancellationToken)
                    .ConfigureAwait(false);

                //  Установка формата результата.
                format = result.Length != 0 ?
                    ExpanseResultFormat.Information :
                    ExpanseResultFormat.Void;
            }
            catch (Exception ex)
            {
                //  Создание потока для записи резальтата.
                using MemoryStream resultStream = new();

                //  Средство для сериализации.
                BinaryFormatter formatter = new();

                //  Сериализация исключения.
#pragma warning disable SYSLIB0011 // Тип или член устарел
                formatter.Serialize(resultStream, ex);
#pragma warning restore SYSLIB0011 // Тип или член устарел

                //  Сброс данных в поток.
                await resultStream.FlushAsync(cancellationToken).ConfigureAwait(false);

                //  Получение результатов.
                result = resultStream.ToArray();

                //  Установка формата результата.
                format = ExpanseResultFormat.Exception;
            }

            //  Запись формата результата.
            await spreader.WriteInt32Async((int)format, cancellationToken).ConfigureAwait(false);

            //  Проверка необходимости записи результата.
            if (format == ExpanseResultFormat.Void)
            {
                //  Переход к следующему методу.
                continue;
            }

            //  Запись размера результата.
            await spreader.WriteInt32Async(result.Length, cancellationToken).ConfigureAwait(false);

            //  Запись результата метода.
            await spreader.WriteBytesAsync(result, cancellationToken).ConfigureAwait(false);

            //  Сброс данных в поток.
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
        }
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
    private async Task<byte[]> InvokeMethodAsync(
        [ParameterNoChecks] ExpanseMethodId method,
        [ParameterNoChecks] byte[] arguments,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание потока для чтения аргументов.
        using MemoryStream argumentsStream = new(arguments);

        //  Создание средства чтения аргументов.
        using BinaryReader reader = new(argumentsStream, Encoding.UTF8, false);

        //  Создание потока для записи результатов.
        using MemoryStream resultStream = new();

        //  Создание средства для записи результатов.
        using BinaryWriter writer = new(resultStream, Encoding.UTF8, false);

        //  Создание контекста выполнения метода.
        MethodContext methodContext = new(reader, writer);

        //  Проверка идентификатора метода.
        switch (method)
        {
            case ExpanseMethodId.GetAllFileStorages:
                await _FileStoragesCoordinator
                    .GetAllFileStoragesAsync(methodContext, cancellationToken)
                    .ConfigureAwait(false);
                break;
            case ExpanseMethodId.GetFileStorageFromId:
                await _FileStoragesCoordinator
                    .GetFileStorageFromIdAsync(methodContext, cancellationToken)
                    .ConfigureAwait(false);
                break;
            case ExpanseMethodId.GetFileStorageFromName:
                await _FileStoragesCoordinator
                    .GetFileStorageFromNameAsync(methodContext, cancellationToken)
                    .ConfigureAwait(false);
                break;
            case ExpanseMethodId.UpdateFileStorage:
                await _FileStoragesCoordinator
                    .UpdateFileStorageAsync(methodContext, cancellationToken)
                    .ConfigureAwait(false);
                break;
            case ExpanseMethodId.RenameFileStorage:
                await _FileStoragesCoordinator
                    .RenameFileStorageAsync(methodContext, cancellationToken)
                    .ConfigureAwait(false);
                break;
            case ExpanseMethodId.CreateFileStorage:
                await _FileStoragesCoordinator
                    .CreateFileStorageAsync(methodContext, cancellationToken)
                    .ConfigureAwait(false);
                break;
            case ExpanseMethodId.RemoveFileStorage:
                await _FileStoragesCoordinator
                    .RemoveFileStorageAsync(methodContext, cancellationToken)
                    .ConfigureAwait(false);
                break;
            default:
                break;
        }

        //  Сброс данных в поток.
        writer.Flush();
        await resultStream.FlushAsync(cancellationToken).ConfigureAwait(false);

        //  Возврат результатов.
        return resultStream.ToArray();
    }
}
