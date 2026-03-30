//using Simargl.Platform.Journals;
//using Simargl.Concurrent;
//using Simargl.Designing;
//using System;
//using System.Threading.Tasks;
//using System.Threading;
//using System.Collections.Generic;
//using Simargl.Support;
//using static Simargl.Designing.Verify;

//namespace Simargl.Platform.Performers;

///// <summary>
///// Представляет исполнителя.
///// </summary>
//public abstract class Performer
//{
//    /// <summary>
//    /// Постоянная, определяющая задержку в миллисекундах по умолчанию перед повторным вызовом действия.
//    /// </summary>
//    public const int SafeDelay = 1000;

//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    /// <param name="journal">
//    /// Журнал.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="journal"/> передана пустая ссылка.
//    /// </exception>
//    public Performer(Journal journal)
//    {
//        //  Установка журнала.
//        Journal = IsNotNull(journal, nameof(journal));
//    }

//    /// <summary>
//    /// Возвращает журнал.
//    /// </summary>
//    protected Journal Journal { get; }

//    /// <summary>
//    /// Асинхронно выполняет работу исполнителя.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая работу исполнителя.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    public abstract Task PerformAsync(CancellationToken cancellationToken);

//    /// <summary>
//    /// Асинхронно выполняет поддержку выполнения действия.
//    /// </summary>
//    /// <param name="action">
//    /// Действие, которое необходимо поддерживать.
//    /// </param>
//    /// <param name="delay">
//    /// Время задержки перед повторным вызовом действия.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая поддержку выполнения действия.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    protected async ValueTask KeepAsync(
//        [NoVerify] AsyncAction action,
//        [NoVerify] int delay, CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//        //  Цикл поддержки работы.
//        while (!cancellationToken.IsCancellationRequested)
//        {
//            //  Безопасное выполнение.
//            await SafeCallAsync(action, cancellationToken).ConfigureAwait(false);

//            //  Ожидание перед повторным вызовом.
//            await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
//        }
//    }

//    /// <summary>
//    /// Асинхронно выполняет поддержку выполнения действия.
//    /// </summary>
//    /// <param name="action">
//    /// Действие, которое необходимо поддерживать.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая поддержку выполнения действия.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    protected async ValueTask KeepAsync(
//        [NoVerify] AsyncAction action, CancellationToken cancellationToken)
//    {
//        //  Поддержка выполнения действия.
//        await KeepAsync(action, SafeDelay, cancellationToken).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Асинхронно выполняет поддержку выполнения коллекции действий.
//    /// </summary>
//    /// <param name="actions">
//    /// Коллекция дейсвтий, которые необходимо поддерживать.
//    /// </param>
//    /// <param name="delay">
//    /// Время задержки перед повторным вызовом действия.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая поддержку выполнения коллекции действий.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    protected async ValueTask KeepAsync(
//        [NoVerify] IEnumerable<AsyncAction> actions,
//        [NoVerify] int delay, CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//        //  Создание списка задач.
//        List<Task> tasks = new();

//        //  Перебор всех действий.
//        foreach (AsyncAction action in actions)
//        {
//            //  Создание новой задачи.
//            tasks.Add(Task.Run(async delegate
//            {
//                //  Проверка токена отмены.
//                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//                //  Асинхронная поддержка действия.
//                await KeepAsync(action, delay, cancellationToken).ConfigureAwait(false);
//            }, cancellationToken));
//        }

//        //  Ожидание всех задач.
//        await Task.WhenAll(tasks).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Асинхронно выполняет поддержку выполнения коллекции действий.
//    /// </summary>
//    /// <param name="actions">
//    /// Коллекция дейсвтий, которые необходимо поддерживать.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая поддержку выполнения коллекции действий.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    protected async ValueTask KeepAsync(
//        [NoVerify] IEnumerable<AsyncAction> actions, CancellationToken cancellationToken)
//    {
//        //  Выполнение поддержки выполнения коллекции действий.
//        await KeepAsync(actions, SafeDelay, cancellationToken).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Асинхронно выполняет безопасный вызов действия.
//    /// </summary>
//    /// <param name="action">
//    /// Действие, которое необходимо выполнить безопасно.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая безопасный вызов.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    protected async ValueTask SafeCallAsync(
//        [NoVerify] AsyncAction action, CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//        //  Блок перехвата некритических исключений.
//        try
//        {
//            //  Асинхронный вызов действия.
//            await action(cancellationToken).ConfigureAwait(false);
//        }
//        catch (Exception ex)
//        {
//            //  Проверка критического исключения.
//            if (ex.IsCritical())
//            {
//                //  Повторный выброс исключения.
//                throw;
//            }

//            //  Проверка токена отмены.
//            if (!cancellationToken.IsCancellationRequested)
//            {
//                //  Вывод информации об ошибке в журнал.
//                await Journal.LogErrorAsync(ex.ToString(), cancellationToken).ConfigureAwait(false);
//            }
//        }
//    }
//}
