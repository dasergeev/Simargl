using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Apeiron.ParserSensorsFiles
{
    /// <summary>
    /// Представляет класс преобразование файлов MTP.
    /// </summary>
    public static class MtpParser
    {
        /// <summary>
        /// Постоянная, определяющее количество каналов передаваемых системой.
        /// </summary>
        public const int ChannelCount = 16;

        /// <summary>
        /// Инициализирует объект класса.
        /// </summary>
        /// <param name="folderPath">
        /// Путь до папки с данными.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="folderPath"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// Не достаточно прав для доступа к объекту.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// В параметре <paramref name="folderPath"/> есть не допустимые символы для пути.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// Параметр <paramref name="folderPath"/> превышает допустимую длинну пути.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// В параметре <paramref name="folderPath"/> передан недействительный путь.
        /// </exception>
        public static async Task<float[][]> LoadFromFolderAsync(string folderPath)
        {
            //  Проверка, что передана не пустая ссылка.
            folderPath = Check.IsNotNull(folderPath, nameof(folderPath));

            //  Получение информации о директории.
            DirectoryInfo directory = new(folderPath);

            //  Получение списка файлов в директории
            var files = directory.EnumerateFiles().OrderBy(f => f.CreationTime).ToList();

            //  Инициализация счетчика длинны всех файлов.
            long lengthOfSignal = 0;

            //  Цикл по всем файлам.
            foreach (var file in files)
            {
                //  Подсчет длины всех файлов.
                lengthOfSignal += file.Length / (ChannelCount * 2);
            }

            //  Инициализация первого измерения массива.
            float[][] data = new float[ChannelCount][];

            //  Цикл по всем каналам.
            for (int i =0; i<ChannelCount;i++)
            {
                //  Инициализация массива каналов.
                data[i] = new float[lengthOfSignal];
            }

            //  Инициализация списка задач.
            List<Task> tasks = new();

            //  Инициализация общего счетчика.
            long dataIndex = 0;

            //  Цикл по всем файлам.
            foreach (var file in files)
            {
                //   Откртие потока файла на чтение
                //using FileStream stream = new(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);

                //  Инициализация позиции записи в буфер.
                long index = dataIndex;

                //  Получение счетчика длины файла
                long length = file.Length / (ChannelCount * 2);

                //  Добавление в список задачи и запуск задачи.
                tasks.Add(Task.Run(() =>
                {
                    var fileData = LoadFromFile(file.FullName);

                    //  Цикл по всем каналам
                    for (int i = 0; i < ChannelCount; i++)
                    {
                        //  Копирование данных файла.
                        Array.Copy(fileData[i], 0, data[i], (int)index, (int)length);
                    }
                }));

                //  Итерация счетчика.
                dataIndex += length;
            }

            //  Флаг завершения всех задач.
            bool isAllTaskComplited = true;

            //  Пока задачи не выполнены.
            do
            {
                //  Ожидание
                await Task.Delay(50).ConfigureAwait(false);

                //  Сброс флага.
                isAllTaskComplited = true;

                //  Проверка и анализ выполнения предыдущей операции
                foreach (var one in tasks)
                {
                    //  Проверка завершения.
                    if (!one.IsCompleted)
                    {
                        //  Установка флага.
                        isAllTaskComplited = false;

                        //  Выход из цикла.
                        break;
                    }
                }
            }
            while (!isAllTaskComplited);

            return data;
        }

        /// <summary>
        /// Инициализирует объект класса.
        /// </summary>
        /// <param name="filePath">
        /// Путь до папки с данными.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="filePath"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// Не достаточно прав для доступа к объекту.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// В параметре <paramref name="filePath"/> есть не допустимые символы для пути.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// Параметр <paramref name="filePath"/> превышает допустимую длинну пути.
        /// </exception>
        public static float[][] LoadFromFile(string filePath)
        {
            //  Проверка, что передана не пустая ссылка.
            filePath = Check.IsNotNull(filePath, nameof(filePath));

            //  Получение информации о файле.
            FileInfo file = new(filePath);

            //  Чтение из файла.
            var array = File.ReadAllBytes(file.FullName);

            return LoadFromArray(array);
        }

        /// <summary>
        /// Представляет функцию загрзуки из массива.
        /// </summary>
        /// <param name="array"></param>
        public static float[][] LoadFromArray(byte[] array)
        {
            //  Создание потока.
            using MemoryStream memory = new(array);

            //   Создание читателя.
            using var reader = new BinaryReader(memory, System.Text.Encoding.UTF8, true);

            //  Получение счетчика длины файла
            long length = array.Length / (ChannelCount * 2);

            //  Инициализация первого измерения массива.
            float[][] data = new float[ChannelCount][];

            //  Цикл по всем каналам.
            for (int i = 0; i < ChannelCount; i++)
            {
                //  Инициализация массива каналов.
                data[i] = new float[length];
            }
            //  Цикл по всему файлу.
            for (long j = 0; j < length; j++)
            {
                //  Цикл по всем каналам
                for (int i = 0; i < ChannelCount; i++)
                {
                    //  Конвертация и сохранение данных из файла.
                    data[i][j] = Convert(reader.ReadUInt16());
                }
            }

            return data;
        }





        /// <summary>
        /// Представляет функцию конвертации значения ADC в мВ.
        /// </summary>
        /// <param name="value">Значение ADC.</param>
        /// <returns>Значение датчика в мВ.</returns>
        private static float Convert(ushort value)
        {
            //  Проверка значения
            if(value > 0x8000)
            {
                //  Возврат и пересчет значения.
                return ((float)(value - 0x8000))  / 0x8000 * (float)1.25;
            }
            else
            {
                //  Возврат и пересчет значения.
                return ((float)(0x8000 - value)) / 0x8000 * ((float)-1.25);
            }
        }
    }
}