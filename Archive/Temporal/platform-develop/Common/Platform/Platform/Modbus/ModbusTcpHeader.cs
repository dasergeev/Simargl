using System.Text;
using Apeiron.IO;

namespace Apeiron.Platform.Modbus;

/// <summary>
/// Представляет заголовок Modbus Tсp транзакции
/// </summary>
public abstract class ModbusTcpHeader 
{
    /// <summary>
    /// Константа  - идентификатор протокола.
    /// </summary>
    private const ushort _ProtocolIdentifier = 0x0000;

    /// <summary>
    /// Возвращает идентификатор транзакции.
    /// </summary>
    public int TransactionIdentifier { get; private set; }

    /// <summary>
    /// Возвращает адрес устройства коммуникации.
    /// </summary>
    public byte Address { get; private set; }

    /// <summary>
    /// Возвращает функциональный код.
    /// </summary>
    public byte FunctionalCode { get; private set; }

    /// <summary>
    /// Возвращает длинну данных.
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Инициализирует объект по умолчанию.
    /// </summary>
    protected ModbusTcpHeader()
    {

    }

    /// <summary>
    /// Инициализирует объект
    /// </summary>
    /// <param name="transactionIdentifier">
    /// Идентификатор транзакции.
    /// </param>
    /// <param name="address">
    /// Адрес устройства
    /// </param>
    /// <param name="code">
    /// Функциональный код.
    /// </param>
    /// <param name="length">
    /// Длина сообщения.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="transactionIdentifier"/> передано не допустимое значенние.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано не допустимое значенние.
    /// </exception>
    protected ModbusTcpHeader(int transactionIdentifier, byte address, byte code, int length)
    {
        // Проверка идентификатора.
        Check.IsNotLarger(transactionIdentifier,ushort.MaxValue,nameof(transactionIdentifier));

        //  Проверка идентификатора.
        Check.IsNotLess(transactionIdentifier,ushort.MinValue,nameof(transactionIdentifier));

        // Проверка длинны.
        Check.IsNotLarger(length, byte.MaxValue + 2, nameof(length));

        //  Проверка длинны.
        Check.IsNotLess(length, byte.MinValue, nameof(length));

        //  Устанавливает длинну.
        Length = length;

        //  Установка идентификатора.
        TransactionIdentifier = transactionIdentifier;  

        //  Установка адреса
        Address = address;

        //  Установка функционального кода.
        FunctionalCode = code;
    }

    /// <summary>
    /// Представляет функцию получения данных из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток.
    /// </param>
    /// <returns>
    /// Экземпляр класса.
    /// </returns>
    /// <exception cref="IOException">
    /// Не верный протокол обмена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="Length"/> вне допустимого диапозона.
    /// </exception>
    internal ModbusTcpHeader Load(Stream stream)
    {
        //  Создание читателя.
        using BinaryReader binaryReader = new(stream, Encoding.UTF8, true);

        //  Чтение идентификатора транзации
        TransactionIdentifier = Reverser.Reverse(binaryReader.ReadUInt16());

        //  Чтение протокола.
        var protocol = Reverser.Reverse(binaryReader.ReadUInt16());

        //  Проверка протокола
        if (protocol != _ProtocolIdentifier)
        {
            throw Exceptions.StreamInvalidFormat();
        }

        //  Чтение длины сообщения.
        Length = Reverser.Reverse(binaryReader.ReadUInt16());

        // Проверка длинны.
        Check.IsNotLarger(Length, byte.MaxValue + 2, nameof(Length));

        //  Проверка длинны.
        Check.IsNotLess(Length, byte.MinValue, nameof(Length));

        //  Чтение адреса устройства.
        Address = binaryReader.ReadByte();

        //  Чтение функционального кода.
        FunctionalCode = binaryReader.ReadByte();

        //  Возврат результата.
        return this;
    }


    /// <summary>
    /// Представляет асинхронное получение данных из потока
    /// </summary>
    /// <param name="stream">
    /// Поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, с экзепляром класса.
    /// </returns>
    /// <exception cref="IOException">
    /// Не верный протокол обмена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="Length"/> вне допустимого диапозона.
    /// </exception>
    internal async Task<ModbusTcpHeader> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Создание читателя.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Чтение идентификатора транзации
        TransactionIdentifier = Reverser.Reverse(await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false));

        //  Чтение протокола.
        var protocol = Reverser.Reverse(await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false));

        //  Проверка протокола
        if (protocol != _ProtocolIdentifier)
        {
            throw Exceptions.StreamInvalidFormat();
        }

        //  Чтение длины сообщения.
        Length = Reverser.Reverse(await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false));

        // Проверка длинны.
        Check.IsNotLarger(Length, byte.MaxValue + 2, nameof(Length));

        //  Проверка длинны.
        Check.IsNotLess(Length, byte.MinValue, nameof(Length));

        //  Чтение адреса устройства.
        Address = await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        //  Чтение функционального кода.
        FunctionalCode = await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        return this;
    }

    /// <summary>
    /// Представляет функцию записи объекта в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток.
    /// </param>
    internal void Save(Stream stream)
    {
        //  Создание писателя.
        using BinaryWriter binaryWriter = new(stream, Encoding.UTF8, true);

        //  Запись идентификатора транзакции.
        binaryWriter.Write(Reverser.Reverse((ushort)TransactionIdentifier));

        //  Запись идентификатора протокола.
        binaryWriter.Write(Reverser.Reverse(_ProtocolIdentifier));

        //  Запись длинны сообщения.
        binaryWriter.Write(Reverser.Reverse((ushort)Length));

        //  Запись адреса.
        binaryWriter.Write(Address);

        //  Запись функционального кода
        binaryWriter.Write(FunctionalCode);
    }

    /// <summary>
    /// Представляет асинхронную функцию записи объекта в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>Возвращает задачу.</returns>
    internal async Task SaveAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Создание писателя.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Запись идентификатора транзакции.
        await spreader.WriteUInt16Async(Reverser.Reverse((ushort)TransactionIdentifier),cancellationToken).ConfigureAwait(false);

        //  Запись идентификатора протокола.
        await spreader.WriteUInt16Async(Reverser.Reverse(_ProtocolIdentifier), cancellationToken).ConfigureAwait(false);

        //  Запись длинны сообщения.
        await spreader.WriteUInt16Async(Reverser.Reverse((ushort)Length), cancellationToken).ConfigureAwait(false);

        //  Запись адреса.
        await spreader.WriteUInt8Async(Address, cancellationToken).ConfigureAwait(false);

        //  Запись функционального кода
        await spreader.WriteUInt8Async(FunctionalCode, cancellationToken).ConfigureAwait(false);
    }
}
