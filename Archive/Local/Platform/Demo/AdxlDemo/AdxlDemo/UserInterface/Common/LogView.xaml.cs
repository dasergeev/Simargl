using Apeiron.Platform.Demo.AdxlDemo.Logging;
using System.Collections.ObjectModel;

namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет просмотр журнала.
/// </summary>
public partial class LogView :
    UserControl
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public LogView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Получение списка сообщений.
        ObservableCollection<LogMessage> logMessages = ((App)Application.Current).UserInterfaceData.LogMessages;

        //  Привязка коллекции сообщений к списку.
        _ListView.ItemsSource = logMessages;

        //  Обновление списка.
        logMessages.CollectionChanged += (sender, e) =>
        {
            //  Прокрутка к последнему элементу.
            _ListView.ScrollIntoView(_ListView.Items[^1]);
            _ListView.UpdateLayout();
        };
    }

    /// <summary>
    /// Обрабатывает событие изменения размера списка.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        //  Проверка объекта, создавшего событие.
        if (sender is ListView listView)
        {
            //  Обновление ширины столбцов.
            UpdateColumnsWidth(listView);
        }
    }

    /// <summary>
    /// Обрабатывает событие изменения загрузки списка.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void ListView_Loaded(object sender, RoutedEventArgs e)
    {
        //  Проверка объекта, создавшего событие.
        if (sender is ListView listView)
        {
            //  Обновление ширины столбцов.
            UpdateColumnsWidth(listView);
        }
    }

    /// <summary>
    /// Выполняет обновление ширины столбцов.
    /// </summary>
    /// <param name="listView">
    /// Список, столбцы которого необходимо обновить.
    /// </param>
    private static void UpdateColumnsWidth(ListView listView)
    {
        //  Получение сектки.
        if (listView.View is GridView grid)
        {
            //  Определение индекса последнего столбца.
            int lastIndex = grid.Columns.Count - 1;

            //  Проверка атуальной ширины списка.
            if (double.IsNaN(listView.ActualWidth))
            {
                //  Корректировка ширины списка.
                listView.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }

            //  Определение доступного пространства.
            double remaining = listView.ActualWidth - 24;

            //  Перебор столбцов.
            for (int i = 0; i < lastIndex; i++)
            {
                //  Учёт ширины предыдущего столбца.
                remaining -= grid.Columns[i].ActualWidth;
            }

            //  Установка ширины последнего столбца.
            grid.Columns[lastIndex].Width = remaining >= 0 ? remaining : 0;
        }
    }
}
