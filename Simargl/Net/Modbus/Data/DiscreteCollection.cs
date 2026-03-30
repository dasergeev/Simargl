namespace Simargl.Net.Modbus.Data;

/// <summary>
/// Коллекция дискретных значений (битов), используемая для представления
/// катушек (Coils) и дискретных входов (Discrete Inputs) в протоколе Modbus.
/// Обеспечивает преобразование набора логических значений в сетевое
/// представление в виде массива байт.
/// </summary>
public class DiscreteCollection :
    Collection<bool>,
    IModbusMessageDataCollection
{
    /// <summary>
    /// Количество битов в одном байте.
    /// Используется при упаковке логических значений в сетевое представление.
    /// </summary>
    private const int BitsPerByte = 8;

    /// <summary>
    /// Внутренний список, хранящий дискретные значения.
    /// Используется как базовая коллекция для <see cref="Collection{T}"/>.
    /// </summary>
    private readonly List<bool> _discretes;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DiscreteCollection"/>
    /// с пустым набором дискретных значений.
    /// </summary>
    public DiscreteCollection() :
        this(new List<bool>())
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DiscreteCollection"/>
    /// на основе массива логических значений.
    /// </summary>
    /// <param name="bits">Массив дискретных значений.</param>
    public DiscreteCollection(params bool[] bits) :
        this((IList<bool>)bits)
    {
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DiscreteCollection"/>
    /// на основе массива байт в сетевом формате Modbus.
    /// Каждый бит каждого байта преобразуется в отдельное логическое значение.
    /// </summary>
    /// <param name="bytes">Массив байт в сетевом представлении.</param>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается, если <paramref name="bytes"/> равен null.
    /// </exception>
    public DiscreteCollection(params byte[] bytes) :
        this()
    {
        if (bytes == null)
        {
            throw new ArgumentNullException(nameof(bytes));
        }

        _discretes.Capacity = bytes.Length * BitsPerByte;

        foreach (byte b in bytes)
        {
            _discretes.Add((b & 1) == 1);
            _discretes.Add((b & 2) == 2);
            _discretes.Add((b & 4) == 4);
            _discretes.Add((b & 8) == 8);
            _discretes.Add((b & 16) == 16);
            _discretes.Add((b & 32) == 32);
            _discretes.Add((b & 64) == 64);
            _discretes.Add((b & 128) == 128);
        }
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DiscreteCollection"/>
    /// на основе списка логических значений.
    /// </summary>
    /// <param name="bits">Список дискретных значений.</param>
    public DiscreteCollection(IList<bool> bits) :
        this(new List<bool>(bits))
    {
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DiscreteCollection"/>
    /// на основе существующего списка логических значений.
    /// Используется внутри библиотеки.
    /// </summary>
    /// <param name="bits">Список дискретных значений.</param>
    internal DiscreteCollection(List<bool> bits) :
        base(bits)
    {
        Debug.Assert(bits != null, "Discrete bits is null.");
        _discretes = bits;
    }

    /// <summary>
    /// Возвращает массив байт в сетевом формате Modbus,
    /// сформированный из текущего набора дискретных значений.
    /// Каждый байт содержит до 8 битов, начиная с младшего бита.
    /// </summary>
    public byte[] NetworkBytes
    {
        get
        {
            byte[] bytes = new byte[ByteCount];

            for (int index = 0; index < _discretes.Count; index++)
            {
                if (_discretes[index])
                {
                    bytes[index / BitsPerByte] |= (byte)(1 << (index % BitsPerByte));
                }
            }

            return bytes;
        }
    }

    /// <summary>
    /// Возвращает количество байт, необходимое для представления
    /// текущего количества дискретных значений в сетевом формате.
    /// </summary>
    public byte ByteCount => (byte)((Count + 7) / 8);

    /// <summary>
    /// Возвращает строковое представление коллекции
    /// в виде последовательности 0 и 1, заключённых в фигурные скобки.
    /// </summary>
    /// <returns>Строковое представление текущей коллекции.</returns>
    public override string ToString()
    {
        return string.Concat("{", string.Join(", ", this.Select(discrete => discrete ? "1" : "0").ToArray()), "}");
    }
}
