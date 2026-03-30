using Apeiron.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Apeiron.Platform.MediatorLibrary;

/// <summary>
/// Представляет Tcp клиент.
/// </summary>
public static class MediatorTcpClient
{
    /// <summary>
    /// Создаёт подключение к серверу и выполняет команду.
    /// </summary>
    /// <param name="iPEndPoint">Представляет сетевую конечную точку в виде IP-адреса и номер порта.</param>
    /// <param name="method">ID метода.</param>
    /// <param name="argumentsWriter">Функция записи аргументов метода.</param>
    /// <param name="resultReader">Функция чтения результата выполнения метода на удаленном сервере.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public static async Task<T?> RemoteMethodCallAsync<T>(IPEndPoint iPEndPoint, 
        MediatorMethodId method, 
        Func<Spreader, CancellationToken, Task> argumentsWriter, 
        Func<Spreader, CancellationToken, Task<T>> resultReader, 
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Создание TCP клиента.
        using TcpClient tcpClient = new();

        // Устанавливает соединение к удалённому TCP серверу.
        await tcpClient.ConnectAsync(iPEndPoint);

        //  Создание распределителя данных потока.
        Spreader spreader = new(tcpClient.GetStream(), Encoding.UTF8);

        // Запись ID команды в сетевой поток.
        await spreader.WriteInt32Async((int)method, cancellationToken).ConfigureAwait(false);

        // Запись аргументов команды в сетевой поток.
        await argumentsWriter(spreader, cancellationToken).ConfigureAwait(false);

        // Сброс данных в поток.
        await tcpClient.GetStream().FlushAsync(cancellationToken).ConfigureAwait(false);


        // Чтение результата выполнения команды с сервера.
        MediatorResult result = (MediatorResult)await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        // Обработка результата.
        switch (result)
        {
            case MediatorResult.Void :
                return default;
            case MediatorResult.Data:
                return await resultReader(spreader, cancellationToken).ConfigureAwait(false);
            default:
                string error = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);
                throw new InvalidOperationException(error);
        }
    }    
}
