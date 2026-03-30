using Apeiron.Platform.Demo.AdxlDemo.Adxl;
using Apeiron.Platform.Demo.AdxlDemo.Nodes;
using System.Collections;

namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет элемент управления для настройки датчика.
/// </summary>
public partial class DeviceView
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public DeviceView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// Асинхронно выполняет новый отсчёт.
    /// </summary>
    /// <param name="node">
    /// Текущий узел.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполнящая новый отсчёт.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task TickAsync(INode node, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Определение источника настроек.
        IEnumerable itemsSource = null!;

        //  Получение устройства.
        if (node is AdxlDevice device)
        {
            //  Установка источника настроек.
            itemsSource = device.Parameters;
        }

        //  Выполнение в основном потоке.
        Invoker.Primary(delegate
        {
            //  Проверка необходимости изменения источника.
            if (!ReferenceEquals(_ListView.ItemsSource, itemsSource))
            {
                //  Установка нового источника.
                _ListView.ItemsSource = itemsSource;
            }
        });
    }

    /// <summary>
    /// Обрабатывает событие загрузки текущих настроек.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Load(object sender, RoutedEventArgs e)
    {
        //  Получение устройства.
        if (Node is AdxlDevice device)
        {
            //  Перебор параметров.
            foreach (AdxlDeviceParameter parameter in device.Parameters)
            {
                //  Установка текущего свойства.
                parameter.Value = parameter.ActiveValue;
            }
        }
    }

    /// <summary>
    /// Обрабатывает событие установки текущих настроек.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Save(object sender, RoutedEventArgs e)
    {
        //  Получение устройства.
        if (Node is AdxlDevice device)
        {
            //  Запуск асинхронной задачи.
            _ = Task.Run(async delegate
            {
                //  Безопасное выполнение.
                await Invoker.CriticalAsync(async cancellationToken =>
                {
                    //  Проверка токена отмены.
                    await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Перебор параметров.
                    foreach (AdxlDeviceParameter parameter in device.Parameters)
                    {
                        //  Безопасное выполнение.
                        Invoker.Critical(delegate
                        {
                            //  Сохранение значения в датчик.
                            parameter.Save();
                        });
                    }

                    //  Перезагрузка дачтка.
                    await device.RebootAsync(cancellationToken).ConfigureAwait(false);
                }, Engine.CancellationToken).ConfigureAwait(false);
            });
        }

    }
}
