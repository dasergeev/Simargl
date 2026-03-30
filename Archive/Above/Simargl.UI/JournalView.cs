using Simargl.Journaling;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Simargl.UI;

/// <summary>
/// Представляет элемент управления, отображающий журнал.
/// </summary>
public partial class JournalView :
    Control
{
    /// <summary>
    /// Поле для хранения значения, определяющего остановлен ли вывод журнала.
    /// </summary>
    private volatile bool _IsPause;

    /// <summary>
    /// Поле для хранения очереди записей.
    /// </summary>
    private readonly ConcurrentQueue<JournalRecord> _Queue;

    /// <summary>
    /// Поле для хранения поставщика журнала.
    /// </summary>
    private readonly JournalProvider _Provider;

    private readonly ToggleButton _PauseButton;
    private readonly TextBox _TextBox;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public JournalView()
    {
        // Создание Grid
        var grid = new Grid();

        // Добавление строк
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        // Создание ToolBar
        var toolBar = new ToolBar();

        // Создание ToggleButton
        _PauseButton = new ToggleButton
        {
            Name = "_PauseButton"
        };
        _PauseButton.Checked += PauseButton_Checked;
        _PauseButton.Unchecked += PauseButton_Unchecked;

        // Добавление изображения в кнопку
        var image = new Image
        {
            Source = Images.Pause,// new BitmapImage(new Uri("pack://application:,,,/Images/Pause.png")), // Указание пути к изображению
            Height = 16,
            Width = 16
        };

        // Добавление изображения в кнопку
        _PauseButton.Content = image;

        // Добавление ToggleButton в ToolBar
        toolBar.Items.Add(_PauseButton);

        // Создание TextBox
        _TextBox = new TextBox
        {
            Name = "_TextBox",
            BorderThickness = new System.Windows.Thickness(0),
            //ScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible,
            //ScrollViewer.CanContentScroll = true,
            VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
            AutoWordSelection = false,
            IsReadOnly = true,
            TextWrapping = TextWrapping.NoWrap,
            FontFamily = new System.Windows.Media.FontFamily("Consolas")
        };

        Grid.SetRow(_TextBox, 1);
        ScrollViewer.SetHorizontalScrollBarVisibility(_TextBox, ScrollBarVisibility.Visible);
        ScrollViewer.SetCanContentScroll(_TextBox, true);

        // Добавление элементов в Grid
        grid.Children.Add(toolBar);
        grid.Children.Add(_TextBox);

        // Установка Grid как содержимого окна
        this.Content = grid;

        //  Установка значения, определяющего остановлен ли вывод журнала.
        _IsPause = false;

        //  Создание очереди записей.
        _Queue = [];

        //  Создание поставщика журнала.
        _Provider = JournalProvider.Create(_Queue.Enqueue);

        //  Проверка режима разработки.
        if (!IsInDesignMode)
        {
            //  Добавление основной задачи в механизм поддержки.
            Entry.Keeper.Add(InvokeAsync);
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
        //  Прикрепление поставщика журнала.
        await Journal.AttachAsync(_Provider, cancellationToken).ConfigureAwait(false);

        //  Список записей.
        List<string> records = [];

        //  Блок с гарантированным завершением.
        try
        {
            //  Вывод информации в журнал.
            Journal.Add("Начало работы журнала");

            //  Основной цикл выполнения.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Извлечение записей из очереди.
                while (_Queue.TryDequeue(out JournalRecord? record))
                {
                    //  Проверка записи.
                    if (record is not null)
                    {
                        //  Добавление записи в список.
                        records.Add($"[{record.Time:yyyy.MM.dd HH.mm.ss}] {record.Level}: {record.Text}");
                    }
                }

                //  Проверка размера списка.
                while (records.Count > 1000)
                {
                    //  Удаление последней записи.
                    records.RemoveAt(records.Count - 1);
                }

                //  Проверка необходимости вывода.
                if (!_IsPause)
                {
                    //  Создание построителя строки.
                    StringBuilder builder = new();

                    //  Перебор записей.
                    for (int i = records.Count - 1; i >= 0; i--)
                    {
                        //  Добавление записи.
                        builder.AppendLine(records[i]);
                    }

                    //  Получение текста.
                    string text = builder.ToString();

                    //  Выполнение в основном потоке.
                    await Entry.Invoker.InvokeAsync(delegate
                    {
                        //  Установка текста.
                        _TextBox.Text = text;

                        //  Прокрутка в начало элемента управления.
                        _TextBox.ScrollToHome();
                    }, cancellationToken).ConfigureAwait(false);
                }

                //  Ожидание перед следующим проходом.
                await Task.Delay(250, cancellationToken).ConfigureAwait(false);
            }
        }
        finally
        {
            //  Открепление поставщика журнала.
            await Journal.DetachAsync(_Provider, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Обрабатывает событие зажатия кнопки "Пауза".
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void PauseButton_Checked(object sender, System.Windows.RoutedEventArgs e)
    {
        //  Установка значения, определяющего остановлен ли вывод журнала.
        _IsPause = true;
    }

    /// <summary>
    /// Обрабатывает событие отжатия кнопки "Пауза".
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void PauseButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
    {
        //  Установка значения, определяющего остановлен ли вывод журнала.
        _IsPause = false;
    }
}
