using System.Text.Json.Nodes;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System;

namespace Simargl.QuantumX;

/// <summary>
/// Класс реализующий механизм подписки/отписки на канал(ы) информационного потока устройства QuantumX
/// </summary>
internal class Subscriber
{
    /// <summary>
    /// Идентификатор потока(устройства)
    /// </summary>
    private readonly string StreamId;

    /// <summary>
    /// IP адрес устройства
    /// </summary>
    private readonly string IPAddress;

    /// <summary>
    /// Путь http 
    /// </summary>
    private readonly string CtrlHttpPath;

    /// <summary>
    /// Представляет идентификатор отправки.
    /// </summary>
    private static uint _SessionIdentifier;

    /// <summary>
    /// Инциализирует объект класса.
    /// </summary>
    /// <param name="streamId">Идентификатор потока</param>
    /// <param name="address"></param>
    /// <param name="ctrlHttpPath"></param>
    public Subscriber(string streamId, string address,string ctrlHttpPath)
    {
        StreamId = streamId;
        IPAddress = address;
        CtrlHttpPath = ctrlHttpPath;
    }
    /// <summary>
    /// Функция подписки на группу сигналов.
    /// </summary>
    /// <param name="signalReferences">Список сигналов.</param>
    /// <param name="token">Токен отмены</param>
    public async Task SubscribeAsync(List<string> signalReferences, CancellationToken token)
    {
        //  Установка адреса.
        string addr = "http://" + IPAddress + CtrlHttpPath;

        //  Создание строителя строки.
        StringBuilder data = new();

        //  Создание префикса строки.
        data.Append("{\"id\":" + $"{++_SessionIdentifier}," +
            "\"jsonrpc\":\"2.0\"," +
            $"\"method\":\"{StreamId}.subscribe\"," +
            "\"params\":[");

        //  Цикл по всем каналов
        for (int i = 0; i < signalReferences.Count; i++)
        {
            //  Проверка первой итерации цикла
            if (i != 0)
            {
                //  Добавление символа
                data.Append(',');
            }
            //  Добавление имени канала
            data.Append("\""+ signalReferences[i] + "\"");
        }
        //  Завершение строки
        data.Append("]}");


        //  Инциализация контента клиента http
        var httpContent = new StringContent($"{data}\n", Encoding.UTF8, "application/json");

        //  Создание клиента.
        using var  сlient = new HttpClient();

        //  Отправка запроса.
        using (HttpResponseMessage response = await сlient.PostAsync(addr, httpContent,token))
        {
            // Выбросит исключение если запрос неудачен
            response.EnsureSuccessStatusCode();

            // Читаем содержимое ответа
            string responseBody = await response.Content.ReadAsStringAsync(token);

            //  Разбор ответа.
            JsonNode? JSONContent = JsonNode.Parse(responseBody);

            // Если не удалось распарсить ответ или в ответе пришла ошибка
            if (JSONContent == null)
            {
                //  Отправка сообщения об ошибке
                Console.WriteLine("Subscribe error");

                //  Возврат из функции
                return;
            }
            else if (JSONContent["error"] != null)
            {
                //  Отправка сообщения об ошибке
                Console.WriteLine("Subscribe error");
             
                //  Отправка сообщения об ошибке
                Console.WriteLine(JSONContent["error"]);

                //  Возврат из функции
                return;
            }
        }

        //  Сообщение об успехе
        Console.WriteLine("Subscribe Ok");
    }
    /// <summary>
    /// Функция отписки от группы сигналов.
    /// </summary>
    /// <param name="signalReferences">Список сигналов.</param>
    /// <param name="token">Токен отмены</param>
    public async Task UnSubscribeAsync(List<string> signalReferences,CancellationToken token)
    {
        //  Установка адреса.
        string addr = "http://" + IPAddress + CtrlHttpPath;

        //  Создание строителя строки.
        StringBuilder data = new();

        //  Создание префикса строки.
        data.Append("{\"id\":" + $"{++_SessionIdentifier}," +
            "\"jsonrpc\":\"2.0\"," +
            $"\"method\":\"{StreamId}.unsubscribe\"," +
            "\"params\":[");

        //  Цикл по всем каналов
        for (int i = 0; i < signalReferences.Count; i++)
        {
            //  Проверка первой итерации цикла
            if (i != 0)
            {
                //  Добавление символа
                data.Append(',');
            }
            //  Добавление имени канала
            data.Append("\"" + signalReferences[i] + "\"");
        }
        //  Завершение строки
        data.Append("]}");

        //  Инциализация контента клиента http
        var httpContent = new StringContent($"{data}\n", Encoding.UTF8, "application/json");

        //  Создание клиента.
        using var сlient = new HttpClient();

        //  Отправка запроса.
        HttpResponseMessage response = await сlient.PostAsync(addr, httpContent, token);
        
        // Выбросит исключение если запрос неудачен
        response.EnsureSuccessStatusCode();

        // Читаем содержимое ответа
        string responseBody = await response.Content.ReadAsStringAsync(token);

        //  Получение параметров ответа.
        JsonNode? JSONContent = JsonNode.Parse(responseBody);

        // Если не удалось распарсить ответ или в ответе пришла ошибка
        if(JSONContent == null)
        {
            //  Сообщение об ошибке
            Console.WriteLine("Unsubscribe error");

            //  Возврат из функции.
            return;
        }
        else if(JSONContent["error"] != null)
        {
            //  Сообщение об ошибке
            Console.WriteLine("Unsubscribe error");

            //  Сообщение об ошибке
            Console.WriteLine(JSONContent["error"]);

            //  Возврат из функции.
            return;
        }

        //  Сообщение об успехе
        Console.WriteLine("Unsubscribe Ok");
    }

}
