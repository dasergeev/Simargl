//using Pulse.Sensors;
//using System.Collections.Concurrent;

//namespace Pulse.Services.Main;

///// <summary>
///// Представляет службу приёма данных от датчиков.
///// </summary>
///// <param name="kernel">
///// Ядро приложения.
///// </param>
///// <param name="receivers">
///// Очередь приёмников данных от датчиков.
///// </param>
//public sealed class DataReceptionService(Kernel kernel, ConcurrentQueue<DataReceiver> receivers) :
//    Service(kernel, ServiceID.DataReceptionService)
//{
//    /// <summary>
//    /// Асинхронно выполняет основную работу.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая основную работу.
//    /// </returns>
//    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
//    {
//        //  Основной цикл работы.
//        while (!cancellationToken.IsCancellationRequested)
//        {
//            //  Извлечение приёмников из очереди.
//            while (receivers.TryDequeue(out DataReceiver? receiver) &&
//                !cancellationToken.IsCancellationRequested)
//            {
//                //  Проверка приёмника.
//                if (receiver is not null)
//                {
//                    //  Запуск асинхронной задачи.
//                    _ = Task.Run(async delegate
//                    {
//                        //  Выполнение основной задачи приёма данных.
//                        await receiver.InvokeAsync(Journal, cancellationToken).ConfigureAwait(false);
//                    }, CancellationToken.None).ConfigureAwait(false);
//                }
//            }

//            //  Задержка перед следующим проходом.
//            await Task.Delay(BasisConstants.FastServicePeriod, cancellationToken).ConfigureAwait(false);
//        }
//    }
//}
