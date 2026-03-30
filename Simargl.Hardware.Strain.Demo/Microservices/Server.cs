using Simargl.IO;
using Simargl.Hardware.Strain.Demo.Core;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Simargl.Hardware.Strain.Demo.Microservices;

/// <summary>
/// Представляет сервер.
/// </summary>
public sealed class Server :
    Microservice
{
    /// <summary>
    /// Поле для хранения идентификатора следующего сеанса.
    /// </summary>
    private static long _NextSessionKey = 1;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="heart">
    /// Сердце приложения.
    /// </param>
    public Server(Heart heart) :
        base(heart)
    {
        //  Обращение к объекту.
        Lay();

        //  Добавление основной задачи в механизм поддержки.
        Keeper.Add(InvokeAsync);
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Порт для прослушивания датчиков Tenso.
        const int port = 49003;

        //  Создание средства прослушивания TCP-соединений датчиков Tenso.
        TcpListener listener = new(IPAddress.Any, port);

        //  Запуск средства прослушивания TCP-соединений датчиков Tenso.
        listener.Start();

        //  Вывод сообщения в журнал.
        Journal.Add("Сервер запущен.");

        //  Блок с гарантированным завершением.
        try
        {
            //  Основной цикл выполнения.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Получение клиента.
                TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                //  Запуск асинхронной задачи.
                _ = Task.Run(async delegate
                {
                    //  Вызов метода для работы с датчиком.
                    await PrepareAsync(client, cancellationToken).ConfigureAwait(false);
                }, cancellationToken);
            }
        }
        finally
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Остановка прослушивания соедиений Tenso.
                listener.Stop();
            }
            catch { }

            //  Вывод сообщения в журнал.
            Journal.Add("Сервер остановлен.");
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с подключением датчика Tenso.
    /// </summary>
    /// <param name="client">
    /// TCP-клиент датчика.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с подключением датчика.
    /// </returns>
    private async Task PrepareAsync(TcpClient client, CancellationToken cancellationToken)
    {
        //  Блок с гарантированным завершением.
        try
        {
            //  Проверка соединения.
            if (client.Client.RemoteEndPoint is not IPEndPoint endPoint ||
                endPoint.AddressFamily != AddressFamily.InterNetwork)
            {
                //  Выброс исключения.
                throw new IOException("Неизвестное подключение.");
            }

            //  Блок с гарантированным завершением.
            try
            {
                //  Вывод сообщения в журнал.
                Journal.Add($"[{endPoint}] Установлено новое соединение.");

                //  Блок перехвата всех исключений.
                try
                {
                    //  Выполнение работы с клиентом.
                    await WorkAsync(client, endPoint, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    //  Вывод сообщения в журнал.
                    Journal.AddError($"[{endPoint}] {ex}");

                    //  Повторный выброс исключения.
                    throw;
                }
            }
            finally
            {
                //  Вывод сообщения в журнал.
                Journal.Add($"[{endPoint}] Соединение разорвано.");
            }
        }
        finally
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Закрытие клиента.
                client.Close();
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение клиента.
                client.Dispose();
            }
            catch { }
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с клиентом.
    /// </summary>
    /// <param name="client">
    /// Клиент.
    /// </param>
    /// <param name="endPoint">
    /// Удалённая точка подключения.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с клиентом.
    /// </returns>
    private async Task WorkAsync(TcpClient client, IPEndPoint endPoint, CancellationToken cancellationToken)
    {
        //  Получение потока для чтения данных.
        await using NetworkStream stream = client.GetStream();

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Получение идентификатора сеанса.
        long sessionKey = Interlocked.Increment(ref _NextSessionKey);

        //  Основной цикл для работы с датчиком.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Чтение префикса.
            uint prefix = await spreader.ReadUInt32Async(cancellationToken).ConfigureAwait(false);

            //  Проверка префикса
            if (prefix != 0x6E727041)
            {
                //  Выброс исключения
                throw new InvalidDataException($"Получен недопустимый префикс: 0x{prefix:X8}");
            }

            //  Чтение формата.
            uint format = await spreader.ReadUInt32Async(cancellationToken).ConfigureAwait(false);

            //  Проверка формата.
            if (format != 5)
            {
                //  Выброс исключения
                throw new InvalidDataException($"Получен недопустимый формат: {format}");
            }

            //  Время получения пакета.
            DateTime receivingTime = DateTime.Now;

            //  Чтение длинны пакета
            ulong packageSize = await spreader.ReadUInt64Async(cancellationToken).ConfigureAwait(false);

            //  Получение данных
            byte[] array = await spreader.ReadBytesAsync((int)packageSize, cancellationToken).ConfigureAwait(false);

            //  Получение полного размера пакета.
            int fullPackageSize = 16 + array.Length;

            //  Создание потока.
            using MemoryStream memory = new(array);

            //   Создание читателя.
            using var reader = new BinaryReader(memory, Encoding.UTF8, true);

            //  Чтение серийного номера
            uint serialNumber = reader.ReadUInt32();

            //  Чтение поля количества каналов и длины данных
            uint nLength = reader.ReadUInt32();

            //  Получение количества каналов
            byte channelCount = (byte)(nLength & 0xFF);

            //  Получение количества точек данных
            uint pointCount = nLength >> 8 & 0xFFFFFF;

            //  Получение флага времени
            byte syncFlag = reader.ReadByte();

            //  Получение времени в секундах
            ulong timeUnix = reader.ReadUInt64();

            //  Получение младшей части времени
            uint timeNano = reader.ReadUInt32();

            //  Получение температуры
            float cpuTemp = reader.ReadSingle();

            //  Получение температуры
            float sensorTemp = reader.ReadSingle();

            //  Получение напряжение питания
            float cpuPower = reader.ReadSingle();

            //  Инициализация первого измерения массива.
            float[][] data = new float[channelCount][];

            //  Цикл по всем каналам.
            for (int i = 0; i < channelCount; i++)
            {
                //  Инициализация массива каналов.
                data[i] = new float[pointCount];
            }

            //  Цикл по всему файлу.
            for (long j = 0; j < pointCount; j++)
            {
                //  Цикл по всем каналам
                for (int i = 0; i < channelCount; i++)
                {
                    //  Конвертация и сохранение данных из файла.
                    data[i][j] = reader.ReadSingle();
                }
            }

            //  Создание пакета данных.
            DataPackage package = new()
            {
                EndPoint = endPoint,
                SessionKey = sessionKey,
                ReceivingTime = receivingTime,
                FullPackageSize = fullPackageSize,
                SerialNumber = serialNumber,
                SyncFlag = syncFlag,
                TimeUnix = timeUnix,
                TimeNano = timeNano,
                CpuTemp = cpuTemp,
                SensorTemp = sensorTemp,
                CpuPower = cpuPower,
                Data = data,
            };

            //  Запуск асинхронной задачи.
            _ = Task.Run(async delegate
            {
                //  Добавление пакета данных.
                await Heart.RootNode.Equipment.AddDataPackageAsync(package, cancellationToken).ConfigureAwait(false);
            }, cancellationToken);
        }
    }
}
