using System.Text;
using Apeiron.IO;

namespace Apeiron.Platform.Modbus;

/// <summary>
/// Представляет заголовок Modbus Tсp транзакции
/// </summary>
public sealed class WriteMultipleHoldingRegisterResponse:
    ModbusTcpHeader
{
    /// <summary>
    /// Константа максимального количества регистров для чтения.
    /// </summary>
    public const byte MaximumRegisterCount = 0x7B;

    /// <summary>
    /// Константа кода команды.
    /// </summary>
    public const byte CodeCommand = 0x10;

    /// <summary>
    /// Возвращает адрес устройства коммуникации.
    /// </summary>
    public int StartAddress { get; private set; }

    /// <summary>
    /// Возвращает функциональный код.
    /// </summary>
    public int CountRegister { get; private set; }

    /// <summary>
    /// Инициализирует объект по умолчанию.
    /// </summary>
    public WriteMultipleHoldingRegisterResponse()
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
    /// <param name="start">
    /// Начальный адрес регистра.
    /// </param>
    /// <param name="count">
    /// Количество записанных регистров.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="transactionIdentifier"/> передано не допустимое значенние.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="start"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано не допустимое значение.
    /// </exception>
    public WriteMultipleHoldingRegisterResponse(int transactionIdentifier, byte address, int start, int count)
        : base(transactionIdentifier, address, CodeCommand, 6)
    {
        // Проверка адреса.
        Check.IsNotLarger(start, ushort.MaxValue, nameof(start));

        //  Проверка адреса.
        Check.IsNotLess(start, ushort.MinValue, nameof(start));

        // Проверка количества регистров.
        Check.IsNotLarger(count, MaximumRegisterCount, nameof(count));

        // Проверка количества регистров.
        Check.IsNotLess(count, byte.MinValue, nameof(count));

        //  Установка адреса
        StartAddress = start;

        //  Установка количества.
        CountRegister = count;
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
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public new WriteMultipleHoldingRegisterResponse Load(Stream stream)
    {
        //  Чтение базового класса.
        base.Load(stream);

        //  Проверка полученного кода функции.
        if (FunctionalCode != CodeCommand)
        {
            //  Выброс исключения
            throw new FormatException($"В modbus header пришел код ошибки: {FunctionalCode}");
        }

        //Создание писателя
        using BinaryReader reader = new(stream, Encoding.UTF8, true);

        //  Чтение первого адресса.
        StartAddress = Reverser.Reverse(reader.ReadUInt16());

        //  Чтение количества регистров.
        CountRegister = Reverser.Reverse(reader.ReadUInt16());

        return this;
    }

    /// <summary>
    /// Представляет асинхронное получение данных из потока
    /// </summary>
    /// <param name="stream">
    /// Поток.
    /// </param>
    /// <param name="cancellationToken"
    /// >Токен отмены.
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
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public new async Task<WriteMultipleHoldingRegisterResponse> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Чтение базового класса.
        await base.LoadAsync(stream, cancellationToken).ConfigureAwait(false);

        //  Проверка полученного кода функции.
        if (FunctionalCode != CodeCommand)
        {
            //  Выброс исключения
            throw new FormatException($"В modbus header пришел код ошибки: {FunctionalCode}");
        }

        //Создание писателя
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Чтение первого адресса.
        StartAddress = Reverser.Reverse((ushort)await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false));

        //  Чтение количества регистров.
        CountRegister = Reverser.Reverse((ushort)await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false));

        //  Возвращает указатель на текущий объект.
        return this;
    }

    /// <summary>
    /// Представляет функцию записи объекта в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток.
    /// </param>
    public new void Save(Stream stream)
    {
        //  Запись базового класса.
        base.Save(stream);

        //  Создание писателя.
        using BinaryWriter writer = new(stream, Encoding.UTF8, true);

        //  Запись начального индекса.
        writer.Write(Reverser.Reverse((ushort)StartAddress));

        //  Запись количества регистров.
        writer.Write(Reverser.Reverse((ushort)CountRegister));
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
    /// <returns>
    /// Возвращает задачу.
    /// </returns>
    public new async Task SaveAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Запись базового класса.
        await base.SaveAsync(stream, cancellationToken).ConfigureAwait(false);

        //  Создание писателя.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Запись начального индекса.
        await spreader.WriteUInt16Async(Reverser.Reverse((ushort)StartAddress), cancellationToken).ConfigureAwait(false);

        //  Запись количества регистров.
        await spreader.WriteUInt16Async(Reverser.Reverse((ushort)CountRegister), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Возвращает размер пакета.
    /// </summary>
    public int Size
    {
        get
        {
            MemoryStream memory = new ();

            Save(memory);

            return (int)memory.Length;
        }
    }
}
