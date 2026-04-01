using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Simargl.Platform.WinAPI;

using LRESULT = nint;
using HWND = nint;
using UINT = Int32;
using WPARAM = nuint;
using LPARAM = nint;
using POINT = System.Drawing.Point;
using RECT = System.Drawing.Rectangle;
using BOOL = Int32;

/// <summary>
/// Предоставляет прямой доступ к функциям Windows API.
/// </summary>
[CLSCompliant(false)]
[SupportedOSPlatform("windows")]
public sealed class Original
{
    /// <summary>
    /// Сообщение WM_PRINT отправляется в окно, чтобы запросить,
    /// что он рисует себя в указанном контексте устройства, чаще всего в контексте устройства принтера.
    /// </summary>
    public const int WM_PRINT = 0x0317;

    /// <summary>
    /// Рисует окно только в том случае, если оно отображается.
    /// </summary>
    public const int PRF_CHECKVISIBLE = 0x00000001;

    /// <summary>
    /// Рисует неклиентную область окна.
    /// </summary>
    public const int PRF_NONCLIENT = 0x00000002;

    /// <summary>
    /// Рисует клиентская область окна.
    /// </summary>
    public const int PRF_CLIENT = 0x00000004;

    /// <summary>
    /// Удаляет фон перед рисованием окна.
    /// </summary>
    public const int PRF_ERASEBKGND = 0x00000008;

    /// <summary>
    /// Рисует все видимые дочерние окна.
    /// </summary>
    public const int PRF_CHILDREN = 0x00000010;

    /// <summary>
    /// Рисует все принадлежащие окна.
    /// </summary>
    public const int PRF_OWNED = 0x00000020;

    /// <summary>
    /// 
    /// </summary>
    public const int WM_MOUSEMOVE = 0x0200;

    /// <summary>
    /// 
    /// </summary>
    public const int WM_LBUTTONDOWN = 0x0201;

    /// <summary>
    /// 
    /// </summary>
    public const int WM_LBUTTONUP = 0x0202;

    /// <summary>
    /// 
    /// </summary>
    public const uint IOCTL_DISK_GET_DRIVE_GEOMETRY_EX = 0x700A0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hDevice"></param>
    /// <param name="dwIoControlCode"></param>
    /// <param name="lpInBuffer"></param>
    /// <param name="nInBufferSize"></param>
    /// <param name="lpOutBuffer"></param>
    /// <param name="nOutBufferSize"></param>
    /// <param name="lpBytesReturned"></param>
    /// <param name="lpOverlapped"></param>
    /// <returns></returns>
    public static int DeviceIoControl(
        IntPtr hDevice,
        uint dwIoControlCode,
        IntPtr lpInBuffer,
        uint nInBufferSize,
        IntPtr lpOutBuffer,
        uint nOutBufferSize,
        out uint lpBytesReturned,
        IntPtr lpOverlapped)
    {
        return directСall(hDevice,
            dwIoControlCode,
            lpInBuffer,
            nInBufferSize,
            lpOutBuffer,
            nOutBufferSize,
            out lpBytesReturned,
            lpOverlapped);

        [DllImport("Kernel32", EntryPoint = "DeviceIoControl", CharSet = CharSet.Unicode)]
        static extern int directСall(
            IntPtr hDevice,
            uint dwIoControlCode,
            IntPtr lpInBuffer,
            uint nInBufferSize,
            IntPtr lpOutBuffer,
            uint nOutBufferSize,
            out uint lpBytesReturned,
            IntPtr lpOverlapped);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hObject"></param>
    /// <returns></returns>
    public static int CloseHandle(IntPtr hObject)
    {
        return directСall(hObject);

        [DllImport("Kernel32", EntryPoint = "CloseHandle", CharSet = CharSet.Unicode)]
        static extern int directСall(IntPtr hObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hFile"></param>
    /// <param name="lpBuffer"></param>
    /// <param name="nNumberOfBytesToRead"></param>
    /// <param name="lpNumberOfBytesRead"></param>
    /// <param name="lpOverlapped"></param>
    /// <returns></returns>
    public static int ReadFile(
        IntPtr hFile,
        IntPtr lpBuffer,
        uint nNumberOfBytesToRead,
        out uint lpNumberOfBytesRead,
        IntPtr lpOverlapped)
    {
        return directСall(hFile,
            lpBuffer,
            nNumberOfBytesToRead,
            out lpNumberOfBytesRead,
            lpOverlapped);

        [DllImport("Kernel32", EntryPoint = "ReadFile", CharSet = CharSet.Unicode)]
        static extern int directСall(
            IntPtr hFile,
            IntPtr lpBuffer,
            uint nNumberOfBytesToRead,
            out uint lpNumberOfBytesRead,
            IntPtr lpOverlapped);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hFile"></param>
    /// <param name="lpBuffer"></param>
    /// <param name="nNumberOfBytesToWrite"></param>
    /// <param name="lpNumberOfBytesWritten"></param>
    /// <param name="lpOverlapped"></param>
    /// <returns></returns>
    public static int WriteFile(
        IntPtr hFile,
        IntPtr lpBuffer,
        uint nNumberOfBytesToWrite,
        out uint lpNumberOfBytesWritten,
        IntPtr lpOverlapped)
    {
        return directСall(hFile,
            lpBuffer,
            nNumberOfBytesToWrite,
            out lpNumberOfBytesWritten,
            lpOverlapped);

        [DllImport("Kernel32", EntryPoint = "WriteFile", CharSet = CharSet.Unicode)]
        static extern int directСall(
            IntPtr hFile,
            IntPtr lpBuffer,
            uint nNumberOfBytesToWrite,
            out uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hFile"></param>
    /// <param name="liDistanceToMove"></param>
    /// <param name="lpNewFilePointer"></param>
    /// <param name="dwMoveMethod"></param>
    /// <returns></returns>
    public static int SetFilePointerEx(
        IntPtr hFile,
        long liDistanceToMove,
        out long lpNewFilePointer,
        uint dwMoveMethod)
    {
        return directСall(hFile,
            liDistanceToMove,
            out lpNewFilePointer,
            dwMoveMethod);

        [DllImport("Kernel32", EntryPoint = "SetFilePointerEx", CharSet = CharSet.Unicode)]
        static extern int directСall(
            IntPtr hFile,
            long liDistanceToMove,
            out long lpNewFilePointer,
            uint dwMoveMethod);
    }

    /// <summary>
    /// Отправляет указанное сообщение в окно или окна.
    /// Метод вызывает процедуру окна для указанного окна и не возвращается,
    /// пока процедура окна не обработала сообщение.
    /// </summary>
    /// <param name="hWnd">
    /// Дескриптор окна, процедура окна которого получит сообщение.
    /// </param>
    /// <param name="msg">
    /// Отправляемое сообщение.
    /// </param>
    /// <param name="wParam">
    /// Дополнительные сведения, относящиеся к сообщению.
    /// </param>
    /// <param name="lParam">
    /// Дополнительные сведения, относящиеся к сообщению.
    /// </param>
    /// <returns>
    /// Возвращаемое значение указывает результат обработки сообщения; это зависит от отправленного сообщения.
    /// </returns>
    public static LRESULT SendMessage(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
    {
        //  Вызов функции.
        return SendMessageW(hWnd, msg, wParam, lParam);

        //  Метод прямого вызова.
        [DllImport("user32.dll")]
        static extern LRESULT SendMessageW(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);
    }

    /// <summary>
    /// Извлекает дескриптор окна, содержащего указанную точку.
    /// </summary>
    /// <param name="Point">
    /// Точка.
    /// </param>
    /// <returns>
    /// Дескриптор окна.
    /// </returns>
    public static HWND WindowFromPoint(POINT Point)
    {
        //  Вызов функции.
        return WindowFromPoint(Point);

        //  Метод прямого вызова.
        [DllImport("user32.dll")]
        static extern HWND WindowFromPoint(POINT Point);
    }

    /// <summary>
    /// Извлекает размеры ограничивающего прямоугольника указанного окна.
    /// Размеры задаются в координатах экрана относительно левого верхнего угла экрана.
    /// </summary>
    /// <param name="handle">
    /// Дескриптор окна.
    /// </param>
    /// <param name="rectangle">
    /// Ограничивающий прямоугольник.
    /// </param>
    /// <returns>
    /// Результат вызова.
    /// </returns>
    public static bool GetWindowRect(HWND handle, out RECT rectangle)
    {
        //  Вызов функции.
        return GetWindowRect(handle, out rectangle) != 0;

        //  Метод прямого вызова.
        [DllImport("user32.dll")]
        static extern BOOL GetWindowRect(HWND hWnd, out RECT lpRect);
    }
}
