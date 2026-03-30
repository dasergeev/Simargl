using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;

using GLboolean = System.Byte;
using GLenum = System.Int32;
using GLbitfield = System.Int32;
using GLsizei = System.Int32;
using GLbyte = System.SByte;
using GLubyte = System.Byte;
using GLshort = System.Int16;
using GLushort = System.UInt16;
using GLint = System.Int32;
using GLuint = System.UInt32;
using GLfloat = System.Single;
using GLdouble = System.Double;
using GLclampf = System.Single;
using GLclampd = System.Double;

namespace RailTest.Drawing.OpenGL.Core
{
    /// <summary>
    /// Предоставляет прямой доступ к исходным функциям библиотеки OpenGL.
    /// </summary>
    public static class Origin
    {
        /// <summary>
        /// Функция работы с буфером накопления.
        /// </summary>
        /// <param name="op">
        /// op - это флаг который имеет несколько возможных значений:
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_ACCUM"/></term>
        /// <description>Функция будет получать RGBA значения цвета из указанного буфера</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_LOAD"/></term>
        /// <description>Функция будет заменять текущее RGBA значение цвета заменяя его в буфере</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_ADD"/></term>
        /// <description>Добавляет значение RGBA в буфер накопления</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_MULT"/></term>
        /// <description>Умножает каждую компоненту цвета RGBA на указанное значение</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_RETURN"/></term>
        /// <description>Перемещает значение из буфера накопления в указанный буфер</description>
        /// </item>
        /// </list>
        /// </para>
        /// </param>
        /// <param name="value">
        /// value - значение используемое в операциях с буфером накопления.
        /// </param>
        /// <remarks>
        /// <para>
        /// Буфер накопления - это буфер цвета с расширенным диапазоном. Изображения не отображаются в нем. 
        /// Скорее, изображения, визуализированные в один из цветовых буферов, добавляются к содержимому буфера накопления после рендеринга. 
        /// Такие эффекты, как сглаживание (точек, линий и многоугольников), размытие движения и глубина резкости, могут создаваться путем накопления изображений, созданных с использованием различных матриц преобразования.
        /// Каждый пиксель в буфере накопления состоит из значений красного, зеленого, синего и альфа. Количество бит на компонент в буфере накопления зависит от реализации.
        /// </para>
        /// <para>
        /// Вы можете проверить этот номер, вызвав <see cref="glGetIntegerv"/> четыре раза с аргументами <see cref="GL_ACCUM_RED_BITS"/>, <see cref="GL_ACCUM_GREEN_BITS"/>, <see cref="GL_ACCUM_BLUE_BITS"/> и <see cref="GL_ACCUM_ALPHA_BITS"/>. 
        /// Независимо от количества битов на компонент диапазон значений, хранимых каждым компонентом, равен -1 1. Пиксели буфера накопления отображаются один в один с пикселями буфера кадра.
        /// </para>
        /// <para>
        /// glAccum работает с буфером накопления. Первый аргумент op является символической константой, которая выбирает операцию буфера накопления. 
        /// Второй value, значение, является значением с плавающей точкой, которое будет использоваться в этой операции. Указано пять операций: <see cref="GL_ACCUM"/>, <see cref="GL_LOAD"/>, <see cref="GL_ADD"/>, <see cref="GL_MULT"/> и <see cref="GL_RETURN"/>.
        /// </para>
        /// Errors
        /// <para>
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_ENUM"/></term>
        /// <description>генерируется, если op не является допустимым значением</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>генерируется, если нет накопительного буфера</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>генерируется, если glAccum выполняется между выполнением <see cref="glBegin"/> и соответствующим выполнением <see cref="glEnd"/></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glAccum(GLenum op, GLfloat value);

        /// <summary>
        /// Функция задает функцию проверки альфа-канала. Данную функцию можно использовать только в режиме цвета RGBA.
        /// </summary>
        /// <param name="func">
        /// Имя функции.
        /// </param>
        /// <param name="ref">
        /// ref - Ссылочное значение, с которым сравниваются входящие альфа-значения. Это значение ограничено диапазоном от 0 до 1, где 0 представляет минимально возможное альфа-значение, а 1 - максимально возможное значение. Ссылка по умолчанию - 0.
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_NEVER"/></term>
        /// <description>никогда не срабатывает</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_LESS"/></term>
        /// <description>если входящее значение Альфа меньше, чем эталонное значение</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_EQUAL"/></term>
        /// <description>если входящее значение Альфа-канала равно эталонному значению</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_LEQUAL"/></term>
        /// <description>если входящее значение Альфа меньше или равен эталонному значению</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_GREATER"/></term>
        /// <description>если входящее значение Альфа больше, чем эталонное значение</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_NOTEQUAL"/></term>
        /// <description>если входящее значение Альфа не равно эталонному значению</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_GEQUAL"/></term>
        /// <description>если входящее значение Альфа больше или равен эталонному значению</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_ALWAYS"/></term>
        /// <description>функция будет срабатывать в любом случае</description>
        /// </item>
        /// </list>
        /// </para>
        /// </param>
        /// <remarks>
        /// Альфа-тест отбрасывает фрагменты в зависимости от результата сравнения между альфа-значением входящего фрагмента и постоянным эталонным значением. 
        /// glAlphaFunc указывает эталонное значение и функцию сравнения. Сравнение выполняется только при включенном альфа-тестировании. 
        /// По умолчанию он не включен. (См. <see cref="glEnable"/> и <see cref="glDisable"/> из <see cref="GL_ALPHA_TEST"/>.)
        /// func и ref определяют условия, при которых пиксель рисуется. Входящее альфа-значение сравнивается с ref с помощью функции, указанной func. 
        /// Если значение проходит сравнение, входящий фрагмент рисуется, если он также проходит последующие тесты трафарета и буфера глубины. 
        /// Если значение не соответствует сравнению, в этом кадровом буфере не производится никаких изменений в буфере кадров.
        /// <para>
        /// Errors
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_ENUM"/></term>
        /// <description>генерируется, если func не является допустимым значением</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>генерируется, если glAlphaFunc выполняется между выполнением <see cref="glBegin"/> и соответствующим выполнением <see cref="glEnd"/></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glAlphaFunc(GLenum func, GLclampf @ref);

        /// <summary>
        /// Определяет, загружены ли текстуры в текстурную память
        /// glAreTexturesResident запрашивает статус размещения текстуры n текстур, названных элементами текстур. 
        /// </summary>
        /// <param name="n">
        /// n - Указывает число текстур, которые будут опрошены
        /// </param>
        /// <param name="textures">
        /// textures - Задает массив, содержащий имена текстур, которые будут опрошены
        /// </param>
        /// <param name="residences">
        /// residences -Задает массив в котором должна быть текстура.
        /// </param>
        /// <returns>Если текстура будет найдена в массиве residences то функция вернут TRUE иначе вернет FALSE</returns>
        /// <remarks>
        /// Если все названные текстуры являются резидентными, glAreTexturesResident возвращает <see cref="GL_TRUE"/>, и содержимое резиденций не нарушается. 
        /// Если не все названные текстуры являются резидентными, glAreTexturesResident возвращает <see cref="GL_FALSE"/>, а подробный статус возвращается в n элементах резиденций. 
        /// Если элементом резидентности является <see cref="GL_TRUE"/>, то текстура, названная соответствующим элементом текстур, является резидентной.
        /// Статус резидентности одной связанной текстуры также может быть запрошен путем вызова glGetTexParameter с целевым аргументом, установленным на цель, с которой связана текстура, и аргументом pname, установленным на <see cref="GL_TEXTURE_RESIDENT"/>.
        /// Это единственный способ получить статус резидентности текстуры по умолчанию.
        /// <para>
        /// Errors
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_VALUE"/></term>
        /// <description>генерируется, если n отрицательно</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_VALUE"/></term>
        /// <description>генерируется, если какой-либо элемент в текстурах равен 0 или не имеет текстуры.В этом случае функция возвращает GL_FALSE, а содержимое резиденций является неопределенным</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>генерируется, если glAreTexturesResident выполняется между выполнением <see cref="glBegin"/> и соответствующим выполнением <see cref="glEnd"/></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe GLboolean glAreTexturesResident(GLsizei n, GLuint* textures, GLboolean* residences);

        /// <summary>
        /// Выводит на экран вершину (точку) из указанного элемента массива вершин.
        /// </summary>
        /// <param name="i">
        /// i - Число указывает индекс из массива вершин
        /// </param>
        /// <remarks>
        /// /// Команды glArrayElement используются в парах <see cref="glBegin"/> / <see cref="glEnd"/> для указания данных вершин и атрибутов для точечных, линейных и полигональных примитивов. 
        /// Если <see cref="GL_VERTEX_ARRAY"/> включен, когда вызывается glArrayElement, рисуется одна вершина, используя данные вершин и атрибутов, взятые из местоположения i включенных массивов. 
        /// Если <see cref="GL_VERTEX_ARRAY"/> не включен, рисование не происходит, но атрибуты, соответствующие активированным массивам, модифицируются.
        /// Используйте glArrayElement для создания примитивов путем индексации данных вершин, а не потоковой передачи массивов данных в порядке от первого до последнего. 
        /// Поскольку каждый вызов задает только одну вершину, можно явно указать атрибуты для каждого примитива, например, одну нормаль для каждого треугольника.
        /// Изменения, внесенные в массив данных между выполнением <see cref="glBegin"/> и соответствующим выполнением <see cref="glEnd"/>, могут повлиять на вызовы glArrayElement, которые выполняются в течение того же периода glBegin / glEnd не последовательными способами. 
        /// То есть вызов glArrayElement, который предшествует изменению данных массива, может получить доступ к измененным данным, а вызов, который следует за изменением данных массива, может получить доступ к исходным данным.
        /// <para>
        /// Errors
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_VALUE"/></term>
        /// <description>генерируется, если n отрицательно</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>генерируется, если ненулевое имя объекта буфера привязано к включенному массиву, а хранилище данных объекта буфера в настоящее время сопоставлено</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glArrayElement(GLint i);

        /// <summary>
        /// Начинает блок вершин примитива или группы примитивов.
        /// </summary>
        /// <param name="mode">
        /// <para>Значение, определяющее тип примитива или примитивов, которые будут созданы из вершин.</para>
        /// <para>Может принимать одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_POINTS"/></term>
        /// <description>Каждую вершину необходимо обрабатывать как одну точку.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_LINES"/></term>
        /// <description>Каждую пару вершин необходимо обрабатывать как независимый отрезок.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_LINE_LOOP"/></term>
        /// <description>Вершины необходимо обрабатывать как связанную группу отрезков линии от первой вершины до последней, затем обратно до первой.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_LINE_STRIP"/></term>
        /// <description>Вершины необходимо обрабатывать как связанную группу отрезков линии от первой вершины до последней.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_TRIANGLES"/></term>
        /// <description>Каждую тройку вершин необходимо обрабатывать как независимый треугольник.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_TRIANGLE_STRIP"/></term>
        /// <description>
        /// Вершины необходимо обрабатывать как связанную группу треугольников от первой вершины до последней.
        /// Один треугольник определен для каждой вершины, начиная с третьей.
        /// Треугольник определяется тремя последовательными вершинами.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_TRIANGLE_FAN"/></term>
        /// <description>
        /// Вершины необходимо обрабатывать как связанную группу треугольников от первой вершины до последней.
        /// Один треугольник определен для каждой вершины, начиная с третьей.
        /// Треугольник определяется первой вершиной и двумя последовательными вершинами.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_QUADS"/></term>
        /// <description>Каждые четыре вершины необходимо обрабатывать как независимый четырехугольник.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_QUAD_STRIP"/></term>
        /// <description>
        /// Вершины необходимо обрабатывать как связанную группу четырехугольников от первой вершины до последней.
        /// Один четырехугольник определен для каждой вершины, начиная с четвёртой.
        /// Четырехугольник определяется четырьмя последовательными вершинами.
        /// </description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_POLYGON"/></term>
        /// <description>Вершины необходимо обрабатывать как многоугольник.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </param>
        /// <remarks>
        /// <para>Между <see cref="glBegin"/> и <see cref="glEnd"/> можно только следующее подмножество команд:</para>
        /// <para>
        /// <list type="bullet">
        /// <item><see cref="glVertex2d"/></item>
        /// <item><see cref="glVertex2dv"/></item>
        /// <item><see cref="glVertex2f"/></item>
        /// <item><see cref="glVertex2fv"/></item>
        /// <item><see cref="glVertex2i"/></item>
        /// <item><see cref="glVertex2iv"/></item>
        /// <item><see cref="glVertex2s"/></item>
        /// <item><see cref="glVertex2sv"/></item>
        /// <item><see cref="glVertex3d"/></item>
        /// <item><see cref="glVertex3dv"/></item>
        /// <item><see cref="glVertex3f"/></item>
        /// <item><see cref="glVertex3fv"/></item>
        /// <item><see cref="glVertex3i"/></item>
        /// <item><see cref="glVertex3iv"/></item>
        /// <item><see cref="glVertex3s"/></item>
        /// <item><see cref="glVertex3sv"/></item>
        /// <item><see cref="glVertex4d"/></item>
        /// <item><see cref="glVertex4dv"/></item>
        /// <item><see cref="glVertex4f"/></item>
        /// <item><see cref="glVertex4fv"/></item>
        /// <item><see cref="glVertex4i"/></item>
        /// <item><see cref="glVertex4iv"/></item>
        /// <item><see cref="glVertex4s"/></item>
        /// <item><see cref="glVertex4sv"/></item>
        /// <item><see cref="glColor3b"/></item>
        /// <item><see cref="glColor3bv"/></item>
        /// <item><see cref="glColor3d"/></item>
        /// <item><see cref="glColor3dv"/></item>
        /// <item><see cref="glColor3f"/></item>
        /// <item><see cref="glColor3fv"/></item>
        /// <item><see cref="glColor3i"/></item>
        /// <item><see cref="glColor3iv"/></item>
        /// <item><see cref="glColor3s"/></item>
        /// <item><see cref="glColor3sv"/></item>
        /// <item><see cref="glColor3ub"/></item>
        /// <item><see cref="glColor3ubv"/></item>
        /// <item><see cref="glColor3ui"/></item>
        /// <item><see cref="glColor3uiv"/></item>
        /// <item><see cref="glColor3us"/></item>
        /// <item><see cref="glColor3usv"/></item>
        /// <item><see cref="glColor4b"/></item>
        /// <item><see cref="glColor4bv"/></item>
        /// <item><see cref="glColor4d"/></item>
        /// <item><see cref="glColor4dv"/></item>
        /// <item><see cref="glColor4f"/></item>
        /// <item><see cref="glColor4fv"/></item>
        /// <item><see cref="glColor4i"/></item>
        /// <item><see cref="glColor4iv"/></item>
        /// <item><see cref="glColor4s"/></item>
        /// <item><see cref="glColor4sv"/></item>
        /// <item><see cref="glColor4ub"/></item>
        /// <item><see cref="glColor4ubv"/></item>
        /// <item><see cref="glColor4ui"/></item>
        /// <item><see cref="glColor4uiv"/></item>
        /// <item><see cref="glColor4us"/></item>
        /// <item><see cref="glColor4usv"/></item>
        /// <item><see cref="glIndexd"/></item>
        /// <item><see cref="glIndexdv"/></item>
        /// <item><see cref="glIndexf"/></item>
        /// <item><see cref="glIndexfv"/></item>
        /// <item><see cref="glIndexi"/></item>
        /// <item><see cref="glIndexiv"/></item>
        /// <item><see cref="glNormal3b"/></item>
        /// <item><see cref="glNormal3bv"/></item>
        /// <item><see cref="glNormal3d"/></item>
        /// <item><see cref="glNormal3dv"/></item>
        /// <item><see cref="glNormal3f"/></item>
        /// <item><see cref="glNormal3fv"/></item>
        /// <item><see cref="glNormal3i"/></item>
        /// <item><see cref="glNormal3iv"/></item>
        /// <item><see cref="glNormal3s"/></item>
        /// <item><see cref="glNormal3sv"/></item>
        /// <item><see cref="glTexCoord1d"/></item>
        /// <item><see cref="glTexCoord1dv"/></item>
        /// <item><see cref="glTexCoord1f"/></item>
        /// <item><see cref="glTexCoord1fv"/></item>
        /// <item><see cref="glTexCoord1i"/></item>
        /// <item><see cref="glTexCoord1iv"/></item>
        /// <item><see cref="glTexCoord1s"/></item>
        /// <item><see cref="glTexCoord1sv"/></item>
        /// <item><see cref="glTexCoord2d"/></item>
        /// <item><see cref="glTexCoord2dv"/></item>
        /// <item><see cref="glTexCoord2f"/></item>
        /// <item><see cref="glTexCoord2fv"/></item>
        /// <item><see cref="glTexCoord2i"/></item>
        /// <item><see cref="glTexCoord2iv"/></item>
        /// <item><see cref="glTexCoord2s"/></item>
        /// <item><see cref="glTexCoord2sv"/></item>
        /// <item><see cref="glTexCoord3d"/></item>
        /// <item><see cref="glTexCoord3dv"/></item>
        /// <item><see cref="glTexCoord3f"/></item>
        /// <item><see cref="glTexCoord3fv"/></item>
        /// <item><see cref="glTexCoord3i"/></item>
        /// <item><see cref="glTexCoord3iv"/></item>
        /// <item><see cref="glTexCoord3s"/></item>
        /// <item><see cref="glTexCoord3sv"/></item>
        /// <item><see cref="glTexCoord4d"/></item>
        /// <item><see cref="glTexCoord4dv"/></item>
        /// <item><see cref="glTexCoord4f"/></item>
        /// <item><see cref="glTexCoord4fv"/></item>
        /// <item><see cref="glTexCoord4i"/></item>
        /// <item><see cref="glTexCoord4iv"/></item>
        /// <item><see cref="glTexCoord4s"/></item>
        /// <item><see cref="glTexCoord4sv"/></item>
        /// <item><see cref="glEvalCoord1d"/></item>
        /// <item><see cref="glEvalCoord1dv"/></item>
        /// <item><see cref="glEvalCoord1f"/></item>
        /// <item><see cref="glEvalCoord1fv"/></item>
        /// <item><see cref="glEvalCoord2d"/></item>
        /// <item><see cref="glEvalCoord2dv"/></item>
        /// <item><see cref="glEvalCoord2f"/></item>
        /// <item><see cref="glEvalCoord2fv"/></item>
        /// <item><see cref="glEvalPoint1"/></item>
        /// <item><see cref="glEvalPoint2"/></item>
        /// <item><see cref="glArrayElement"/></item>
        /// <item><see cref="glMaterialf"/></item>
        /// <item><see cref="glMaterialfv"/></item>
        /// <item><see cref="glMateriali"/></item>
        /// <item><see cref="glMaterialiv"/></item>
        /// <item><see cref="glEdgeFlag"/></item>
        /// <item><see cref="glEdgeFlagv"/></item>
        /// <item><see cref="glCallList"/></item>
        /// <item><see cref="glCallLists"/></item>
        /// </list>
        /// </para>
        /// <para>
        /// Если между <see cref="glBegin"/> и <see cref="glEnd"/> выполняется какая-либо другая команда,
        /// устанавливается флаг ошибки, и команда игнорируется.
        /// </para>
        /// <para>
        /// Независимо от значения, выбранного для режима, количество вершин,
        /// которые могут быть определены между <see cref="glBegin"/> и <see cref="glEnd"/>, не ограничено.
        /// </para>
        /// <para>
        /// Линии, треугольники, четырехугольники и многоугольники, которые указаны не полностью, не отображаются.
        /// Неполная спецификация возникает, когда либо слишком мало вершин,
        /// чтобы указать даже один примитив, либо когда указано неверное множество вершин. 
        /// Неполный примитив игнорируется; остальные отрисовываются.
        /// </para>
        /// <para>В случае ошибки функция <see cref="glGetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_INVALID_ENUM"/></term>
        /// <description>В параметре <paramref name="mode"/> передано недопустимое значение.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description><see cref="glBegin"/> вызвана между <see cref="glBegin"/> и соответствующим выполнением <see cref="glEnd"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <seealso cref="glEnd"/>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glBegin(GLenum mode);

        /// <summary>
        /// Выбирает указанную текстуру как активную для наложения ее на объекты.
        /// </summary>
        /// <param name="target">
        /// GLuint texture - имя текстуры обычно это целое число.
        /// </param>
        /// <param name="texture">
        /// Флаг GLenum target может принимать следующий параметр:
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_TEXTURE_1D"/></term>
        /// <description>Одномерная текстура</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_TEXTURE_2D"/></term>
        /// <description>Двухмерная текстура</description>
        /// </item>
        /// <item>
        /// <term>GL_TEXTURE_3D</term>
        /// <description>Трехмерная текстура (не определены в коде библиотеки)</description>
        /// </item>
        /// <item>
        /// <term>GL_TEXTURE_CUBE_MAP</term>
        /// <description>Кубическая текстура (не определены в коде библиотеки)</description>
        /// </item>
        /// </list>
        /// </para>
        /// </param>      
        /// <remarks>
        /// Имена текстур представляют собой целые числа без знака. 
        /// Нулевое значение зарезервировано для представления текстуры по умолчанию для каждой цели текстуры. 
        /// Имена текстур и соответствующее содержимое текстур являются локальными по отношению к общему пространству списка отображения (см. GlXCreateContext) текущего контекста рендеринга GL; два контекста рендеринга совместно используют имена текстур, только если они также совместно используют списки отображения.
        /// Пока текстура связана, операции GL с целью, с которой она связана, влияют на связанную текстуру, а запросы цели, с которой она связана, возвращают состояние из связанной текстуры. 
        /// Если на цели, к которой привязана текстура, активно наложение текстуры, используется связанная текстура. 
        /// По сути, цели текстур становятся псевдонимами текстур, привязанных к ним в данный момент, а нулевое имя текстуры относится к текстурам по умолчанию, которые были привязаны к ним при инициализации.
        /// Привязка текстуры, созданная с помощью glBindTexture, остается активной до тех пор, пока другая текстура не будет привязана к той же цели, или пока связанная текстура не будет удалена с помощью glDeleteTextures.
        /// <para>
        /// Errors
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_ENUM"/></term>
        /// <description>генерируется, если цель не является одним из допустимых значений</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>генерируется, если текстура была ранее создана с целью, которая не соответствует цели</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>генерируется, если glAlphaFunc выполняется между выполнением <see cref="glBegin"/> и соответствующим выполнением <see cref="glEnd"/></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glBindTexture(GLenum target, GLuint texture);

        /// <summary>
        /// Отображает/рисует битовый рисунок.
        /// </summary>
        /// <param name="width">
        /// width - Значение ширины в пикселях растрового изображения
        /// </param>
        /// <param name="height">
        /// height - значение высоты в пикселях растрового изображения
        /// </param>
        /// <param name="xorig">
        /// xorig - Значения расположение источника растрового изображения. Происхождение измеряется от нижнего левого угла растрового изображения.
        /// </param>
        /// <param name="yorig">
        /// yorig - Значения расположение источника растрового изображения. Происхождение измеряется от нижнего левого угла растрового изображения.
        /// </param>
        /// <param name="xmove">
        /// xmove - Значение X смещения будут добавлены к текущей растровой позиции.
        /// </param>
        /// <param name="ymove">
        /// ymove - Значение Y смещения будут добавлены к текущей растровой позиции.
        /// </param>
        /// <param name="bitmap">
        /// bitmap - Адрес растрового изображения. Массив который хранит растровый рисунок.
        /// </param>
        /// <remarks>
        /// glBitmap принимает семь аргументов. Первая пара определяет ширину и высоту растрового изображения. 
        /// Вторая пара указывает местоположение источника растрового изображения относительно нижнего левого угла растрового изображения. 
        /// Третья пара аргументов определяет смещения x и y, которые будут добавлены к текущей позиции растра после того, как растровое изображение было нарисовано. Последний аргумент - указатель на само растровое изображение.
        /// Если ненулевой именованный объект буфера привязан к цели GL_PIXEL_UNPACK_BUFFER (см. GlBindBuffer), когда задано растровое изображение, растровое изображение обрабатывается как смещение байта в хранилище данных объекта буфера.
        /// Растровое изображение интерпретируется как данные изображения для команды <see cref="glDrawPixels"/> с шириной и высотой, соответствующей аргументам ширины и высоты этой команды, и с типом, установленным в <see cref="GL_BITMAP"/>, и форматом, установленным в <see cref="GL_COLOR_INDEX"/>. 
        /// Режимы, указанные с помощью glPixelStore, влияют на интерпретацию данных растрового изображения; режимы, указанные с помощью glPixelTransfer, не имеют.
        /// <para>
        /// Чтобы установить действительную позицию растра за пределами области просмотра, сначала установите действительную позицию растра внутри области просмотра, затем вызовите <see cref="glBitmap"/> с NULL в качестве параметра растрового изображения и с xmove и ymove, установленными на смещения новой позиции растра. 
        /// Этот метод полезен при панорамировании изображения вокруг области просмотра.
        /// </para>
        /// <para>
        /// Errors
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_VALUE"/></term>
        /// <description>генерируется, если ширина или высота отрицательны</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>генерируется, если ненулевое имя объекта буфера привязано к цели GL_PIXEL_UNPACK_BUFFER, и хранилище данных объекта буфера в настоящее время сопоставлено</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>генерируется, если ненулевое имя объекта буфера привязано к целевому объекту GL_PIXEL_UNPACK_BUFFER, и данные будут распакованы из объекта буфера, так что требуемые чтения памяти превысят размер хранилища данных</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>генерируется, если glAlphaFunc выполняется между выполнением <see cref="glBegin"/> и соответствующим выполнением <see cref="glEnd"/></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glBitmap(GLsizei width, GLsizei height, GLfloat xorig, GLfloat yorig, GLfloat xmove, GLfloat ymove, GLubyte* bitmap);

        /// <summary>
        /// Задает функцию смешивания цветов.
        /// </summary>
        /// <param name="sfactor">
        /// sfactor - Имя функции для обработки входящего цвета.
        /// sfactor - Определяет, как вычисляются коэффициенты смешивания красного, зеленого, синего и альфа-источника. 
        /// Следующие символьные константы принимаются: GL_ZERO, GL_ONE, GL_SRC_COLOR, GL_ONE_MINUS_SRC_COLOR, GL_DST_COLOR, GL_ONE_MINUS_DST_COLOR, GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA, GL_DST_ALPHA, GL_ONE_MINUS_DST_ALPHA, GL_CONSTANT_COLOR, GL_ONE_MINUS_CONSTANT_COLOR, GL_CONSTANT_ALPHA, GL_ONE_MINUS_CONSTANT_ALPHA и GL_SRC_ALPHA_SATURATE. Начальное значение GL_ONE.
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_ZERO"/></term>
        /// <description>Результат равен (0, 0, 0, 0)</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_ONE"/></term>
        /// <description>Результат равен (1, 1, 1, 1)</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_SRC_COLOR"/></term>
        /// <description>Результат равен (Rs, Gs, Bs, As)</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_ONE_MINUS_SRC_COLOR"/></term>
        /// <description>Результат равен (1, 1, 1, 1) - (Rs, Gs, Bs, As)</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_DST_COLOR"/></term>
        /// <description>Результат равен (Rd, Gd, Bd, Ad)</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_ONE_MINUS_DST_COLOR"/></term>
        /// <description>Результат равен (1, 1, 1, 1) - (Rd, Gd, Bd, Ad)</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_SRC_ALPHA"/></term>
        /// <description>Результат равен (As, As, As, As)</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_ONE_MINUS_SRC_ALPHA"/></term>
        /// <description>Результат равен (1, 1, 1, 1) - (As, As, As, As)</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_DST_ALPHA"/></term>
        /// <description>Результат равен (Ad, Ad, Ad, Ad)</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_ONE_MINUS_DST_ALPHA"/></term>
        /// <description>Результат равен (1, 1, 1, 1) - (Ad, Ad, Ad, Ad)</description>
        /// </item>
        /// <item>
        /// <term>GL_CONSTANT_COLOR</term>
        /// <description>Результат равен (Rc, Gc, Bc, Ac)</description>
        /// </item>
        /// <item>
        /// <term>GL_ONE_MINUS_CONSTANT_COLOR</term>
        /// <description>Результат равен (1, 1, 1, 1) - (Ac, Ac, Ac, Ac)</description>
        /// </item>
        /// <item>
        /// <term>GL_CONSTANT_ALPHA</term>
        /// <description>Результат равен (Ac, Ac, Ac, Ac)</description>
        /// </item>
        /// <item>
        /// <term>GL_ONE_MINUS_CONSTANT_ALPHA</term>
        /// <description>Результат равен (1, 1, 1, 1) - (Ac, Ac, Ac, Ac)</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_SRC_ALPHA_SATURATE"/></term>
        /// <description>Результат равен (f, f, f, f)</description>
        /// </item>
        /// </list>
        /// </para>
        /// </param>
        /// <param name="dfactor">
        /// </param>
        /// <remarks>
        /// В режиме RGBA пиксели могут быть нарисованы с использованием функции, которая смешивает входящие (исходные) значения RGBA со значениями RGBA, которые уже находятся в буфере кадра (целевые значения). Смешивание изначально отключено. 
        /// Используйте glEnable и glDisable с аргументом GL_BLEND, чтобы включить или отключить смешивание.
        /// </remarks>
        /// <seealso href="https://www.khronos.org/registry/OpenGL-Refpages/gl2.1/"/>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glBlendFunc(GLenum sfactor, GLenum dfactor);

        /// <summary>
        /// Функция выполняет команды указанного списка отображения.
        /// </summary>
        /// <param name="list">
        /// list -  Уникальный номер списка
        /// </param>
        /// <remarks>
        /// glCallList вызывает выполнение именованного списка отображения. 
        /// Команды, сохраненные в списке отображения, выполняются по порядку, как если бы они были вызваны без использования списка отображения. 
        /// Если список не был определен как список отображения, glCallList игнорируется.
        /// glCallList может появиться внутри списка отображения.Чтобы исключить возможность бесконечной рекурсии в результате вызова списков отображения, существует ограничение на уровень вложенности списков отображения во время выполнения списка отображения. 
        /// Это ограничение составляет не менее 64, и это зависит от реализации.
        /// Состояние GL не сохраняется и не восстанавливается при вызове glCallList. 
        /// Таким образом, изменения, внесенные в состояние GL во время выполнения списка отображения, остаются после завершения выполнения списка отображения. Используйте <see cref="glPushAttrib"/>, <see cref="glPopAttrib"/>, <see cref="glPushMatrix"/> и <see cref="glPopMatrix"/> для сохранения состояния GL при вызовах <see cref="glCallList"/>.
        /// Списки отображения могут выполняться между вызовом glBegin и соответствующим вызовом glEnd, при условии, что список отображения включает только команды, разрешенные в этом интервале.
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glCallList(GLuint list);

        /// <summary>
        /// Выполняет список списков отображения
        /// </summary>
        /// <param name="n">
        /// Количество отображаемых списков для выполнения
        /// </param>
        /// <param name="type">
        /// Тип значений в списках. Следующие символические константы принимаются
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_BYTE"/></term>
        /// <description>Параметр lists обрабатывается как массив байтов со знаком, каждый в диапазоне от -128 до 127</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_UNSIGNED_BYTE"/></term>
        /// <description>Параметр lists обрабатывается как массив байтов без знака, каждый из которых находится в диапазоне от 0 до 255</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_SHORT"/></term>
        /// <description>Параметр lists обрабатывается как массив 2-байтовых целых чисел со знаком, каждое из которых находится в диапазоне от -32768 до 32767</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_UNSIGNED_SHORT"/></term>
        /// <description>Параметр lists обрабатывается как массив 2-байтовых целых чисел без знака, каждое из которых находится в диапазоне от 0 до 65535</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INT"/></term>
        /// <description>Параметр lists обрабатывается как массив 4-байтовых целых чисел со знаком</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_UNSIGNED_INT"/></term>
        /// <description>Параметр lists обрабатывается как массив 4-байтовых целых чисел без знака</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_FLOAT"/></term>
        /// <description>Параметр lists обрабатывается как массив 4-байтовых значений с плавающей запятой</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_2_BYTES"/></term>
        /// <description>Параметр lists обрабатывается как массив байтов без знака. Каждая пара байтов определяет одно имя списка отображения. Значение пары вычисляется как 256-кратное значение без знака первого байта плюс значение без знака второго байта</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_3_BYTES"/></term>
        /// <description>Параметр lists обрабатывается как массив байтов без знака. Каждый триплет байтов определяет одно имя списка отображения. Значение триплета вычисляется как 65536, умноженное на значение без знака первого байта, плюс 256 раз без знака без знака второго байта, плюс значение без знака третьего байта</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_4_BYTES"/></term>
        /// <description>Параметр lists обрабатывается как массив байтов без знака. Каждая четверка байтов задает одно имя списка отображения. Значение четверки вычисляется как 16777216 раз значение без знака первого байта, плюс 65536 раз значение без знака второго байта, плюс 256 раз значение без знака третьего байта, плюс значение без знака четвертого байта</description>
        /// </item>
        /// </list>
        /// </para>
        /// </param>
        /// <param name="lists">
        /// Адрес массива смещений имен в списке отображения. Тип указателя является недействительным, поскольку смещения могут быть байтами, короткими замыканиями, целыми числами или числами с плавающей запятой, в зависимости от значения типа
        /// </param>
        /// <remarks>
        ///<para>
        ///Функция glCallLists обеспечивает выполнение каждого списка отображения в списке имен, переданных в виде списков. В результате функции, сохраненные в каждом списке отображения, выполняются по порядку, как если бы они были вызваны без использования списка отображения. Имена списков отображения, которые не были определены, игнорируются
        ///</para>
        ///<para>
        ///Функция glCallLists предоставляет эффективные средства для выполнения списков отображения. Параметр n указывает количество списков с различными форматами имен (указанных параметром типа), которые выполняет glCallLists
        ///</para>
        ///<para>
        ///Список имен отображаемых списков не заканчивается нулем. Скорее, n указывает, сколько имен нужно взять из списков.
        ///</para>
        ///<para>
        ///Функция glListBase делает доступным дополнительный уровень косвенности. Функция <see cref="glListBase"/> определяет беззнаковое смещение, которое добавляется к каждому имени списка отображения, указанному в списках, перед выполнением этого списка отображения
        ///</para>
        ///<para>
        ///Функция glCallLists может появляться внутри списка отображения. Чтобы избежать возможности бесконечной рекурсии в результате вызова списков отображения друг друга, на уровне вложенности списков отображения накладывается ограничение во время выполнения списка отображения. Этот предел должен быть не менее 64, и это зависит от реализации
        ///</para>
        ///<para>
        ///Состояние OpenGL не сохраняется и не восстанавливается при вызове glCallLists. Таким образом, изменения, внесенные в состояние OpenGL во время выполнения списков отображения, остаются после завершения выполнения. Используйте <see cref="glPushAttrib"/>, <see cref="glPopAttrib"/>, <see cref="glPushMatrix"/> и <see cref="glPopMatrix"/> для сохранения состояния OpenGL при вызовах glCallLists
        ///</para>
        ///<para>
        ///Вы можете выполнить списки отображения между вызовом <see cref="glBegin"/> и соответствующим вызовом <see cref="glEnd"/>, если список отображения включает только функции, которые разрешены в этом интервале
        ///</para>
        ///<para>
        ///Следующие функции получают информацию, относящуюся к функции glCallLists:
        ///<para>glGet с аргументом <see cref="GL_LIST_BASE"/></para>
        ///<para>glGet с аргументом <see cref="GL_MAX_LIST_NESTING"/></para>
        ///<para><see cref="glIsList"/></para>
        ///</para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glCallLists(GLsizei n, GLenum type, void* lists);

        /// <summary>
        /// Очищает буферы.
        /// </summary>
        /// <param name="mask">
        /// <para>Маски очищаемых буферов.</para>
        /// <para>Может принимать одно или несколько значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_COLOR_BUFFER_BIT"/></term>
        /// <description>цветовой буфер</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_DEPTH_BUFFER_BIT"/></term>
        /// <description>буфер глубины</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_ACCUM_BUFFER_BIT"/></term>
        /// <description>буфер накопления</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_STENCIL_BUFFER_BIT"/></term>
        /// <description>трафаретный буфер</description>
        /// </item>
        /// </list>
        /// </para>
        /// </param>
        /// <remarks>
        /// <para>Следующие коды ошибок могут быть возвращаены функцией <see cref="glGetError"/>:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_INVALID_VALUE"/></term>
        /// <description>В параметре <paramref name="mask"/> был установлен бит отличный от четырех определенных битов.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Функция была вызвана между вызовом <see cref="glBegin"/> и соответствующим вызовом <see cref="glEnd"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glClear(GLbitfield mask);

        /// <summary>
        /// Функция определяет значения очистки для буфера накопления
        /// </summary>
        /// <param name="red">
        /// Значение красного цвета, используемое при очистке буфера накопления. Значение по умолчанию равно нулю
        /// </param>
        /// <param name="green">
        /// Значение зеленого цвета, используемое при очистке буфера накопления. Значение по умолчанию равно нулю
        /// </param>
        /// <param name="blue">
        /// Значение синего цвета используется при очистке буфера накопления. Значение по умолчанию равно нулю
        /// </param>
        /// <param name="alpha">
        /// Значение альфа используется при очистке буфера накопления. Значение по умолчанию равно нулю
        /// </param>
        /// <remarks>
        /// <para>
        /// Функция glClearAccum определяет значения красного, зеленого, синего и альфа, используемые <see cref="glClear"/> для очистки буфера накопления.
        /// </para>
        /// <para>
        /// Значения, указанные в glClearAccum, ограничены диапазоном [1,1]
        /// </para>
        /// <para>
        /// Следующая функция извлекает информацию, связанную с glClearAccum:
        /// </para>
        /// <para>
        /// glGet с аргументом <see cref="GL_ACCUM_CLEAR_VALUE"/>
        /// </para>
        /// <para>
        /// Errors
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Функция была вызвана между вызовом <see cref="glBegin"/> и соответствующим вызовом <see cref="glEnd"/></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glClearAccum(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);

        /// <summary>
        /// Задаёт цвет, которой используется для очистки цветового буффера.
        /// </summary>
        /// <param name="red">
        /// Значение красной составялющей.
        /// </param>
        /// <param name="green">
        /// Значение зелёной составялющей.
        /// </param>
        /// <param name="blue">
        /// Значение синей составялющей.
        /// </param>
        /// <param name="alpha">
        /// Значений альфа-канала.
        /// </param>
        /// <remarks>
        /// <para>Следующие коды ошибок могут быть возвращаены функцией <see cref="glGetError"/>:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Функция была вызвана между вызовом <see cref="glBegin"/> и соответствующим вызовом <see cref="glEnd"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glClearColor(GLclampf red, GLclampf green, GLclampf blue, GLclampf alpha);

        /// <summary>
        /// Функция определяет значение очистки для буфера глубины
        /// </summary>
        /// <param name="depth">
        /// Значение глубины, используемое при очистке буфера глубины
        /// </param>
        /// <remarks>
        /// <para>
        /// Функция glClearDepth определяет значение глубины, используемое glClear для очистки буфера глубины. Значения, указанные в glClearDepth, ограничены диапазоном [0,1].
        /// </para>
        /// Следующая функция извлекает информацию, связанную с функцией glClearDepth:
        /// <para>glGet с аргументом <see cref="GL_DEPTH_CLEAR_VALUE"/></para>
        /// <para>
        /// Errors
        /// </para>
        /// <para>
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Функция была вызвана между вызовом <see cref="glBegin"/> и соответствующим вызовом <see cref="glEnd"/></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glClearDepth(GLclampd depth);


        /// <summary>
        /// Функция определяет значение очистки для буферов индекса цвета
        /// </summary>
        /// <param name="c">
        /// Индекс, используемый при очистке буферов индекса цвета. Значение по умолчанию равно нулю
        /// </param>
        /// <remarks>
        /// <para>
        /// Функция glClearIndex указывает индекс, используемый <see cref="glClear"/> для очистки буферов индекса цвета. Параметр c не зафиксирован. Скорее, c преобразуется в значение с фиксированной точкой с неопределенной точностью справа от двоичной точки. Целочисленная часть этого значения затем маскируется с помощью 2m - 1, где m - количество битов в индексе цвета, хранящемся в буфере кадров.
        /// </para>
        /// <para>
        /// Следующие функции получают информацию, связанную с glClearIndex:
        /// </para>
        /// <para>
        /// glGet с аргументом <see cref="GL_INDEX_CLEAR_VALUE"/>
        /// </para>
        /// <para>
        /// glGet с аргументом <see cref="GL_INDEX_BITS"/>
        /// </para>
        /// <para>
        /// Errors
        /// </para>
        /// <para>
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Функция была вызвана между вызовом <see cref="glBegin"/> и соответствующим вызовом <see cref="glEnd"/></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glClearIndex(GLfloat c);

        /// <summary>
        /// Функция определяет значение очистки для буфера трафарета
        /// </summary>
        /// <param name="s">
        /// Индекс, используемый при очистке буфера трафарета. Значение по умолчанию равно нулю
        /// </param>
        /// <remarks>
        /// Функция glClearStencil указывает индекс, используемый <see cref="glClear"/> для очистки буфера трафарета. Параметр s маскируется с помощью 2m - 1, где m - количество бит в буфере трафарета
        /// <para>
        /// Следующие функции извлекают информацию, связанную с функцией glClearStencil:
        /// </para>
        /// <para>
        /// glGet с аргументом <see cref="GL_STENCIL_CLEAR_VALUE"/>
        /// </para>
        /// <para>
        /// glGet с аргументом <see cref="GL_STENCIL_BITS"/>
        /// </para>
        /// <para>
        /// Errors
        /// </para>
        /// <para>
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Функция была вызвана между вызовом <see cref="glBegin"/> и соответствующим вызовом <see cref="glEnd"/></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glClearStencil(GLint s);


        /// <summary>
        /// функция определяет плоскость, к которой привязана вся геометрия
        /// </summary>
        /// <param name="plane">
        /// Плоскость отсечения, которая позиционируется. Символьные имена в форме GL_CLIP_PLANEi, где i является целым числом от 0 до GL_MAX_CLIP_PLAN
        /// </param>
        /// <param name="equation">
        /// Адрес массива из четырех значений с плавающей запятой двойной точности. Эти значения интерпретируются как плоское уравнение
        /// </param>
        /// <remarks>
        /// <para>
        /// Геометрия всегда ограничена границами усечённости шести плоскостей по x, y и z. Функция glClipPlane позволяет задавать дополнительные плоскости, не обязательно перпендикулярные оси X, Y или Z, относительно которых ограничена вся геометрия. 
        /// Можно указать до <see cref="GL_MAX_CLIP_PLANES"/> плоскостей, где GL_MAX_CLIP_PLANES равно по крайней мере шести во всех реализациях. Поскольку результирующая область отсечения является пересечением определенных полупространств, она всегда является выпуклой.
        /// </para>
        /// <para>
        /// Функция glClipPlane определяет полупространство, используя четырехкомпонентное плоское уравнение. Когда вы вызываете glClipPlane, уравнение преобразуется с помощью обратной матрицы просмотра модели и сохраняется в результирующих координатах глаза. 
        /// Последующие изменения в матрице вида модели не влияют на сохраненные компоненты уравнения плоскости. Если скалярное произведение координат глаза вершины с сохраненными компонентами уравнения плоскости положительно или равно нулю, вершина находится относительно этой плоскости отсечения. В противном случае это не так.
        /// </para>
        /// <para>
        /// Используйте функции <see cref="glEnable"/> и <see cref="glDisable"/> для включения и отключения плоскостей отсечения. 
        /// Вызвать плоскости отсечения с аргументом GL_CLIP_PLANEi, где i - номер плоскости.
        /// </para>
        /// <para>
        /// По умолчанию все плоскости отсечения определены как (0,0,0,0) в координатах глаза и отключены.
        ///Это всегда тот случай, когда GL_CLIP_PLANEi = <see cref="GL_CLIP_PLANE0"/> + i.
        /// </para>
        /// <para>
        /// Следующие функции получают информацию, связанную с glClipPlane:
        /// </para>
        /// <para>
        /// <see cref="glGetClipPlane"/>
        /// </para>
        /// <para>
        /// <see cref="glIsEnabled"/> с аргументом GL_CLIP_PLANE i
        /// </para>
        /// <para>
        /// Errors
        /// </para>
        /// <para>
        /// <list type="bullet">
        /// <item>
        /// <term><see cref="GL_INVALID_ENUM"/></term>
        /// <description>plane - был не принимаемым значением</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Функция была вызвана между вызовом <see cref="glBegin"/> и соответствующим вызовом <see cref="glEnd"/></description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glClipPlane(GLenum plane, GLdouble* equation);

        /// <summary>
        /// Функция устанавливает текущий цвет
        /// </summary>
        /// <param name="red">
        /// Новое значение красного для текущего цвета
        /// </param>
        /// <param name="green">
        /// Новое значение зеленого для текущего цвета
        /// </param>
        /// <param name="blue">
        /// Новое значение синего для текущего цвета
        /// </param>
        /// <remarks>
        /// <para>
        /// GL хранит как текущий однозначный индекс цвета, так и текущий четырехзначный цвет RGBA. glcolor устанавливает новый четырехзначный цвет RGBA. 
        /// У glcolor есть два основных варианта: glcolor3 и glcolor4. Варианты glcolor3 явно задают новые значения красного, зеленого и синего цветов и неявно устанавливают текущее значение альфа-канала равным 1,0 (полная интенсивность). 
        /// Варианты glcolor4 явно указывают все четыре цветовых компонента
        /// </para>
        /// <para>
        /// В качестве аргументов glcolor3b, glcolor4b, glcolor3s, glcolor4s, glcolor3i и glcolor4i принимают три или четыре целых байта со знаком, короткие или длинные. 
        /// Когда к имени добавляется v, цветовые команды могут получить указатель на массив таких значений.
        /// </para>
        /// <para>
        /// Текущие значения цвета хранятся в формате с плавающей запятой с неопределенными размерами мантиссы и экспоненты. 
        /// Целочисленные компоненты цвета без знака, если они указаны, линейно отображаются на значения с плавающей точкой, так что наибольшее представимое значение отображается на 1,0 (полная интенсивность), а 0 - на 0,0 (нулевая интенсивность). 
        /// Целочисленные компоненты цвета со знаком, если они указаны, линейно отображаются в значения с плавающей запятой, так что наиболее положительное представимое значение отображается в 1,0, а наиболее отрицательное представимое значение - в -1,0. (Обратите внимание, что это отображение не преобразует 0 точно в 0.0.) 
        /// Значения с плавающей точкой отображаются напрямую.
        /// </para>
        /// <para>
        /// Ни значения с плавающей запятой, ни целые числа со знаком не ограничены диапазоном [0,1], пока текущий цвет не будет обновлен. 
        /// Однако цветовые компоненты фиксируются в этом диапазоне до того, как они будут интерполированы или записаны в буфер цвета.
        /// </para>>
        /// </remarks>
        /// <seealso cref="glBegin"/>
        /// <seealso cref="glEnd"/>
        /// <seealso cref="glGetBooleanv"/><seealso cref="glGetDoublev"/><seealso cref="glGetFloatv"/><seealso cref="glGetIntegerv"/>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glColor3b(GLbyte red, GLbyte green, GLbyte blue);


        /// <summary>
        /// Функция устанавливает текущий цвет из уже существующего массива значений цвета
        /// </summary>
        /// <param name="v">
        /// Указатель на массив, содержащий красные, зеленые и синие значения
        /// </param>
        /// <remarks>
        /// <para>
        /// GL хранит как текущий однозначный индекс цвета, так и текущий четырехзначный цвет RGBA. glcolor устанавливает новый четырехзначный цвет RGBA. 
        /// У glcolor есть два основных варианта: glcolor3 и glcolor4. 
        /// Варианты glcolor3 явно задают новые значения красного, зеленого и синего цветов и неявно устанавливают текущее значение альфа-канала равным 1,0 (полная интенсивность). 
        /// Варианты glcolor4 явно указывают все четыре цветовых компонента.
        /// </para>
        /// <para>
        /// В качестве аргументов glcolor3b, glcolor4b, glcolor3s, glcolor4s, glcolor3i и glcolor4i принимают три или четыре целых байта со знаком, короткие или длинные. 
        /// Когда к имени добавляется v, цветовые команды могут получить указатель на массив таких значений.
        /// </para>
        /// <para>
        /// Текущие значения цвета хранятся в формате с плавающей запятой с неопределенными размерами мантиссы и экспоненты. 
        /// Целочисленные компоненты цвета без знака, если они указаны, линейно отображаются на значения с плавающей точкой, так что наибольшее представимое значение отображается на 1,0 (полная интенсивность), а 0 - на 0,0 (нулевая интенсивность). 
        /// Целочисленные компоненты цвета со знаком, если они указаны, линейно отображаются в значения с плавающей запятой, так что наиболее положительное представимое значение отображается в 1,0, а наиболее отрицательное представимое значение - в -1,0. (Обратите внимание, что это отображение не преобразует 0 точно в 0.0.) 
        /// Значения с плавающей точкой отображаются напрямую.
        /// </para>
        /// <para>
        /// Ни значения с плавающей запятой, ни целые числа со знаком не ограничиваются диапазоном [0,1] до обновления текущего цвета. 
        /// Тем не менее, цветовые компоненты фиксируются в этом диапазоне, прежде чем они будут интерполированы или записаны в цветовой буфер.
        /// </para>
        /// </remarks>
        /// <seealso cref="glBegin"/>
        /// <seealso cref="glEnd"/>
        /// <seealso cref="glGetBooleanv"/><seealso cref="glGetDoublev"/><seealso cref="glGetFloatv"/><seealso cref="glGetIntegerv"/>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor3bv(GLbyte* v);

        /// <summary>
        /// Функция устанавливает текущий цвет
        /// </summary>
        /// <param name="red">Новое значение красного для текущего цвета</param>
        /// <param name="green">Новое значение зеленого для текущего цвета</param>
        /// <param name="blue">Новое значение синего для текущего цвета</param>
        /// <remarks>
        /// <para>
        /// GL хранит как текущий однозначный индекс цвета, так и текущий четырехзначный цвет RGBA. glcolor устанавливает новый четырехзначный цвет RGBA. 
        /// У glcolor есть два основных варианта: glcolor3 и glcolor4. 
        /// Варианты glcolor3 явно задают новые значения красного, зеленого и синего цветов и неявно устанавливают текущее значение альфа-канала равным 1,0 (полная интенсивность). 
        /// Варианты glcolor4 явно указывают все четыре цветовых компонента
        /// </para>
        /// <para>
        /// В качестве аргументов glcolor3b, glcolor4b, glcolor3s, glcolor4s, glcolor3i и glcolor4i принимают три или четыре целых байта со знаком, короткие или длинные. 
        /// Когда к имени добавляется v, цветовые команды могут получить указатель на массив таких значений.
        /// </para>
        /// <para>
        /// Текущие значения цвета хранятся в формате с плавающей запятой с неопределенными размерами мантиссы и экспоненты. 
        /// Целочисленные компоненты цвета без знака, если они указаны, линейно отображаются на значения с плавающей точкой, так что наибольшее представимое значение отображается на 1,0 (полная интенсивность), а 0 - на 0,0 (нулевая интенсивность). 
        /// Целочисленные компоненты цвета со знаком, если они указаны, линейно отображаются в значения с плавающей запятой, так что наиболее положительное представимое значение отображается в 1,0, а наиболее отрицательное представимое значение - в -1,0. (Обратите внимание, что это отображение не преобразует 0 точно в 0.0.) 
        /// Значения с плавающей точкой отображаются напрямую.
        /// </para>
        /// <para>
        /// Ни значения с плавающей запятой, ни целые числа со знаком не ограничиваются диапазоном [0,1] до обновления текущего цвета. 
        /// Тем не менее, цветовые компоненты фиксируются в этом диапазоне, прежде чем они будут интерполированы или записаны в цветовой буфер.
        /// </para>
        /// </remarks>
        /// <seealso cref="glBegin"/>
        /// <seealso cref="glEnd"/>
        /// <seealso cref="glGetBooleanv"/><seealso cref="glGetDoublev"/><seealso cref="glGetFloatv"/><seealso cref="glGetIntegerv"/>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColor3d(GLdouble red, GLdouble green, GLdouble blue);

        /// <summary>
        /// Функция устанавливает текущий цвет из уже существующего массива значений цвета
        /// </summary>
        /// <param name="v">
        /// Указатель на массив, содержащий красные, зеленые и синие значения
        /// </param>
        /// <remarks>
        /// <para>
        /// GL хранит как текущий однозначный индекс цвета, так и текущий четырехзначный цвет RGBA. glcolor устанавливает новый четырехзначный цвет RGBA. 
        /// У glcolor есть два основных варианта: glcolor3 и glcolor4. 
        /// Варианты glcolor3 явно задают новые значения красного, зеленого и синего цветов и неявно устанавливают текущее значение альфа-канала равным 1,0 (полная интенсивность). 
        /// Варианты glcolor4 явно указывают все четыре цветовых компонента
        /// </para>
        /// <para>
        /// В качестве аргументов glcolor3b, glcolor4b, glcolor3s, glcolor4s, glcolor3i и glcolor4i принимают три или четыре целых байта со знаком, короткие или длинные. 
        /// Когда к имени добавляется v, цветовые команды могут получить указатель на массив таких значений
        /// </para>
        /// <para>
        /// Текущие значения цвета хранятся в формате с плавающей запятой с неопределенными размерами мантиссы и экспоненты. 
        /// Целочисленные компоненты цвета без знака, если они указаны, линейно отображаются на значения с плавающей точкой, так что наибольшее представимое значение отображается на 1,0 (полная интенсивность), а 0 - на 0,0 (нулевая интенсивность). 
        /// Целочисленные компоненты цвета со знаком, если они указаны, линейно отображаются в значения с плавающей запятой, так что наиболее положительное представимое значение отображается в 1,0, а наиболее отрицательное представимое значение - в -1,0. (Обратите внимание, что это отображение не преобразует 0 точно в 0.0.) 
        /// Значения с плавающей точкой отображаются напрямую.
        /// </para>
        /// <para>
        /// Ни значения с плавающей запятой, ни целые числа со знаком не ограничиваются диапазоном [0,1] до обновления текущего цвета. Тем не менее, цветовые компоненты фиксируются в этом диапазоне, прежде чем они будут интерполированы или записаны в цветовой буфер.
        /// </para>
        /// </remarks>
        /// <seealso cref="glBegin"/>
        /// <seealso cref="glEnd"/>
        /// <seealso cref="glGetBooleanv"/><seealso cref="glGetDoublev"/><seealso cref="glGetFloatv"/><seealso cref="glGetIntegerv"/>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor3dv(GLdouble* v);

        /// <summary>
        /// Функция устанавливает текущий цвет
        /// </summary>
        /// <param name="red">
        /// Новое значение красного для текущего цвета.
        /// </param>
        /// <param name="green">
        /// Новое значение зеленого для текущего цвета.
        /// </param>
        /// <param name="blue">
        /// Новое значение синего для текущего цвета.
        /// </param>
        /// <remarks>
        /// <para>
        /// GL хранит как текущий однозначный индекс цвета, так и текущий четырехзначный цвет RGBA. glcolor устанавливает новый четырехзначный цвет RGBA. 
        /// У glcolor есть два основных варианта: glcolor3 и glcolor4. Варианты glcolor3 явно задают новые значения красного, зеленого и синего цветов и неявно устанавливают текущее значение альфа-канала равным 1,0 (полная интенсивность). 
        /// Варианты glcolor4 явно указывают все четыре цветовых компонента.
        /// </para>
        /// <para>
        /// В качестве аргументов glcolor3b, glcolor4b, glcolor3s, glcolor4s, glcolor3i и glcolor4i принимают три или четыре целых байта со знаком, короткие или длинные. 
        /// Когда к имени добавляется v, цветовые команды могут получить указатель на массив таких значений.
        /// </para>
        /// <para>
        /// Текущие значения цвета хранятся в формате с плавающей запятой с неопределенными размерами мантиссы и экспоненты. 
        /// Целочисленные компоненты цвета без знака, если они указаны, линейно отображаются на значения с плавающей точкой, так что наибольшее представимое значение отображается на 1,0 (полная интенсивность), а 0 - на 0,0 (нулевая интенсивность). 
        /// Целочисленные компоненты цвета со знаком, если они указаны, линейно отображаются в значения с плавающей запятой, так что наиболее положительное представимое значение отображается в 1,0, а наиболее отрицательное представимое значение - в -1,0. (Обратите внимание, что это отображение не преобразует 0 точно в 0.0.) 
        /// Значения с плавающей точкой отображаются напрямую.
        /// </para>
        /// <para>
        /// Ни значения с плавающей запятой, ни целые числа со знаком не ограничиваются диапазоном [0,1] до обновления текущего цвета. 
        /// Тем не менее, цветовые компоненты фиксируются в этом диапазоне, прежде чем они будут интерполированы или записаны в цветовой буфер.
        /// </para>
        /// </remarks>
        /// <seealso cref="glBegin"/>
        /// <seealso cref="glEnd"/>
        /// <seealso cref="glGetBooleanv"/><seealso cref="glGetDoublev"/><seealso cref="glGetFloatv"/><seealso cref="glGetIntegerv"/>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColor3f(GLfloat red, GLfloat green, GLfloat blue);

        /// <summary>
        /// Функция устанавливает текущий цвет из уже существующего массива значений цвета.
        /// </summary>
        /// <param name="v">
        /// Указатель на массив, содержащий красные, зеленые и синие значения.
        /// </param>
        /// <remarks>
        /// <para>
        /// GL хранит как текущий однозначный индекс цвета, так и текущий четырехзначный цвет RGBA. glcolor устанавливает новый четырехзначный цвет RGBA. У glcolor есть два основных варианта: glcolor3 и glcolor4. 
        /// Варианты glcolor3 явно задают новые значения красного, зеленого и синего цветов и неявно устанавливают текущее значение альфа-канала равным 1,0 (полная интенсивность). 
        /// Варианты glcolor4 явно указывают все четыре цветовых компонента.
        /// </para>
        /// <para>
        /// В качестве аргументов glcolor3b, glcolor4b, glcolor3s, glcolor4s, glcolor3i и glcolor4i принимают три или четыре целых байта со знаком, короткие или длинные. 
        /// Когда к имени добавляется v, цветовые команды могут получить указатель на массив таких значений.
        /// </para>
        /// <para>
        /// Текущие значения цвета хранятся в формате с плавающей запятой с неопределенными размерами мантиссы и экспоненты. 
        /// Целочисленные компоненты цвета без знака, если они указаны, линейно отображаются на значения с плавающей точкой, так что наибольшее представимое значение отображается на 1,0 (полная интенсивность), а 0 - на 0,0 (нулевая интенсивность). 
        /// Целочисленные компоненты цвета со знаком, если они указаны, линейно отображаются в значения с плавающей запятой, так что наиболее положительное представимое значение отображается в 1,0, а наиболее отрицательное представимое значение - в -1,0. (Обратите внимание, что это отображение не преобразует 0 точно в 0.0.) 
        /// Значения с плавающей точкой отображаются напрямую.
        /// </para>
        /// <para>
        /// Ни значения с плавающей запятой, ни целые числа со знаком не ограничиваются диапазоном [0,1] до обновления текущего цвета. 
        /// Тем не менее, цветовые компоненты фиксируются в этом диапазоне, прежде чем они будут интерполированы или записаны в цветовой буфер.
        /// </para>
        /// </remarks>
        /// <seealso cref="glBegin"/>
        /// <seealso cref="glEnd"/>
        /// <seealso cref="glGetBooleanv"/><seealso cref="glGetDoublev"/><seealso cref="glGetFloatv"/><seealso cref="glGetIntegerv"/>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor3fv(GLfloat* v);

        /// <remarks>
        /// Sets the current color.
        /// Parameters
        /// red
        /// The new red value for the current color.
        /// green
        /// The new green value for the current color.
        /// blue
        /// The new blue value for the current color.
        /// Return value
        /// This function does not return a value.
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColor3i(GLint red, GLint green, GLint blue);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor3iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColor3s(GLshort red, GLshort green, GLshort blue);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor3sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColor3ub(GLubyte red, GLubyte green, GLubyte blue);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor3ubv(GLubyte* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glColor3ui(GLuint red, GLuint green, GLuint blue);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor3uiv(GLuint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glColor3us(GLushort red, GLushort green, GLushort blue);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor3usv(GLushort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        /// <param name="alpha">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glColor4b(GLbyte red, GLbyte green, GLbyte blue, GLbyte alpha);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor4bv(GLbyte* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        /// <param name="alpha">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColor4d(GLdouble red, GLdouble green, GLdouble blue, GLdouble alpha);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor4dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        /// <param name="alpha">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColor4f(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor4fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        /// <param name="alpha">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColor4i(GLint red, GLint green, GLint blue, GLint alpha);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor4iv(GLint* v);

        /// <remarks>
        /// Sets the current color.
        /// Parameters
        /// red
        /// The new red value for the current color.
        /// green
        /// The new green value for the current color.
        /// blue
        /// The new blue value for the current color.
        /// alpha
        /// The new alpha value for the current color.
        /// Return value
        /// This function does not return a value.
        /// Remarks
        /// The GL stores both a current single-valued color index and a current four-valued RGBA color. glcolor sets a new four-valued RGBA color. glcolor has two major variants: glcolor3 and glcolor4. glcolor3 variants specify new red, green, and blue values explicitly and set the current alpha value to 1.0 (full intensity) implicitly. glcolor4 variants specify all four color components explicitly.
        /// glcolor3b, glcolor4b, glcolor3s, glcolor4s, glcolor3i, and glcolor4i take three or four signed byte, short, or long integers as arguments. When v is appended to the name, the color commands can take a pointer to an array of such values.
        /// Current color values are stored in floating-point format, with unspecified mantissa and exponent sizes. Unsigned integer color components, when specified, are linearly mapped to floating-point values such that the largest representable value maps to 1.0 (full intensity), and 0 maps to 0.0 (zero intensity). Signed integer color components, when specified, are linearly mapped to floating-point values such that the most positive representable value maps to 1.0, and the most negative representable value maps to -1.0. (Note that this mapping does not convert 0 precisely to 0.0.) Floating-point values are mapped directly.
        /// Neither floating-point nor signed integer values are clamped to the range [0,1] before the current color is updated. However, color components are clamped to this range before they are interpolated or written into a color buffer.
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        /// <param name="alpha">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColor4s(GLshort red, GLshort green, GLshort blue, GLshort alpha);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor4sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        /// <param name="alpha">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColor4ub(GLubyte red, GLubyte green, GLubyte blue, GLubyte alpha);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor4ubv(GLubyte* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        /// <param name="alpha">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glColor4ui(GLuint red, GLuint green, GLuint blue, GLuint alpha);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor4uiv(GLuint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        /// <param name="alpha">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glColor4us(GLushort red, GLushort green, GLushort blue, GLushort alpha);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColor4usv(GLushort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="red">
        /// 
        /// </param>
        /// <param name="green">
        /// 
        /// </param>
        /// <param name="blue">
        /// 
        /// </param>
        /// <param name="alpha">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="face">
        /// 
        /// </param>
        /// <param name="mode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glColorMaterial(GLenum face, GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="stride">
        /// 
        /// </param>
        /// <param name="pointer">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glColorPointer(GLint size, GLenum type, GLsizei stride, void* pointer);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="height">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glCopyPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum type);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="internalFormat">
        /// 
        /// </param>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="border">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glCopyTexImage1D(GLenum target, GLint level, GLenum internalFormat, GLint x, GLint y, GLsizei width, GLint border);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="internalFormat">
        /// 
        /// </param>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="height">
        /// 
        /// </param>
        /// <param name="border">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glCopyTexImage2D(GLenum target, GLint level, GLenum internalFormat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="xoffset">
        /// 
        /// </param>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glCopyTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="xoffset">
        /// 
        /// </param>
        /// <param name="yoffset">
        /// 
        /// </param>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="height">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glCopyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glCullFace(GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">
        /// 
        /// </param>
        /// <param name="range">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glDeleteLists(GLuint list, GLsizei range);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">
        /// 
        /// </param>
        /// <param name="textures">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glDeleteTextures(GLsizei n, GLuint* textures);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glDepthFunc(GLenum func);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glDepthMask(GLboolean flag);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zNear">
        /// 
        /// </param>
        /// <param name="zFar">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glDepthRange(GLclampd zNear, GLclampd zFar);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cap">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glDisable(GLenum cap);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glDisableClientState(GLenum array);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        /// <param name="first">
        /// 
        /// </param>
        /// <param name="count">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glDrawArrays(GLenum mode, GLint first, GLsizei count);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glDrawBuffer(GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        /// <param name="count">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="indices">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glDrawElements(GLenum mode, GLsizei count, GLenum type, void* indices);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="height">
        /// 
        /// </param>
        /// <param name="format">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="pixels">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glDrawPixels(GLsizei width, GLsizei height, GLenum format, GLenum type, void* pixels);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEdgeFlag(GLboolean flag);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stride">
        /// 
        /// </param>
        /// <param name="pointer">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glEdgeFlagPointer(GLsizei stride, void* pointer);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glEdgeFlagv(GLboolean* flag);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cap">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEnable(GLenum cap);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEnableClientState(GLenum array);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEnd();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEndList();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEvalCoord1d(GLdouble u);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glEvalCoord1dv(GLdouble* u);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEvalCoord1f(GLfloat u);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glEvalCoord1fv(GLfloat* u);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">
        /// 
        /// </param>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEvalCoord2d(GLdouble u, GLdouble v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glEvalCoord2dv(GLdouble* u);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">
        /// 
        /// </param>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEvalCoord2f(GLfloat u, GLfloat v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glEvalCoord2fv(GLfloat* u);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        /// <param name="i1">
        /// 
        /// </param>
        /// <param name="i2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEvalMesh1(GLenum mode, GLint i1, GLint i2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        /// <param name="i1">
        /// 
        /// </param>
        /// <param name="i2">
        /// 
        /// </param>
        /// <param name="j1">
        /// 
        /// </param>
        /// <param name="j2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEvalMesh2(GLenum mode, GLint i1, GLint i2, GLint j1, GLint j2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEvalPoint1(GLint i);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">
        /// 
        /// </param>
        /// <param name="j">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glEvalPoint2(GLint i, GLint j);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="buffer">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glFeedbackBuffer(GLsizei size, GLenum type, GLfloat* buffer);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glFinish();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glFlush();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glFogf(GLenum pname, GLfloat param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glFogfv(GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glFogi(GLenum pname, GLint param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glFogiv(GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glFrontFace(GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left">
        /// 
        /// </param>
        /// <param name="right">
        /// 
        /// </param>
        /// <param name="bottom">
        /// 
        /// </param>
        /// <param name="top">
        /// 
        /// </param>
        /// <param name="zNear">
        /// 
        /// </param>
        /// <param name="zFar">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glFrustum(GLdouble left, GLdouble right, GLdouble bottom, GLdouble top, GLdouble zNear, GLdouble zFar);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="range">
        /// 
        /// </param>
        /// <returns></returns>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern GLuint glGenLists(GLsizei range);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">
        /// 
        /// </param>
        /// <param name="textures">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGenTextures(GLsizei n, GLuint* textures);

        /// <summary>
        /// Возвращает одно или несколько значений, соответствующих выбранному параметру.
        /// </summary>
        /// <param name="pname">
        /// Определяет значение параметра, которое будет возвращено.
        /// </param>
        /// <param name="params">
        /// Указатель на область памяти, в которую необходимо поместить одно или несколько запрошенных значений.
        /// </param>
        /// <remarks>
        /// <para><paramref name="pname"/> может принимать одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_ACCUM_ALPHA_BITS"/></term>
        /// <description>params returns one value, the number of alpha bitplanes in the accumulation buffer.</description>
        /// <term><see cref="GL_ACCUM_BLUE_BITS"/></term>
        /// <description>params returns one value, the number of blue bitplanes in the accumulation buffer.</description>
        /// <term><see cref="GL_ACCUM_CLEAR_VALUE"/></term>
        /// <description>params returns four values: the red, green, blue, and alpha values used to clear the accumulation buffer. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and -1.0 returns the most negative representable integer value. The initial value is (0, 0, 0, 0). See glClearAccum.</description>
        /// <term><see cref="GL_ACCUM_GREEN_BITS"/></term>
        /// <description>params returns one value, the number of green bitplanes in the accumulation buffer.</description>
        /// <term><see cref="GL_ACCUM_RED_BITS"/></term>
        /// <description>params returns one value, the number of red bitplanes in the accumulation buffer.</description>
        /// <term><see cref="GL_ALPHA_BIAS"/></term>
        /// <description>params returns one value, the alpha bias factor used during pixel transfers. The initial value is 0. See glPixelTransfer.</description>
        /// <term><see cref="GL_ALPHA_BITS"/></term>
        /// <description>params returns one value, the number of alpha bitplanes in each color buffer.</description>
        /// <term><see cref="GL_ALPHA_SCALE"/></term>
        /// <description>params returns one value, the alpha scale factor used during pixel transfers. The initial value is 1. See glPixelTransfer.</description>
        /// <term><see cref="GL_ALPHA_TEST"/></term>
        /// <description>params returns a single boolean value indicating whether alpha testing of fragments is enabled. The initial value is GL_FALSE. See glAlphaFunc.</description>
        /// <term><see cref="GL_ALPHA_TEST_FUNC"/></term>
        /// <description>params returns one value, the symbolic name of the alpha test function. The initial value is GL_ALWAYS. See glAlphaFunc.</description>
        /// <term><see cref="GL_ALPHA_TEST_REF"/></term>
        /// <description>params returns one value, the reference value for the alpha test. The initial value is 0. See glAlphaFunc. An integer value, if requested, is linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and -1.0 returns the most negative representable integer value.</description>
        /// <term><see cref="GL_ATTRIB_STACK_DEPTH"/></term>
        /// <description>params returns one value, the depth of the attribute stack. If the stack is empty, 0 is returned. The initial value is 0. See glPushAttrib.</description>
        /// <term><see cref="GL_AUTO_NORMAL"/></term>
        /// <description>params returns a single boolean value indicating whether 2D map evaluation automatically generates surface normals. The initial value is GL_FALSE. See glMap2.</description>
        /// <term><see cref="GL_AUX_BUFFERS"/></term>
        /// <description>params returns one value, the number of auxiliary color buffers available.</description>
        /// <term><see cref="GL_BLEND"/></term>
        /// <description>params returns a single boolean value indicating whether blending is enabled. The initial value is GL_FALSE. See glBlendFunc.</description>
        /// <term><see cref="GL_BLUE_BIAS"/></term>
        /// <description>params returns one value, the blue bias factor used during pixel transfers. The initial value is 0. See glPixelTransfer.</description>
        /// <term><see cref="GL_BLUE_BITS"/></term>
        /// <description>params returns one value, the number of blue bitplanes in each color buffer.</description>
        /// <term><see cref="GL_BLUE_SCALE"/></term>
        /// <description>params returns one value, the blue scale factor used during pixel transfers. The initial value is 1. See glPixelTransfer.</description>
        /// <term><see cref="GL_CLIENT_ATTRIB_STACK_DEPTH"/></term>
        /// <description>params returns one value indicating the depth of the attribute stack. The initial value is 0. See glPushClientAttrib.</description>
        /// <term><see cref="GL_CLIP_PLANE0"/></term>
        /// <description>params returns a single boolean value indicating whether the specified clipping plane is enabled. The initial value is GL_FALSE. See glClipPlane.</description>
        /// <term><see cref="GL_CLIP_PLANE1"/></term>
        /// <description>params returns a single boolean value indicating whether the specified clipping plane is enabled. The initial value is GL_FALSE. See glClipPlane.</description>
        /// <term><see cref="GL_CLIP_PLANE2"/></term>
        /// <description>params returns a single boolean value indicating whether the specified clipping plane is enabled. The initial value is GL_FALSE. See glClipPlane.</description>
        /// <term><see cref="GL_CLIP_PLANE3"/></term>
        /// <description>params returns a single boolean value indicating whether the specified clipping plane is enabled. The initial value is GL_FALSE. See glClipPlane.</description>
        /// <term><see cref="GL_CLIP_PLANE4"/></term>
        /// <description>params returns a single boolean value indicating whether the specified clipping plane is enabled. The initial value is GL_FALSE. See glClipPlane.</description>
        /// <term><see cref="GL_CLIP_PLANE5"/></term>
        /// <description>params returns a single boolean value indicating whether the specified clipping plane is enabled. The initial value is GL_FALSE. See glClipPlane.</description>
        /// <term><see cref="GL_COLOR_ARRAY"/></term>
        /// <description>params returns a single boolean value indicating whether the color array is enabled. The initial value is GL_FALSE. See glColorPointer.</description>
        /// <term><see cref="GL_COLOR_ARRAY_SIZE"/></term>
        /// <description>params returns one value, the number of components per color in the color array. The initial value is 4. See glColorPointer.</description>
        /// <term><see cref="GL_COLOR_ARRAY_STRIDE"/></term>
        /// <description>params returns one value, the byte offset between consecutive colors in the color array. The initial value is 0. See glColorPointer.</description>
        /// <term><see cref="GL_COLOR_ARRAY_TYPE"/></term>
        /// <description>params returns one value, the data type of each component in the color array. The initial value is GL_FLOAT. See glColorPointer.</description>
        /// <term><see cref="GL_COLOR_CLEAR_VALUE"/></term>
        /// <description>params returns four values: the red, green, blue, and alpha values used to clear the color buffers. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and -1.0 returns the most negative representable integer value. The initial value is (0, 0, 0, 0). See glClearColor.</description>
        /// <term><see cref="GL_COLOR_LOGIC_OP"/></term>
        /// <description>params returns a single boolean value indicating whether a fragment's RGBA color values are merged into the framebuffer using a logical operation. The initial value is GL_FALSE. See glLogicOp.</description>
        /// <term><see cref="GL_COLOR_MATERIAL"/></term>
        /// <description>params returns a single boolean value indicating whether one or more material parameters are tracking the current color. The initial value is GL_FALSE. See glColorMaterial.</description>
        /// <term><see cref="GL_COLOR_MATERIAL_FACE"/></term>
        /// <description>params returns one value, a symbolic constant indicating which materials have a parameter that is tracking the current color. The initial value is GL_FRONT_AND_BACK. See glColorMaterial.</description>
        /// <term><see cref="GL_COLOR_MATERIAL_PARAMETER"/></term>
        /// <description>params returns one value, a symbolic constant indicating which material parameters are tracking the current color. The initial value is GL_AMBIENT_AND_DIFFUSE. See glColorMaterial.</description>
        /// <term><see cref="GL_COLOR_WRITEMASK"/></term>
        /// <description>params returns four boolean values: the red, green, blue, and alpha write enables for the color buffers. The initial value is (GL_TRUE, GL_TRUE, GL_TRUE, GL_TRUE). See glColorMask.</description>
        /// <term><see cref="GL_CULL_FACE"/></term>
        /// <description>params returns a single boolean value indicating whether polygon culling is enabled. The initial value is GL_FALSE. See glCullFace.</description>
        /// <term><see cref="GL_CULL_FACE_MODE"/></term>
        /// <description>params returns one value, a symbolic constant indicating which polygon faces are to be culled. The initial value is GL_BACK. See glCullFace.</description>
        /// <term><see cref="GL_CURRENT_COLOR"/></term>
        /// <description>params returns four values: the red, green, blue, and alpha values of the current color. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and -1.0 returns the most negative representable integer value. The initial value is (1, 1, 1, 1). See glColor.</description>
        /// <term><see cref="GL_CURRENT_INDEX"/></term>
        /// <description>params returns one value, the current color index. The initial value is 1. See glIndex.</description>
        /// <term><see cref="GL_CURRENT_NORMAL"/></term>
        /// <description>params returns three values: the x, y, and z values of the current normal. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and -1.0 returns the most negative representable integer value. The initial value is (0, 0, 1). See glNormal.</description>
        /// <term><see cref="GL_CURRENT_RASTER_COLOR"/></term>
        /// <description>params returns four values: the red, green, blue, and alpha color values of the current raster position. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and -1.0 returns the most negative representable integer value. The initial value is (1, 1, 1, 1). See glRasterPos.</description>
        /// <term><see cref="GL_CURRENT_RASTER_DISTANCE"/></term>
        /// <description>params returns one value, the distance from the eye to the current raster position. The initial value is 0. See glRasterPos.</description>
        /// <term><see cref="GL_CURRENT_RASTER_INDEX"/></term>
        /// <description>params returns one value, the color index of the current raster position. The initial value is 1. See glRasterPos.</description>
        /// <term><see cref="GL_CURRENT_RASTER_POSITION"/></term>
        /// <description>params returns four values: the x, y, z, and w components of the current raster position. x, y, and z are in window coordinates, and w is in clip coordinates. The initial value is (0, 0, 0, 1). See glRasterPos.</description>
        /// <term><see cref="GL_CURRENT_RASTER_POSITION_VALID"/></term>
        /// <description>params returns a single boolean value indicating whether the current raster position is valid. The initial value is GL_TRUE. See glRasterPos.</description>
        /// <term><see cref="GL_CURRENT_RASTER_TEXTURE_COORDS"/></term>
        /// <description>params returns four values: the s, t, r, and q texture coordinates of the current raster position. The initial value is (0, 0, 0, 1). See glRasterPos and glMultiTexCoord.</description>
        /// <term><see cref="GL_CURRENT_TEXTURE_COORDS"/></term>
        /// <description>params returns four values: the s, t, r, and q current texture coordinates. The initial value is (0, 0, 0, 1). See glMultiTexCoord.</description>
        /// <term><see cref="GL_DEPTH_BIAS"/></term>
        /// <description>params returns one value, the depth bias factor used during pixel transfers. The initial value is 0. See glPixelTransfer.</description>
        /// <term><see cref="GL_DEPTH_BITS"/></term>
        /// <description>params returns one value, the number of bitplanes in the depth buffer.</description>
        /// <term><see cref="GL_DEPTH_CLEAR_VALUE"/></term>
        /// <description>params returns one value, the value that is used to clear the depth buffer. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and -1.0 returns the most negative representable integer value. The initial value is 1. See glClearDepth.</description>
        /// <term><see cref="GL_DEPTH_FUNC"/></term>
        /// <description>params returns one value, the symbolic constant that indicates the depth comparison function. The initial value is GL_LESS. See glDepthFunc.</description>
        /// <term><see cref="GL_DEPTH_RANGE"/></term>
        /// <description>params returns two values: the near and far mapping limits for the depth buffer. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and -1.0 returns the most negative representable integer value. The initial value is (0, 1). See glDepthRange.</description>
        /// <term><see cref="GL_DEPTH_SCALE"/></term>
        /// <description>params returns one value, the depth scale factor used during pixel transfers. The initial value is 1. See glPixelTransfer.</description>
        /// <term><see cref="GL_DEPTH_TEST"/></term>
        /// <description>params returns a single boolean value indicating whether depth testing of fragments is enabled. The initial value is GL_FALSE. See glDepthFunc and glDepthRange.</description>
        /// <term><see cref="GL_DEPTH_WRITEMASK"/></term>
        /// <description>params returns a single boolean value indicating if the depth buffer is enabled for writing. The initial value is GL_TRUE. See glDepthMask.</description>
        /// <term><see cref="GL_DITHER"/></term>
        /// <description>params returns a single boolean value indicating whether dithering of fragment colors and indices is enabled. The initial value is GL_TRUE.</description>
        /// <term><see cref="GL_DOUBLEBUFFER"/></term>
        /// <description>params returns a single boolean value indicating whether double buffering is supported.</description>
        /// <term><see cref="GL_DRAW_BUFFER"/></term>
        /// <description>params returns one value, a symbolic constant indicating which buffers are being drawn to. See glDrawBuffer. The initial value is GL_BACK if there are back buffers, otherwise it is GL_FRONT.</description>
        /// <term><see cref="GL_EDGE_FLAG"/></term>
        /// <description>params returns a single boolean value indicating whether the current edge flag is GL_TRUE or GL_FALSE. The initial value is GL_TRUE. See glEdgeFlag.</description>
        /// <term><see cref="GL_EDGE_FLAG_ARRAY"/></term>
        /// <description>params returns a single boolean value indicating whether the edge flag array is enabled. The initial value is GL_FALSE. See glEdgeFlagPointer.</description>
        /// <term><see cref="GL_EDGE_FLAG_ARRAY_STRIDE"/></term>
        /// <description>params returns one value, the byte offset between consecutive edge flags in the edge flag array. The initial value is 0. See glEdgeFlagPointer.</description>
        /// <term><see cref="GL_FEEDBACK_BUFFER_SIZE"/></term>
        /// <description>params returns one value, the size of the feedback buffer. See glFeedbackBuffer.</description>
        /// <term><see cref="GL_FEEDBACK_BUFFER_TYPE"/></term>
        /// <description>params returns one value, the type of the feedback buffer. See glFeedbackBuffer.</description>
        /// <term><see cref="GL_FOG"/></term>
        /// <description>params returns a single boolean value indicating whether fogging is enabled. The initial value is GL_FALSE. See glFog.</description>
        /// <term><see cref="GL_FOG_COLOR"/></term>
        /// <description>params returns four values: the red, green, blue, and alpha components of the fog color. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and -1.0 returns the most negative representable integer value. The initial value is (0, 0, 0, 0). See glFog.</description>
        /// <term><see cref="GL_FOG_DENSITY"/></term>
        /// <description>params returns one value, the fog density parameter. The initial value is 1. See glFog.</description>
        /// <term><see cref="GL_FOG_END"/></term>
        /// <description>params returns one value, the end factor for the linear fog equation. The initial value is 1. See glFog.</description>
        /// <term><see cref="GL_FOG_HINT"/></term>
        /// <description>params returns one value, a symbolic constant indicating the mode of the fog hint. The initial value is GL_DONT_CARE. See glHint.</description>
        /// <term><see cref="GL_FOG_INDEX"/></term>
        /// <description>params returns one value, the fog color index. The initial value is 0. See glFog.</description>
        /// <term><see cref="GL_FOG_MODE"/></term>
        /// <description>params returns one value, a symbolic constant indicating which fog equation is selected. The initial value is GL_EXP. See glFog.</description>
        /// <term><see cref="GL_FOG_START"/></term>
        /// <description>params returns one value, the start factor for the linear fog equation. The initial value is 0. See glFog.</description>
        /// <term><see cref="GL_FRONT_FACE"/></term>
        /// <description>params returns one value, a symbolic constant indicating whether clockwise or counterclockwise polygon winding is treated as front-facing. The initial value is GL_CCW. See glFrontFace.</description>
        /// <term><see cref="GL_GREEN_BIAS"/></term>
        /// <description>params returns one value, the green bias factor used during pixel transfers. The initial value is 0.</description>
        /// <term><see cref="GL_GREEN_BITS"/></term>
        /// <description>params returns one value, the number of green bitplanes in each color buffer.</description>
        /// <term><see cref="GL_GREEN_SCALE"/></term>
        /// <description>params returns one value, the green scale factor used during pixel transfers. The initial value is 1. See glPixelTransfer.</description>
        /// <term><see cref="GL_INDEX_ARRAY"/></term>
        /// <description>params returns a single boolean value indicating whether the color index array is enabled. The initial value is GL_FALSE. See glIndexPointer.</description>
        /// <term><see cref="GL_INDEX_ARRAY_STRIDE"/></term>
        /// <description>params returns one value, the byte offset between consecutive color indexes in the color index array. The initial value is 0. See glIndexPointer.</description>
        /// <term><see cref="GL_INDEX_ARRAY_TYPE"/></term>
        /// <description>params returns one value, the data type of indexes in the color index array. The initial value is GL_FLOAT. See glIndexPointer.</description>
        /// <term><see cref="GL_INDEX_BITS"/></term>
        /// <description>params returns one value, the number of bitplanes in each color index buffer.</description>
        /// <term><see cref="GL_INDEX_CLEAR_VALUE"/></term>
        /// <description>params returns one value, the color index used to clear the color index buffers. The initial value is 0. See glClearIndex.</description>
        /// <term><see cref="GL_INDEX_LOGIC_OP"/></term>
        /// <description>params returns a single boolean value indicating whether a fragment's index values are merged into the framebuffer using a logical operation. The initial value is GL_FALSE. See glLogicOp.</description>
        /// <term><see cref="GL_INDEX_MODE"/></term>
        /// <description>params returns a single boolean value indicating whether the GL is in color index mode (GL_TRUE) or RGBA mode (GL_FALSE).</description>
        /// <term><see cref="GL_INDEX_OFFSET"/></term>
        /// <description>params returns one value, the offset added to color and stencil indices during pixel transfers. The initial value is 0. See glPixelTransfer.</description>
        /// <term><see cref="GL_INDEX_SHIFT"/></term>
        /// <description>params returns one value, the amount that color and stencil indices are shifted during pixel transfers. The initial value is 0. See glPixelTransfer.</description>
        /// <term><see cref="GL_INDEX_WRITEMASK"/></term>
        /// <description>params returns one value, a mask indicating which bitplanes of each color index buffer can be written. The initial value is all 1's. See glIndexMask.</description>
        /// <term><see cref="GL_LIGHT0"/></term>
        /// <description>params returns a single boolean value indicating whether the specified light is enabled. The initial value is GL_FALSE. See glLight and glLightModel.</description>
        /// <term><see cref="GL_LIGHT1"/></term>
        /// <description>params returns a single boolean value indicating whether the specified light is enabled. The initial value is GL_FALSE. See glLight and glLightModel.</description>
        /// <term><see cref="GL_LIGHT2"/></term>
        /// <description>params returns a single boolean value indicating whether the specified light is enabled. The initial value is GL_FALSE. See glLight and glLightModel.</description>
        /// <term><see cref="GL_LIGHT3"/></term>
        /// <description>params returns a single boolean value indicating whether the specified light is enabled. The initial value is GL_FALSE. See glLight and glLightModel.</description>
        /// <term><see cref="GL_LIGHT4"/></term>
        /// <description>params returns a single boolean value indicating whether the specified light is enabled. The initial value is GL_FALSE. See glLight and glLightModel.</description>
        /// <term><see cref="GL_LIGHT5"/></term>
        /// <description>params returns a single boolean value indicating whether the specified light is enabled. The initial value is GL_FALSE. See glLight and glLightModel.</description>
        /// <term><see cref="GL_LIGHT6"/></term>
        /// <description>params returns a single boolean value indicating whether the specified light is enabled. The initial value is GL_FALSE. See glLight and glLightModel.</description>
        /// <term><see cref="GL_LIGHT7"/></term>
        /// <description>params returns a single boolean value indicating whether the specified light is enabled. The initial value is GL_FALSE. See glLight and glLightModel.</description>
        /// <term><see cref="GL_LIGHTING"/></term>
        /// <description>params returns a single boolean value indicating whether lighting is enabled. The initial value is GL_FALSE. See glLightModel.</description>
        /// <term><see cref="GL_LIGHT_MODEL_AMBIENT"/></term>
        /// <description>params returns four values: the red, green, blue, and alpha components of the ambient intensity of the entire scene. Integer values, if requested, are linearly mapped from the internal floating-point representation such that 1.0 returns the most positive representable integer value, and -1.0 returns the most negative representable integer value. The initial value is (0.2, 0.2, 0.2, 1.0). See glLightModel.</description>
        /// <term><see cref="GL_LIGHT_MODEL_LOCAL_VIEWER"/></term>
        /// <description>params returns a single boolean value indicating whether specular reflection calculations treat the viewer as being local to the scene. The initial value is GL_FALSE. See glLightModel.</description>
        /// <term><see cref="GL_LIGHT_MODEL_TWO_SIDE"/></term>
        /// <description>params returns a single boolean value indicating whether separate materials are used to compute lighting for front- and back-facing polygons. The initial value is GL_FALSE. See glLightModel.</description>
        /// <term><see cref="GL_LINE_SMOOTH"/></term>
        /// <description>params returns a single boolean value indicating whether antialiasing of lines is enabled. The initial value is GL_FALSE. See glLineWidth.</description>
        /// <term><see cref="GL_LINE_SMOOTH_HINT"/></term>
        /// <description>params returns one value, a symbolic constant indicating the mode of the line antialiasing hint. The initial value is GL_DONT_CARE. See glHint.</description>
        /// <term><see cref="GL_LINE_STIPPLE"/></term>
        /// <description>params returns a single boolean value indicating whether stippling of lines is enabled. The initial value is GL_FALSE. See glLineStipple.</description>
        /// <term><see cref="GL_LINE_STIPPLE_PATTERN"/></term>
        /// <description>params returns one value, the 16-bit line stipple pattern. The initial value is all 1's. See glLineStipple.</description>
        /// <term><see cref="GL_LINE_STIPPLE_REPEAT"/></term>
        /// <description>params returns one value, the line stipple repeat factor. The initial value is 1. See glLineStipple.</description>
        /// <term><see cref="GL_LINE_WIDTH"/></term>
        /// <description>params returns one value, the line width as specified with glLineWidth. The initial value is 1.</description>
        /// <term><see cref="GL_LINE_WIDTH_GRANULARITY"/></term>
        /// <description>params returns one value, the width difference between adjacent supported widths for antialiased lines. See glLineWidth.</description>
        /// <term><see cref="GL_LINE_WIDTH_RANGE"/></term>
        /// <description>params returns two values: the smallest and largest supported widths for antialiased lines. See glLineWidth.</description>
        /// <term><see cref="GL_LIST_BASE"/></term>
        /// <description>params returns one value, the base offset added to all names in arrays presented to glCallLists. The initial value is 0. See glListBase.</description>
        /// <term><see cref="GL_LIST_INDEX"/></term>
        /// <description>params returns one value, the name of the display list currently under construction. 0 is returned if no display list is currently under construction. The initial value is 0. See glNewList.</description>
        /// <term><see cref="GL_LIST_MODE"/></term>
        /// <description>params returns one value, a symbolic constant indicating the construction mode of the display list currently under construction. The initial value is 0. See glNewList.</description>
        /// <term><see cref="GL_LOGIC_OP_MODE"/></term>
        /// <description>params returns one value, a symbolic constant indicating the selected logic operation mode. The initial value is GL_COPY. See glLogicOp.</description>
        /// <term><see cref="GL_MAP1_COLOR_4"/></term>
        /// <description>params returns a single boolean value indicating whether 1D evaluation generates colors. The initial value is GL_FALSE. See glMap1.</description>
        /// <term><see cref="GL_MAP1_GRID_DOMAIN"/></term>
        /// <description>params returns two values: the endpoints of the 1D map's grid domain. The initial value is (0, 1). See glMapGrid.</description>
        /// <term><see cref="GL_MAP1_GRID_SEGMENTS"/></term>
        /// <description>params returns one value, the number of partitions in the 1D map's grid domain. The initial value is 1. See glMapGrid.</description>
        /// <term><see cref="GL_MAP1_INDEX"/></term>
        /// <description>params returns a single boolean value indicating whether 1D evaluation generates color indices. The initial value is GL_FALSE. See glMap1.</description>
        /// <term><see cref="GL_MAP1_NORMAL"/></term>
        /// <description>params returns a single boolean value indicating whether 1D evaluation generates normals. The initial value is GL_FALSE. See glMap1.</description>
        /// <term><see cref="GL_MAP1_TEXTURE_COORD_1"/></term>
        /// <description>params returns a single boolean value indicating whether 1D evaluation generates 1D texture coordinates. The initial value is GL_FALSE. See glMap1.</description>
        /// <term><see cref="GL_MAP1_TEXTURE_COORD_2"/></term>
        /// <description>params returns a single boolean value indicating whether 1D evaluation generates 2D texture coordinates. The initial value is GL_FALSE. See glMap1.</description>
        /// <term><see cref="GL_MAP1_TEXTURE_COORD_3"/></term>
        /// <description>params returns a single boolean value indicating whether 1D evaluation generates 3D texture coordinates. The initial value is GL_FALSE. See glMap1.</description>
        /// <term><see cref="GL_MAP1_TEXTURE_COORD_4"/></term>
        /// <description>params returns a single boolean value indicating whether 1D evaluation generates 4D texture coordinates. The initial value is GL_FALSE. See glMap1.</description>
        /// <term><see cref="GL_MAP1_VERTEX_3"/></term>
        /// <description>params returns a single boolean value indicating whether 1D evaluation generates 3D vertex coordinates. The initial value is GL_FALSE. See glMap1.</description>
        /// <term><see cref="GL_MAP1_VERTEX_4"/></term>
        /// <description>params returns a single boolean value indicating whether 1D evaluation generates 4D vertex coordinates. The initial value is GL_FALSE. See glMap1.</description>
        /// <term><see cref="GL_MAP2_COLOR_4"/></term>
        /// <description>params returns a single boolean value indicating whether 2D evaluation generates colors. The initial value is GL_FALSE. See glMap2.</description>
        /// <term><see cref="GL_MAP2_GRID_DOMAIN"/></term>
        /// <description>params returns four values: the endpoints of the 2D map's i and j grid domains. The initial value is (0,1; 0,1). See glMapGrid.</description>
        /// <term><see cref="GL_MAP2_GRID_SEGMENTS"/></term>
        /// <description>params returns two values: the number of partitions in the 2D map's i and j grid domains. The initial value is (1,1). See glMapGrid.</description>
        /// <term><see cref="GL_MAP2_INDEX"/></term>
        /// <description>params returns a single boolean value indicating whether 2D evaluation generates color indices. The initial value is GL_FALSE. See glMap2.</description>
        /// <term><see cref="GL_MAP2_NORMAL"/></term>
        /// <description>params returns a single boolean value indicating whether 2D evaluation generates normals. The initial value is GL_FALSE. See glMap2.</description>
        /// <term><see cref="GL_MAP2_TEXTURE_COORD_1"/></term>
        /// <description>params returns a single boolean value indicating whether 2D evaluation generates 1D texture coordinates. The initial value is GL_FALSE. See glMap2.</description>
        /// <term><see cref="GL_MAP2_TEXTURE_COORD_2"/></term>
        /// <description>params returns a single boolean value indicating whether 2D evaluation generates 2D texture coordinates. The initial value is GL_FALSE. See glMap2.</description>
        /// <term><see cref="GL_MAP2_TEXTURE_COORD_3"/></term>
        /// <description>params returns a single boolean value indicating whether 2D evaluation generates 3D texture coordinates. The initial value is GL_FALSE. See glMap2.</description>
        /// <term><see cref="GL_MAP2_TEXTURE_COORD_4"/></term>
        /// <description>params returns a single boolean value indicating whether 2D evaluation generates 4D texture coordinates. The initial value is GL_FALSE. See glMap2.</description>
        /// <term><see cref="GL_MAP2_VERTEX_3"/></term>
        /// <description>params returns a single boolean value indicating whether 2D evaluation generates 3D vertex coordinates. The initial value is GL_FALSE. See glMap2.</description>
        /// <term><see cref="GL_MAP2_VERTEX_4"/></term>
        /// <description>params returns a single boolean value indicating whether 2D evaluation generates 4D vertex coordinates. The initial value is GL_FALSE. See glMap2.</description>
        /// <term><see cref="GL_MAP_COLOR"/></term>
        /// <description>params returns a single boolean value indicating if colors and color indices are to be replaced by table lookup during pixel transfers. The initial value is GL_FALSE. See glPixelTransfer.</description>
        /// <term><see cref="GL_MAP_STENCIL"/></term>
        /// <description>params returns a single boolean value indicating if stencil indices are to be replaced by table lookup during pixel transfers. The initial value is GL_FALSE. See glPixelTransfer.</description>
        /// <term><see cref="GL_MATRIX_MODE"/></term>
        /// <description>params returns one value, a symbolic constant indicating which matrix stack is currently the target of all matrix operations. The initial value is GL_MODELVIEW. See glMatrixMode.</description>
        /// <term><see cref="GL_MAX_CLIENT_ATTRIB_STACK_DEPTH"/></term>
        /// <description>params returns one value indicating the maximum supported depth of the client attribute stack. See glPushClientAttrib.</description>
        /// <term><see cref="GL_MAX_ATTRIB_STACK_DEPTH"/></term>
        /// <description>params returns one value, the maximum supported depth of the attribute stack. The value must be at least 16. See glPushAttrib.</description>
        /// <term><see cref="GL_MAX_CLIP_PLANES"/></term>
        /// <description>params returns one value, the maximum number of application-defined clipping planes. The value must be at least 6. See glClipPlane.</description>
        /// <term><see cref="GL_MAX_EVAL_ORDER"/></term>
        /// <description>params returns one value, the maximum equation order supported by 1D and 2D evaluators. The value must be at least 8. See glMap1 and glMap2.</description>
        /// <term><see cref="GL_MAX_LIGHTS"/></term>
        /// <description>params returns one value, the maximum number of lights. The value must be at least 8. See glLight.</description>
        /// <term><see cref="GL_MAX_LIST_NESTING"/></term>
        /// <description>params returns one value, the maximum recursion depth allowed during display-list traversal. The value must be at least 64. See glCallList.</description>
        /// <term><see cref="GL_MAX_MODELVIEW_STACK_DEPTH"/></term>
        /// <description>params returns one value, the maximum supported depth of the modelview matrix stack. The value must be at least 32. See glPushMatrix.</description>
        /// <term><see cref="GL_MAX_NAME_STACK_DEPTH"/></term>
        /// <description>params returns one value, the maximum supported depth of the selection name stack. The value must be at least 64. See glPushName.</description>
        /// <term><see cref="GL_MAX_PIXEL_MAP_TABLE"/></term>
        /// <description>params returns one value, the maximum supported size of a glPixelMap lookup table. The value must be at least 32. See glPixelMap.</description>
        /// <term><see cref="GL_MAX_PROJECTION_STACK_DEPTH"/></term>
        /// <description>params returns one value, the maximum supported depth of the projection matrix stack. The value must be at least 2. See glPushMatrix.</description>
        /// <term><see cref="GL_MAX_TEXTURE_SIZE"/></term>
        /// <description>params returns one value. The value gives a rough estimate of the largest texture that the GL can handle. The value must be at least 64. If the GL version is 1.1 or greater, use GL_PROXY_TEXTURE_1D or GL_PROXY_TEXTURE_2D to determine if a texture is too large. See glTexImage1D and glTexImage2D.</description>
        /// <term><see cref="GL_MAX_TEXTURE_STACK_DEPTH"/></term>
        /// <description>params returns one value, the maximum supported depth of the texture matrix stack. The value must be at least 2. See glPushMatrix.</description>
        /// <term><see cref="GL_MAX_VIEWPORT_DIMS"/></term>
        /// <description>params returns two values: the maximum supported width and height of the viewport. These must be at least as large as the visible dimensions of the display being rendered to. See glViewport.</description>
        /// <term><see cref="GL_MODELVIEW_MATRIX"/></term>
        /// <description>params returns sixteen values: the modelview matrix on the top of the modelview matrix stack. Initially this matrix is the identity matrix. See glPushMatrix.</description>
        /// <term><see cref="GL_MODELVIEW_STACK_DEPTH"/></term>
        /// <description>params returns one value, the number of matrices on the modelview matrix stack. The initial value is 1. See glPushMatrix.</description>
        /// <term><see cref="GL_NAME_STACK_DEPTH"/></term>
        /// <description>params returns one value, the number of names on the selection name stack. The initial value is 0. See glPushName.</description>
        /// <term><see cref="GL_NORMAL_ARRAY"/></term>
        /// <description>params returns a single boolean value, indicating whether the normal array is enabled. The initial value is GL_FALSE. See glNormalPointer.</description>
        /// <term><see cref="GL_NORMAL_ARRAY_STRIDE"/></term>
        /// <description>params returns one value, the byte offset between consecutive normals in the normal array. The initial value is 0. See glNormalPointer.</description>
        /// <term><see cref="GL_NORMAL_ARRAY_TYPE"/></term>
        /// <description>params returns one value, the data type of each coordinate in the normal array. The initial value is GL_FLOAT. See glNormalPointer.</description>
        /// <term><see cref="GL_NORMALIZE"/></term>
        /// <description>params returns a single boolean value indicating whether normals are automatically scaled to unit length after they have been transformed to eye coordinates. The initial value is GL_FALSE. See glNormal.</description>
        /// <term><see cref="GL_PACK_ALIGNMENT"/></term>
        /// <description>params returns one value, the byte alignment used for writing pixel data to memory. The initial value is 4. See glPixelStore.</description>
        /// <term><see cref="GL_PACK_LSB_FIRST"/></term>
        /// <description>params returns a single boolean value indicating whether single-bit pixels being written to memory are written first to the least significant bit of each unsigned byte. The initial value is GL_FALSE. See glPixelStore.</description>
        /// <term><see cref="GL_PACK_ROW_LENGTH"/></term>
        /// <description>params returns one value, the row length used for writing pixel data to memory. The initial value is 0. See glPixelStore.</description>
        /// <term><see cref="GL_PACK_SKIP_PIXELS"/></term>
        /// <description>params returns one value, the number of pixel locations skipped before the first pixel is written into memory. The initial value is 0. See glPixelStore.</description>
        /// <term><see cref="GL_PACK_SKIP_ROWS"/></term>
        /// <description>params returns one value, the number of rows of pixel locations skipped before the first pixel is written into memory. The initial value is 0. See glPixelStore.</description>
        /// <term><see cref="GL_PACK_SWAP_BYTES"/></term>
        /// <description>params returns a single boolean value indicating whether the bytes of two-byte and four-byte pixel indices and components are swapped before being written to memory. The initial value is GL_FALSE. See glPixelStore.</description>
        /// <term><see cref="GL_PERSPECTIVE_CORRECTION_HINT"/></term>
        /// <description>params returns one value, a symbolic constant indicating the mode of the perspective correction hint. The initial value is GL_DONT_CARE. See glHint.</description>
        /// <term><see cref="GL_PIXEL_MAP_A_TO_A_SIZE"/></term>
        /// <description>params returns one value, the size of the alpha-to-alpha pixel translation table. The initial value is 1. See glPixelMap.</description>
        /// <term><see cref="GL_PIXEL_MAP_B_TO_B_SIZE"/></term>
        /// <description>params returns one value, the size of the blue-to-blue pixel translation table. The initial value is 1. See glPixelMap.</description>
        /// <term><see cref="GL_PIXEL_MAP_G_TO_G_SIZE"/></term>
        /// <description>params returns one value, the size of the green-to-green pixel translation table. The initial value is 1. See glPixelMap.</description>
        /// <term><see cref="GL_PIXEL_MAP_I_TO_A_SIZE"/></term>
        /// <description>params returns one value, the size of the index-to-alpha pixel translation table. The initial value is 1. See glPixelMap.</description>
        /// <term><see cref="GL_PIXEL_MAP_I_TO_B_SIZE"/></term>
        /// <description>params returns one value, the size of the index-to-blue pixel translation table. The initial value is 1. See glPixelMap.</description>
        /// <term><see cref="GL_PIXEL_MAP_I_TO_G_SIZE"/></term>
        /// <description>params returns one value, the size of the index-to-green pixel translation table. The initial value is 1. See glPixelMap.</description>
        /// <term><see cref="GL_PIXEL_MAP_I_TO_I_SIZE"/></term>
        /// <description>params returns one value, the size of the index-to-index pixel translation table. The initial value is 1. See glPixelMap.</description>
        /// <term><see cref="GL_PIXEL_MAP_I_TO_R_SIZE"/></term>
        /// <description>params returns one value, the size of the index-to-red pixel translation table. The initial value is 1. See glPixelMap.</description>
        /// <term><see cref="GL_PIXEL_MAP_R_TO_R_SIZE"/></term>
        /// <description>params returns one value, the size of the red-to-red pixel translation table. The initial value is 1. See glPixelMap.</description>
        /// <term><see cref="GL_PIXEL_MAP_S_TO_S_SIZE"/></term>
        /// <description>params returns one value, the size of the stencil-to-stencil pixel translation table. The initial value is 1. See glPixelMap.</description>
        /// <term><see cref="GL_POINT_SIZE"/></term>
        /// <description>params returns one value, the point size as specified by glPointSize. The initial value is 1.</description>
        /// <term><see cref="GL_POINT_SIZE_GRANULARITY"/></term>
        /// <description>params returns one value, the size difference between adjacent supported sizes for antialiased points. See glPointSize.</description>
        /// <term><see cref="GL_POINT_SIZE_RANGE"/></term>
        /// <description>params returns two values: the smallest and largest supported sizes for antialiased points. The smallest size must be at most 1, and the largest size must be at least 1. See glPointSize.</description>
        /// <term><see cref="GL_POINT_SMOOTH"/></term>
        /// <description>params returns a single boolean value indicating whether antialiasing of points is enabled. The initial value is GL_FALSE. See glPointSize.</description>
        /// <term><see cref="GL_POINT_SMOOTH_HINT"/></term>
        /// <description>params returns one value, a symbolic constant indicating the mode of the point antialiasing hint. The initial value is GL_DONT_CARE. See glHint.</description>
        /// <term><see cref="GL_POLYGON_MODE"/></term>
        /// <description>params returns two values: symbolic constants indicating whether front-facing and back-facing polygons are rasterized as points, lines, or filled polygons. The initial value is GL_FILL. See glPolygonMode.</description>
        /// <term><see cref="GL_POLYGON_OFFSET_FACTOR"/></term>
        /// <description>params returns one value, the scaling factor used to determine the variable offset that is added to the depth value of each fragment generated when a polygon is rasterized. The initial value is 0. See glPolygonOffset.</description>
        /// <term><see cref="GL_POLYGON_OFFSET_UNITS"/></term>
        /// <description>params returns one value. This value is multiplied by an implementation-specific value and then added to the depth value of each fragment generated when a polygon is rasterized. The initial value is 0. See glPolygonOffset.</description>
        /// <term><see cref="GL_POLYGON_OFFSET_FILL"/></term>
        /// <description>params returns a single boolean value indicating whether polygon offset is enabled for polygons in fill mode. The initial value is GL_FALSE. See glPolygonOffset.</description>
        /// <term><see cref="GL_POLYGON_OFFSET_LINE"/></term>
        /// <description>params returns a single boolean value indicating whether polygon offset is enabled for polygons in line mode. The initial value is GL_FALSE. See glPolygonOffset.</description>
        /// <term><see cref="GL_POLYGON_OFFSET_POINT"/></term>
        /// <description>params returns a single boolean value indicating whether polygon offset is enabled for polygons in point mode. The initial value is GL_FALSE. See glPolygonOffset.</description>
        /// <term><see cref="GL_POLYGON_SMOOTH"/></term>
        /// <description>params returns a single boolean value indicating whether antialiasing of polygons is enabled. The initial value is GL_FALSE. See glPolygonMode.</description>
        /// <term><see cref="GL_POLYGON_SMOOTH_HINT"/></term>
        /// <description>params returns one value, a symbolic constant indicating the mode of the polygon antialiasing hint. The initial value is GL_DONT_CARE. See glHint.</description>
        /// <term><see cref="GL_POLYGON_STIPPLE"/></term>
        /// <description>params returns a single boolean value indicating whether polygon stippling is enabled. The initial value is GL_FALSE. See glPolygonStipple.</description>
        /// <term><see cref="GL_PROJECTION_MATRIX"/></term>
        /// <description>params returns sixteen values: the projection matrix on the top of the projection matrix stack. Initially this matrix is the identity matrix. See glPushMatrix.</description>
        /// <term><see cref="GL_PROJECTION_STACK_DEPTH"/></term>
        /// <description>params returns one value, the number of matrices on the projection matrix stack. The initial value is 1. See glPushMatrix.</description>
        /// <term><see cref="GL_READ_BUFFER"/></term>
        /// <description>params returns one value, a symbolic constant indicating which color buffer is selected for reading. The initial value is GL_BACK if there is a back buffer, otherwise it is GL_FRONT. See glReadPixels and glAccum.</description>
        /// <term><see cref="GL_RED_BIAS"/></term>
        /// <description>params returns one value, the red bias factor used during pixel transfers. The initial value is 0.</description>
        /// <term><see cref="GL_RED_BITS"/></term>
        /// <description>params returns one value, the number of red bitplanes in each color buffer.</description>
        /// <term><see cref="GL_RED_SCALE"/></term>
        /// <description>params returns one value, the red scale factor used during pixel transfers. The initial value is 1. See glPixelTransfer.</description>
        /// <term><see cref="GL_RENDER_MODE"/></term>
        /// <description>params returns one value, a symbolic constant indicating whether the GL is in render, select, or feedback mode. The initial value is GL_RENDER. See glRenderMode.</description>
        /// <term><see cref="GL_RGBA_MODE"/></term>
        /// <description>params returns a single boolean value indicating whether the GL is in RGBA mode (true) or color index mode (false). See glColor.</description>
        /// <term><see cref="GL_SCISSOR_BOX"/></term>
        /// <description>params returns four values: the x and y window coordinates of the scissor box, followed by its width and height. Initially the x and y window coordinates are both 0 and the width and height are set to the size of the window. See glScissor.</description>
        /// <term><see cref="GL_SCISSOR_TEST"/></term>
        /// <description>params returns a single boolean value indicating whether scissoring is enabled. The initial value is GL_FALSE. See glScissor.</description>
        /// <term><see cref="GL_SELECTION_BUFFER_SIZE"/></term>
        /// <description>params return one value, the size of the selection buffer. See glSelectBuffer.</description>
        /// <term><see cref="GL_SHADE_MODEL"/></term>
        /// <description>params returns one value, a symbolic constant indicating whether the shading mode is flat or smooth. The initial value is GL_SMOOTH. See glShadeModel.</description>
        /// <term><see cref="GL_STENCIL_BITS"/></term>
        /// <description>params returns one value, the number of bitplanes in the stencil buffer.</description>
        /// <term><see cref="GL_STENCIL_CLEAR_VALUE"/></term>
        /// <description>params returns one value, the index to which the stencil bitplanes are cleared. The initial value is 0. See glClearStencil.</description>
        /// <term><see cref="GL_STENCIL_FAIL"/></term>
        /// <description>params returns one value, a symbolic constant indicating what action is taken when the stencil test fails. The initial value is GL_KEEP. See glStencilOp. If the GL version is 2.0 or greater, this stencil state only affects non-polygons and front-facing polygons. Back-facing polygons use separate stencil state. See glStencilOpSeparate.</description>
        /// <term><see cref="GL_STENCIL_FUNC"/></term>
        /// <description>params returns one value, a symbolic constant indicating what function is used to compare the stencil reference value with the stencil buffer value. The initial value is GL_ALWAYS. See glStencilFunc. If the GL version is 2.0 or greater, this stencil state only affects non-polygons and front-facing polygons. Back-facing polygons use separate stencil state. See glStencilFuncSeparate.</description>
        /// <term><see cref="GL_STENCIL_PASS_DEPTH_FAIL"/></term>
        /// <description>params returns one value, a symbolic constant indicating what action is taken when the stencil test passes, but the depth test fails. The initial value is GL_KEEP. See glStencilOp. If the GL version is 2.0 or greater, this stencil state only affects non-polygons and front-facing polygons. Back-facing polygons use separate stencil state. See glStencilOpSeparate.</description>
        /// <term><see cref="GL_STENCIL_PASS_DEPTH_PASS"/></term>
        /// <description>params returns one value, a symbolic constant indicating what action is taken when the stencil test passes and the depth test passes. The initial value is GL_KEEP. See glStencilOp. If the GL version is 2.0 or greater, this stencil state only affects non-polygons and front-facing polygons. Back-facing polygons use separate stencil state. See glStencilOpSeparate.</description>
        /// <term><see cref="GL_STENCIL_REF"/></term>
        /// <description>params returns one value, the reference value that is compared with the contents of the stencil buffer. The initial value is 0. See glStencilFunc. If the GL version is 2.0 or greater, this stencil state only affects non-polygons and front-facing polygons. Back-facing polygons use separate stencil state. See glStencilFuncSeparate.</description>
        /// <term><see cref="GL_STENCIL_TEST"/></term>
        /// <description>params returns a single boolean value indicating whether stencil testing of fragments is enabled. The initial value is GL_FALSE. See glStencilFunc and glStencilOp.</description>
        /// <term><see cref="GL_STENCIL_VALUE_MASK"/></term>
        /// <description>params returns one value, the mask that is used to mask both the stencil reference value and the stencil buffer value before they are compared. The initial value is all 1's. See glStencilFunc. If the GL version is 2.0 or greater, this stencil state only affects non-polygons and front-facing polygons. Back-facing polygons use separate stencil state. See glStencilFuncSeparate.</description>
        /// <term><see cref="GL_STENCIL_WRITEMASK"/></term>
        /// <description>params returns one value, the mask that controls writing of the stencil bitplanes. The initial value is all 1's. See glStencilMask. If the GL version is 2.0 or greater, this stencil state only affects non-polygons and front-facing polygons. Back-facing polygons use separate stencil state. See glStencilMaskSeparate.</description>
        /// <term><see cref="GL_STEREO"/></term>
        /// <description>params returns a single boolean value indicating whether stereo buffers (left and right) are supported.</description>
        /// <term><see cref="GL_SUBPIXEL_BITS"/></term>
        /// <description>params returns one value, an estimate of the number of bits of subpixel resolution that are used to position rasterized geometry in window coordinates. The value must be at least 4.</description>
        /// <term><see cref="GL_TEXTURE_1D"/></term>
        /// <description>params returns a single boolean value indicating whether 1D texture mapping is enabled. The initial value is GL_FALSE. See glTexImage1D.</description>
        /// <term><see cref="GL_TEXTURE_BINDING_1D"/></term>
        /// <description>params returns a single value, the name of the texture currently bound to the target GL_TEXTURE_1D. The initial value is 0. See glBindTexture.</description>
        /// <term><see cref="GL_TEXTURE_2D"/></term>
        /// <description>params returns a single boolean value indicating whether 2D texture mapping is enabled. The initial value is GL_FALSE. See glTexImage2D.</description>
        /// <term><see cref="GL_TEXTURE_BINDING_2D"/></term>
        /// <description>params returns a single value, the name of the texture currently bound to the target GL_TEXTURE_2D. The initial value is 0. See glBindTexture.</description>
        /// <term><see cref="GL_TEXTURE_COORD_ARRAY"/></term>
        /// <description>params returns a single boolean value indicating whether the texture coordinate array is enabled. The initial value is GL_FALSE. See glTexCoordPointer.</description>
        /// <term><see cref="GL_TEXTURE_COORD_ARRAY_SIZE"/></term>
        /// <description>params returns one value, the number of coordinates per element in the texture coordinate array. The initial value is 4. See glTexCoordPointer.</description>
        /// <term><see cref="GL_TEXTURE_COORD_ARRAY_STRIDE"/></term>
        /// <description>params returns one value, the byte offset between consecutive elements in the texture coordinate array. The initial value is 0. See glTexCoordPointer.</description>
        /// <term><see cref="GL_TEXTURE_COORD_ARRAY_TYPE"/></term>
        /// <description>params returns one value, the data type of the coordinates in the texture coordinate array. The initial value is GL_FLOAT. See glTexCoordPointer.</description>
        /// <term><see cref="GL_TEXTURE_GEN_Q"/></term>
        /// <description>params returns a single boolean value indicating whether automatic generation of the q texture coordinate is enabled. The initial value is GL_FALSE. See glTexGen.</description>
        /// <term><see cref="GL_TEXTURE_GEN_R"/></term>
        /// <description>params returns a single boolean value indicating whether automatic generation of the r texture coordinate is enabled. The initial value is GL_FALSE. See glTexGen.</description>
        /// <term><see cref="GL_TEXTURE_GEN_S"/></term>
        /// <description>params returns a single boolean value indicating whether automatic generation of the S texture coordinate is enabled. The initial value is GL_FALSE. See glTexGen.</description>
        /// <term><see cref="GL_TEXTURE_GEN_T"/></term>
        /// <description>params returns a single boolean value indicating whether automatic generation of the T texture coordinate is enabled. The initial value is GL_FALSE. See glTexGen.</description>
        /// <term><see cref="GL_TEXTURE_MATRIX"/></term>
        /// <description>params returns sixteen values: the texture matrix on the top of the texture matrix stack. Initially this matrix is the identity matrix. See glPushMatrix.</description>
        /// <term><see cref="GL_TEXTURE_STACK_DEPTH"/></term>
        /// <description>params returns one value, the number of matrices on the texture matrix stack. The initial value is 1. See glPushMatrix.</description>
        /// <term><see cref="GL_UNPACK_ALIGNMENT"/></term>
        /// <description>params returns one value, the byte alignment used for reading pixel data from memory. The initial value is 4. See glPixelStore.</description>
        /// <term><see cref="GL_UNPACK_LSB_FIRST"/></term>
        /// <description>params returns a single boolean value indicating whether single-bit pixels being read from memory are read first from the least significant bit of each unsigned byte. The initial value is GL_FALSE. See glPixelStore.</description>
        /// <term><see cref="GL_UNPACK_ROW_LENGTH"/></term>
        /// <description>params returns one value, the row length used for reading pixel data from memory. The initial value is 0. See glPixelStore.</description>
        /// <term><see cref="GL_UNPACK_SKIP_PIXELS"/></term>
        /// <description>params returns one value, the number of pixel locations skipped before the first pixel is read from memory. The initial value is 0. See glPixelStore.</description>
        /// <term><see cref="GL_UNPACK_SKIP_ROWS"/></term>
        /// <description>params returns one value, the number of rows of pixel locations skipped before the first pixel is read from memory. The initial value is 0. See glPixelStore.</description>
        /// <term><see cref="GL_UNPACK_SWAP_BYTES"/></term>
        /// <description>params returns a single boolean value indicating whether the bytes of two-byte and four-byte pixel indices and components are swapped after being read from memory. The initial value is GL_FALSE. See glPixelStore.</description>
        /// <term><see cref="GL_VERTEX_ARRAY"/></term>
        /// <description>params returns a single boolean value indicating whether the vertex array is enabled. The initial value is GL_FALSE. See glVertexPointer.</description>
        /// <term><see cref="GL_VERTEX_ARRAY_SIZE"/></term>
        /// <description>params returns one value, the number of coordinates per vertex in the vertex array. The initial value is 4. See glVertexPointer.</description>
        /// <term><see cref="GL_VERTEX_ARRAY_STRIDE"/></term>
        /// <description>params returns one value, the byte offset between consecutive vertices in the vertex array. The initial value is 0. See glVertexPointer.</description>
        /// <term><see cref="GL_VERTEX_ARRAY_TYPE"/></term>
        /// <description>params returns one value, the data type of each coordinate in the vertex array. The initial value is GL_FLOAT. See glVertexPointer.</description>
        /// <term><see cref="GL_VIEWPORT"/></term>
        /// <description>params returns four values: the x and y window coordinates of the viewport, followed by its width and height. Initially the x and y window coordinates are both set to 0, and the width and height are set to the width and height of the window into which the GL will do its rendering. See glViewport.</description>
        /// <term><see cref="GL_ZOOM_X"/></term>
        /// <description>params returns one value, the x pixel zoom factor. The initial value is 1. See glPixelZoom.</description>
        /// <term><see cref="GL_ZOOM_Y"/></term>
        /// <description>params returns one value, the y pixel zoom factor. The initial value is 1. See glPixelZoom.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetBooleanv(GLenum pname, GLboolean* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plane">
        /// 
        /// </param>
        /// <param name="equation">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetClipPlane(GLenum plane, GLdouble* equation);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetDoublev(GLenum pname, GLdouble* @params);

        /// <summary>
        /// Возвращает информацию об ошибке.
        /// </summary>
        /// <returns>
        /// <para>Возвращает один из следующих кодов ошибок:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_NO_ERROR"/></term>
        /// <description>Не было зарегистрировано ни одной ошибки.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_ENUM"/></term>
        /// <description>Недопустимое значение для перечисляемого аргумента.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_VALUE"/></term>
        /// <description>Числовой аргумент находится вне допустимого диапазона значений.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Указанная операция не разрешена в текущем состоянии.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_STACK_OVERFLOW"/></term>
        /// <description>Команда вызовет переполнение стека.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_STACK_UNDERFLOW"/></term>
        /// <description>Команда вызовет опустошение стека.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_OUT_OF_MEMORY"/></term>
        /// <description>Недостаточно памяти для выполнения команды.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </returns>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern GLenum glGetError();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetFloatv(GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetIntegerv(GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="light">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetLightfv(GLenum light, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="light">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetLightiv(GLenum light, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="query">
        /// 
        /// </param>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetMapdv(GLenum target, GLenum query, GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="query">
        /// 
        /// </param>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetMapfv(GLenum target, GLenum query, GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="query">
        /// 
        /// </param>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetMapiv(GLenum target, GLenum query, GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="face">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetMaterialfv(GLenum face, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="face">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetMaterialiv(GLenum face, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="map">
        /// 
        /// </param>
        /// <param name="values">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetPixelMapfv(GLenum map, GLfloat* values);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="map">
        /// 
        /// </param>
        /// <param name="values">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetPixelMapuiv(GLenum map, GLuint* values);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="map">
        /// 
        /// </param>
        /// <param name="values">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetPixelMapusv(GLenum map, GLushort* values);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetPointerv(GLenum pname, void** @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mask">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetPolygonStipple(GLubyte* mask);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">
        /// 
        /// </param>
        /// <returns></returns>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe GLubyte* glGetString(GLenum name);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetTexEnvfv(GLenum target, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetTexEnviv(GLenum target, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetTexGendv(GLenum coord, GLenum pname, GLdouble* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetTexGenfv(GLenum coord, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetTexGeniv(GLenum coord, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="format">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="pixels">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetTexImage(GLenum target, GLint level, GLenum format, GLenum type, void* pixels);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetTexLevelParameterfv(GLenum target, GLint level, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetTexLevelParameteriv(GLenum target, GLint level, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetTexParameterfv(GLenum target, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glGetTexParameteriv(GLenum target, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="mode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glHint(GLenum target, GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mask">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glIndexMask(GLuint mask);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="stride">
        /// 
        /// </param>
        /// <param name="pointer">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glIndexPointer(GLenum type, GLsizei stride, void* pointer);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glIndexd(GLdouble c);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glIndexdv(GLdouble* c);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glIndexf(GLfloat c);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glIndexfv(GLfloat* c);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glIndexi(GLint c);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glIndexiv(GLint* c);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glIndexs(GLshort c);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glIndexsv(GLshort* c);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glIndexub(GLubyte c);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glIndexubv(GLubyte* c);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glInitNames();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">
        /// 
        /// </param>
        /// <param name="stride">
        /// 
        /// </param>
        /// <param name="pointer">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glInterleavedArrays(GLenum format, GLsizei stride, void* pointer);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cap">
        /// 
        /// </param>
        /// <returns></returns>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern GLboolean glIsEnabled(GLenum cap);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">
        /// 
        /// </param>
        /// <returns></returns>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern GLboolean glIsList(GLuint list);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="texture">
        /// 
        /// </param>
        /// <returns></returns>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern GLboolean glIsTexture(GLuint texture);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glLightModelf(GLenum pname, GLfloat param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glLightModelfv(GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glLightModeli(GLenum pname, GLint param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glLightModeliv(GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="light">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glLightf(GLenum light, GLenum pname, GLfloat param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="light">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glLightfv(GLenum light, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="light">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glLighti(GLenum light, GLenum pname, GLint param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="light">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glLightiv(GLenum light, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factor">
        /// 
        /// </param>
        /// <param name="pattern">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glLineStipple(GLint factor, GLushort pattern);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glLineWidth(GLfloat width);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="base">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glListBase(GLuint @base);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glLoadIdentity();

        /// <summary>
        /// Заменяют текущую матрицу.
        /// </summary>
        /// <param name="m">
        /// Указатель на матрицу размера 4 x 4, сохраненную в главном порядке столбцов как 16 последовательных значений.
        /// </param>
        /// <remarks>
        /// <para>В случае ошибки функция <see cref="glGetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Функция была вызвана между вызовами функций <see cref="glBegin"/> и <see cref="glEnd"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glLoadMatrixd(GLdouble* m);

        /// <summary>
        /// Заменяют текущую матрицу.
        /// </summary>
        /// <param name="m">
        /// Указатель на матрицу размера 4 x 4, сохраненную в главном порядке столбцов как 16 последовательных значений.
        /// </param>
        /// <remarks>
        /// <para>В случае ошибки функция <see cref="glGetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Функция была вызвана между вызовами функций <see cref="glBegin"/> и <see cref="glEnd"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glLoadMatrixf(GLfloat* m);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glLoadName(GLuint name);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opcode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glLogicOp(GLenum opcode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="u1">
        /// 
        /// </param>
        /// <param name="u2">
        /// 
        /// </param>
        /// <param name="stride">
        /// 
        /// </param>
        /// <param name="order">
        /// 
        /// </param>
        /// <param name="points">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glMap1d(GLenum target, GLdouble u1, GLdouble u2, GLint stride, GLint order, GLdouble* points);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="u1">
        /// 
        /// </param>
        /// <param name="u2">
        /// 
        /// </param>
        /// <param name="stride">
        /// 
        /// </param>
        /// <param name="order">
        /// 
        /// </param>
        /// <param name="points">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glMap1f(GLenum target, GLfloat u1, GLfloat u2, GLint stride, GLint order, GLfloat* points);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="u1">
        /// 
        /// </param>
        /// <param name="u2">
        /// 
        /// </param>
        /// <param name="ustride">
        /// 
        /// </param>
        /// <param name="uorder">
        /// 
        /// </param>
        /// <param name="v1">
        /// 
        /// </param>
        /// <param name="v2">
        /// 
        /// </param>
        /// <param name="vstride">
        /// 
        /// </param>
        /// <param name="vorder">
        /// 
        /// </param>
        /// <param name="points">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glMap2d(GLenum target, GLdouble u1, GLdouble u2, GLint ustride, GLint uorder, GLdouble v1, GLdouble v2, GLint vstride, GLint vorder, GLdouble* points);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="u1">
        /// 
        /// </param>
        /// <param name="u2">
        /// 
        /// </param>
        /// <param name="ustride">
        /// 
        /// </param>
        /// <param name="uorder">
        /// 
        /// </param>
        /// <param name="v1">
        /// 
        /// </param>
        /// <param name="v2">
        /// 
        /// </param>
        /// <param name="vstride">
        /// 
        /// </param>
        /// <param name="vorder">
        /// 
        /// </param>
        /// <param name="points">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glMap2f(GLenum target, GLfloat u1, GLfloat u2, GLint ustride, GLint uorder, GLfloat v1, GLfloat v2, GLint vstride, GLint vorder, GLfloat* points);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="un">
        /// 
        /// </param>
        /// <param name="u1">
        /// 
        /// </param>
        /// <param name="u2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glMapGrid1d(GLint un, GLdouble u1, GLdouble u2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="un">
        /// 
        /// </param>
        /// <param name="u1">
        /// 
        /// </param>
        /// <param name="u2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glMapGrid1f(GLint un, GLfloat u1, GLfloat u2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="un">
        /// 
        /// </param>
        /// <param name="u1">
        /// 
        /// </param>
        /// <param name="u2">
        /// 
        /// </param>
        /// <param name="vn">
        /// 
        /// </param>
        /// <param name="v1">
        /// 
        /// </param>
        /// <param name="v2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glMapGrid2d(GLint un, GLdouble u1, GLdouble u2, GLint vn, GLdouble v1, GLdouble v2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="un">
        /// 
        /// </param>
        /// <param name="u1">
        /// 
        /// </param>
        /// <param name="u2">
        /// 
        /// </param>
        /// <param name="vn">
        /// 
        /// </param>
        /// <param name="v1">
        /// 
        /// </param>
        /// <param name="v2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glMapGrid2f(GLint un, GLfloat u1, GLfloat u2, GLint vn, GLfloat v1, GLfloat v2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="face">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glMaterialf(GLenum face, GLenum pname, GLfloat param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="face">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glMaterialfv(GLenum face, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="face">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glMateriali(GLenum face, GLenum pname, GLint param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="face">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glMaterialiv(GLenum face, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glMatrixMode(GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glMultMatrixd(GLdouble* m);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glMultMatrixf(GLfloat* m);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">
        /// 
        /// </param>
        /// <param name="mode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glNewList(GLuint list, GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nx">
        /// 
        /// </param>
        /// <param name="ny">
        /// 
        /// </param>
        /// <param name="nz">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glNormal3b(GLbyte nx, GLbyte ny, GLbyte nz);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glNormal3bv(GLbyte* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nx">
        /// 
        /// </param>
        /// <param name="ny">
        /// 
        /// </param>
        /// <param name="nz">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glNormal3d(GLdouble nx, GLdouble ny, GLdouble nz);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glNormal3dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nx">
        /// 
        /// </param>
        /// <param name="ny">
        /// 
        /// </param>
        /// <param name="nz">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glNormal3f(GLfloat nx, GLfloat ny, GLfloat nz);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glNormal3fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nx">
        /// 
        /// </param>
        /// <param name="ny">
        /// 
        /// </param>
        /// <param name="nz">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glNormal3i(GLint nx, GLint ny, GLint nz);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glNormal3iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nx">
        /// 
        /// </param>
        /// <param name="ny">
        /// 
        /// </param>
        /// <param name="nz">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glNormal3s(GLshort nx, GLshort ny, GLshort nz);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glNormal3sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="stride">
        /// 
        /// </param>
        /// <param name="pointer">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glNormalPointer(GLenum type, GLsizei stride, void* pointer);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left">
        /// 
        /// </param>
        /// <param name="right">
        /// 
        /// </param>
        /// <param name="bottom">
        /// 
        /// </param>
        /// <param name="top">
        /// 
        /// </param>
        /// <param name="zNear">
        /// 
        /// </param>
        /// <param name="zFar">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glOrtho(GLdouble left, GLdouble right, GLdouble bottom, GLdouble top, GLdouble zNear, GLdouble zFar);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPassThrough(GLfloat token);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="map">
        /// 
        /// </param>
        /// <param name="mapsize">
        /// 
        /// </param>
        /// <param name="values">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glPixelMapfv(GLenum map, GLsizei mapsize, GLfloat* values);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="map">
        /// 
        /// </param>
        /// <param name="mapsize">
        /// 
        /// </param>
        /// <param name="values">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glPixelMapuiv(GLenum map, GLsizei mapsize, GLuint* values);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="map">
        /// 
        /// </param>
        /// <param name="mapsize">
        /// 
        /// </param>
        /// <param name="values">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glPixelMapusv(GLenum map, GLsizei mapsize, GLushort* values);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPixelStoref(GLenum pname, GLfloat param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPixelStorei(GLenum pname, GLint param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPixelTransferf(GLenum pname, GLfloat param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPixelTransferi(GLenum pname, GLint param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xfactor">
        /// 
        /// </param>
        /// <param name="yfactor">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPixelZoom(GLfloat xfactor, GLfloat yfactor);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPointSize(GLfloat size);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="face">
        /// 
        /// </param>
        /// <param name="mode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPolygonMode(GLenum face, GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factor">
        /// 
        /// </param>
        /// <param name="units">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPolygonOffset(GLfloat factor, GLfloat units);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mask">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glPolygonStipple(GLubyte* mask);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPopAttrib();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPopClientAttrib();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPopMatrix();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPopName();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">
        /// 
        /// </param>
        /// <param name="textures">
        /// 
        /// </param>
        /// <param name="priorities">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glPrioritizeTextures(GLsizei n, GLuint* textures, GLclampf* priorities);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mask">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPushAttrib(GLbitfield mask);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mask">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPushClientAttrib(GLbitfield mask);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glPushMatrix();

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glPushName(GLuint name);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos2d(GLdouble x, GLdouble y);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos2dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos2f(GLfloat x, GLfloat y);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos2fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos2i(GLint x, GLint y);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos2iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos2s(GLshort x, GLshort y);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos2sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos3d(GLdouble x, GLdouble y, GLdouble z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos3dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos3f(GLfloat x, GLfloat y, GLfloat z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos3fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos3i(GLint x, GLint y, GLint z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos3iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos3s(GLshort x, GLshort y, GLshort z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos3sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        /// <param name="w">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos4d(GLdouble x, GLdouble y, GLdouble z, GLdouble w);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos4dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        /// <param name="w">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos4f(GLfloat x, GLfloat y, GLfloat z, GLfloat w);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos4fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        /// <param name="w">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos4i(GLint x, GLint y, GLint z, GLint w);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos4iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        /// <param name="w">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRasterPos4s(GLshort x, GLshort y, GLshort z, GLshort w);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRasterPos4sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glReadBuffer(GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="height">
        /// 
        /// </param>
        /// <param name="format">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="pixels">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glReadPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, void* pixels);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1">
        /// 
        /// </param>
        /// <param name="y1">
        /// 
        /// </param>
        /// <param name="x2">
        /// 
        /// </param>
        /// <param name="y2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRectd(GLdouble x1, GLdouble y1, GLdouble x2, GLdouble y2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1">
        /// 
        /// </param>
        /// <param name="v2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRectdv(GLdouble* v1, GLdouble* v2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1">
        /// 
        /// </param>
        /// <param name="y1">
        /// 
        /// </param>
        /// <param name="x2">
        /// 
        /// </param>
        /// <param name="y2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRectf(GLfloat x1, GLfloat y1, GLfloat x2, GLfloat y2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1">
        /// 
        /// </param>
        /// <param name="v2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRectfv(GLfloat* v1, GLfloat* v2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1">
        /// 
        /// </param>
        /// <param name="y1">
        /// 
        /// </param>
        /// <param name="x2">
        /// 
        /// </param>
        /// <param name="y2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRecti(GLint x1, GLint y1, GLint x2, GLint y2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1">
        /// 
        /// </param>
        /// <param name="v2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRectiv(GLint* v1, GLint* v2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1">
        /// 
        /// </param>
        /// <param name="y1">
        /// 
        /// </param>
        /// <param name="x2">
        /// 
        /// </param>
        /// <param name="y2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRects(GLshort x1, GLshort y1, GLshort x2, GLshort y2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1">
        /// 
        /// </param>
        /// <param name="v2">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glRectsv(GLshort* v1, GLshort* v2);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        /// <returns></returns>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern GLint glRenderMode(GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle">
        /// 
        /// </param>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRotated(GLdouble angle, GLdouble x, GLdouble y, GLdouble z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle">
        /// 
        /// </param>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glRotatef(GLfloat angle, GLfloat x, GLfloat y, GLfloat z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glScaled(GLdouble x, GLdouble y, GLdouble z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glScalef(GLfloat x, GLfloat y, GLfloat z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="height">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glScissor(GLint x, GLint y, GLsizei width, GLsizei height);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">
        /// 
        /// </param>
        /// <param name="buffer">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glSelectBuffer(GLsizei size, GLuint* buffer);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glShadeModel(GLenum mode);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func">
        /// 
        /// </param>
        /// <param name="ref">
        /// 
        /// </param>
        /// <param name="mask">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glStencilFunc(GLenum func, GLint @ref, GLuint mask);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mask">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern void glStencilMask(GLuint mask);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fail">
        /// 
        /// </param>
        /// <param name="zfail">
        /// 
        /// </param>
        /// <param name="zpass">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glStencilOp(GLenum fail, GLenum zfail, GLenum zpass);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord1d(GLdouble s);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord1dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord1f(GLfloat s);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord1fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord1i(GLint s);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord1iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord1s(GLshort s);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord1sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord2d(GLdouble s, GLdouble t);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord2dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord2f(GLfloat s, GLfloat t);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord2fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord2i(GLint s, GLint t);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord2iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord2s(GLshort s, GLshort t);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord2sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        /// <param name="r">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord3d(GLdouble s, GLdouble t, GLdouble r);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord3dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        /// <param name="r">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord3f(GLfloat s, GLfloat t, GLfloat r);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord3fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        /// <param name="r">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord3i(GLint s, GLint t, GLint r);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord3iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        /// <param name="r">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord3s(GLshort s, GLshort t, GLshort r);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord3sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        /// <param name="r">
        /// 
        /// </param>
        /// <param name="q">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord4d(GLdouble s, GLdouble t, GLdouble r, GLdouble q);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord4dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        /// <param name="r">
        /// 
        /// </param>
        /// <param name="q">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord4f(GLfloat s, GLfloat t, GLfloat r, GLfloat q);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord4fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        /// <param name="r">
        /// 
        /// </param>
        /// <param name="q">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord4i(GLint s, GLint t, GLint r, GLint q);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord4iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">
        /// 
        /// </param>
        /// <param name="t">
        /// 
        /// </param>
        /// <param name="r">
        /// 
        /// </param>
        /// <param name="q">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexCoord4s(GLshort s, GLshort t, GLshort r, GLshort q);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoord4sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="stride">
        /// 
        /// </param>
        /// <param name="pointer">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexCoordPointer(GLint size, GLenum type, GLsizei stride, void* pointer);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexEnvf(GLenum target, GLenum pname, GLfloat param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexEnvfv(GLenum target, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexEnvi(GLenum target, GLenum pname, GLint param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexEnviv(GLenum target, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexGend(GLenum coord, GLenum pname, GLdouble param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexGendv(GLenum coord, GLenum pname, GLdouble* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexGenf(GLenum coord, GLenum pname, GLfloat param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexGenfv(GLenum coord, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexGeni(GLenum coord, GLenum pname, GLint param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coord">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexGeniv(GLenum coord, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="internalformat">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="border">
        /// 
        /// </param>
        /// <param name="format">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="pixels">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexImage1D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLint border, GLenum format, GLenum type, void* pixels);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="internalformat">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="height">
        /// 
        /// </param>
        /// <param name="border">
        /// 
        /// </param>
        /// <param name="format">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="pixels">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexImage2D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, void* pixels);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexParameterf(GLenum target, GLenum pname, GLfloat param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexParameterfv(GLenum target, GLenum pname, GLfloat* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="param">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTexParameteri(GLenum target, GLenum pname, GLint param);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="pname">
        /// 
        /// </param>
        /// <param name="params">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexParameteriv(GLenum target, GLenum pname, GLint* @params);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="xoffset">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="format">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="pixels">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, void* pixels);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">
        /// 
        /// </param>
        /// <param name="level">
        /// 
        /// </param>
        /// <param name="xoffset">
        /// 
        /// </param>
        /// <param name="yoffset">
        /// 
        /// </param>
        /// <param name="width">
        /// 
        /// </param>
        /// <param name="height">
        /// 
        /// </param>
        /// <param name="format">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="pixels">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, void* pixels);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTranslated(GLdouble x, GLdouble y, GLdouble z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glTranslatef(GLfloat x, GLfloat y, GLfloat z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex2d(GLdouble x, GLdouble y);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex2dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex2f(GLfloat x, GLfloat y);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex2fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex2i(GLint x, GLint y);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex2iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex2s(GLshort x, GLshort y);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex2sv(GLshort* v);

        /// <summary>
        /// Определяет вершину.
        /// </summary>
        /// <param name="x">Абсцисса вершины.</param>
        /// <param name="y">Ордината вершины.</param>
        /// <param name="z">Аппликата вершины.</param>
        /// <remarks>
        /// Функция должна быть вызвана между вызовами функций <see cref="glBegin"/> и <see cref="glEnd"/>.
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex3d(GLdouble x, GLdouble y, GLdouble z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex3dv(GLdouble* v);

        /// <summary>
        /// Определяет вершину.
        /// </summary>
        /// <param name="x">Абсцисса вершины.</param>
        /// <param name="y">Ордината вершины.</param>
        /// <param name="z">Аппликата вершины.</param>
        /// <remarks>
        /// Функция должна быть вызвана между вызовами функций <see cref="glBegin"/> и <see cref="glEnd"/>.
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex3f(GLfloat x, GLfloat y, GLfloat z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex3fv(GLfloat* v);

        /// <summary>
        /// Определяет вершину.
        /// </summary>
        /// <param name="x">Абсцисса вершины.</param>
        /// <param name="y">Ордината вершины.</param>
        /// <param name="z">Аппликата вершины.</param>
        /// <remarks>
        /// Функция должна быть вызвана между вызовами функций <see cref="glBegin"/> и <see cref="glEnd"/>.
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex3i(GLint x, GLint y, GLint z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex3iv(GLint* v);

        /// <summary>
        /// Определяет вершину.
        /// </summary>
        /// <param name="x">Абсцисса вершины.</param>
        /// <param name="y">Ордината вершины.</param>
        /// <param name="z">Аппликата вершины.</param>
        /// <remarks>
        /// Функция должна быть вызвана между вызовами функций <see cref="glBegin"/> и <see cref="glEnd"/>.
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex3s(GLshort x, GLshort y, GLshort z);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex3sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        /// <param name="w">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex4d(GLdouble x, GLdouble y, GLdouble z, GLdouble w);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex4dv(GLdouble* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        /// <param name="w">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex4f(GLfloat x, GLfloat y, GLfloat z, GLfloat w);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex4fv(GLfloat* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        /// <param name="w">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex4i(GLint x, GLint y, GLint z, GLint w);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex4iv(GLint* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">
        /// 
        /// </param>
        /// <param name="y">
        /// 
        /// </param>
        /// <param name="z">
        /// 
        /// </param>
        /// <param name="w">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glVertex4s(GLshort x, GLshort y, GLshort z, GLshort w);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertex4sv(GLshort* v);

        /// <remarks>
        /// 
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size">
        /// 
        /// </param>
        /// <param name="type">
        /// 
        /// </param>
        /// <param name="stride">
        /// 
        /// </param>
        /// <param name="pointer">
        /// 
        /// </param>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        [CLSCompliant(false)]
        public static extern unsafe void glVertexPointer(GLint size, GLenum type, GLsizei stride, void* pointer);

        /// <summary>
        /// Устанавливает область просмотра.
        /// </summary>
        /// <param name="x">
        /// Абсцисса нижней левой точки области просмотра в пикселях.
        /// </param>
        /// <param name="y">
        /// Ордината нижней левой точки области просмотра в пикселях.
        /// </param>
        /// <param name="width">
        /// Ширина области просмотра в пикселях.
        /// </param>
        /// <param name="height">
        /// Высота области просмотра в пикселях.
        /// </param>
        /// <remarks>
        /// <para>
        /// Когда контекст рендеринга OpenGL впервые присоединяется к окну,
        /// ширина и высота устанавливаются в соответствии с размерами этого окна,
        /// а координаты левой точки области просмотра инициализируются нулевыми значениями.
        /// </para>
        /// <para>В случае ошибки функция <see cref="glGetError"/> может вернуть одно из следующих значений:</para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term><see cref="GL_INVALID_VALUE"/></term>
        /// <description>В параметре <paramref name="height"/> или <paramref name="width"/> передано отрицательное значение.</description>
        /// </item>
        /// <item>
        /// <term><see cref="GL_INVALID_OPERATION"/></term>
        /// <description>Функция была вызвана между вызовами функций <see cref="glBegin"/> и <see cref="glEnd"/>.</description>
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// Ширина и высота области просмотра ограничены диапазоном, который зависит от реализации.
        /// Этот диапазон запрашивается путем вызова <see cref="glGetIntegerv"/> с аргументом <see cref="GL_MAX_VIEWPORT_DIMS"/>.
        /// </para>
        /// <para>
        /// Следующие функции извлекают вспомогательную информацию:
        /// </para>
        /// <para>
        /// <list type="bullet">
        /// <item><see cref="glGetIntegerv"/> с аргументом <see cref="GL_VIEWPORT"/></item>
        /// <item><see cref="glGetIntegerv"/> с аргументом <see cref="GL_MAX_VIEWPORT_DIMS"/></item>
        /// </list>
        /// </para>
        /// </remarks>
        [DllImport("Opengl32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        public static extern void glViewport(GLint x, GLint y, GLsizei width, GLsizei height);

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERSION_1_1 = 1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ACCUM = 0x0100;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LOAD = 0x0101;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RETURN = 0x0102;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MULT = 0x0103;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ADD = 0x0104;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NEVER = 0x0200;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LESS = 0x0201;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EQUAL = 0x0202;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LEQUAL = 0x0203;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_GREATER = 0x0204;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NOTEQUAL = 0x0205;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_GEQUAL = 0x0206;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALWAYS = 0x0207;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_BIT = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POINT_BIT = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_BIT = 0x00000004;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_BIT = 0x00000008;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_STIPPLE_BIT = 0x00000010;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MODE_BIT = 0x00000020;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHTING_BIT = 0x00000040;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FOG_BIT = 0x00000080;

        /// <summary>
        /// Буфер глубины.
        /// </summary>
        public const GLenum GL_DEPTH_BUFFER_BIT = 0x00000100;

        /// <summary>
        /// Буфер накопления.
        /// </summary>
        public const GLenum GL_ACCUM_BUFFER_BIT = 0x00000200;

        /// <summary>
        /// Трафаретный буфер.
        /// </summary>
        public const GLenum GL_STENCIL_BUFFER_BIT = 0x00000400;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VIEWPORT_BIT = 0x00000800;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TRANSFORM_BIT = 0x00001000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ENABLE_BIT = 0x00002000;

        /// <summary>
        /// Цветовой буфер.
        /// </summary>
        public const GLenum GL_COLOR_BUFFER_BIT = 0x00004000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_HINT_BIT = 0x00008000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EVAL_BIT = 0x00010000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIST_BIT = 0x00020000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_BIT = 0x00040000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SCISSOR_BIT = 0x00080000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALL_ATTRIB_BITS = 0x000fffff;

        /// <summary>
        /// Значение, указывающее, что каждую вершину необходимо обрабатывать как одну точку.
        /// </summary>
        public const GLenum GL_POINTS = 0x0000;

        /// <summary>
        /// Значение, указывающее, что каждую пару вершин необходимо обрабатывать как независимый отрезок.
        /// </summary>
        public const GLenum GL_LINES = 0x0001;

        /// <summary>
        /// Значение, указывающее, что вершины необходимо обрабатывать как связанную группу отрезков линии от первой вершины до последней, затем обратно до первой.
        /// </summary>
        public const GLenum GL_LINE_LOOP = 0x0002;

        /// <summary>
        /// Значение, указывающее, что вершины необходимо обрабатывать как связанную группу отрезков линии от первой вершины до последней.
        /// </summary>
        public const GLenum GL_LINE_STRIP = 0x0003;

        /// <summary>
        /// Значение, указывающее, что каждую тройку вершин необходимо обрабатывать как независимый треугольник.
        /// </summary>
        public const GLenum GL_TRIANGLES = 0x0004;

        /// <summary>
        /// <para>Значение, указывающее, что вершины необходимо обрабатывать как связанную группу треугольников от первой вершины до последней.</para>
        /// <para>Один треугольник определен для каждой вершины, начиная с третьей.Треугольник определяется тремя последовательными вершинами.</para>
        /// </summary>
        public const GLenum GL_TRIANGLE_STRIP = 0x0005;

        /// <summary>
        /// <para>Значение, указывающее, что вершины необходимо обрабатывать как связанную группу треугольников от первой вершины до последней.</para>
        /// <para>Один треугольник определен для каждой вершины, начиная с третьей. Треугольник определяется первой вершиной и двумя последовательными вершинами.</para>
        /// </summary>
        public const GLenum GL_TRIANGLE_FAN = 0x0006;

        /// <summary>
        /// Значение, указывающее, что каждые четыре вершины необходимо обрабатывать как независимый четырехугольник.
        /// </summary>
        public const GLenum GL_QUADS = 0x0007;

        /// <summary>
        /// <para>Значение, указывающее, что вершины необходимо обрабатывать как связанную группу четырехугольников от первой вершины до последней.</para>
        /// <para>Один четырехугольник определен для каждой вершины, начиная с четвёртой. Четырехугольник определяется четырьмя последовательными вершинами.</para>
        /// </summary>
        public const GLenum GL_QUAD_STRIP = 0x0008;

        /// <summary>
        /// Значение, указывающее, что вершины необходимо обрабатывать как многоугольник.
        /// </summary>
        public const GLenum GL_POLYGON = 0x0009;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ZERO = 0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ONE = 1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SRC_COLOR = 0x0300;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ONE_MINUS_SRC_COLOR = 0x0301;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SRC_ALPHA = 0x0302;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ONE_MINUS_SRC_ALPHA = 0x0303;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DST_ALPHA = 0x0304;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ONE_MINUS_DST_ALPHA = 0x0305;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DST_COLOR = 0x0306;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ONE_MINUS_DST_COLOR = 0x0307;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SRC_ALPHA_SATURATE = 0x0308;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TRUE = 1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FALSE = 0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLIP_PLANE0 = 0x3000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLIP_PLANE1 = 0x3001;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLIP_PLANE2 = 0x3002;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLIP_PLANE3 = 0x3003;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLIP_PLANE4 = 0x3004;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLIP_PLANE5 = 0x3005;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BYTE = 0x1400;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_UNSIGNED_BYTE = 0x1401;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SHORT = 0x1402;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_UNSIGNED_SHORT = 0x1403;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INT = 0x1404;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_UNSIGNED_INT = 0x1405;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FLOAT = 0x1406;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_2_BYTES = 0x1407;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_3_BYTES = 0x1408;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_4_BYTES = 0x1409;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DOUBLE = 0x140A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NONE = 0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FRONT_LEFT = 0x0400;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FRONT_RIGHT = 0x0401;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BACK_LEFT = 0x0402;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BACK_RIGHT = 0x0403;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FRONT = 0x0404;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BACK = 0x0405;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LEFT = 0x0406;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RIGHT = 0x0407;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FRONT_AND_BACK = 0x0408;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AUX0 = 0x0409;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AUX1 = 0x040A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AUX2 = 0x040B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AUX3 = 0x040C;

        /// <summary>
        /// Не было зарегистрировано ни одной ошибки.
        /// </summary>
        public const GLenum GL_NO_ERROR = 0;

        /// <summary>
        /// Недопустимое значение для перечисляемого аргумента.
        /// Команда-нарушитель игнорируется и не имеет другого побочного эффекта, кроме как установить флаг ошибки.
        /// </summary>
        public const GLenum GL_INVALID_ENUM = 0x0500;

        /// <summary>
        /// Числовой аргумент находится вне допустимого диапазона значений.
        /// Команда-нарушитель игнорируется и не имеет другого побочного эффекта, кроме как установить флаг ошибки.
        /// </summary>
        public const GLenum GL_INVALID_VALUE = 0x0501;

        /// <summary>
        /// Указанная операция не разрешена в текущем состоянии.
        /// Команда-нарушитель игнорируется и не имеет другого побочного эффекта, кроме как установить флаг ошибки.
        /// </summary>
        public const GLenum GL_INVALID_OPERATION = 0x0502;

        /// <summary>
        /// Команда вызовет переполнение стека.
        /// Команда-нарушитель игнорируется и не имеет другого побочного эффекта, кроме как установить флаг ошибки.
        /// </summary>
        public const GLenum GL_STACK_OVERFLOW = 0x0503;

        /// <summary>
        /// Команда вызовет опустошение стека.
        /// Команда-нарушитель игнорируется и не имеет другого побочного эффекта, кроме как установить флаг ошибки.
        /// </summary>
        public const GLenum GL_STACK_UNDERFLOW = 0x0504;

        /// <summary>
        /// Недостаточно памяти для выполнения команды.
        /// Состояние графической библиотеки не определено, за исключением состояния флагов ошибок, после того как эта ошибка записана.
        /// </summary>
        public const GLenum GL_OUT_OF_MEMORY = 0x0505;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_2D = 0x0600;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_3D = 0x0601;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_3D_COLOR = 0x0602;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_3D_COLOR_TEXTURE = 0x0603;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_4D_COLOR_TEXTURE = 0x0604;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PASS_THROUGH_TOKEN = 0x0700;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POINT_TOKEN = 0x0701;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_TOKEN = 0x0702;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_TOKEN = 0x0703;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BITMAP_TOKEN = 0x0704;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DRAW_PIXEL_TOKEN = 0x0705;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COPY_PIXEL_TOKEN = 0x0706;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_RESET_TOKEN = 0x0707;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EXP = 0x0800;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EXP2 = 0x0801;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CW = 0x0900;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CCW = 0x0901;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COEFF = 0x0A00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ORDER = 0x0A01;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DOMAIN = 0x0A02;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_COLOR = 0x0B00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_INDEX = 0x0B01;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_NORMAL = 0x0B02;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_TEXTURE_COORDS = 0x0B03;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_RASTER_COLOR = 0x0B04;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_RASTER_INDEX = 0x0B05;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_RASTER_TEXTURE_COORDS = 0x0B06;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_RASTER_POSITION = 0x0B07;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_RASTER_POSITION_VALID = 0x0B08;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CURRENT_RASTER_DISTANCE = 0x0B09;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POINT_SMOOTH = 0x0B10;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POINT_SIZE = 0x0B11;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POINT_SIZE_RANGE = 0x0B12;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POINT_SIZE_GRANULARITY = 0x0B13;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_SMOOTH = 0x0B20;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_WIDTH = 0x0B21;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_WIDTH_RANGE = 0x0B22;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_WIDTH_GRANULARITY = 0x0B23;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_STIPPLE = 0x0B24;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_STIPPLE_PATTERN = 0x0B25;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_STIPPLE_REPEAT = 0x0B26;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIST_MODE = 0x0B30;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_LIST_NESTING = 0x0B31;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIST_BASE = 0x0B32;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIST_INDEX = 0x0B33;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_MODE = 0x0B40;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_SMOOTH = 0x0B41;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_STIPPLE = 0x0B42;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EDGE_FLAG = 0x0B43;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CULL_FACE = 0x0B44;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CULL_FACE_MODE = 0x0B45;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FRONT_FACE = 0x0B46;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHTING = 0x0B50;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT_MODEL_LOCAL_VIEWER = 0x0B51;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT_MODEL_TWO_SIDE = 0x0B52;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT_MODEL_AMBIENT = 0x0B53;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SHADE_MODEL = 0x0B54;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_MATERIAL_FACE = 0x0B55;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_MATERIAL_PARAMETER = 0x0B56;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_MATERIAL = 0x0B57;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FOG = 0x0B60;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FOG_INDEX = 0x0B61;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FOG_DENSITY = 0x0B62;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FOG_START = 0x0B63;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FOG_END = 0x0B64;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FOG_MODE = 0x0B65;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FOG_COLOR = 0x0B66;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DEPTH_RANGE = 0x0B70;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DEPTH_TEST = 0x0B71;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DEPTH_WRITEMASK = 0x0B72;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DEPTH_CLEAR_VALUE = 0x0B73;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DEPTH_FUNC = 0x0B74;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ACCUM_CLEAR_VALUE = 0x0B80;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_TEST = 0x0B90;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_CLEAR_VALUE = 0x0B91;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_FUNC = 0x0B92;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_VALUE_MASK = 0x0B93;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_FAIL = 0x0B94;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_PASS_DEPTH_PASS = 0x0B96;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_REF = 0x0B97;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_WRITEMASK = 0x0B98;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MATRIX_MODE = 0x0BA0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NORMALIZE = 0x0BA1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VIEWPORT = 0x0BA2;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MODELVIEW_STACK_DEPTH = 0x0BA3;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PROJECTION_STACK_DEPTH = 0x0BA4;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_STACK_DEPTH = 0x0BA5;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MODELVIEW_MATRIX = 0x0BA6;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PROJECTION_MATRIX = 0x0BA7;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_MATRIX = 0x0BA8;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ATTRIB_STACK_DEPTH = 0x0BB0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLIENT_ATTRIB_STACK_DEPTH = 0x0BB1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA_TEST = 0x0BC0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA_TEST_FUNC = 0x0BC1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA_TEST_REF = 0x0BC2;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DITHER = 0x0BD0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BLEND_DST = 0x0BE0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BLEND_SRC = 0x0BE1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BLEND = 0x0BE2;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LOGIC_OP_MODE = 0x0BF0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_LOGIC_OP = 0x0BF1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_LOGIC_OP = 0x0BF2;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AUX_BUFFERS = 0x0C00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DRAW_BUFFER = 0x0C01;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_READ_BUFFER = 0x0C02;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SCISSOR_BOX = 0x0C10;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SCISSOR_TEST = 0x0C11;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_CLEAR_VALUE = 0x0C20;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_WRITEMASK = 0x0C21;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_CLEAR_VALUE = 0x0C22;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_WRITEMASK = 0x0C23;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_MODE = 0x0C30;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGBA_MODE = 0x0C31;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DOUBLEBUFFER = 0x0C32;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STEREO = 0x0C33;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RENDER_MODE = 0x0C40;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PERSPECTIVE_CORRECTION_HINT = 0x0C50;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POINT_SMOOTH_HINT = 0x0C51;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE_SMOOTH_HINT = 0x0C52;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_SMOOTH_HINT = 0x0C53;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FOG_HINT = 0x0C54;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_GEN_S = 0x0C60;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_GEN_T = 0x0C61;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_GEN_R = 0x0C62;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_GEN_Q = 0x0C63;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_I_TO_I = 0x0C70;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_S_TO_S = 0x0C71;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_I_TO_R = 0x0C72;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_I_TO_G = 0x0C73;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_I_TO_B = 0x0C74;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_I_TO_A = 0x0C75;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_R_TO_R = 0x0C76;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_G_TO_G = 0x0C77;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_B_TO_B = 0x0C78;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_A_TO_A = 0x0C79;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_I_TO_I_SIZE = 0x0CB0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_S_TO_S_SIZE = 0x0CB1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_I_TO_R_SIZE = 0x0CB2;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_I_TO_G_SIZE = 0x0CB3;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_I_TO_B_SIZE = 0x0CB4;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_I_TO_A_SIZE = 0x0CB5;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_R_TO_R_SIZE = 0x0CB6;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_G_TO_G_SIZE = 0x0CB7;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_B_TO_B_SIZE = 0x0CB8;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PIXEL_MAP_A_TO_A_SIZE = 0x0CB9;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_UNPACK_SWAP_BYTES = 0x0CF0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_UNPACK_LSB_FIRST = 0x0CF1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_UNPACK_ROW_LENGTH = 0x0CF2;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_UNPACK_SKIP_ROWS = 0x0CF3;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_UNPACK_SKIP_PIXELS = 0x0CF4;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_UNPACK_ALIGNMENT = 0x0CF5;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PACK_SWAP_BYTES = 0x0D00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PACK_LSB_FIRST = 0x0D01;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PACK_ROW_LENGTH = 0x0D02;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PACK_SKIP_ROWS = 0x0D03;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PACK_SKIP_PIXELS = 0x0D04;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PACK_ALIGNMENT = 0x0D05;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP_COLOR = 0x0D10;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP_STENCIL = 0x0D11;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_SHIFT = 0x0D12;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_OFFSET = 0x0D13;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RED_SCALE = 0x0D14;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RED_BIAS = 0x0D15;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ZOOM_X = 0x0D16;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ZOOM_Y = 0x0D17;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_GREEN_SCALE = 0x0D18;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_GREEN_BIAS = 0x0D19;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BLUE_SCALE = 0x0D1A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BLUE_BIAS = 0x0D1B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA_SCALE = 0x0D1C;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA_BIAS = 0x0D1D;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DEPTH_SCALE = 0x0D1E;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DEPTH_BIAS = 0x0D1F;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_EVAL_ORDER = 0x0D30;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_LIGHTS = 0x0D31;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_CLIP_PLANES = 0x0D32;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_TEXTURE_SIZE = 0x0D33;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_PIXEL_MAP_TABLE = 0x0D34;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_ATTRIB_STACK_DEPTH = 0x0D35;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_MODELVIEW_STACK_DEPTH = 0x0D36;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_NAME_STACK_DEPTH = 0x0D37;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_PROJECTION_STACK_DEPTH = 0x0D38;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_TEXTURE_STACK_DEPTH = 0x0D39;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_VIEWPORT_DIMS = 0x0D3A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_CLIENT_ATTRIB_STACK_DEPTH = 0x0D3B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SUBPIXEL_BITS = 0x0D50;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_BITS = 0x0D51;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RED_BITS = 0x0D52;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_GREEN_BITS = 0x0D53;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BLUE_BITS = 0x0D54;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA_BITS = 0x0D55;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DEPTH_BITS = 0x0D56;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_BITS = 0x0D57;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ACCUM_RED_BITS = 0x0D58;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ACCUM_GREEN_BITS = 0x0D59;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ACCUM_BLUE_BITS = 0x0D5A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ACCUM_ALPHA_BITS = 0x0D5B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NAME_STACK_DEPTH = 0x0D70;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AUTO_NORMAL = 0x0D80;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_COLOR_4 = 0x0D90;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_INDEX = 0x0D91;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_NORMAL = 0x0D92;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_TEXTURE_COORD_1 = 0x0D93;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_TEXTURE_COORD_2 = 0x0D94;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_TEXTURE_COORD_3 = 0x0D95;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_TEXTURE_COORD_4 = 0x0D96;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_VERTEX_3 = 0x0D97;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_VERTEX_4 = 0x0D98;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_COLOR_4 = 0x0DB0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_INDEX = 0x0DB1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_NORMAL = 0x0DB2;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_TEXTURE_COORD_1 = 0x0DB3;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_TEXTURE_COORD_2 = 0x0DB4;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_TEXTURE_COORD_3 = 0x0DB5;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_TEXTURE_COORD_4 = 0x0DB6;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_VERTEX_3 = 0x0DB7;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_VERTEX_4 = 0x0DB8;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_GRID_DOMAIN = 0x0DD0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP1_GRID_SEGMENTS = 0x0DD1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_GRID_DOMAIN = 0x0DD2;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAP2_GRID_SEGMENTS = 0x0DD3;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_1D = 0x0DE0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_2D = 0x0DE1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FEEDBACK_BUFFER_POINTER = 0x0DF0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FEEDBACK_BUFFER_SIZE = 0x0DF1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FEEDBACK_BUFFER_TYPE = 0x0DF2;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SELECTION_BUFFER_POINTER = 0x0DF3;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SELECTION_BUFFER_SIZE = 0x0DF4;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_WIDTH = 0x1000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_HEIGHT = 0x1001;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_INTERNAL_FORMAT = 0x1003;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_BORDER_COLOR = 0x1004;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_BORDER = 0x1005;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DONT_CARE = 0x1100;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FASTEST = 0x1101;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NICEST = 0x1102;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT0 = 0x4000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT1 = 0x4001;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT2 = 0x4002;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT3 = 0x4003;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT4 = 0x4004;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT5 = 0x4005;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT6 = 0x4006;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LIGHT7 = 0x4007;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AMBIENT = 0x1200;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DIFFUSE = 0x1201;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SPECULAR = 0x1202;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POSITION = 0x1203;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SPOT_DIRECTION = 0x1204;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SPOT_EXPONENT = 0x1205;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SPOT_CUTOFF = 0x1206;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CONSTANT_ATTENUATION = 0x1207;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINEAR_ATTENUATION = 0x1208;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_QUADRATIC_ATTENUATION = 0x1209;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COMPILE = 0x1300;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COMPILE_AND_EXECUTE = 0x1301;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLEAR = 0x1500;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AND = 0x1501;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AND_REVERSE = 0x1502;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COPY = 0x1503;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AND_INVERTED = 0x1504;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NOOP = 0x1505;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_XOR = 0x1506;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_OR = 0x1507;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NOR = 0x1508;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EQUIV = 0x1509;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INVERT = 0x150A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_OR_REVERSE = 0x150B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COPY_INVERTED = 0x150C;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_OR_INVERTED = 0x150D;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NAND = 0x150E;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SET = 0x150F;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EMISSION = 0x1600;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SHININESS = 0x1601;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_AMBIENT_AND_DIFFUSE = 0x1602;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_INDEXES = 0x1603;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MODELVIEW = 0x1700;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PROJECTION = 0x1701;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE = 0x1702;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR = 0x1800;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DEPTH = 0x1801;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL = 0x1802;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_INDEX = 0x1900;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_STENCIL_INDEX = 0x1901;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DEPTH_COMPONENT = 0x1902;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RED = 0x1903;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_GREEN = 0x1904;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BLUE = 0x1905;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA = 0x1906;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGB = 0x1907;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGBA = 0x1908;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE = 0x1909;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE_ALPHA = 0x190A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BITMAP = 0x1A00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POINT = 0x1B00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINE = 0x1B01;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FILL = 0x1B02;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RENDER = 0x1C00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FEEDBACK = 0x1C01;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SELECT = 0x1C02;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FLAT = 0x1D00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SMOOTH = 0x1D01;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_KEEP = 0x1E00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_REPLACE = 0x1E01;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INCR = 0x1E02;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DECR = 0x1E03;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VENDOR = 0x1F00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RENDERER = 0x1F01;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERSION = 0x1F02;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EXTENSIONS = 0x1F03;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_S = 0x2000;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_T = 0x2001;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_R = 0x2002;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_Q = 0x2003;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MODULATE = 0x2100;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DECAL = 0x2101;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_ENV_MODE = 0x2200;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_ENV_COLOR = 0x2201;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_ENV = 0x2300;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EYE_LINEAR = 0x2400;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_OBJECT_LINEAR = 0x2401;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_SPHERE_MAP = 0x2402;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_GEN_MODE = 0x2500;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_OBJECT_PLANE = 0x2501;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EYE_PLANE = 0x2502;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NEAREST = 0x2600;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINEAR = 0x2601;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NEAREST_MIPMAP_NEAREST = 0x2700;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINEAR_MIPMAP_NEAREST = 0x2701;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NEAREST_MIPMAP_LINEAR = 0x2702;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LINEAR_MIPMAP_LINEAR = 0x2703;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_MAG_FILTER = 0x2800;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_MIN_FILTER = 0x2801;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_WRAP_S = 0x2802;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_WRAP_T = 0x2803;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLAMP = 0x2900;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_REPEAT = 0x2901;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLIENT_PIXEL_STORE_BIT = 0x00000001;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLIENT_VERTEX_ARRAY_BIT = 0x00000002;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_CLIENT_ALL_ATTRIB_BITS = -1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_OFFSET_FACTOR = 0x8038;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_OFFSET_UNITS = 0x2A00;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_OFFSET_POINT = 0x2A01;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_OFFSET_LINE = 0x2A02;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_POLYGON_OFFSET_FILL = 0x8037;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA4 = 0x803B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA8 = 0x803C;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA12 = 0x803D;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_ALPHA16 = 0x803E;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE4 = 0x803F;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE8 = 0x8040;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE12 = 0x8041;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE16 = 0x8042;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE4_ALPHA4 = 0x8043;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE6_ALPHA2 = 0x8044;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE8_ALPHA8 = 0x8045;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE12_ALPHA4 = 0x8046;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE12_ALPHA12 = 0x8047;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LUMINANCE16_ALPHA16 = 0x8048;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INTENSITY = 0x8049;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INTENSITY4 = 0x804A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INTENSITY8 = 0x804B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INTENSITY12 = 0x804C;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INTENSITY16 = 0x804D;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_R3_G3_B2 = 0x2A10;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGB4 = 0x804F;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGB5 = 0x8050;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGB8 = 0x8051;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGB10 = 0x8052;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGB12 = 0x8053;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGB16 = 0x8054;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGBA2 = 0x8055;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGBA4 = 0x8056;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGB5_A1 = 0x8057;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGBA8 = 0x8058;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGB10_A2 = 0x8059;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGBA12 = 0x805A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_RGBA16 = 0x805B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_RED_SIZE = 0x805C;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_GREEN_SIZE = 0x805D;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_BLUE_SIZE = 0x805E;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_ALPHA_SIZE = 0x805F;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_LUMINANCE_SIZE = 0x8060;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_INTENSITY_SIZE = 0x8061;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PROXY_TEXTURE_1D = 0x8063;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PROXY_TEXTURE_2D = 0x8064;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_PRIORITY = 0x8066;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_RESIDENT = 0x8067;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_BINDING_1D = 0x8068;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_BINDING_2D = 0x8069;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY = 0x8074;


        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NORMAL_ARRAY = 0x8075;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY = 0x8076;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_ARRAY = 0x8077;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY = 0x8078;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EDGE_FLAG_ARRAY = 0x8079;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY_SIZE = 0x807A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY_TYPE = 0x807B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY_STRIDE = 0x807C;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NORMAL_ARRAY_TYPE = 0x807E;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NORMAL_ARRAY_STRIDE = 0x807F;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY_SIZE = 0x8081;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY_TYPE = 0x8082;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY_STRIDE = 0x8083;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_ARRAY_TYPE = 0x8085;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_ARRAY_STRIDE = 0x8086;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY_SIZE = 0x8088;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY_TYPE = 0x8089;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY_STRIDE = 0x808A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EDGE_FLAG_ARRAY_STRIDE = 0x808C;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY_POINTER = 0x808E;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NORMAL_ARRAY_POINTER = 0x808F;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY_POINTER = 0x8090;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_ARRAY_POINTER = 0x8091;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY_POINTER = 0x8092;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EDGE_FLAG_ARRAY_POINTER = 0x8093;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_V2F = 0x2A20;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_V3F = 0x2A21;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_C4UB_V2F = 0x2A22;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_C4UB_V3F = 0x2A23;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_C3F_V3F = 0x2A24;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_N3F_V3F = 0x2A25;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_C4F_N3F_V3F = 0x2A26;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_T2F_V3F = 0x2A27;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_T4F_V4F = 0x2A28;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_T2F_C4UB_V3F = 0x2A29;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_T2F_C3F_V3F = 0x2A2A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_T2F_N3F_V3F = 0x2A2B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_T2F_C4F_N3F_V3F = 0x2A2C;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_T4F_C4F_N3F_V4F = 0x2A2D;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EXT_vertex_array = 1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EXT_bgra = 1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EXT_paletted_texture = 1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_WIN_swap_hint = 1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_WIN_draw_range_elements = 1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY_EXT = 0x8074;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NORMAL_ARRAY_EXT = 0x8075;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY_EXT = 0x8076;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_ARRAY_EXT = 0x8077;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY_EXT = 0x8078;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EDGE_FLAG_ARRAY_EXT = 0x8079;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY_SIZE_EXT = 0x807A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY_TYPE_EXT = 0x807B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY_STRIDE_EXT = 0x807C;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY_COUNT_EXT = 0x807D;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NORMAL_ARRAY_TYPE_EXT = 0x807E;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NORMAL_ARRAY_STRIDE_EXT = 0x807F;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NORMAL_ARRAY_COUNT_EXT = 0x8080;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY_SIZE_EXT = 0x8081;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY_TYPE_EXT = 0x8082;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY_STRIDE_EXT = 0x8083;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY_COUNT_EXT = 0x8084;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_ARRAY_TYPE_EXT = 0x8085;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_ARRAY_STRIDE_EXT = 0x8086;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_ARRAY_COUNT_EXT = 0x8087;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY_SIZE_EXT = 0x8088;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY_TYPE_EXT = 0x8089;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY_STRIDE_EXT = 0x808A;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY_COUNT_EXT = 0x808B;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EDGE_FLAG_ARRAY_STRIDE_EXT = 0x808C;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EDGE_FLAG_ARRAY_COUNT_EXT = 0x808D;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_VERTEX_ARRAY_POINTER_EXT = 0x808E;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_NORMAL_ARRAY_POINTER_EXT = 0x808F;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_ARRAY_POINTER_EXT = 0x8090;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_INDEX_ARRAY_POINTER_EXT = 0x8091;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COORD_ARRAY_POINTER_EXT = 0x8092;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_EDGE_FLAG_ARRAY_POINTER_EXT = 0x8093;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_DOUBLE_EXT = GL_DOUBLE;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BGR_EXT = 0x80E0;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_BGRA_EXT = 0x80E1;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_TABLE_FORMAT_EXT = 0x80D8;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_TABLE_WIDTH_EXT = 0x80D9;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_TABLE_RED_SIZE_EXT = 0x80DA;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_TABLE_GREEN_SIZE_EXT = 0x80DB;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_TABLE_BLUE_SIZE_EXT = 0x80DC;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_TABLE_ALPHA_SIZE_EXT = 0x80DD;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_TABLE_LUMINANCE_SIZE_EXT = 0x80DE;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_TABLE_INTENSITY_SIZE_EXT = 0x80DF;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_INDEX1_EXT = 0x80E2;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_INDEX2_EXT = 0x80E3;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_INDEX4_EXT = 0x80E4;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_INDEX8_EXT = 0x80E5;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_INDEX12_EXT = 0x80E6;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_COLOR_INDEX16_EXT = 0x80E7;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_ELEMENTS_VERTICES_WIN = 0x80E8;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_MAX_ELEMENTS_INDICES_WIN = 0x80E9;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PHONG_WIN = 0x80EA;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_PHONG_HINT_WIN = 0x80EB;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_FOG_SPECULAR_TEXTURE_WIN = 0x80EC;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_LOGIC_OP = GL_INDEX_LOGIC_OP;

        /// <summary>
        /// 
        /// </summary>
        public const GLenum GL_TEXTURE_COMPONENTS = GL_TEXTURE_INTERNAL_FORMAT;
    }
}
