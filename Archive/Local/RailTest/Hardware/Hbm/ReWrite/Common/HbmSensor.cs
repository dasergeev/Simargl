//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет собой физический датчик.
//    /// </summary>
//    public abstract class HbmSensor : Ancestor
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        internal HbmSensor(global::Hbm.Api.SensorDB.Entities.Sensor target)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal global::Hbm.Api.SensorDB.Entities.Sensor Target;

//        /// <summary>
//        /// Создаёт новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <returns>
//        /// Новый экземпляр класса.
//        /// </returns>
//        internal static HbmSensor Create(global::Hbm.Api.SensorDB.Entities.Sensor target)
//        {
//            if (target is null)
//            {
//                throw new ArgumentNullException("target", "Передана пустая ссылка.");
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.BridgeSensor targetBridgeSensor)
//            {
//                return new HbmBridgeSensor(targetBridgeSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.CanBusSensor targetCanBusSensor)
//            {
//                return new HbmCanBusSensor(targetCanBusSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.CounterSensor targetCounterSensor)
//            {
//                return new HbmCounterSensor(targetCounterSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.CurrentProbeSensor targetCurrentProbeSensor)
//            {
//                return new HbmCurrentProbeSensor(targetCurrentProbeSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.CurrentSensor targetCurrentSensor)
//            {
//                return new HbmCurrentSensor(targetCurrentSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.FbgAccelerometerSensor targetFbgAccelerometerSensor)
//            {
//                return new HbmFbgAccelerometerSensor(targetFbgAccelerometerSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.FbgGenericSensor targetFbgGenericSensor)
//            {
//                return new HbmFbgGenericSensor(targetFbgGenericSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.FbgStrainGaugeSensor targetFbgStrainGaugeSensor)
//            {
//                return new HbmFbgStrainGaugeSensor(targetFbgStrainGaugeSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.FbgThermocoupleSensor targetFbgThermocoupleSensor)
//            {
//                return new HbmFbgThermocoupleSensor(targetFbgThermocoupleSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.FbgWavelengthSensor targetFbgWavelengthSensor)
//            {
//                return new HbmFbgWavelengthSensor(targetFbgWavelengthSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.FrequencySensor targetFrequencySensor)
//            {
//                return new HbmFrequencySensor(targetFrequencySensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.IepeSensor targetIepeSensor)
//            {
//                return new HbmIepeSensor(targetIepeSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.LvdtSensor targetLvdtSensor)
//            {
//                return new HbmLvdtSensor(targetLvdtSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.NotSupportedSensor targetNotSupportedSensor)
//            {
//                return new HbmNotSupportedSensor(targetNotSupportedSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.PiezoPassiveSensor targetPiezoPassiveSensor)
//            {
//                return new HbmPiezoPassiveSensor(targetPiezoPassiveSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.PotentiometerSensor targetPotentiometerSensor)
//            {
//                return new HbmPotentiometerSensor(targetPotentiometerSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.PtSensor targetPtSensor)
//            {
//                return new HbmPtSensor(targetPtSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.PwmSensor targetPwmSensor)
//            {
//                return new HbmPwmSensor(targetPwmSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.ResistanceSensor targetResistanceSensor)
//            {
//                return new HbmResistanceSensor(targetResistanceSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.SsiSensor targetSsiSensor)
//            {
//                return new HbmSsiSensor(targetSsiSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.ThermoCoupleSensor targetThermoCoupleSensor)
//            {
//                return new HbmThermoCoupleSensor(targetThermoCoupleSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.VoltageProbeSensor targetVoltageProbeSensor)
//            {
//                return new HbmVoltageProbeSensor(targetVoltageProbeSensor);
//            }
//            if (target is global::Hbm.Api.SensorDB.Entities.Sensors.VoltageSensor targetVoltageSensor)
//            {
//                return new HbmVoltageSensor(targetVoltageSensor);
//            }
//            throw new NotSupportedException($"Тип {target.GetType().FullName} не поддерживается.");
//        }
//    }
//}
