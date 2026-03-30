using System.Runtime.InteropServices;
using System.Security;

namespace RailTest.Core
{
    /// <summary>
    /// Предоставляет доступ к неуправляемым библиотеками.
    /// </summary>
    internal static unsafe partial class Import
    {
        /// <summary>
        /// Выполняет копирование содержимого исходного блока памяти в целевой блок памяти.
        /// </summary>
        /// <param name="destination">
        /// Указатель на целевой блок памяти, в который необходимо выполнить копирование.
        /// </param>
        /// <param name="source">
        /// Указатель на исходный блок памяти, из которого необходимо выполнить копирование.
        /// </param>
        /// <param name="size">
        /// Размер копируемых данных в байтах.
        /// </param>
        /// <remarks>
        /// Метод <see cref="RtlCopyMemory"/> работает быстрее, чем метод <see cref="RtlMoveMemory"/>,
        /// но для корректной работы метод <see cref="RtlCopyMemory"/> требует,
        /// чтобы исходный блок памяти <paramref name="source"/> не перекрывал целевой блок памяти <paramref name="destination"/>.
        /// </remarks>
        [LibraryImport("kernel32.dll")]
        [SuppressUnmanagedCodeSecurity]
        public static partial void RtlCopyMemory(void* destination, void* source, ulong size);

        /// <summary>
        /// Выполняет копирование содержимого исходного блока памяти в целевой блок памяти.
        /// </summary>
        /// <param name="destination">
        /// Указатель на целевой блок памяти, в который необходимо выполнить копирование.
        /// </param>
        /// <param name="source">
        /// Указатель на исходный блок памяти, из которого необходимо выполнить копирование.
        /// </param>
        /// <param name="size">
        /// Размер копируемых данных в байтах.
        /// </param>
        /// <remarks>
        /// Допускается перекрытие блоков памяти <paramref name="destination"/> и <paramref name="source"/>.
        /// </remarks>
        [LibraryImport("kernel32.dll")]
        [SuppressUnmanagedCodeSecurity]
        public static partial void RtlMoveMemory(void* destination, void* source, ulong size);

        /// <summary>
        /// Заполняет область памяти указанным значением.
        /// </summary>
        /// <param name="destination">
        /// Указатель на блок памяти, который необходимо заполнить.
        /// </param>
        /// <param name="size">
        /// Размер заполняемого блока памяти в байтах.
        /// </param>
        /// <param name="fill">
        /// Значение, которым заполняется каждый байт в блоке памяти.
        /// </param>
        [LibraryImport("kernel32.dll")]
        [SuppressUnmanagedCodeSecurity]
        public static partial void RtlFillMemory(void* destination, ulong size, int fill);

        /// <summary>
        /// Заполняет область памяти нулевым значением.
        /// </summary>
        /// <param name="destination">
        /// Указатель на блок памяти, который необходимо заполнить.
        /// </param>
        /// <param name="size">
        /// Размер заполняемого блока памяти в байтах.
        /// </param>
        [LibraryImport("kernel32.dll")]
        [SuppressUnmanagedCodeSecurity]
        public static partial void RtlZeroMemory(void* destination, ulong size);
    }
}
