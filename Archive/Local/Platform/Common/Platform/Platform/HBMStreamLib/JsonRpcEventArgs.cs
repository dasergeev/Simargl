using System.Text.Json.Nodes;

namespace Apeiron.QuantumX;

/// <summary>
/// Представляет класс передачи параметров протокола JsonRpc
/// </summary>
public class JsonRpcEventArgs : EventArgs
{
    /// <summary>
    /// Возвращает строку метода.
    /// </summary>
    public string Method { get; }

    /// <summary>
    /// Возвращает строку тела сообщения.
    /// </summary>
    public JsonNode Params { get; }

    /// <summary>
    /// Инициализирует объект класса.
    /// </summary>
    /// <param name="method">Метод</param>
    /// <param name="parameters">Запись</param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="method"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="parameters"/> передана пустая ссылка.
    /// </exception>
    public JsonRpcEventArgs(string method, JsonNode parameters)
    {
        //  Установка метода
        Method = Check.IsNotNull(method, nameof(method));

        //  Установка записи.
        Params = Check.IsNotNull(parameters,nameof(parameters));
    }
}
