using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;

using BOOL = Int32;
using DWORD = UInt32;

/// <summary>
/// Представляет объект, отображающий текст на сцене OpenGL.
/// </summary>
public sealed class TextRenderer
{
    /// <summary>
    /// Поле для хранения начального номера списков.
    /// </summary>
    private readonly uint _BeginNumber;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="renderingContext">
    /// Контекст рендеринга.
    /// </param>
    /// <param name="fontHandle">
    /// Дескриптор шрифта.
    /// </param>
    /// <param name="firstGlyphNumber">
    /// Номер первого глифа.
    /// </param>
    /// <param name="glyphCount">
    /// Количество глифов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="renderingContext"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="fontHandle"/> передано нулевое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="firstGlyphNumber"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="glyphCount"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="glyphCount"/> передано нулевое значение.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public TextRenderer(RenderingContext renderingContext, IntPtr fontHandle, int firstGlyphNumber, int glyphCount)
    {
        //  Проверка шрифта.
        IsNotZero(fontHandle, nameof(fontHandle));

        //  Установка контекста рендеринга.
        RenderingContext = IsNotNull(renderingContext, nameof(renderingContext));

        //  Установка номера первого глифа.
        FirstGlyphNumber = IsNotNegative(firstGlyphNumber, nameof(firstGlyphNumber));

        //  Установка количества глифов.
        GlyphCount = IsPositive(glyphCount, nameof(glyphCount));

        //  Начальный номер списков.
        uint beginNumber = 0;

        //  Выполнение в контексте рендеринга.
        renderingContext.Invoke(delegate
        {
            //  Выбор текущего шрифта.
            IntPtr oldFontHandle = SelectObject(renderingContext.DeviceContext.DeviceHandle, fontHandle);

            //  Проверка полученного значения.
            if (oldFontHandle == IntPtr.Zero)
            {
                //  Произошла попытка выполнить недопустимую операцию.
                throw Exceptions.OperationInvalid();
            }

            //  Блок с гарантированным завершением.
            try
            {
                //  Получение свободной области номеров списков.
                beginNumber = Original.GenLists(glyphCount);

                //  Проверка полученного номера.
                if (beginNumber == 0)
                {
                    //  Произошла попытка выполнить недопустимую операцию.
                    throw Exceptions.OperationInvalid();
                }

                //  Создание списка.
                if (0 == wglUseFontBitmapsW(renderingContext.DeviceContext.DeviceHandle,
                    unchecked((DWORD)firstGlyphNumber),
                    unchecked((DWORD)glyphCount),
                    beginNumber))
                {
                    //  Произошла попытка выполнить недопустимую операцию.
                    throw Exceptions.OperationInvalid();
                }
            }
            finally
            {
                //  Установка предыдущего шрифта.
                _ = SelectObject(renderingContext.DeviceContext.DeviceHandle, oldFontHandle);
            }
        });

        //  Установка начального номера списков.
        _BeginNumber = beginNumber;

        //  Функции прямого вызова.
        [DllImport("Gdi32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);

        [DllImport("Opengl32")][SuppressUnmanagedCodeSecurity][MethodImpl(MethodImplOptions.AggressiveInlining)]
        static extern BOOL wglUseFontBitmapsW(IntPtr hdc, DWORD first, DWORD count, DWORD listBase);
    }

    /// <summary>
    /// Возвращает контекст рендеринга.
    /// </summary>
    public RenderingContext RenderingContext { get; private set; }

    /// <summary>
    /// Возвращает номер первого глифа.
    /// </summary>
    public int FirstGlyphNumber { get; }

    /// <summary>
    /// Возвращает количество глифов.
    /// </summary>
    public int GlyphCount { get; }

    /// <summary>
    /// Выполняет растрирование текста.
    /// </summary>
    /// <param name="x">
    /// Координата положения точки вывода по оси Ox.
    /// </param>
    /// <param name="y">
    /// Координата положения точки вывода по оси Oy.
    /// </param>
    /// <param name="text">
    /// Текст, который необходимо растрировать.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="text"/> передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public void Rasterize(double x, double y, string text)
    {
        //  Проверка ссылки на текст.
        IsNotNull(text, nameof(text));

        //  Проверка необходимости растрирования.
        if (text.Length == 0)
        {
            //  Нет символов для растрирования.
            return;
        }

        //  Создание массива номеров списков.
        uint[] numbers = new uint[text.Length];

        //  Перебор символов текста.
        for (int i = 0; i != text.Length; ++i)
        {
            //  Определение номера отображаемого списка.
            int number = text[i] - (FirstGlyphNumber - 1);

            //  Проверка номера.
            if (number < 1 || number > GlyphCount)
            {
                //  Недопустимое значение номера списка.
                throw Exceptions.ArgumentOutOfRange(nameof(text));
            }

            //  Установка значения номера.
            numbers[i] = unchecked((uint)number);
        }

        //  Выполнение в контексте ренедринга.
        RenderingContext.Invoke(delegate
        {
            //  Установка начального номера спиков.
            Original.ListBase(_BeginNumber);

            //  Установка растровой позиции.
            Original.RasterPos(x, y);

            //  Перебор номеров списка.
            for (int i = 0; i != numbers.Length; ++i)
            {
                //  Вызов списка, отображающего символ.
                Original.CallList(numbers[i]);
            }
        });
    }

    /// <summary>
    /// Удаляет объект.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public void Delete()
    {
        //  Выполнение в контексте ренедринга.
        RenderingContext.Invoke(delegate
        {
            //  Удаление начального номера спиков.
            Original.DeleteLists(_BeginNumber, GlyphCount);

            //  Сброс текущего контекста рендеринга.
            RenderingContext = RenderingContext.Invalid;
        });
    }
}
