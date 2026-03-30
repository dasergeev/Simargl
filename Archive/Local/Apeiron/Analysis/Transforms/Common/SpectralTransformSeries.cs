namespace Apeiron.Analysis.Transforms;

/// <summary>
/// Представляет серию спектральных преобразований.
/// </summary>
public sealed class SpectralTransformSeries :
    SpectralTransform
{
    /// <summary>
    /// Поле для хранения массива перобразований.
    /// </summary>
    private readonly SpectralTransform[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="transforms">
    /// Коллекция преобразований.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="transforms"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В одном из элементов коллекции параметре <paramref name="transforms"/> передана пустая ссылка.
    /// </exception>
    public SpectralTransformSeries(IEnumerable<SpectralTransform> transforms)
    {
        //  Проверка ссылки на коллекцию.
        IsNotNull(transforms, nameof(transforms));

        //  Создание списка преобразований.
        List<SpectralTransform> items = new();

        //  Перебор коллекции.
        foreach (SpectralTransform transform in transforms)
        {
            //  Проверка ссылки на преобразование.
            IsNotNull(transform, nameof(transform));

            //  Проверка серии.
            if (transform is SpectralTransformSeries series)
            {
                //  Добавление вложенных элементов.
                items.AddRange(series._Items);
            }
            else
            {
                //  Добавление преобразования.
                items.Add(transform);
            }
        }

        //  Получение массива преобразований.
        _Items = items.ToArray();
    }

    /// <summary>
    /// Возвращает количество преобразований в последовательности.
    /// </summary>
    public int Count => _Items.Length;

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Объект для преобразования.
    /// </param>
    internal protected override void InvokeCore([ParameterNoChecks] Spectrum source)
    {
        //  Перебор преобразований.
        for (int i = 0; i < _Items.Length; i++)
        {
            //  Выполенение преобразования.
            _Items[i].InvokeCore(source);
        }
    }
}
