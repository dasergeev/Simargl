namespace Apeiron.Platform.Utilities.Sources;

[Name("Информация о таблице базы данных"), Category("Reflection")]
[TableName("DatabaseTables"), CollectionName("Tables")]
internal sealed class DatabaseTable :
    NamedEntity
{
    [Order(1)]
    [Name("Количество элементов в таблице"), DisplayName("Количество")]
    public long Count { get; }

    [Order(2)]
    [Name("Изменение количества элементов в таблице за час"), DisplayName("За час")]
    public long ChangedInHour { get; }

    [Order(3)]
    [Name("Изменение количества элементов в таблице за день"), DisplayName("За день")]
    public long ChangedInDay { get; }

    [Order(4)]
    [Name("Изменение количества элементов в таблице за месяц"), DisplayName("За месяц")]
    public long ChangedInMonth { get; }

    [Order(5)]
    [Name("Скорость изменения количества элементов за час"), DisplayName("Элементов в час")]
    public double SpeedPerHour { get; }

    [Order(6)]
    [Name("Скорость изменения количества элементов за день"), DisplayName("Элементов в день")]
    public double SpeedPerDay { get; }

    [Order(7)]
    [Name("Скорость изменения количества элементов за месяц"), DisplayName("Элементов в месяц")]
    public double SpeedPerMonth { get; }

    [Order(8)]
    [Name("Категория таблицы"), DisplayName("Категория")]
    [Cascade]
    public DatabaseTableCategory? Category { get; }
}
