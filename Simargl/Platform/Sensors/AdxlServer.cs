//using Simargl.Concurrent;
//using System.Net;
//using System.Net.Sockets;
//using System.Threading.Tasks;
//using System.Threading;
//using System;
//using Simargl.Support;
//using System.Collections.Generic;
//using Simargl.Designing;
//using System.Linq;
//using static Simargl.Designing.Verify;

//namespace Simargl.Platform.Sensors
//{
//    /// <summary>
//    /// Представляет класс сервера Tcp со списком разрешенных клиентов.
//    /// </summary>
//    public class AdxlServer : IDisposable
//    {
//        /// <summary>
//        /// Представляет объект синхронизации.
//        /// </summary>
//        private readonly object _SyncRoot = new();

//        /// <summary>
//        /// Представляет флаг освобождения ресурсов.
//        /// </summary>
//        private bool _Disposed;

//        /// <summary>
//        /// Представляет список датчиков обслуживаемых коллекцией.
//        /// </summary>
//        public List<AdxlSensor> ListenSensor { get; } = new();

//        /// <summary>
//        /// Представляет источник токена отмены класса.
//        /// </summary>
//        public CancellationTokenSource? TokenSource { get; private set; }

//        /// <summary>
//        /// Представляет handle задачи прослушивания порта.
//        /// </summary>
//        public Task? ListeningRoutineTask { get; private set; }

//        /// <summary>
//        /// Возвращает порт сервера.
//        /// </summary>
//        public int Port { get; }

//        /// <summary>
//        /// Инициализирует объект класса.
//        /// </summary>
//        /// <param name="port">Порт сервера.</param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="port"/> передано не допустимое значение.
//        /// </exception>
//        public AdxlServer(int port)
//        {
//            //  Проверка порта на маскимальное значение.
//            Verify.IsNotLarger(port, ushort.MaxValue, nameof(port));

//            //  Проверка порта на минимальное значение.
//            Verify.IsNotLess(port, ushort.MinValue, nameof(port));

//            //  Установка значения порта.
//            Port = port;

//        }


//        /// <summary>
//        /// Представляет фунцкию добавления датчика в список.
//        /// </summary>
//        /// <param name="sensor">Датчик для добавления.</param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="sensor"/> передана пустая ссылка.
//        /// </exception>
//        public void Add(AdxlSensor sensor)
//        {
//            //  Проверка ссылки.
//            IsNotNull(sensor, nameof(sensor));

//            lock (_SyncRoot)
//            {
//                //  Добавление в список.
//                ListenSensor.Add(sensor);
//            }
//        }


//        /// <summary>
//        /// Представляет фунцкию удаление датчика из список.
//        /// </summary>
//        /// <param name="sensor">Датчик для удаления.</param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="sensor"/> передана пустая ссылка.
//        /// </exception>
//        public void Remove(AdxlSensor sensor)
//        {
//            //  Проверка ссылки.
//            IsNotNull(sensor, nameof(sensor));

//            lock (_SyncRoot)
//            {
//                //  Добавление в список.
//                ListenSensor.Remove(sensor);
//            }
//        }

//        /// <summary>
//        /// Представляет функцию запуска прослушивания.
//        /// </summary>
//        public void Start(CancellationToken serviceToken)
//        {
//            //  Установка токена отмены.
//            TokenSource = new CancellationTokenSource();

//            //  Связывание токенов.
//            using var tokenSource = CancellationTokenSource.CreateLinkedTokenSource(serviceToken, TokenSource.Token);

//            //  Получение токена отмены.
//            var token = tokenSource.Token;

//            //  Запуск задачи
//            ListeningRoutineTask = Task.Run(() => keepAsync(ListeningRoutineAsync, token), token);

//            static async Task keepAsync(AsyncAction action, CancellationToken cancellationToken)
//            {
//                //  Проверка ссылки на действие.
//                action = IsNotNull(action, nameof(action));

//                //  Проверка токена отмены.
//                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//                //  Цикл для отслеживания задачи.
//                while (!cancellationToken.IsCancellationRequested)
//                {
//                    //  Безопасный вызов действия.
//                    await safeNotCriticalAsync(action, cancellationToken).ConfigureAwait(false);

//                    static async Task safeNotCriticalAsync(AsyncAction action, CancellationToken cancellationToken)
//                    {
//                        //  Проверка ссылки на действие.
//                        action = IsNotNull(action, nameof(action));

//                        //  Проверка токена отмены.
//                        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//                        //  Блок перехвата некритических исключений.
//                        try
//                        {
//                            //  Вызов действия.
//                            await action(cancellationToken).ConfigureAwait(false);
//                        }
//                        catch (Exception ex)
//                        {
//                            //  Проверка критического исключения.
//                            if (ex.IsCritical())
//                            {
//                                //  Повторный выброс исключения.
//                                throw;
//                            }
//                        }
//                    }

//                }
//            }

//        }

//        private async Task ListeningRoutineAsync(CancellationToken linkedToken)
//        {

//            //  Порт для прослушивания.
//            int port = Port;

//            //  Создание средства прослушивания TCP-соединений датчиков Adxl.
//            TcpListener listener = new(IPAddress.Any, port);

//            //  Запуск средства прослушивания TCP-соединений датчиков Adxl.
//            listener.Start();

//            try
//            {
//                //  Основной цикл.
//                while (!linkedToken.IsCancellationRequested)
//                {
//                    //  Проверка подключений.
//                    while (listener.Pending())
//                    {
//                        //  Получение клиента.
//                        TcpClient client = await listener.AcceptTcpClientAsync(linkedToken).ConfigureAwait(false);

//                        //  Проверка соединения.
//                        if (client.Client.RemoteEndPoint is not IPEndPoint point)
//                        {
//                            //  Рассоединение
//                            client.Dispose();

//                            //  Продолжение цикла.
//                            continue;
//                        }

//                        //  Получение массива байт.
//                        IPAddress address = point.Address;

//                        lock (_SyncRoot)
//                        {
//                            //  Получение датчика из списка.
//                            var sensor = ListenSensor.FirstOrDefault(x => x.Modbus.SessionIP.Equals(address));

//                            //  Проверка что дачтик в списке.
//                            if (sensor != default)
//                            {
//                                //  Запуск задачи для работы с датчиком.
//                                _ = Task.Run(async delegate
//                                {
//                                //  Вызов метода для работы с датчиком.
//                                    await sensor.StreamRoutineAsync(client, linkedToken);

//                                }, linkedToken);
//                            }
//                        }
//                    }

//                    //  Ожидaние.
//                    await Task.Delay(1000, linkedToken).ConfigureAwait(false);
//                }
//            }
//            finally
//            {
//                //  Остановка прослушивания соедиений Adxl.
//                listener.Stop();

//                //  Ожидaние.
//                await Task.Delay(1000, linkedToken).ConfigureAwait(false);
//            }
//        }

//        /// <summary>
//        /// Представляет функцию освобождения ресурсов.
//        /// </summary>
//        public void Dispose()
//        {
//            Dispose(disposing: true);
//            GC.SuppressFinalize(this);
//        }

//        /// <summary>
//        /// Представляет функци освобождения ресурсов.
//        /// </summary>
//        /// <param name="disposing">Флаг завершения ресурсов.</param>
//        protected virtual void Dispose(bool disposing)
//        {
//            //  Проверка флага
//            if (_Disposed)
//            {
//                return;
//            }

//            //  Проверка параметра.
//            if (disposing)
//            {
//                //  Отмена токера
//                TokenSource?.Cancel();

//                //  Освобождение токена.
//                TokenSource?.Dispose();
//            }

//            //  Установка флага.
//            _Disposed = true;
//        }
//    }
//}
