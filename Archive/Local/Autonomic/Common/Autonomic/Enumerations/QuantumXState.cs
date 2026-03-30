using System;
using System.Collections.Generic;
using System.Text;

namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Представляет состояние устройства.
    /// </summary>
    public enum QuantumXState
    {
        /// <summary>
        /// Не подготовлено.
        /// </summary>
        NotPrepared,

        /// <summary>
        /// Отключено.
        /// </summary>
        NotConnected,

        /// <summary>
        /// Устройство подключено.
        /// </summary>
        Connected,

        /// <summary>
        /// Устройство запущено.
        /// </summary>
        Registration,
    }
}
