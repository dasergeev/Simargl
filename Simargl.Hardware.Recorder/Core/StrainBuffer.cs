using Microsoft.Extensions.Logging;
using Simargl.Hardware.Receiving.Net;
using System.Collections.Concurrent;
using System.IO;

namespace Simargl.Hardware.Recorder.Core;

/// <summary>
/// Представляет буфер данных тензомодуля.
/// </summary>
/// <param name="connectionKey">
/// Ключ соединения.
/// </param>
public sealed class StrainBuffer(long connectionKey)
{
    /// <summary>
    /// Поле для хранения времени получения последних данных.
    /// </summary>
    private DateTime _LastUpdateTime = DateTime.Now;

    /// <summary>
    /// Поле для хранения данных потока.
    /// </summary>
    private byte[] _StreamData = [];

    /// <summary>
    /// Текущая длина данных.
    /// </summary>
    private int _Length = 0;

    /// <summary>
    /// Возвращает ключ соединения.
    /// </summary>
    public long ConnectionKey { get; } = connectionKey;

    /// <summary>
    /// Возвращает время простоя.
    /// </summary>
    public TimeSpan Outage => DateTime.Now - _LastUpdateTime;

    /// <summary>
    /// Возвращает флаг переполнения.
    /// </summary>
    public bool IsOverflow => _Length > 16384 * 5;

    /// <summary>
    /// Асинхронно добавляет данные.
    /// </summary>
    /// <param name="result">
    /// Данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, добавляющая данные.
    /// </returns>
    public async Task AddAsync(TcpDataReceiveResult result, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Обновление времени получения последних данных.
        _LastUpdateTime = DateTime.Now;

        //  Получение данных.
        byte[] data = result.Data;

        //  Проверка размера данных.
        if (_StreamData.Length < _Length + data.Length)
        {
            //  Увеличение буфера.
            Array.Resize(ref _StreamData, _Length + data.Length);
        }

        //  Копирование данных.
        Array.Copy(data, 0, _StreamData, _Length, data.Length);

        //  Увеличение размера данных.
        _Length += data.Length;
    }

    /// <summary>
    /// Асинхронно выполняет чтение пакета.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение пакета.
    /// </returns>
    public async Task<bool> TryReadAsync(ILogger logger, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение первой позиции.
        int firstPosition = GetPosition(0);

        //  Проверка первой позиции.
        if (firstPosition < 0)
        {
            //  Пакет не найден.
            return false;
        }

        //  Проверка длины.
        if (firstPosition != 0)
        {
            //  Вывод предупреждения.
            logger.LogWarning("Получены нестандартные данные от тензомодуля: смещение {length} байт.", firstPosition);
        }

        //  Получение второй позиции.
        int secondPosition = GetPosition(firstPosition + 1);

        //  Проверка второй позиции.
        if (secondPosition < 0)
        {
            //  Пакет не найден.
            return false;
        }

        //  Определение длины пакета.
        int length = secondPosition - firstPosition;

        //  Создание массива данных.
        byte[] rawData = new byte[length];

        //  Копирование данных.
        Array.Copy(_StreamData, firstPosition, rawData, 0, length);

        //  Корректировка длины данных.
        _Length -= secondPosition;

        //  Корректировка данных потока.
        Array.Copy(_StreamData, secondPosition, _StreamData, 0, _Length);

        //  Создание потока.
        using MemoryStream stream = new(rawData);

        //  Создание средства чтения двоичных данных.
        using BinaryReader reader = new(stream);

        uint serialNumber = 0;

        if (length >= 20)
        {
            //  Чтение префикса.
            uint prefix = reader.ReadUInt32();  //  4

            //  Чтение формата.
            uint format = reader.ReadUInt32();  //  4

            //  Чтение длинны пакета.
            ulong packageSize = reader.ReadUInt64();    //  8

            //  Чтение серийного номера.
            serialNumber = reader.ReadUInt32();    //  4

            //  Нормализация серийного номера.
            byte[] serialData = BitConverter.GetBytes(serialNumber);

            //  Конвертация.
            (serialData[0], serialData[1]) = (serialData[1], serialData[0]);
            (serialData[2], serialData[3]) = (serialData[3], serialData[2]);

            //  Восстановление серийного номера.
            serialNumber = BitConverter.ToUInt32(serialData);
        }

        if (_SerialNumbers.TryAdd(serialNumber, serialNumber))
        {
            logger.LogInformation(
                "Обнаружен новый тензомодуль {number:X8}",
                serialNumber);
        }

        //  Проверка длины.
        if (length != 1409)
        {
            //  Вывод предупреждения.
            logger.LogWarning("Получены нестандартные данные от тензомодуля {number:X8}: размер {length} байт.", serialNumber, length);
        }
        else
        {
            //  Блок перехвата всех исключений.
            try
            {

                //  Чтение поля количества каналов и длины данных.
                uint nLength = reader.ReadUInt32(); //  4

                //  Получение количества каналов.
                byte channelCount = (byte)(nLength & 0xFF);

                //  Получение количества точек данных.
                uint pointCount = nLength >> 8 & 0xFFFFFF;

                //  Получение флага времени.
                byte syncFlag = reader.ReadByte();  //  1

                //  Получение времени в секундах.
                ulong timeUnix = reader.ReadUInt64();   //  8

                //  Получение младшей части времени.
                uint timeNano = reader.ReadUInt32();    //  4

                //  Получение температуры.
                float cpuTemp = reader.ReadSingle();    //  4

                //  Получение температуры.
                float sensorTemp = reader.ReadSingle(); //  4

                //  Получение напряжение питания.
                float cpuPower = reader.ReadSingle();   //  4

                //  49 байт.
                //  1360 = 4 * 340 = 4 * 4 * 85.

                //  Проверка количества каналов.
                if (channelCount != 4)
                {
                    //  Вывод предупреждения.
                    logger.LogWarning("Получены нестандартные данные от тензомодуля {number:X8}: количество каналов {channelCount}.", serialNumber, channelCount);
                }

                //  Проверка точек данных.
                if (pointCount != 85)
                {
                    //  Вывод предупреждения.
                    logger.LogWarning("Получены нестандартные данные от тензомодуля {number:X8}: количество точек {pointCount}.", serialNumber, pointCount);
                }

                //  Инициализация первого измерения массива.
                float[][] data = new float[channelCount][];

                //  Цикл по всем каналам.
                for (int i = 0; i < channelCount; i++)
                {
                    //  Инициализация массива каналов.
                    data[i] = new float[pointCount];
                }

                //  Цикл по всему файлу.
                for (long j = 0; j < pointCount; j++)
                {
                    //  Цикл по всем каналам
                    for (int i = 0; i < channelCount; i++)
                    {
                        //  Конвертация и сохранение данных из файла.
                        data[i][j] = reader.ReadSingle();
                    }
                }



                //  Добавление данных.
                Heart.Unique.Measurer.Indicators.TryAddStrain(serialNumber, data);
            }
            catch (Exception ex)
            {
                //  Вывод информации в журнал.
                logger.LogError("Произошла ошибка при разборе данных: {ex}", ex);
            }
        }

        //  Пакет прочитан.
        return true;
    }

    private readonly ConcurrentDictionary<uint, uint> _SerialNumbers = [];

    /// <summary>
    /// Возвращает начальную позицию пакета данных.
    /// </summary>
    /// <param name="begin">
    /// Начальный индекс.
    /// </param>
    /// <returns>
    /// Начальная позиция или
    /// -1 если не найдена.
    /// </returns>
    private int GetPosition(int begin)
    {
        //  Установка начальной позиции.
        int position = begin;

        //  Перебор позиций.
        while (position + 15 < _Length)
        {
            //  Проверка позиции.
            if (_StreamData[position + 0] == 0x41 &&
                _StreamData[position + 1] == 0x70 &&
                _StreamData[position + 2] == 0x72 &&
                _StreamData[position + 3] == 0x6e &&
                _StreamData[position + 4] == 0x05 &&
                _StreamData[position + 5] == 0x00 &&
                _StreamData[position + 6] == 0x00 &&
                _StreamData[position + 7] == 0x00 &&
                _StreamData[position + 8] == 0x71 &&
                _StreamData[position + 9] == 0x05 &&
                _StreamData[position + 10] == 0x00 &&
                _StreamData[position + 11] == 0x00 &&
                _StreamData[position + 12] == 0x00 &&
                _StreamData[position + 13] == 0x00 &&
                _StreamData[position + 14] == 0x00 &&
                _StreamData[position + 15] == 0x00)
            {
                //  Позиция найдена.
                return position;
            }

            //  Смещение позиции.
            position++;
        }

        //  Позиция не найдена.
        return -1;
    }
}
