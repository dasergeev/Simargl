namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;

using System.Windows.Forms;
using Control = System.Windows.Forms.Control;

/// <summary>
/// Представляет элемент управления, управляющий рендерингом OpenGL.
/// </summary>
public abstract class RenderControl :
    Control
{
    /// <summary>
    /// Поле для хранения контекста устройства.
    /// </summary>
    private DeviceContext? _DeviceContext;

    /// <summary>
    /// Поле для хранения контекста рендеринга.
    /// </summary>
    private RenderingContext? _RenderingContext;

    /// <summary>
    /// Поле для хранения объекта, отображающего текст на сцене OpenGL.
    /// </summary>
    private TextRenderer? _TextRender;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public RenderControl()
    {
        //  Установка перерисовки при изменении размеров.
        ResizeRedraw = true;

        //  Установка значений полей.
        _DeviceContext = null;
        _RenderingContext = null;
        _TextRender = null;
    }

    /// <summary>
    /// Выполняет рендеринг.
    /// </summary>
    /// <param name="renderer">
    /// Средство рендеринга.
    /// </param>
    protected abstract void Render(Renderer renderer);

    /// <summary>
    /// Вызывает событие <see cref="Control.HandleCreated"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override sealed void OnHandleCreated(EventArgs e)
    {
        //  Создание контекста устройства.
        _DeviceContext = DeviceContext.FromWindow(Handle);

        //  Создание контекста рендеринга.
        _RenderingContext = RenderingContext.FromDevice(_DeviceContext);

        //  Обновление устройства рендеринга текста.
        UpdateTextRender();

        //  Выполнение в контексте рендеринга.
        _RenderingContext.Invoke(delegate
        {
            //  Очистка буферов.
            Original.ClearColor(BackColor.R / 255.0f, BackColor.G / 255.0f, BackColor.B / 255.0f, BackColor.A / 255.0f);
            Original.Clear(ClearBufferMask.Depth | ClearBufferMask.Color);

            //  Принудительное выполнение команд.
            Original.Flush();

            //  Переключение буферов.
            _DeviceContext.SwapBuffers();
        });

        //  Вызов метода базового класса.
        base.OnHandleCreated(e);
    }

    /// <summary>
    /// Вызывает событие <see cref="Control.HandleDestroyed"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override sealed void OnHandleDestroyed(EventArgs e)
    {
        //  Выполнение в контексте рендеринга.
        _RenderingContext?.Invoke(delegate
        {
            //  Установка недействительного контекста текущим.
            RenderingContext.Invalid.MakeCurrent();

            //  Удаление контекста рендеринга.
            _RenderingContext.Delete();

            //  Освобождение контекста устройства.
            _DeviceContext?.Release();
        });

        //  Вызов метода базового класса.
        base.OnHandleDestroyed(e);
    }

    /// <summary>
    /// Рисует фон элемента.
    /// </summary>
    /// <param name="pevent">
    /// Сведения об элементе управления, который следует нарисовать.
    /// </param>
    protected override sealed void OnPaintBackground(PaintEventArgs pevent)
    {
        //  Выполнение в контексте рендеринга.
        _RenderingContext?.Invoke(delegate
        {
            //  Проверка средства рендеринга текста.
            if (_TextRender is not null)
            {
                //  Создание средства рендеринга.
                Renderer renderer = new(_TextRender);

                //  Очистка буферов.
                Original.ClearColor(BackColor.R / 255.0f, BackColor.G / 255.0f, BackColor.B / 255.0f, 1.0f);
                Original.Clear(ClearBufferMask.Depth | ClearBufferMask.Color);

                //  Отображение сцены.
                Render(renderer);

                //  Принудительное выполнение команд.
                Original.Flush();

                //  Переключение буферов.
                _DeviceContext?.SwapBuffers();
            }
        });
    }

    /// <summary>
    /// Вызывает событие <see cref="Control.BackColorChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override sealed void OnBackColorChanged(EventArgs e)
    {
        //  Вызов метода базового класса.
        base.OnBackColorChanged(e);

        //  Перерисовка сцены.
        Invalidate();
    }

    /// <summary>
    /// Вызывает событие <see cref="Control.FontChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override sealed void OnFontChanged(EventArgs e)
    {
        //  Вызов метода базового класса.
        base.OnFontChanged(e);

        //  Обновление устройства рендеринга текста.
        UpdateTextRender();
    }

    /// <summary>
    /// Обновляет устройство рендеринга текста.
    /// </summary>
    private void UpdateTextRender()
    {
        //  Выполнение в контексте рендеринга.
        _RenderingContext?.Invoke(delegate
        {
            //  Удаление объекта, отображающего текст на сцене OpenGL.
            _TextRender?.Delete();

            //  Сброс ссылки.
            _TextRender = null;

            //  Создание объекта, отображающего текст на сцене OpenGL.
            _TextRender = new(_RenderingContext, Font.ToHfont(), 1, 254);
        });
    }
}
