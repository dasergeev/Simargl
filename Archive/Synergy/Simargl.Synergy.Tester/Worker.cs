using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simargl.Synergy.Core;

namespace Simargl.Synergy.Tester;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public class Worker(ILogger<Worker> logger) :
    BackgroundService
{
    /// <summary>
    /// Поле для хранения средства ведения журнала.
    /// </summary>
    private readonly ILogger<Worker> _Logger = logger;

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл поддержки работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение основной работы.
                await InvokeAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (cancellationToken.IsCancellationRequested)
                {
                    //  Завершение работы.
                    return;
                }

                //  Вывод информации в журнал.
                _Logger.LogError("Произошло исключение: {ex}", ex);
            }

            //  Ожидание перед следующей попыткой.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
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
        //  Ожидание для инициализации консоли.
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        _Logger.LogInformation("Запуск тестирования.");




        await using Connection connection = await Connection.CreateAsync(
            "mpt-it.net", 48659, cancellationToken).ConfigureAwait(false);

        _Logger.LogInformation("Подключено к серверу с SSL.");

        await using Dispenser dispenser = await Dispenser.CreateAsync(connection.SslStream, cancellationToken).ConfigureAwait(false);

        _ = Task.Run(async delegate
        {
            while (!dispenser.CancellationToken.IsCancellationRequested)
            {
                try
                {
                    using Block block = await Block.LoadAsync(dispenser, cancellationToken).ConfigureAwait(false);
                    _Logger.LogInformation("Получен блок данных: {size} байт.", block.Size);
                }
                catch (Exception ex)
                {
                    _Logger.LogError("{ex}", ex);
                }
            }
        }, cancellationToken);

        while (!cancellationToken.IsCancellationRequested)
        {
            using Block block = new(100);
            await block.SaveAsync(dispenser, cancellationToken).ConfigureAwait(false);

            await Task.Delay(900, cancellationToken).ConfigureAwait(false);
        }


        //using Bond bond = await Bond.CreateAsync(
        //    connection.Stream, TimeSpan.FromSeconds(10), cancellationToken).ConfigureAwait(false);

        //bond.Received += delegate (object sender, BondEventArgs e)
        //{
        //    try
        //    {
        //        string message = $"Получена порция: {e.Portion.Format}";
        //        Console.WriteLine(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //    //_Logger.LogInformation("Получена порция: {format}", e.Portion.Format);
        //};

        //ConfirmPortion confirm = new();
        //while (!cancellationToken.IsCancellationRequested)
        //{
        //    await bond.SendAsync(confirm, cancellationToken).ConfigureAwait(false);
        //    _Logger.LogInformation("Отправлена порция.");
        //    await Task.Delay(1000, cancellationToken);
        //}

        await Task.Delay(-1, cancellationToken).ConfigureAwait(false);

            //using StreamSender sender = await StreamSender.CreateAsync(
            //    connection.Stream, TimeSpan.FromMilliseconds(100), cancellationToken).ConfigureAwait(false);

            //while (!cancellationToken.IsCancellationRequested)
            //{
            //    using (Block block = new(100))
            //    {
            //        await sender.SendAsync(block, cancellationToken).ConfigureAwait(false);
            //    }
            //    _Logger.LogInformation("Отправлен блок.");
            //    await Task.Delay(1000, cancellationToken);
            //}



        await Task.Delay(-1, cancellationToken).ConfigureAwait(false);
    }
}
