using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Apeiron.ParserSensorsFiles;
using System.Net;
using System.IO;
using Path = System.IO.Path;
using System.Text.Json;

namespace PointerDataViewer
{
    /// <summary>
    /// Представляет класс основного диалога.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Представляет конфигурацию программы.
        /// </summary>
        internal static Options? Option;

        /// <summary>
        /// Представляет лист дачтчиков, данные которых берутся из файла.
        /// </summary>
        private readonly List<TeeChartSensor> _FileSensorList = new();

        /// <summary>
        /// Представляет лист дачтчиков MTP, данные которых берутся из сервера.
        /// </summary>
        private readonly List<TeeChartSensor> _MtpStreamSensorList = new();
        
        /// <summary>
        /// Представляет лист дачтчиков Adxl, данные которых берутся из сервера.
        /// </summary>
        private readonly List<TeeChartSensor> _AdxlStreamSensorList = new();

        /// <summary>
        /// Представляет класс сервера.
        /// </summary>
        private readonly SensorsTcpReceivers _Server = new(default);

        /// <summary>
        /// Инициализирует объект.
        /// </summary>
        public MainWindow()
        {
            //InitializeComponent();

            ////  Отключение 3D
            //_FileChart.Aspect.View3D = false;
            //_AdxlStreamChart.Aspect.View3D = false;
            //_MtpStreamChart.Aspect.View3D = false;

            ////  Включение автоматической перерисовки
            //_FileChart.AutoRepaint = true;
            //_MtpStreamChart.AutoRepaint = true;
            //_AdxlStreamChart.AutoRepaint = true;

            ////  Установка оси Y
            //_FileChart.Axes.Left.SetMinMax(-1.30, 1.30);
            //_MtpStreamChart.Axes.Left.SetMinMax(-1.30, 1.30);
            //_AdxlStreamChart.Axes.Left.SetMinMax(-10, 10);

            ////  Установка оси X
            //_MtpStreamChart.Axes.Bottom.SetMinMax(0, 10000);
            //_AdxlStreamChart.Axes.Bottom.SetMinMax(0, 1000);

            //  Подписываение на события объекта Server
            _Server.MtpData += MtpHook;
            _Server.AdxlData += AdxlHook;
            _Server.Error += ErrorHook;

            //  Чтение настроек из файла.
            var jsonString = Check.IsNotNull(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")),"appsettings.json");

            //  Преобразование настроек в класс
            Option = JsonSerializer.Deserialize<Options>(jsonString);

            //  Проверка что настройки получены.
            if (Option is not null)
            {
                //  Проверка что получены настройки IP MTP
                if (Option.MtpIPAddress is not null)
                {
                    //  Цикл по всем IP
                    foreach (var ip in Option.MtpIPAddress)
                    {
                        ////  Создание объекта рисования графика.
                        //TeeChartSensor mtpSensor = new(_MtpStreamChart, IPAddress.Parse(ip), MtpParser.ChannelCount, -1, 10000);

                        ////  Подписывание на событие
                        //mtpSensor.Message += ErrorHook;

                        ////  Добавление объекта в список.
                        //_MtpStreamSensorList.Add(mtpSensor);
                    }
                }

                //  Проверка что получены настройки IP Adxl
                if (Option.AdxlIPAddress is not null)
                {
                    //  Цикл по всем IP
                    foreach (var ip in Option.AdxlIPAddress)
                    {
                        ////  Создание объекта рисования графика.
                        //TeeChartSensor adxlSensor = new(_AdxlStreamChart, IPAddress.Parse(ip), 3, -1, 1000);

                        //////  Подписывание на событие
                        //adxlSensor.Message += ErrorHook;

                        ////  Добавление объекта в список.
                        //_AdxlStreamSensorList.Add(adxlSensor);
                    }
                }
            }
        }


        private void MtpHook(object? obj, DataEventArgs e)
        {
            //  Клонирование данных
            float[][] data = (float[][])e.Data.Clone();

            //  Запуск задачи
            _ = Task.Run(async () =>
            {
                //  Поиск датчика в списке зерегистрованых
                TeeChartSensor? sensor = null!;// _MtpStreamSensorList.FirstOrDefault(x => x.IP.Equals(e.SensorAddress));

                //  Проверка найден ли
                if (sensor == default)
                {
                    //  Возврат из функции.
                    return;
                }

                //  Добавление данных
                await sensor.AddDataAsync(data).ConfigureAwait(false);
            });
        }
        private void AdxlHook(object? obj, DataEventArgs e)
        {
            //  Клонирование данных
            float[][] data = (float[][])e.Data.Clone();

            //  Запуск задачи
            _ = Task.Run(async () =>
            {
                //  Поиск датчика в списке зерегистрованых
                TeeChartSensor? sensor = null!;// _AdxlStreamSensorList.FirstOrDefault(x => x.IP.Equals(e.SensorAddress));

                //  Проверка найден ли
                if (sensor == default)
                {
                    //  Возврат из функции.
                    return;
                }

                //  Добавление данных
                await sensor.AddDataAsync(data).ConfigureAwait(false);
            });
        }

        private void ErrorHook(object? obj, StringEventArgs e)
        {
            //   Вывод сообщения об ошибки.
            MessageBox.Show(e.Data);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //  Создание объекта датчика файла..
            TeeChartSensor sensor = null!;// new(_FileChart, $"folder {_FileSensorList.Count}", MtpParser.ChannelCount, TeeChartSensor._ChartMaximumCount);

            //  Добавление в список
            _FileSensorList.Add(sensor);

            //  Создание диалога
            var dialog = new Ookii.Dialogs.Wpf.VistaOpenFileDialog();

            //  Запуск диалога с проверка выполнения
            if (dialog.ShowDialog() == true)
            {
                //  Получение имени файла.
                string file = dialog.FileName;

                //  Запуск задачи
                _ = Task.Run(async () =>
                {
                    //  Чтение данных их файла
                    float[][] data = MtpParser.LoadFromFile(file);

                    //  Добавление данных
                    await sensor.AddDataAsync(data).ConfigureAwait(false);
                });
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //  Запуск сервера.
            _Server.Start();
        }
    }
}
