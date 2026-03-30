
using Simargl.Hardware.Strain.Demo.Main;
using Simargl.Hardware.Strain.Demo.Nodes;
using System.Windows;

namespace Simargl.Hardware.Strain.Demo.UI.Views;

/// <summary>
/// Представляет элемент управления, отображающий оборудование.
/// </summary>
partial class EquipmentView
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public EquipmentView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// Возвращает целевой тип узла.
    /// </summary>
    public override Type TargetNodeType { get; } = typeof(EquipmentNode);

    /// <summary>
    /// Обрабатывает событие копирования информации в буфер обмена.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void CopyButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Создание построителя строки.
            StringBuilder builder = new();

            //  Получение узла.
            if (TargetNode is EquipmentNode equipmentNode)
            {
                //  Перебор дочерних узлов.
                foreach (Node node in equipmentNode.Nodes)
                {
                    //  Проверка узла.
                    if (node is SensorNode sensorNode)
                    {
                        //  Получение датчика.
                        Sensor sensor = sensorNode.Sensor;

                        //  Добавление информации.
                        builder.Append($"{sensor.Serial:X8}");
                        builder.Append('\t');
                        builder.Append($"{sensor.Address}");
                        builder.AppendLine();
                    }
                }
            }

            //  Получение текста.
            string text = builder.ToString();

            //  Отправка в буфер обмена.
            Clipboard.SetText(text);

            //  Вывод в журнал.
            Journal.Add("Информация скопирована в буфер обмена.");
        }
        catch
        {
            //  Вывод в журнал.
            Journal.Add("Не удалось скопировать информацию в буфер обмена.");
        }
    }
}
