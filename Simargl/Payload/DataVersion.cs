using System.Globalization;
using System.Text.RegularExpressions;

namespace Simargl.Payload;

/// <summary>
/// Представляет версию данных.
/// </summary>
public readonly partial struct DataVersion :
    IEquatable<DataVersion>,
    IComparable<DataVersion>
{
    /// <summary>
    /// Возвращает значение, определяющее отсутствие версии.
    /// </summary>
    public static DataVersion Node { get; } = default;

    /// <summary>
    /// Выполняет проверку текста.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsValid(string value) => IsValidString(value);

    // [ Major(11) | Minor(10) | Patch(10) | Debug(1) ]  => ровно 32 бита
    private const int DebugBits = 1;
    private const int PatchBits = 10;
    private const int MinorBits = 10;
    private const int MajorBits = 11;

    private const int DebugShift = 0;
    private const int PatchShift = DebugShift + DebugBits;   // 1
    private const int MinorShift = PatchShift + PatchBits;   // 11
    private const int MajorShift = MinorShift + MinorBits;   // 21

    private const uint DebugMask = (1u << DebugBits) - 1;    // 0x1
    private const uint PatchMask = (1u << PatchBits) - 1;    // 0x3FF
    private const uint MinorMask = (1u << MinorBits) - 1;    // 0x3FF
    private const uint MajorMask = (1u << MajorBits) - 1;    // 0x7FF

    /// <summary>
    /// 
    /// </summary>
    public const int MaxMajor = (int)MajorMask;  // 0..2047

    /// <summary>
    /// 
    /// </summary>
    public const int MaxMinor = (int)MinorMask;  // 0..1023

    /// <summary>
    /// 
    /// </summary>
    public const int MaxPatch = (int)PatchMask;  // 0..1023

    // Строковый формат: x.x.x[.D]  (буква D; регистр не важен)
    private static readonly Regex Rx =
        MyRegex();

    /// <summary>
    /// 
    /// </summary>
    public int Major { get; }

    /// <summary>
    /// 
    /// </summary>
    public int Minor { get; }

    /// <summary>
    /// 
    /// </summary>
    public int Patch { get; }

    /// <summary>
    /// 
    /// </summary>
    public bool Debug { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="major"></param>
    /// <param name="minor"></param>
    /// <param name="patch"></param>
    /// <param name="debug"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public DataVersion(int major, int minor, int patch, bool debug = false)
    {
        if ((uint)major > MajorMask) throw new ArgumentOutOfRangeException(nameof(major), $"0..{MaxMajor}");
        if ((uint)minor > MinorMask) throw new ArgumentOutOfRangeException(nameof(minor), $"0..{MaxMinor}");
        if ((uint)patch > PatchMask) throw new ArgumentOutOfRangeException(nameof(patch), $"0..{MaxPatch}");

        Major = major; Minor = minor; Patch = patch; Debug = debug;
    }

    private DataVersion(uint packed)
    {
        Debug = ((packed >> DebugShift) & DebugMask) != 0;
        Patch = (int)((packed >> PatchShift) & PatchMask);
        Minor = (int)((packed >> MinorShift) & MinorMask);
        Major = (int)((packed >> MajorShift) & MajorMask);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [CLSCompliant(false)]
    public uint ToUInt32()
    {
        uint v = 0;
        v |= ((uint)Major & MajorMask) << MajorShift;
        v |= ((uint)Minor & MinorMask) << MinorShift;
        v |= ((uint)Patch & PatchMask) << PatchShift;
        v |= (Debug ? 1u : 0u) << DebugShift;
        return v;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public static DataVersion FromUInt32(uint value) => new(value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryParse(string s, out DataVersion result)
    {
        result = default;
        if (string.IsNullOrWhiteSpace(s)) return false;

        var m = Rx.Match(s);
        if (!m.Success) return false;

        if (!int.TryParse(m.Groups[1].Value, NumberStyles.None, CultureInfo.InvariantCulture, out var major)) return false;
        if (!int.TryParse(m.Groups[2].Value, NumberStyles.None, CultureInfo.InvariantCulture, out var minor)) return false;
        if (!int.TryParse(m.Groups[3].Value, NumberStyles.None, CultureInfo.InvariantCulture, out var patch)) return false;

        if ((uint)major > MajorMask || (uint)minor > MinorMask || (uint)patch > PatchMask) return false;

        bool debug = m.Groups[4].Success; // есть ".D" → Debug = true
        result = new DataVersion(major, minor, patch, debug);
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public static DataVersion Parse(string s)
    {
        if (!TryParse(s, out var v))
            throw new FormatException($"Ожидается формат x.x.x[.D] (буква D), и значения в диапазонах: Major 0..{MaxMajor}, Minor 0..{MaxMinor}, Patch 0..{MaxPatch}.");
        return v;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsValidString(string s) => TryParse(s, out _);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        Debug ? $"{Major}.{Minor}.{Patch}.D" : $"{Major}.{Minor}.{Patch}";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(DataVersion other) => ToUInt32() == other.ToUInt32();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) => obj is DataVersion pv && Equals(pv);
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => (int)ToUInt32();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(DataVersion other)
    {
        var a = ToUInt32(); var b = other.ToUInt32();
        return a < b ? -1 : (a > b ? 1 : 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(DataVersion left, DataVersion right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(DataVersion left, DataVersion right)
    {
        return !(left == right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator <(DataVersion left, DataVersion right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator <=(DataVersion left, DataVersion right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator >(DataVersion left, DataVersion right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator >=(DataVersion left, DataVersion right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^\s*(\d+)\.(\d+)\.(\d+)(?:\.([dD]))?\s*$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex MyRegex();
}
