using Microsoft.Extensions.Logging;
using Simargl.Synergy.Core;
using System.Net.Security;

namespace Simargl.Synergy.Hub;

partial class Worker
{
    /// <summary>
    /// Асинхронно выполняет работу с SSL-потоком.
    /// </summary>
    /// <param name="stream">
    /// SSL-поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с SSL-потоком.
    /// </returns>
    private async Task SslStreamAsync(SslStream stream, CancellationToken cancellationToken)
    {
        try
        {
            await using Dispenser dispenser = await Dispenser.CreateAsync(stream, cancellationToken).ConfigureAwait(false);

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

                await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            }


            //using Bond bond = await Bond.CreateAsync(stream, TimeSpan.FromSeconds(10), cancellationToken).ConfigureAwait(false);

            //bond.Received += delegate (object sender, BondEventArgs e)
            //{
            //    _Logger.LogInformation("Получена порция: {format}", e.Portion.Format);
            //};

            //ConfirmPortion confirm = new();
            //while (!cancellationToken.IsCancellationRequested)
            //{
            //    await bond.SendAsync(confirm, cancellationToken).ConfigureAwait(false);
            //    _Logger.LogInformation("Отправлена порция.");
            //    await Task.Delay(1000, cancellationToken);
            //}

            //using StreamSender sender = await StreamSender.CreateAsync(
            //    stream, TimeSpan.FromMilliseconds(100), cancellationToken).ConfigureAwait(false);

            //while (!cancellationToken.IsCancellationRequested)
            //{
            //    using (Block block = new(100))
            //    {
            //        await sender.SendAsync(block, cancellationToken).ConfigureAwait(false);
            //    }
            //    _Logger.LogInformation("Отправлен блок.");
            //    //await connection.Stream.FlushAsync(cancellationToken);
            //    await Task.Delay(1000, cancellationToken);
            //}

            //using StreamReceiver receiver = await StreamReceiver.CreateAsync(stream, cancellationToken);

            //while (!cancellationToken.IsCancellationRequested)
            //{
            //    //  Извлечение блоков из очереди.
            //    while (receiver.TryDequeue(out Block? block) && !cancellationToken.IsCancellationRequested)
            //    {
            //        if (block is not null)
            //        {
            //            using (block)
            //            {
            //                _Logger.LogInformation("Получен блок: {size} байт.", block.Size);
            //            }
            //        }
            //    }

            //    await Task.Delay(100, cancellationToken).ConfigureAwait(false);
            //}



            ////  Создание буфера для чтения данных.
            //byte[] buffer = new byte[16];

            ////  Чтение формата.
            //await stream.ReadExactlyAsync(buffer, 0, 1, cancellationToken);



            //_ = this;
            //_ = stream;
            await Task.Delay(-1, cancellationToken);
        }
        catch (Exception ex)
        {
            _Logger.LogError("{ex}", ex);

            throw;
        }
        
    }
}
