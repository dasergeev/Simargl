using Apeiron.Platform.Communication.Elements;
using Apeiron.Platform.Communication.Remoting;
using Apeiron.Support;
using System.Collections.ObjectModel;

namespace Apeiron.Platform.Communication;

/// <summary>
/// Представляет коллекцию сообщений.
/// </summary>
public sealed class MessageCollection :
    ElementCollection<Message>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="communicator">
    /// Коммуникатор с серверным узлом.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    internal MessageCollection(Communicator communicator) :
        base(communicator)
    {

    }

    /// <summary>
    /// Возвращает последнее сообщение в коллекции или пустую ссылку.
    /// </summary>
    /// <returns>
    /// Последнее сообщение в коллекции или пустая ссылка.
    /// </returns>
    public Message? LastOrDefault()
    {
        //  Последнее сообщение.
        Message? lastMessageOrDefault = null;

        //  Выполнение в основном потоке.
        //Provider.PrimaryInvoke(messages => lastMessageOrDefault = messages.Count > 0 ? messages[^1] : null);

        Provider.PrimaryInvoke(messages =>
        {
            if (messages.Count > 0)
            {
                lastMessageOrDefault = messages[^1];
            }
            else
            {
                lastMessageOrDefault = null;
            }
        });

        //  Возврат последнего сообщения.
        return lastMessageOrDefault;
    }

    /// <summary>
    /// Возвращает массив идентификаторов всех сообщений.
    /// </summary>
    /// <returns>
    /// Массив идентификаторов всех сообщений.
    /// </returns>
    internal long[] GetAllIDs()
    {
        //  Массив идентификаторов.
        long[] ids = Array.Empty<long>();

        //  Выполнение действие над хранилищем элементов в основном потоке.
        Provider.PrimaryInvoke(messages =>
        {
            //  Получение массива идентификаторов.
            ids = messages.Select(message => message.ID).ToArray();
        });

        //  Возврат массива идентификаторов.
        return ids;
    }

    /// <summary>
    /// Добавляет сообщение в коллекцию.
    /// </summary>
    /// <param name="message">
    /// Сообщение, добавляемое в коллекцию.
    /// </param>
    internal void Add(Message message)
    {
        //  Выполнение действие над хранилищем элементов в основном потоке.
        Provider.PrimaryInvoke(messages =>
        {
            //  Добавление сообщения.
            AddCore(messages, message);
        });
    }

    /// <summary>
    /// Добавляет сообщение в коллекцию, если коллекция не содержит данное сообщение.
    /// </summary>
    /// <param name="message">
    /// Сообщение, добавляемое в коллекцию.
    /// </param>
    internal void Update(Message message)
    {
        //  Выполнение действие над хранилищем элементов в основном потоке.
        Provider.PrimaryInvoke(messages =>
        {
            //  Перебор сообщений в коллекции.
            foreach (var item in messages)
            {
                //  Проверка идентификаторов.
                if (item.ID == message.ID)
                {
                    //  Сообщение не требуется добавлять.
                    return;
                }
            }
            
            //  Добавление сообщения.
            AddCore(messages, message);
        });
    }


    /// <summary>
    /// Добавляет сообщение в коллекцию.
    /// </summary>
    /// <param name="messages">
    /// Коллекция.
    /// </param>
    /// <param name="message">
    /// Добавляемое сообщение.
    /// </param>
    private static void AddCore(
        [ParameterNoChecks] ObservableCollection<Message> messages,
        [ParameterNoChecks] Message message)
    {
        //  Проверка количества элементов.
        if (messages.Count == 0)
        {
            //  Добавление сообщения в коллекцию.
            messages.Add(message);

            //  Завершение работы с коллекцией.
            return;
        }

        //  Проверка возможности вставки в конец.
        if (messages[^1].RegistrationTime <= message.RegistrationTime)
        {
            //  Добавление сообщения в коллекцию.
            messages.Add(message);

            //  Завершение работы с коллекцией.
            return;
        }

        //  Проверка возможности вставки в начало.
        if (messages[0].RegistrationTime >= message.RegistrationTime)
        {
            //  Добавление сообщения в коллекцию.
            messages.Insert(0, message);

            //  Завершение работы с коллекцией.
            return;
        }

        //  Поиск места вставки.
        for (int i = 1; i < messages.Count; i++)
        {
            //  Проверка возможности вставки.
            if (messages[i - 1].RegistrationTime <= message.RegistrationTime &&
                messages[i].RegistrationTime >= message.RegistrationTime)
            {
                //  Добавление сообщения в коллекцию.
                messages.Insert(i, message);

                //  Завершение работы с коллекцией.
                return;
            }
        }
    }
}
