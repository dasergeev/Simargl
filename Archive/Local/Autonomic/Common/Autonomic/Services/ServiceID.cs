using System;

namespace RailTest.Satellite.Autonomic.Services
{
    /// <summary>
    /// Представляет идентификатор службы.
    /// </summary>
    public enum ServiceID
    {
        /// <summary>
        /// Аудитор регистратора.
        /// </summary>
        Auditor,

        /// <summary>
        /// Служба работы с устройством QuantumX.
        /// </summary>
        QuantumX,

        /// <summary>
        /// Служба для работы с тельтоникой.
        /// </summary>
        Teltonika,

        /// <summary>
        /// Служба сохранения файлов.
        /// </summary>
        Recorder,
    }

    /// <summary>
    /// Предоставляет методы расширения для <see cref="ServiceID"/>.
    /// </summary>
    public static class ServiceIDExtension
    {
        /// <summary>
        /// Возвращает имя службы.
        /// </summary>
        /// <param name="serviceID">
        /// Идентификатор службы.
        /// </param>
        /// <returns>
        /// Имя службы.
        /// </returns>
        public static string GetServiceName(this ServiceID serviceID)
        {
            return Enum.GetName(typeof(ServiceID), serviceID);
        }
    }
}
