//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет средство сбора данных.
//    /// Класс, который управляет сбором данных. Используйте этот класс для выполнения непрерывного измерения. Поэтому следует придерживаться следующего рабочего процесса: * Сканирование устройств (Hbm.Api.Common.DaqEnvironment.ScanScan) * Подключение к устройству (ам) (Hbm.Api.Common.DaqEnvironment.Connect (System.Collections.Generic.List { Hbm.Api.Common.Entities.Device}, System.Collections.Generic.List {Hbm.Api.Common.Entities.Problems.Problem} @) Connect) * Добавить / удалить сигналы для / из измерения (Hbm.Api.Common .DaqMeasurement.AddSignals (Hbm.Api.Common.Entities.Device, System.Collections.Generic.List {Hbm.Api.Common.Entities.Signals.Signal}), Hbm.Api.Common.DaqMeasurement.RembApi .Common.Entities.Device, System.Collections.Generic.List {Hbm.Api.Common.Entities.Signals.Signal})) * Подготовка сбора данных (Hbm.Api.Common.DaqMeasurement.PrepareDaq (System.Int32, System. Int32, System.Int32, System.Boolean, System.Boolean, System.Int32, System.Boolean)) * Запуск сбора данных (Hbm.Api.Common.DaqMeasurement.StartDaq (Hbm.Api.Common.Enums.DataAcquisitionMode, System. Int32)) * Loop * Получить значения измерения (Hbm.Api.Common.DaqMe asurement.FillMeasurementValues ​​(System.Boolean)) * Что-то сделать с этими значениями * Конец цикла * Остановить сбор данных (Hbm.Api.Common.DaqMeasurement.StopDaq) После того, как устройство было Hbm.Api.Common.DaqEnvironment.Connect (Система. Collections.Generic.List {Hbm.Api.Common.Entities.Device}, System.Collections.Generic.List {Hbm.Api.Common.Entities.Problems.Problem} @) подключен (и подготовлен к вашей задаче измерения - например, Device.AssignConnector, вы можете выбрать сигналы устройства, которое вы хотите измерить. Каждый из этих сигналов должен быть добавлен к измерению. Вызовите Hbm.Api.Common.DaqMeasurement.PrepareDaq (System.Int32, System.Int32, System.Int32, System.Boolean, System.Boolean, System.Int32, System.Boolean), чтобы позволить API выделять внутренние циклические буферы сигналы и дальнейшая внутренняя подготовка, например, сортировка сигналов в разные группы для разных частот дискретизации и т. д. Чтобы начать измерение, необходимо вызвать Hbm.Api.Common.DaqMeasurement.StartDaq (Hbm.Api.Common.Enums.DataAcquisitionMode, System.Int32. ). Если вы используете более одного устройства, и устройства синхронизируются (это означает, что системное время на устройствах довольно одинаковое), вы должны начать сбор данных с помощью Hbm.Api.Common.Enums.DataAcquisitionModeDataAcquisitionModeDataAcquisitionMode.TimestampSynchronized. В случае синхронизированного измерения первая временная метка каждого сигнала в идеале должна быть одинаковой (или, по крайней мере, как можно ближе друг к другу, насколько это возможно). Чтобы получить значения измерений, периодически должна вызываться функция Hbm.Api.Common.DaqMeasurement.FillMeasurementValues ​​(System.Boolean) FillMeasurementValues. До следующего вызова этой функции вы можете получить доступ к значениям измерения в разделе Hbm.Api.Common.Entities.Signals.Signal.ContinuousMeasurementValuesSignal.ContinuousMeasurementValues ​​\ из сигналов, добавленных в измерение. Если измерение было начато синхронизировано, гарантируется, что каждый сигнал с одинаковой частотой дискретизации (распределенный по различным устройствам) получит одинаковое количество новых значений измерения. Hbm.Api.Common.DaqMeasurement.StopDaq завершает текущее измерение и удаляет все ранее добавленные сигналы из измерения. Так что имейте в виду, чтобы добавить сигналы снова перед подготовкой / началом нового измерения.
//    /// Class that manages data acquisition. Use this class to execute a continuous measurement. Therefore you should keep with following work flow: * Scan for devices (Hbm.Api.Common.DaqEnvironment.ScanScan) * Connect to device(s) (Hbm.Api.Common.DaqEnvironment.Connect(System.Collections.Generic.List{Hbm.Api.Common.Entities.Device},System.Collections.Generic.List{Hbm.Api.Common.Entities.Problems.Problem}@)Connect) * Add/Remove signals to/from measurement (Hbm.Api.Common.DaqMeasurement.AddSignals(Hbm.Api.Common.Entities.Device,System.Collections.Generic.List{Hbm.Api.Common.Entities.Signals.Signal}), Hbm.Api.Common.DaqMeasurement.RemoveSignals(Hbm.Api.Common.Entities.Device,System.Collections.Generic.List{Hbm.Api.Common.Entities.Signals.Signal})) * Prepare data acquisition (Hbm.Api.Common.DaqMeasurement.PrepareDaq(System.Int32,System.Int32,System.Int32,System.Boolean,System.Boolean,System.Int32,System.Boolean)) * Start data acquisition (Hbm.Api.Common.DaqMeasurement.StartDaq(Hbm.Api.Common.Enums.DataAcquisitionMode,System.Int32)) * Loop * Get measurement values(Hbm.Api.Common.DaqMeasurement.FillMeasurementValues(System.Boolean)) * Do something with these values * Loop end * Stop data acquisition (Hbm.Api.Common.DaqMeasurement.StopDaq) After a device has been Hbm.Api.Common.DaqEnvironment.Connect(System.Collections.Generic.List{Hbm.Api.Common.Entities.Device},System.Collections.Generic.List{Hbm.Api.Common.Entities.Problems.Problem}@)connected (and prepared to your measurement task - e.g. by Device.AssignConnector, you can choose signals of the device you want to measure. Each of these signals has to be added to the measurement. Call Hbm.Api.Common.DaqMeasurement.PrepareDaq(System.Int32,System.Int32,System.Int32,System.Boolean,System.Boolean,System.Int32,System.Boolean) to let the API allocate the internal circular buffers of the signals and do further internal preparations like sorting signals into different groups for different sample rates etc. To start the measurement, you have to call Hbm.Api.Common.DaqMeasurement.StartDaq(Hbm.Api.Common.Enums.DataAcquisitionMode,System.Int32). If you use more than one device and the devices are synchronized (that means that the system time on the devices is pretty equal), you should start the data acquisition with Hbm.Api.Common.Enums.DataAcquisitionModeDataAcquisitionMode.TimestampSynchronized. In case of a synchronized measurement, the first timestamp of each signal will ideally be the same (or at least as close to each other as possible). To get measurement values, the function Hbm.Api.Common.DaqMeasurement.FillMeasurementValues(System.Boolean)FillMeasurementValues has to be called periodically. Until the next call to this function, you can access the measurement values under Hbm.Api.Common.Entities.Signals.Signal.ContinuousMeasurementValuesSignal.ContinuousMeasurementValues \of the signals you added to the measurement. If the measurement has been started synchronized, it is guaranteed, that each signal with same sample rate (distributed to various devices) gets the same number of new measurement values. Hbm.Api.Common.DaqMeasurement.StopDaq ends a running measurement and removes all, prior added signals from the measurement. So keep in mind to add the signals again before preparing/starting a new measurement.
//    /// </summary>
//    internal class HbmDaqMeasurement : IDisposable
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="target"/> передана пустая ссылка.
//        /// </exception>
//        internal HbmDaqMeasurement(global::Hbm.Api.Common.DaqMeasurement target)
//        {
//            Target = target ?? throw new ArgumentNullException("target", "Передана пустая ссылка.");
//        }

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось создать средство сбора данных.
//        /// </exception>
//        public HbmDaqMeasurement() :
//            this(CreateTarget())
//        {

//        }

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="fetchingInterval">
//        /// Интервал времени в мс, в котором данные измерений запрашиваются со всех устройств, которые принимают участие в сеансе измерений.
//        /// </param>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось создать средство сбора данных.
//        /// </exception>
//        public HbmDaqMeasurement(int fetchingInterval) :
//            this(fetchingInterval, 2000)
//        {

//        }

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="fetchingInterval">
//        /// Интервал времени в мс, в котором данные измерений запрашиваются со всех устройств, которые принимают участие в сеансе измерений.
//        /// </param>
//        /// <param name="transmissionTime">
//        /// Максимальное время в мс, которое может занять передача данных измерения, 
//        /// режде чем сигнал будет исключен из измерения (из-за того, что не получены значения измерения от устройства).
//        /// </param>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось создать средство сбора данных.
//        /// </exception>
//        public HbmDaqMeasurement(int fetchingInterval, int transmissionTime) :
//            this(CreateTarget(fetchingInterval, transmissionTime))
//        {

//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal global::Hbm.Api.Common.DaqMeasurement Target { get; }

//        /// <summary>
//        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
//        /// </summary>
//        public void Dispose()
//        {
//            ((IDisposable)Target).Dispose();
//        }

//        /// <summary>
//        /// Создаёт новый целевой объект.
//        /// </summary>
//        /// <returns>
//        /// Новый целевой объект.
//        /// </returns>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось создать средство сбора данных.
//        /// </exception>
//        private static global::Hbm.Api.Common.DaqMeasurement CreateTarget()
//        {
//            return Performing.Perform(() => new global::Hbm.Api.Common.DaqMeasurement(),
//                "Не удалось создать средство сбора данных.");
//        }

//        /// <summary>
//        /// Создаёт новый целевой объект.
//        /// </summary>
//        /// <param name="fetchingInterval">
//        /// Интервал времени в мс, в котором данные измерений запрашиваются со всех устройств, которые принимают участие в сеансе измерений.
//        /// </param>
//        /// <param name="transmissionTime">
//        /// Максимальное время в мс, которое может занять передача данных измерения, 
//        /// режде чем сигнал будет исключен из измерения (из-за того, что не получены значения измерения от устройства).
//        /// </param>
//        /// <returns>
//        /// Новый целевой объект.
//        /// </returns>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось создать средство сбора данных.
//        /// </exception>
//        private static global::Hbm.Api.Common.DaqMeasurement CreateTarget(int fetchingInterval, int transmissionTime)
//        {
//            return Performing.Perform(() => new global::Hbm.Api.Common.DaqMeasurement(fetchingInterval, transmissionTime),
//                "Не удалось создать средство сбора данных.");
//        }

//        /// <summary>
//        /// Возвращает значение, определяющее, выполняется ли сбор данных.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public bool IsRunning => Performing.Perform(() => Target.IsRunning);

//        /// <summary>
//        /// Возвращает общее время измерений в секундах.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public double MeasurementTotalTime => Performing.Perform(() => Target.MeasurementTotalTime);

//        /// <summary>
//        /// Возвращает время локальной остановки и дату последнего измерения, выполненного на этом компьютере.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public DateTime MeasurementStopSystemTime => Performing.Perform(() => Target.MeasurementStopSystemTime);

//        /// <summary>
//        /// Возвращает время и дату последнего измерения.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public DateTime MeasurementStopTime => Performing.Perform(() => Target.MeasurementStopTime);

//        /// <summary>
//        /// Остановите дату и время последнего измерения в формате UTC. Это самая последняя отметка времени всех сигналов, участвующих в измерении. Формат времени UIX Unix (секунды с 1.1.2000, после запятой - вторая секунда)
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public double MeasurementStopUTCTime => Performing.Perform(() => Target.MeasurementStopUTCTime);

//        /// <summary>
//        /// Местное время начала и дата последнего измерения, выполненного на этом компьютере. Это время берется с ПК при вызове функции Hqm.Api.Common.Daq Measurement.Start Daq.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public DateTime MeasurementStartSystemTime => Performing.Perform(() => Target.MeasurementStartSystemTime);

//        /// <summary>
//        /// Время начала и дата последнего измерения. Это время генерируется путем преобразования первой метки времени последнего измерения (Hbm.Api.Common.DaqMeasurement.MeasurementStartUTCTime в локальное представление даты и времени этого компьютера.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public DateTime MeasurementStartTime => Performing.Perform(() => Target.MeasurementStartTime);

//        /// <summary>
//        /// Дата и время начала последнего измерения в формате UTC. Это минимальная временная метка всех сигналов, которые участвуют в измерении. Формат времени UIX Unix (секунды с 1.1.2000, после запятой - вторая секунда)
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public double MeasurementStartUTCTime => Performing.Perform(() => Target.MeasurementStartUTCTime);

//        /// <summary>
//        /// Интервал времени в мс, в течение которого данные измерений запрашиваются со всех устройств, которые принимают участие в измерении.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public int FetchingInterval => Performing.Perform(() => Target.DataFetchingInterval);

//        /// <summary>
//        /// Возвращает true, если повторная синхронизация источника времени во время текущего сбора данных должна игнорироваться. В настоящее время это возможно только для потоковых устройств (семейство QuantumX). Повторная синхронизация происходит, когда источник времени будет изменен во время текущего измерения или если разница во времени между двумя устройствами оценена как слишком высокая. Значение по умолчанию неверно. Значение может быть установлено только функцией DaqMeasurement.PrepareDaq ().
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public bool IgnoreResync => Performing.Perform(() => Target.IgnoreResync);

//        /// <summary>
//        /// Возвращает используемый Hbm.Api.Common.Enums.DataAcquisitionMode, который использовался для запуска последнего измерения. Если последнее измерение было начато с Hbm.Api.Common.Enums.DataAcquisitionMode.Auto, используемый режим сбора данных будет реальным выбранным режимом (Hbm.Api.Common.Enums.DataAcquisitionMode.Unsynchronized, Hbm.Api. Common.Enums.DataAcquisitionMode.HardwareSynchronized или Hbm.Api.Common.Enums.DataAcquisitionMode.TimestampSynchronized).
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public HbmDataAcquisitionMode UsedDataAcquisitionMode
//        {
//            get
//            {
//                var source = Performing.Perform(() => Target.UsedDataAcquisitionMode);
//                switch (source)
//                {
//                    case global::Hbm.Api.Common.Enums.DataAcquisitionMode.TimestampSynchronized:
//                        return HbmDataAcquisitionMode.TimestampSynchronized;
//                    case global::Hbm.Api.Common.Enums.DataAcquisitionMode.HardwareSynchronized:
//                        return HbmDataAcquisitionMode.HardwareSynchronized;
//                    case global::Hbm.Api.Common.Enums.DataAcquisitionMode.Unsynchronized:
//                        return HbmDataAcquisitionMode.Unsynchronized;
//                    case global::Hbm.Api.Common.Enums.DataAcquisitionMode.Auto:
//                        return HbmDataAcquisitionMode.Auto;
//                    default:
//                        throw new NotSupportedException($"Значение {source} перечисления не поддерживается.");
//                }
//            }
//        }

//        /// <summary>
//        /// Добавляет сигнал к измеряемым сигналам.
//        /// </summary>
//        /// <param name="signal">
//        /// Сигнал, который необходимо добавить к измеряемым сигналам.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="signal"/> передана пустая ссылка.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось добавить сигнал.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Сигнал не может быть добавлен.
//        /// </exception>
//        public void Add(SignalOld signal)
//        {
//            if (signal is null)
//            {
//                throw new ArgumentNullException("signal", "Передана пустая ссылка");
//            }
//            if (signal is HbmSignal hbmSignal)
//            {
//                Performing.Perform(() => Target.AddSignals(hbmSignal.Device.Target, hbmSignal.Target), "Не удалось добавить сигнал.");
//            }
//            else
//            {
//                throw new InvalidOperationException("Сигнал не может быть добавлен.");
//            }
//        }

//        /// <summary>
//        /// Добавляет все сигналы из коллекции к измеряемым сигналам.
//        /// </summary>
//        /// <param name="signals">
//        /// Коллекция добавляемых сигналов.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="signals"/> передана пустая ссылка.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось добавить один из сигналов.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Один из сигналов не может быть добавлен.
//        /// </exception>
//        public void AddRange(IEnumerable<SignalOld> signals)
//        {
//            foreach (var signal in signals ?? throw new ArgumentNullException("signals", "Передана пустая ссылка."))
//            {
//                Add(signal);
//            }
//        }

//        /// <summary>
//        /// Добавляет все сигналы устройства к измеряемым сигналам.
//        /// </summary>
//        /// <param name="device">
//        /// Устройство, каналы которого необходимо добавить к измеряемым сигналам.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="device"/> передана пустая ссылка.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось добавить один из сигналов.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Один из сигналов не может быть добавлен.
//        /// </exception>
//        public void AddRange(DeviceOld device)
//        {
//            if (device is null)
//            {
//                new ArgumentNullException("device", "Передана пустая ссылка.");
//            }
//            AddRange(device.Signals);
//        }

//        /// <summary>
//        /// Исключает из измерения уже добавленный сигнал данного устройства.
//        /// </summary>
//        /// <param name="signal">
//        /// Сигнал, который необходимо исключить из измеряемых сигналов.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="signal"/> передана пустая ссылка.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось исключить сигнал.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Сигнал не может быть исключён.
//        /// </exception>
//        public void Remove(SignalOld signal)
//        {
//            if (signal is null)
//            {
//                throw new ArgumentNullException("signal", "Передана пустая ссылка");
//            }
//            if (signal is HbmSignal hbmSignal)
//            {
//                Performing.Perform(() => Target.RemoveSignals(hbmSignal.Device.Target, hbmSignal.Target), "Не удалось исключить сигнал.");
//            }
//            else
//            {
//                throw new InvalidOperationException("Сигнал не может быть исключён.");
//            }
//        }

//        /// <summary>
//        /// Исключает все сигналы из коллекции из измеряемых сигналов.
//        /// </summary>
//        /// <param name="signals">
//        /// Коллекция исключаемых сигналов.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="signals"/> передана пустая ссылка.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось исключить один из сигналов.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Один из сигналов не может быть исключён.
//        /// </exception>
//        public void RemoveRange(IEnumerable<SignalOld> signals)
//        {
//            foreach (var signal in signals ?? throw new ArgumentNullException("signals", "Передана пустая ссылка."))
//            {
//                Remove(signal);
//            }
//        }

//        /// <summary>
//        /// Исключает все сигналы устройства из измеряемых сигналов.
//        /// </summary>
//        /// <param name="device">
//        /// Устройство, каналы которого необходимо исключить из измеряемых сигналов.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="device"/> передана пустая ссылка.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось исключить один из сигналов.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Один из сигналов не может быть исключён.
//        /// </exception>
//        public void RemoveRange(DeviceOld device)
//        {
//            if (device is null)
//            {
//                new ArgumentNullException("device", "Передана пустая ссылка.");
//            }
//            RemoveRange(device.Signals);
//        }

//        /// <summary>
//        /// Подготавливает непрерывное измерение.
//        /// </summary>
//        /// <param name="bufferTimeout">
//        /// Тайм-аут буфера в миллисекундах. Используется для расчета размера внутреннего кольцевого буфера. По крайней мере, есть буфер из 1000 значений для каждого сигнала. Обычно размер буфера равен (bufferTimeout / 1000) * частота дискретизации сигнала, предварительно добавленного к измерению (например, Signal.SampleRate = 1200Hz, bufferTimeOut = 1000ms => размер внутреннего циклического буфера составляет 1200 записей)
//        /// </param>
//        /// <param name="ignoreResync">
//        /// Определяет, следует ли игнорировать повторную синхронизацию источника времени во время текущего сбора данных. В настоящее время это возможно только для потоковых устройств (семейство QuantumX). Установите в значение true, если сбор данных должен быть продолжен в случае обнаруженной повторной синхронизации.
//        /// </param>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось подготовить непрерывное измерение.
//        /// </exception>
//        public void Prepare(int bufferTimeout, bool ignoreResync = false)
//        {
//            Performing.Perform(() => Target.PrepareDaq(bufferTimeout, ignoreResync), "Не удалось подготовить непрерывное измерение.");
//        }

//        /// <summary>
//        /// Готовит непрерывное измерение с подгонкой параметров для большинства случаев.
//        /// </summary>
//        /// <param name="ignoreResync">
//        /// Определяет, следует ли игнорировать повторную синхронизацию источника времени во время текущего сбора данных. В настоящее время это возможно только для потоковых устройств (семейство QuantumX). Установите в значение true, если сбор данных должен быть продолжен в случае обнаруженной повторной синхронизации.
//        /// </param>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось подготовить непрерывное измерение.
//        /// </exception>
//        public void Prepare(bool ignoreResync = false)
//        {
//            Performing.Perform(() => Target.PrepareDaq(ignoreResync), "Не удалось подготовить непрерывное измерение.");
//        }

//        /// <summary>
//        /// Готовит непрерывное измерение. Участвующие устройства готовятся параллельно.
//        /// </summary>
//        /// <param name="bufferTimeout">
//        /// Тайм-аут буфера в миллисекундах. Используется для расчета размера внутреннего кольцевого буфера. По крайней мере, есть буфер из 1000 значений для каждого сигнала. Обычно размер буфера равен (bufferTimeout / 1000) * частота дискретизации сигнала, предварительно добавленного к измерению (например, Signal.SampleRate = 1200Hz, bufferTimeOut = 1000ms => размер внутреннего циклического буфера составляет 1200 записей)
//        /// </param>
//        /// <param name="maxNumberOfConcurrentDaqThreads">
//        /// Максимальное количество одновременно работающих потоков, которые извлекают данные с устройств и помещают их в кольцевые буферы сигналов. По крайней мере, 1 поток необходим. Максимальное количество зависит от вашей операционной системы и количества используемых устройств. До 100 потоков не должно быть проблем. Например: если вы хотите измерить 20 устройств, вы можете использовать один поток для каждого устройства.
//        /// </param>
//        /// <param name="minNumberOfBufferedValues">
//        /// Минимум количество элементов, которые будут буферизироваться в кольцевых буферах сигналов, участвующих в измерении. MinNumberOfBufferedValues используется, если minNumberOfBufferedValues> bufferTimeout * signal.Samplerate. Это важно для сигналов, которые не имеют частоты дискретизации (или частоты дискретизации = 0 Гц), которые обычно представляют собой сигналы с SynchronMode = NonEquidistant
//        /// </param>
//        /// <param name="isFirstTimeStampOnlyRequested">
//        /// Если true, только первая временная метка эквидистантного сигнала будет скопирована в значения измерения сигналов во время вызова Hbm.Api.Common.DaqMeasurement.FillMeasurementValues (System.Boolean)
//        /// </param>
//        /// <param name="isFirstStatusOnlyRequested">
//        /// Если true, только первое значение состояния эквидистантного сигнала будет скопировано в значения измерения сигналов во время вызова Hbm.Api.Common.DaqMeasurement.FillMeasurementValues (System.Boolean)
//        /// </param>
//        /// <param name="maxThreadStackSize">
//        /// Максимальный размер стека в байтах, который будет использоваться каждым потоком выборки данных устройства, или 0, чтобы использовать максимальный размер стека по умолчанию. Измените этот параметр, например, при использовании API в Diadem на 0.
//        /// </param>
//        /// <param name="ignoreResync">
//        /// Определяет, следует ли игнорировать повторную синхронизацию источника времени во время текущего сбора данных. В настоящее время это возможно только для потоковых устройств (семейство QuantumX). Установите в значение true, если сбор данных должен быть продолжен в случае обнаруженной повторной синхронизации.
//        /// </param>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось подготовить непрерывное измерение.
//        /// </exception>
//        public void Prepare(int bufferTimeout, int maxNumberOfConcurrentDaqThreads, int minNumberOfBufferedValues = 1000, bool isFirstTimeStampOnlyRequested = true, bool isFirstStatusOnlyRequested = true, int maxThreadStackSize = 262144, bool ignoreResync = false)
//        {
//            Performing.Perform(() => Target.PrepareDaq(bufferTimeout, maxNumberOfConcurrentDaqThreads, minNumberOfBufferedValues, isFirstTimeStampOnlyRequested, isFirstStatusOnlyRequested, maxThreadStackSize, ignoreResync), "Не удалось подготовить непрерывное измерение.");
//        }

//        /// <summary>
//        /// Запускает (синхронизирует) измерение всех сигналов, которые были добавлены к измерению.
//        /// </summary>
//        /// <param name="mode">
//        /// Режим Hbm.Api.Common.Enums.DataAcquisitionModeacquisition определяет процедуру, используемую для синхронизации различных устройств во время начала измерения, а также способ предоставления значений измерения (DaqMeasurement.FillMeasurementValues).
//        /// </param>
//        /// <param name="syncTimeOut">
//        /// Максимальное время в мс, которое используется для запуска синхронизированного измерения. Если невозможно запустить синхронизированное измерение в течение этого времени, будет сгенерировано исключение Hbm.API.Common.Exceptions.StartDaqFailedException.
//        /// </param>
//        /// <returns>
//        /// Максимальная разница между временными метками первых значений измерений в секундах. 0 означает, что первая отметка времени каждого сигнала имеет одинаковое значение
//        /// </returns>
//        /// <remarks>
//        /// После начала измерения время начала (наименьшая временная метка из всех сигналов, участвующих в измерении) можно найти в StartTime. Если измерение начато Hbm.Api.Common.Enums.DataAcquisitionMode.Unsynchronizedunsynchronized, функция возвращает 0.
//        /// </remarks>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="mode"/> передано значение, которое не содержится в перечислении <see cref="HbmDataAcquisitionMode"/>.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось запустить измерение.
//        /// </exception>
//        public double Start(HbmDataAcquisitionMode mode, int syncTimeOut = 5000)
//        {
//            global::Hbm.Api.Common.Enums.DataAcquisitionMode codeMode;
//            switch (mode)
//            {
//                case HbmDataAcquisitionMode.TimestampSynchronized:
//                    codeMode = global::Hbm.Api.Common.Enums.DataAcquisitionMode.TimestampSynchronized;
//                    break;
//                case HbmDataAcquisitionMode.HardwareSynchronized:
//                    codeMode = global::Hbm.Api.Common.Enums.DataAcquisitionMode.HardwareSynchronized;
//                    break;
//                case HbmDataAcquisitionMode.Unsynchronized:
//                    codeMode = global::Hbm.Api.Common.Enums.DataAcquisitionMode.Unsynchronized;
//                    break;
//                case HbmDataAcquisitionMode.Auto:
//                    codeMode = global::Hbm.Api.Common.Enums.DataAcquisitionMode.Auto;
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException("mode", "Передано значение, которое не содержится в перечислении.");
//            }
//            return Performing.Perform(() => Target.StartDaq(codeMode, syncTimeOut), "Не удалось запустить измерение.");
//        }

//        /// <summary>
//        /// Останавливает сбор данных всех устройств, которые участвуют в измерениях.
//        /// </summary>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось остановить измерения.
//        /// </exception>
//        public void Stop()
//        {
//            Performing.Perform(() => Target.StopDaq(), "Не удалось остановить измерения.");
//        }

//        /// <summary>
//        /// Эта функция обновляет все значения измерений всех сигналов, которые участвуют в измерениях. В случае синхронизированного начала сбора данных утверждается, что каждый сигнал с одинаковой частотой дискретизации (распределенный по различным устройствам) получает одинаковое количество новых значений измерений. Например. signal_A на устройстве 1 с частотой дискретизации 20 Гц получает то же количество новых значений измерения, что и signal_B на устройстве 2 с частотой дискретизации 20 Гц! В противном случае (если StartDaq запустил несинхронизированное измерение, сигналы получают все значения измерений, которые накапливаются с момента последнего вызова этой функции. Эта функция также утверждает, что имеется достаточно выделенной памяти для значений измерений каждого сигнала. Поэтому периодически вызывайте эту функцию !
//        /// </summary>
//        /// <param name="fillUntilSmallestCommonTimestamp">
//        /// Активен только в случае синхронизированного начала сбора данных. Если установлено значение true (по умолчанию false!), Оно будет заполнять значения измерений только до последней общей временной метки по всем сигналам, не исключенным из сбора данных. Сигналы с одинаковой частотой дискретизации продолжают получать то же количество новых значений измерений, как описано выше.
//        /// </param>
//        /// <remarks>
//        /// В случае Сигнала с SynchronMode, установленным в NonEquidistant, будут возвращены все полученные значения измерений! Независимо от того, как было начато измерение (см. DataAcquisitionMode).
//        /// </remarks>
//        public void FillMeasurementValues(bool fillUntilSmallestCommonTimestamp = false)
//        {
//            Performing.Perform(() => Target.FillMeasurementValues(fillUntilSmallestCommonTimestamp));
//        }

//        //// Сводка:
//        ////     Группирует зарегистрированные сигналы, если требуется. Если зарегистрированные сигналы содержат DigitalCompressedSignals, будет создан один DigitalCompressedGroupSignal для каждой группы сжатия (указанной в DigitalCompressedSignals). DigitalCompressedGroupSignal содержит все DigitalCompressedSignals с одинаковым номером группы сжатия. DigitalCompressedGroupSignal будет частью измерения, а не DigitalCompressedSignals.
//        ////
//        //// Параметры:
//        ////   registeredSignals:
//        ////     List of registered signals (of one device)
//        ////
//        //// Возврат:
//        ////     Grouped list of registered signals (of one device)
//        ////
//        //// Примечания:
//        ////     This method may be used in concrete device implementations if the device uses
//        ////     compression (one measurement value for multiple digital signals) for measuring
//        ////     digital signals (using Hbm.Api.Common.Entities.Signals.DigitalCompressedSignal).
//        ////     Then its useful for implementing the device specific methods Hbm.Api.Common.Entities.Device.ReadSingleMeasurementValue(System.Collections.Generic.List{Hbm.Api.Common.Entities.Signals.Signal})
//        ////     and Hbm.Api.Common.Entities.Device.ReadSingleMeasurementValueOfAllSignals
//        //public static List<Signal> GroupRegisteredSignals(List<Signal> registeredSignals);

//        //// Сводка:
//        ////     Gets the possible data acquisition modes according to the current synchronization
//        ////     settings of all devices of alldevice families that take part in measurement.
//        ////     Normally Hbm.Api.Common.Enums.DataAcquisitionMode.Unsynchronized is always possible
//        ////     (otherwise the devices would not deliver any measurement values at all). Hbm.Api.Common.Enums.DataAcquisitionMode.HardwareSynchronized
//        ////     is only available if all devices belong to the same family and are synchronized
//        ////     via a synchronization cable. Hbm.Api.Common.Enums.DataAcquisitionMode.TimestampSynchronized
//        ////     is possible if all devices (also of different device families) use the same Hbm.Api.Common.Entities.TimeSources.TimeSource.
//        ////
//        //// Возврат:
//        ////     List of possible Hbm.Api.Common.Enums.DataAcquisitionMode for this device according
//        ////     to its current synchronization settings.
//        ////
//        //// Примечания:
//        ////     You have to call Hbm.Api.Common.DaqMeasurement.PrepareDaq(System.Int32,System.Int32,System.Int32,System.Boolean,System.Boolean,System.Int32,System.Boolean)
//        ////     before you use this function because otherwise the signals and devices which
//        ////     take part in the measurement are not known.
//        //public List<DataAcquisitionMode> GetPossibleDataAcquisitionModes();
//    }
//}
