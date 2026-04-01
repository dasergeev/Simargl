using Simargl.FlawDetector.ViewModels.Sources; // Подключает конкретную view model вращательного источника.

namespace Simargl.FlawDetector.Controls.SourceEditors; // Определяет пространство имён контролов редакторов источников.

/// <summary> // Описывает назначение класса.
/// Представляет контрол настройки источника вращательной вибрации. // Уточняет роль контрола в интерфейсе.
/// </summary> // Завершает XML-документацию класса.
public partial class WheelRotationSignalSourceEditorControl : System.Windows.Controls.UserControl // Объявляет контрол настройки вращательного источника.
{ // Начинает тело класса.
    /// <summary> // Документирует конструктор.
    /// Инициализирует новый контрол настройки вращательного источника. // Уточняет назначение конструктора.
    /// </summary> // Завершает XML-документацию конструктора.
    public WheelRotationSignalSourceEditorControl() // Объявляет конструктор контрола.
    { // Начинает тело конструктора.
        InitializeComponent(); // Инициализирует визуальное дерево контрола.
    } // Завершает тело конструктора.
}
