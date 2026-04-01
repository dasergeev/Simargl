namespace Simargl.AdxlRecorder.Hardware.Recording;

/// <summary>
/// Метод, распределяющий данные по файлам.
/// </summary>
/// <param name="data">
/// Данные.
/// </param>
/// <returns>
/// Путь к файлу.
/// </returns>
public delegate string DataRecorderDistributor(DataReceiveResult data);
