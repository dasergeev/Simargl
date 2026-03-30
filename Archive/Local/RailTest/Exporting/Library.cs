using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;

namespace RailTest.Exporting
{
    /// <summary>
    /// Представляет неуправляемую библиотеку.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    public class Library : Ancestor
    {
        /// <summary>
        /// Поле для хранения дескриптора библиотеки.
        /// </summary>
        [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
        private readonly IntPtr _Handle;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="name">
        /// Имя библиотеки.
        /// </param>
        /// <exception cref="DllNotFoundException">
        /// Библиотека не найдена.
        /// </exception>
        public Library(string name)
        {
            _Handle = LoadLibraryW(name);
            if (_Handle == IntPtr.Zero)
            {
                throw new DllNotFoundException("Не найдена библиотека \"" + name + "\".");
            }
        }

        /// <summary>
        /// Возвращает метод.
        /// </summary>
        /// <param name="name">
        /// Имя метода.
        /// </param>
        /// <returns>
        /// Делегат, вызывающий метод.
        /// </returns>
        public T GetMethod<T>(string name)
        {
            IntPtr address = GetProcAddress(_Handle, name);
            if (address == IntPtr.Zero)
            {
                throw new InvalidOperationException("Не найдена функция \"" + name + "\".");
            }
            return Marshal.GetDelegateForFunctionPointer<T>(address);
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern IntPtr AddDllDirectory(string NewDirectory);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern int DisableThreadLibraryCalls(IntPtr hLibModule);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern int FreeLibrary(IntPtr hLibModule);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern void FreeLibraryAndExitThread(IntPtr hLibModule, uint dwExitCode);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern uint GetDllDirectoryW(uint nBufferLength, string lpBuffer);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern uint GetModuleFileNameW(IntPtr hModule, string lpFilename, uint nSize);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern IntPtr GetModuleHandleW(string lpModuleName);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        unsafe private static extern int GetModuleHandleExW(uint dwFlags, string lpModuleName, IntPtr* phModule);

        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [SuppressMessage("Microsoft.Globalization", "CA2101:SpecifyMarshalingForPInvokeStringArguments", MessageId = "1")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Ansi)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        /// <summary>
        /// Загружает указанный модуль в адресное пространство вызывающего процесса.
        /// Загрузка модуля может стать причиной загрузки других модулей.
        /// </summary>
        /// <param name="lpLibFileName">
        /// <para>Имя модуля. Это может быть либо библиотечный модуль (файл .dll), либо исполняемый модуль (файл .exe).</para>
        /// <para>Если в строке указан полный путь, функция ищет только этот путь для модуля.</para>
        /// <para>Если в строке указан относительный путь или имя модуля без пути,
        /// функция использует стандартную стратегию поиска для поиска модуля.</para>
        /// <para>При указании пути обязательно используйте обратную косую черту (\), а не прямую косую черту (/).</para>
        /// <para>Если в строке указано имя модуля без пути и расширение имени файла опущено,
        /// функция добавляет расширение библиотеки DLL по умолчанию к имени модуля.
        /// Чтобы функция не добавляла .dll к имени модуля, добавьте символ конечной точки (.) В строку имени модуля.</para>
        /// </param>
        /// <returns>
        /// <para>Если функция завершается успешно, возвращаемое значение является дескриптором модуля.</para>
        /// <para>Если функция завершается ошибкой, возвращаемое значение равно <see cref="IntPtr.Zero"/>.</para>
        /// </returns>
        /// <remarks>
        /// <para>Если <paramref name="lpLibFileName"/> не содержит путь
        /// и существует более одного загруженного модуля с одинаковым базовым именем и расширением,
        /// функция возвращает дескриптор модуля, который был загружен первым.</para>
        /// <para>Если путь не указан, функция выполняет поиск загруженных модулей,
        /// базовое имя которых совпадает с базовым именем загружаемого модуля.
        /// Если имя совпадает, загрузка успешна. В противном случае функция ищет файл.</para>
        /// <para>Первый искомый каталог - это каталог, содержащий файл изображения, используемый для создания вызывающего процесса (для получения дополнительной информации см. Функцию CreateProcess). Это позволяет находить файлы частной динамической библиотеки (DLL), связанные с процессом, без добавления установленного каталога процесса в переменную среды PATH. Если указан относительный путь, весь относительный путь добавляется к каждому токену в списке путей поиска DLL. Чтобы загрузить модуль из относительного пути без поиска другого пути, используйте GetFullPathName, чтобы получить не относительный путь, и вызовите LoadLibrary с не относительным путем. Для получения дополнительной информации о порядке поиска DLL см. Порядок поиска в библиотеке Dynamic-Link.</para>
        /// <para>Путь поиска можно изменить с помощью функции <see cref="SetDllDirectoryW"/>.
        /// Это решение рекомендуется вместо жесткого кодирования полного пути к DLL.</para>
        /// <para>Если указан путь и существует файл перенаправления для приложения,
        /// функция выполняет поиск модуля в каталоге приложения.
        /// Если модуль существует в каталоге приложения, <see cref="LoadLibraryW"/> игнорирует указанный путь
        /// и загружает модуль из каталога приложения.
        /// Если модуль не существует в каталоге приложения, <see cref="LoadLibraryW"/> загружает модуль из указанного каталога.</para>
        /// <para>Если вы вызываете <see cref="LoadLibraryW"/> с именем сборки без указания пути,
        /// и сборка указана в совместимом с системой манифесте, вызов автоматически перенаправляется в параллельную сборку.</para>
        /// <para>Система поддерживает подсчет ссылок для каждого загруженного модуля.
        /// Вызов <see cref="LoadLibraryW"/> увеличивает счетчик ссылок.
        /// Вызов функции <see cref="FreeLibrary"/> или <see cref="FreeLibraryAndExitThread"/> уменьшает счетчик ссылок.
        /// Система выгружает модуль, когда его счетчик ссылок достигает нуля или когда процесс завершается (независимо от счетчика ссылок).</para>
        /// </remarks>
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern IntPtr LoadLibraryW(string lpLibFileName);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern IntPtr LoadLibraryExW(string lpLibFileName, IntPtr hFile, uint dwFlags);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern IntPtr LoadPackagedLibrary(string lpwLibFileName, uint Reserved);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern int RemoveDllDirectory(IntPtr Cookie);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern int SetDefaultDllDirectories(uint DirectoryFlags);

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, BestFitMapping = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        [HandleProcessCorruptedStateExceptions]
        [SuppressMessage("Стиль", "IDE1006:Стили именования", Justification = "Оригинальное имя.")]
        private static extern int SetDllDirectoryW(string lpPathName);
    }
}








//namespace NativeLibraryLoader

//{

//    internal static class Kernel32

//    {

//        [DllImport("kernel32")]

//        public static extern IntPtr LoadLibrary(string fileName);



//        [DllImport("kernel32")]

//        public static extern IntPtr GetProcAddress(IntPtr module, string procName);



//        [DllImport("kernel32")]

//        public static extern int FreeLibrary(IntPtr module);

//    }

//}

//namespace NativeLibraryLoader

//{

//    internal static class Libdl

//    {

//        private const string LibName = "libdl";



//        public const int RTLD_NOW = 0x002;



//        [DllImport(LibName)]

//        public static extern IntPtr dlopen(string fileName, int flags);



//        [DllImport(LibName)]

//        public static extern IntPtr dlsym(IntPtr handle, string name);



//        [DllImport(LibName)]

//        public static extern int dlclose(IntPtr handle);



//        [DllImport(LibName)]

//        public static extern string dlerror();

//    }

//}

//namespace NativeLibraryLoader

//{

//    /// <summary>
//    /// Exposes functionality for loading native libraries and function pointers.
//    /// </summary>
//    public abstract class LibraryLoader

//    {

//        /// <summary>
//        /// Loads a native library by name and returns an operating system handle to it.
//        /// </summary>
//        /// <param name="name">The name of the library to open.</param>
//        /// <returns>The operating system handle for the shared library.</returns>
//        public IntPtr LoadNativeLibrary(string name)

//        {
//            return LoadNativeLibrary(name, PathResolver.Default);
//        }



//        /// <summary>
//        /// Loads a native library by name and returns an operating system handle to it.
//        /// </summary>
//        /// <param name="names">An ordered list of names. Each name is tried in turn, until the library is successfully loaded.
//        /// </param>
//        /// <returns>The operating system handle for the shared library.</returns>
//        public IntPtr LoadNativeLibrary(string[] names)
//        {
//            return LoadNativeLibrary(names, PathResolver.Default);
//        }



//        /// <summary>
//        /// Loads a native library by name and returns an operating system handle to it.
//        /// </summary>
//        /// <param name="name">The name of the library to open.</param>
//        /// <param name="pathResolver">The path resolver to use.</param>
//        /// <returns>The operating system handle for the shared library.</returns>
//        public IntPtr LoadNativeLibrary(string name, PathResolver pathResolver)

//        {

//            if (string.IsNullOrEmpty(name))

//            {

//                throw new ArgumentException("Parameter must not be null or empty.", nameof(name));

//            }



//            IntPtr ret = LoadWithResolver(name, pathResolver);



//            if (ret == IntPtr.Zero)

//            {

//                throw new FileNotFoundException("Could not find or load the native library: " + name);

//            }



//            return ret;

//        }



//        /// <summary>
//        /// Loads a native library by name and returns an operating system handle to it.
//        /// </summary>
//        /// <param name="names">An ordered list of names. Each name is tried in turn, until the library is successfully loaded.
//        /// </param>
//        /// <param name="pathResolver">The path resolver to use.</param>
//        /// <returns>The operating system handle for the shared library.</returns>
//        public IntPtr LoadNativeLibrary(string[] names, PathResolver pathResolver)

//        {

//            if (names == null || names.Length == 0)

//            {

//                throw new ArgumentException("Parameter must not be null or empty.", nameof(names));

//            }



//            IntPtr ret = IntPtr.Zero;

//            foreach (string name in names)

//            {

//                ret = LoadWithResolver(name, pathResolver);

//                if (ret != IntPtr.Zero)

//                {

//                    break;

//                }

//            }



//            if (ret == IntPtr.Zero)

//            {

//                throw new FileNotFoundException($"Could not find or load the native library from any name: [ {string.Join(", ", names)} ]");

//            }



//            return ret;

//        }



//        private IntPtr LoadWithResolver(string name, PathResolver pathResolver)

//        {

//            foreach (string loadTarget in pathResolver.EnumeratePossibleLibraryLoadTargets(name))

//            {

//                if (!Path.IsPathRooted(loadTarget) || File.Exists(loadTarget))

//                {

//                    IntPtr ret = CoreLoadNativeLibrary(loadTarget);

//                    if (ret != IntPtr.Zero)

//                    {

//                        return ret;

//                    }

//                }

//            }



//            return IntPtr.Zero;

//        }



//        /// <summary>
//        /// Loads a function pointer out of the given library by name.
//        /// </summary>
//        /// <param name="handle">The operating system handle of the opened shared library.</param>
//        /// <param name="functionName">The name of the exported function to load.</param>
//        /// <returns>A pointer to the loaded function.</returns>
//        public IntPtr LoadFunctionPointer(IntPtr handle, string functionName)

//        {

//            if (string.IsNullOrEmpty(functionName))

//            {

//                throw new ArgumentException("Parameter must not be null or empty.", nameof(functionName));

//            }



//            return CoreLoadFunctionPointer(handle, functionName);

//        }



//        /// <summary>
//        /// Frees the library represented by the given operating system handle.
//        /// </summary>
//        /// <param name="handle">The handle of the open shared library.</param>
//        public void FreeNativeLibrary(IntPtr handle)

//        {

//            if (handle == IntPtr.Zero)

//            {

//                throw new ArgumentException("Parameter must not be zero.", nameof(handle));

//            }



//            CoreFreeNativeLibrary(handle);

//        }



//        /// <summary>
//        /// Loads a native library by name and returns an operating system handle to it.
//        /// </summary>
//        /// <param name="name">The name of the library to open. This parameter must not be null or empty.</param>
//        /// <returns>The operating system handle for the shared library.
//        /// If the library cannot be loaded, IntPtr.Zero should be returned.</returns>
//        protected abstract IntPtr CoreLoadNativeLibrary(string name);



//        /// <summary>
//        /// Frees the library represented by the given operating system handle.
//        /// </summary>
//        /// <param name="handle">The handle of the open shared library. This must not be zero.</param>
//        protected abstract void CoreFreeNativeLibrary(IntPtr handle);



//        /// <summary>
//        /// Loads a function pointer out of the given library by name.
//        /// </summary>
//        /// <param name="handle">The operating system handle of the opened shared library. This must not be zero.</param>
//        /// <param name="functionName">The name of the exported function to load. This must not be null or empty.</param>
//        /// <returns>A pointer to the loaded function.</returns>
//        protected abstract IntPtr CoreLoadFunctionPointer(IntPtr handle, string functionName);

//        /// <summary>
//        /// Returns a default library loader for the running operating system.
//        /// </summary>
//        /// <returns>A LibraryLoader suitable for loading libraries.</returns>
//        public static LibraryLoader GetPlatformDefaultLoader()

//        {

//            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))

//            {

//                return new Win32LibraryLoader();

//            }

//            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))

//            {

//                return new UnixLibraryLoader();

//            }



//            throw new PlatformNotSupportedException("This platform cannot load native libraries.");

//        }

//        private class Win32LibraryLoader : LibraryLoader
//        {

//            protected override void CoreFreeNativeLibrary(IntPtr handle)

//            {

//                Kernel32.FreeLibrary(handle);

//            }



//            protected override IntPtr CoreLoadFunctionPointer(IntPtr handle, string functionName)

//            {

//                return Kernel32.GetProcAddress(handle, functionName);

//            }



//            protected override IntPtr CoreLoadNativeLibrary(string name)

//            {

//                return Kernel32.LoadLibrary(name);

//            }

//        }



//        private class UnixLibraryLoader : LibraryLoader

//        {

//            protected override void CoreFreeNativeLibrary(IntPtr handle)

//            {

//                Libdl.dlclose(handle);

//            }



//            protected override IntPtr CoreLoadFunctionPointer(IntPtr handle, string functionName)

//            {

//                return Libdl.dlsym(handle, functionName);

//            }



//            protected override IntPtr CoreLoadNativeLibrary(string name)

//            {

//                return Libdl.dlopen(name, Libdl.RTLD_NOW);

//            }

//        }

//    }

//}


//namespace NativeLibraryLoader

//{

//    /// <summary>
//    /// Represents a native shared library opened by the operating system.
//    /// This type can be used to load native function pointers by name.
//    /// </summary>
//    public class NativeLibrary : IDisposable

//    {

//        private static readonly LibraryLoader s_platformDefaultLoader = LibraryLoader.GetPlatformDefaultLoader();

//        private readonly LibraryLoader _loader;



//        /// <summary>
//        /// The operating system handle of the loaded library.
//        /// </summary>
//        public IntPtr Handle { get; }



//        /// <summary>
//        /// Constructs a new NativeLibrary using the platform's default library loader.
//        /// </summary>
//        /// <param name="name">The name of the library to load.</param>
//        public NativeLibrary(string name) : this(name, s_platformDefaultLoader, PathResolver.Default)
//        {

//        }



//        /// <summary>
//        /// Constructs a new NativeLibrary using the platform's default library loader.
//        /// </summary>
//        /// <param name="names">An ordered list of names to attempt to load.</param>
//        public NativeLibrary(string[] names) : this(names, s_platformDefaultLoader, PathResolver.Default)

//        {

//        }


//        /// <summary>
//        /// Constructs a new NativeLibrary using the specified library loader.
//        /// </summary>
//        /// <param name="name">The name of the library to load.</param>
//        /// <param name="loader">The loader used to open and close the library, and to load function pointers.</param>
//        public NativeLibrary(string name, LibraryLoader loader) : this(name, loader, PathResolver.Default)

//        {

//        }



//        /// <summary>
//        /// Constructs a new NativeLibrary using the specified library loader.
//        /// </summary>
//        /// <param name="names">An ordered list of names to attempt to load.</param>
//        /// <param name="loader">The loader used to open and close the library, and to load function pointers.</param>
//        public NativeLibrary(string[] names, LibraryLoader loader) : this(names, loader, PathResolver.Default)
//        {

//        }

//        /// <summary>
//        /// Constructs a new NativeLibrary using the specified library loader.
//        /// </summary>
//        /// <param name="name">The name of the library to load.</param>
//        /// <param name="loader">The loader used to open and close the library, and to load function pointers.</param>
//        /// <param name="pathResolver">The path resolver, used to identify possible load targets for the library.</param>
//        public NativeLibrary(string name, LibraryLoader loader, PathResolver pathResolver)

//        {

//            _loader = loader;

//            Handle = _loader.LoadNativeLibrary(name, pathResolver);

//        }



//        /// <summary>
//        /// Constructs a new NativeLibrary using the specified library loader.
//        /// </summary>
//        /// <param name="names">An ordered list of names to attempt to load.</param>
//        /// <param name="loader">The loader used to open and close the library, and to load function pointers.</param>
//        /// <param name="pathResolver">The path resolver, used to identify possible load targets for the library.</param>
//        public NativeLibrary(string[] names, LibraryLoader loader, PathResolver pathResolver)

//        {

//            _loader = loader;

//            Handle = _loader.LoadNativeLibrary(names, pathResolver);

//        }


//        /// <summary>
//        /// Loads a function whose signature matches the given delegate type's signature.
//        /// </summary>
//        /// <typeparam name="T">The type of delegate to return.</typeparam>
//        /// <param name="name">The name of the native export.</param>
//        /// <returns>A delegate wrapping the native function.</returns>
//        /// <exception cref="InvalidOperationException">Thrown when no function with the given name
//        /// is exported from the native library.</exception>
//        public T LoadFunction<T>(string name)

//        {

//            IntPtr functionPtr = _loader.LoadFunctionPointer(Handle, name);

//            if (functionPtr == IntPtr.Zero)

//            {

//                throw new InvalidOperationException($"No function was found with the name {name}.");

//            }



//            return Marshal.GetDelegateForFunctionPointer<T>(functionPtr);

//        }



//        /// <summary>
//        /// Frees the native library. Function pointers retrieved from this library will be void.
//        /// </summary>
//        public void Dispose()

//        {

//            _loader.FreeNativeLibrary(Handle);

//        }

//    }

//}


//namespace NativeLibraryLoader
//{
//    /// <summary>
//    /// Enumerates possible library load targets.
//    /// </summary>
//    public abstract class PathResolver
//    {
//        /// <summary>
//        /// Returns an enumerator which yields possible library load targets, in priority order.
//        /// </summary>
//        /// <param name="name">The name of the library to load.</param>
//        /// <returns>An enumerator yielding load targets.</returns>
//        public abstract IEnumerable<string> EnumeratePossibleLibraryLoadTargets(string name);

//        /// <summary>
//        /// Gets a default path resolver.
//        /// </summary>
//        public static PathResolver Default { get; } = new DefaultPathResolver();
//    }

//    /// <summary>
//    /// Enumerates possible library load targets. This default implementation returns the following load targets:
//    /// First: The library contained in the applications base folder.
//    /// Second: The simple name, unchanged.
//    /// Third: The library as resolved via the default DependencyContext, in the default nuget package cache folder.
//    /// </summary>
//    public class DefaultPathResolver : PathResolver
//    {
//        /// <summary>
//        /// Returns an enumerator which yieilds possible library load targets, in priority order.
//        /// </summary>
//        /// <param name="name">The name of the library to load.</param>
//        /// <returns>An enumerator yielding load targets.</returns>
//        public override IEnumerable<string> EnumeratePossibleLibraryLoadTargets(string name)
//        {
//            yield return Path.Combine(AppContext.BaseDirectory, name);
//            yield return name;
//            if (TryLocateNativeAssetFromDeps(name, out string depsResolvedPath))
//            {
//                yield return depsResolvedPath;
//            }
//        }

//        private bool TryLocateNativeAssetFromDeps(string name, out string depsResolvedPath)
//        {
//            DependencyContext defaultContext = DependencyContext.Default;

//            if (defaultContext == null)

//            {

//                depsResolvedPath = null;

//                return false;

//            }



//            string currentRID = Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment.GetRuntimeIdentifier();

//            List<string> allRIDs = new List<string>();

//            allRIDs.Add(currentRID);

//            if (!AddFallbacks(allRIDs, currentRID, defaultContext.RuntimeGraph))

//            {

//                string guessedFallbackRID = GuessFallbackRID(currentRID);

//                if (guessedFallbackRID != null)

//                {

//                    allRIDs.Add(guessedFallbackRID);

//                    AddFallbacks(allRIDs, guessedFallbackRID, defaultContext.RuntimeGraph);

//                }

//            }



//            foreach (string rid in allRIDs)

//            {

//                foreach (var runtimeLib in defaultContext.RuntimeLibraries)

//                {

//                    foreach (var nativeAsset in runtimeLib.GetRuntimeNativeAssets(defaultContext, rid))

//                    {

//                        if (Path.GetFileName(nativeAsset) == name || Path.GetFileNameWithoutExtension(nativeAsset) == name)

//                        {

//                            string fullPath = Path.Combine(

//                                GetNugetPackagesRootDirectory(),

//                                runtimeLib.Name.ToLowerInvariant(),

//                                runtimeLib.Version, nativeAsset);

//                            fullPath = Path.GetFullPath(fullPath);

//                            depsResolvedPath = fullPath;

//                            return true;

//                        }

//                    }

//                }

//            }



//            depsResolvedPath = null;

//            return false;

//        }



//        private string GuessFallbackRID(string actualRuntimeIdentifier)

//        {

//            if (actualRuntimeIdentifier == "osx.10.13-x64")

//            {

//                return "osx.10.12-x64";

//            }

//            else if (actualRuntimeIdentifier.StartsWith("osx"))

//            {

//                return "osx-x64";

//            }



//            return null;

//        }



//        private bool AddFallbacks(List<string> fallbacks, string rid, IReadOnlyList<RuntimeFallbacks> allFallbacks)

//        {

//            foreach (RuntimeFallbacks fb in allFallbacks)

//            {

//                if (fb.Runtime == rid)

//                {

//                    fallbacks.AddRange(fb.Fallbacks);

//                    return true;

//                }

//            }



//            return false;

//        }



//        private string GetNugetPackagesRootDirectory()

//        {

//            // TODO: Handle alternative package directories, if they are configured.

//            return Path.Combine(GetUserDirectory(), ".nuget", "packages");

//        }



//        private string GetUserDirectory()

//        {

//            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))

//            {

//                return Environment.GetEnvironmentVariable("USERPROFILE");

//            }

//            else

//            {

//                return Environment.GetEnvironmentVariable("HOME");

//            }

//        }

//    }

//}
