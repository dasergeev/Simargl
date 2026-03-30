using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Security.AccessControl;
using System.Threading;

namespace RailTest.Memory
{
    /// <summary>
    /// Представляет общую память.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class SharedMemory
    {
        /// <summary>
        /// Постоянная для хранения базовой части полного имени примитива синхронизации.
        /// </summary>
        private const string _MutexBaseName = "Global\\[CC5EAED3-14D4-4D75-9E0E-EB3D7055B534] ";

        /// <summary>
        /// Постоянная для хранения базовой части полного имени файла.
        /// </summary>
        private const string _MappedBaseName = "Global\\[A176EBD7-8C54-44E0-8E4B-EF0475493447] ";

        /// <summary>
        /// Поле для хранения размещенного в памяти файла.
        /// </summary>
        private readonly MemoryMappedFile _Mapped;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="name">
        /// Имя общей памяти.
        /// </param>
        /// <param name="size">
        /// Размер памяти.
        /// </param>
        public SharedMemory(string name, long size)
        {
            name = name ?? string.Empty;

            const string user = "Все";
            MutexSecurity mutexSecurity = new MutexSecurity();

            mutexSecurity.AddAccessRule(new MutexAccessRule(user, MutexRights.Synchronize | MutexRights.Modify, AccessControlType.Allow));
            mutexSecurity.AddAccessRule(new MutexAccessRule(user, MutexRights.ChangePermissions, AccessControlType.Deny));
            mutexSecurity.AddAccessRule(new MutexAccessRule(user, MutexRights.ReadPermissions, AccessControlType.Allow));

            Mutex = new Mutex(true, _MutexBaseName + name, out bool createdNew, mutexSecurity);
            if (createdNew)
            {
                MemoryMappedFileSecurity mappedSecurity = new MemoryMappedFileSecurity();
                mappedSecurity.AddAccessRule(new AccessRule<MemoryMappedFileRights>(user, MemoryMappedFileRights.ReadWrite, AccessControlType.Allow));
                _Mapped = MemoryMappedFile.CreateNew(_MappedBaseName + name, size, MemoryMappedFileAccess.ReadWrite, MemoryMappedFileOptions.None, mappedSecurity, HandleInheritability.Inheritable);
                Mutex.ReleaseMutex();
            }
            else
            {
                _Mapped = MemoryMappedFile.OpenExisting(_MappedBaseName + name);
            }
            Name = name;
            Size = size;
        }

        /// <summary>
        /// Возвращает имя общей памяти.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает размер памяти.
        /// </summary>
        public long Size { get; }

        /// <summary>
        /// Возвращает примитив межпроцессной синхронизации.
        /// </summary>
        public Mutex Mutex { get; }

        /// <summary>
        /// Возвращает объект для произвольного доступа к памяти.
        /// </summary>
        /// <returns>
        /// Объект для произвольного доступа к памяти.
        /// </returns>
        public MemoryMappedViewAccessor CreateViewAccessor() => _Mapped.CreateViewAccessor();

        /// <summary>
        /// Возвращает объект для произвольного доступа к памяти.
        /// </summary>
        /// <param name="offset">
        /// Смещение.
        /// </param>
        /// <param name="size">
        /// Размер.
        /// </param>
        /// <returns>
        /// Объект для произвольного доступа к памяти.
        /// </returns>
        public MemoryMappedViewAccessor CreateViewAccessor(long offset, long size) => _Mapped.CreateViewAccessor(offset, size);
    }
}
