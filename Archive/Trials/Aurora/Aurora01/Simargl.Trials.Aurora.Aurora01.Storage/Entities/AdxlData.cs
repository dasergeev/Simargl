using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Simargl.Trials.Aurora.Aurora01.Storage.Entities;

/// <summary>
/// Представляет данные датчика Adxl.
/// </summary>
public class AdxlData
{
    /// <summary>
    /// Возвращает или задаёт адрес датчика.
    /// </summary>
    public int Address { get; set; }

    /// <summary>
    /// Возвращает коллекцию информации о файлах данных датчиков.
    /// </summary>
    public HashSet<AdxlFileData> AdxlFiles { get; set; } = [];

    /// <summary>
    /// Возвращает коллекцию данных Adxl.
    /// </summary>
    public HashSet<AdxlChannelData> AdxlChannels { get; set; } = [];

    /// <summary>
    /// Возвращает IP-адрес.
    /// </summary>
    /// <param name="address">
    /// Адрес датчика.
    /// </param>
    /// <returns>
    /// IP-адрес.
    /// </returns>
    public static string ToIPAddress(int address)
    {
        //  Получение частей адреса.
        byte part0 = unchecked((byte)(address & 0xFF));
        byte part1 = unchecked((byte)((address >> 8) & 0xFF));
        byte part2 = unchecked((byte)((address >> 16) & 0xFF));
        byte part3 = unchecked((byte)((address >> 24) & 0xFF));

        //  Создание построителя строки.
        StringBuilder builder = new(16);

        //  Формирование строки.
        builder.Append(part0);
        builder.Append('.');
        builder.Append(part1);
        builder.Append('.');
        builder.Append(part2);
        builder.Append('.');
        builder.Append(part3);

        //  Возврат полученной строки.
        return builder.ToString();
    }

    /// <summary>
    /// Выполняет попытку получить адрес.
    /// </summary>
    /// <param name="ipAddress">
    /// IP-адрес.
    /// </param>
    /// <param name="address">
    /// Адрес.
    /// </param>
    /// <returns>
    /// Результат попытки.
    /// </returns>
    public static bool TryFromIPAddress(string ipAddress, out int address)
    {
        //  Установка значения по умолчанию.
        address = 0;

        //  Проверка IP-адреса.
        if (string.IsNullOrEmpty(ipAddress))
        {
            //  Строка не содержит информации.
            return false;
        }

        //  Разбивка строки.
        string[] parts = ipAddress.Split('.');

        //  Проверка данных.
        if (parts.Length != 4 ||
            !byte.TryParse(parts[0], out byte part0) ||
            !byte.TryParse(parts[1], out byte part1) ||
            !byte.TryParse(parts[2], out byte part2) ||
            !byte.TryParse(parts[3], out byte part3))
        {
            //  Строка содержит некорректную информации.
            return false;
        }

        //  Получение адреса.
        address = part0 | (part1 << 8) | (part2 << 16) | (part3 << 24);

        //  Адрес успешно получен.
        return true;
    }

    /// <summary>
    /// Возвращает адрес.
    /// </summary>
    /// <param name="ipAddress">
    /// IP-адрес.
    /// </param>
    /// <returns>
    /// Адрес.
    /// </returns>
    public static int FromIPAddress(string ipAddress)
    {
        //  Попытка получить адрес.
        if (!TryFromIPAddress(ipAddress, out int address))
        {
            //  Строка не содержит IP-адрес.
            throw new InvalidDataException($"Строка \"{ipAddress}\" не содержит IP-адреса.");
        }

        //  Возврат адреса.
        return address;
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
        //  Установка первичного ключа.
        typeBuilder.HasKey(x => x.Address);

        //  Отключение автоинкремента.
        typeBuilder.Property(x => x.Address).ValueGeneratedNever();
    }
}
