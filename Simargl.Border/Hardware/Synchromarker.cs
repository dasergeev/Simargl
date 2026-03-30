using System.Globalization;
using System.Runtime.CompilerServices;

namespace Simargl.Border.Hardware;

/// <summary>
/// Представляет синхромаркер.
/// </summary>
public readonly struct Synchromarker :
    IEquatable<Synchromarker>,
    IComparable<Synchromarker>,
    IComparable
{
    /// <summary>
    /// Постоянная, поределяющая модуль.
    /// </summary>
    public const long Module = 1L << 32;

    /// <summary>
    /// Поле для хранения значения.
    /// </summary>
    private readonly uint _Value;

    /// <summary>
    /// Инициализирует новый экземпляр структуры.
    /// </summary>
    /// <param name="value">
    /// Значение синхромаркера.
    /// </param>
    public Synchromarker(long value)
    {
        //  Установка значения синхромаркера.
        _Value = unchecked((uint)value);
    }

    /// <summary>
    /// Возвращает или задаёт значение синхромаркера.
    /// </summary>
    public long Value => _Value;

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
    public static bool operator ==(Synchromarker left, Synchromarker right)
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
    public static bool operator !=(Synchromarker left, Synchromarker right)
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
    public static bool operator <(Synchromarker left, Synchromarker right)
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
    public static bool operator >(Synchromarker left, Synchromarker right)
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
    public static bool operator <=(Synchromarker left, Synchromarker right)
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
    public static bool operator >=(Synchromarker left, Synchromarker right)
    {
        //  Выполнение операции.
        return CompareCore(left, right) >= 0;
    }

    /// <summary>
    /// Выполняет операцию вычитания.
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
    public static long operator -(Synchromarker left, Synchromarker right)
    {
        //  Основные постоянные.
        const long half = 1L << 31;

        //  Получение значения.
        long value = left.Value - right.Value;

        //  Нормализация.
        if (value >= half) value -= Module;
        if (value < -half) value += Module;

        //  Возврат нормализованного значения.
        return value;
    }

    /// <summary>
    /// Выполняет операцию сложения.
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
    public static Synchromarker operator +(Synchromarker left, int right)
    {
        //  Получение значения.
        long value = left.Value + right;

        //  Нормализация.
        if (value >= Module) value -= Module;
        if (value < 0) value += Module;

        //  Возврат нормализованного значения.
        return new(value);
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
    public override readonly bool Equals(object? obj)
    {
        //  Проверка типа.
        if (obj is Synchromarker instance)
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
    public readonly bool Equals(Synchromarker other)
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
    public override readonly int GetHashCode()
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
    public override readonly string ToString()
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
    public readonly int CompareTo(Synchromarker other)
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
    readonly int IComparable.CompareTo(object? obj)
    {
        ////  Проверка типа объекта.
        //var other = IsType<Stm32Synchromarker>(obj, nameof(obj));

        //  Сравнение значений.
        return CompareCore(this, (Synchromarker)obj!);
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
    private static int CompareCore(Synchromarker left, Synchromarker right)
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
    static int GetHashCodeCore(Synchromarker instance)
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
    static string ToStringCore(Synchromarker instance)
    {
        //  Возврат текстового представления значения.
        return instance.Value.ToString(CultureInfo.InvariantCulture);
    }
}
