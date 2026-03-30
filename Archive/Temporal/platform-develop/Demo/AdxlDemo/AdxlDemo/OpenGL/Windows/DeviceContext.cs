using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;

using BOOL = Int32;

/// <summary>
/// Представляет контекст устройства.
/// </summary>
public sealed class DeviceContext
{
    /// <summary>
    /// Возвращает недействительный контекст устройства.
    /// </summary>
    public static DeviceContext Invalid { get; } = new(IntPtr.Zero, IntPtr.Zero);

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="windowHandle">
    /// Дескриптор окна, с которым связан контекст.
    /// </param>
    /// <param name="deviceHandle">
    /// Дескриптор контекста устройства.
    /// </param>
    private DeviceContext([ParameterNoChecks] IntPtr windowHandle, [ParameterNoChecks] IntPtr deviceHandle)
    {
        //  Установка дескрипторов.
        WindowHandle = windowHandle;
        DeviceHandle = deviceHandle;
    }

    /// <summary>
    /// Возвращает дескриптор окна, с которым связан контекст.
    /// </summary>
    public IntPtr WindowHandle { get; private set; }

    /// <summary>
    /// Возвращает дескриптор контекста устройства.
    /// </summary>
    public IntPtr DeviceHandle { get; private set; }

    /// <summary>
    /// Возвращает значение, определяющее, является ли контекст недействительным.
    /// </summary>
    public bool IsInvalid => DeviceHandle == IntPtr.Zero;

    /// <summary>
    /// Освобождает контекст.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public void Release()
    {
        //  Проверка контекста.
        if (!IsInvalid)
        {
            //  Освобождение контекста.
            if (ReleaseDC(WindowHandle, DeviceHandle) == 0)
            {
                //  Произошла попытка выполнить недопустимую операцию.
                throw Exceptions.OperationInvalid();
            }

            //  Сброс дескрипторов.
            DeviceHandle = IntPtr.Zero;
            WindowHandle = IntPtr.Zero;
        }

        //  Функция прямого вызова.
        [DllImport("User32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    }

    /// <summary>
    /// Меняет местами буферы переднего и заднего плана, если буфер заднего плана включен.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public void SwapBuffers()
    {
        //  Проверка контекста.
        IsContext();

        //  Смена буферов.
        if (SwapBuffers(DeviceHandle) == 0)
        {
            //  Произошла попытка выполнить недопустимую операцию.
            throw Exceptions.OperationInvalid();
        }

        //  Функция прямого вызова.
        [DllImport("Gdi32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern BOOL SwapBuffers(IntPtr hdc);
    }

    /// <summary>
    /// Создаёт контекст устройства для заданного окна.
    /// </summary>
    /// <param name="windowHandle">
    /// Дескриптор окна.
    /// </param>
    /// <returns>
    /// Новый контекст устройства.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="windowHandle"/> передано нулевое значение.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public static DeviceContext FromWindow(IntPtr windowHandle)
    {
        //  Проверка дескриптора окна.
        IsNotZero(windowHandle, nameof(windowHandle));

        //  Получение дескриптора контекста устройства.
        IntPtr deviceHandle = GetDC(windowHandle);

        //  Проверка полученного дескриптора.
        if (deviceHandle == IntPtr.Zero)
        {
            //  Произошла попытка выполнить недопустимую операцию.
            throw Exceptions.OperationInvalid();
        }

        //  Возврат нового контекста устройства.
        return new(windowHandle, deviceHandle);

        //  Функция прямого вызова.
        [DllImport("User32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern IntPtr GetDC(IntPtr hWnd);
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
