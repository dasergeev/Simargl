namespace Apeiron.Platform.Utilities.Sources;

[Name("Метрика внутреннего файл файловой системы"), Category("FileSystem")]
[TableName("InternalFileMetrics"), CollectionName("Metrics")]
[AlternateKey("Уникальное время создания метрики для данного файла",
    nameof(File) + "Id", nameof(DeterminationTime))]
internal sealed class InternalFileMetric :
    Entity
{
    [Order(1)]
    [Name("Значение, определяющее, существует ли файл"), DisplayName("Существование")]
    [Indexable]
    public bool IsExists { get; }

    [Order(2)]
    [Name("Время определения метрики"), DisplayName("Определение")]
    [Indexable]
    public DateTime DeterminationTime { get; }

    [Order(3)]
    [Name("Время регистрации файла"), DisplayName("Регистрация")]
    [Indexable]
    public DateTime RegistrationTime { get; }

    [Order(4)]
    [Name("Время создания файла"), DisplayName("Создан")]
    [Indexable]
    public DateTime CreationTime { get; }

    [Order(5)]
    [Name("Время последнего доступа к файлу"), DisplayName("Доступ")]
    [Indexable]
    public DateTime LastAccessTime { get; }

    [Order(6)]
    [Name("Время последней операции записи в файл"), DisplayName("Изменён")]
    [Indexable]
    public DateTime LastWriteTime { get; }

    [Order(7)]
    [Name("Размер файла"), DisplayName("Размер")]
    [Indexable]
    public long Size { get; }

    [Order(8)]
    [Name("Циклический избыточный код"), DisplayName("Crc32")]
    public long Crc32 { get; }

    [Order(9)]
    [Name("Хэш-код файла"), DisplayName("Хэш-код")]
    public long HashCode { get; }

    [Order(10)]
    [Name("Сумма байтов файла"), DisplayName("Сумма")]
    public long BytesSum { get; }

    [Order(11)]
    [Name("Внутренний файл файловой системы"), DisplayName("Файл")]
    [Cascade]
    public InternalFile? File { get; }
}
