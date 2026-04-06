using System.Net;

namespace Simargl.Hardware.Strain.Demo.Main.Properties;

/// <summary>
/// Представляет свойство датчика.
/// </summary>
public abstract class SensorProperty
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="context">
    /// Контекст свойства датчика.
    /// </param>
    /// <param name="valueType">
    /// Тип значения.
    /// </param>
    public SensorProperty(SensorPropertyContext context, Type valueType)
    {
        //  Установка контекста свойства датчика.
        Context = context;

        //  Установка типа значения.
        ValueType = valueType;
    }

    /// <summary>
    /// Возвращает контекст свойства датчика.
    /// </summary>
    protected SensorPropertyContext Context { get; }

    /// <summary>
    /// Возвращает тип значения.
    /// </summary>
    public Type ValueType { get; }

    /// <summary>
    /// Возвращает значение, определяющее формат свойства датчика.
    /// </summary>
    public SensorPropertyFormat Format => Context.Format;

    /// <summary>
    /// Возвращает имя свойства.
    /// </summary>
    public string Name => Context.Name;

    /// <summary>
    /// Возвращает описание свойства.
    /// </summary>
    public string Description => Context.Description;

    /// <summary>
    /// Асинхронно выполняет инициализацию.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая инициализацию.
    /// </returns>
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение значения.
        _ = await ReadObjectAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет чтение значения.
    /// </summary>
    /// <returns>
    /// Задача, выполняющая чтение значения.
    /// </returns>
    public async Task<object> ReadObjectAsync(CancellationToken cancellationToken)
    {
        //  Разбор типа значения.
        if (ValueType == typeof(string))
        {
            //  Чтение регистров.
            return await Context.Connection.ReadStringAsync(
                Context.Start, Context.Count, Encoding.ASCII, cancellationToken).ConfigureAwait(false);
        }
        if (ValueType == typeof(ushort))
        {
            //  Чтение регистров.
            return await Context.Connection.ReadUInt16Async(
                Context.Start, cancellationToken).ConfigureAwait(false);
        }
        if (ValueType == typeof(IPAddress))
        {
            //  Чтение регистров.
            return (IPAddress)await Context.Connection.ReadIPv4AddressAsync(
                Context.Start, cancellationToken).ConfigureAwait(false);
        }
        if (ValueType == typeof(float))
        {
            //  Чтение регистров.
            return await Context.Connection.ReadFloat32Async(
                Context.Start, cancellationToken).ConfigureAwait(false);
        }

        //  Выбро исключения.
        throw new OperationCanceledException($"Тип значения {ValueType.Name} для свойства датчика не поддерживается.");
    }

    /// <summary>
    /// Асинхронно записывает значение.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо записать.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись значения.
    /// </returns>
    public async Task WriteObjectAsync(object? value, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на значение.
        value = IsNotNull(value);

        //  Проверка значения.
        if (value.GetType() != ValueType)
        {
            //  Выбро исключения.
            throw new OperationCanceledException($"Тип переданого значения {value.GetType()} не совпадает с типом значения свойства {ValueType.Name}.");
        }

        //  Разбор типа значения.
        //if (ValueType == typeof(string))
        //{
        //    //  Запись регистров.
        //    await Context.Connection.ReadStringAsync(
        //        Context.Start, Context.Count, Encoding.ASCII, cancellationToken).ConfigureAwait(false);
        //}
        if (ValueType == typeof(ushort))
        {
            //  Запись регистров.
            await Context.Connection.WriteUInt16Async(
                Context.Start, (ushort)value, cancellationToken).ConfigureAwait(false);

            //  Завершение записи.
            return;
        }
        if (ValueType == typeof(IPAddress))
        {
            //  Запись регистров.
            await Context.Connection.WriteIPv4AddressAsync(
                Context.Start, new((IPAddress)value), cancellationToken).ConfigureAwait(false);

            //  Завершение записи.
            return;
        }
        if (ValueType == typeof(float))
        {
            //  Запись регистров.
            await Context.Connection.WriteFloat32Async(
                Context.Start, (float)value, cancellationToken).ConfigureAwait(false);

            //  Завершение записи.
            return;
        }

        //  Выбро исключения.
        throw new OperationCanceledException($"Тип значения {ValueType.Name} для свойства датчика не поддерживается.");
    }
}

/// <summary>
/// Представляет свойство датчика.
/// </summary>
/// <typeparam name="T">
/// Тип значения.
/// </typeparam>
public sealed class SensorProperty<T> :
    SensorProperty
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="context">
    /// Контекст свойства датчика.
    /// </param>
    public SensorProperty(SensorPropertyContext context) :
        base(context, typeof(T))
    {

    }

    /// <summary>
    /// Асинхронно выполняет чтение значения.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение значения.
    /// </returns>
    public async Task<T> ReadAsync(CancellationToken cancellationToken)
    {
        //  Чтение значения.
        return (T)await ReadObjectAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно записывает значение.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо записать.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись значения.
    /// </returns>
    public async Task WriteAsync(T value, CancellationToken cancellationToken)
    {
        //  Чтение значения.
        await WriteObjectAsync(value, cancellationToken).ConfigureAwait(false);
    }
}
