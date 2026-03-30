using Simargl.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Simargl.Border.Schematic;

/// <summary>
/// Представляет преобразователь IPv4-адреса.
/// </summary>
internal sealed class IPv4AddressJsonConverter :
    JsonConverter<IPv4Address>
{
    /// <inheritdoc/>
    public override IPv4Address Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        //  Получение значения.
        string? value = reader.GetString();

        //  Проверка значения.
        if (!IPv4Address.TryParse(value, out IPv4Address address))
        {
            //  Выброс исключения.
            throw new JsonException($"Некорректный IPv4-адрес: {value}");
        }

        //  Возврат значения.
        return address;
    }

    /// <inheritdoc/>
    public override void Write(
        Utf8JsonWriter writer,
        IPv4Address value,
        JsonSerializerOptions options)
    {
        //  Запись значения.
        writer.WriteStringValue(value.ToString());
    }
}
