using System.Globalization;
using System.Runtime.CompilerServices;

namespace Apeiron.Recording.Stm32;

/// <summary>
/// Представляет синхромаркер датчика на базе микроконтроллеров STM32.
/// </summary>
public struct Stm32Synchromarker :
    IEquatable<Stm32Synchromarker>,
    IComparable<Stm32Synchromarker>,
    IComparable
{
    /// <summary>
    /// Поле для хранения постоянной, определяющей количество тиков, с которого начинается эпоха UNIX.
    /// </summary>
    private const long _UnixEpochTicks = TimeSpan.TicksPerDay * 719162; // 621,355,968,000,000,000

    /// <summary>
    /// Инициализирует новый экземпляр структуры.
    /// </summary>
    /// <param name="value">
    /// Значение синхромаркера.
    /// </param>
    public Stm32Synchromarker(long value)
    {
        //  Установка значения синхромаркера.
        Value = value;
    }

    /// <summary>
    /// Возвращает или задаёт значение синхромаркера.
    /// </summary>
    public long Value { get; set; }

    /// <summary>
    /// Возвращает количество тактов.
    /// </summary>
    /// <remarks>
    /// Наименьшая единица времени - это такт, который равен 100 наносекунд.
    /// В миллисекунде 10000 тактов.
    /// </remarks>
    public long Ticks => Value * TimeSpan.TicksPerMillisecond;

    /// <summary>
    /// Выполняет операцию проверки на равенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator ==(Stm32Synchromarker left, Stm32Synchromarker right)
    {
        //  Выполнение операции.
        return CompareCore(left, right) == 0;
    }

    /// <summary>
    /// Выполняет операцию проверки на неравенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator !=(Stm32Synchromarker left, Stm32Synchromarker right)
    {
        //  Выполнение операции.
        return CompareCore(left, right) != 0;
    }

    /// <summary>
    /// Выполняет операцию проверки на возрастание.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator <(Stm32Synchromarker left, Stm32Synchromarker right)
    {
        //  Выполнение операции.
        return CompareCore(left, right) < 0;
    }

    /// <summary>
    /// Выполняет операцию проверки на убывание.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator >(Stm32Synchromarker left, Stm32Synchromarker right)
    {
        //  Выполнение операции.
        return CompareCore(left, right) > 0;
    }

    /// <summary>
    /// Выполняет операцию проверки на неубывание.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator <=(Stm32Synchromarker left, Stm32Synchromarker right)
    {
        //  Выполнение операции.
        return CompareCore(left, right) <= 0;
    }

    /// <summary>
    /// Выполняет операцию проверки на невозрастание.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator >=(Stm32Synchromarker left, Stm32Synchromarker right)
    {
        //  Выполнение операции.
        return CompareCore(left, right) >= 0;
    }

    /// <summary>
    /// Возвращает интервал времени, соответствующий синхромаркеру.
    /// </summary>
    /// <returns>
    /// Интервал времени, соответствующий синхромаркеру.
    /// </returns>
    public TimeSpan ToTimeSpan()
    {
        //  Расчёт тактов и возврат интервала.
        return TimeSpan.FromTicks(Ticks);
    }

    /// <summary>
    /// Возвращает время в UNIX-формате.
    /// </summary>
    /// <returns>
    /// Время в UNIX-формате.
    /// </returns>
    public DateTime ToUnixTime()
    {
        //  Расчёт тактов и возврат времени.
        return new(_UnixEpochTicks + Ticks);
    }

    /// <summary>
    /// Указывает, равен ли этот экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">
    /// Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если <paramref name="obj"/>
    /// и данный экземпляр относятся к одному типу и представляют одинаковые значения;
    /// в противном случае - значение <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        //  Проверка типа.
        if (obj is Stm32Synchromarker instance)
        {
            //  Сравнение.
            return CompareCore(this, instance) == 0;
        }

        //  Типы не совпадают.
        return false;
    }

    /// <summary>
    /// Указывает, равен ли текущий объект другому объекту того же типа.
    /// </summary>
    /// <param name="other">
    /// Объект, который требуется сравнить с данным объектом.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если текущий объект эквивалентен параметру <paramref name="other"/>,
    /// в противном случае - <c>false</c>.
    /// </returns>
    public bool Equals(Stm32Synchromarker other)
    {
        //  Сравнение.
        return CompareCore(this, other) == 0;
    }

    /// <summary>
    /// Возвращает хэш-код данного экземпляра.
    /// </summary>
    /// <returns>
    /// 32-разрядное целое число со знаком,
    /// которое является хэш-кодом для этого экземпляра.
    /// </returns>
    public override int GetHashCode()
    {
        //  Возврат хэш-кода.
        return GetHashCodeCore(this);
    }

    /// <summary>
    /// Возвращает текстовое представление текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Текстовое представление текущего экземпляра.
    /// </returns>
    public override string ToString()
    {
        //  Возврат текстового представления.
        return ToStringCore(this);
    }

    /// <summary>
    /// Сравнивает текущий экземпляр с другим объектом того же типа
    /// и возвращает целое число, которое показывает,
    /// расположен ли текущий экземпляр перед, после или на той же позиции
    /// в порядке сортировки, что и другой объект.
    /// </summary>
    /// <param name="other">
    /// Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///     <para>
    ///     Значение, указывающее, каков относительный порядок сравниваемых объектов.
    ///     Возвращаемые значения представляют следующие результаты сравнения.
    ///     </para>
    ///     <para>
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Значение</term>
    ///             <description>Описание</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Меньше нуля</term>
    ///             <description>
    ///                 Данный экземпляр предшествует параметру 
    ///                 <paramref name="other"/> в порядке сортировки.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Нуль</term>
    ///             <description>
    ///                 Данный экземпляр занимает ту же позицию в порядке сортировки,
    ///                 что и параметр <paramref name="other"/>.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Больше нуля</term>
    ///             <description>
    ///                 Данный экземпляр следует за параметром
    ///                 <paramref name="other"/> в порядке сортировки.
    ///             </description>
    ///         </item>
    ///     </list>
    /// </para>
    /// </returns>
    public int CompareTo(Stm32Synchromarker other)
    {
        //  Сравнение.
        return CompareCore(this, other);
    }

    /// <summary>
    /// Сравнивает текущий экземпляр с другим объектом того же типа
    /// и возвращает целое число, которое показывает,
    /// расположен ли текущий экземпляр перед, после или на той же позиции
    /// в порядке сортировки, что и другой объект.
    /// </summary>
    /// <param name="obj">
    /// Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///     <para>
    ///     Значение, указывающее, каков относительный порядок сравниваемых объектов.
    ///     Возвращаемые значения представляют следующие результаты сравнения.
    ///     </para>
    ///     <para>
    ///     <list type="table">
    ///         <listheader>
    ///             <term>Значение</term>
    ///             <description>Описание</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Меньше нуля</term>
    ///             <description>
    ///                 Данный экземпляр предшествует параметру 
    ///                 <paramref name="obj"/> в порядке сортировки.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Нуль</term>
    ///             <description>
    ///                 Данный экземпляр занимает ту же позицию в порядке сортировки,
    ///                 что и параметр <paramref name="obj"/>.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Больше нуля</term>
    ///             <description>
    ///                 Данный экземпляр следует за параметром
    ///                 <paramref name="obj"/> в порядке сортировки.
    ///             </description>
    ///         </item>
    ///     </list>
    /// </para>
    /// </returns>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="obj"/> передан объект недопустимого типа.
    /// </exception>
    int IComparable.CompareTo(object? obj)
    {
        //  Проверка типа объекта.
        var other = IsType<Stm32Synchromarker>(obj, nameof(obj));

        //  Сравнение значений.
        return CompareCore(this, other);
    }

    /// <summary>
    /// Выполняет операцию сравнения.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CompareCore(Stm32Synchromarker left, Stm32Synchromarker right)
    {
        //  Проверка на равенство.
        if (left.Value == right.Value)
        {
            //  Операнды равны.
            return 0;
        }

        //  Проверка на возрастание.
        if (left.Value < right.Value)
        {
            //  Левый операнд меньше правого.
            return -1;
        }

        //  Левый операнд больше правого.
        return 1;
    }

    /// <summary>
    /// Возвращает хэш-код.
    /// </summary>
    /// <param name="instance">
    /// Значение, для которого необходимо вычислить хэш-код.
    /// </param>
    /// <returns>
    /// 32-разрядное целое число со знаком,
    /// которое является хэш-кодом для этого экземпляра.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int GetHashCodeCore(Stm32Synchromarker instance)
    {
        //  Возврат хэш-кода значения.
        return instance.Value.GetHashCode();
    }

    /// <summary>
    /// Возвращает текстовое представление.
    /// </summary>
    /// <param name="instance">
    /// Значение, которое необходимо представить в текстовом виде.
    /// </param>
    /// <returns>
    /// Текстовое представление.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static string ToStringCore(Stm32Synchromarker instance)
    {
        //  Возврат текстового представления значения.
        return instance.Value.ToString(CultureInfo.InvariantCulture);
    }
}
