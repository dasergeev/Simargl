//using Hbm.Api.Common;
//using RailTest.Fibers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет базовый класс для всех объектов, реализующих устройство HBM.
//    /// </summary>
//    public abstract class HbmDevice : DeviceOld
//    {
//        /// <summary>
//        /// Возвращает объект, который предоставляет доступ ко всей системе устройств HBM.
//        /// </summary>
//        internal static DaqEnvironment DaqEnvironment { get; }

//        /// <summary>
//        /// Поле для хранения волокна для фоновой работы.
//        /// </summary>
//        private readonly Fiber _BackgroundFiber;

//        /// <summary>
//        /// Поле для хранения волокна для фоновой работы.
//        /// </summary>
//        private readonly Fiber _WorkerFiber;

//        /// <summary>
//        /// Поле для хранения средства измерения.
//        /// </summary>
//        private HbmDaqMeasurement _DaqMeasurement;

//        /// <summary>
//        /// Инициализирует статические члены.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось инициализировать среду доступа к устройствам HBM.
//        /// </exception>
//        static HbmDevice()
//        {
//            DaqEnvironment daqEnvironment = null;
//            Performing.Perform(() =>
//            {
//                daqEnvironment = DaqEnvironment.GetInstance();
//            }, "Не удалось инициализировать среду доступа к устройствам HBM.");
//            DaqEnvironment = daqEnvironment;
//        }

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="count">
//        /// Количество сигналов.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="count"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        internal HbmDevice(global::Hbm.Api.Common.Entities.Device target, int count) :
//            base(count)
//        {
//            Target = target;
//            Connectors = new HbmConnectorCollection(this);
//            Channels = new HbmChannelCollection(Connectors);
//            _BackgroundFiber = new Fiber(BackgroundEntryPoint);
//            _WorkerFiber = new Fiber(WorkerEntryPoint);
//            Connected += (object sender, EventArgs e) => _BackgroundFiber.Start();
//            Disconnected += (object sender, EventArgs e) => _BackgroundFiber.Stop(250);
//            Started += (object sender, EventArgs e) => _WorkerFiber.Start();
//            Stopped += (object sender, EventArgs e) => _WorkerFiber.Stop(250);
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal global::Hbm.Api.Common.Entities.Device Target { get; }

//        /// <summary>
//        /// Возвращает коллекцию разъёмов.
//        /// </summary>
//        public HbmConnectorCollection Connectors { get; }

//        /// <summary>
//        /// Возвращает коллекцию каналов.
//        /// </summary>
//        public HbmChannelCollection Channels { get; }

//        /// <summary>
//        /// Подключает устройство.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось подключить устройство.
//        /// </exception>
//        protected override void CoreConnect()
//        {
//            try
//            {
//                Performing.Perform(DaqEnvironment.Connect, Target, "Не удалось подключить устройство.");
//                Connectors.Connect();
//            }
//            catch
//            {
//                try
//                {
//                    CoreDisconnect();
//                }
//                catch
//                {
                    
//                }
//                throw;
//            }
//        }

//        /// <summary>
//        /// Отключает устройство.
//        /// </summary>
//        protected override void CoreDisconnect()
//        {
//            try
//            {
//                Connectors.Disconnect();
//            }
//            catch
//            {

//            }
//            Performing.Perform(() =>
//            {
//                DaqEnvironment.Disconnect(Target);
//            }, "Не удалось отключить устройство.");
//        }

//        /// <summary>
//        /// Запускает устройство.
//        /// </summary>
//        protected override void CoreStart()
//        {
//            _DaqMeasurement = new HbmDaqMeasurement(100, 500);
//            foreach (var signal in Signals)
//            {
//                if (signal.IsActive)
//                {
//                    _DaqMeasurement.Add(signal);
//                }
//            }
//            _DaqMeasurement.Prepare();
//            _DaqMeasurement.Start(HbmDataAcquisitionMode.Auto);
//        }

//        /// <summary>
//        /// Останавливает устройство.
//        /// </summary>
//        protected override void CoreStop()
//        {
//            if (_DaqMeasurement is object)
//            {
//                if (_DaqMeasurement.IsRunning)
//                {
//                    _DaqMeasurement.Stop();
//                }
//                _DaqMeasurement.Dispose();
//                _DaqMeasurement = null;
//            }
//        }

//        /// <summary>
//        /// Загружает доступные сигналы.
//        /// </summary>
//        /// <returns>
//        /// Коллекция доступных сигналов.
//        /// </returns>
//        protected override IList<SignalOld> CoreLoadSignals()
//        {
//            List<SignalOld> signals = new List<SignalOld>();
//            foreach (var channel in Channels)
//            {
//                signals.Add(channel.Signal);
//            }
//            return signals;
//        }

//        /// <summary>
//        /// Поле для хранения точки входа волокна для фоновой работы.
//        /// </summary>
//        /// <param name="context">
//        /// Контекст волокна.
//        /// </param>
//        private void BackgroundEntryPoint(FiberContext context)
//        {
//            while (context.IsWork)
//            {
//                try
//                {
//                    Performing.Perform(() =>
//                    {
//                        Target.ReadSingleMeasurementValueOfAllSignals();
//                    });
//                    foreach (var signal in Signals)
//                    {
//                        if (signal is HbmSignal hbmSignal)
//                        {
//                            hbmSignal.UpdateCurrentValue();
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Exception exception = ex;
//                    while (ex is object)
//                    {
//                        if (ex is ThreadAbortException)
//                        {
//                            exception = null;
//                            break;
//                        }
//                        ex = ex.InnerException;
//                    }
//                    if (exception is object)
//                    {
//                        throw exception;
//                    }
//                }
//                Thread.Sleep(100);
//            }
//        }

//        /// <summary>
//        /// Поле для хранения точки входа волокна для фоновой работы.
//        /// </summary>
//        /// <param name="context">
//        /// Контекст волокна.
//        /// </param>
//        private void WorkerEntryPoint(FiberContext context)
//        {
//            while (context.IsWork)
//            {
//                try
//                {
//                    _DaqMeasurement.FillMeasurementValues();
//                    foreach (var signal in Signals)
//                    {
//                        if (signal is HbmSignal hbmSignal)
//                        {
//                            if (signal.IsActive)
//                            {
//                                hbmSignal.UpdateResponse(_DaqMeasurement.MeasurementStartSystemTime);
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Exception exception = ex;
//                    while (ex is object)
//                    {
//                        if (ex is ThreadAbortException)
//                        {
//                            exception = null;
//                            break;
//                        }
//                        ex = ex.InnerException;
//                    }
//                    if (exception is object)
//                    {
//                        throw exception;
//                    }
//                }
//                Thread.Sleep(100);
//            }
//        }

//        /// <summary>
//        /// Определяет значение, которое будет использоваться вместо недопустимого значения измерения (переполнение). Для каждого значения измерения, состояние которого является недействительным, соответствующее значение измерения будет установлено на это значение переполнения. Используйте Hbm.Api.Common.DaqEnvironment.Overflow, чтобы установить это значение (по умолчанию 1000000).
//        /// </summary>
//        public static double Overflow
//        {
//            get
//            {
//                double value = double.NaN;
//                Performing.Perform(() =>
//                {
//                    value = global::Hbm.Api.Common.Entities.Device.Overflow;
//                }, "Не удалось прочитать значение, которое используется вместо недопустимого значения измерения.");
//                return value;
//            }
//        }

//        /// <summary>
//        /// Назначает новое имя каналу.
//        /// </summary>
//        /// <param name="channel">
//        /// Канал, которому необходимо назначить новое имя.
//        /// </param>
//        /// <param name="name">
//        /// Новое имя.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="channel"/> передана пуста ссылка.
//        /// </exception>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="name"/> передана пуста ссылка.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        internal void SetChannelName(HbmChannel channel, string name)
//        {
//            if (channel is null)
//            {
//                throw new ArgumentNullException("channel", "Передана пустая ссылка.");
//            }
//            if (name is null)
//            {
//                throw new ArgumentNullException("name", "Передана пустая ссылка.");
//            }
//            Performing.Perform(Target.SetChannelName, channel.Target, name, "Не удалось изменить имя канала.");
//        }

//        /// <summary>
//        /// Назначает настройки данного канала (включая датчик, сигналы и т.д.) физическому каналу устройства.
//        /// </summary>
//        /// <param name="channel">
//        /// Существующий (модифицированный) канал.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="channel"/> передана пуста ссылка.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        internal void AssignChannel(HbmChannel channel)
//        {
//            if (channel is null)
//            {
//                throw new ArgumentNullException("channel", "Передана пустая ссылка.");
//            }
//            Performing.Perform(Target.AssignChannel, channel.Target, "Не удалось настроить канал.");
//        }

//        /// <summary>
//        /// Назначает настройки данного разъема (включая каналы, датчик, сигналы и т.д.) физическому разъему устройства.
//        /// </summary>
//        /// <param name="connector">
//        /// Существующий (модифицированный) соединитель.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="connector"/> передана пуста ссылка.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        internal void AssignConnector(HbmConnector connector)
//        {
//            if (connector is null)
//            {
//                throw new ArgumentNullException("connector", "Передана пустая ссылка.");
//            }
//            Performing.Perform(Target.AssignConnector, connector.Target, "Не удалось настроить разъём.");
//        }

//        /// <summary>
//        /// Назначает настройки данного сигнала физическому сигналу устройства.
//        /// </summary>
//        /// <param name="signal">
//        /// Существующий (модифицированный) сигнал.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="signal"/> передана пуста ссылка.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        internal void AssignSignal(HbmSignal signal)
//        {
//            if (signal is null)
//            {
//                throw new ArgumentNullException("signal", "Передана пустая ссылка.");
//            }
//            Performing.Perform(Target.AssignSignal, signal.Target, "Не удалось настроить сигнал.");
//        }

//        /// <summary>
//        /// Выполняет балансировку нуля.
//        /// </summary>
//        /// <param name="channel">
//        /// Канал, для которого необходимо выполнить балансировку нуля.
//        /// </param>
//        /// <param name="values">
//        /// Количество последовательных измерений (> 0, по умолчанию равно 1) для использования при нулевой балансировке (усреднение).
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="channel"/> передана пуста ссылка.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="values"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        internal void SetZeroBalance(HbmChannel channel, int values)
//        {
//            bool сoreSetZeroBalance(
//                global::Hbm.Api.Common.Entities.Channels.Channel coreChannel, 
//                int coreNumberOfMeasurementValuesToUse, 
//                out List<global::Hbm.Api.Common.Entities.Problems.Problem> problems)
//            {
//                return Target.SetZeroBalance(coreChannel, out problems, coreNumberOfMeasurementValuesToUse);
//            }
//            if (channel is null)
//            {
//                throw new ArgumentNullException("channel", "Передана пустая ссылка.");
//            }
//            if (values < 1)
//            {
//                throw new ArgumentOutOfRangeException("values", "Передано отрицательное или равное нулю значение.");
//            }
//            Performing.Perform(сoreSetZeroBalance, channel.Target, values, "Не удалось выполнить балансировку канала.");
//        }

//        ////
//        //// Сводка:
//        ////     Assigns the sensor settings of the given channel to the physical channel of the
//        ////     device. Warnings and errors during assign process are collected in list of problems.
//        ////
//        //// Параметры:
//        ////   channel:
//        ////     Existing (modified) channel
//        ////
//        ////   problems:
//        ////     Warnings and errors that occurred during assign process
//        ////
//        //// Возврат:
//        ////     true if no error has occurred, otherwise false
//        //public abstract bool AssignSensor(Channel channel, out List<Problem> problems);
//    }
//}
