using Apeiron.Platform.Demo.AdxlDemo.Adxl;
using Apeiron.Platform.Demo.AdxlDemo.Nodes;
using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Primitives;
using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo.Channels;

/// <summary>
/// Представляет организатора канала.
/// </summary>
public sealed class ChannelOrganizer :
    Node<INode>
{
    /// <summary>
    /// Постоянная, определяющая минимальную длительность отображаемого фрагмента в секундах.
    /// </summary>
    public const double MinDuration = 5;

    /// <summary>
    /// Постоянная, определяющая максимальную длительность отображаемого фрагмента в секундах.
    /// </summary>
    public const double MaxDuration = 60;

    /// <summary>
    /// Постоянная, определяющая минимальную частоту дискретизации.
    /// </summary>
    public const double MinSampling = 1;

    /// <summary>
    /// Постоянная, определяющая максимальную частоту дискретизации.
    /// </summary>
    public const double MaxSampling = 4000;

    /// <summary>
    /// Постоянная, определяющая минимальную частоту среза.
    /// </summary>
    public const double MinCutoff = 1;

    /// <summary>
    /// Постоянная, определяющая максимальную частоту среза.
    /// </summary>
    public const double MaxCutoff = 1000;

    /// <summary>
    /// Поле для хранения информации о канале.
    /// </summary>
    private readonly ChannelInfo _ChannelInfo;

    /// <summary>
    /// Поле для хранения кэша канала.
    /// </summary>
    private readonly ChannelCache _ChannelCache;

    /// <summary>
    /// Поле для хранения длительности отображаемого фрагмента в секундах.
    /// </summary>
    private volatile float _Duration;

    /// <summary>
    /// Поле для хранения значения, определяющего, задаётся ли время отображения.
    /// </summary>
    private bool _IsCustomTime;

    /// <summary>
    /// Поле для хранения времени начала отображаемого фрагмента.
    /// </summary>
    private DateTime _BeginTime;

    /// <summary>
    /// Поле для хранения значения, определяющего отображается ли канал.
    /// </summary>
    private bool _IsVisible;

    /// <summary>
    /// Поле для хранения цвета элемента управления.
    /// </summary>
    private System.Windows.Media.Color _MediaColor;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="channelInfo">
    /// Информация о канале.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="channelInfo"/> передана пустая ссылка.
    /// </exception>
    public ChannelOrganizer(Engine engine, ChannelInfo channelInfo) : 
        base(engine, IsNotNull(channelInfo, nameof(channelInfo)).Name, NodeFormat.ChannelOrganizer)
    {
        //  Установка информации о канале.
        _ChannelInfo = channelInfo;

        //  Создание кэша канала.
        _ChannelCache = new(engine, channelInfo);

        //  Установка параметров отображения.
        _Duration = 60;
        _IsCustomTime = false;
        _BeginTime = DateTime.Now;
        _IsVisible = false;
        _MediaColor = System.Windows.Media.Color.FromArgb(255, Color.R, Color.G, Color.B);
    }

    /// <summary>
    /// Возвращает ширину линии.
    /// </summary>
    [Browsable(false)]
    public double Width { get; } = 1;

    /// <summary>
    /// Возвращает минимальное время данных.
    /// </summary>
    [Browsable(false)]
    public DateTime MinTime => _ChannelCache.MinTime;

    /// <summary>
    /// Возвращает серийный номер датчика.
    /// </summary>
    [Category("Подключение")]
    [DisplayName("Датчик")]
    [Description("Серйный номер датчика.")]
    public string SerialNumber => $"{_ChannelInfo.SerialNumber:X8}";

    /// <summary>
    /// Возвращает тип канала.
    /// </summary>
    [Category("Подключение")]
    [DisplayName("Тип")]
    [Description("Тип канала.")]
    public ChannelType ChannelType => _ChannelInfo.ChannelType;

    /// <summary>
    /// Возвращает номер сигнала.
    /// </summary>
    [Category("Подключение")]
    [DisplayName("Сигнал")]
    [Description("Номер сигнала.")]
    public int SignalNumber => _ChannelInfo.SignalNumber;

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    [Category("Запись")]
    [DisplayName("Частота")]
    [Description("Частота дискретизации.")]
    public double Sampling
    {
        get => _ChannelInfo.Sampling;
        set
        {
            //  Корректировка значения.
            value = Math.Round(value);
            value = Math.Max(value, MinSampling);
            value = Math.Min(value, MaxSampling);

            //  Выполнение в базовом потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_ChannelInfo.Sampling != value)
                {
                    //  Выполнение транзакции.
                    Engine.ContextManager.Transaction(context =>
                    {
                        //  Установка нового значения.
                        _ChannelInfo.Sampling = value;

                        //  Сохранение изменений.
                        context.SaveChanges();
                    });

                    //  Вызов события.
                    OnPropertyChanged(new(nameof(Sampling)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    [Category("Запись")]
    [DisplayName("Срез")]
    [Description("Частота среза фильтра.")]
    public double Cutoff
    {
        get => _ChannelInfo.Cutoff;
        set
        {
            //  Корректировка значения.
            value = Math.Round(value);
            value = Math.Max(value, MinCutoff);
            value = Math.Min(value, MaxCutoff);

            //  Выполнение в базовом потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_ChannelInfo.Cutoff != value)
                {
                    //  Выполнение транзакции.
                    Engine.ContextManager.Transaction(context =>
                    {
                        //  Установка нового значения.
                        _ChannelInfo.Cutoff = value;

                        //  Сохранение изменений.
                        context.SaveChanges();
                    });

                    //  Вызов события.
                    OnPropertyChanged(new(nameof(Cutoff)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает или задаёт длительности отображаемого фрагмента в секундах.
    /// </summary>
    [Category("Отображение")]
    [DisplayName("Длительность")]
    [Description("Длительность отображаемого фрагмента в секундах.")]
    public double Duration
    {
        get => _Duration;
        set
        {
            //  Корректировка значения.
            value = Math.Round(value);
            value = Math.Max(value, MinDuration);
            value = Math.Min(value, MaxDuration);

            //  Преобразование значения.
            float duration = (float)value;

            //  Выполнение в базовом потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_Duration != duration)
                {
                    //  Установка значения.
                    _Duration = (float)value;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(Duration)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее, задаётся ли время отображения.
    /// </summary>
    [Browsable(false)]
    public bool IsCustomTime
    {
        get => _IsCustomTime;
        set
        {
            //  Выполнение в базовом потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_IsCustomTime != value)
                {
                    //  Установка значения.
                    _IsCustomTime = value;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(IsCustomTime)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает или задаёт время начала отображаемого фрагмента.
    /// </summary>
    [Browsable(false)]
    public DateTime BeginTime
    {
        get
        {
            //  Проверка ручного режима.
            if (!_IsCustomTime)
            {
                //  Корректировка текущего значения.
                _BeginTime = DateTime.Now - TimeSpan.FromSeconds(Duration);
            }

            //  Возврат текущего значения.
            return _BeginTime;
        }
        set
        {
            //  Проверка необходимости изменения времени.
            if (_BeginTime != value)
            {
                //  Установка ручного режима.
                IsCustomTime = Math.Abs((_BeginTime - value).TotalMilliseconds) > 100;

                //  Установка текущего значения времени.
                _BeginTime = value;
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт цвет.
    /// </summary>
    [Category("Отображение")]
    [DisplayName("Цвет")]
    [Description("Цвет отображаемого фрагмента.")]
    public System.Drawing.Color Color
    {
        get => System.Drawing.Color.FromArgb((int)_ChannelInfo.Color);
        set
        {
            //  Получение числового значения.
            long newValue = value.ToArgb();

            //  Выполнение в базовом потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_ChannelInfo.Color != newValue)
                {
                    //  Выполнение транзакции.
                    Engine.ContextManager.Transaction(context =>
                    {
                        //  Установка нового значения.
                        _ChannelInfo.Color = newValue;

                        //  Сохранение изменений.
                        context.SaveChanges();
                    });

                    //  Вызов события.
                    OnPropertyChanged(new(nameof(Color)));
                }
            });

            //  Корректировка цвета отображения.
            MediaColor = System.Windows.Media.Color.FromArgb(255, Color.R, Color.G, Color.B);
        }
    }

    /// <summary>
    /// Возвращает или задаёт цвет элемента управления.
    /// </summary>
    [Browsable(false)]
    public System.Windows.Media.Color MediaColor
    {
        get => _MediaColor;
        set
        {
            //  Выполнение в базовом потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_MediaColor != value)
                {
                    //  Установка нового значения.
                    _MediaColor = value;

                    //  Вызов события.
                    OnPropertyChanged(new(nameof(MediaColor)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее отображается ли канал.
    /// </summary>
    [Browsable(false)]
    public bool IsVisible
    {
        get => _IsVisible;
        set
        {
            //  Выполнение в базовом потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_IsVisible != value)
                {
                    //  Установка нового значения.
                    _IsVisible = value;

                    //  Вызов события.
                    OnPropertyChanged(new(nameof(IsVisible)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает серийный номер датчика.
    /// </summary>
    public long GetSerialNumber() => Invoker.Primary(() => _ChannelInfo.SerialNumber);

    /// <summary>
    /// Устанавливает значение серийного номера.
    /// </summary>
    /// <param name="value">
    /// Новое значение.
    /// </param>
    public void SetSerialNumber(long value)
    {
        //  Выполнение в базовом потоке.
        Invoker.Primary(delegate
        {
            //  Проверка необходимости изменения значения.
            if (_ChannelInfo.SerialNumber != value)
            {
                //  Выполнение транзакции.
                Engine.ContextManager.Transaction(context =>
                {
                    //  Установка нового значения.
                    _ChannelInfo.SerialNumber = value;

                    //  Сохранение изменений.
                    context.SaveChanges();
                });

                //  Вызов события.
                OnPropertyChanged(new(nameof(SerialNumber)));
            }
        });
    }

    /// <summary>
    /// Асинхронно возвращает данные.
    /// </summary>
    /// <param name="beginTime">
    /// Время начала данных.
    /// </param>
    /// <param name="duration">
    /// Длительность данных.
    /// </param>
    /// <param name="xOffset">
    /// Смещение значений по оси Ox.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая данные.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<PolylineCollection> GetDataAsync(DateTime beginTime, TimeSpan duration,
        double xOffset, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Возврат данных фрагментов.
        return await _ChannelCache.GetDataAsync(beginTime, duration, xOffset, Color, Width, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно уведомляет о новом пакете.
    /// </summary>
    /// <param name="package">
    /// Пакет.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая уведомление.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="package"/> передана пустая ссылка.
    /// </exception>
    public async Task NotificationAsync(AdxlExtendedPackage package, CancellationToken cancellationToken)
    {
        //  Проверка пакета.
        IsNotNull(package, nameof(package));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Частота дискретизации.
        double? sampling = null;

        //  Частота среза фильтра.
        double? cutoff = null;

        //  Буфер значений.
        double[]? buffer = null;

        //  Выполнение в базовом потоке.
        await PrimaryInvokeAsync(delegate
        {
            //  Проверка серийного номера.
            if (package.DeviceProperties.SerialNumber != _ChannelInfo.SerialNumber)
            {
                //  Завершение работы с пакетом.
                return;
            }

            //  Получение частоты дискретизации.
            sampling = _ChannelInfo.Sampling;

            //  Получение частоты среза фильтра.
            cutoff = _ChannelInfo.Cutoff;

            //  Получение буфера значений.
            buffer = _ChannelInfo.ChannelType switch
            {
                ChannelType.Sync => package.DataPackage.Signals[_ChannelInfo.SignalNumber].Select(x => (double)x).ToArray(),
                ChannelType.Async => new double[] { package.DataPackage.AsyncValues[_ChannelInfo.SignalNumber] },
                ChannelType.Info => _ChannelInfo.SignalNumber switch
                {
                    0 => new double[] { package.DeviceProperties.DiagnosticValue },
                    _ => null,
                },
                _ => null,
            };

            //  Масштабирование напряжения.
            if (_ChannelInfo.ChannelType == ChannelType.Async && _ChannelInfo.SignalNumber == 2 &&
                buffer is not null && buffer.Length > 0)
            {
                buffer[0] /= 1000.0;
            }
        }, cancellationToken).ConfigureAwait(false);

        //  Проверка полученных значений.
        if (sampling is not null && cutoff is not null && buffer is not null &&
            package.BeginTime is not null && package.Duration is not null)
        {
            //  Добавление данных в коллекцию фрагментов канала.
            await _ChannelCache.AddDataAsync(sampling.Value, cutoff.Value, buffer,
                package.BeginTime.Value, package.Duration.Value, cancellationToken).ConfigureAwait(false);
        }
    }
}
