using System;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет значение, определяющее коды ошибок.
/// </summary>
[Flags]
public enum AdxlErrorCodes
{
    /// <summary>
    /// Ошибки отсутствуют.
    /// </summary>
    NoError = 0,

    /// <summary>
    /// Ошибка запуска системы тактирования.
    /// </summary>
    ClockingError = 0b1,

    /// <summary>
    /// Чип Ethernet не найден.
    /// </summary>
    EthernetError = 0b10,

    /// <summary>
    /// Чип ADXL-357 не найден.
    /// </summary>
    Adxl357Error = 0b100,

    /// <summary>
    /// Температура системы была выше критической.
    /// </summary>
    MaxTemperatureError = 0b1000,

    /// <summary>
    /// Температура системы была ниже критической.
    /// </summary>
    MinTemperatureError = 0b10000,

    /// <summary>
    /// Напряжение питания системы было выше критической отметки.
    /// </summary>
    MaxVoltage = 0b100000,

    /// <summary>
    /// Напряжение питания системы было ниже критической отметки.
    /// </summary>
    MinVoltage = 0b1000000,

    /// <summary>
    /// Ошибка самотестирования ADXL-357.
    /// </summary>
    DiagnosticError = 0b10000000,

    /// <summary>
    /// Ошибка чтения чипа идентификации.
    /// </summary>
    IdentificationError = 0b100000000,
}
