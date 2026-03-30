using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailTest.Satellite.Autonomic.Services
{
    /// <summary>
    /// Представляет базовый класс для всех объектов, предоставляющих информацию о службе.
    /// </summary>
    public abstract class ServiceInfo
    {
        /// <summary>
        /// Постоянная для хранения полного размера информаци.
        /// </summary>
        public const int FullSize = 8;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="serviceID">
        /// Идентификатор службы.
        /// </param>
        /// <param name="memoryAccessor">
        /// Объект для прямого доступа к памяти.
        /// </param>
        /// <param name="mutex">
        /// Примитив для межпроцессной синхронизации.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="serviceID"/> передано значение,
        /// которое не содержится в перечислении <see cref="ServiceID"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="memoryAccessor"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="mutex"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// В параметре <paramref name="memoryAccessor"/> передан объект с ёмкостью меньшей <see cref="FullSize"/>.
        /// </exception>
        public ServiceInfo(ServiceID serviceID, MemoryMappedViewAccessor memoryAccessor, Mutex mutex)
        {
            if (!Enum.IsDefined(typeof(ServiceID), serviceID))
            {
                throw new ArgumentOutOfRangeException("serviceID", "Значение не содержится в перечислении.");
            }

            ServiceID = ServiceID;
            ServiceName = serviceID.GetServiceName();

            MemoryAccessor = memoryAccessor ?? throw new ArgumentNullException("memoryAccessor", "Передана пустая ссылка.");
            Mutex = mutex ?? throw new ArgumentNullException("mutex", "Передана пустая ссылка.");

            if (MemoryAccessor.Capacity < FullSize)
            {
                throw new ArgumentException("Ёмкость меньше допустимой.", "memoryAccessor");
            }
        }

        /// <summary>
        /// Возвращает идентификатор службы.
        /// </summary>
        public ServiceID ServiceID { get; }

        /// <summary>
        /// Возвращает имя службы.
        /// </summary>
        public string ServiceName { get; }

        /// <summary>
        /// Возращает объект для прямого доступа к памяти.
        /// </summary>
        protected MemoryMappedViewAccessor MemoryAccessor { get; }

        /// <summary>
        /// Возвращает примитив межпроцессной синхронизации.
        /// </summary>
        public Mutex Mutex { get; }
    }
}
