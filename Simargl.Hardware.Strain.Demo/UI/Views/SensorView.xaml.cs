using Simargl.Hardware.Strain.Demo.Main;
using Simargl.Hardware.Strain.Demo.Nodes;
using System.Windows;

namespace Simargl.Hardware.Strain.Demo.UI.Views;

/// <summary>
/// Представляет элемент управления отображающий датчик.
/// </summary>
partial class SensorView
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public SensorView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// Возвращает целевой тип узла.
    /// </summary>
    public override Type TargetNodeType { get; } = typeof(SensorNode);

    /// <summary>
    /// Обрабатывает событие нажати на кнопку перезагрузки датчика.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void RebootButton_Click(object sender, RoutedEventArgs e)
    {
        //  Получение целевого узла.
        if (TargetNode is SensorNode sensorNode)
        {
            //  Получение датчика.
            Sensor sensor = sensorNode.Sensor;

            //  Получение токена отмены.
            CancellationToken cancellationToken = Application.CancellationToken;

            //  Асинхронное выполнение.
            _ = Task.Run(async delegate
            {
                //  Отправка запроса на перезагрузку.
                await sensor.RebootAsync(cancellationToken).ConfigureAwait(false);
            });
        }
    }
}
