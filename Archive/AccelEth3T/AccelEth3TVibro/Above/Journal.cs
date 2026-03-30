using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет средство ведения журнала.
/// </summary>
/// <param name="core">
/// Ядро.
/// </param>
/// <param name="richTextBox">
/// Элемент управления, отображающий текст.
/// </param>
public sealed class Journal(Core core, RichTextBox richTextBox) :
    Worker(core)
{
    /// <summary>
    /// Поле для хранения очереди строк.
    /// </summary>
    private readonly ConcurrentQueue<string> _Queue = [];

    /// <summary>
    /// Поле для хранения строк журнала.
    /// </summary>
    private readonly List<string> _Lines = [];

    /// <summary>
    /// Добавляет сообщение в журнал.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо добавить в журнал.
    /// </param>
    public void Add(string message)
    {
        //  Проверка сообщения.
        IsNotNull(message);

        //  Добавление сообщения в очередь.
        _Queue.Enqueue(message);
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
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Основный цикл вывода информации в журнал.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Флаг обновления.
            bool isUpdate = false;

            //  Извлечение сообщений из очереди.
            while (_Queue.TryDequeue(out string? message))
            {
                //  Установка флага обновления.
                isUpdate = true;

                //  Добавление сообщения в журнал.
                _Lines.Insert(0, message);
            }

            //  Обрезка журнала.
            while (_Lines.Count > 1000)
            {
                //  Удаление последней строки.
                _Lines.RemoveAt(_Lines.Count - 1);
            }

            //  Проверка необходимости обновления.
            if (isUpdate)
            {
                //  Создание построителя строки.
                StringBuilder builder = new();

                //  Перебор строк журнала.
                foreach (string line in _Lines)
                {
                    //  Добавление строки.
                    builder.AppendLine(line);
                }

                //  Получение нового значения текста.
                string text = builder.ToString();

                //  Выполнение в основном потоке.
                await Core.Invoker.InvokeAsync(delegate
                {
                    //  Установка текста элемента управления.
                    richTextBox.Text = text;
                }, cancellationToken).ConfigureAwait(false);
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }
    }
}
