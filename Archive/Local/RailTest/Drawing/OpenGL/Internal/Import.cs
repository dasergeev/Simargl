using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

using GLenum = System.UInt32;
using GLboolean = System.Byte;
using GLbitfield = System.UInt32;
using GLbyte = System.SByte;
using GLshort = System.Int16;
using GLint = System.Int32;
using GLsizei = System.Int32;
using GLubyte = System.Byte;
using GLushort = System.UInt16;
using GLuint = System.UInt32;
using GLfloat = System.Single;
using GLclampf = System.Single;
using GLdouble = System.Double;
using GLclampd = System.Double;

using UINT = System.UInt32;
using COLORREF = System.UInt32;
using BYTE = System.Byte;
using WORD = System.UInt16;
using DWORD = System.UInt32;
using FLOAT = System.Single;
using BOOL = System.Int32;
using LPCSTR = System.IntPtr;
using HWND = System.IntPtr;
using HGDIOBJ = System.IntPtr;
using HDC = RailTest.Drawing.OpenGL.DeviceContextHandle;
using HGLRC = RailTest.Drawing.OpenGL.RenderingContextHandle;
using PIXELFORMATDESCRIPTOR = RailTest.Drawing.OpenGL.PixelFormatDescriptor;
using GLYPHMETRICSFLOAT = System.IntPtr;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Предоставляет прямой доступ к функциям в соответсвии со спецификацией OpenGL.
    /// </summary>
    internal unsafe static partial class Import
    {
        //public struct POINTFLOAT
        //{
        //    public FLOAT x;
        //    public FLOAT y;
        //}

        //public struct GLYPHMETRICSFLOAT
        //{
        //    public FLOAT gmfBlackBoxX;
        //    public FLOAT gmfBlackBoxY;
        //    public POINTFLOAT gmfptGlyphOrigin;
        //    public FLOAT gmfCellIncX;
        //    public FLOAT gmfCellIncY;
        //}

        //public struct LAYERPLANEDESCRIPTOR
        //{
        //    public WORD nSize;
        //    public WORD nVersion;
        //    public DWORD dwFlags;
        //    public BYTE iPixelType;
        //    public BYTE cColorBits;
        //    public BYTE cRedBits;
        //    public BYTE cRedShift;
        //    public BYTE cGreenBits;
        //    public BYTE cGreenShift;
        //    public BYTE cBlueBits;
        //    public BYTE cBlueShift;
        //    public BYTE cAlphaBits;
        //    public BYTE cAlphaShift;
        //    public BYTE cAccumBits;
        //    public BYTE cAccumRedBits;
        //    public BYTE cAccumGreenBits;
        //    public BYTE cAccumBlueBits;
        //    public BYTE cAccumAlphaBits;
        //    public BYTE cDepthBits;
        //    public BYTE cStencilBits;
        //    public BYTE cAuxBuffers;
        //    public BYTE iLayerPlane;
        //    public BYTE bReserved;
        //    public COLORREF crTransparent;
        //}

        public delegate int PROC();




        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern BOOL wglCopyContext(HGLRC hglrcSrc, HGLRC hglrcDst, UINT mask);

        /// <summary>
        /// Создает новый контекст рендеринга OpenGL, который подходит для рисования на устройстве, на которое ссылается <paramref name="hdc"/>.
        /// Контекст рендеринга имеет тот же формат пикселей, что и контекст устройства.
        /// </summary>
        /// <param name="hdc">
        /// Дескриптор контекста устройства, для которого функция создает подходящий контекст рендеринга OpenGL.
        /// </param>
        /// <returns>
        /// Если функция завершается успешно, возвращаемое значение является допустимым дескриптором контекста рендеринга OpenGL.
        /// Если функция завершается ошибкой, возвращаемое значение равно <see cref="HGLRC.Invalid"/>.
        /// </returns>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        unsafe public static extern HGLRC wglCreateContext(HDC hdc);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern HGLRC wglCreateLayerContext(HDC hdc, int iLayerPlane);

        /// <summary>
        /// Функция удаляет указанный контекст рендеринга OpenGL.
        /// </summary>
        /// <param name="hrc">
        /// Дескриптор контекста рендеринга OpenGL для удаления.
        /// </param>
        /// <returns>
        /// Если функция завершается успешно, возвращаемое значение равно 1.
        /// Если функция завершается ошибкой, возвращаемое значение - 0.
        /// </returns>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        unsafe public static extern BOOL wglDeleteContext(HGLRC hrc);

        //[DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        //[SuppressUnmanagedCodeSecurity]
        //[HandleProcessCorruptedStateExceptions]
        //[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        //public static extern BOOL   wglDescribeLayerPlane(HDC hdc, int iPixelFormat, int iLayerPlane, UINT nBytes, LAYERPLANEDESCRIPTOR* plpd);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern HGLRC wglGetCurrentContext();

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern HDC wglGetCurrentDC();

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern int wglGetLayerPaletteEntries(HDC hdc, int iLayerPlane, int iStart, int cEntries, COLORREF* pcr);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern PROC wglGetProcAddress(LPCSTR lpszProc);

        /// <summary>
        /// Делает указанный контекст рендеринга OpenGL текущим контекстом визуализации вызывающего потока.
        /// Все последующие вызовы OpenGL, сделанные потоком, отрисовываются на устройстве, идентифицированном <paramref name="hdc"/>.
        /// Вы также можете использовать wglMakeCurrent, чтобы изменить текущий контекст рендеринга вызывающего потока, чтобы он больше не был текущим.
        /// </summary>
        /// <param name="hdc">
        /// Дескриптор к контексту устройства. Последующие вызовы OpenGL, сделанные вызывающим потоком, отрисовываются на устройстве, идентифицированном hdc.
        /// </param>
        /// <param name="hrc">
        /// Дескриптор контекста рендеринга OpenGL, который функция устанавливает как контекст рендеринга вызывающего потока.
        /// Если <paramref name="hdc"/> имеет значение <see cref="HGLRC.Invalid"/>, функция делает текущий контекст рендеринга вызывающего потока не текущим
        /// и освобождает контекст устройства, который используется контекстом рендеринга.
        /// В этом случае <paramref name="hdc"/> игнорируется.
        /// </param>
        /// <returns>
        /// Когда функция завершается успешно, возвращаемое значение равно 1;
        /// в противном случае возвращаемое значение - 0.
        /// </returns>
        /// <remarks>
        /// Перед переключением в новый контекст рендеринга OpenGL сбрасывает любой предыдущий контекст рендеринга, который был текущим для вызывающего потока.
        /// Поток может иметь один текущий контекст рендеринга. Процесс может иметь несколько контекстов рендеринга с помощью многопоточности.
        /// Поток должен установить текущий контекст рендеринга перед вызовом любых функций OpenGL. В противном случае все вызовы OpenGL игнорируются.
        /// Контекст рендеринга может быть актуальным только для одного потока. Вы не можете сделать контекст рендеринга текущим для нескольких потоков.
        /// Приложение может выполнять многопоточное рисование, делая разные контексты рендеринга текущими для разных потоков,
        /// предоставляя каждому потоку свой собственный контекст рендеринга и контекст устройства.
        /// Если возникает ошибка, функция делает текущий контекст рендеринга потока не текущим до возврата.
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        unsafe public static extern BOOL wglMakeCurrent(HDC hdc, HGLRC hrc);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern BOOL wglRealizeLayerPalette(HDC hdc, int iLayerPlane, BOOL bRealize);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern int wglSetLayerPaletteEntries(HDC hdc, int iLayerPlane, int iStart, int cEntries, COLORREF* pcr);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern BOOL wglShareLists(HGLRC hglrc1, HGLRC hglrc2);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern BOOL wglSwapLayerBuffers(HDC hdc, UINT fuPlanes);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern BOOL wglUseFontBitmapsA(HDC hdc, DWORD first, DWORD count, DWORD listBase);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern BOOL wglUseFontBitmapsW(HDC hdc, DWORD first, DWORD count, DWORD listBase);

        //[DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        //[SuppressUnmanagedCodeSecurity]
        //[HandleProcessCorruptedStateExceptions]
        //[SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        //public static extern BOOL   wglUseFontOutlinesA(HDC hdc, DWORD first, DWORD count, DWORD listBase, FLOAT deviation, FLOAT extrusion, int format, GLYPHMETRICSFLOAT* lpgmf);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern BOOL wglUseFontOutlinesW(HDC hdc, DWORD first, DWORD count, DWORD listBase, FLOAT deviation, FLOAT extrusion, int format, GLYPHMETRICSFLOAT* lpgmf);

        /// <summary>
        /// Извлекает дескриптор контекста устройства для клиентской области указанного окна или для всего экрана.
        /// </summary>
        /// <param name="hWnd">
        /// Дескриптор окна, для которого необходимо получить дескриптор контекста устройства для клиентской области.
        /// Если это значение равно <see cref="HDC.Invalid"/>, функция возвращает дескриптор контекста устройства для всего экрана.
        /// </param>
        /// <returns>
        /// Если функция завершается успешно, возвращаемое значение является дескриптором контекста устройства для клиентской области указанного окна.
        /// Если функция завершается ошибкой, возвращаемое значение равно <see cref="HWND.Zero"/>.
        /// </returns>
        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        public static extern HDC GetDC(HWND hWnd);

        /// <summary>
        /// Функция освобождает контекст устройства, освобождая его для использования другими приложениями.
        /// </summary>
        /// <param name="hWnd">
        /// Дескриптор окна, контекст устройства которого должен быть освобожден.
        /// </param>
        /// <param name="hDC">
        /// Дескриптор контекста устройства, который необходимо освободить.
        /// </param>
        /// <returns>
        /// Если контекст устройства был освобожден, возвращаемое значение равно 1.
        /// Если контекст устройства не был освобожден, возвращаемое значение равно нулю.
        /// </returns>
        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        unsafe public static extern int ReleaseDC(HWND hWnd, HDC hDC);

        /// <summary>
        /// Выполняет попытку сопоставить соответствующий формат пикселя, поддерживаемый контекстом устройства, с заданной спецификацией формата пикселя.
        /// </summary>
        /// <param name="hdc">
        /// Контекст устройства, который функция проверяет, чтобы определить наилучшее соответствие для дескриптора формата пикселя.
        /// </param>
        /// <param name="ppfd">
        /// Указатель на структуру <see cref="PIXELFORMATDESCRIPTOR"/>, которая определяет запрашиваемый формат пикселей.
        /// </param>
        /// <returns>
        /// Если функция завершается успешно, возвращаемое значение представляет собой индекс формата пикселя,
        /// который наиболее близко соответствует данному дескриптору формата пикселя.
        /// Если функция завершается ошибкой, возвращаемое значение равно нулю.
        /// </returns>
        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        unsafe public static extern int ChoosePixelFormat(HDC hdc, PIXELFORMATDESCRIPTOR* ppfd);

        /// <summary>
        /// Получает информацию о формате пикселей.
        /// </summary>
        /// <param name="hdc">
        /// Контекст устройства.
        /// </param>
        /// <param name="iPixelFormat">
        /// Индекс, который определяет формат пикселя.
        /// Пиксельные форматы, которые поддерживает контекст устройства, идентифицируются с помощью положительных целочисленных индексов.
        /// </param>
        /// <param name="nBytes">
        /// Размер в байтах структуры, на которую указывает <paramref name="ppfd"/>.
        /// </param>
        /// <param name="ppfd">
        /// Указатель на структуру <see cref="PIXELFORMATDESCRIPTOR"/>, члены которой функция устанавливает данными формата пикселей.
        /// Функция хранит количество байтов, скопированных в структуру, в элементе nSize структуры.
        /// Если <paramref name="ppfd"/> имеет значение null, функция не записывает данные в структуру.
        /// Это полезно, когда вы хотите получить только максимальный индекс формата пикселя контекста устройства.
        /// </param>
        /// <returns>
        /// Если функция завершается успешно, возвращаемое значение равно максимальному индексу формата пикселя контекста устройства.
        /// Если функция завершается ошибкой, возвращаемое значение равно нулю.
        /// </returns>
        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        unsafe public static extern int DescribePixelFormat(HDC hdc, int iPixelFormat, UINT nBytes, PIXELFORMATDESCRIPTOR* ppfd);

        /// <summary>
        /// Функция устанавливает формат пикселя указанного контекста устройства в формате, указанном в индексе <paramref name="iPixelFormat"/>.
        /// </summary>
        /// <param name="hdc">
        /// Контекст устройства.
        /// </param>
        /// <param name="iPixelFormat">
        /// Индекс, который идентифицирует формат пикселя для установки.
        /// </param>
        /// <param name="ppfd">
        /// Указатель на структуру <see cref="PIXELFORMATDESCRIPTOR"/>, которая содержит спецификацию формата логического пикселя.
        /// Компонент метафайла системы использует эту структуру для записи спецификации формата логического пикселя.
        /// Структура не оказывает никакого другого влияния на поведение функции.
        /// </param>
        /// <returns>
        /// Если функция завершается успешно, возвращаемое значение равно 1.
        /// Если функция завершается ошибкой, возвращаемое значение - 0.
        /// </returns>
        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        unsafe public static extern BOOL SetPixelFormat(HDC hdc, int iPixelFormat, PIXELFORMATDESCRIPTOR* ppfd);

        /// <summary>
        /// Обменивает передний и задний буферы, если текущий формат пикселя для окна,
        /// на которое ссылается указанный контекст устройства, включает в себя задний буфер.
        /// </summary>
        /// <param name="hdc">
        /// Определяет контекст устройства.
        /// </param>
        /// <returns>
        /// Если функция завершается успешно, возвращаемое значение равно 1.
        /// Если функция завершается ошибкой, возвращаемое значение - 0.
        /// </returns>
        /// <remarks>
        /// Если текущий формат пикселей для окна, на которое ссылается контекст устройства,
        /// не включает в себя обратный буфер, этот вызов не имеет никакого эффекта,
        /// и содержимое заднего буфера не определено, когда функция возвращается.
        /// В многопоточных приложениях перед вызовом функции сбросьте команды рисования в любом другом потоке, рисующем в то же окно.
        /// </remarks>
        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        unsafe public static extern BOOL SwapBuffers(HDC hdc);

        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        public static extern HGDIOBJ SelectObject(HDC hdc, HGDIOBJ h);
    }
}
