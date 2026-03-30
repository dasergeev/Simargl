using System.ComponentModel;

namespace Apeiron.Platform.Software.ChannelAdditor.Projects;

/// <summary>
/// Представляет правило, добавляющее канал.
/// </summary>
public sealed class Rule :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения имени канала.
    /// </summary>
    private string _Name;

    /// <summary>
    /// Поле для хранения единица измерения канала.
    /// </summary>
    private string _Unit;

    /// <summary>
    /// Поле для хранения частоты дискретизации.
    /// </summary>
    private int _Sampling;

    /// <summary>
    /// Поле для хранения частоты среза фильтра.
    /// </summary>
    private int _Cutoff;

    /// <summary>
    /// Поле для хранения значения.
    /// </summary>
    private double _Value;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Rule()
    {
        //  Установка значений полей по умолчанию.
        _Name = string.Empty;
        _Unit = string.Empty;
        _Sampling = 1;
        _Cutoff = 0;
        _Value = 0;
    }

    /// <summary>
    /// Возвращает или задаёт имя канала.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Name
    {
        get => _Name;
        set
        {
            //  Проверка ссылки на путь.
            Check.IsNotNull(value, nameof(Name));

            //  Проверка изменения значения свойства.
            if (_Name != value)
            {
                //  Изменение значения свойства.
                _Name = value;

                //  Вызов события об изменении значения свойства.
                PropertyChanged?.Invoke(this, new(nameof(Name)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт единицу измерения.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Unit
    {
        get => _Unit;
        set
        {
            //  Проверка ссылки на путь.
            Check.IsNotNull(value, nameof(Unit));

            //  Проверка изменения значения свойства.
            if (_Unit != value)
            {
                //  Изменение значения свойства.
                _Unit = value;

                //  Вызов события об изменении значения свойства.
                PropertyChanged?.Invoke(this, new(nameof(Unit)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    public int Sampling
    {
        get => _Sampling;
        set
        {
            //  Проверка изменения значения свойства.
            if (_Sampling != value)
            {
                //  Изменение значения свойства.
                _Sampling = value;

                //  Вызов события об изменении значения свойства.
                PropertyChanged?.Invoke(this, new(nameof(Sampling)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public int Cutoff
    {
        get => _Cutoff;
        set
        {
            //  Проверка изменения значения свойства.
            if (_Cutoff != value)
            {
                //  Изменение значения свойства.
                _Cutoff = value;

                //  Вызов события об изменении значения свойства.
                PropertyChanged?.Invoke(this, new(nameof(Cutoff)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение канала.
    /// </summary>
    public double Value
    {
        get => _Value;
        set
        {
            //  Проверка изменения значения свойства.
            if (_Value != value)
            {
                //  Изменение значения свойства.
                _Value = value;

                //  Вызов события об изменении значения свойства.
                PropertyChanged?.Invoke(this, new(nameof(Value)));
            }
        }
    }
}
