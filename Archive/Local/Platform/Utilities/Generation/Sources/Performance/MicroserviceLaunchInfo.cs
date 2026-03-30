namespace Apeiron.Platform.Utilities.Sources;

[Name("Информация о запуске микрослужбы"), Category("Performance")]
[TableName("MicroserviceLaunchInfos"), CollectionName("Launches")]
[AlternateKey("Уникальная пара Микрослужба-Компьютер",
    nameof(Microservice) + "Id", nameof(Computer) + "Id")]
internal sealed class MicroserviceLaunchInfo :
    Entity
{
    [Order(1)]
    [Name("Флаг, указывающий, включен ли запуск микрослужбы"), DisplayName("Включен")]
    [Indexable]
    public bool IsEnable { get; }

    [Order(2)]
    [Name("Микрослужба"), DisplayName("Микрослужба")]
    [Cascade]
    public MicroserviceInfo? Microservice { get; }

    [Order(3)]
    [Name("Компьютер"), DisplayName("Компьютер")]
    [Cascade]
    public HostComputerInfo? Computer { get; }
}
