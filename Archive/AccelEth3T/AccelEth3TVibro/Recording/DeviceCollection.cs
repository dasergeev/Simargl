using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет коллекцию устройств.
/// </summary>
/// <param name="core">
/// Ядро.
/// </param>
public sealed class DeviceCollection(Core core) :
    Worker(core),
    IEnumerable<Device>
{
    /// <summary>
    /// Поле для хранения массива элементов.
    /// </summary>
    private readonly Device[] _Items =
        [
            new Device(core, 101, "Vibro"),
            new Device(core, 102, "Pure"),
            new Device(core, 103, "Cover"),
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
    public Device this[int index] => _Items[index];

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Device> GetEnumerator()
    {
        return ((IEnumerable<Device>)_Items).GetEnumerator();
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
