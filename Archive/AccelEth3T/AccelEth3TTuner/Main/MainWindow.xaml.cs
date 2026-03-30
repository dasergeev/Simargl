using Simargl.Designing;
using Simargl.Embedded.AccelEth3T;
using Simargl.Embedded.Modbus;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет главное окно приложения.
/// </summary>
partial class MainWindow
{
    /// <summary>
    /// Поле для хранения сетки свойств.
    /// </summary>
    private readonly PropertyGrid _PropertyGrid;

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private CancellationTokenSource? _CancellationTokenSource = null;

    /// <summary>
    /// Поле для хранения действий, выполняемых в основном потоке.
    /// </summary>
    private readonly ConcurrentQueue<Action> _Actions = [];

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public MainWindow()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Создание сетки свойств.
        _PropertyGrid = new()
        {
            Dock = DockStyle.Fill,
        };

        //  Добавление сетки свойств.
        _Host.Child = _PropertyGrid;

        //  Настройка узлов.
        _TreeView.ItemsSource = App.RootNode.Nodes;

        //  Создание таймера для выполнения действий в основном потоке.
        DispatcherTimer timer = new()
        {
            Interval = TimeSpan.FromMilliseconds(100),
        };

        //  Добавление обработчика события таймера.
        timer.Tick += (sender, e) =>
        {
            //  Извлечение действий из очереди.
            while (_Actions.TryDequeue(out Action? action))
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Выполнение действия.
                    action();
                }
                catch { }
            }
        };

        //  Запуск таймера.
        timer.Start();
    }

    /// <summary>
    /// Обрабатывает событие изменения выбора элемента.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        //  Установка нового значения в сетку свойств.
        _PropertyGrid.SelectedObject = e.NewValue;

        //  Установка устройства.
        _DeviceView.SetDeivce(e.NewValue as AdxlDevice);
    }

    /// <summary>
    /// Происходит при запуске поиска.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Start_Click(object sender, RoutedEventArgs e)
    {
        //  Смена режима кнопок.
        _StartButton.Visibility = Visibility.Collapsed;
        _StopButton.Visibility = Visibility.Visible;

        //  Создание источника токена отмены.
        _CancellationTokenSource = new();

        //  Запуск асинхронной задачи.
        _ = Task.Run(async delegate
        {
            //  Сканирование.
            await ScanAsync(_CancellationTokenSource.Token).ConfigureAwait(false);
        });
    }

    /// <summary>
    /// Происходит при остановке поиска.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Stop_Click(object sender, RoutedEventArgs e)
    {
        //  Отмена задачи сканирования.
        _CancellationTokenSource?.CancelAfter(0);

        //  Сброс источника токена отмены.
        _CancellationTokenSource = null;
    }

    /// <summary>
    /// Асинхронно выполняет сканирование.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сканирование.
    /// </returns>
    private async Task ScanAsync(CancellationToken cancellationToken)
    {
        //  Очистка окна вывода.
        Clear();

        //  Вывод информации.
        WriteLine("Начало сканирования.");

        //  Номер датчика.
        int index = 0;

        //  Выполнение в основном потоке.
        _Actions.Enqueue(App.RootNode.NetNode.Nodes.Clear);

        //  Блок перехвата всех исключений.
        try
        {
            //  Получение диапазона IP-адресов.
            IPAddress firstAddress = IPAddress.Parse(App.RootNode.NetNode.FirstAddress);
            IPAddress lastAddress = IPAddress.Parse(App.RootNode.NetNode.LastAddress);

            //  Поиск соединений.
            AccelEth3TConnect[] connects = await AccelEth3TConnect.ScanAsync(
                firstAddress, lastAddress,
                App.RootNode.NetNode.Timeout, cancellationToken).ConfigureAwait(false);

            //  Перебор соединений.
            foreach (AccelEth3TConnect connect in connects)
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Получение серийного номера.
                    uint serialNumber = await connect.ReadSerialNumberAsync(cancellationToken).ConfigureAwait(false);

                    //  Создание датчика.
                    AdxlDevice device = new(serialNumber, connect, _Actions.Enqueue);

                    //  Обновление информации о датчике.
                    await device.UpdateAsync(cancellationToken).ConfigureAwait(false);

                    //  Выполнение в основном потоке.
                    _Actions.Enqueue(delegate
                    {
                        //  Добавление узла.
                        App.RootNode.NetNode.Nodes.Add(device);
                    });

                    //  Вывод информации.
                    WriteLine($"{++index}\t{connect.Address}\t{serialNumber:X8}");
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Вывод информации.
                WriteLine($"Ошибка при сканировании: {ex}");
            }
        }

        //  Вывод информации.
        WriteLine("Завершение сканирования.");

        //  Выполнение в основном потоке.
        _Actions.Enqueue(delegate
        {
            //  Смена режима кнопок.
            _StartButton.Visibility = Visibility.Visible;
            _StopButton.Visibility = Visibility.Collapsed;
        });
    }

    /// <summary>
    /// Очищает окно вывода.
    /// </summary>
    private void Clear()
    {
        //  Выполнение в основном потоке.
        _Actions.Enqueue(delegate
        {
            //  Сброс текста.
            _TextBox.Text = string.Empty;
        });
    }

    /// <summary>
    /// Выводит текст в окно вывода.
    /// </summary>
    /// <param name="text">
    /// Текст, который необходимо вывести.
    /// </param>
    private void WriteLine(string text)
    {
        //  Выполнение в основном потоке.
        _Actions.Enqueue(delegate
        {
            //  Создание построителя текста.
            StringBuilder builder = new(_TextBox.Text);

            //  Добавление нового текста.
            builder.AppendLine(text);

            //  Установка нового текста.
            _TextBox.Text = builder.ToString();

            //  Прокрутка.
            _TextBox.ScrollToEnd();
        });
    }
}
