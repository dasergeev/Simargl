using RailTest.Drawing.OpenGL.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет элемент управления, отображающий сцену.
    /// </summary>
    public class SceneView : Control
    {
        /// <summary>
        /// Происходит при отрисовке сцены.
        /// </summary>
        public event EventHandler Render;

        /// <summary>
        /// Поле для хранения контекста устройства.
        /// </summary>
        private DeviceContext _DeviceContext;

        /// <summary>
        /// Поле для хранения контекста рендеринга.
        /// </summary>
        private RenderingContext _RenderingContext;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public SceneView()
        {
            ResizeRedraw = true;
            CleaningMask = CleaningMask.Color | CleaningMask.Depth;
        }

        /// <summary>
        /// Возвращает или задаёт значение, определяющее маску очистки, выполняемой перед отрисовкой сцены.
        /// </summary>
        public CleaningMask CleaningMask { get; set; }

        /// <summary>
        /// Создаёт контуры шрифта.
        /// </summary>
        /// <param name="font">
        /// Шрифт.
        /// </param>
        /// <returns>
        /// Контуры шрифта.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось установить контекст рендеринга OpenGL.
        /// </exception>
        public FontOutlines CreateFontOutlines(Font font)
        {
            if (_RenderingContext is null)
            {
                throw new ObjectDisposedException("RenderingContext", "Произошла попытка выполнения операции над удаленным объектом.");
            }
            return _RenderingContext.CreateFontOutlines(font);
        }

        /// <summary>
        /// Вызывает событие <see cref="Control.HandleCreated"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        protected override unsafe void OnHandleCreated(EventArgs e)
        {
            try
            {
                _DeviceContext = new DeviceContext(Handle);
                _DeviceContext.SetPixelFormat(new PixelFormatDescriptor(
                        PixelFormatDescriptorFlag.DoubleBuffer |
                        PixelFormatDescriptorFlag.DrawToWindow |
                        PixelFormatDescriptorFlag.SupportOpenGL)
                {
                    IsColorIndex = false,
                    ColorBits = 16,
                    DepthBits = 16,
                });
                _RenderingContext = new RenderingContext(_DeviceContext);
            }
            catch (Exception)
            {
                if (_RenderingContext is object)
                {
                    _RenderingContext.Dispose();
                    _RenderingContext = null;
                }
                if (_DeviceContext is object)
                {
                    _DeviceContext.Dispose();
                    _DeviceContext = null;
                }
                throw;
            }
            base.OnHandleCreated(e);
        }

        /// <summary>
        /// Вызывает событие <see cref="Control.HandleDestroyed"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (_RenderingContext is object)
            {
                _RenderingContext.Dispose();
                _RenderingContext = null;
            }
            if (_DeviceContext is object)
            {
                _DeviceContext.Dispose();
                _DeviceContext = null;
            }
        }

        /// <summary>
        /// Рисует фон элемента.
        /// </summary>
        /// <param name="pevent">
        /// Сведения об элементе управления, который следует нарисовать.
        /// </param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            _RenderingContext.MakeCurrent();
            Origin.glClearColor(BackColor.R / 255.0f, BackColor.G / 255.0f, BackColor.B / 255.0f, BackColor.A / 255.0f);
            _RenderingContext.Clear(CleaningMask);
            OnRender(EventArgs.Empty);
            Origin.glFlush();
            RenderingContext.ClearCurrentContext();
            _DeviceContext.SwapBuffers();
        }

        /// <summary>
        /// Вызывает событие <see cref="Render"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected virtual void OnRender(EventArgs e)
        {
            Render?.Invoke(this, e);
        }
    }
}
