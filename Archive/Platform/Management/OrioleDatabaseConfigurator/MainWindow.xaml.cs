using Apeiron.Platform.Databases.OrioleDatabase;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Apeiron.Oriole.DatabaseConfigurator;

/// <summary>
/// Класс описывающий логику работы основнога окна MainWindow.xaml приложения.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Содержит контекст базы данных.
    /// </summary>
    private readonly OrioleDatabaseContext _DBContext;

    /// <summary>
    /// Содержит список всех DataGrid.
    /// </summary>
    private readonly List<DataGrid> _DataGridsList;

    /// <summary>
    /// Конструктор. Инициализирует окно приложения.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();

        // Получаем настройки и создаём DBContext.
        _DBContext = new OrioleDatabaseContext(/*DatabaseContentConfig.DbContextOptionsBuilder.Options*/);

        //Формируем список таблиц DataGrid для работы.
        _DataGridsList = new()
        {
            SensorsDataGrid,
            RegistrarsDataGrid,
            RawDirectoriesDataGrid,
            RecordDirectoriesDataGrid,
            ChannelsDataGrid,
            SourcesDataGrid
        };
    }


    #region Работа интерфейса программы.

    /// <summary>
    /// Обработчик события загрузки окна.
    /// </summary>
    /// <param name="sender">Объект создавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void DatabaseConfigurator_Loaded(object sender, RoutedEventArgs e)
    {
        //// Создаём новый TabControl
        //MainTabControl.Items.Add(new TabItem
        //{
        //    Header = new TextBlock { Text = "Rowdirectories" },
        //    Content = RecordDirectoriesEntityControl
        //});
    }


    /// <summary>
    /// Обработчик события загрузки данных.
    /// </summary>
    /// <param name="sender">Объект создавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
    {
        foreach (var itemDataGrid in _DataGridsList)
        {
            // Удаляем привязки.
            BindingOperations.ClearAllBindings(itemDataGrid);
            // Очищаем таблицы.
            itemDataGrid.Items.Clear();
        }

        try
        {
            // Подгружаем данные таблиц.
            _DBContext.Registrars.Load();
            _DBContext.Channels.Load();
            _DBContext.RawDirectories.Load();
            _DBContext.RecordDirectories.Load();
            _DBContext.Sensors.Load();
            _DBContext.Sources.Load();

            SensorsDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Source = _DBContext.Sensors.Local.ToObservableCollection()
            });

            RegistrarsDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Source = _DBContext.Registrars.Local.ToObservableCollection()
            });

            RawDirectoriesDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Source = _DBContext.RawDirectories.Local.ToObservableCollection()
            });

            RecordDirectoriesDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Source = _DBContext.RecordDirectories.Local.ToObservableCollection()
            });

            ChannelsDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Source = _DBContext.Channels.Local.ToObservableCollection()
            });

            SourcesDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Source = _DBContext.Sources.Local.ToObservableCollection()
            });
        }
        catch (Exception ex) when (ex is System.Data.SqlClient.SqlException || ex is Microsoft.Data.SqlClient.SqlException)
        {
            MessageBox.Show($"Ошибка подключения к базе данных!\n\n\r{ex.Message}", "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);
        }
          
    }

    /// <summary>
    /// Обработчик пункта меню - Сохранить данные.
    /// </summary>
    /// <param name="sender">Объект создавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
    {
        // Сохраняем все DataGrid
        foreach (var item in _DataGridsList)
        {
            // Проверяем наличие привязки.
            BindingExpression expression = BindingOperations.GetBindingExpression(item, DataGrid.ItemsSourceProperty);

            if (expression is null)
                throw new InvalidOperationException($"{nameof(expression)} - пустое значение.");

            // Обновляем источник.
            expression.UpdateSource();
        }

        // Сохраняем данные.
        _DBContext.SaveChanges();
    }

    /// <summary>
    /// Отображает версию в строке состояния.
    /// </summary>
    /// <param name="sender">Объект создавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void FooterStatusBar_Loaded(object sender, RoutedEventArgs e)
    {
        ProgramVersion.Text = $"Ver. {Assembly.GetExecutingAssembly().GetName().Version}";
        DBOnline.Text = $"БД: {_DBContext.GetBaseName()}";
    }

    /// <summary>
    /// Обработчик события при закрытии формы.
    /// </summary>
    /// <param name="sender">Объект создавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void DatabaseConfigurator_Closed(object sender, EventArgs e)
    {
        // Разрушаем объект работы с БД.
        //databaseConfigViewModel.Dispose();
        _DBContext?.Dispose();
    }

    /// <summary>
    /// Обработчик меню "Закрыть"
    /// </summary>
    /// <param name="sender">Объект создавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
    {
        _DBContext?.Dispose();
        Close();
    }
    #endregion
}
