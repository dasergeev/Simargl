using Simargl.Hardware.Strain.Demo.Main;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Simargl.Hardware.Strain.Demo.Nodes;

/// <summary>
/// Прдеставляет узел датчика.
/// </summary>
public sealed class SensorNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="heart">
    /// Сердце приложения.
    /// </param>
    /// <param name="sensor">
    /// Датчик.
    /// </param>
    public SensorNode(Heart heart, Sensor sensor) :
        base(heart)
    {
        //  Установка датчика.
        Sensor = sensor;

        //  Обновление имени.
        UpdateName();

        //  Обновление изображения.
        UpdateImage();

        //  Добавление обработчика события.
        Sensor.AddressChanged += delegate (object? sender, EventArgs e)
        {
            //  Обновление имени.
            UpdateName();
        };

        //  Добавление обработчика события.
        Sensor.PropertyChanged += delegate (object? sender, PropertyChangedEventArgs e)
        {
            //  Проверка имени.
            if (e.PropertyName == nameof(Sensor.IsConnected) ||
                e.PropertyName == nameof(Sensor.IsRegistration))
            {
                //  Обновление изображения.
                UpdateImage();
            }
        };
    }

    /// <summary>
    /// Возвращает датчик.
    /// </summary>
    public Sensor Sensor { get; }

    /// <summary>
    /// Обновляет имя узла.
    /// </summary>
    private void UpdateName()
    {
        //  Установка имени узла.
        //Name = $"{Sensor.Serial:X8} - {Sensor.Address}";
        Name = $"{Sensor.Serial:X8}";
    }

    /// <summary>
    /// Обновляет изображение.
    /// </summary>
    private void UpdateImage()
    {
        //  Проверка подключения.
        if (Sensor.IsConnected)
        {
            //  Проверка регистрации.
            if (Sensor.IsRegistration)
            {
                //  Установка изображения.
                SetImage("BallGreen.ico");
            }
            else
            {
                //  Установка изображения.
                SetImage("BallYellow.ico");
            }
        }
        else
        {
            //  Установка изображения.
            SetImage("BallRed.ico");
        }
    }
}
