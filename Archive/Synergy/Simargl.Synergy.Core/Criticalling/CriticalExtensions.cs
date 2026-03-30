using System.Net.Security;
using System.Net.Sockets;

namespace Simargl.Synergy.Core.Criticalling;

/// <summary>
/// Предоставляет методы расширения для класса <see cref="Critical"/>.
/// </summary>
internal static class CriticalExtensions
{
    /// <summary>
    /// Асинхронно прикрепляет к критическому объекту разрушаемый объект.
    /// </summary>
    /// <param name="critical">
    /// Критический объект.
    /// </param>
    /// <param name="disposable">
    /// Прикрепляемый объект.
    /// </param>
    /// <returns>
    /// Задача, выполняющая прикрепление.
    /// </returns>
    public static async ValueTask AttachAsync(this Critical critical, IDisposable disposable)
    {
        //  Добавление метода разрушения.
        await critical.AddDestroyerAsync(async delegate
        {
            //  Ожидание завершённой задачи.
            await ValueTask.CompletedTask.ConfigureAwait(false);

            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение объекта.
                disposable.Dispose();
            }
            catch { }
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно прикрепляет к критическому объекту TCP-клиента.
    /// </summary>
    /// <param name="critical">
    /// Критический объект.
    /// </param>
    /// <param name="tcpClient">
    /// Прикрепляемый объект.
    /// </param>
    /// <returns>
    /// Задача, выполняющая прикрепление.
    /// </returns>
    public static async ValueTask AttachAsync(this Critical critical, TcpClient tcpClient)
    {
        //  Добавление метода разрушения.
        await critical.AddDestroyerAsync(async delegate
        {
            //  Ожидание завершённой задачи.
            await ValueTask.CompletedTask.ConfigureAwait(false);

            //  Блок перехвата всех исключений.
            try
            {
                //  Закрытие TCP-соединения.
                tcpClient.Close();
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение TCP-соединения.
                tcpClient.Dispose();
            }
            catch { }
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно прикрепляет к критическому объекту сетевой поток.
    /// </summary>
    /// <param name="critical">
    /// Критический объект.
    /// </param>
    /// <param name="networkStream">
    /// Прикрепляемый объект.
    /// </param>
    /// <returns>
    /// Задача, выполняющая прикрепление.
    /// </returns>
    public static async ValueTask AttachAsync(this Critical critical, NetworkStream networkStream)
    {
        //  Добавление метода разрушения.
        await critical.AddDestroyerAsync(async delegate
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Закрытие потока.
                networkStream.Close();
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение потока.
                await networkStream.DisposeAsync().ConfigureAwait(false);
            }
            catch { }
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно прикрепляет к критическому объекту SSL-поток.
    /// </summary>
    /// <param name="critical">
    /// Критический объект.
    /// </param>
    /// <param name="sslStream">
    /// Прикрепляемый объект.
    /// </param>
    /// <returns>
    /// Задача, выполняющая прикрепление.
    /// </returns>
    public static async ValueTask AttachAsync(this Critical critical, SslStream sslStream)
    {
        //  Добавление метода разрушения.
        await critical.AddDestroyerAsync(async delegate
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Завершение сессии TLS.
                await sslStream.ShutdownAsync().ConfigureAwait(false);
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Закрытие потока.
                sslStream.Close();
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение потока.
                await sslStream.DisposeAsync().ConfigureAwait(false);
            }
            catch { }
        }).ConfigureAwait(false);
    }
}
