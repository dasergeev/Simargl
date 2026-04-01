using Simargl.Designing;
using System.IO;

namespace Simargl.Platform.Sensors;

internal class BinPackage
{
    /// <summary>
    /// Представляет префикс пакета.
    /// </summary>
    private static readonly uint _Prefix = 0x6E727041;

    /// <summary>
    /// Возвращает формат данных.
    /// </summary>
    public long Format { get; private set; }


    /// <summary>
    /// Возвращает формат данных.
    /// </summary>
    public long Size { get; private set; }


    /// <summary>
    /// Возвращает данные пакета.
    /// </summary>
    public byte[] Data { get; private set; }


    /// <summary>
    /// Инициализирует объект класса.
    /// </summary>
    /// <param name="format">Формат.</param>
    /// <param name="data">Данные</param>
    /// <exception cref="ArgumentNullException"> 
    /// В параметре <paramref name="data"/> передана пустая ссылка.
    /// </exception>
    public BinPackage([NoVerify]long format, byte[] data)
    {
        //  Установка формата 
        Format = format;

        //   Проверка и установка данных.
        Data = IsNotNull(data,nameof(data));

        //  Установка размера данных.
        Size = data.Length;
    }


    /// <summary>
    /// Возвращает пакет на основании массива данных
    /// </summary>
    /// <param name="array">Массив</param>
    /// <returns>Объект класса.</returns>
    /// <exception cref="ArgumentException">
    /// В параметр <paramref name="array"/> передан массив не достаточной длины.
    /// </exception>
    /// <exception cref="FormatException">
    /// Получен не допустимый префикс.
    /// </exception>
    /// <exception cref="FormatException">
    /// Получен не допустимый размер данных.
    /// </exception>
    public static BinPackage FromArray(byte[] array)
    {
        //  Создание потока памяти
        using MemoryStream memory = new(array);

        //  Проверка размера массива.
        if (array.Length < 16)
        {
            //  Выброс исключения.
            throw new ArgumentException("Массив не достаточной длинны");
        }

        //  Создание читателя.
        using BinaryReader reader = new(memory);

        //  Чтение префикса.
        uint prefix = reader.ReadUInt32();

        //  Проверка префикса.
        if (prefix != _Prefix)
        {
            //  Выброс исключения.
            throw new FormatException();
        }

        //  Чтение формата.
        uint format = reader.ReadUInt32();

        //  Чтение размера данных.
        ulong size = reader.ReadUInt64();   

        //  Проверка размера.
        if (size > 508 - 16)
        {
            //  Выброс исключения.
            throw new FormatException();
        }

        //  Чтение буфера.
        byte[] buffer = reader.ReadBytes((int)size);

        //  Создание пакета 
        BinPackage package = new(format, buffer);

        //  Возврат значения.
        return package;
    }


    /// <summary>
    /// Представляет функцию превращения данных в массив.
    /// </summary>
    /// <returns>Массив сырых данных.</returns>
    public byte[] ToArray()
    {
        //  Создание потока памяти
        using MemoryStream memory = new();

        //  Создание писателя.
        using BinaryWriter writer = new(memory);

        //  Запись префикса.
        writer.Write(_Prefix);

        //  Запись формата
        writer.Write((uint)Format);

        //  Запись формата
        writer.Write((long)Size);

        //  Запись данных.
        writer.Write(Data);

        //  Возврат значения.
        return memory.ToArray();
    }
}
