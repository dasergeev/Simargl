using RailTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Представляет контекст устройства.
    /// </summary>
    internal class DeviceContext : Ancestor, IDisposable
    {
        /// <summary>
        /// Поле для хранения дескриптора окна, которому принадлежит контекст устройства.
        /// </summary>
        private readonly IntPtr _WindowHandle;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="windowHandle">
        /// Дескриптор окна, которому принадлежит контекста устройства.
        /// </param>
        /// <exception cref="ArgumentException">
        /// В параметре <paramref name="windowHandle"/> передан недействительный дескриптор.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось получить дескриптор контекста устройства.
        /// </exception>
        public DeviceContext(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
            {
                throw new ArgumentException("windowHandle", "Передан недействительный дескриптор.");
            }
            _WindowHandle = windowHandle;
            Handle = Import.GetDC(windowHandle);
            if (Handle == DeviceContextHandle.Invalid)
            {
                throw new InvalidOperationException("Не удалось получить дескриптор контекста устройства.");
            }
        }

        /// <summary>
        /// Разрушает объект.
        /// </summary>
        ~DeviceContext()
        {
            if (Handle != DeviceContextHandle.Invalid)
            {
                Import.ReleaseDC(_WindowHandle, Handle);
                Handle = DeviceContextHandle.Invalid;
            }
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением,
        /// высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            if (Handle != DeviceContextHandle.Invalid)
            {
                Import.ReleaseDC(_WindowHandle, Handle);
                Handle = DeviceContextHandle.Invalid;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Возвращает дескриптор контекста устройства.
        /// </summary>
        public DeviceContextHandle Handle { get; private set; }

        /// <summary>
        /// Выполняет проверку дескриптора контекста устройства.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        internal void CheckHandle()
        {
            if (Handle == DeviceContextHandle.Invalid)
            {
                throw new ObjectDisposedException("DeviceContext", "Произошла попытка выполнения операции над удаленным объектом.");
            }
        }

        /// <summary>
        /// Выполняет попытку сопоставить соответствующий формат пикселя, поддерживаемый контекстом устройства, с заданной спецификацией формата пикселя.
        /// </summary>
        /// <param name="pixelFormatDescriptor">
        /// Запрашиваемый формат пикселей.
        /// </param>
        /// <returns>
        /// Индекс формата пикселя, который наиболее близко соответствует данному дескриптору формата пикселя.
        /// </returns>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось определить индекс формата пикселя.
        /// </exception>
        public unsafe int ChoosePixelFormat(PixelFormatDescriptor pixelFormatDescriptor)
        {
            CheckHandle();
            int pixelFormat = Import.ChoosePixelFormat(Handle, &pixelFormatDescriptor);
            if (pixelFormat == 0)
            {
                throw new InvalidOperationException("Не удалось определить индекс формата пикселя.");
            }
            return pixelFormat;
        }

        /// <summary>
        /// Получает информацию о формате пикселей.
        /// </summary>
        /// <param name="pixelFormat">
        /// Индекс, который определяет формат пикселя.
        /// </param>
        /// <param name="pixelFormatDescriptor">
        /// Указатель на структуру <see cref="PixelFormatDescriptor"/>, члены которой функция устанавливает данными формата пикселей.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="pixelFormat"/> передано отрицательное или равное нулю значение.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось получить информацию о формате пикселей.
        /// </exception>
        public unsafe void DescribePixelFormat(int pixelFormat, PixelFormatDescriptor pixelFormatDescriptor)
        {
            CheckHandle();
            if (pixelFormat <= 0)
            {
                throw new ArgumentOutOfRangeException("pixelFormat", "Передано отрицательное или равное нулю значение.");
            }
            if (0 == Import.DescribePixelFormat(Handle, pixelFormat, PixelFormatDescriptor.StandardSize, &pixelFormatDescriptor))
            {
                throw new InvalidOperationException("Не удалось получить информацию о формате пикселей.");
            }
        }

        /// <summary>
        /// Функция устанавливает формат пикселя указанного контекста устройства в формате, указанном в индексе <paramref name="pixelFormat"/>.
        /// </summary>
        /// <param name="pixelFormat">
        /// Индекс, который идентифицирует формат пикселя для установки.
        /// </param>
        /// <param name="pixelFormatDescriptor">
        /// Указатель на структуру <see cref="PixelFormatDescriptor"/>, которая содержит спецификацию формата логического пикселя.
        /// Компонент метафайла системы использует эту структуру для записи спецификации формата логического пикселя.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="pixelFormat"/> передано отрицательное или равное нулю значение.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось устанавливает формат пикселя.
        /// </exception>
        public unsafe void SetPixelFormat(int pixelFormat, PixelFormatDescriptor pixelFormatDescriptor)
        {
            CheckHandle();
            if (pixelFormat <= 0)
            {
                throw new ArgumentOutOfRangeException("pixelFormat", "Передано отрицательное или равное нулю значение.");
            }
            if (0 == Import.SetPixelFormat(Handle, pixelFormat, &pixelFormatDescriptor))
            {
                throw new InvalidOperationException("Не удалось устанавливает формат пикселя.");
            }
        }

        /// <summary>
        /// Функция устанавливает формат пикселя указанного контекста устройства.
        /// </summary>
        /// <param name="pixelFormatDescriptor">
        /// Указатель на структуру <see cref="PixelFormatDescriptor"/>, которая содержит спецификацию формата логического пикселя.
        /// </param>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось получить информацию о формате пикселей.
        /// - или -
        /// Не удалось устанавливает формат пикселя.
        /// </exception>
        public void SetPixelFormat(PixelFormatDescriptor pixelFormatDescriptor)
        {
            int pixelFormat = ChoosePixelFormat(pixelFormatDescriptor);
            SetPixelFormat(pixelFormat, pixelFormatDescriptor);
        }

        /// <summary>
        /// Обменивает передний и задний буферы, если текущий формат пикселя для окна включает в себя задний буфер.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Произошла попытка выполнения операции над удаленным объектом.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Не удалось выполнить смену буферов.
        /// </exception>
        public void SwapBuffers()
        {
            CheckHandle();
            if (0 == Import.SwapBuffers(Handle))
            {
                throw new InvalidOperationException("Не удалось выполнить смену буферов.");
            }
        }
    }
}
