using System.Text;
using System.Net;
using System.Text.Json.Nodes;
using System.IO;


#nullable enable

namespace Simargl.QuantumX;

/// <summary>
/// Метаинформация потока
/// </summary>
public class HbmMetaInfo
{
    /// <summary>
    /// Виды представления метаинформации.
    /// </summary>
    public enum MetaInfoType
    {
        ///
        UNKNOWN = 0, //< Не определено.
        ///
        JSON = 1, //< Метаинформация в формате JSON.
        ///
        BINARY = 2 //< Бинарная метаинформация.
    }
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public HbmMetaInfo() { Type = MetaInfoType.UNKNOWN; }

    /// <summary>
    /// Тип представления метаданных
    /// </summary>
    public MetaInfoType Type { get; private set; }
    /// <summary>
    /// Метаданные в двоичном формате, если такие имеются
    /// </summary>
    public byte[]? BinaryContent { get; private set; }
    /// <summary>
    /// Метаданные в формате JSON, если такие имеются
    /// </summary>
    public JsonNode? JsonContent { get; private set; }
    /// <summary>
    /// "Метод" метаинформации
    /// </summary>
    public JsonNode? Method { get; private set; }
    /// <summary>
    /// Набор параметров имеющихся в составе метаинформации 
    /// </summary>
    public JsonNode? Params { get; private set; }



    /// <summary>
    /// Функция чтения метаинформации из потока.
    /// </summary>
    /// <param name="stream">Поток данных</param>
    /// <param name="metaInfoSize">Размер, байт</param>
    public static HbmMetaInfo  GetMeta(MemoryStream stream, int metaInfoSize)
    {
        //  Создание объекта
        HbmMetaInfo meta = new();

        //  Создание читателя.
        using BinaryReader reader = new(stream, Encoding.UTF8,true);

        //  Чтение типа.
        int metaTypeBig =  reader.ReadInt32();

        //  Расчет размера данных.
        int dataSize = metaInfoSize - sizeof(int);

        //  Преоборазование типа в сетевой порядок байт
        meta.Type = (MetaInfoType)IPAddress.NetworkToHostOrder(metaTypeBig);

        //  Проверка что тип JSON
        if (meta.Type == MetaInfoType.JSON)
        {
            // Читаем непосредственно метаинформацию в формате JSON
            var bytes = reader.ReadBytes((int)dataSize);

            //  Преобразование строки из кодировки ANSI
            string jsonStr = Encoding.ASCII.GetString(bytes);

            //  Получение контента JSON.
            meta.JsonContent = JsonNode.Parse(jsonStr);

            //  Проверка что контент имеется.
            if (meta.JsonContent is not null)
            {
                //  Получение метода
                meta.Method = meta.JsonContent["method"];

                //  Получение параметров.
                meta.Params = meta.JsonContent["params"];
            }
        }
        else
        {
            //  Получение бинарной Meta 
            meta.BinaryContent = reader.ReadBytes((int)dataSize);
        }

        //  Возврат значения.
        return meta;
    }
}
