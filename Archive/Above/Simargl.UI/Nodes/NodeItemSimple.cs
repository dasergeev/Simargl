using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Simargl.UI.Nodes;

/// <summary>
/// Представляет простой элемент управления, отображающий узел в дереве.
/// </summary>
public sealed class NodeItemSimple :
    NodeItem
{
    /// <summary>
    /// Поле для хранения элемента управления, отображающего изображение.
    /// </summary>
    private readonly Image _Image;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public NodeItemSimple()
    {
        //  Создание сетки.
        Grid grid = new();

        // Определение колонок
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        //  Создание элемента управления, отображающего изображение.
        _Image = new Image
        {
            Margin = new Thickness(0, 0, 4, 0),
            Width = 16,
            Height = 16,
        };
        Grid.SetColumn(_Image, 0);

        //  Создание текстового блока.
        TextBlock textBlock = new();
        textBlock.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Name"));
        Grid.SetColumn(textBlock, 1);

        //  Добавление элементов на сетку.
        grid.Children.Add(_Image);
        grid.Children.Add(textBlock);

        //  Установка сетки, как содержимого.
        Content = grid;
    }

    /// <summary>
    /// Возвращает или задаёт источник изображения.
    /// </summary>
    public ImageSource ImageSource
    {
        get => _Image.Source;
        set => _Image.Source = value;
    }

    /// <summary>
    /// Создаёт источник элемента управления.
    /// </summary>
    /// <param name="imageSource">
    /// Источник изображения.
    /// </param>
    /// <returns>
    /// Источник элемента управления.
    /// </returns>
    public static ControlSource CreateControlSource(ImageSource imageSource)
    {
        return new SimpleControlSource(imageSource);
    }

    /// <summary>
    /// Представляет источник элемента управления.
    /// </summary>
    private sealed class SimpleControlSource(ImageSource imageSource) :
        ControlSource<NodeItemSimple>
    {
        /// <summary>
        /// Создаёт элемент управления.
        /// </summary>
        /// <returns>
        /// Новый элемент управления.
        /// </returns>
        internal override sealed object CreateInstance()
        {
            //  Создание объекта.
            NodeItemSimple nodeItem = Activator.CreateInstance(GetControlType()) as NodeItemSimple ??
                throw new InvalidOperationException("Не удалось создать элемент управления.");

            //  Установка источника изображения.
            nodeItem.ImageSource = imageSource;

            //  Возврат созданного объекта.
            return nodeItem;
        }
    }
}
