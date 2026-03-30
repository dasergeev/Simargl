using Simargl.Designing;

namespace Simargl.Analysis.Transforms;

/// <summary>
/// Представляет преобразование.
/// </summary>
public abstract class Transform
{
    /// <summary>
    /// Возвращает тождественное преобразование.
    /// </summary>
    public static IdentityTransform Identity { get; } = new IdentityTransform();

    /// <summary>
    /// Выполнят операцию композиции.
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    public static Transform operator*(Transform left, Transform right)
    {
        //  Проверка ссылок на преобразования.
        IsNotNull(left, nameof(left));
        IsNotNull(right, nameof(right));

        //  Возврат композиции преобразований.
        return new TransformSeries(right, left);
    }

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Исходный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="source"/> передана пустая ссылка.
    /// </exception>
    public void Invoke(Signal source)
    {
        //  Проверка ссылки на исходный объект.
        IsNotNull(source, nameof(source));

        //  Выполнение преобразования.
        InvokeCore(source, source);
    }

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Исходный объект.
    /// </param>
    /// <returns>
    /// Преобразованный объект.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="source"/> передана пустая ссылка.
    /// </exception>
    public Signal CloneInvoke(Signal source)
    {
        //  Проверка ссылки на исходный объект.
        IsNotNull(source, nameof(source));

        //  Создание копии исходного объекта.
        Signal target = source.Clone();

        //  Выполнение преобразования.
        InvokeCore(source, target);

        //  Возврат результата преобразования.
        return target;
    }

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Исходный объект.
    /// </param>
    /// <param name="target">
    /// Преобразованный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="source"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="target"/> передана пустая ссылка.
    /// </exception>
    public void Invoke(Signal source, Signal target)
    {
        //  Проверка ссылок.
        IsNotNull(source, nameof(source));
        IsNotNull(target, nameof(target));

        //  Выполнение преобразования.
        InvokeCore(source, target);
    }

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Исходный объект.
    /// </param>
    /// <param name="target">
    /// Преобразованный объект.
    /// </param>
    internal protected abstract void InvokeCore([NoVerify] Signal source, [NoVerify] Signal target);
}
