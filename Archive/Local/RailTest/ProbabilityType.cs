using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest
{
    /// <summary>
    /// Представляет значение, определяющее тип закона распределения.
    /// </summary>
    public enum ProbabilityType
    {
        /// <summary>
        /// Нормальный закон распределения.
        /// </summary>
        Standard,

        /// <summary>
        /// Эмпирический закон распределения.
        /// </summary>
        Empirical,
    }
}
