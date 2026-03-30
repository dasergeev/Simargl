using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Simargl.Hardware.Recorder.Measurement;
using System.Collections.Concurrent;

namespace Simargl.Hardware.Recorder.Core;

/// <summary>
/// Представляет ядро приложения.
/// </summary>
public sealed class Heart
{
    /// <summary>
    /// Возвращает уникальный объект.
    /// </summary>
    public static Heart Unique { get; } = new Heart();

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    private Heart()
    {

    }

    /// <summary>
    /// Возвращает настройки.
    /// </summary>
    public HeartOptions Options { get; } = new();

    /// <summary>
    /// Возвращает измеритель.
    /// </summary>
    public Measurer Measurer { get; } = new();

    /// <summary>
    /// Возвращает очередь плохих ключей потоков тензомодулей.
    /// </summary>
    public ConcurrentQueue<long> BadStrainKeys { get; } = [];

    /// <summary>
    /// Возвращает очередь плохих ключей потоков датчиков ускорений.
    /// </summary>
    public ConcurrentQueue<long> BadAdxlKeys { get; } = [];

    /// <summary>
    /// Присоединяет настройки.
    /// </summary>
    /// <param name="configuration">
    /// Конфигурация.
    /// </param>
    public void Bind(ConfigurationManager configuration)
    {
        //  Чтение настроек.
        configuration.GetSection("Heart").Bind(Options);

        //  Присоединение настроек измерителя.
        Measurer.Bind(configuration);
    }

}
