using Simargl.Concurrent;
using Simargl.Hardware.Strain.Demo.Core;
using Simargl.Hardware.Strain.Demo.Microservices;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media.Imaging;

namespace Simargl.Hardware.Strain.Demo.Main;

/// <summary>
/// Представляет сигнал данных.
/// </summary>
public sealed class Signal :
    Anything,
    INotifyPropertyChanged
{
    /// <summary>
    /// Поле для хранения карты изображений.
    /// </summary>
    private static readonly ConcurrentDictionary<string, BitmapImage> _ImageMap = [];

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Примитив синхронизации.
    /// </summary>
    public AsyncLock Lock { get; } = new();

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Signal(long serial, int index)
    {
        //  Обращение к объекту.
        Lay();

        Serial = serial;
        Index = index;

        Name = $"{serial:X8} - {index + 1}";

        //  Установка изображения.
        _Image = GetImage("BallRed.ico");

        Series = new();

        _ColorImage = ChartManager.CreateSolidColorBitmap(32, 32, Color.Black);

        IsChecked = false;
       

        BindProperties();
    }

    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public System.Windows.Forms.DataVisualization.Charting.Series Series { get; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime MoveTime { get; set; } = DateTime.Now;

    //public 

    /// <summary>
    /// Возвращает серийный номер.
    /// </summary>
    public long Serial { get; }

    /// <summary>
    /// Возвращает индекс сигнала в датчике.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Возвращает имя сигнала.
    /// </summary>
    public string Name { get; }


    private DateTime _FastMoveTime = DateTime.Now;

    /// <summary>
    /// Асинхронно добавляет пакет данных.
    /// </summary>
    /// <param name="package">
    /// Пакет данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, добавляющая пакет данных.
    /// </returns>
    public async Task AddDataPackageAsync(DataPackage package, CancellationToken cancellationToken)
    {
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        using AsyncLocking locking = await Lock.LockAsync(cancellationToken);

        DateTime now = DateTime.Now;
        double offset = (now - _FastMoveTime).TotalSeconds;
        _FastMoveTime = now;
        foreach (var point in Points)
        {
            point.XValue -= offset;
        }


        float[] values = package.Data[Index];
        double sampling = package.Sampling;
        double xStepBase = 1 / sampling;

        double xStep = offset / values.Length;
        if (xStep > 2 * xStepBase)
        {
            xStep = xStepBase;
        }

        double xBegin = -(values.Length - 1) * xStep;
        var points = Series.Points;



        for (int i = 0; i < values.Length; i++)
        {
            Points.Add(new(xBegin + i * xStep, values[i]));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public List<DataPoint> Points { get; } = [];




















    private bool _IsConnected;
    /// <summary/>
    public bool IsConnected
    {
        get => _IsConnected;
        set => SetProperty(ref _IsConnected, value);
    }

    private bool _IsVisible;
    /// <summary/>
    public bool IsVisible
    {
        get => _IsVisible;
        set => SetProperty(ref _IsVisible, value);
    }

    private BitmapImage _Image;
    /// <summary/>
    public BitmapImage Image
    {
        get => _Image;
        set => SetProperty(ref _Image, value);
    }

    private BitmapSource _ColorImage;
    /// <summary/>
    public BitmapSource ColorImage
    {
        get => _ColorImage;
        set => SetProperty(ref _ColorImage, value);
    }

    private bool? _IsChecked;
    /// <summary/>
    public bool? IsChecked
    {
        get => _IsChecked;
        set => SetProperty(ref _IsChecked, value);
    }


    //

    private void BindProperties()
    {
        SetBind(nameof(IsConnected), delegate
        {
            Image = IsConnected ? GetImage("BallGreen.ico") : GetImage("BallRed.ico");
        });
        SetBind(nameof(IsChecked), delegate
        {
            Series.Enabled = IsChecked ?? false;
        });
    }

    private void SetBind(string name, Action action)
    {
        PropertyChanged += delegate (object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == name)
            {
                action();
            }
        };

        action();
    }

    /// <summary>
    /// Устанавливает значение свойства.
    /// </summary>
    private void SetProperty<T>(ref T source, T value, [CallerMemberName] string callerName = null!)
    {
        if (!EqualityComparer<T>.Default.Equals(source, value))
        {
            source = value;
            Volatile.Read(ref PropertyChanged)?.Invoke(this, new(callerName));
        }
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

}
