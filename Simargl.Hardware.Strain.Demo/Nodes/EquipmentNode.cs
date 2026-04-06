using Simargl.Concurrent;
using Simargl.Hardware.Strain.Demo.Core;
using Simargl.Hardware.Strain.Demo.Main;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;

namespace Simargl.Hardware.Strain.Demo.Nodes;

/// <summary>
/// Представляет узел аппаратуры.
/// </summary>
public sealed class EquipmentNode :
    Node
{
    /// <summary>
    /// Поле для хранения примитива синхронизации.
    /// </summary>
    private readonly AsyncLock _Lock;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="heart">
    /// Сердце приложения.
    /// </param>
    public EquipmentNode(Heart heart) :
        base(heart)
    {
        //  Установка имени узла.
        Name = "Датчики";

        //  Создание примитива синхронизации.
        _Lock = new();

        //  Установка изображения.
        SetImage("Equipment.ico");
    }

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
        //  Узел датчика.
        SensorNode? sensorNode = null;

        //  Перебор узлов.
        foreach (Node node in Nodes)
        {
            //  Проверка узла.
            if (node is SensorNode targetNode &&
                targetNode.Sensor.Serial == package.SerialNumber)
            {
                //  Установка узла.
                sensorNode = targetNode;

                //  Завершение поиска.
                break;
            }
        }

        //  Проверка узла.
        if (sensorNode is not null)
        {
            //  Добавление пакета данных.
            await sensorNode.Sensor.AddDataPackageAsync(package, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно регистрирует отклик от датчика.
    /// </summary>
    /// <param name="serial">
    /// Серийный номер датчика.
    /// </param>
    /// <param name="endPoint">
    /// Конечная точка подключения.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, регистрирующая отклик от датчика.
    /// </returns>
    [CLSCompliant(false)]
    public async Task AddResponseAsync(uint serial, IPEndPoint endPoint, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка конечной точки.
        if (endPoint.AddressFamily != AddressFamily.InterNetwork)
        {
            //  Неподдерживаемая конечная точка.
            return;
        }

        //  Узел датчика.
        SensorNode? sensorNode = null;

        //  Блокировка примтива синхронизации.
        using (await _Lock.LockAsync(cancellationToken))
        {
            //  Перебор узлов.
            foreach (Node node in Nodes)
            {
                //  Проверка узла.
                if (node is SensorNode targetNode &&
                    targetNode.Sensor.Serial == serial)
                {
                    //  Установка узла.
                    sensorNode = targetNode;

                    //  Завершение поиска.
                    break;
                }
            }

            //  Проверка узла.
            sensorNode ??= await Invoker.InvokeAsync(delegate
            {
                //  Создание датчика.
                Sensor sensor = new(Heart, serial, endPoint.Address);

                //  Создание узла.
                sensorNode = new(Heart, sensor);

                //  Индекс для вставки.
                int index = 0;

                //  Перебор узлов.
                while (index < Provider.Count)
                {
                    //  Проверка серийного номера.
                    if (Provider[index] is SensorNode targetNode &&
                        serial < targetNode.Sensor.Serial)
                    {
                        //  Завершение поиска.
                        break;
                    }

                    //  Смещение индекса.
                    ++index;
                }

                //  Вставка узла.
                Provider.Insert(index, sensorNode);

                //  Получение коллекции сигналов.
                ObservableCollection<Signal> signals = Heart.RootNode.Recorder.Signals;

                //  Сброс индекса.
                index = 0;

                //  Перебор сигналов.
                while (index < signals.Count)
                {
                    //  Проверка серийного номера.
                    if (serial < signals[index].Serial)
                    {
                        //  Завершение поиска.
                        break;
                    }

                    //  Смещение индекса.
                    ++index;
                }

                //  Вставка сигналов.
                for (int i = 0; i < sensor.Signals.Length; i++)
                {
                    signals.Insert(index + i, sensor.Signals[i]);
                }

                //  Возврат узла.
                return sensorNode;
            }, cancellationToken).ConfigureAwait(false);
        }

        //  Установка IP-адреса.
        await sensorNode.Sensor.SetIPAddressAsync(endPoint.Address, cancellationToken).ConfigureAwait(false);
    }
}
