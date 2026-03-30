//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет канал устройства HBM.
//    /// </summary>
//    public abstract class HbmChannel : Ancestor
//    {
//        /// <summary>
//        /// Происходит при изменении свойства <see cref="Name"/>.
//        /// </summary>
//        public event EventHandler NameChanged;

//        /// <summary>
//        /// Поле для хранения имени канала.
//        /// </summary>
//        private string _Name;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="connector">
//        /// Разъём, к которому присоединён канал.
//        /// </param>
//        internal HbmChannel(global::Hbm.Api.Common.Entities.Channels.Channel target, HbmConnector connector)
//        {
//            Target = target;
//            Connector = connector;
//            Device = Connector.Device;
//            Signal = HbmSignal.Create(Target.Signals[0], this);
//            _Name = target.Name ?? string.Empty;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal global::Hbm.Api.Common.Entities.Channels.Channel Target;

//        /// <summary>
//        /// Возвращает устройство, которому принадлежит разъём.
//        /// </summary>
//        public HbmDevice Device { get; }

//        /// <summary>
//        /// Возвращает разъём, к которому присоединён канал.
//        /// </summary>
//        public HbmConnector Connector { get; }

//        /// <summary>
//        /// Возвращает сигнал, который принадлежит каналу.
//        /// </summary>
//        public HbmSignal Signal { get; }

//        /// <summary>
//        /// Возвращает или задаёт имя канала.
//        /// </summary>
//        public string Name
//        {
//            get
//            {
//                return _Name;
//            }
//            set
//            {
//                if (_Name != value)
//                {
//                    Target.Name = value;
//                    try
//                    {
//                        Device.SetChannelName(this, value);
//                    }
//                    catch
//                    {
//                        Target.Name = _Name;
//                        throw;
//                    }
//                    _Name = value;
//                    OnNameChanged(EventArgs.Empty);
//                }
//            }
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="NameChanged"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnNameChanged(EventArgs e)
//        {
//            NameChanged?.Invoke(this, e);
//        }

//        /// <summary>
//        /// Создаёт новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="connector">
//        /// Разъём, к которому присоединён канал.
//        /// </param>
//        /// <returns>
//        /// Новый экземпляр класса.
//        /// </returns>
//        internal static HbmChannel Create(global::Hbm.Api.Common.Entities.Channels.Channel target, HbmConnector connector)
//        {
//            if (target is null)
//            {
//                throw new ArgumentNullException("target", "Передана пустая ссылка.");
//            }
//            if (target is global::Hbm.Api.GenericStreaming.Channels.GenericStreamingVirtualChannel targetGenericStreamingVirtualChannel)
//            {
//                return new HbmGenericStreamingVirtualChannel(targetGenericStreamingVirtualChannel, connector);
//            }
//            if (target is global::Hbm.Api.Mgc.Channels.MgcAnalogInChannel targetMgcAnalogInChannel)
//            {
//                return new HbmMgcAnalogInChannel(targetMgcAnalogInChannel, connector);
//            }
//            if (target is global::Hbm.Api.Mgc.Channels.MgcAnalogOutChannel targetMgcAnalogOutChannel)
//            {
//                return new HbmMgcAnalogOutChannel(targetMgcAnalogOutChannel, connector);
//            }
//            if (target is global::Hbm.Api.Mgc.Channels.MgcCanInChannel targetMgcCanInChannel)
//            {
//                return new HbmMgcCanInChannel(targetMgcCanInChannel, connector);
//            }
//            if (target is global::Hbm.Api.Mgc.Channels.MgcDigitalChannel targetMgcDigitalChannel)
//            {
//                return new HbmMgcDigitalChannel(targetMgcDigitalChannel, connector);
//            }
//            if (target is global::Hbm.Api.Mgc.Channels.MgcVirtualChannel targetMgcVirtualChannel)
//            {
//                return new HbmMgcVirtualChannel(targetMgcVirtualChannel, connector);
//            }
//            if (target is global::Hbm.Api.Pmx.Channels.PmxAnalogInChannel targetPmxAnalogInChannel)
//            {
//                return new HbmPmxAnalogInChannel(targetPmxAnalogInChannel, connector);
//            }
//            if (target is global::Hbm.Api.Pmx.Channels.PmxAnalogOutChannel targetPmxAnalogOutChannel)
//            {
//                return new HbmPmxAnalogOutChannel(targetPmxAnalogOutChannel, connector);
//            }
//            if (target is global::Hbm.Api.Pmx.Channels.PmxDigitalChannel targetPmxDigitalChannel)
//            {
//                return new HbmPmxDigitalChannel(targetPmxDigitalChannel, connector);
//            }
//            if (target is global::Hbm.Api.Pmx.Channels.PmxVirtualChannel targetPmxVirtualChannel)
//            {
//                return new HbmPmxVirtualChannel(targetPmxVirtualChannel, connector);
//            }
//            if (target is global::Hbm.Api.QuantumX.Channels.QuantumXAnalogInChannel targetQuantumXAnalogInChannel)
//            {
//                return new HbmQuantumXAnalogInChannel(targetQuantumXAnalogInChannel, connector);
//            }
//            if (target is global::Hbm.Api.QuantumX.Channels.QuantumXAnalogOutChannel targetQuantumXAnalogOutChannel)
//            {
//                return new HbmQuantumXAnalogOutChannel(targetQuantumXAnalogOutChannel, connector);
//            }
//            if (target is global::Hbm.Api.QuantumX.Channels.QuantumXCanInChannel targetQuantumXCanInChannel)
//            {
//                return new HbmQuantumXCanInChannel(targetQuantumXCanInChannel, connector);
//            }
//            if (target is global::Hbm.Api.QuantumX.Channels.QuantumXCanOutChannel targetQuantumXCanOutChannel)
//            {
//                return new HbmQuantumXCanOutChannel(targetQuantumXCanOutChannel, connector);
//            }
//            if (target is global::Hbm.Api.QuantumX.Channels.QuantumXDigitalChannel targetQuantumXDigitalChannel)
//            {
//                return new HbmQuantumXDigitalChannel(targetQuantumXDigitalChannel, connector);
//            }
//            if (target is global::Hbm.Api.QuantumX.Channels.QuantumXFbgChannel targetQuantumXFbgChannel)
//            {
//                return new HbmQuantumXFbgChannel(targetQuantumXFbgChannel, connector);
//            }
//            throw new NotSupportedException($"Тип {target.GetType().FullName} не поддерживается.");
//        }
//    }
//}
