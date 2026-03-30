using Apeiron.Platform.Journals;
using Apeiron.Platform.Transmitters;
using Apeiron.Support;
using System.Text;

namespace Apeiron.EC25;

public class Worker : BackgroundService
{
    /// <summary>
    /// Представляет интерфейс логирования.
    /// </summary>
    private readonly Journal _Journal;

    /// <summary>
    /// Представляет свойство конфигурации программы.
    /// </summary>
    public static Options Configuration { get; set; } = new();

    /// <summary>
    /// Представляет переадресатор Nmea.
    /// </summary>
    private readonly BinaryTransmitter _NmeaTransmitter;

    /// <summary>
    /// Предствляет логер.
    /// </summary>
    private readonly ILogger<Worker> _Logger;

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="logger">
    /// Логер.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре конфигурации передана пустая ссылка.
    /// </exception>
    public Worker(ILogger<Worker> logger)
    {
        //  Установка логера.
        _Logger = Check.IsNotNull(logger, nameof(logger));

        //  Создание системы логирования.
        _Journal = Journal.FromLogger(_Logger);

        //  Создание передатчика
        _NmeaTransmitter = new BinaryTransmitter(_Journal, Check.IsNotNull(Configuration.NmeaTransmitteOptions,nameof(Configuration.NmeaTransmitteOptions)));
    }

    /// <summary>
    /// Выполняет основную работу службы.
    /// </summary>
    /// <param name="stoppingToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задачу.
    /// </returns>
    /// <exception cref="ArgumentNullException">Не задана полная конфигурация.</exception>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //  Проверка конфигурации
        var configuration = Check.IsNotNull(Configuration, nameof(Configuration));

        //  Проверка пути файла устройства.
        var path = Check.IsNotNull(configuration.NmeaPipeFile, nameof(configuration.NmeaPipeFile)); 

        //  Цикл выполнения службы
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                //  Открытие файла
                using FileStream stream = new(path, FileMode.Open,FileAccess.Read);

                //  Создание читателя
                using StreamReader reader = new(stream, Encoding.ASCII, true);

                //  Цикл чтения
                while (!stoppingToken.IsCancellationRequested)
                {
                    //  Преобразование кодировки
                    string? result = await reader.ReadLineAsync().ConfigureAwait(false);

                    //  Проверка результата
                    if (result == null)
                    {
                        //  Продолжение цикла.
                        continue;
                    }

                    //  Пересылка данных
                    await _NmeaTransmitter.SendTransparentAsync($"{result}\r\n", stoppingToken).ConfigureAwait(false);
                   
                    //  Ожидание
                    await Task.Delay(1, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                //  Проверка исключения
                if (ex.IsSystem())
                {
                    //  Выброс исключения
                    throw;
                }

                //  Сохранение ошибки
                await _Journal.LogErrorAsync(ex.Message,stoppingToken);

                //  Ожидание
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
