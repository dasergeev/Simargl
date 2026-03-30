using Simargl.Embedded.Tunings.TuningsEditor.Core;
using System.ComponentModel;

namespace Simargl.Embedded.Tunings.TuningsEditor;

/// <summary>
/// Представляет главное окно приложения.
/// </summary>
partial class MainWindow
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public MainWindow()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// Обрабатывает событие попытки закрытия окна.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Window_Closing(object sender, CancelEventArgs e)
    {
        //  Получение сердца приложения.
        if (DataContext is Heart heart)
        {
            //  Проверка сохранения.
            if (!heart.CheckSave())
            {
                //  Отмена закрытия окна.
                e.Cancel = true;
            }
        }
    }
}
