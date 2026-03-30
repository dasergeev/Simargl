using Simargl.Concurrent;
using System.Runtime.CompilerServices;

namespace Simargl.Journaling;

/// <summary>
/// Представляет журнал.
/// </summary>
public sealed class Journal
{
    /// <summary>
    /// Возвращает журнал по умолчанию.
    /// </summary>
    public static Journal Default { get; } = new();

    /// <summary>
    /// Поле для хранения критического объекта, используемого для синхронизации доступа.
    /// </summary>
    private readonly AsyncLock _Lock = new();

    /// <summary>
    /// Поле для хранения списка поставщиков.
    /// </summary>
    private List<JournalProvider> _Providers = [];

    /// <summary>
    /// Добавляет запись в журнал.
    /// </summary>
    /// <param name="text">
    /// Текст записи.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    public void Add(string text)
    {
        //  Добавление записи в журнал.
        Add(text, JournalRecordLevel.Information);
    }

    /// <summary>
    /// Добавляет запись в журнал.
    /// </summary>
    /// <param name="text">
    /// Текст записи.
    /// </param>
    /// <param name="level">
    /// Значение, определяющее уровень записи в журнале.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="level"/> передано значение,
    /// которое не содержится в перечислении <see cref="JournalRecordLevel"/>.
    /// </exception>
    public void Add(string text, JournalRecordLevel level)
    {
        //  Создание записи.
        JournalRecord record = new(text, level, DateTime.Now);

        //  Отправка записи.
        Send(record);
    }

    /// <summary>
    /// Добавляет запись в журнал.
    /// </summary>
    /// <param name="record">
    /// Запись, добавляемая в журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="record"/> передана пустая ссылка.
    /// </exception>
    public void Add(JournalRecord record)
    {
        //  Проверка записи.
        IsNotNull(record);

        //  Отправка записи.
        Send(record);
    }

    /// <summary>
    /// Отправляет запись в журнал.
    /// </summary>
    /// <param name="record">
    /// Запись, отправляемая в журнал.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Send([NoVerify] JournalRecord record)
    {
        //  Асинхронное выполнение.
        _ = Task.Run(async delegate
        {
            //  Получение списка поставщиков.
            List<JournalProvider> providers = Volatile.Read(ref _Providers);

            //  Перебор списка поставщиков.
            foreach (JournalProvider provider in providers)
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Подавление некритических исключений.
                    await DefyCriticalAsync(async delegate (CancellationToken cancellationToken)
                    {
                        //  Отправка записи.
                        await provider.SendAsync(record, cancellationToken).ConfigureAwait(false);
                    }, CancellationToken.None);
                }
                catch { }
            }
        });
    }

    /// <summary>
    /// Асинхронно прикрепляет поставщика журнала.
    /// </summary>
    /// <param name="provider">
    /// Прикрепляемый поставщик жкрнала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, прикрепляющая поставщика журнала.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="provider"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task AttachAsync(JournalProvider provider, CancellationToken cancellationToken)
    {
        //  Проверка поставщика журнала.
        IsNotNull(provider);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Блокировка критического объекта.
        using (await _Lock.LockAsync(cancellationToken))
        {
            //  Получение списка поставщиков.
            List<JournalProvider> providers = Volatile.Read(ref _Providers);

            //  Создание копии списка поставщиков.
            providers = new(providers)
            {
                //  Добавление поставщика.
                provider
            };

            //  Замена списка.
            Interlocked.Exchange(ref _Providers, providers);
        }
    }

    /// <summary>
    /// Асинхронно открепляет поставщика журнала.
    /// </summary>
    /// <param name="provider">
    /// Открепляемый поставщик жкрнала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, открепляющая поставщика журнала.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="provider"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task DetachAsync(JournalProvider provider, CancellationToken cancellationToken)
    {
        //  Проверка поставщика журнала.
        IsNotNull(provider);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Блокировка критического объекта.
        using (await _Lock.LockAsync(cancellationToken))
        {
            //  Получение списка поставщиков.
            List<JournalProvider> providers = Volatile.Read(ref _Providers);

            //  Создание копии списка поставщиков.
            providers = new(providers);

            //  Удаление поставщика.
            providers.Remove(provider);

            //  Замена списка.
            Interlocked.Exchange(ref _Providers, providers);
        }
    }
}
