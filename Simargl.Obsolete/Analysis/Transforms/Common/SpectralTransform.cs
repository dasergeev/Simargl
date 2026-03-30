using Simargl.Designing;

namespace Simargl.Analysis.Transforms;

/// <summary>
/// Представляет спектральное преобразование.
/// </summary>
public abstract class SpectralTransform :
    Transform
{
    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Объект для преобразования.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="source"/> передана пустая ссылка.
    /// </exception>
    public void Invoke(Spectrum source)
    {
        //  Проверка ссылки на объект для преобразования.
        IsNotNull(source, nameof(source));

        //  Выполнение преобразования.
        InvokeCore(source);
    }

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Объект для преобразования.
    /// </param>
    internal protected abstract void InvokeCore([NoVerify] Spectrum source);

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Исходный объект.
    /// </param>
    /// <param name="target">
    /// Преобразованный объект.
    /// </param>
    internal protected override sealed void InvokeCore([NoVerify] Signal source, [NoVerify] Signal target)
    {
        //  Получение спектра.
        Spectrum spectrum = new(source);

        //  Выполнение преобразования.
        InvokeCore(spectrum);

        //  Восстановление сигнала.
        spectrum.Restore(target);
    }
}
