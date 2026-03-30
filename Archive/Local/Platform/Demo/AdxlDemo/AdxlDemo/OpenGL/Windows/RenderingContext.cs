using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;

using UINT = UInt32;
using BOOL = Int32;

/// <summary>
/// Представляет контекст рендеринга OpenGL.
/// </summary>
public sealed class RenderingContext
{
    /// <summary>
    /// Возвращает недействительный контекст рендеринга OpenGL.
    /// </summary>
    public static RenderingContext Invalid { get; } = new(DeviceContext.Invalid, IntPtr.Zero);

    /// <summary>
    /// Возвращает критический объект, используемый для синхронизации операций с контекстами рендеринга.
    /// </summary>
    public static object SyncRoot { get; } = new();

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="deviceContext">
    /// Контекст устройства.
    /// </param>
    /// <param name="renderingHandle">
    /// Контекст рендеринга OpenGL.
    /// </param>
    private RenderingContext([ParameterNoChecks] DeviceContext deviceContext, [ParameterNoChecks] IntPtr renderingHandle)
    {
        //  Установка значений свойств.
        DeviceContext = deviceContext;
        RenderingHandle = renderingHandle;
    }

    /// <summary>
    /// Возвращает контекс устройства.
    /// </summary>
    public DeviceContext DeviceContext { get; }

    /// <summary>
    /// Возвращает дескриптор контекста рендеринга OpenGL.
    /// </summary>
    public IntPtr RenderingHandle { get; private set; }

    /// <summary>
    /// Возвращает значение, определяющее, является ли контекст недействительным.
    /// </summary>
    public bool IsInvalid => RenderingHandle == IntPtr.Zero;

    /// <summary>
    /// Выполняет действие в текущем контексте рендеринга.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в текущем контексте рендеринга.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public void Invoke(Action action)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Блокировка критического объекта.
        lock (SyncRoot)
        {
            //  Проверка контекста.
            IsContext();

            //  Установка текущего контекста рендеринга.
            bool isContextChanged = MakeCurrent();

            //  Блок с гарантированным завершением.
            try
            {
                //  Выполнение действия.
                action();
            }
            finally
            {
                //  Проверка необходимости сброса контекста.
                if (isContextChanged)
                {
                    //  Установка недействительного контекста текущим.
                    Invalid.MakeCurrent();
                }
            }
        }
    }

    /// <summary>
    /// Создаёт контекст рендеринга OpenGL для заданного устройства.
    /// </summary>
    /// <param name="deviceContext">
    /// Устройство, для которого необходимо создать контекст рендеринга.
    /// </param>
    /// <returns>
    /// Новый контекст.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="deviceContext"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public static RenderingContext FromDevice(DeviceContext deviceContext)
    {
        //  Проверка ссылки на контекст устройства.
        IsNotNull(deviceContext, nameof(deviceContext));

        //  Блокировка критического объекта.
        lock (SyncRoot)
        {
            //  Блок небезопасного кода.
            unsafe
            {
                //  Созадние дескриптора формата пиксела.
                PixelFormatDescriptor pfd = new()
                {
                    nSize = (ushort)sizeof(PixelFormatDescriptor),
                    nVersion = 1,
                    dwFlags = 0x25,
                    iPixelType = 0,
                    cColorBits = 16,
                    cDepthBits = 16
                };

                //  Сопоставление формата пикселей, поддерживаемый контекстом устройства с заданной спецификацией формата пикселей.
                int pixelFormat = ChoosePixelFormat(deviceContext.DeviceHandle, ref pfd);

                //  Проверка формата пикселей.
                if (pixelFormat == 0)
                {
                    //  Произошла попытка выполнить недопустимую операцию.
                    throw Exceptions.OperationInvalid();
                }

                //  Получение сведений о формате пикселей.
                if (0 == DescribePixelFormat(deviceContext.DeviceHandle, pixelFormat, (uint)sizeof(PixelFormatDescriptor), ref pfd))
                {
                    //  Произошла попытка выполнить недопустимую операцию.
                    throw Exceptions.OperationInvalid();
                }

                //  Установка формата пикселей.
                if (0 == SetPixelFormat(deviceContext.DeviceHandle, pixelFormat, ref pfd))
                {
                    //  Произошла попытка выполнить недопустимую операцию.
                    throw Exceptions.OperationInvalid();
                }

                //  Создание дескриптора контекста рендеринга.
                IntPtr renderingHandle = wglCreateContext(deviceContext.DeviceHandle);

                //  Проверка полученного контекста рендеринга.
                if (renderingHandle == IntPtr.Zero)
                {
                    //  Произошла попытка выполнить недопустимую операцию.
                    throw Exceptions.OperationInvalid();
                }

                //  Создание и возврат контекста рендеринга.
                return new(deviceContext, renderingHandle);
            }
        }

        //  Функции прямого вызова.
        [DllImport("Gdi32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern int ChoosePixelFormat(IntPtr hdc, ref PixelFormatDescriptor ppfd);

        [DllImport("Gdi32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern int DescribePixelFormat(IntPtr hdc, int iPixelFormat, UINT nBytes, ref PixelFormatDescriptor ppfd);

        [DllImport("Gdi32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern BOOL SetPixelFormat(IntPtr hdc, int format, ref PixelFormatDescriptor ppfd);

        [DllImport("Opengl32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern IntPtr wglCreateContext(IntPtr hdc);
    }

    /// <summary>
    /// Устанавливает контекст текущим.
    /// </summary>
    /// <returns>
    /// Значение, указывающее, был ли изменён контекст.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public bool MakeCurrent()
    {
        //  Блокировка критического объекта.
        lock (SyncRoot)
        {
            //  Получение текущего контекста.
            IntPtr currentHandle = wglGetCurrentContext();

            //  Проверка необходимости изменения контекста.
            if (currentHandle == RenderingHandle)
            {
                //  Контекст остался текущим.
                return false;
            }

            //  Установка текущего контекста.
            if (0 == wglMakeCurrent(DeviceContext.DeviceHandle, RenderingHandle))
            {
                //  Произошла попытка выполнить недопустимую операцию.
                throw Exceptions.OperationInvalid();
            }

            //  Контекст изменён.
            return true;
        }

        //  Функции прямого вызова.
        [DllImport("Opengl32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern BOOL wglMakeCurrent(IntPtr hdc, IntPtr hrc);

        [DllImport("Opengl32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern IntPtr wglGetCurrentContext();
    }

    /// <summary>
    /// Удаляет контекст.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public void Delete()
    {
        //  Блокировка критического объекта.
        lock (SyncRoot)
        {
            //  Проверка контекста.
            if (!IsInvalid)
            {
                //  Удаление контекста.
                if (wglDeleteContext(RenderingHandle) == 0)
                {
                    //  Произошла попытка выполнить недопустимую операцию.
                    throw Exceptions.OperationInvalid();
                }

                //  Сброс дескрипторов.
                RenderingHandle = IntPtr.Zero;
            }
        }

        //  Функция прямого вызова.
        [DllImport("Opengl32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern BOOL wglDeleteContext(IntPtr hrc);
    }

    /// <summary>
    /// Выполняет проверку текущего контекста.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void IsContext()
    {
        //  Проверка контекста.
        if (IsInvalid)
        {
            //  В результате операции произошло обращение к разрушенному объекту.
            throw Exceptions.OperationObjectDisposed(nameof(DeviceContext));
        }
    }
}
