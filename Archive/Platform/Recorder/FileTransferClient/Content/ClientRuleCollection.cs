using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Platform.Recorder.FileTransferClient.Content;

/// <summary>
/// Представляет коллекцию правил, выполняющихся на клиенте.
/// </summary>
public class ClientRuleCollection :
    List<ClientRule>
{
    /// <summary>
    /// Асинхронно выполняет работу правил.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <param name="logic">
    /// Логика работы службы.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу правил.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logic"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task InvokeAsync(ILogger logger, ClientLogic logic, CancellationToken cancellationToken)
    {
        //  Проверка средства записи в журнал службы.
        logger = IsNotNull(logger, nameof(logger));

        //  Проверка ссылки на логику.
        logic = IsNotNull(logic, nameof(logic));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание списка задач.
        List<Task> tasks = new();

        //  Перебор всех правил.
        foreach (ClientRule rule in this)
        {
            //  Создание новой задачи.
            tasks.Add(Task.Run(
                async delegate
                {
                    //  Проверка токена отмены.
                    await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                    //  Поддержка правила.
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        //  Блок перехвата исключений для вывода в журнал.
                        try
                        {
                            //  Выполнение правила.
                            await rule.InvokeAsync(logger, logic, cancellationToken).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            //  Вывод информации в журнал.
                            logger.LogError("[{name}] Исключение:\n{exception}",
                                rule.Name, ex);
                        }

                        await Task.Delay(100, cancellationToken).ConfigureAwait(false);
                    }
                }, cancellationToken));
        }

        //  Ожиадние всех задач.
        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет проверку данных.
    /// </summary>
    /// <param name="writer">
    /// Средство записи текстовой информации.
    /// </param>
    /// <param name="level">
    /// Уровень вложенности.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая проверку данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="writer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="level"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Недопустимый формат данных.
    /// </exception>
    public async Task CheckValidityAsync(TextWriter writer, int level, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на средство записи в журнал.
        writer = IsNotNull(writer, nameof(writer));

        //  Проверка уровня вложенности.
        level = IsNotNegative(level, nameof(level));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Формирование отступа.
        string indent = new(' ', 2 * level);

        //  Вывод информации о проверке правил.
        await writer.WriteAsync($"{indent}Правила:\n".AsMemory(), cancellationToken).ConfigureAwait(false);

        //  Вывод информации о количестве правил.
        await writer.WriteAsync($"{indent}  Всего: {Count}\n".AsMemory(), cancellationToken).ConfigureAwait(false);

        //  Перебор всех правил.
        for (int i = 0; i < Count; i++)
        {
            //  Вывод информации о правиле.
            await writer.WriteAsync($"{indent}  Правило[{i}]:\n".AsMemory(), cancellationToken).ConfigureAwait(false);

            //  Проверка правила.
            await this[i].CheckValidityAsync(writer, level + 2, cancellationToken).ConfigureAwait(false);
        }
    }
}
