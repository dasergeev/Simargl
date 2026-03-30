using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Apeiron.Platform.Sensors;
using Microsoft.VisualStudio.Threading;

namespace AdxlToolWindow
{
    /// <summary>
    /// Представляет клас основного диалога.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Объект выполнения задачи в потоке диалога
        /// </summary>
        private static readonly JoinableTaskFactory _JoinableTaskFactory = new(new JoinableTaskContext());

        /// <summary>
        /// Объект синхронизации списка
        /// </summary>
        private readonly object _SyncListOfSensorWpf = new();

        /// <summary>
        /// Объект синхронизации списка
        /// </summary>
        private readonly object _SyncListOfParamsWpf = new();

        /// <summary>
        /// Представляет интерфейс обращения по Modbus к датчикам ADXL.
        /// </summary>
        internal AdxlManager Manager { get; set; } = new(default);

        /// <summary>
        /// Представляет список параметров датчика.
        /// </summary>
        internal ObservableCollection<PropertyViewer> TargetParams { get; } = new ObservableCollection<PropertyViewer>();

        /// <summary>
        /// Возвращает интерфейс modbus для выбранного датчика.
        /// </summary>
        internal AdxlModbus? TargetDevice { get; private set; }

        /// <summary>
        /// Представляет ссылку на выбранный элемент.
        /// </summary>
        internal RadioButton? TargetRadioButton;

        /// <summary>
        /// Представляет байт маски сканирования.
        /// </summary>
        public string IPMask1 { get; set; } = "192";
        
        /// <summary>
        /// Представляет байт маски сканирования.
        /// </summary>
        public string IPMask2 { get; set; } = "168";

        /// <summary>
        /// Представляет байт маски сканирования.
        /// </summary>
        public string IPMask3 { get; set; } = "1";


        /// <summary>
        /// Инициализирует объект.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //Создание объекта синхронизации для списка
            BindingOperations.EnableCollectionSynchronization(Manager.ScanedList ,_SyncListOfSensorWpf);

            //Создание объекта синхронизации для списка
            BindingOperations.EnableCollectionSynchronization(TargetParams, _SyncListOfParamsWpf);

            //  Привязка списка.
            _AdxlDevicesListBox.ItemsSource = Manager.ScanedList;

            //  Привязка списка.
            _ParamsListBox.ItemsSource = TargetParams;

            //  Привязка байта маски
            _Mask1TextBox.DataContext = this;

            //  Привязка байта маски
            _Mask2TextBox.DataContext = this;

            //  Привязка байта маски
            _Mask3TextBox.DataContext = this;


            //  Установка состояния диалога.
            OnDeSelectItem();
        }

        /// <summary>
        /// Представляет обработчик события установки RadioButton
        /// </summary>
        /// <param name="sender">
        /// Отправитель
        /// </param>
        /// <param name="e">
        /// Аргемент события.
        /// </param>
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {

            //Получение объекта RadioButton
            var radioButton = sender as RadioButton;

            //Проверка что объект существует
            if (radioButton is not null)
            {
                //  Присвоение ссылки.
                TargetRadioButton = radioButton;

                //Проверка что к обекту привязанно содержимое
                if (radioButton.Content is not null)
                {
                    //Получение содержимого
                    string? identifer = radioButton.Content.ToString();
                    if (identifer is not null)
                    {
                        //Поиск нового устройства соответствующего содержимому RadioButton
                        var targetDevice = Manager.ScanedList.FirstOrDefault(x=>x.Address.Equals(System.Net.IPAddress.Parse(identifer)));

                        //Проверка что устройство найдено
                        if (targetDevice != default)
                        {
                            //  Очистка списка.
                            TargetParams.Clear();
                            
                            //Установка выбранного устройства
                            TargetDevice = targetDevice;

                            //  Получение всех свойств.
                            var infos = AdxlSensor.GetAllProperty().OrderBy(x=>x.Name); 

                            //  Цикл по всем свойствам.
                            foreach (var info in infos)
                            {
                                TargetParams.Add(new(TargetDevice, info));
                            }

                            //  Привязка серийного номера.
                            _SerialNumberTextBox.DataContext = TargetDevice;

                            //  Привязка даты производства.
                            _DateTextBox.DataContext = TargetDevice;

                            //  Привязка версии прошивки.
                            _VersionTextBox.DataContext = TargetDevice;

                            //Реакция приложения на выбранное устройство
                            OnSelectItem();
                        }
                    }
                }

            }
        }
        /// <summary>
        /// Представляет реакцию системы если сенсор выбран и готов к операциям.
        /// </summary>
        private void OnSelectItem()
        {
            //Проверка что устройство доступно
            if (TargetDevice is not null)
            {
                //Запуск задачи в потоке диалога
                _ = Task.Run(async () =>
                {
                    //Ожидание потока диалога
                    await _JoinableTaskFactory.SwitchToMainThreadAsync();
                    try
                    {
                        //  Включение кнопок.
                        _WriteButton.IsEnabled = true;
                        _ReadButton.IsEnabled = true;
                        _ClearButton.IsEnabled = true;
                        _StartButton.IsEnabled = true;
                        _StopButton.IsEnabled = true;
                    }
                    catch (Exception ex)
                    {
                        //  Вывод сообщения об ошибки.
                        MessageBox.Show(ex.Message);
                    }

                });
            }
        }

        /// <summary>
        /// Представляет реакцию системы если сенсор выбран и готов к операциям.
        /// </summary>
        private void OnDeSelectItem()
        {
            //  Сброс выбранного устройства.
            TargetDevice = null;

            //  Очистка списка.
            TargetParams.Clear();

            //Запуск задачи в потоке диалога
            _ = Task.Run(async () =>
            {
                //Ожидание потока диалога
                await _JoinableTaskFactory.SwitchToMainThreadAsync();
                try
                {
                    //  Выключение кнопок.
                    _WriteButton.IsEnabled = false;
                    _ReadButton.IsEnabled = false;
                    _ClearButton.IsEnabled = false;
                    _StartButton.IsEnabled = false;
                    _StopButton.IsEnabled = false;

                    //  Проверка что выбран элемент
                    if (TargetRadioButton is not null)
                    {
                        //  Сброс выбора.
                        TargetRadioButton.IsChecked = false;
                    }
                }
                catch (Exception ex)
                {
                    //  Вывод сообщения об ошибки.
                    MessageBox.Show(ex.Message);
                }
            });
        }


        /// <summary>
        /// Представляет обработчик события нажатия кнопки Write
        /// </summary>
        /// <param name="sender">
        /// Отправитель
        /// </param>
        /// <param name="e">
        /// Аргемент события.
        /// </param>
        private void WriteButton_Click(object sender, RoutedEventArgs e)
        {
            //  Сохранение текста кнопки.
            string content = (string)_WriteButton.Content;

            //  Установка нового текста.
            _WriteButton.Content = "processing";

            //  Выключение кнопки.
            _WriteButton.IsEnabled = false;

            //  Запуск задачи
            _ = Task.Run(async () =>
            {
                try
                {
                    //  Проверка что существует целевое устройство
                    if (TargetDevice is not null)
                    {
                        //  Сохранение данных
                        await TargetDevice.UpdateAsync(default).ConfigureAwait(false);

                        //  Перезагрузка устройства.
                        await TargetDevice.ResetAsync(default).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    //  Вывод сообщения об ошибки.
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //  Переключение в поток диалога.
                    await _JoinableTaskFactory.SwitchToMainThreadAsync();

                    //  Изменение текста кнопки
                    _WriteButton.Content = content;

                    //  Перезагрузка списка
                    RefreshButton_Click(this, new());
                }
            });
            
        }

        /// <summary>
        /// Представляет обработчик события нажатия кнопки Clear
        /// </summary>
        /// <param name="sender">
        /// Отправитель
        /// </param>
        /// <param name="e">
        /// Аргемент события.
        /// </param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            //  Сохранение текста кнопки.
            string content = (string)_ClearButton.Content;

            //  Установка нового текста.
            _ClearButton.Content = "processing";

            //  Выключение кнопки.
            _ClearButton.IsEnabled = false;

            //  Запуск задачи
            _ = Task.Run(async () =>
            {
                try
                {
                    //  Проверка что существует целевое устройство
                    if (TargetDevice is not null)
                    {
                        //  Очистка напряжения
                        await TargetDevice.ClearVoltageAsync(default).ConfigureAwait(false);    

                        //  Очистка темературы
                        await TargetDevice.ClearTempAsync(default).ConfigureAwait(false);

                        //  Очистка ошибок
                        await TargetDevice.ClearErrorAsync(default).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    //  Вывод сообщения об ошибки.
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //  Переключение в поток диалога.
                    await _JoinableTaskFactory.SwitchToMainThreadAsync();

                    //  Изменение текста кнопки
                    _ClearButton.Content = content;

                    //  Выключение кнопки.
                    _ClearButton.IsEnabled = true;

                    //  Сброс источника
                    _ParamsListBox.ItemsSource = null;

                    //  Установка источника.
                    _ParamsListBox.ItemsSource = TargetParams;
                }
            });
        }


        /// <summary>
        /// Представляет обработчик события нажатия кнопки Refresh
        /// </summary>
        /// <param name="sender">
        /// Отправитель
        /// </param>
        /// <param name="e">
        /// Аргемент события.
        /// </param>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            //  Сохранение текста кнопки.
            string content = (string)_ScanButton.Content;

            //  Установка нового текста.
            _ScanButton.Content = "In processing...";

            //  Выключение кнопки.
            _ScanButton.IsEnabled = false;

            //  Выключение поля ввода
            _Mask1TextBox.IsEnabled = false;
            
            //  Выключение поля ввода
            _Mask2TextBox.IsEnabled = false;
            
            //  Выключение поля ввода
            _Mask3TextBox.IsEnabled = false;

            //  Выполнения сценария сброса выбора
            OnDeSelectItem();

            //  Запуск задачи
            _ = Task.Run(async () =>
            {
                try
                {
                    //  Создание маски
                    string mask = $"{IPMask1}.{IPMask2}.{IPMask3}.";

                    //  Выполнение операции сканирования
                    await Manager.ScanAsync(mask).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    //  Вывод сообщения об ошибки.
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //  Переключение в поток диалога.
                    await _JoinableTaskFactory.SwitchToMainThreadAsync();

                    //  Изменение текста кнопки
                    _ScanButton.Content = content;

                    //  Включение кнопки.
                    _ScanButton.IsEnabled = true;

                    //  Включение поля ввода
                    _Mask1TextBox.IsEnabled = true;

                    //  Включение поля ввода
                    _Mask2TextBox.IsEnabled = true;

                    //  Включение поля ввода
                    _Mask3TextBox.IsEnabled = true;
                }
            });
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //  Сохранение текста кнопки.
            string content = (string)_StartButton.Content;

            //  Установка нового текста.
            _StartButton.Content = "processing";

            //  Выключение кнопки.
            _StartButton.IsEnabled = false;

            //  Запуск задачи
            _ = Task.Run(async () =>
            {
                try
                {
                    //  Проверка что существует целевое устройство
                    if (TargetDevice is not null)
                    {
                        //  Запуск датчика
                        await TargetDevice.StartAsync(default).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    //  Вывод сообщения об ошибки.
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //  Переключение в поток диалога.
                    await _JoinableTaskFactory.SwitchToMainThreadAsync();

                    //  Изменение текста кнопки
                    _StartButton.Content = content;

                    //  Выключение кнопки.
                    _StartButton.IsEnabled = true;

                    //  Сброс источника
                    _ParamsListBox.ItemsSource = null;

                    //  Установка источника.
                    _ParamsListBox.ItemsSource = TargetParams;
                }
            });
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {

            //  Сохранение текста кнопки.
            string content = (string)_StopButton.Content;

            //  Установка нового текста.
            _StopButton.Content = "processing";

            //  Выключение кнопки.
            _StopButton.IsEnabled = false;

            //  Запуск задачи
            _ = Task.Run(async () =>
            {
                try
                {
                    //  Проверка что существует целевое устройство
                    if (TargetDevice is not null)
                    {
                        //  Запуск датчика
                        await TargetDevice.StopAsync(default).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    //  Вывод сообщения об ошибки.
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //  Переключение в поток диалога.
                    await _JoinableTaskFactory.SwitchToMainThreadAsync();

                    //  Изменение текста кнопки
                    _StopButton.Content = content;

                    //  Выключение кнопки.
                    _StopButton.IsEnabled = true;

                    //  Сброс источника
                    _ParamsListBox.ItemsSource = null;

                    //  Установка источника.
                    _ParamsListBox.ItemsSource = TargetParams;
                }
            });
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            //  Сохранение текста кнопки.
            string content = (string)_ReadButton.Content;

            //  Установка нового текста.
            _ReadButton.Content = "processing";

            //  Выключение кнопки.
            _ReadButton.IsEnabled = false;

            //  Запуск задачи
            _ = Task.Run(async () =>
            {
                try
                {
                    //  Проверка что существует целевое устройство
                    if (TargetDevice is not null)
                    {
                        //  Обновление данных
                        await TargetDevice.LoadAsync(default).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    //  Вывод сообщения об ошибки.
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //  Переключение в поток диалога.
                    await _JoinableTaskFactory.SwitchToMainThreadAsync();

                    //  Изменение текста кнопки
                    _ReadButton.Content = content;

                    //  Выключение кнопки.
                    _ReadButton.IsEnabled = true;

                    //  Сброс источника
                    _ParamsListBox.ItemsSource = null;

                    //  Установка источника.
                    _ParamsListBox.ItemsSource = TargetParams;
                }
            });
        }

        private new void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //  Создание списка символов
            Regex regex = new(@"^\d+");

            //  Установка результата проверки
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void NumberValidationTextBox(object sender, TextChangedEventArgs e)
        {
            //  Получение объекта
            TextBox? box = sender as TextBox;

            //  Проверка на пустую ссылку
            if (box is not null)
            {
                //  Инициализация значения

                //  Проверка символа.
                bool result = int.TryParse(box.Text, out int number);

                //  Проверка значения
                if ((result == true && (number > 255 || number == 0)))
                {
                    //  Установка значения
                    box.Text = "255";
                }
            }
        }
    }
}
