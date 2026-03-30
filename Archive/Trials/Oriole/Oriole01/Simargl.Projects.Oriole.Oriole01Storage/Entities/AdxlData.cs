using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Net;

namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет данные датчика Adxl.
/// </summary>
public class AdxlData
{
    /// <summary>
    /// Возвращает или инициализирует ключ сущности.
    /// </summary>
    public long Key { get; init; }

    /// <summary>
    /// Возвращает или задаёт адрес датчика.
    /// </summary>
    public int Address { get; set; }

    /// <summary>
    /// Возвращает коллекцию информации о файлах данных датчиков.
    /// </summary>
    public HashSet<AdxlFileData> AdxlFiles { get; set; } = [];

    /// <summary>
    /// Возвращает коллекцию информации о каналах.
    /// </summary>
    public HashSet<ChannelData> Channels { get; set; } = [];

    /// <summary>
    /// Возвращает IP-адрес датчика.
    /// </summary>
    /// <returns>
    /// IP-адрес датчика.
    /// </returns>
    public string GetIPAddress()
    {
        //  Возвращает IP-адрес датчика.
        return new IPAddress(BitConverter.GetBytes(Address)).ToString();
    }

    /// <summary>
    /// Устанавливает IP-адрес датчика.
    /// </summary>
    /// <param name="ipAddress">
    /// IP-адрес датчика.
    /// </param>
    public void SetIPAddress(string ipAddress)
    {
        //  Установка адреса.
        Address = BitConverter.ToInt32(IPAddress.Parse(ipAddress).GetAddressBytes());
    }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<AdxlData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => x.Key);

        //  Настройка уникального адреса.
        typeBuilder.HasIndex(x => x.Address).IsUnique(true);
    }
}
