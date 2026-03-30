using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//using Steema.TeeChart.Functions;

//using Steema.TeeChart.Styles;
using Microsoft.VisualStudio.Threading;
//using Steema.TeeChart.WPF;
using System.Drawing;

namespace PointerDataViewer
{
    /// <summary>
    /// Представляет класс управления TeeChart.
    /// </summary>
    internal class TeeChartSensor
    {
        /// <summary>
        /// Представляет количество точек в линии.
        /// </summary>
        public const int _ChartMaximumCount = 10000;

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
        ///// Представляет класс исходных линий.
        ///// </summary>
        //private readonly FastLine[] _SourceLines;

        ///// <summary>
        ///// Представляет класс уменьшенных линий.
        ///// </summary>
        //private readonly FastLine[] _ReducedLines;

        ///// <summary>
        ///// Представляет инструмент уменьшения количество значений данных.
        ///// </summary>
        //private readonly DownSampling[] _Reducers;

        ///// <summary>
        ///// Представляет ссылку на контрол графика
        ///// </summary>
        //private readonly TChart _ChartControl;

        ///// <summary>
        ///// Представляет количество уменьшеных точек датчика.
        ///// </summary>
        //private readonly int _ReduceCount;

        ///// <summary>
        ///// Представляет количество каналов датчика.
        ///// </summary>
        //private readonly int _StreamCount;

        /// <summary>
        /// Представляет количество каналов датчика.
        /// </summary>
        private long _AddedCount;

        ///// <summary>
        ///// Представляет IP адресс датчика.
        ///// </summary>
        //internal IPAddress IP { get; init; }

        /// <summary>
        /// Представляет количество каналов датчика.
        /// </summary>
        internal byte ChannelCount { get; init; }

        /// <summary>
        /// Представляет событие информирования класса.
        /// </summary>
        internal event EventHandler<StringEventArgs>? Message;



        ///// <summary>
        ///// Инициализирует объект
        ///// </summary>
        ///// <param name="chartControl">Контрол графика</param>
        ///// <param name="ip">IP адрес датчика.</param>
        ///// <param name="countOfChannel">Количество каналов.</param>
        ///// <param name="reduceCount">Количество значений уменьшеного графика. (отрацательное значение выключает функцию)</param>
        ///// <param name="streamCount">Количество значений графика потока. (отрацательное значение выключает функцию)</param>
        //internal TeeChartSensor([ParameterNoChecks] TChart chartControl, [ParameterNoChecks] IPAddress ip, byte countOfChannel, [ParameterNoChecks] int reduceCount, [ParameterNoChecks] int streamCount)
        //{
        //    //  Иницаилизация ссылки на контрол.
        //    _ChartControl = chartControl;

        //    //  IP адрес датчика.
        //    IP = ip;

        //    //  Количество каналов датчика.
        //    ChannelCount = countOfChannel;

        //    //  Инициализация количества уменшения значений.
        //    _ReduceCount = reduceCount;

        //    //  Инициализация количества значений потока.
        //    _StreamCount = streamCount;

        //    //  Инициализация на линии источников.
        //    _SourceLines = new FastLine[ChannelCount];

        //    //  Инициализация на линии уменшения
        //    _ReducedLines = new FastLine[ChannelCount];

        //    //  Инициализация на линии уменьшенителя.
        //    _Reducers = new DownSampling[ChannelCount];

        //    //  Цикл по всем каналам.
        //    for (uint i = 0; i < ChannelCount; i++)
        //    {
        //        //  Создание объекта уменшения.
        //        _Reducers[i] = new DownSampling(_ChartControl.Chart)
        //        {
        //            Method = DownSamplingMethod.MinMaxFirstLastNull,
        //            DisplayedPointCount = _ReduceCount,
        //            Tolerance = 10,
        //        };

        //        //  Создание исходной линии.
        //        _SourceLines[i] = new FastLine()
        //        {
        //            Title = $"Sensor: {IP.MapToIPv4()}; Channel: {i}",
        //            Color = Colors[i],
        //            DrawAllPoints = false,
        //            TreatNulls = TreatNullsStyle.DoNotPaint,
        //            AutoRepaint = true,
        //            Visible = true,
        //        };

        //        //  Создание уменьшенной линии.
        //        _ReducedLines[i] = new FastLine()
        //        {
        //            Title = $"Sensor: {IP.MapToIPv4()}; Channel: {i}",
        //            Color = Colors[i],
        //            DrawAllPoints = false,
        //            // TreatNulls = TreatNullsStyle.DoNotPaint,
        //            AutoRepaint = true,
        //            Visible = true,
        //        };

        //        lock (_SyncRoot)
        //        {
        //            //  Если требуется уменьшение.
        //            if (_ReduceCount > 0)
        //            {
        //                //   Добваление линии в серию.
        //                _ChartControl.Series.Add(_ReducedLines[i]);

        //            }
        //            else
        //            {
        //                //   Добваление линии в серию.
        //                _ChartControl.Series.Add(_SourceLines[i]);
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// Инициализирует объект
        ///// </summary>
        ///// <param name="chartControl">Контрол графика</param>
        ///// <param name="name">Псевдони дачика.</param>
        ///// <param name="countOfChannel">Количество каналов.</param>
        ///// <param name="reduceCount">Количество значений уменьшеного графика. (отрацательное значение выключает функцию)</param>
        //internal TeeChartSensor([ParameterNoChecks] TChart chartControl, [ParameterNoChecks] string name, byte countOfChannel, [ParameterNoChecks] int reduceCount)
        //{
        //    //  Иницаилизация ссылки на контрол.
        //    _ChartControl = chartControl;

        //    //  IP адрес датчика.
        //    IP = IPAddress.Loopback;

        //    //  Количество каналов датчика.
        //    ChannelCount = countOfChannel;

        //    //  Инициализация количества уменшения значений.
        //    _ReduceCount = reduceCount;

        //    //  Инициализация количества  значений потока.
        //    _StreamCount = -1;

        //    //  Инициализация на линии источников.
        //    _SourceLines = new FastLine[ChannelCount];

        //    //  Инициализация на линии уменшения
        //    _ReducedLines = new FastLine[ChannelCount];

        //    //  Инициализация на линии уменьшенителя.
        //    _Reducers = new DownSampling[ChannelCount];

        //    //  Цикл по всем каналам.
        //    for (uint i = 0; i < ChannelCount; i++)
        //    {
        //        //  Создание объекта уменшения.
        //        _Reducers[i] = new DownSampling(_ChartControl.Chart)
        //        {
        //            Method = DownSamplingMethod.MinMaxFirstLastNull,
        //            DisplayedPointCount = _ReduceCount,
        //            Tolerance = 10,
        //        };

        //        //  Создание исходной линии.
        //        _SourceLines[i] = new FastLine()
        //        {
        //            Title = $"File: {name}; Channel: {i}",
        //            Color = Colors[i],
        //            DrawAllPoints = false,
        //            TreatNulls = TreatNullsStyle.DoNotPaint,
        //            AutoRepaint = true,
        //            Visible = true,
        //        };

        //        //  Создание уменьшенной линии.
        //        _ReducedLines[i] = new FastLine()
        //        {
        //            Title = $"File: {name}; Channel: {i}",
        //            Color = Colors[i],
        //            DrawAllPoints = false,
        //            Function = _Reducers[i],
        //            DataSource = _SourceLines[i],
        //        };

        //        //  Если требуется уменьшение.
        //        if (_ReduceCount > 0)
        //        {
        //            //   Добваление линии в серию.
        //            _ChartControl.Series.Add(_ReducedLines[i]);
        //        }
        //        else
        //        {
        //            //   Добваление линии в серию.
        //            _ChartControl.Series.Add(_SourceLines[i]);
        //        }
        //    }
        //}

        /// <summary>
        /// Представляет функцию перерасчета линий.
        /// </summary>
        /// <returns>Возвращает задачу.</returns>
        internal async Task AddDataAsync([ParameterNoChecks] float[][] data)
        {
            await _JoinableTaskFactory.SwitchToMainThreadAsync(default);

            try
            {
                lock (_SyncRoot)
                {
                    //  Инициализация счетчика.
                    int count = ChannelCount > data.Length ? data.Length : ChannelCount;

                    var x = Enumerable.Range((int)_AddedCount, (int)data[0].Length);

                    _AddedCount += data[0].Length;

                    //  Цикл по всем данным.
                    for (int i = 0; i < count; i++)
                    {
                        //  Создание ссылки вектора.
                        float[] vector = data[i];

                        ////  Получение линии.
                        //FastLine sourceLine = _SourceLines[i];

                        ////  Получение линии.
                        //FastLine reducedLine = _ReducedLines[i];

                        ////  Получение линии.
                        //DownSampling down = _Reducers[i];

                        ////  Цикл по всем линиям.
                        //foreach (var item in _ChartControl.Series)
                        //{
                        //    //  Если требуется уменьшение точек
                        //    if (_ReduceCount > 0)
                        //    {
                        //        //  Проверка что линия этого класса.
                        //        if (reducedLine.Equals(item))
                        //        {
                        //            //  Подготовка обновления
                        //            item.BeginUpdate();

                        //            //  Добавление точек
                        //            item.Add(x.ToArray(),vector,true);

                        //            //  Проверка что добавлено больше точек чем допустимо и что режим работы - поток.
                        //            if (_AddedCount > _StreamCount && _StreamCount > 0)
                        //            {
                        //                //  Удаление устаревших точек.
                        //                item.Delete((int)item.XValues.First, vector.Length, true);

                        //            }

                        //            //  Обновление линии
                        //            item.RefreshSeries();

                        //            //  Завершение операции.
                        //            item.EndUpdate();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        //  Проверка что линия этого класса.
                        //        if (sourceLine.Equals(item))
                        //        {
                        //            //  Подготовка обновления
                        //            item.BeginUpdate();

                        //            //  Добавление точек
                        //            item.Add(x.ToArray(), vector, true);

                        //            //  Проверка что добавлено больше точек чем допустимо и что режим работы - поток.
                        //            if (_AddedCount > _StreamCount && _StreamCount > 0)
                        //            {
                        //                //  Удаление устаревших точек.
                        //                item.Delete((int)item.XValues.First, vector.Length, true);

                        //            }

                        //            //  Обновление линии
                        //            item.RefreshSeries();

                        //            //  Завершение операции.
                        //            item.EndUpdate();
                        //        }
                        //    }

                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                //  Вызов события.
                Message?.Invoke(this,new(ex.Message));
            }

            ////  Перерисовка графика.
            //_ChartControl.Invalidate();
        }
    }
}
