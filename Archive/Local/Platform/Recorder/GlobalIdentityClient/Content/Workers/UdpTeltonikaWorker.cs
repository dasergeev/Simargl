using Apeiron.Services.GlobalIdentity.Tunings;
using System.Net.Sockets;
using ExternalPackage;
namespace Apeiron.Services.GlobalIdentity.Workers;

/// <summary>
/// Представляет класс службы получения данных Teltonika от службы TeltonikaService
/// </summary>
internal class UdpTeltonikaWorker :
     Worker<UdpTeltonikaWorker, ClientTuning>
{
    /// <summary>
    /// Возвращает последний полученый пакет.
    /// </summary>
    public static GeolocationPackage? Package { get; private set; }

    /// <summary>
    /// Возвращает флаг достоверности данных.
    /// </summary>
    public static bool IsValide { get; private set; } = false;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="context">
    /// Контекст фонового процесса службы глобальной идентификации.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="context"/> передана пустая ссылка.
    /// </exception>
    public UdpTeltonikaWorker(WorkerContext<UdpTeltonikaWorker, ClientTuning> context) :
        base(context)
    {

    }

    /// <summary>
    /// Ассинхронно выполняет фоновую работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        try
        {


            //  Создание UDP-клиента.
            using UdpClient udpClient = new(Tuning.GeolocationServicePort);

            //  Инициализация времени получения пакета.
            DateTime lastReceiveTime = DateTime.Now;

            //  Время достоверности данных согласно настройкам
            TimeSpan time = new(0, 0, Tuning.GeolocationPackageLifeTimeSecond);

            //  Основной цикл службы.
            while (!cancellationToken.IsCancellationRequested)
            {
                //   Ожидание
                await Task.Delay(100, cancellationToken).ConfigureAwait(false);

                //  Проверка времени пакета.
                if ((DateTime.Now - lastReceiveTime) > time)
                {
                    //  Сброс флага
                    IsValide = false;
                }

                //  Проверка что есть данные UdpClient
                if (udpClient.Available > 0)
                {
                    //  Получение UDP-датаграммы:
                    UdpReceiveResult receiveResult = await udpClient.ReceiveAsync(cancellationToken).ConfigureAwait(false);

                    //  Получение пакета данных TeltonikaService
                    if (GeolocationPackage.TryParse(receiveResult.Buffer, out GeolocationPackage package) == false)
                    {
                        //  Не получен коректный пакет.
                        continue;
                    }

                    //  Установка времени получения.
                    lastReceiveTime = DateTime.Now;

                    //  Установка пакета.
                    Package = package;

                    //  Установка флага достоверности данных.
                    IsValide = package.IsNewAndValideGps;
                }
            }
        }
        finally
        {
            //  Ожидание в случае исключения.
            await Task.Delay(Tuning.ExceptionsTimeout, cancellationToken).ConfigureAwait(false);
        }
    }
}
 