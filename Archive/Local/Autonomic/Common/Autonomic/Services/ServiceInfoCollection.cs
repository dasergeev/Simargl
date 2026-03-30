using RailTest.Memory;
using System.Collections.Generic;

namespace RailTest.Satellite.Autonomic.Services
{
    /// <summary>
    /// Представляет коллекцию информации о службах.
    /// </summary>
    public abstract class ServiceInfoCollection
    {
        /// <summary>
        /// Постоянная для хранения префикса имени файла.
        /// </summary>
        private const string _PrefixName = "ServiceInfoCollection ";

        /// <summary>
        /// Поле для хранения общей памяти.
        /// </summary>
        private readonly SharedMemory _Memory;

        /// <summary>
        /// Поле для хранения элементов коллекции.
        /// </summary>
        private readonly IReadOnlyList<ServiceInfo> _Items;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="name">
        /// Имя коллекции.
        /// </param>
        public ServiceInfoCollection(string name)
        {
            name = name ?? string.Empty;
            _Memory = new SharedMemory(_PrefixName + name, ServiceInfo.FullSize * Count);
            _Items = InitializeItems(_Memory);
        }

        /// <summary>
        /// Возвращает имя коллекции.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает количество элементов в коллекции.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Инициализирует элементы коллекции.
        /// </summary>
        /// <param name="memory">
        /// Общая память.
        /// </param>
        /// <returns>
        /// Коллекция элементов.
        /// </returns>
        protected abstract IReadOnlyList<ServiceInfo> InitializeItems(SharedMemory memory);
    }
}
