using Simargl.Hardware.Strain.Demo.Core;
using Simargl.Hardware.Strain.Demo.Journaling;
using Simargl.Hardware.Strain.Demo.Main.Properties;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Simargl.Hardware.Strain.Demo.Main.Attributes;

/// <summary>
/// Представляет атрибут датчика.
/// </summary>
public class SensorAttribute :
    Anything,
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Происходит при изменении значения <see cref="HasValue"/>.
    /// </summary>
    public event EventHandler? HasValueChanged;

    /// <summary>
    /// Происходит при изменении значения <see cref="Value"/>.
    /// </summary>
    public event EventHandler? ValueChanged;

    /// <summary>
    /// Происходит при изменении значения <see cref="IsAvailable"/>.
    /// </summary>
    public event EventHandler? IsAvailableChanged;

    /// <summary>
    /// Происходит при изменении значения <see cref="IsSynchronized"/>.
    /// </summary>
    public event EventHandler? IsSynchronizedChanged;

    /// <summary>
    /// Поле для хранения карты изображений.
    /// </summary>
    private static readonly ConcurrentDictionary<string, BitmapImage> _ImageMap = [];

    /// <summary>
    /// Поле для хранения значения, определяющего имеет ли атрибут значение.
    /// </summary>
    private bool _HasValue;

    /// <summary>
    /// Поле для хранения значения.
    /// </summary>
    private string _Value = string.Empty;

    /// <summary>
    /// Поле для хранения значения, определяющего доступен ли атрибут.
    /// </summary>
    private bool _IsAvailable;

    /// <summary>
    /// Поле для хранения значения, определяющего синхронизирован ли атрибут.
    /// </summary>
    private bool _IsSynchronized;

    /// <summary>
    /// Поле для хранения изображения.
    /// </summary>
    private BitmapImage _Image;

    /// <summary>
    /// Поле для хранения выбранного элемента.
    /// </summary>
    private int _SelectedItemIndex;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="name">
    /// Имя атрибута.
    /// </param>
    /// <param name="format">
    /// Формат атрибута.
    /// </param>
    public SensorAttribute(string name, SensorAttributeFormat format)
    {
        //  Обращение к объекту.
        Lay();

        //  Установка имени атрибута.
        Name = name;

        //  Установка формата.
        Format = format;

        //  Установка изображения.
        _Image = GetImage("BallRed.ico");

        //  Добавление обработчиков событий, которые могу изменить изображение.
        HasValueChanged += UpdateImage;
        IsAvailableChanged += UpdateImage;
        IsSynchronizedChanged += UpdateImage;

        //  Обновление изображения.
        UpdateImage(this, EventArgs.Empty);
    }

    /// <summary>
    /// Возвращает имя атрибута.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает формат атрибута.
    /// </summary>
    public SensorAttributeFormat Format { get; }

    /// <summary>
    /// Возвращает значение, определяющее имеет ли атрибут значение.
    /// </summary>
    public bool HasValue
    {
        get => _HasValue;
        set
        {
            //  Проверка изменения значения.
            if (_HasValue != value)
            {
                //  Установка нового значения.
                _HasValue = value;

                //  Вызов события об изменении значения.
                OnHasValueChanged(EventArgs.Empty);
                OnPropertyChanged(new(nameof(HasValue)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение.
    /// </summary>
    public string Value
    {
        get => _Value;
        set
        {
            //  Корректировка значения.
            if (string.IsNullOrWhiteSpace(value))
            {
                //  Установка значения по умолчанию.
                value = string.Empty;
            }

            //  Проверка изменения значения.
            if (_Value != value)
            {
                //  Отправка значения.
                SendValue(value);

                //  Установка нового значения.
                _Value = value;

                //  Вызов события об изменении значения.
                OnValueChanged(EventArgs.Empty);
                OnPropertyChanged(new(nameof(Value)));
            }
        }
    }

    /// <summary>
    /// Возвращает значение, определяющее доступен ли атрибут.
    /// </summary>
    public bool IsAvailable
    {
        get => _IsAvailable;
        set
        {
            //  Проверка изменения значения.
            if (_IsAvailable != value)
            {
                //  Установка нового значения.
                _IsAvailable = value;

                //  Вызов события об изменении значения.
                OnIsAvailableChanged(EventArgs.Empty);
                OnPropertyChanged(new(nameof(IsAvailable)));
            }
        }
    }

    /// <summary>
    /// Возвращает значение, определяющее синхронизирован ли атрибут.
    /// </summary>
    public bool IsSynchronized
    {
        get => _IsSynchronized;
        set
        {
            //  Проверка изменения значения.
            if (_IsSynchronized != value)
            {
                //  Установка нового значения.
                _IsSynchronized = value;

                //  Вызов события об изменении значения.
                OnIsSynchronizedChanged(EventArgs.Empty);
                OnPropertyChanged(new(nameof(IsSynchronized)));
            }
        }
    }

    /// <summary>
    /// Возвращает изображение.
    /// </summary>
    public BitmapImage Image
    {
        get => _Image;
        private set
        {
            //  Проверка изменения изображения.
            if (!ReferenceEquals(_Image, value))
            {
                //  Установка нового значения.
                _Image = value;

                //  Вызов события об изменении значения.
                OnPropertyChanged(new(nameof(Image)));
            }
        }
    }

    /// <summary>
    /// Возвращает выбранный индекс.
    /// </summary>
    public int SelectedItemIndex
    {
        get => _SelectedItemIndex;
        set
        {
            //  Проверка изменения изображения.
            if (_SelectedItemIndex != value)
            {
                //  Установка нового значения.
                _SelectedItemIndex = value;

                //  Вызов события об изменении значения.
                OnPropertyChanged(new(nameof(SelectedItemIndex)));
            }
        }
    }

    /// <summary>
    /// Возвращает список значений.
    /// </summary>
    public virtual string[] Values { get; } = [];

    /// <summary>
    /// Отправляет значение.
    /// </summary>
    /// <param name="value">
    /// Отправляемое значение.
    /// </param>
    protected virtual void SendValue(string value)
    {

    }

    /// <summary>
    /// Выполняет сброс значения.
    /// </summary>
    public virtual void Reset()
    {

    }

    /// <summary>
    /// Асинхронно загружает значение.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задча, загружающая значение.
    /// </returns>
    public virtual async Task LoadAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref PropertyChanged)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="HasValueChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnHasValueChanged(EventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref HasValueChanged)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="ValueChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnValueChanged(EventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref ValueChanged)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="IsAvailableChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnIsAvailableChanged(EventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref IsAvailableChanged)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="IsSynchronized"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnIsSynchronizedChanged(EventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref IsSynchronizedChanged)?.Invoke(this, e);
    }

    /// <summary>
    /// Возвращает изображение.
    /// </summary>
    /// <param name="name">
    /// Имя изображения.
    /// </param>
    /// <returns>
    /// Изображение.
    /// </returns>
    private static BitmapImage GetImage(string name)
    {
        //  Возврат изображения из карты.
        return _ImageMap.GetOrAdd(name, delegate (string name)
        {
            //  Получение изображения.
            return new(new Uri($"pack://application:,,,/Images/{name}", UriKind.Absolute));
        });
    }

    /// <summary>
    /// Обрабатывает событие, которое может изменить изображение.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void UpdateImage(object? sender, EventArgs e)
    {
        //  Проверка формата.
        if (Format == SensorAttributeFormat.Static)
        {
            //  Установка изображения.
            Image = GetImage("BallGreen.ico");

            //  Завершение обновления.
            return;
        }

        //  Проверка доступности.
        if (!IsAvailable)
        {
            //  Установка изображения.
            Image = GetImage("BallRed.ico");

            //  Завершение обновления.
            return;
        }

        //  Проверка синхронизации.
        if (!IsSynchronized || !HasValue)
        {
            //  Установка изображения.
            Image = GetImage("BallYellow.ico");

            //  Завершение обновления.
            return;
        }

        //  Установка изображения.
        Image = GetImage("BallGreen.ico");
    }
}

/// <summary>
/// Представляет атрибут датчика.
/// </summary>
/// <typeparam name="T">
/// Тип значения атрибута.
/// </typeparam>
public sealed class SensorAttribute<T> :
    SensorAttribute
{
    /// <summary>
    /// Поле для хранения свойства.
    /// </summary>
    private readonly SensorProperty<T> _Property;

    /// <summary>
    /// Поле для хранения списка значений.
    /// </summary>
    private readonly string[]? _Values;

    /// <summary>
    /// Поле для хранения метода преобразования значения.
    /// </summary>
    private readonly Func<T, string>? _Converter;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="property">
    /// Свойство.
    /// </param>
    /// <param name="format">
    /// Формат атрибута.
    /// </param>
    public SensorAttribute(SensorProperty<T> property, SensorAttributeFormat format) :
        base(property.Name, format)
    {
        //  Обращение к объекту.
        Lay();

        //  Установка свойства.
        _Property = property;
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="property">
    /// Свойство.
    /// </param>
    /// <param name="values">
    /// Список значений.
    /// </param>
    public SensorAttribute(SensorProperty<T> property, string[] values) :
        this(property, SensorAttributeFormat.Selectable)
    {
        //  Установка списка значений.
        _Values = values;

        //  Добавление обработчика события.
        PropertyChanged += delegate (object? sender, PropertyChangedEventArgs e)
        {
            //  Проверка имени свойства.
            if (e.PropertyName == nameof(SelectedItemIndex))
            {
                //  Проверка индекса.
                if (0 <= SelectedItemIndex && SelectedItemIndex < Values.Length - 1)
                {
                    //  Установка значения.
                    Value = SelectedItemIndex.ToString();
                }
                else
                {
                    //  Получение токена отмены.
                    CancellationToken cancellationToken = Application.CancellationToken;

                    //  Запуск асинхронной задачи.
                    _ = Task.Run(async delegate
                    {
                        //  Ожидание.
                        await Task.Delay(300, cancellationToken).ConfigureAwait(false);

                        //  Выполнение в основном потоке.
                        await Application.Invoker.InvokeAsync(delegate
                        {
                            //  Получение индекса.
                            if (int.TryParse(Value, out int index))
                            {
                                //  Проверка индекса.
                                if (index < 0 || index > values.Length)
                                {
                                    //  Корректировка индекса.
                                    index = values.Length - 1;
                                }

                                //  Установка индекса.
                                SelectedItemIndex = index;
                            }
                        }, cancellationToken).ConfigureAwait(false);
                    }, cancellationToken);
                }
            }
            else if (e.PropertyName == nameof(Value))
            {
                //  Получение индекса.
                if (int.TryParse(Value, out int index))
                {
                    //  Проверка индекса.
                    if (index < 0 || index > values.Length)
                    {
                        //  Корректировка индекса.
                        index = values.Length - 1;
                    }

                    //  Установка индекса.
                    SelectedItemIndex = index;
                }
            }
        };
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="property">
    /// Свойство.
    /// </param>
    /// <param name="format">
    /// Формат атрибута.
    /// </param>
    /// <param name="converter">
    /// Метод преобразования значения.
    /// </param>
    public SensorAttribute(SensorProperty<T> property, SensorAttributeFormat format, Func<T, string> converter) :
        this(property, format)
    {
        //  Установка метода преобразования значения.
        _Converter = converter;
    }

    /// <summary>
    /// Возвращает список значений.
    /// </summary>
    public override string[] Values => _Values ?? base.Values;

    /// <summary>
    /// Отправляет значение.
    /// </summary>
    /// <param name="value">
    /// Отправляемое значение.
    /// </param>
    protected override void SendValue(string value)
    {
        //  Проверка значения.
        if (string.IsNullOrWhiteSpace(value))
        {
            //  Сбор значения.
            HasValue = false;

            //  Завершение установки.
            return;
        }

        //  Проверка т

        //uint16
        //float
        //IPAddress

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка формата.
            if (Format == SensorAttributeFormat.Writable ||
                Format == SensorAttributeFormat.Selectable)
            {
                //  Проверка типа.
                if (_Property is SensorProperty<ushort> ushortProperty)
                {
                    //  Получение значения.
                    ushort tValue = ushort.Parse(value);

                    //  Отправка значения.
                    send(ushortProperty, tValue);
                }

                //  Проверка типа.
                if (_Property is SensorProperty<float> floatProperty)
                {
                    //  Получение значения.
                    float tValue = float.Parse(value);

                    //  Отправка значения.
                    send(floatProperty, tValue);
                }

                //  Проверка типа.
                if (_Property is SensorProperty<IPAddress> ipAddressProperty)
                {
                    //  Получение значения.
                    IPAddress tValue = IPAddress.Parse(value);

                    //  Проверка типа.
                    if (tValue.AddressFamily != AddressFamily.InterNetwork)
                    {
                        //  Выброс исключения.
                        throw new InvalidOperationException("Введено некорректное значение");
                    }

                    //  Отправка значения.
                    send(ipAddressProperty, tValue);
                }
            }
        }
        catch (Exception ex)
        {
            //  Выброс исключения.
            throw new InvalidOperationException($"Введено некорректное значение: {value}", ex);
        }

        //  Отправляет значение.
        void send<TOther>(SensorProperty<TOther> property, TOther value)
        {
            //  Получение токена отмены.
            CancellationToken cancellationToken = Application.CancellationToken;

            //  Запуск асинхронной задачи.
            _ = Task.Run(async delegate
            {
                try
                {
                    //  Отправка значения.
                    await property.WriteAsync(value, cancellationToken).ConfigureAwait(false);

                    //  Выполнение в основном потоке.
                    await Invoker.InvokeAsync(delegate
                    {
                        //  Сброс значения.
                        HasValue = false;

                        //  Установка состояния синхронизации.
                        IsSynchronized = false;
                    }, cancellationToken).ConfigureAwait(false);

                    //  Загрузка значения.
                    await LoadAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Application.Journal.AddError(ex);
                    throw;
                }

            }, cancellationToken);
        }
    }

    /// <summary>
    /// Асинхронно загружает значение.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задча, загружающая значение.
    /// </returns>
    public override async Task LoadAsync(CancellationToken cancellationToken)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Вызов базового метода.
            await base.LoadAsync(cancellationToken).ConfigureAwait(false);

            //  Выполнение в основном потоке.
            await Invoker.InvokeAsync(delegate
            {
                //  Установка состояния синхронизации.
                IsSynchronized = false;
            }, cancellationToken).ConfigureAwait(false);

            //  Чтение значения.
            T obj = await _Property.ReadAsync(cancellationToken).ConfigureAwait(false) ??
                throw new InvalidOperationException("Не удалось получить значение.");

            //  Текстовое значение.
            string value = obj.ToString() ??
                throw new InvalidOperationException("Не удалось получить значение.");

            //  Проверка списка значений.
            if (_Values is string[] values)
            {
                //  Получение индекса.
                int index = int.Parse(value);

                //  Проверка индекса.
                if (index < 0 || index > values.Length)
                {
                    //  Корректировка индекса.
                    index = values.Length - 1;
                }

                //  Установка индекса.
                SelectedItemIndex = index;

                ////  Проверка индекса.
                //if (index < values.Length)
                //{
                //    //  Установка значения.
                //    value = values[index];
                //}
                //else
                //{
                //    //  Установка значения.
                //    value = $"Неизвестное значение ($index)";
                //}
            }

            //  Проверка преобразователя значений.
            if (_Converter is Func<T, string> converter)
            {
                //  Преобразование значения.
                value = converter(obj);
            }

            //  Выполнение в основном потоке.
            await Invoker.InvokeAsync(delegate
            {
                //  Установка значения.
                Value = value;

                //  Проверка доступности.
                if (IsAvailable)
                {
                    //  Установка значения.
                    HasValue = true;

                    //  Установка состояния синхронизации.
                    IsSynchronized = true;
                }
                else
                {
                    //  Сброс значения.
                    HasValue = false;

                    //  Установка состояния синхронизации.
                    IsSynchronized = false;
                }
            }, cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            //  Выполнение в основном потоке.
            await Invoker.InvokeAsync(delegate
            {
                //  Сброс значения.
                HasValue = false;

                //  Установка состояния синхронизации.
                IsSynchronized = false;
            }, cancellationToken).ConfigureAwait(false);

            //  Повторный выброс исключения.
            throw;
        }
    }

    /// <summary>
    /// Выполняет сброс значения.
    /// </summary>
    public override void Reset()
    {
        //  Проверка формата.
        if (Format == SensorAttributeFormat.Resettable)
        {
            //  Получение токена отмены.
            CancellationToken cancellationToken = Application.CancellationToken;

            //  Запуск асинхронной задачи.
            _ = Task.Run(async delegate
            {
                try
                {
                    //  Проверка типа.
                    if (_Property is SensorProperty<ushort> ushortProperty)
                    {
                        //  Сброс значения.
                        await ushortProperty.WriteAsync(0, cancellationToken).ConfigureAwait(false);
                    }

                    //  Проверка типа.
                    if (_Property is SensorProperty<float> floatProperty)
                    {
                        //  Сброс значения.
                        await floatProperty.WriteAsync(0, cancellationToken).ConfigureAwait(false);
                    }

                    //  Выполнение в основном потоке.
                    await Invoker.InvokeAsync(delegate
                    {
                        //  Сброс значения.
                        HasValue = false;

                        //  Установка состояния синхронизации.
                        IsSynchronized = false;
                    }, cancellationToken).ConfigureAwait(false);

                    //  Загрузка значения.
                    await LoadAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Application.Journal.AddError(ex);
                    throw;
                }
            }, cancellationToken);
        }
    }

    /// <summary>
    /// Поле для хранения приложения.
    /// </summary>
    private App? _Application;

    /// <summary>
    /// Возвращает приложение.
    /// </summary>
    private App Application
    {
        get
        {
            //  Проверка приложения.
            if (_Application is not App application)
            {
                //  Получение приложения.
                application = (App)System.Windows.Application.Current;

                //  Замена приложения.
                application = Interlocked.CompareExchange(ref _Application, application, null) ?? application;
            }

            //  Возврат приложения.
            return application;
        }
    }

    ///// <summary>
    ///// Возвращает журнал.
    ///// </summary>
    //private Journal Journal => Application.Journal;

    /// <summary>
    /// Возвращает средство вызова методов в основном потоке.
    /// </summary>
    private Invoker Invoker => Application.Invoker;

    ///// <summary>
    ///// Возвращает механизм поддержки.
    ///// </summary>
    //private Keeper Keeper => Application.Keeper;

}
