//using Simargl.Platform.Journals;
//using Simargl.Concurrent;
//using System.Net;
//using System.Net.Sockets;
//using Simargl.Designing;
//using System.Threading;
//using System.Collections.Generic;
//using System;
//using System.Threading.Tasks;
//using Simargl.Support;
//using static Simargl.Designing.Verify;

//namespace Simargl.Platform.Transmitters;

///// <summary>
///// Представляет класс сервера ретрансляции данных подключаемым клиентам к серверу.
///// </summary>
//internal class TcpServerTransmitter : ITransmitter
//{
//    /// <summary>
//    /// Представляет интерфейс логирования.
//    /// </summary>
//    private readonly Journal _Journal;

//    /// <summary>
//    /// Представляет механизм синхронизации
//    /// </summary>
//    private readonly SemaphoreSlim _Semaphore = new(1, 1);

//    /// <summary>
//    /// Представляет класс 
//    /// </summary>
//    private readonly int _Port;

//    /// <summary>
//    /// Представляет список клиентов.
//    /// </summary>
//    private readonly List<TcpClientTransmitter> Clients = new();

//    /// <summary>
//    /// Инициализирует экземпляр класса
//    /// </summary>
//    /// <param name="journal">
//    /// Журнал.
//    /// </param>
//    /// <param name="port">
//    /// Порт сервера.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметр <paramref name="journal"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметр <paramref name="port"/> передано не допустимое значение.
//    /// </exception>
//    internal TcpServerTransmitter(Journal journal,int port)
//    {
//        //  Установка интерфейса логирования.
//        _Journal = IsNotNull(journal, nameof(journal));

//        //  Проверка порта
//        Verify.IsNotLess(port, 0, nameof(port));

//        //  Проверка порта
//        Verify.IsNotLarger(port, ushort.MaxValue, nameof(port));

//        //  Установка порта.
//        _Port = port;

//        //  Запуск работы сервера.
//        _ = Task.Run(async () => await keepAsync(InvokeAsync, default).ConfigureAwait(false));

//        static async Task keepAsync(AsyncAction action, CancellationToken cancellationToken)
//        {
//            //  Проверка ссылки на действие.
//            action = IsNotNull(action, nameof(action));

//            //  Проверка токена отмены.
//            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//            //  Цикл для отслеживания задачи.
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                //  Безопасный вызов действия.
//                await safeNotCriticalAsync(action, cancellationToken).ConfigureAwait(false);

//                static async Task safeNotCriticalAsync(AsyncAction action, CancellationToken cancellationToken)
//                {
//                    //  Проверка ссылки на действие.
//                    action = IsNotNull(action, nameof(action));

//                    //  Проверка токена отмены.
//                    await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//                    //  Блок перехвата некритических исключений.
//                    try
//                    {
//                        //  Вызов действия.
//                        await action(cancellationToken).ConfigureAwait(false);
//                    }
//                    catch (Exception ex)
//                    {
//                        //  Проверка критического исключения.
//                        if (ex.IsCritical())
//                        {
//                            //  Повторный выброс исключения.
//                            throw;
//                        }
//                    }
//                }

//            }
//        }

//    }

//    /// <summary>
//    /// Асинхронно выполняет основную работу службы.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая основную работу службы.
//    /// </returns>
//    private async Task InvokeAsync(CancellationToken cancellationToken)
//    {
//        //  Создание средства прослушивания TCP-соединений датчиков MTP.
//        TcpListener listener = new(IPAddress.Any, _Port);

//        //  Запуск средства прослушивания TCP-соединений датчиков MTP.
//        listener.Start();

//        //  Корректировка времени получения пакета.
//        DateTime time = DateTime.Now;

//        try
//        {
//            //  Основной цикл.
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                //  Проверка подключений.
//                while (listener.Pending())
//                {
//                    //  Получение клиента.
//                    TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

//                    //  Ожидание семафора.
//                    await _Semaphore.WaitAsync(cancellationToken);

//                    //  Создание объекта клиента.
//                    TcpClientTransmitter endClient = new(client);

//                    //  Подписывание на событие.
//                    endClient.Disconnected += DiconectedHook;

//                    //  Добавление в список.
//                    Clients.Add(endClient);

//                    //  Освобождение семафора.
//                    _Semaphore.Release();
//                }

//                //  Ожидaние.
//                await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
//            }
//        }
//        catch (Exception ex)
//        {
//            //  Проверка исключения
//            if (ex.IsSystem())
//            {
//                //  Перенаправление исключения
//                throw;
//            }
//            //  Ожидaние.
//            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

//            //  Логирование исключения.
//            await _Journal.LogErrorAsync($"TcpTransmitter:{ex.Message}", cancellationToken).ConfigureAwait(false);
//        }
//        finally
//        {
//            //  Остановка прослушивания соедиений MTP.
//            listener.Stop();
//        }
//    }

//    private void DiconectedHook(object? sender, EventArgs e)
//    {
//        //  Проверка отправителя
//        if (sender is not TcpClientTransmitter client)
//        {
//            //  Возврат из функции.
//            return;
//        }

//        //  Ожидание семафора.
//        _Semaphore.Wait();

//        //  Цикл по всем клиентам.
//        foreach (TcpClientTransmitter endClient in Clients)
//        {
//            //  Проверка отправителя
//            if (endClient == client)
//            {

//                // Удаление из списка
//                Clients.Remove(endClient);

//                //  Возврат из цикла
//                break;
//            }
//        }

//        //  Освобождение семафора.
//        _Semaphore.Release();

//        //  Освобождение ресурсов.
//        client.Dispose();
//    }


//    /// <summary>
//    /// Представляет функцию пересылки сообщения.
//    /// </summary>
//    /// <param name="data">Сообщение</param>
//    public void Send(byte[] data)
//    {
//        //  Проверка ссылки на данные
//        if (data is not null)
//        {
//            //  Ожидание семафора.
//            _Semaphore.Wait();

//            //  Цикл по всем клиентам.
//            foreach (TcpClientTransmitter endClient in Clients)
//            {
//                //  Отправка данных
//                endClient.Send(data);
//            }

//            //  Освобождение семафора.
//            _Semaphore.Release();
//        }
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
//    public async Task SendAsync(byte[] data, CancellationToken token)
//    {
//        //  Проверка ссылки на данные
//        if (data is not null)
//        {
//            //  Ожидание семафора.
//            await _Semaphore.WaitAsync(token).ConfigureAwait(false);

//            //  Цикл по всем клиентам.
//            foreach (TcpClientTransmitter endClient in Clients)
//            {
//                //  Отправка данных
//                await endClient.SendAsync(data, token).ConfigureAwait(false);
//            }

//            //  Освобождение семафора.
//            _Semaphore.Release();
//        }
//    }
//}
