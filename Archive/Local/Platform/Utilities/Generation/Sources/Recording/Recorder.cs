namespace Apeiron.Platform.Utilities.Sources;

[Name("Информация о регистраторе"), Category("Recording")]
[TableName("Recorders"), CollectionName("Recorders")]
internal sealed class Recorder :
    NamedEntity
{
    [Order(1)]
    [Name("Идентификатор, используемый при удалённой передаче файлов")]
    [DisplayName("Передача")]
    [Indexable]
    public int TransferIdentificator { get; }

    [Order(2)]
    [Name("Каталог для хранения переданных файлов"), DisplayName("Каталог")]
    public InternalDirectory? TransferDirectory { get; }
}
