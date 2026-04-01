using System.Collections.Generic;

namespace Simargl.Analysis.Transforms;

/// <summary>
/// Представляет последовательность преобразований.
/// </summary>
public sealed class TransformSeries :
    Transform
{
    /// <summary>
    /// Поле для хранения элементов преобразования.
    /// </summary>
    private readonly List<Transform> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="first">
    /// Первое преобразование.
    /// </param>
    /// <param name="second">
    /// Второе преобразование.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="first"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="second"/> передана пустая ссылка.
    /// </exception>
    public TransformSeries(Transform first, Transform second)
    {
        //  Проверка ссылок на преобразования.
        IsNotNull(first, nameof(first));
        IsNotNull(second, nameof(second));

        //  Создание хранилища элементов преобразования.
        _Items = new();

        //  Добавление первого преобразования.
        if (first is TransformSeries firstSeries)
        {
            //  Добавление элементов первого преобразования.
            _Items.AddRange(firstSeries._Items);
        }
        else
        {
            //  Добавление первого преобразования.
            _Items.Add(first);
        }

        //  Добавление второго преобразования.
        if (second is TransformSeries secondSeries)
        {
            //  Добавление элементов второго преобразования.
            _Items.AddRange(secondSeries._Items);
        }
        else
        {
            //  Добавление второго преобразования.
            _Items.Add(second);
        }

        //  Выполнение оптимизации.
        Optimization();
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
    internal protected override sealed void InvokeCore(Signal source, Signal target)
    {
        //  Выполнение первого преобразования.
        _Items[0].InvokeCore(source, target);

        //  Выполнение последующих преобразований.
        for (int i = 1; i < _Items.Count; i++)
        {
            //  Выполнение очередного преобразования.
            _Items[i].InvokeCore(target, target);
        }
    }

    /// <summary>
    /// Выполняет оптимизацию.
    /// </summary>
    private void Optimization()
    {
        //  Создание нового списка преобразований.
        List<Transform> transforms = new();

        //  Список спектральных преобразований.
        List<SpectralTransform> spectrals = new();

        //  Добавляет спектральные преобразования.
        void addSpectrals()
        {
            //  Проверка списка спектральных преобразований.
            if (spectrals.Count != 0)
            {
                //  Создание серии спектральных преобразований.
                SpectralTransformSeries series = new(spectrals);

                //  Проверка серии.
                if (series.Count != 0)
                {
                    //  Добавление серии в список.
                    transforms.Add(series);
                }

                //  Очистка списка спектральных преобразований.
                spectrals.Clear();
            }
        }

        //  Перебор преобразований.
        foreach (Transform transform in _Items)
        {
            //  Проверка тождественного преобразования.
            if (transform is IdentityTransform)
            {
                //  Переход к следующему элементу.
                continue;
            }

            //  Проверка спектрального преобразования.
            if (transform is SpectralTransform spectral)
            {
                //  Добавление в список спектральных преобразований.
                spectrals.Add(spectral);
            }
            else
            {
                //  Добавление спектрального преобразования.
                addSpectrals();

                //  Добавление текущего преобразования в список.
                transforms.Add(transform);
            }
        }

        //  Добавление спектрального преобразования.
        addSpectrals();

        //  Очистка исходного списка.
        _Items.Clear();

        //  Добавление оптимизированных преобразований.
        _Items.AddRange(transforms);

        //  Проверка пустого списка.
        if (_Items.Count == 0)
        {
            //  Добавление тождественного преобразования.
            _Items.Add(Identity);
        }
    }
}
