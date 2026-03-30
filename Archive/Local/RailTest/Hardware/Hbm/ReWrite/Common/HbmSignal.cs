//using RailTest.Signals;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет сигнал устройства HBM.
//    /// </summary>
//    public abstract class HbmSignal : SignalOld
//    {
//        /// <summary>
//        /// Поле для хранения текущего значения.
//        /// </summary>
//        private double _Value;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="channel">
//        /// Канал, к которому принадлежит сигнал.
//        /// </param>
//        internal HbmSignal(global::Hbm.Api.Common.Entities.Signals.Signal target, HbmChannel channel) :
//            base(target.Name, (double)target.SampleRate)
//        {
//            Target = target;
//            Channel = channel;
//            Device = Channel.Device;
//            Connector = Channel.Connector;
//            _Value = 0;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal global::Hbm.Api.Common.Entities.Signals.Signal Target;

//        /// <summary>
//        /// Возвращает устройство, которому принадлежит разъём.
//        /// </summary>
//        public HbmDevice Device { get; }

//        /// <summary>
//        /// Возвращает разъём, к которому присоединён канал.
//        /// </summary>
//        public HbmConnector Connector { get; }

//        /// <summary>
//        /// Возвращает канал, к которому принадлежит сигнал.
//        /// </summary>
//        public HbmChannel Channel { get; }

//        /// <summary>
//        /// Уставнавливает новое имя сигнала.
//        /// </summary>
//        /// <param name="name">
//        /// Новое имя сигнала.
//        /// </param>
//        protected override void CoreSetName(string name)
//        {
//            string lastName = Target.Name;
//            try
//            {
//                Target.Name = name;
//                Device.AssignSignal(this);
//            }
//            catch
//            {
//                Target.Name = lastName;
//                throw;
//            }
//        }

//        /// <summary>
//        /// Уставнавливает частоту дискретизации.
//        /// </summary>
//        /// <param name="sampling">
//        /// Частота дискретизации.
//        /// </param>
//        protected override void CoreSetSampling(double sampling)
//        {
//            double lastSampling = (double)Target.SampleRate;
//            try
//            {
//                Target.SampleRate = (decimal)sampling;
//                Device.AssignSignal(this);
//            }
//            catch
//            {
//                Target.SampleRate = (decimal)lastSampling;
//                throw;
//            }
//        }

//        /// <summary>
//        /// Возвращает текущее значение.
//        /// </summary>
//        /// <returns>
//        /// Текущее значение.
//        /// </returns>
//        protected override double CoreGetValue()
//        {
//            return _Value;
//        }

//        /// <summary>
//        /// Выполняет балансировку нуля.
//        /// </summary>
//        /// <param name="values">
//        /// Количество измерений, используемое при балансировке.
//        /// </param>
//        protected override void CoreZeroBalance(int values)
//        {
//            Device.SetZeroBalance(Channel, values);
//        }

//        /// <summary>
//        /// Обновляет текущее значение сигнала.
//        /// </summary>
//        internal void UpdateCurrentValue()
//        {
//            double value = 0;
//            Performing.Perform(() =>
//            {
//                global::Hbm.Api.Common.Entities.MeasurementValue measurementValue = null;
//                measurementValue = Target.GetSingleMeasurementValue();
//                if (measurementValue is object)
//                {
//                    value = measurementValue.Value;
//                }
//                if (value == HbmDevice.Overflow)
//                {
//                    value = double.PositiveInfinity;
//                }
//            }, "Не удалось прочитать значение сигнала.");
//            if (_Value != value)
//            {
//                _Value = value;
//                OnValueChanged(EventArgs.Empty);
//            }
//        }

//        /// <summary>
//        /// Обновляет отклик аппаратуры.
//        /// </summary>
//        /// <param name="startTime">
//        /// Время начала регистрации.
//        /// </param>
//        internal void UpdateResponse(DateTime startTime)
//        {
//            var values = Target.ContinuousMeasurementValues;
//            int count = values.UpdatedValueCount;//values.Values.Length;
//            if (count != 0)
//            {
//                double second = values.Timestamps[0];
//                DateTime time = startTime.AddSeconds(second);
//                Timestamp timestamp = time;
//                double[] items = new double[count];
//                for (int i = 0; i < count; i++)
//                {
//                    items[i] = values.Values[i];
//                }
//                OnResponse(new ResponseEventArgsOld(new ValueCollectionOld(timestamp, items)));
//            }
//        }

//        /// <summary>
//        /// Создаёт новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="channel">
//        /// Канал, к которому принадлежит сигнал.
//        /// </param>
//        /// <returns>
//        /// Новый экземпляр класса.
//        /// </returns>
//        internal static HbmSignal Create(global::Hbm.Api.Common.Entities.Signals.Signal target, HbmChannel channel)
//        {
//            if (target is null)
//            {
//                throw new ArgumentNullException("target", "Передана пустая ссылка.");
//            }
//            if (target is global::Hbm.Api.Common.Entities.Signals.DigitalCompressedGroupSignal targetDigitalCompressedGroupSignal)
//            {
//                return new HbmDigitalCompressedGroupSignal(targetDigitalCompressedGroupSignal, channel);
//            }
//            if (target is global::Hbm.Api.GenericStreaming.Signals.GenericStreamingVirtualSignal targetGenericStreamingVirtualSignal)
//            {
//                return new HbmGenericStreamingVirtualSignal(targetGenericStreamingVirtualSignal, channel);
//            }
//            if (target is global::Hbm.Api.Mgc.Signals.MgcAnalogInSignal targetMgcAnalogInSignal)
//            {
//                return new HbmMgcAnalogInSignal(targetMgcAnalogInSignal, channel);
//            }
//            if (target is global::Hbm.Api.Mgc.Signals.MgcAnalogOutSignal targetMgcAnalogOutSignal)
//            {
//                return new HbmMgcAnalogOutSignal(targetMgcAnalogOutSignal, channel);
//            }
//            if (target is global::Hbm.Api.Mgc.Signals.MgcCanInSignal targetMgcCanInSignal)
//            {
//                return new HbmMgcCanInSignal(targetMgcCanInSignal, channel);
//            }
//            if (target is global::Hbm.Api.Mgc.Signals.MgcDigitalSignal targetMgcDigitalSignal)
//            {
//                return new HbmMgcDigitalSignal(targetMgcDigitalSignal, channel);
//            }
//            if (target is global::Hbm.Api.Mgc.Signals.MgcVirtualSignal targetMgcVirtualSignal)
//            {
//                return new HbmMgcVirtualSignal(targetMgcVirtualSignal, channel);
//            }
//            if (target is global::Hbm.Api.Pmx.Signals.PmxAnalogInSignal targetPmxAnalogInSignal)
//            {
//                return new HbmPmxAnalogInSignal(targetPmxAnalogInSignal, channel);
//            }
//            if (target is global::Hbm.Api.Pmx.Signals.PmxAnalogOutSignal targetPmxAnalogOutSignal)
//            {
//                return new HbmPmxAnalogOutSignal(targetPmxAnalogOutSignal, channel);
//            }
//            if (target is global::Hbm.Api.Pmx.Signals.PmxDigitalSignal targetPmxDigitalSignal)
//            {
//                return new HbmPmxDigitalSignal(targetPmxDigitalSignal, channel);
//            }
//            if (target is global::Hbm.Api.Pmx.Signals.PmxVirtualSignal targetPmxVirtualSignal)
//            {
//                return new HbmPmxVirtualSignal(targetPmxVirtualSignal, channel);
//            }
//            if (target is global::Hbm.Api.QuantumX.Signals.QuantumXAnalogInSignal targetQuantumXAnalogInSignal)
//            {
//                return new HbmQuantumXAnalogInSignal(targetQuantumXAnalogInSignal, channel);
//            }
//            if (target is global::Hbm.Api.QuantumX.Signals.QuantumXAnalogOutSignal targetQuantumXAnalogOutSignal)
//            {
//                return new HbmQuantumXAnalogOutSignal(targetQuantumXAnalogOutSignal, channel);
//            }
//            if (target is global::Hbm.Api.QuantumX.Signals.QuantumXCanInSignal targetQuantumXCanInSignal)
//            {
//                return new HbmQuantumXCanInSignal(targetQuantumXCanInSignal, channel);
//            }
//            if (target is global::Hbm.Api.QuantumX.Signals.QuantumXDigitalSignal targetQuantumXDigitalSignal)
//            {
//                return new HbmQuantumXDigitalSignal(targetQuantumXDigitalSignal, channel);
//            }
//            throw new NotSupportedException($"Тип {target.GetType().FullName} не поддерживается.");
//        }
//    }
//}
