//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет разъем физического устройства.
//    /// </summary>
//    public abstract class HbmConnector : Ancestor
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="device">
//        /// Устройство, которому принадлежит разъём.
//        /// </param>
//        internal HbmConnector(global::Hbm.Api.Common.Entities.Connectors.Connector target, HbmDevice device)
//        {
//            Target = target;
//            Device = device;
//            Channel = HbmChannel.Create(Target.Channels[0], this);
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal global::Hbm.Api.Common.Entities.Connectors.Connector Target { get; }

//        /// <summary>
//        /// Возвращает устройство, которому принадлежит разъём.
//        /// </summary>
//        public HbmDevice Device { get; }

//        /// <summary>
//        /// Возвращает канал, который присоединён к разъёму.
//        /// </summary>
//        public HbmChannel Channel { get; }

//        /// <summary>
//        /// Создаёт новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="device">
//        /// Устройство, которому принадлежит разъём.
//        /// </param>
//        /// <returns>
//        /// Новый экземпляр класса.
//        /// </returns>
//        internal static HbmConnector Create(global::Hbm.Api.Common.Entities.Connectors.Connector target, HbmDevice device)
//        {
//            if (target is null)
//            {
//                throw new ArgumentNullException("target", "Передана пустая ссылка.");
//            }
//            if (target is global::Hbm.Api.GenericStreaming.Connectors.GenericStreamingVirtualConnector targetGenericStreamingVirtualConnector)
//            {
//                return new HbmGenericStreamingVirtualConnector(targetGenericStreamingVirtualConnector, device);
//            }
//            if (target is global::Hbm.Api.Mgc.Connectors.MgcAnalogInConnector targetMgcAnalogInConnector)
//            {
//                return new HbmMgcAnalogInConnector(targetMgcAnalogInConnector, device);
//            }
//            if (target is global::Hbm.Api.Mgc.Connectors.MgcAnalogOutConnector targetMgcAnalogOutConnector)
//            {
//                return new HbmMgcAnalogOutConnector(targetMgcAnalogOutConnector, device);
//            }
//            if (target is global::Hbm.Api.Mgc.Connectors.MgcCanConnector targetMgcCanConnector)
//            {
//                return new HbmMgcCanConnector(targetMgcCanConnector, device);
//            }
//            if (target is global::Hbm.Api.Mgc.Connectors.MgcDigitalConnector targetMgcDigitalConnector)
//            {
//                return new HbmMgcDigitalConnector(targetMgcDigitalConnector, device);
//            }
//            if (target is global::Hbm.Api.Mgc.Connectors.MgcVirtualConnector targetMgcVirtualConnector)
//            {
//                return new HbmMgcVirtualConnector(targetMgcVirtualConnector, device);
//            }
//            if (target is global::Hbm.Api.Pmx.Connectors.PmxAnalogInConnector targetPmxAnalogInConnector)
//            {
//                return new HbmPmxAnalogInConnector(targetPmxAnalogInConnector, device);
//            }
//            if (target is global::Hbm.Api.Pmx.Connectors.PmxAnalogOutConnector targetPmxAnalogOutConnector)
//            {
//                return new HbmPmxAnalogOutConnector(targetPmxAnalogOutConnector, device);
//            }
//            if (target is global::Hbm.Api.Pmx.Connectors.PmxDigitalConnector targetPmxDigitalConnector)
//            {
//                return new HbmPmxDigitalConnector(targetPmxDigitalConnector, device);
//            }
//            if (target is global::Hbm.Api.Pmx.Connectors.PmxVirtualConnector targetPmxVirtualConnector)
//            {
//                return new HbmPmxVirtualConnector(targetPmxVirtualConnector, device);
//            }
//            if (target is global::Hbm.Api.QuantumX.Connectors.QuantumXAnalogInConnector targetQuantumXAnalogInConnector)
//            {
//                return new HbmQuantumXAnalogInConnector(targetQuantumXAnalogInConnector, device);
//            }
//            if (target is global::Hbm.Api.QuantumX.Connectors.QuantumXAnalogOutConnector targetQuantumXAnalogOutConnector)
//            {
//                return new HbmQuantumXAnalogOutConnector(targetQuantumXAnalogOutConnector, device);
//            }
//            if (target is global::Hbm.Api.QuantumX.Connectors.QuantumXCanConnector targetQuantumXCanConnector)
//            {
//                return new HbmQuantumXCanConnector(targetQuantumXCanConnector, device);
//            }
//            if (target is global::Hbm.Api.QuantumX.Connectors.QuantumXDigitalConnector targetQuantumXDigitalConnector)
//            {
//                return new HbmQuantumXDigitalConnector(targetQuantumXDigitalConnector, device);
//            }
//            if (target is global::Hbm.Api.QuantumX.Connectors.QuantumXFbgConnector targetQuantumXFbgConnector)
//            {
//                return new HbmQuantumXFbgConnector(targetQuantumXFbgConnector, device);
//            }
//            if (target is global::Hbm.Api.QuantumX.Connectors.QuantumXOffConnector targetQuantumXOffConnector)
//            {
//                return new HbmQuantumXOffConnector(targetQuantumXOffConnector, device);
//            }
//            throw new NotSupportedException($"Тип {target.GetType().FullName} не поддерживается.");
//        }
//    }
//}
