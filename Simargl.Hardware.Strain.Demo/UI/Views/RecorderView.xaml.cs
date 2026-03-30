using Simargl.Hardware.Strain.Demo.Nodes;
using System.Windows;
using System.Windows.Media;

namespace Simargl.Hardware.Strain.Demo.UI.Views;

/// <summary>
/// Представляет элемент управления, отображающий регистратор.
/// </summary>
partial class RecorderView
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public RecorderView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Добавление обработчика события изменения сердца приложения.
        HeartChanged += heartChanged;

        //  Обрабатывает событие изменения сердца приложения.
        void heartChanged(object? sender, EventArgs e)
        {
            //  Проверка сердца приложения.
            if (Heart is not null)
            {
                //  Добавление элемента управления, отображающего графики.
                _Host.Child = Heart.ChartManager.Chart;

                

//                Background
//Foreground

                //  Удаление обработчика события.
                HeartChanged -= heartChanged;
            }
        }

    }

    /// <summary>
    /// Возвращает целевой тип узла.
    /// </summary>
    public override Type TargetNodeType { get; } = typeof(RecorderNode);

    static RecorderView()
    {
        BackgroundProperty.OverrideMetadata(typeof(RecorderView),
            new FrameworkPropertyMetadata(null, OnBackgroundChanged));
    }

    private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //var control = (RecorderView)d;
        Brush oldBrush = (Brush)e.OldValue;
        Brush newBrush = (Brush)e.NewValue;

        // Реакция на изменение
        ((App)App.Current).Journal.Add($"Background изменился: {oldBrush} → {newBrush}");
    }
}
