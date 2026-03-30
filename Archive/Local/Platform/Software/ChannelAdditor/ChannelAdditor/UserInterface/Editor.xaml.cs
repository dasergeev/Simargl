using Apeiron.Platform.Software.ChannelAdditor.Projects;
using Ookii.Dialogs.Wpf;
using System.IO;

namespace Apeiron.Platform.Software.ChannelAdditor.UserInterface;

/// <summary>
/// Представляет элемент управления, с помощью которого можно редактировать параметры.
/// </summary>
public partial class Editor :
    UserControl
{
    /// <summary>
    /// Происходит при изменении значения свойства <see cref="SelectedRule"/>.
    /// </summary>
    public event EventHandler? SelectedRuleChanged;
    
    /// <summary>
    /// Поле для хранения прокта.
    /// </summary>
    private Project _Project;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Editor()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Установка значения свойств по умолчанию.
        _Project = null!;

        //  Создание пустого проекта.
        Project = new();
    }

    /// <summary>
    /// Возвращает или задаёт проект.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public Project Project
    {
        get => _Project;
        set
        {
            //  Проверка ссылки на проект.
            Check.IsNotNull(value, nameof(Project));

            //  Установка нового значения.
            _Project = value;

            //  Привязка данных.
            _Grid.DataContext = _Project;
            _ListView.ItemsSource = _Project.Rules;
        }
    }

    /// <summary>
    /// Возвращает выбранное правило.
    /// </summary>
    public Rule? SelectedRule { get; private set; }

    /// <summary>
    /// Обрабатывает событие нажатия кнопки выбора каталога с исходными кадрами.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void SourceButton_Click(object sender, RoutedEventArgs e)
    {
        //  Создание диалога.
        VistaFolderBrowserDialog dialog = new();

        //  Проверка текущего каталога.
        if (Directory.Exists(Project.SourcePath))
        {
            //  Установка текущего каталога.
            dialog.SelectedPath = Project.SourcePath;
        }

        //  Проверка выбора каталога.
        if (dialog.ShowDialog() == true)
        {
            //  Установка выбранного каталога.
            Project.SourcePath = dialog.SelectedPath;
        }
    }

    /// <summary>
    /// Обрабатывает событие нажатия кнопки выбора каталога с исходными кадрами.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TargetButton_Click(object sender, RoutedEventArgs e)
    {
        //  Создание диалога.
        VistaFolderBrowserDialog dialog = new();

        //  Проверка текущего каталога.
        if (Directory.Exists(Project.TargetPath))
        {
            //  Установка текущего каталога.
            dialog.SelectedPath = Project.TargetPath;
        }

        //  Проверка выбора каталога.
        if (dialog.ShowDialog() == true)
        {
            //  Установка выбранного каталога.
            Project.TargetPath = dialog.SelectedPath;
        }
    }

    /// <summary>
    /// Обрабатывает событие изменения выбранного канала.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //  Выбранный канал.
        Rule? selectedRule = null;

        //  Проверка аргументов, связанных с событием.
        if (e is not null && e.AddedItems.Count > 0 && e.AddedItems[0] is Rule rule)
        {
            selectedRule = rule;
        }

        //  Проверка изменения выбранного канала.
        if (!ReferenceEquals(SelectedRule, selectedRule))
        {
            //  Установка нового значения.
            SelectedRule = selectedRule;

            //  Вызов события.
            SelectedRuleChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
