using System.Text;
using System.IO;
using Simargl.IO;

namespace Simargl.Platform.Modbus;

/// <summary>
/// Представляет заголовок Modbus Tсp транзакции
/// </summary>
public sealed class ReadMultipleHoldingRegisterResponse :
    ModbusTcpHeader
{

    /// <summary>
    /// Константа максимального количества регистров для чтения.
    /// </summary>
    public const byte MaximumRegisterCount = 0x7D;

    /// <summary>
    /// Константа кода команды.
    /// </summary>
    public const byte CodeCommand = 0x03;

    /// <summary>
    /// Возвращает регистры.
    /// </summary>
    [CLSCompliant(false)]
    public ushort[] Registers { get; private set; } = Array.Empty<ushort>();

    /// <summary>
    /// Инициализирует объект по умолчанию.
    /// </summary>
    public ReadMultipleHoldingRegisterResponse()
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
    /// <param name="registers">
    /// Регистры.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="registers"/> передан пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="transactionIdentifier"/> передано не допустимое значенние.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="registers"/> передан массив длинны больше допустимой.
    /// </exception>
    [CLSCompliant(false)]
    public ReadMultipleHoldingRegisterResponse(int transactionIdentifier, byte address, ushort[] registers)
        : base(transactionIdentifier,address, CodeCommand, IsNotNull(registers, nameof(registers)).Length * 2 + 1)
    {
        //  Проверка количества регистров.
        IsNotLarger(registers.Length, MaximumRegisterCount, nameof(registers));

        //  Установка значения массива.
        Registers = registers;
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
    public new ReadMultipleHoldingRegisterResponse Load(Stream stream)
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

        //  Чтение длины данных.
        var length = reader.ReadByte();

        //  Инициализация данных
        Registers = new ushort[length / 2];

        //  Цикл.
        for(int i = 0; i < length / 2; i++)
        {
            //  Чтение регистров.  
            Registers[i] = Reverser.Reverse(reader.ReadUInt16());
        }

        //  Возврат значения.
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
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public new async Task<ReadMultipleHoldingRegisterResponse> LoadAsync(Stream stream, CancellationToken cancellationToken)
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

        //  Чтение длины данных.
        byte length = await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        //  Инициализация данных
        Registers = new ushort[length / 2];

        //  Цикл.
        for (int i = 0; i < length / 2; i++)
        {
            //  Чтение регистров.  
            Registers[i] = Reverser.Reverse(await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false));
        }

        //  Возврат значения.
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

        //  Подсчет длинны.
        byte length = (byte)(Registers.Length * 2);

        //  Запись длинны данных
        writer.Write(length);

        //  Цикл.
        for(int i = 0; i<length / 2; i++)
        {
            //  Запись регистра.
            writer.Write(Reverser.Reverse(Registers[i]));
        }
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

        //  Подсчет длинны.
        byte length = (byte)(Registers.Length * 2);

        //  Запись длинны данных
        await spreader.WriteUInt8Async(length, cancellationToken).ConfigureAwait(false);

        //  Цикл.
        for (int i = 0; i < length / 2; i++)
        {
            //  Запись регистра.
            await spreader.WriteUInt16Async(Reverser.Reverse(Registers[i]), cancellationToken).ConfigureAwait(false);
        }
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
