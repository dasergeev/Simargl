using RailTest.Drawing.OpenGL.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет контекст рендеринга.
    /// </summary>
    public class RenderingContext : Ancestor, IDisposable
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="deviceContext">
        /// Контекст устройства.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось создать новый контекст рендеринга OpenGL.
        /// </exception>
        internal RenderingContext(DeviceContext deviceContext)
        {
            deviceContext.CheckHandle();
            DeviceContext = deviceContext;
            Handle = Import.wglCreateContext(DeviceContext.Handle);
            if (Handle == RenderingContextHandle.Invalid)
            {
                throw new InvalidOperationException("Не удалось создать новый контекст рендеринга OpenGL.");
            }
        }

        /// <summary>
        /// Разрушает объект.
        /// </summary>
        ~RenderingContext()
        {
            if (Handle != RenderingContextHandle.Invalid)
            {
                Import.wglDeleteContext(Handle);
                Handle = RenderingContextHandle.Invalid;
            }
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением,
        /// высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            if (Handle != RenderingContextHandle.Invalid)
            {
                Import.wglDeleteContext(Handle);
                Handle = RenderingContextHandle.Invalid;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Возвращает контекст устройства.
        /// </summary>
        internal DeviceContext DeviceContext { get; }

        /// <summary>
        /// Возвращает дексприптор контекста рендеринга.
        /// </summary>
        internal RenderingContextHandle Handle { get; private set; }

        /// <summary>
        /// Выполняет проверку дескриптора контекста устройства.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        internal void CheckHandle()
        {
            DeviceContext.CheckHandle();
            if (Handle == RenderingContextHandle.Invalid)
            {
                throw new ObjectDisposedException("RenderingContext", "Произошла попытка выполнения операции над удаленным объектом.");
            }
        }

        /// <summary>
        /// Создаёт контуры шрифта.
        /// </summary>
        /// <param name="font">
        /// Шрифт.
        /// </param>
        /// <returns>
        /// Контуры шрифта.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="font"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        public FontOutlines CreateFontOutlines(Font font)
        {
            if (font is null)
            {
                throw new ArgumentNullException("font", "Передана пустая ссылка.");
            }
            CheckHandle();
            return new FontOutlines(font, this);
        }

        /// <summary>
        /// Делает контекст рендеринга OpenGL текущим контекстом визуализации вызывающего потока.
        /// Все последующие вызовы OpenGL, сделанные потоком, отрисовываются в этом контексте.
        /// </summary>
        /// <remarks>
        /// Перед переключением в новый контекст рендеринга OpenGL сбрасывает любой предыдущий контекст рендеринга, который был текущим для вызывающего потока.
        /// Поток может иметь один текущий контекст рендеринга. Процесс может иметь несколько контекстов рендеринга с помощью многопоточности.
        /// Поток должен установить текущий контекст рендеринга перед вызовом любых функций OpenGL. В противном случае все вызовы OpenGL игнорируются.
        /// Контекст рендеринга может быть актуальным только для одного потока. Вы не можете сделать контекст рендеринга текущим для нескольких потоков.
        /// Приложение может выполнять многопоточное рисование, делая разные контексты рендеринга текущими для разных потоков,
        /// предоставляя каждому потоку свой собственный контекст рендеринга и контекст устройства.
        /// Если возникает ошибка, функция делает текущий контекст рендеринга потока не текущим до возврата.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось установить контекст рендеринга OpenGL.
        /// </exception>
        internal void MakeCurrent()
        {
            CheckHandle();
            if (0 == Import.wglMakeCurrent(DeviceContext.Handle, Handle))
            {
                throw new InvalidOperationException("Не удалось установить контекст рендеринга OpenGL.");
            }
        }

        /// <summary>
        /// Делает текущий контекст рендеринга вызывающего потока не текущим и освобождает контекст устройства, который используется контекстом рендеринга.
        /// </summary>
        internal static void ClearCurrentContext()
        {
            Import.wglMakeCurrent(DeviceContextHandle.Invalid, RenderingContextHandle.Invalid);
        }

        /// <summary>
        /// Очищает буферы.
        /// </summary>
        /// <param name="mask">
        /// Значение, определяющее маску очистки.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        /// <exception cref="OpenGLException">
        /// Библиотека OpenGL вернула ошибку.
        /// </exception>
        internal void Clear(CleaningMask mask)
        {
            CheckHandle();
            int safeMask = 0;
            if ((mask & CleaningMask.Color) == CleaningMask.Color)
            {
                safeMask |= Origin.GL_COLOR_BUFFER_BIT;
            }
            if ((mask & CleaningMask.Depth) == CleaningMask.Depth)
            {
                safeMask |= Origin.GL_DEPTH_BUFFER_BIT;
            }
            if ((mask & CleaningMask.Accumulator) == CleaningMask.Accumulator)
            {
                safeMask |= Origin.GL_ACCUM_BUFFER_BIT;
            }
            if ((mask & CleaningMask.Stencil) == CleaningMask.Stencil)
            {
                safeMask |= Origin.GL_STENCIL_BUFFER_BIT;
            }
            if (safeMask != 0)
            {
                Origin.glClear(safeMask);
                OpenGLException.CheckErrorCode(Origin.glGetError());
            }
        }
    }
}
