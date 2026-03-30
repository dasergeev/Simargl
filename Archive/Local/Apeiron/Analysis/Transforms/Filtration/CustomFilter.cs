namespace Apeiron.Analysis.Transforms;

/// <summary>
/// Пользовательский фильтр.
/// </summary>
public sealed class CustomFilter :
    SpectralTransform
{
    /// <summary>
    /// Поле для хранения метода, выполняющего преобразование спектра.
    /// </summary>
    private readonly SpectrumReformer _Reformer;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="reformer">
    /// Метод, выполняющий преобразование спектра.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="reformer"/> передана пустая ссылка.
    /// </exception>
    public CustomFilter(SpectrumReformer reformer)
    {
        //  Установка метода, выполняющего преобразование спектра.
        _Reformer = IsNotNull(reformer, nameof(reformer));
    }

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Объект для преобразования.
    /// </param>
    protected internal override void InvokeCore([ParameterNoChecks] Spectrum source)
    {
        //  Преобразование спектра.
        source.Reform(_Reformer);
    }
}
