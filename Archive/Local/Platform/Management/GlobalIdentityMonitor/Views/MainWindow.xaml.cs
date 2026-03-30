using System.Windows.Controls;

namespace Apeiron.Services.GlobalIdentity;

/// <summary>
/// Класс описывающий логику работы основного окна приложения MainWindow.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Конструктор. Инициализирует окно приложения.
    /// </summary>
    public MainWindow()
    {
        // Инициализирует окно.
        InitializeComponent();
    }

    /// <summary>
    /// Отображает нумерированный список номеров строк в первой колонке элемента DataGrid.
    /// </summary>
    private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
    {
        e.Row.Header = (e.Row.GetIndex() + 1).ToString();
    }
}

