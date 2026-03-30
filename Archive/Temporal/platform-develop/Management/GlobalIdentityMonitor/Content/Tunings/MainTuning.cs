using System.ComponentModel;

namespace Apeiron.Services.GlobalIdentity.Tunings;

/// <summary>
/// Представляет класс описывающий секцию основных настроек.
/// </summary>
[Serializable]
public class MainTuning : Tuning, INotifyPropertyChanged
{
    private int _RefreshPeriod;
    private int _KeepAlivePeriod;

    /// <summary>
    /// Возвращает объект, с помощью которого можно синхронизировать доступ.
    /// </summary>
    private readonly object SyncRoot = new();

    /// <summary>
    /// Возвращает или инициализирует период формирования идентификационных данных в миллисекундах.
    /// </summary>
    public int RefreshPeriod 
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                return _RefreshPeriod;
            }
        }
        set
        {
            if (_RefreshPeriod == value)
                return;

            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                _RefreshPeriod = value;
            }

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(RefreshPeriod)));
        }
    }

    /// <summary>
    /// Возвращает или задаёт период в течении которого система считается на связи. KeepAlive (по умолчанию 1 час)
    /// </summary>
    public int KeepAlivePeriod
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                return _KeepAlivePeriod;
            }
        }
        set
        {
            if (_KeepAlivePeriod == value)
                return;
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                _KeepAlivePeriod = value;
            }

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(KeepAlivePeriod)));
        }
    }

    /// <summary>
    /// Инициализирует класс.
    /// </summary>
    public MainTuning()
    {
        // Устанавливает значения по умолчанию для свойства.
        RefreshPeriod = 7000; // В милисекундах
        KeepAlivePeriod = 60; // в минутах
    }

    /// <summary>
    /// Выполняет проверку настроек.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Один из параметров меньше или равен нулю.</exception>
    public override void Validation()
    {
        // Проверка периода формирования идентификационных данных в миллисекундах.
        Check.IsPositive(RefreshPeriod, nameof(RefreshPeriod));
        // Проверка периода больше, которого система считается неактивной.
        Check.IsPositive(KeepAlivePeriod, nameof(KeepAlivePeriod));
    }

    /// <summary>
    /// Событие возникающее при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Оброботчик события.
    /// </summary>
    /// <param name="e">Аргументы события.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }

}
