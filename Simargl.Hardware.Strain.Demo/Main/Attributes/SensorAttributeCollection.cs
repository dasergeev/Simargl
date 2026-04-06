using Simargl.Hardware.Strain.Demo.Main.Properties;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace Simargl.Hardware.Strain.Demo.Main.Attributes;

/// <summary>
/// Представляет коллекцию атрибутов датчика.
/// </summary>
public sealed class SensorAttributeCollection :
    IEnumerable<SensorAttribute>
{
    /// <summary>
    /// Поле для хранения списка элементов.
    /// </summary>
    private readonly List<SensorAttribute> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public SensorAttributeCollection()
    {
        //  Создание списка элементов.
        _Items = [];
    }

    /// <summary>
    /// Добавляет статический атрибут.
    /// </summary>
    /// <param name="name">
    /// Имя атрибута.
    /// </param>
    /// <param name="value">
    /// Статическое значение атрибута.
    /// </param>
    public void Add(string name, string value)
    {
        //  Создание атрибута.
        SensorAttribute attribute = new(name, SensorAttributeFormat.Static)
        {
            Value = value,
            HasValue = true,
            IsSynchronized = true,
        };

        //  Добавление атрибута.
        _Items.Add(attribute);
    }

    /// <summary>
    /// Добавляет доступный только для чтения атрибут.
    /// </summary>
    /// <param name="name">
    /// Имя атрибута.
    /// </param>
    /// <param name="subscribe">
    /// Метод, подписывающийся на событие.
    /// </param>
    /// <param name="getter">
    /// Метод, получающий значение.
    /// </param>
    public void Add(string name, Action<EventHandler> subscribe, Func<string> getter)
    {
        //  Создание атрибута.
        SensorAttribute attribute = new(name, SensorAttributeFormat.Readable)
        {
            Value = getter(),
            HasValue = true,
            IsSynchronized = true,
        };

        //  Добавление обработчика события.
        subscribe((sender, e) => attribute.Value = getter());

        //  Добавление атрибута.
        _Items.Add(attribute);
    }

    /// <summary>
    /// Добавляет доступный только для чтения атрибут.
    /// </summary>
    /// <param name="name">
    /// Имя атрибута.
    /// </param>
    /// <param name="parent">
    /// Объект, содержащий свойство.
    /// </param>
    /// <param name="propertyName">
    /// Имя свойства.
    /// </param>
    public void Add(string name, INotifyPropertyChanged parent, string propertyName)
    {
        try
        {
            //  Получение свойства.
            if (parent.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance)
                is not PropertyInfo property)
            {
                //  Завершение работы.
                throw new InvalidOperationException(
                    $"Свойство {propertyName} не найдено в {parent.GetType().FullName}.");
            }

            //  Создание атрибута.
            SensorAttribute attribute = new(name, SensorAttributeFormat.Readable)
            {
                Value = getter(),
                HasValue = true,
                IsSynchronized = true,
            };

            //  Добавление обработчика события.
            parent.PropertyChanged += delegate (object? sender, PropertyChangedEventArgs e)
            {
                //  Проверка имени.
                if (e.PropertyName == propertyName)
                {
                    //  Установка значения.
                    attribute.Value = getter();
                }
            };

            //  Получает значение.
            string getter()
            {
                //  Получение значения.
                if (property.GetValue(parent) is not object obj)
                {
                    //  Завершение работы.
                    throw new InvalidOperationException(
                        $"Не удалось получить значение свойства {propertyName} в {parent.GetType().FullName}.");
                }

                //  Возврат значения.
                return obj.ToString() ?? throw new InvalidOperationException(
                    $"Не удалось получить значение свойства {propertyName} в {parent.GetType().FullName}.");
            }

            //  Добавление атрибута.
            _Items.Add(attribute);
        }
        catch (Exception ex)
        {
            ((App)Application.Current).Journal.AddError(ex);
        }
    }

    /// <summary>
    /// Добавляет доступный только для чтения атрибут.
    /// </summary>
    /// <param name="name">
    /// Имя атрибута.
    /// </param>
    /// <param name="parent">
    /// Объект, содержащий свойство.
    /// </param>
    /// <param name="propertyName">
    /// Имя свойства.
    /// </param>
    /// <param name="format">
    /// Формат отображения.
    /// </param>
    public void Add(string name, INotifyPropertyChanged parent, string propertyName, string format)
    {
        //  Получение свойства.
        if (parent.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance)
            is not PropertyInfo property)
        {
            //  Завершение работы.
            throw new InvalidOperationException(
                $"Свойство {propertyName} не найдено в {parent.GetType().FullName}.");
        }

        //  Создание атрибута.
        SensorAttribute attribute = new(name, SensorAttributeFormat.Readable)
        {
            Value = getter(),
            HasValue = true,
            IsSynchronized = true,
        };

        //  Добавление обработчика события.
        parent.PropertyChanged += delegate (object? sender, PropertyChangedEventArgs e)
        {
            //  Проверка имени.
            if (e.PropertyName == propertyName)
            {
                //  Установка значения.
                attribute.Value = getter();
            }
        };

        //  Получает значение.
        string getter()
        {
            //  Получение значения.
            if (property.GetValue(parent) is not IFormattable formattable)
            {
                //  Завершение работы.
                throw new InvalidOperationException(
                    $"Не удалось получить значение свойства {propertyName} в {parent.GetType().FullName}.");
            }

            //  Возврат значения.
            return formattable.ToString(format, null);
        }

        //  Добавление атрибута.
        _Items.Add(attribute);
    }

    /// <summary>
    /// Добавляет атрибут, связанный со свойством.
    /// </summary>
    /// <typeparam name="T">
    /// Тип значения.
    /// </typeparam>
    /// <param name="property">
    /// Свойство.
    /// </param>
    /// <param name="format">
    /// Формат атрибута.
    /// </param>
    public SensorAttribute<T> Add<T>(SensorProperty<T> property, SensorAttributeFormat format)
    {
        //  Создание атрибута.
        SensorAttribute<T> attribute = new(property, format);

        //  Добавление атрибута.
        _Items.Add(attribute);

        return attribute;
    }

    /// <summary>
    /// Добавляет атрибут, связанный со свойством.
    /// </summary>
    /// <typeparam name="T">
    /// Тип значения.
    /// </typeparam>
    /// <param name="property">
    /// Свойство.
    /// </param>
    /// <param name="values">
    /// Список значений.
    /// </param>
    public void Add<T>(SensorProperty<T> property, string[] values)
    {
        //  Создание атрибута.
        SensorAttribute<T> attribute = new(property, values);

        //  Добавление атрибута.
        _Items.Add(attribute);
    }

    /// <summary>
    /// Добавляет атрибут, связанный со свойством.
    /// </summary>
    /// <typeparam name="T">
    /// Тип значения.
    /// </typeparam>
    /// <param name="property">
    /// Свойство.
    /// </param>
    /// <param name="format">
    /// Формат атрибута.
    /// </param>
    /// <param name="converter">
    /// Метод преобразования значения.
    /// </param>
    public void Add<T>(SensorProperty<T> property, SensorAttributeFormat format, Func<T, string> converter)
    {
        //  Создание атрибута.
        SensorAttribute<T> attribute = new(property, format, converter);

        //  Добавление атрибута.
        _Items.Add(attribute);
    }

    /// <summary>
    /// Возвращает перечислитель элементов коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель элементов коллекции.
    /// </returns>
    public IEnumerator<SensorAttribute> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable<SensorAttribute>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель элементов коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель элементов коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable)_Items).GetEnumerator();
    }
}
