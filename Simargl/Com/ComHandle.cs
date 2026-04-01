using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Simargl.Com;

/// <summary>
/// Представляет дескриптор COM-объекта.
/// </summary>
public sealed class ComHandle :
    IDisposable
{
    /// <summary>
    /// Поле для хранения карты дескрипторов COM-объектов.
    /// </summary>
    private static readonly ConcurrentDictionary<nint, ComHandle> _Map = new();

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="key">
    /// Ключ COM-объекта.
    /// </param>
    /// <param name="comObject">
    /// COM-объект.
    /// </param>
    private ComHandle(nint key, object comObject)
    {
        //  Установка ключа COM-объекта.
        Key = key;

        //  Установка COM-объекта.
        ComObject = comObject;
    }

    /// <summary>
    /// Возвращает ключ COM-объекта.
    /// </summary>
    public nint Key { get; }

    /// <summary>
    /// Возвращает COM-объект.
    /// </summary>
    public dynamic ComObject { get; }

    /// <summary>
    /// Возвращает дескриптор COM-объекта.
    /// </summary>
    /// <param name="comObject">
    /// COM-объект.
    /// </param>
    /// <returns>
    /// Дескриптор COM-объекта.
    /// </returns>
    [SupportedOSPlatform("windows")]
    public static ComHandle GetHandle(object comObject)
    {
        //  Получение ключа объекта.
        nint key = Marshal.GetIUnknownForObject(comObject);

        //  Блок с гарантированным завершением.
        try
        {
            //  Получение дескриптора COM-объекта.
            return _Map.GetOrAdd(key,
                delegate (nint key)
                {
                    //  Получение COM-объекта.
                    object comObject = Marshal.GetObjectForIUnknown(key);

                    //  Возврат дескриптора COM-объекта.
                    return new(key, comObject);
                });
        }
        finally
        {
            //  Освобождение ключа.
            Marshal.Release(key);
        }
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    public void Dispose()
    {
        //  Попытка удаления дескриптора из карты.
        if (_Map.TryRemove(Key, out _))
        {
            //  Освобождение объекта.
            Marshal.FinalReleaseComObject(ComObject);
        }
    }
}
