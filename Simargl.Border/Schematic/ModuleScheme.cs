using Simargl.Border.Geometry;
using Simargl.Net;

namespace Simargl.Border.Schematic;

/// <summary>
/// Представляет схему модуля.
/// </summary>
public sealed class ModuleScheme
{
    /// <summary>
    /// Возвращает или инициализирует метку.
    /// </summary>
    public required string Label { get; init; }

    /// <summary>
    /// Возвращает или инициализирует рельс.
    /// </summary>
    public required Rail Rail { get; init; }

    /// <summary>
    /// Возвращает или инициализирует номер секции.
    /// </summary>
    public required int Section { get; init; }

    /// <summary>
    /// Возвращает или инициализирует имя.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Возвращает или инициализирует первый IPv4-адрес.
    /// </summary>
    public required IPv4Address IPAddress1 { get; init; }

    /// <summary>
    /// Возвращает или инициализирует второй IPv4-адрес.
    /// </summary>
    public required IPv4Address IPAddress2 { get; init; }

    /// <summary>
    /// Возвращает или инициализирует масштаб.
    /// </summary>
    public required double Scale { get; init; }
}
