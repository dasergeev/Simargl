using Simargl.Hardware.Strain.Demo.ReWrite;

namespace Simargl.Hardware.Strain.Demo.Microservices;

/// <summary>
/// Представляет микросервис сканирования.
/// </summary>
public sealed class Scanner :
    Microservice
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="heart">
    /// Сердце приложения.
    /// </param>
    public Scanner(Heart heart) :
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

        //  Добавление задачи сканирования.
        Keeper.Add(async delegate (CancellationToken cancellationToken)
        {
            //  Основной цикл сканирования.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Сканирование.
                await SensorScaner.ScanAsync(cancellationToken).ConfigureAwait(false);

                //  Ожидание перед следующим сканированием.
                await Task.Delay(500, cancellationToken).ConfigureAwait(false);
            }
        });

        //  Добавление задачи сканирования.
        Keeper.Add(async delegate (CancellationToken cancellationToken)
        {
            //  Основной цикл сканирования.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Сканирование.
                await SensorScaner.Scan2Async(cancellationToken).ConfigureAwait(false);

                //  Ожидание перед следующим сканированием.
                await Task.Delay(500, cancellationToken).ConfigureAwait(false);
            }
        });


        //  Добавление задачи разбора результатов сканирования.
        Keeper.Add(async delegate (CancellationToken cancellationToken)
        {
            //  Основной цикл сканирования.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Перебор результатов.
                while (!cancellationToken.IsCancellationRequested 
                    && SensorScaner.Results.TryDequeue(out var result))
                {
                    //  Добавление отклика.
                    await Heart.RootNode.Equipment.AddResponseAsync(
                        result.Serial, result.EndPoint, cancellationToken).ConfigureAwait(false);
                }

                //  Ожидание перед следующим сканированием.
                await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            }
        });
    }
}
