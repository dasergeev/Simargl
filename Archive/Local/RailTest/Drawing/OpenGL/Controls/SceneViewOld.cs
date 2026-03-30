using RailTest.Drawing.OpenGL.Core;
using System;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет элемент управления, отображающий сцену.
    /// </summary>
    public class SceneViewOld : Control
    {
        /// <summary>
        /// Поле для хранения контекста устройства.
        /// </summary>
        private DeviceContext _DeviceContext;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public SceneViewOld()
        {
            ResizeRedraw = true;
            RenderingRegion = new SceneViewRegion(this);
        }

        /// <summary>
        /// Возвращает область рендеринга.
        /// </summary>
        public RenderingRegion RenderingRegion { get; }

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
            }
            catch (Exception)
            {
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
            void action()
            {
                using (var renderingContext = new RenderingContext(_DeviceContext))
                {
                    renderingContext.MakeCurrent();
                    Origin.glClearColor(BackColor.R / 255.0f, BackColor.G / 255.0f, BackColor.B / 255.0f, BackColor.A / 255.0f);
                    renderingContext.Clear(CleaningMask.Color | CleaningMask.Depth);
                    
                    //Origin.glEnable(Origin.GL_CULL_FACE);
                    Origin.glEnable(Origin.GL_DEPTH_TEST);

                    Origin.glEnable(Origin.GL_LIGHTING);
                    Origin.glEnable(Origin.GL_NORMALIZE);

                    RenderingRegion.OnRender(EventArgs.Empty);

                    Origin.glFlush();
                    RenderingContext.ClearCurrentContext();
                }
            }
            Task.Run(action).Wait();
            _DeviceContext.SwapBuffers();
        }
    }
}
