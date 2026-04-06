using Simargl.Hardware.Strain.Demo.Core;
using Simargl.Hardware.Strain.Demo.Journaling;
using Simargl.Hardware.Strain.Demo.Nodes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Simargl.Hardware.Strain.Demo.UI;

/// <summary>
/// Представляет элемент управления.
/// </summary>
public abstract class Control :
    System.Windows.Controls.UserControl
{
    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Heart"/>.
    /// </summary>
    public event EventHandler? HeartChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="SelectedNode"/>.
    /// </summary>
    public event EventHandler? SelectedNodeChanged;

    /// <summary>
    /// Поле для хранения сердца приложения.
    /// </summary>
    private Heart? _Heart;

    /// <summary>
    /// Поле для хранения выбранного узла.
    /// </summary>
    private Node? _SelectedNode;

    /// <summary>
    /// Поле для хранения свойства зависимости для <see cref="Heart"/>.
    /// </summary>
    public static readonly DependencyProperty HeartProperty =
            DependencyProperty.Register(
                nameof(Heart),
                typeof(Heart),
                typeof(Control),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.Inherits,
                    OnHeartChanged)
            );

    /// <summary>
    /// Поле для хранения приложения.
    /// </summary>
    private App? _Application;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Control()
    {
        //  Внутреннее обращение к объекту.
        Lay();

        //  Установка значений по умолчанию.
        _Heart = null;
        _SelectedNode = null;

        //  Добавление обработчика события изменения сердца приложения.
        HeartChanged += delegate (object? sender, EventArgs e)
        {
            //  Обновление выбранного узла.
            UpdateSelectedNode();
        };

        //  Обновление выбранного узла.
        UpdateSelectedNode();
    }

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    public Heart Heart => (Heart)GetValue(HeartProperty);

    /// <summary>
    /// Возвращает выбранный узел.
    /// </summary>
    public Node? SelectedNode => _SelectedNode;

    /// <summary>
    /// Возвращает значение, определяющее, находится ли элемент приложения в режиме разработки.
    /// </summary>
    protected bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(this);

    /// <summary>
    /// Возвращает приложение.
    /// </summary>
    protected App Application
    {
        get
        {
            //  Проверка приложения.
            if (_Application is not App application)
            {
                //  Получение приложения.
                application = (App)System.Windows.Application.Current;

                //  Замена приложения.
                application = Interlocked.CompareExchange(ref _Application, application, null) ?? application;
            }

            //  Возврат приложения.
            return application;
        }
    }

    /// <summary>
    /// Возвращает журнал.
    /// </summary>
    protected Journal Journal => Application.Journal;

    /// <summary>
    /// Возвращает средство вызова методов в основном потоке.
    /// </summary>
    protected Invoker Invoker => Application.Invoker;

    /// <summary>
    /// Возвращает механизм поддержки.
    /// </summary>
    protected Keeper Keeper => Application.Keeper;

    /// <summary>
    /// Выполняет внутреннее обращение к объекту.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void Lay()
    {
        //  Для анализатора.
        _ = this;
    }

    /// <summary>
    /// Происходит при изменении свойства зависимости <see cref="Heart"/>.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы связанные с событием.
    /// </param>
    private static void OnHeartChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        //  Получение элемента управления.
        if (sender is Control control)
        {
            //  Вызов события.
            Volatile.Read(ref control.HeartChanged)?.Invoke(control, EventArgs.Empty);
        }
    }


    /// <summary>
    /// Обновляет выбранный узел.
    /// </summary>
    private void UpdateSelectedNode()
    {
        //  Проверка изменения сердца приложения.
        if (!ReferenceEquals(_Heart, Heart))
        {
            //  Проверка сердца приложения.
            if (_Heart is not null)
            {
                //  Удаление обработчика события.
                _Heart.SelectedNodeChanged -= Heart_SelectedNodeChanged;
            }

            //  Установка сердца приложения.
            _Heart = Heart;

            //  Проверка сердца приложения.
            if (_Heart is not null)
            {
                //  Добавление обработчика события.
                _Heart.SelectedNodeChanged += Heart_SelectedNodeChanged;

                //  Вызов обработчика события.
                Heart_SelectedNodeChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Происходит при изменении выбранного узла.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Heart_SelectedNodeChanged(object? sender, EventArgs e)
    {
        //  Выбранный узел.
        Node? selectedNode = null;

        //  Проверка сердца приложения.
        if (_Heart is Heart heart)
        {
            //  Получение выбранного узла.
            selectedNode = heart.SelectedNode;
        }

        //  Проверка изменения выбранного узла.
        if (!ReferenceEquals(_SelectedNode, selectedNode))
        {
            //  Установка выбранного узла.
            _SelectedNode = selectedNode;

            //  Вызов события об изменении значения свойства.
            Volatile.Read(ref SelectedNodeChanged)?.Invoke(this, EventArgs.Empty);
        }
    }
}
