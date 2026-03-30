using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using Steema.TeeChart.Functions;

//using Steema.TeeChart.Styles;
using Microsoft.VisualStudio.Threading;
//using Steema.TeeChart.WPF;
using System.Drawing;

using Apeiron.Analysis;

namespace PointerDataViewer
{
    /// <summary>
    /// Представляет класс управления TeeChart.
    /// </summary>
    internal class TeeChartSensor
    {

        /// <summary>
        /// Представляет объект синхронизации.
        /// </summary>
        private static readonly object _SyncRoot = new();

        /// <summary>
        /// Объект выполнения задачи в потоке диалога
        /// </summary>
        private static readonly JoinableTaskFactory _JoinableTaskFactory = new(new JoinableTaskContext());

        /// <summary>
        /// Представляет набор цветов для палитры 16 каналов.
        /// </summary>
        private static readonly Color[] Colors = new Color[16]
        {
            Color.FromArgb(255, 0, 0),
            Color.FromArgb(0, 255, 0),
            Color.FromArgb(0, 0, 255),
            Color.FromArgb(170, 0, 0),
            Color.FromArgb(0, 170, 0),
            Color.FromArgb(0, 0, 170),
            Color.FromArgb(100, 0, 0),
            Color.FromArgb(0, 100, 0),
            Color.FromArgb(0, 0, 100),
            Color.FromArgb(50, 0, 0),
            Color.FromArgb(0, 50, 0),
            Color.FromArgb(0, 0, 50),
            Color.FromArgb(100, 100, 0),
            Color.FromArgb(0, 100, 100),
            Color.FromArgb(100, 0, 100),
            Color.FromArgb(0, 0, 0),
        };

        ///// <summary>
        ///// Представляет ссылку на контрол графика 1
        ///// </summary>
        //private readonly Steema.TeeChart.WPF.TChart _ChartControl1;

        ///// <summary>
        ///// Представляет ссылку на контрол графика 2
        ///// </summary>
        //private readonly Steema.TeeChart.WPF.TChart _ChartControl2;

        ///// <summary>
        ///// Представляет ссылку на контрол графика 3
        ///// </summary>
        //private readonly Steema.TeeChart.WPF.TChart _ChartControl3;

        ///// <summary>
        ///// Представляет ссылку на контрол графика 3
        ///// </summary>
        //private readonly Steema.TeeChart.WPF.TChart _ChartControl4;

        ///// <summary>
        ///// Представляет класс исходных линий.
        ///// </summary>
        //private readonly FastLine _SourceLines1;

        ///// <summary>
        ///// Представляет класс исходных линий.
        ///// </summary>
        //private readonly FastLine _SourceLines2;

        ///// <summary>
        ///// Представляет класс исходных линий.
        ///// </summary>
        //private readonly FastLine _SourceLines3;

        ///// <summary>
        ///// Представляет класс исходных линий.
        ///// </summary>
        //private readonly FastLine _SourceLines4;

        /// <summary>
        /// Представляет количество каналов датчика.
        /// </summary>
        private long _AddedCount;

        /// <summary>
        /// Представляет серийный номер датчика.
        /// </summary>
        internal uint SerialNumber { get; init; }

        /// <summary>
        /// Представляет событие информирования класса.
        /// </summary>
        internal event EventHandler<StringEventArgs>? Message;

        ///// <summary>
        ///// Представляет количество каналов датчика.
        ///// </summary>
        //private readonly int _StreamCount;

        /// <summary>
        /// Представляет список пакетов данных.
        /// </summary>
        private List<float[][]> _Buffer = new();

        ///// <summary>
        ///// Инициализирует объект
        ///// </summary>
        ///// <param name="chartControl1">Контрол графика</param>
        ///// <param name="chartControl2">Контрол графика</param>
        ///// <param name="chartControl3">Контрол графика</param>
        ///// <param name="chartControl4">Контрол графика</param>
        ///// <param name="serialNumber">Серийный номер.</param>
        ///// <param name="count">Количество точек в графике.</param>
        ///// <exception cref="InvalidOperationException">Не поддерживаемое количество каналов.</exception>
        //internal TeeChartSensor(
        //    [ParameterNoChecks] TChart chartControl1, 
        //    [ParameterNoChecks] TChart chartControl2,
        //    [ParameterNoChecks] TChart chartControl3, 
        //    [ParameterNoChecks] TChart chartControl4, 
        //    [ParameterNoChecks] uint serialNumber,
        //    [ParameterNoChecks] int count)
        //{
        //    //  Количество точек в графике
        //    _StreamCount = count;

        //    //  Иницаилизация ссылки на контрол.
        //    _ChartControl1 = chartControl1;

        //    //  Иницаилизация ссылки на контрол.
        //    _ChartControl2 = chartControl2;

        //    //  Иницаилизация ссылки на контрол.
        //    _ChartControl3 = chartControl3;

        //    //  Иницаилизация ссылки на контрол.
        //    _ChartControl4 = chartControl4;

        //    //  Серийный номер датчика
        //    SerialNumber = serialNumber;

        //    //  Установка буфера.
        //    _AddedCount = 0;

        //    //  Создание исходной линии.
        //    _SourceLines1 = new FastLine()
        //    {
        //        Title = "Channel 1",
        //        Color = Colors[0],
        //        DrawAllPoints = false,
        //        TreatNulls = TreatNullsStyle.DoNotPaint,
        //        AutoRepaint = true,
        //        Visible = true,
        //    };

        //    //  Создание исходной линии.
        //    _SourceLines2 = new FastLine()
        //    {
        //        Title = "Channel 2",
        //        Color = Colors[1],
        //        DrawAllPoints = false,
        //        TreatNulls = TreatNullsStyle.DoNotPaint,
        //        AutoRepaint = true,
        //        Visible = true,
        //    };

        //    //  Создание исходной линии.
        //    _SourceLines3 = new FastLine()
        //    {
        //        Title = "Channel 3",
        //        Color = Colors[2],
        //        DrawAllPoints = false,
        //        TreatNulls = TreatNullsStyle.DoNotPaint,
        //        AutoRepaint = true,
        //        Visible = true,
        //    };

        //    //  Создание исходной линии.
        //    _SourceLines4 = new FastLine()
        //    {
        //        Title = "Channel 4",
        //        Color = Colors[3],
        //        DrawAllPoints = false,
        //        TreatNulls = TreatNullsStyle.DoNotPaint,
        //        AutoRepaint = true,
        //        Visible = true,
        //    };


        //    lock (_SyncRoot)
        //    {
        //        //   Добваление линии в серию.
        //        _ChartControl1.Series.Add(_SourceLines1);

        //        //   Добваление линии в серию.
        //        _ChartControl2.Series.Add(_SourceLines2);

        //        //   Добваление линии в серию.
        //        _ChartControl3.Series.Add(_SourceLines3);

        //        //   Добваление линии в серию.
        //        _ChartControl4.Series.Add(_SourceLines4);
        //    }
        //}
        /// <summary>
        /// Представляет функцию перерасчета линий.
        /// </summary>
        /// <returns>Возвращает задачу.</returns>
        internal async Task AddDataAsync([ParameterNoChecks] float[][] data)
        {

            if (_Buffer.Count < 5)
            {
                _Buffer.Add(data);
                return;
            }


            await _JoinableTaskFactory.SwitchToMainThreadAsync(default);
            
            try
            {
                ////  Подготовка обновления
                //_SourceLines1.BeginUpdate();

                ////  Подготовка обновления
                //_SourceLines2.BeginUpdate();

                ////  Подготовка обновления
                //_SourceLines3.BeginUpdate();

                ////  Подготовка обновления
                //_SourceLines4.BeginUpdate();

                foreach (var item in _Buffer)
                {
                    var x = Enumerable.Range((int)_AddedCount, (int)data[0].Length);

                    _AddedCount += item[0].Length;

                    if (item.Length > 0)
                    {
                        ////  Добавление точек
                        //_SourceLines1.Add(x.ToArray(), item[0], true);

                        ////  Проверка что добавлено больше точек чем допустимо и что режим работы - поток.
                        //if (_AddedCount > _StreamCount && _StreamCount > 0)
                        //{
                        //    //  Удаление устаревших точек.
                        //    _SourceLines1.Delete((int)_SourceLines1.XValues.First, item[0].Length, true);
                        //}

                    }

                    if (item.Length > 1)
                    {
                        ////  Добавление точек
                        //_SourceLines2.Add(x.ToArray(), item[1], true);

                        ////  Проверка что добавлено больше точек чем допустимо и что режим работы - поток.
                        //if (_AddedCount > _StreamCount && _StreamCount > 0)
                        //{
                        //    //  Удаление устаревших точек.
                        //    _SourceLines2.Delete((int)_SourceLines2.XValues.First, item[1].Length, true);
                        //}
                    }

                    if (item.Length > 2)
                    {
                        ////  Добавление точек
                        //_SourceLines3.Add(x.ToArray(), item[2], true);

                        ////  Проверка что добавлено больше точек чем допустимо и что режим работы - поток.
                        //if (_AddedCount > _StreamCount && _StreamCount > 0)
                        //{
                        //    //  Удаление устаревших точек.
                        //    _SourceLines3.Delete((int)_SourceLines3.XValues.First, item[2].Length, true);
                        //}
                    }

                    if (item.Length > 3)
                    {

                        ////  Добавление точек
                        //_SourceLines4.Add(x.ToArray(), item[3], true);

                        ////  Проверка что добавлено больше точек чем допустимо и что режим работы - поток.
                        //if (_AddedCount > _StreamCount && _StreamCount > 0)
                        //{
                        //    //  Удаление устаревших точек.
                        //    _SourceLines4.Delete((int)_SourceLines4.XValues.First, item[3].Length, true);
                        //}

                    }
                }

                ////  Обновление линии
                //_SourceLines1.RefreshSeries();

                ////  Завершение операции.
                //_SourceLines1.EndUpdate();

                ////  Обновление линии
                //_SourceLines2.RefreshSeries();

                ////  Завершение операции.
                //_SourceLines2.EndUpdate();

                ////  Обновление линии
                //_SourceLines3.RefreshSeries();

                ////  Завершение операции.
                //_SourceLines3.EndUpdate();

                ////  Обновление линии
                //_SourceLines4.RefreshSeries();

                ////  Завершение операции.
                //_SourceLines4.EndUpdate();

                ////  Перерисовка графика.
                //_ChartControl1.Invalidate();

                ////  Перерисовка графика.
                //_ChartControl2.Invalidate();

                ////  Перерисовка графика.
                //_ChartControl3.Invalidate();

                ////  Перерисовка графика.
                //_ChartControl4.Invalidate();

                _Buffer.Clear();
            }
            catch (Exception ex)
            {
                //  Вызов события.
                Message?.Invoke(this, new(ex.Message));
            }

        }
    }

}
