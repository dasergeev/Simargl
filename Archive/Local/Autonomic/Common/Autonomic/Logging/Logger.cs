using RailTest.Memory;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailTest.Satellite.Autonomic.Logging
{
    /// <summary>
    /// Представляет самописца.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Постоянная для хранения максимальной длины строки.
        /// </summary>
        public const int MaxLineLength = 256;

        /// <summary>
        /// Постоянная для хранения количества строк.
        /// </summary>
        public const int LineCount = 1024;

        /// <summary>
        /// Постоянная для хранения размера строки.
        /// </summary>
        private const int HeaderSize = 8;

        /// <summary>
        /// Постоянная для хранения размера строки.
        /// </summary>
        private const int LineSize = MaxLineLength * 2;

        /// <summary>
        /// Постоянная для хранения размера буфера в памяти.
        /// </summary>
        public const int FullSize = HeaderSize + LineCount * LineSize;

        /// <summary>
        /// Постоянная для хранения имени писателя по умолчанию.
        /// </summary>
        public const string DefaultWriterName = "Default";

        /// <summary>
        /// Поле для хранения кодировки.
        /// </summary>
        private static readonly Encoding Encoding = Encoding.Unicode;

        /// <summary>
        /// Поле для хранения общей памяти.
        /// </summary>
        private readonly SharedMemory _Memory;

        /// <summary>
        /// Поле для хранения объекта доступа к памяти.
        /// </summary>
        private readonly MemoryMappedViewAccessor _View;

        /// <summary>
        /// Поле для хранения объекта доступа к памяти.
        /// </summary>
        private readonly MemoryMappedViewAccessor _Header;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="writerName">
        /// Имя писателя.
        /// </param>
        public Logger(string writerName)
        {
            if (string.IsNullOrEmpty(writerName))
            {
                WriterName = DefaultWriterName;
            }
            else
            {
                WriterName = writerName;
            }
            _Memory = new SharedMemory("Logger", FullSize);
            _Header = _Memory.CreateViewAccessor(0, HeaderSize);
            _View = _Memory.CreateViewAccessor(HeaderSize, FullSize);
            Mutex = _Memory.Mutex;
        }

        /// <summary>
        /// Возвращает имя писателя.
        /// </summary>
        public string WriterName { get; }

        /// <summary>
        /// Возвращает примитив межпроцессорной синхронизации.
        /// </summary>
        private Mutex Mutex { get; }

        /// <summary>
        /// Возвращает индекс текущей строки.
        /// </summary>
        public int LineIndex
        {
            get
            {
                Mutex.WaitOne();
                try
                {
                    return (int)_Header.ReadInt64(0);
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }
            }
            private set
            {
                Mutex.WaitOne();
                try
                {
                    _Header.Write(0, (long)value);
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }
            }
        }

        /// <summary>
        /// Записывает строку.
        /// </summary>
        /// <param name="message">
        /// Строка.
        /// </param>
        public void WriteLine(string message)
        {
            try
            {
                message = $"{DateTime.Now:HH:mm:ss} {WriterName}: {message ?? string.Empty}";
                if (message.Length > MaxLineLength)
                {
                    message = message.Substring(0, MaxLineLength);
                }
                while (Encoding.GetByteCount(message) > LineSize)
                {
                    message = message.Substring(0, message.Length - 1);
                }
                byte[] buffer = new byte[LineSize];
                Encoding.GetBytes(message, 0, message.Length, buffer, 0);
                Mutex.WaitOne();
                try
                {
                    _View.WriteArray(LineIndex * LineSize, buffer, 0, LineSize);
                    LineIndex = (LineIndex + 1) % LineCount;
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }
            }
            catch (FormatException)
            {

            }
            catch (EncoderFallbackException)
            {

            }
        }

        /// <summary>
        /// Выполняет чтение строки.
        /// </summary>
        /// <param name="index">
        /// Индекс строки.
        /// </param>
        /// <returns>
        /// Строка.
        /// </returns>
        public string ReadLine(int index)
        {
            string message = string.Empty;
            try
            {
                index %= LineCount;
                if (index < 0)
                {
                    index += LineCount;
                }

                byte[] buffer = new byte[LineSize];
                Mutex.WaitOne();
                try
                {
                    _View.ReadArray(index * LineSize, buffer, 0, LineSize);
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }

                int count = LineCount;
                for (int i = 0; i != LineCount - 1; ++i)
                {
                    if (buffer[i] == 0 && buffer[i + 1] == 0)
                    {
                        count = i + 1;
                        break;
                    }
                }

                message = Encoding.GetString(buffer, 0, count);
            }
            catch (DecoderFallbackException)
            {
                
            }
            return message;
        }
    }
}
