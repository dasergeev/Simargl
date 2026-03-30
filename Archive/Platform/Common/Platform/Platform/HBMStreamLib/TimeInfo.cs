namespace Apeiron.QuantumX;

/// <summary>
/// Класс для хранения времени
/// </summary>
[CLSCompliant(false)] public class TimeInfo
{
    /// <summary>
    /// Конструктор класса TimeInfo.
    /// </summary>
    public TimeInfo()
    {
        Era = 0;
        NtpTimeStamp = 0;
        SubFraction = 0;
    }
    /// <summary>
    /// Таймштамп.
    /// </summary>
    public ulong NtpTimeStamp { get; private set; } 
    /// <summary>
    /// Эра.
    /// </summary>
    public uint Era { get; private set; }
    /// <summary>
    /// Получить количество секунд.
    /// </summary>
    public uint Seconds { get { return (uint)(NtpTimeStamp >> 32);  } }
    /// <summary>
    /// Получить доли секунд.
    /// </summary>
    public uint Fraction { get { return (uint)(NtpTimeStamp & 0xffffffff); } }
    /// <summary>
    /// Доли долей секунд
    /// </summary>
    /// <value></value>
    public uint SubFraction { get; private set; }
    
    /// <summary>
    /// Установить значения.
    /// </summary>
    public void Set(HbmMetaInfo metaInfo)
    {
        var stampNode = metaInfo.Params?["stamp"];
        if(stampNode?["type"]?.ToString() == "ntp")
        {
            uint seconds = 0;
            uint fraction = 0;
            var value = stampNode["era"];
            Era = value == null ? 0 : value.GetValue<uint>();

            value = stampNode["seconds"];
            if (value != null)
                seconds = value.GetValue<uint>();
            
            value = stampNode["fraction"];
            if (value != null)
                fraction = value.GetValue<uint>();
            
            NtpTimeStamp = seconds;
            NtpTimeStamp <<= 32;
            NtpTimeStamp |= fraction;

            value = stampNode["subFraction"];
            SubFraction = value == null ? 0 : value.GetValue<uint>();
        }
    }
    /// <summary>
    /// Функция коррекции времени
    /// </summary>
    /// <param name="inc"></param>
    /// <returns></returns>
    public ulong Add(ulong inc)
    {
        NtpTimeStamp += inc;
			return NtpTimeStamp;
    }
    /// <summary>
    /// Функция чистки данных
    /// </summary>
    public void Clear()
    {
        NtpTimeStamp = 0;
    }         
}
