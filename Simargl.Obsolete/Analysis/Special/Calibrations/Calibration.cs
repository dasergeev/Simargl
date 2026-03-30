using System;
using System.Collections.Generic;

namespace Simargl.Analysis.Calibrations;

/// <summary>
/// Представляет калибровку.
/// </summary>
public sealed class Calibration
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="forceInfos">
    /// Коллекция информации о силах.
    /// </param>
    /// <param name="signalInfos">
    /// Коллекция информации об исходных сигналах.
    /// </param>
    /// <param name="calibrationFrameInfos">
    /// Коллекция информации о калибровочных кадрах.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="forceInfos"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="forceInfos"/> передана коллекция, содержащая пустую ссылку.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="forceInfos"/> передана коллекция,
    /// содержащая несколько элементов с одинаковым именем.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="signalInfos"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="signalInfos"/> передана коллекция, содержащая пустую ссылку.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="signalInfos"/> передана коллекция,
    /// содержащая несколько элементов с одинаковым именем.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="calibrationFrameInfos"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="calibrationFrameInfos"/> передана коллекция, содержащая пустую ссылку.
    /// </exception>
    public Calibration(
        IEnumerable<ForceInfo> forceInfos,
        IEnumerable<SignalInfo> signalInfos,
        IEnumerable<CalibrationFrameInfo> calibrationFrameInfos)
    {
        //  Создание коллекции информации о силах.
        ForceInfos = new(forceInfos);

        //  Создание коллекции информации об исходных сигналах.
        SignalInfos = new(signalInfos);

        //  Создание коллекции информации о калибровочных кадрах.
        CalibrationFrameInfos = new(calibrationFrameInfos);
    }

    /// <summary>
    /// Возвращает коллекцию информации о силах.
    /// </summary>
    public ForceInfoCollection ForceInfos { get; }

    /// <summary>
    /// Возвращает коллекцию информации об исходных сигналах.
    /// </summary>
    public SignalInfoCollection SignalInfos { get; }

    /// <summary>
    /// Возвращает коллекцию информации о калибровочных кадрах.
    /// </summary>
    public CalibrationFrameInfoCollection CalibrationFrameInfos { get; }
}
