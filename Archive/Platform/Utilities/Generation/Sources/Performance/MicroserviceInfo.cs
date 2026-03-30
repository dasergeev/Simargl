namespace Apeiron.Platform.Utilities.Sources;

[Name("Информация о микрослужбе"), Category("Performance")]
[TableName("MicroserviceInfos")]
internal sealed class MicroserviceInfo :
    NamedEntity
{
    [Order(1)]
    [Name("Время задержки в миллисекундах перед выполнением следующего шага микрослужбы")]
    [DisplayName("Задержка")]
    [Positive]
    public int NextStepDelay { get; }

    [Order(2)]
    [Name("Полное имя службы"), DisplayName("Полное имя")]
    [Nullable]
    public string? FullName { get; }
}
