using System.Text;
using Apeiron.IO;

namespace Apeiron.Platform.Modbus;

/// <summary>
/// Представляет заголовок Modbus Tсp транзакции
/// </summary>
public sealed class WriteMultipleHodingRegisterCommand :
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
    /// Возвращает регистры.
    /// </summary>
    [CLSCompliant(false)]
    public ushort[] Registers { get; private set; } = Array.Empty<ushort>();

    /// <summary>
    /// Инициализирует объект по умолчанию.
    /// </summary>
    public WriteMultipleHodingRegisterCommand()
    {

    }

    /// <summary>
    /// Инициализирует объект
    /// </summary>
    /// <param name="transactionIdentifier">
    /// Идентификатор транзакции.
    /// </param>
    /// <param name="address">
    /// Адрес устройства.
    /// </param>
    /// <param name="start">
    /// Начальный адрес регистра.
    /// </param>
    /// <param name="registers">
    /// Данные регистров для записи.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="registers"/> передан пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="transactionIdentifier"/> передано не допустимое значенние.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="start"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="registers"/> передан массив длинны больше допустимой.
    /// </exception>
    [CLSCompliant(false)]
    public WriteMultipleHodingRegisterCommand(int transactionIdentifier, byte address, int start, ushort[] registers)
        : base(transactionIdentifier, address, CodeCommand, Check.IsNotNull(registers,nameof(registers)).Length * 2 + 7)
    {
        //  Проверка адреса.
        Check.IsNotLarger(start, ushort.MaxValue, nameof(start));

        //  Проверка адреса.
        Check.IsNotLess(start, ushort.MinValue, nameof(start));

        //  Проверка количества регистров.
        Check.IsNotLarger(registers.Length, MaximumRegisterCount, nameof(registers));

        //  Подсчет количества регистров.
        byte count = (byte)(registers.Length);

        //  Установка адреса
        StartAddress = start;

        //  Установка количества.
        CountRegister = count;

        //  Установка значения регистров.
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
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public new WriteMultipleHodingRegisterCommand Load(Stream stream)
    {
        //  Чтение базового класса.
        base.Load(stream);

        //Создание писателя
        using BinaryReader reader = new(stream, Encoding.UTF8, true);

        //  Чтение первого адресса.
        StartAddress = Reverser.Reverse(reader.ReadUInt16());

        //  Чтение количества регистров.
        CountRegister = Reverser.Reverse(reader.ReadUInt16());

        //  Чтение длинны данных.
        var length = reader.ReadByte();

        //  Инициализация массива.
        Registers = new ushort[length / 2];

        //  Цикл
        for(int i = 0; i < length / 2; i++)
        {
            //  Чтение регистра.
            Registers[i] = Reverser.Reverse(reader.ReadUInt16());
        }

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
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public new async Task<WriteMultipleHodingRegisterCommand> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Чтение базового класса.
        await base.LoadAsync(stream, cancellationToken).ConfigureAwait(false);

        //Создание писателя
       Spreader spreader = new(stream, Encoding.UTF8);

        //  Чтение первого адресса.
        StartAddress = Reverser.Reverse((ushort)await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false));

        //  Чтение количества регистров.
        CountRegister = Reverser.Reverse((ushort)await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false));

        //  Чтение длинны данных.
        var length = await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        //  Инициализация массива.
        Registers = new ushort[length / 2];

        //  Цикл
        for (int i = 0; i < length / 2; i++)
        {
            //  Чтение регистра.
            Registers[i] = Reverser.Reverse(await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false));
        }
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

        //  Подсчет длинны сообщения
        byte length = (byte)(CountRegister * 2);

        //  Запись длинны данных
        writer.Write(length);

        //  Цикл
        for (int i = 0; i < length / 2; i++)
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

        //  Запись начального индекса.
        await spreader.WriteUInt16Async(Reverser.Reverse((ushort)StartAddress), cancellationToken).ConfigureAwait(false);

        //  Запись количества регистров.
        await spreader.WriteUInt16Async(Reverser.Reverse((ushort)CountRegister), cancellationToken).ConfigureAwait(false);
        //  Подсчет длинны сообщения
        byte length = (byte)(CountRegister * 2);

        //  Запись длинны данных
        await spreader.WriteUInt8Async(length, cancellationToken).ConfigureAwait(false);

        //  Цикл
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
