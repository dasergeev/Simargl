using System.Text;

namespace Apeiron.Platform.Teltonika;

/// <summary>
/// Представляет класс данных модема и SIM карты.
/// </summary>
public class ModemInfo
{
    /// <summary>
    /// Представляет идентификатор модема.
    /// </summary>
    public string Imei { get; private set; }

    /// <summary>
    /// Представляет идентификатор SIM карты.
    /// </summary>
    public string Iccid { get; private set; }
    
    /// <summary>
    /// Представляет идентификатор SIM карты.
    /// </summary>
    public string Imsi { get; private set; }

    /// <summary>
    /// Представляет уровень сигнала GSM.
    /// </summary>
    public int GsmSignal { get; private set; }

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="imei">
    /// Идентификатор модема.
    /// </param>
    /// <param name="iccid">
    /// Идентификатор Sim карты.
    /// </param>
    /// <param name="imsi">
    /// Идентификатор Sim карты.
    /// </param>
    /// <param name="gsmSignal">
    /// Уровень сигнала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="imei"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="iccid"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="imsi"/> передана пустая ссылка.
    /// </exception>
    public ModemInfo(string imei, string iccid, string imsi, int gsmSignal)
    {
        Imei = Check.IsNotNull(imei,nameof(imei));
        Iccid = Check.IsNotNull(iccid, nameof(iccid));
        Imsi = Check.IsNotNull(imsi, nameof(imsi));
        GsmSignal = gsmSignal;
    }


    /// <summary>
    /// Преобразует данные в строки.
    /// </summary>
    /// <returns>
    /// Строка данных.
    /// </returns>
    public override string ToString()
    {
        //  Создание конструктора строки.
        StringBuilder builder = new(2048);

        //  Добавление индентификатора
        builder.Append($"IMEI\t{Imei}\r\n");

        //  Добавление индентификатора
        builder.Append($"ICCID\t{Iccid}\r\n");

        //  Добавление индентификатора
        builder.Append($"IMSI\t{Imsi}\r\n");

        //  Добавление уровня сигнала
        builder.Append($"GsmSignal\t{GsmSignal}\r\n");

        //  Возврат результата
        return builder.ToString();
    }

}
