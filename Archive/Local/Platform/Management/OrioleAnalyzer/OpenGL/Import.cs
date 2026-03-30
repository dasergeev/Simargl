using System.Runtime.InteropServices;
using System.Security;

using HFONT = System.IntPtr;
using HGDIOBJ = System.IntPtr;
using HDC = System.IntPtr;
using HWND = System.IntPtr;
using UINT = System.UInt32;
using BOOL = System.Int32;
using HGLRC = System.IntPtr;
using GLbitfield = System.UInt32;
using GLint = System.Int32;
using GLsizei = System.Int32;
using GLenum = System.UInt32;
using GLdouble = System.Double;
using GLclampf = System.Single;
using GLubyte = System.Byte;
using GLfloat = System.Single;
using GLuint = System.UInt32;
using LPCWSTR = System.String;
using LONG = System.Int32;
using DWORD = System.UInt32;

namespace RailTest.TwoSection.DataViewer.OpenGL
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "SYSLIB1054:Используйте \\\"LibraryImportAttribute\\\" вместо \\\"DllImportAttribute\\\" для генерирования кода маршализации P/Invoke во время компиляции", Justification = "<Ожидание>")]
    internal class Import
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public readonly struct RECT
        {
            private readonly LONG left;
            private readonly LONG top;
            private readonly LONG right;
            private readonly LONG bottom;
        }


        public const GLenum GL_COMPILE = 0x1300;

        /// <summary>
        /// Значение, показывающее, что каждую вершину необходимо обрабатывать как одну точку.
        /// </summary>
        public const GLenum GL_POINTS = 0x0000;

        /// <summary>
        /// Значение, показывающее, что каждую пару вершин необходимо обрабатывать как независимый отрезок.
        /// </summary>
        public const GLenum GL_LINES = 0x0001;

        /// <summary>
        /// Значение, показывающее, что вершины определяют связанную группу отрезков линии
        /// от первой вершины до последней, а затем обратно до первой.
        /// </summary>
        public const GLenum GL_LINE_LOOP = 0x0002;

        /// <summary>
        /// Значение, показывающее, что вершины определяют связанную группу отрезков линии от первой вершины до последней.
        /// </summary>
        public const GLenum GL_LINE_STRIP = 0x0003;

        /// <summary>
        /// Значение, показывающее, что каждые три вершины необходимо обрабатывать как независимый треугольник.
        /// </summary>
        public const GLenum GL_TRIANGLES = 0x0004;

        /// <summary>
        /// Значение, показывающее, что вершины определяют связанную группу треугольников.
        /// Один треугольник определен для каждой вершины, представленной после первых двух вершин.
        /// Для нечетного n вершины n, n + 1 и n + 2 определяют треугольник n.
        /// Для четных n вершины n + 1, n и n + 2 определяют треугольник n.
        /// </summary>
        public const GLenum GL_TRIANGLE_STRIP = 0x0005;

        /// <summary>
        /// Значение, показывающее, что вершины определяют связанную группу треугольников.
        /// Один треугольник определен для каждой вершины, представленной после первых двух вершин.
        /// Вершины 1, n + 1, n + 2 определяют треугольник n.
        /// </summary>
        public const GLenum GL_TRIANGLE_FAN = 0x0006;

        /// <summary>
        /// Значение, показывающее, что вершины определяют независимый четырехугольник.
        /// </summary>
        public const GLenum GL_QUADS = 0x0007;

        /// <summary>
        /// Значение, показывающее, что вершины определяют связанную группу четырехугольников.
        /// Один четырехугольник определен для каждой пары вершин, представленных после первой пары.
        /// Вершины 2n - 1, 2n, 2n + 2 и 2n + 1 определяют четырехугольник n.
        /// </summary>
        public const GLenum GL_QUAD_STRIP = 0x0008;

        /// <summary>
        /// Значение, показывающее, что вершины определяют один выпуклый многоугольник.
        /// </summary>
        public const GLenum GL_POLYGON = 0x0009;

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HDC GetDC(HWND hWnd);

        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern int ChoosePixelFormat(HDC hdc, PIXELFORMATDESCRIPTOR* ppfd);

        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern int DescribePixelFormat(HDC hdc, int iPixelFormat, UINT nBytes, PIXELFORMATDESCRIPTOR* ppfd);

        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern BOOL SetPixelFormat(HDC hdc, int format, PIXELFORMATDESCRIPTOR* ppfd);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern HGLRC wglCreateContext(HDC hdc);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern BOOL wglMakeCurrent(HDC hdc, HGLRC hrc);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern void glClear(GLbitfield mask);

        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern BOOL SwapBuffers(HDC hdc);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern BOOL wglDeleteContext(HGLRC hrc);

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern int ReleaseDC(HWND hWnd, HDC hDC);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern void glViewport(GLint x, GLint y, GLsizei width, GLsizei height);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern void glMatrixMode(GLenum mode);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern void glLoadIdentity();

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        unsafe public static extern void glOrtho(GLdouble left, GLdouble right, GLdouble bottom, GLdouble top, GLdouble zNear, GLdouble zFar);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glClearColor(GLclampf red, GLclampf green, GLclampf blue, GLclampf alpha);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glBegin(GLenum mode);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glEnd();

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glVertex2f(GLfloat x, GLfloat y);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glVertex2d(GLdouble x, GLdouble y);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glColor3ub(GLubyte red, GLubyte green, GLubyte blue);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glNewList(GLuint list, GLenum mode);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glEndList();

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glCallList(GLuint list);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern GLuint glGenLists(GLsizei range);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glFlush();

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glDeleteLists(GLuint list, GLsizei range);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern BOOL wglUseFontBitmapsW(HDC hdc, DWORD first, DWORD count, DWORD listBase);

        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HGDIOBJ SelectObject(HDC hdc, HGDIOBJ h);

        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern HFONT CreateFontW(int cHeight, int cWidth, int cEscapement, int cOrientation, int cWeight, DWORD bItalic,
                             DWORD bUnderline, DWORD bStrikeOut, DWORD iCharSet, DWORD iOutPrecision, DWORD iClipPrecision,
                             DWORD iQuality, DWORD iPitchAndFamily, LPCWSTR pszFaceName);

        [DllImport("Gdi32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern BOOL DeleteObject(HGDIOBJ ho);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glListBase(GLuint offset);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glRasterPos2f(GLfloat x, GLfloat y);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glVertex2i(GLint x, GLint y);

        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern void glColor4ub(GLubyte red, GLubyte green, GLubyte blue, GLubyte alpha);


    }
}
