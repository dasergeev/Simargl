using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;

namespace Simargl.Embedded.Tunings.TuningsEditor.UI;

/// <summary>
/// Представляет элемент управления, отображающий рабочее пространство.
/// </summary>
partial class Workspace
{
    ///// <summary>
    ///// Поле для хранения массива элементов управления, отображающих узлы.
    ///// </summary>
    //private readonly NodeView[] _Views;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Workspace()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        ////  Создание массива элементов управления, отображающих узлы.
        //_Views =
        //    [
        //        ArrayParamView,
        //        BitFieldParamView,
        //        CommandParamView,
        //        ControlSectionView,
        //        CriticalSectionView,
        //        DebugSectionView,
        //        EnumParamView,
        //        InfoSectionView,
        //        SeparateSectionView,
        //        SimpleParamView,
        //        SpecView,
        //    ];

        ////  Обновление элементов.
        //UpdateViews();

        ////  Добавление обработчика события.
        //Heart.SelectedNodeChanged += delegate (object? sender, EventArgs e)
        //{
        //    //  Обновление элементов.
        //    UpdateViews();
        //};
    }

    ///// <summary>
    ///// Обновляет элементы управления, отображающие узлы.
    ///// </summary>
    //private void UpdateViews()
    //{
    //    //  Получение выбранного узла.
    //    Node? node = Heart.GlobalSelectedNode;

    //    //  Получение типа содержимого.
    //    Type? type = node?.GetType();

    //    //  Перебор элементов управления, отображающих узлы.
    //    foreach (NodeView view in _Views)
    //    {
    //        //  Проверка типа.
    //        if (view.ContentType == type)
    //        {
    //            //  Отображение элемента.
    //            view.Visibility = Visibility.Visible;

    //            //  Установка контекста данных.
    //            view.DataContext = node;
    //        }
    //        else
    //        {
    //            //  Скрытие элемента.
    //            view.Visibility = Visibility.Collapsed;

    //            //  Установка контекста данных.
    //            view.DataContext = null;
    //        }
    //    }
    //}
}
