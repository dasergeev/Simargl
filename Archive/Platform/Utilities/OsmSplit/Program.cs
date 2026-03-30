using System.Text;
using System.Xml;

//  Путь к исходному файлу.
const string sourcePath = @"E:\planet-220131.osm\planet-220131.osm";

//  Путь к каталогу частей.
const string partsPath = @"\\railtest.ru\Data\06-НТО\RawData\Osm\Parts";

//  Максимальное количество элементов в файле.
const long maxCount = 10000;

//  Средство записи файла.
StreamWriter? writer = null;

//  Текущий номер файла.
long number = 0;

//  Создаёт новое средство записи файла.
void newWriter()
{
    //  Проверка текущего средства.
    if (writer is not null)
    {
        //  Запись конца файла.
        writer.WriteLine(@"</osm>");

        //  Сброс данных на диск.
        writer.Flush();

        //  Закрытие средства.
        writer.Close();

        //  Разрушение средства.
        writer.Dispose();
    }

    //  Увеличение номера счётчика.
    number++;

    //  Создание нового средства.
    writer = new(Path.Combine(partsPath, $"{number: 000 000 000 000 000}.xml"), false, Encoding.UTF8);

    //  Запись начала файла.
    writer.WriteLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
    writer.WriteLine(@"<osm>");
}

//  Блок перехвата всех исключений.
try
{
    //  Средство записи файла.
    using StreamWriter nodeTypes = new(Path.Combine(partsPath, $"NodeTypes.txt"), false, Encoding.UTF8);

    //  Открытие файла для чтения.
    using XmlReader reader = XmlReader.Create(sourcePath);

    //  Вывод информации в консоль.
    Console.WriteLine("Файл открыт.");

    //  Индекс текущего элемента.
    long index = 0;

    //  Чтение данных файла.
    while (reader.Read())
    {
        //  Проверка индекса.
        if (index % maxCount == 0)
        {
            //  Вывод информации в консоль.
            Console.WriteLine($"index: {index}");

            //  Создание нового средства записи.
            newWriter();
        }

        //  Проверка типа узла.
        if (reader.NodeType != XmlNodeType.Whitespace &&
            reader.NodeType != XmlNodeType.Element)
        {
            //  Запись в файл.
            nodeTypes.WriteLine($"{reader.NodeType}\t{index}");
        }

        //  Проверка узла.
        if (reader.NodeType == XmlNodeType.Element && reader.Name != "osm")
        {
            //  Запись данных в файл.
            writer?.WriteLine(reader.ReadOuterXml());
        }

        //  Увеличение индекса.
        index++;
    }
}
catch (Exception ex)
{
    //  Вывод информации об исключении в консоль.
    Console.WriteLine();
    Console.WriteLine(new string('=', 32));
    Console.WriteLine(ex);
}
finally
{
    //  Разрушение средства записи файла.
    writer?.Dispose();
}

//  Вывод информации в консоль.
Console.WriteLine("Разбивка завершена.");
