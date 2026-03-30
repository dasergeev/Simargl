using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace Apeiron.Platform.Demo.AdxlDemo.Adxl;

/// <summary>
/// Представляет параметр датчика ADXL.
/// </summary>
public sealed class AdxlDeviceParameter :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения текущего значения.
    /// </summary>
    private double _Value;

    /// <summary>
    /// Поле для хранения индекса текущего значения.
    /// </summary>
    private int _ValueIndex;

    /// <summary>
    /// Поле для хранения текстового значения.
    /// </summary>
    private string _TextValue;

    /// <summary>
    /// Поле для хранения метода, возвращающего минимальное значение.
    /// </summary>
    private readonly Func<double> _RecipientMinValue;

    /// <summary>
    /// Поле для хранения метода, возвращающего максимальное значение.
    /// </summary>
    private readonly Func<double> _RecipientMaxValue;

    /// <summary>
    /// Поле для хранения метода, возвращающего массив значений.
    /// </summary>
    private readonly Func<double[]> _RecipientValues;

    /// <summary>
    /// Поле для хранения метода, возвращающего массив текстовых значений.
    /// </summary>
    private readonly Func<string[]> _RecipientTextValues;

    /// <summary>
    /// Поле для хранения массива значений.
    /// </summary>
    private List<double> _Values;

    /// <summary>
    /// Поле для хранения метода, отправляющего настройки в датчик.
    /// </summary>
    private readonly Action<double, int> _Sender;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="device">
    /// Устройство.
    /// </param>
    /// <param name="initValue">
    /// Начальное значение.
    /// </param>
    /// <param name="name">
    /// Имя параметра.
    /// </param>
    /// <param name="unit">
    /// ЕДиница измерения.
    /// </param>
    /// <param name="description">
    /// Описание параметра.
    /// </param>
    /// <param name="isContinuous">
    /// Значение, определяющее является ли параметр непрерывным.
    /// </param>
    /// <param name="recipientActiveValue">
    /// Метод, получающий активное значение.
    /// </param>
    /// <param name="recipientMinValue">
    /// Метод, получающий минимальное значение.
    /// </param>
    /// <param name="recipientMaxValue">
    /// Метод, получающий максимальное значение.
    /// </param>
    /// <param name="recipientValues">
    /// Метод, получающий массив значений.
    /// </param>
    /// <param name="recipientTextValues">
    /// Метод, получающий массив текстовых значений.
    /// </param>
    /// <param name="sender">
    /// Метод, отправляющий значение в датчик.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="device"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="description"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="recipientActiveValue"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="recipientMinValue"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="recipientMaxValue"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="recipientValues"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="recipientTextValues"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sender"/> передана пустая ссылка.
    /// </exception>
    public AdxlDeviceParameter(AdxlDevice device, double initValue, string name, string unit, string description, bool isContinuous,
        Func<double> recipientActiveValue, Func<double> recipientMinValue, Func<double> recipientMaxValue,
        Func<double[]> recipientValues, Func<string[]> recipientTextValues,
        Action<double, int> sender)
    {
        //  Проверка ссылок.
        IsNotNull(device, nameof(device));
        IsNotNull(recipientActiveValue, nameof(recipientActiveValue));

        //  Установка значений полей.
        _RecipientMinValue = IsNotNull(recipientMinValue, nameof(recipientMinValue));
        _RecipientMaxValue = IsNotNull(recipientMaxValue, nameof(recipientMaxValue));
        _RecipientValues = IsNotNull(recipientValues, nameof(recipientValues));
        _RecipientTextValues = IsNotNull(recipientTextValues, nameof(recipientTextValues));
        _Sender = IsNotNull(sender, nameof(sender));

        //  Установка значений свойств.
        Name = IsNotNull(name, nameof(name));
        Unit = IsNotNull(unit, nameof(unit));
        Description = IsNotNull(description, nameof(description));
        IsContinuous = isContinuous;

        //  Запрос значений свойств.
        ActiveValue = recipientActiveValue();
        MinValue = recipientMinValue();
        MaxValue = recipientMaxValue();
        _Values = new(recipientValues());

        //  Создание коллекции текстовых значений.
        TextValues = new(recipientTextValues());

        //  Добавление обработчика события.
        device.PropertyChanged += (sender, e) =>
        {
            //  Получение текущего значения.
            double activeValue = recipientActiveValue();

            //  Проверка необходимости изменения значения.
            if (ActiveValue != activeValue)
            {
                //  Установка нового значения.
                ActiveValue = activeValue;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(ActiveValue)));
            }
        };

        //  Установка значения.
        Value = initValue;
        _TextValue = $"{Value} {Unit}";

        //  Создание отметок для непрерывного изменения значений.
        TickMarks = new();

        //  Перебор отметок.
        for (int i = -40; i <= 40; i++)
        {
            //  Добавление отметки.
            TickMarks.Add(i);
        }
    }

    /// <summary>
    /// Возвращает имя параметра.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает единицу измерения.
    /// </summary>
    public string Unit { get; }

    /// <summary>
    /// Возвращает описание параметра.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Возвращает значение, определяющее является ли параметр непрерывным.
    /// </summary>
    public bool IsContinuous { get; }

    /// <summary>
    /// Возвращает значение, определяющее видимость элементов управления, настраивающих непрерывное значение.
    /// </summary>
    public Visibility ContinuousVisibility => IsContinuous ? Visibility.Visible : Visibility.Collapsed;

    /// <summary>
    /// Возвращает значение, определяющее видимость элементов управления, настраивающих дискретное значение.
    /// </summary>
    public Visibility NotContinuousVisibility => IsContinuous ? Visibility.Collapsed : Visibility.Visible;

    /// <summary>
    /// Возвращает активное значение.
    /// </summary>
    public double ActiveValue { get; private set; }

    /// <summary>
    /// Возвращает минимально допустимое значение.
    /// </summary>
    public double MinValue { get; private set; }

    /// <summary>
    /// Возвращает максимально допустимое значение.
    /// </summary>
    public double MaxValue { get; private set; }

    /// <summary>
    /// Возвращает коллекцию текстовых значений.
    /// </summary>
    public ObservableCollection<string> TextValues { get; }

    /// <summary>
    /// Возвращает отметки для непрерывного изменения значений.
    /// </summary>
    public DoubleCollection TickMarks { get; }

    /// <summary>
    /// Возвращает индекс значения.
    /// </summary>
    public int ValueIndex
    {
        get => _ValueIndex;
        set
        {
            //  Проверка дискретного значения.
            if (!IsContinuous)
            {
                //  Проверка изменения индекса значения.
                if (_ValueIndex != value)
                {
                    //  Установка нового значения.
                    _ValueIndex = value;

                    ////  Вызов события об изменении значения свойства.
                    //OnPropertyChanged(new(nameof(ValueIndex)));

                    //  Изменение текущего значения.
                    if (_ValueIndex != -1)
                    {
                        Value = _Values[_ValueIndex];
                    }
                }
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт текстовое значение.
    /// </summary>
    public string TextValue
    {
        get => _TextValue;
        set
        {
            //  Проверка непрерывного значения.
            if (IsContinuous)
            {
                //  Корректировка значения.
                value = value.ToLower().Replace(Unit.ToLower(), string.Empty);

                //  Извлечение числового значения.
                if (double.TryParse(value, out double numericValue))
                {
                    //  Установка нового значения.
                    Value = numericValue;
                }
                else
                {
                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(TextValue)));
                }
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт текущее значение свойства.
    /// </summary>
    public double Value
    {
        get => _Value;
        set
        {
            //  Корректировка значения.
            value = Math.Max(value, MinValue);
            value = Math.Min(value, MaxValue);

            //  Проверка дискретного значения.
            if (!IsContinuous)
            {
                //  Корректировка по ближайшему значению.
                value = _Values.Select(x => new
                {
                    Value = x,
                    Delta = Math.Abs(x - value)
                }).OrderBy(x => x.Delta).First().Value;
            }

            //  Проверка необхоимости изменения значения.
            if (_Value != value)
            {
                //  Установка нового значения.
                _Value = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(Value)));
            }

            //  Проверка непрерывного значения.
            if (IsContinuous)
            {
                //  Получение текстового значения.
                string textValue = $"{Value} {Unit}";

                //  Проверка изменения значения.
                if (_TextValue != textValue)
                {
                    //  Установка нового значения.
                    _TextValue = textValue;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(TextValue)));
                }
            }

            //  Проверка дискретного значения.
            if (!IsContinuous)
            {
                //  Получение индекса значения.
                int index = _Values.IndexOf(value);

                //  Проверка изменения индекса значения.
                if (_ValueIndex != index)
                {
                    //  Установка нового значения.
                    _ValueIndex = index;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(ValueIndex)));
                }
            }
        }
    }

    /// <summary>
    /// Обновляет параметры.
    /// </summary>
    public void Update()
    {
        //  Получение текстовых значений.
        string[] textValues = _RecipientTextValues();

        //  Флаг необходимости изменения текстовых значений.
        bool isTextUpdate = textValues.Length != TextValues.Count;

        //  Проверка необходимости поэлементного сравнения.
        if (!isTextUpdate)
        {
            //  Перебор текстовых значений.
            for (int i = 0; i < TextValues.Count; i++)
            {
                //  Проверка значения.
                if (TextValues[i] != textValues[i])
                {
                    //  Установка флага.
                    isTextUpdate = true;

                    //  Завершение перебора значений.
                    break;
                }
            }
        }

        //  Проверка необходимости обновления.
        if (isTextUpdate)
        {
            //  Очистка списка значений.
            TextValues.Clear();

            //  Перебор значений.
            for (int i = 0; i < textValues.Length; i++)
            {
                //  Добавление значения в список.
                TextValues.Add(textValues[i]);
            }
        }

        //  Запрос значений.
        _Values = new(_RecipientValues());
        double minValue = _RecipientMinValue();
        double maxValue = _RecipientMaxValue();

        //  Проверка изменения минимального значения.
        if (MinValue != minValue)
        {
            //  Установка нового значения.
            MinValue = minValue;

            //  Вызов события об изменении значения свойства.
            OnPropertyChanged(new(nameof(MinValue)));
        }

        //  Проверка изменения максимального значения.
        if (MaxValue != maxValue)
        {
            //  Установка нового значения.
            MaxValue = maxValue;

            //  Вызов события об изменении значения свойства.
            OnPropertyChanged(new(nameof(MaxValue)));
        }

        //  Установка текущего значения.
        Value = _Value;
    }

    /// <summary>
    /// Сохраняет настройки в датчик.
    /// </summary>
    public void Save()
    {
        //  Отправка значения.
        _Sender(_Value, ValueIndex);
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        PropertyChanged?.Invoke(this, e);
    }
}
