using System.Collections;
using System.Collections.Generic;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет коллекцию сигналов.
/// </summary>
public sealed class AccelEth3TSignalCollection() :
    IEnumerable<AccelEth3TSignal>
{
    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    private readonly AccelEth3TSignal[] _Items =
        [
            new AccelEth3TSignal("Ox", Settings.SignalSampling, Settings.SignalLength),
            new AccelEth3TSignal("Oy", Settings.SignalSampling, Settings.SignalLength),
            new AccelEth3TSignal("Oz", Settings.SignalSampling, Settings.SignalLength),
            new AccelEth3TSignal("Oyz", Settings.SignalSampling, Settings.SignalLength),
            new AccelEth3TSignal("Oxz", Settings.SignalSampling, Settings.SignalLength),
            new AccelEth3TSignal("Oxy", Settings.SignalSampling, Settings.SignalLength),
            new AccelEth3TSignal("3D", Settings.SignalSampling, Settings.SignalLength),
        ];

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Length;

    /// <summary>
    /// Возвращает элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс устройства.
    /// </param>
    /// <returns>
    /// Элемент с указанным индексом.
    /// </returns>
    public AccelEth3TSignal this[int index] => _Items[index];

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<AccelEth3TSignal> GetEnumerator()
    {
        return ((IEnumerable<AccelEth3TSignal>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _Items.GetEnumerator();
    }
}
