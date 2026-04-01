using System.Collections; // Подключает неуниверсальный интерфейс перечисления для передачи дефектов.
using System.Windows; // Подключает базовые типы WPF и dependency properties.

namespace Simargl.FlawDetector.Controls.SourceEditors; // Определяет пространство имён контролов редакторов источников.

/// <summary> // Описывает назначение класса.
/// Представляет контрол настройки источника дефектных импульсов. // Уточняет роль контрола в интерфейсе.
/// </summary> // Завершает XML-документацию класса.
public partial class DefectImpulseSignalSourceEditorControl : System.Windows.Controls.UserControl // Объявляет контрол настройки дефектных импульсов.
{ // Начинает тело класса.
    /// <summary> // Документирует dependency property списка дефектов.
    /// Идентифицирует свойство, содержащее коллекцию редактируемых дефектов. // Уточняет назначение статического поля.
    /// </summary> // Завершает XML-документацию поля.
    public static readonly DependencyProperty FaultsProperty = DependencyProperty.Register( // Регистрирует dependency property списка дефектов.
        nameof(Faults), // Передаёт имя свойства списка дефектов.
        typeof(IEnumerable), // Указывает тип значения свойства.
        typeof(DefectImpulseSignalSourceEditorControl), // Указывает тип владельца свойства.
        new PropertyMetadata(null)); // Задаёт метаданные свойства по умолчанию.

    /// <summary> // Документирует конструктор.
    /// Инициализирует новый контрол настройки источника дефектных импульсов. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public DefectImpulseSignalSourceEditorControl() // Объявляет конструктор контрола.
    { // Начинает тело конструктора.
        InitializeComponent(); // Инициализирует визуальное дерево контрола.
    } // Завершает тело конструктора.

    /// <summary> // Документирует коллекцию дефектов.
    /// Получает или задаёт набор дефектов, редактируемых вместе с источником дефектных импульсов. // Уточняет значение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public IEnumerable? Faults // Объявляет свойство набора дефектов.
    { // Начинает тело свойства.
        get => (IEnumerable?)GetValue(FaultsProperty); // Возвращает текущий набор дефектов.
        set => SetValue(FaultsProperty, value); // Обновляет текущий набор дефектов.
    } // Завершает тело свойства.
} // Завершает тело класса.
