using Simargl.Hardware.Strain.Demo.Journaling;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Simargl.Hardware.Strain.Demo.UI;

/// <summary>
/// Представляет элемент управления, отображающий журнал.
/// </summary>
partial class JournalView
{
    /// <summary>
    /// Постоянная, определяющая количество отображаемых записей в журнале.
    /// </summary>
    private const int _JournalSize = 1024;

    /// <summary>
    /// Поле для хранения очереди сообщений.
    /// </summary>
    private readonly ConcurrentQueue<JournalRecord> _Queue;

    /// <summary>
    /// Поле для хранения коллекции отображаемых записей журнала.
    /// </summary>
    private readonly ObservableCollection<JournalRecord> _Collection;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public JournalView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Получение очереди сообщений.
        _Queue = IsInDesignMode ? [] : Application.JournalRecords;

        //  Создание коллекции отображаемых записей журнала.
        _Collection = [];

        //  Привязка списка записей.
        _ListView.ItemsSource = _Collection;

        //  Проверка режима разработки.
        if (!IsInDesignMode)
        {
            //  Добавление основной задачи в механизм поддержки.
            Application.Keeper.Add(InvokeAsync);
        }
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл выполнения.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Проверка очереди.
            if (!_Queue.IsEmpty)
            {
                //  Создание списка записей.
                List<JournalRecord> records = [];

                //  Разбор очереди записей.
                while (!cancellationToken.IsCancellationRequested &&
                    _Queue.TryDequeue(out JournalRecord? record))
                {
                    //  Проверка записи.
                    if (record is not null)
                    {
                        //  Добавление записи в список.
                        records.Add(record);
                    }
                }

                //  Проверка размера списка.
                if (records.Count > _JournalSize)
                {
                    //  Корректировка размера списка.
                    records.RemoveRange(_JournalSize, records.Count - _JournalSize);
                }

                //  Определение начального размера списка.
                int initialSize = _JournalSize - records.Count;

                //  Выполнение в основном потоке.
                await Application.Invoker.InvokeAsync(async delegate (CancellationToken cancellationToken)
                {
                    //  Обрезка коллекции записей.
                    while (_Collection.Count > initialSize)
                    {
                        //  Удаление последнего элемента из списка.
                        _Collection.RemoveAt(_Collection.Count - 1);

                        //  Ожидание завершённой задачи.
                        await ValueTask.CompletedTask.ConfigureAwait(true);
                    }

                    //  Перебор новых записей.
                    foreach (JournalRecord record in records)
                    {
                        //  Вставка новой записи.
                        _Collection.Insert(0, record);

                        //  Ожидание завершённой задачи.
                        await ValueTask.CompletedTask.ConfigureAwait(true);
                    }
                }, cancellationToken).ConfigureAwait(false);
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(500, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Происходит при изменении размера списка.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_ListView.View is GridView gv && gv.Columns.Count >= 3)
        {
            double total = 0;
            for (int i = 0; i < gv.Columns.Count - 1; i++)
            {
                total += gv.Columns[i].Width;
            }

            double remaining = _ListView.ActualWidth - total - 35; // учесть scroll + margin

            if (remaining > 0)
                gv.Columns[gv.Columns.Count - 1].Width = remaining;
        }
    }
}
