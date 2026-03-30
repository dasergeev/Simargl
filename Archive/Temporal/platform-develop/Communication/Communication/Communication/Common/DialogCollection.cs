using Apeiron.Platform.Communication.Elements;
using Apeiron.Platform.Communication.Remoting;

namespace Apeiron.Platform.Communication;

/// <summary>
/// Представляет коллекцию диалогов.
/// </summary>
public sealed class DialogCollection :
    ElementCollection<Dialog>
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
    internal DialogCollection(Communicator communicator) :
        base(communicator)
    {

    }

    /// <summary>
    /// Асинхронно обновляет коллекцию.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обновление.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        //  Обновление коллекции с параметрами по умолчанию.
        await UpdateAsync(default, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно обновляет коллекцию.
    /// </summary>
    /// <param name="options">
    /// Параметры удалённого вызова.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обновление.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task UpdateAsync(RemoteInvokeOptions options, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос пользователей.
        User[] users = await RemoteInvoker.RequestAllUsersAsync(options, cancellationToken).ConfigureAwait(false);

        //  Выполнение действие над хранилищем элементов в основном потоке.
        Provider.PrimaryInvoke(dialogs =>
        {
            //  Создание словаря диалогов.
            SortedDictionary<string, Dialog> map = new();

            //  Перебор элементов коллекции.
            foreach (Dialog dialog in dialogs)
            {
                //  Добавление диалога в словарь.
                map.Add(dialog.Companion.Name, dialog);
            }

            //  Перебор пользователей.
            foreach (User user in users)
            {
                //  Проверка текущего пользователя.
                if (user.Name != ConnectionOptions.Login)
                {
                    //  Проверка диалога.
                    if (map.TryGetValue(user.Name, out Dialog? dialog))
                    {
                        dialog.Companion.ID = user.ID;
                    }
                    else
                    {
                        //  Создание нового диалога.
                        dialog = new(Communicator, user);

                        //  Добавление диалога в список.
                        dialogs.Add(dialog);

                        //  Запуск асинхронной задачи.
                        _ = Task.Run(async delegate
                        {
                            //  Обновление диалога.
                            await dialog.UpdateAsync(options, cancellationToken);
                        });
                    }
                }
            }
        });
    }
}
