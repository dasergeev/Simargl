namespace Simargl.Projects.Egypt.EgyptXXX.Converter.Core;

/// <summary>
/// Представляет значение, определяющее формат файла.
/// </summary>
public enum FileFormat
{
    /// <summary>
    /// Данные геолокации.
    /// </summary>
    Nmea,

    /// <summary>
    /// Данные датчиков ускорения.
    /// </summary>
    Adxl,

    /// <summary>
    /// Данные тензометрии.
    /// </summary>
    Strain,

    /// <summary>
    /// Данные RS485.
    /// </summary>
    RS485,
}
