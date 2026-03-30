using Apeiron.Platform.Databases.Entities;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Databases.BomPgDatabase.Entities;

/// <summary>
/// Представляет сущность, которая описывает устройство Teltonika.
/// </summary>
public class Teltonika : Entity
{
    /// <summary>
    /// Серийный номер устройства.
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// Инвентарный номер устройства.
    /// </summary>
    public string? InventoryNumber { get; set; } = string.Empty;

    /// <summary>
    /// Версия прошивки устройства.
    /// </summary>
    public string FirmwareVersion { get; set; } = string.Empty;

    /// <summary>
    /// Mac адрес Lan интерфейса.
    /// </summary>
    public long LanMacAddress { get; set; }

    /// <summary>
    /// IMEI идентификатор устройства.
    /// </summary>
    public long Imei { get; set; }

    /// <summary>
    /// Логин пользователя для подключения к устройству.
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Пароль пользователя для подключения к устройству.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// SSID Wi-Fi сети.
    /// </summary>
    public string? WiFiSsid { get; set; } = string.Empty;

    /// <summary>
    /// Пароль для подключения к Wi-Fi сети.
    /// </summary>
    public string? WiFiPassword { get; set; } = string.Empty;

    /// <summary>
    /// Дата установки устройства.
    /// </summary>
    public DateTime? InstallDateTime { get; set; }

    /// <summary>
    /// Дата изменения конфигурации устройства.
    /// </summary>
    public DateTime? ChangeConfigurationDateTime { get; set; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<Teltonika> typeBuilder)
    {
        typeBuilder.ToTable("Teltonika");
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
        typeBuilder.Property(entity => entity.Id).ValueGeneratedOnAdd().HasColumnName("Id"); ;
        typeBuilder.HasIndex(entity => entity.Id);
    }
}
