using Apeiron.Recording.Adxl357;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace Apeiron.Platform.Demo.AdxlDemo.Adxl;

/// <summary>
/// Представляет приёмник пакетов датчиков ADXL357.
/// </summary>
public sealed class AdxlReceiver :
    Active
{
    /// <summary>
    /// Постоянная, определяющая задержку перед следующим шагом основного цикла выполнения.
    /// </summary>
    private const int _InvokeDelay = 1000;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public AdxlReceiver(Engine engine) :
        base(engine)
    {

    }

    /// <summary>
    /// Асинхронно выполняет основную задачу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Основная задача.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Выполнение метода базового класса.
        await base.InvokeAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Безопасное выполнение.
            await Invoker.SystemAsync(async (cancellationToken) =>
            {
                //  Средство прослушивания сети.
                TcpListener? listener = null;

                //  Блок с гарантированным завершением.
                try
                {
                    listener = new(IPAddress.Any, 49001);
                    listener.Start();

                    //  Вывод в журнал.
                    await Logger.LogAsync($"Начало прослушивания сети.", cancellationToken).ConfigureAwait(false);

                    //  Цикл прослушивания.
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        //  Ожидание нового подключения.
                        TcpClient tcpClient = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                        //  Работа с новым подключением.
                        _ = Task.Run(async delegate
                        {
                            //  Захват соедиения.
                            using TcpClient client = tcpClient;

                            //  Проверка токена отмены.
                            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                            //  Проверка подключения.
                            if (client.Client.RemoteEndPoint is not IPEndPoint endPoint)
                            {
                                throw new InvalidOperationException();
                            }

                            //  Вывод в журнал.
                            await Logger.LogAsync($"Установлено новое соединение {endPoint}.", cancellationToken).ConfigureAwait(false);

                            //  Поиск датчика.
                            AdxlDevice? device = await Engine.Root.Devices.FindAsync(new(endPoint.Address), cancellationToken).ConfigureAwait(false);

                            //  Проверка датчика.
                            if (device is null)
                            {
                                //  Завершение работы с клиентом.
                                return;
                            }

                            //  Обновление информации о датчике.
                            await device.UpdateAsync(cancellationToken).ConfigureAwait(false);

                            //  Получение потока для чтения.
                            NetworkStream stream = client.GetStream();

                            //  Исключение в событии синхронизатора.
                            Exception? exception = null;

                            //  Очередь пакетов.
                            ConcurrentQueue<Tuple<DateTime, Adxl357DataPackage>> queue = new();

                            //  Определение времени ожидания.
                            double waiting = 5 * 50 / device.Sampling + 1;

                            //  Время получения последнего пакета.
                            DateTime lastLoad = DateTime.Now;

                            //  Обработчик события синхронизатора.
                            void tick(object? sender, SynchronizerEventArgs e)
                            {
                                //  Безопасное выполнение.
                                Invoker.Critical(delegate
                                {
                                    //  Проверка наличия данных.
                                    while (client.Available >= 636)
                                    {
                                        //  Чтение очередного пакета.
                                        Adxl357DataPackage package = Adxl357DataPackage.Load(stream);

                                        //  Корректировка времи получения последнего пакета.
                                        lastLoad = DateTime.Now;

                                        //  Добавление пакета в очередь.
                                        queue.Enqueue(new(e.Time, package));
                                    }
                                }, out exception);
                            }

                            //  Добавление обработчика события в синхронизатор.
                            Engine.Synchronizer.Tick += tick;

                            //  Блок с гарантированным завершением.
                            try
                            {
                                //  Основной цикл работы с соединением.
                                while (!cancellationToken.IsCancellationRequested && exception is null)
                                {
                                    //  Извлечение пакетов из очереди.
                                    while (queue.TryDequeue(out Tuple<DateTime, Adxl357DataPackage>? item))
                                    {
                                        //  Регистрация пакета.
                                        await Engine.PackageManager.RegistrationAsync(
                                            device, item.Item2, item.Item1, cancellationToken).ConfigureAwait(false);
                                    }

                                    //  Проверка времени ожидания.
                                    if ((DateTime.Now - lastLoad).TotalSeconds > waiting)
                                    {
                                        //  Удаление датчика из коллекции.
                                        await Engine.Root.Devices.TryRemoveAsync(device.SerialNumber, cancellationToken).ConfigureAwait(false);

                                        //  Выброс исключения для завершения работы с соединением.
                                        throw Exceptions.OperationInvalid();
                                    }

                                    //  Ожидание перед следующим циклом.
                                    await Task.Delay(10, cancellationToken).ConfigureAwait(false);
                                }
                            }
                            finally
                            {
                                //  Удаление обработчика события из синхронизатора.
                                Engine.Synchronizer.Tick -= tick;
                            }
                        }, cancellationToken);
                    }
                }
                finally
                {
                    listener?.Stop();
                }
            }, cancellationToken).ConfigureAwait(false);

            //  Ожидание перед повтором.
            await Task.Delay(_InvokeDelay, cancellationToken).ConfigureAwait(false);
        }
    }
}
