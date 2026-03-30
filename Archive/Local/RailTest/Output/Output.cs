using System.Drawing;

namespace RailTest
{
    /// <summary>
    /// Представляет базовый класс для всех объектов, реализующих вывод текстовой информации.
    /// </summary>
    public abstract partial class Output : Ancestor
    {
        /// <summary>
        /// Возвращает строку с символом табуляции.
        /// </summary>
        public static string Tab { get; } = "\t";

        /// <summary>
        /// Сбрасывает уровень табуляции.
        /// </summary>
        public abstract void TabLevelReset();

        /// <summary>
        /// Поднимает уровень табуляции.
        /// </summary>
        public abstract void TabLevelUp();

        /// <summary>
        /// Опускает уровень табуляции.
        /// </summary>
        public abstract void TabLevelDown();

        /// <summary>
        /// Выполняет очистку средства вывода текстовой информации.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Производит выполнение всех команд.
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// Осуществляет переход на новую строку.
        /// </summary>
        public abstract void WriteLine();

        /// <summary>
        /// Выводит текстовую строку.
        /// </summary>
        /// <param name="value">
        /// Текстовая строка.
        /// </param>
        public abstract void Write(string value);

        /// <summary>
        /// Заменяет элемент формата в указанной строке строковым представлением
        /// соответствующего объекта в указанном массиве и выводит в средство вывода текстовой информации.
        /// </summary>
        /// <param name="format">
        /// Строка составного форматирования.
        /// </param>
        /// <param name="args">
        /// Массив объектов, содержащий нуль или более форматируемых объектов.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
        public void Write(string format, params object[] args)
        {
            Write(string.Format(format, args));
        }

        /// <summary>
        /// Выводит текстовую строку.
        /// </summary>
        /// <param name="color">
        /// Цвет.
        /// </param>
        /// <param name="value">
        /// Текстовая строка.
        /// </param>
        public abstract void Write(Color color, string value);

        /// <summary>
        /// Заменяет элемент формата в указанной строке строковым представлением
        /// соответствующего объекта в указанном массиве и выводит в средство вывода текстовой информации.
        /// </summary>
        /// <param name="color">
        /// Цвет.
        /// </param>
        /// <param name="format">
        /// Строка составного форматирования.
        /// </param>
        /// <param name="args">
        /// Массив объектов, содержащий нуль или более форматируемых объектов.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
        public void Write(Color color, string format, params object[] args)
        {
            Write(color, string.Format(format, args));
        }

        /// <summary>
        /// Выводит текстовую строку и осуществляет перход на новую строку.
        /// </summary>
        /// <param name="value">
        /// Текстовая срока.
        /// </param>
        public void WriteLine(string value)
        {
            lock (SyncRoot)
            {
                Write(value);
                WriteLine();
            }
        }

        /// <summary>
        /// Выводит объект и осуществляет перход на новую строку.
        /// </summary>
        /// <param name="value">
        /// Объект.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Проверить аргументы или открытые методы", MessageId = "0")]
        public void WriteLine(object value)
        {
            WriteLine(value.ToString());
        }


        /// <summary>
        /// Заменяет элемент формата в указанной строке строковым представлением
        /// соответствующего объекта в указанном массиве и выводит в средство вывода текстовой информации.
        /// </summary>
        /// <param name="format">
        /// Строка составного форматирования.
        /// </param>
        /// <param name="args">
        /// Массив объектов, содержащий нуль или более форматируемых объектов.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        /// <summary>
        /// Выводит текстовую строку и осуществляет переход на новую строку.
        /// </summary>
        /// <param name="color">
        /// Цвет.
        /// </param>
        /// <param name="value">
        /// Текстовая строка.
        /// </param>
        public void WriteLine(Color color, string value)
        {
            lock (SyncRoot)
            {
                Write(color, value);
                WriteLine();
            }
        }

        /// <summary>
        /// Заменяет элемент формата в указанной строке строковым представлением
        /// соответствующего объекта в указанном массиве и выводит в средство вывода текстовой информации.
        /// </summary>
        /// <param name="color">
        /// Цвет.
        /// </param>
        /// <param name="format">
        /// Строка составного форматирования.
        /// </param>
        /// <param name="args">
        /// Массив объектов, содержащий нуль или более форматируемых объектов.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
        public void WriteLine(Color color, string format, params object[] args)
        {
            WriteLine(color, string.Format(format, args));
        }

        /// <summary>
        /// Создаёт дочернее средство вывода текстовой информации.
        /// </summary>
        /// <returns>
        /// Дочернее средство вывода текстовой информации.
        /// </returns>
        public Output CreateSubOutput()
        {
            return new SubOutput(this);
        }
    }
}