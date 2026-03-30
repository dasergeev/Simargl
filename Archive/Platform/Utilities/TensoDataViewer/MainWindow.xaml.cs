using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.IO;
using Path = System.IO.Path;
using System.Text.Json;
using SixLabors.ImageSharp;
using Microsoft.VisualStudio.Threading;

namespace PointerDataViewer
{
    /// <summary>
    /// Представляет класс основного диалога.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Объект выполнения задачи в потоке диалога
        /// </summary>
        private static readonly JoinableTaskFactory _JoinableTaskFactory = new(new JoinableTaskContext());

        /// <summary>
        /// Представляет конфигурацию программы.
        /// </summary>
        internal static Options? Option;
        
        /// <summary>
        /// Представляет лист дачтчиков Adxl, данные которых берутся из сервера.
        /// </summary>
        private readonly List<TeeChartSensor> _TensoStreamSensorList = new();

        /// <summary>
        /// Представляет класс сервера.
        /// </summary>
        private readonly SensorsTcpReceivers _Server = new(default);


        /// <summary>
        /// Инициализирует объект.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //  Отключение 3D
            _TensoChannel1.Aspect.View3D = false;
            _TensoChannel2.Aspect.View3D = false;
            _TensoChannel3.Aspect.View3D = false;
            _TensoChannel4.Aspect.View3D = false;

            //  Включение автоматической перерисовки
            _TensoChannel1.AutoRepaint = true;
            _TensoChannel2.AutoRepaint = true;
            _TensoChannel3.AutoRepaint = true;
            _TensoChannel4.AutoRepaint = true;


            //  Установка оси X
            _TensoChannel1.Axes.Bottom.SetMinMax(0, 10000);
            _TensoChannel2.Axes.Bottom.SetMinMax(0, 10000);
            _TensoChannel3.Axes.Bottom.SetMinMax(0, 10000);
            _TensoChannel4.Axes.Bottom.SetMinMax(0, 10000);

            //  Подписываение на события объекта Server
            _Server.TensoData += TensoHook;
            _Server.Connected += TensoConnectHook;
            _Server.Error += ErrorHook;

            //  Чтение настроек из файла.
            var jsonString = Check.IsNotNull(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")),"appsettings.json");

            //  Преобразование настроек в класс
            Option = JsonSerializer.Deserialize<Options>(jsonString);
           
        }

        private void TensoConnectHook(object? obj, DataEventArgs e)
        {

            //  Запуск задачи
            _ = Task.Run(async () =>
            {
                //  Инициализация сенсора.
                TeeChartSensor? sensor = default;

                await _JoinableTaskFactory.SwitchToMainThreadAsync(default);

                lock (_TensoStreamSensorList)
                {
                    //  Поиск датчика в списке зерегистрованых
                    sensor = _TensoStreamSensorList.FirstOrDefault(x => x.SerialNumber.Equals(e.SerialNumber));

                    //  Проверка найден ли
                    if (sensor == default)
                    {
                        //  Создание объекта рисования графика.
                        sensor = new(_TensoChannel1, _TensoChannel2, _TensoChannel3, _TensoChannel4, e.SerialNumber, 10000);

                        //  Подписывание на событие
                        sensor.Message += ErrorHook;

                        //  Добавление объекта в список.
                        _TensoStreamSensorList.Add(sensor);
                    }
                }
            });
        }

        private void TensoHook(object? obj, DataEventArgs e)
        {

            //  Запуск задачи
            _ = Task.Run(async () =>
            {
                //  Поиск датчика в списке зерегистрованых
                TeeChartSensor? sensor = _TensoStreamSensorList.FirstOrDefault(x => x.SerialNumber.Equals(e.SerialNumber));

                //  Проверка найден ли
                if (sensor != default)
                {
                    //  Добавление данных
                    await sensor.AddDataAsync(e.Data).ConfigureAwait(false);
                }
            });
        }

        private void ErrorHook(object? obj, StringEventArgs e)
        {
            //   Вывод сообщения об ошибки.
            MessageBox.Show(e.Data);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //  Запуск сервера.
            _Server.Start();
        }
    }
}
