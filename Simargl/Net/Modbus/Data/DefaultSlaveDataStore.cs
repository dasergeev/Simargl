using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Data;

/// <summary>
/// Реализация хранилища данных ведомого устройства (Slave) Modbus.
/// Содержит независимые источники точек для:
/// регистров хранения (Holding Registers),
/// входных регистров (Input Registers),
/// дискретных выходов (Coils),
/// дискретных входов (Discrete Inputs).
/// </summary>
public class DefaultSlaveDataStore :
    ISlaveDataStore
{
    /// <summary>
    /// Источник точек для регистров хранения (Holding Registers, функция 0x03/0x06/0x10).
    /// Хранит значения типа <see cref="ushort"/>.
    /// </summary>
    private readonly IPointSource<ushort> _holdingRegisters = new DefaultPointSource<ushort>();

    /// <summary>
    /// Источник точек для входных регистров (Input Registers, функция 0x04).
    /// Хранит значения типа <see cref="ushort"/>.
    /// </summary>
    private readonly IPointSource<ushort> _inputRegisters = new DefaultPointSource<ushort>();

    /// <summary>
    /// Источник точек для дискретных выходов (Coils, функция 0x01/0x05/0x0F).
    /// Хранит значения типа <see cref="bool"/>.
    /// </summary>
    private readonly IPointSource<bool> _coilDiscretes = new DefaultPointSource<bool>();

    /// <summary>
    /// Источник точек для дискретных входов (Discrete Inputs, функция 0x02).
    /// Хранит значения типа <see cref="bool"/>.
    /// </summary>
    private readonly IPointSource<bool> _coilInputs = new DefaultPointSource<bool>();

    /// <summary>
    /// Предоставляет доступ к регистрам хранения (Holding Registers).
    /// </summary>
    /// <remarks>
    /// Свойство помечено как не соответствующее CLS,
    /// поскольку использует тип <see cref="ushort"/>.
    /// </remarks>
    [CLSCompliant(false)]
    public IPointSource<ushort> HoldingRegisters => _holdingRegisters;

    /// <summary>
    /// Предоставляет доступ к входным регистрам (Input Registers).
    /// </summary>
    /// <remarks>
    /// Свойство помечено как не соответствующее CLS,
    /// поскольку использует тип <see cref="ushort"/>.
    /// </remarks>
    [CLSCompliant(false)]
    public IPointSource<ushort> InputRegisters => _inputRegisters;

    /// <summary>
    /// Предоставляет доступ к дискретным выходам (Coils).
    /// </summary>
    [CLSCompliant(false)]
    public IPointSource<bool> CoilDiscretes => _coilDiscretes;

    /// <summary>
    /// Предоставляет доступ к дискретным входам (Discrete Inputs).
    /// </summary>
    [CLSCompliant(false)]
    public IPointSource<bool> CoilInputs => _coilInputs;
}
