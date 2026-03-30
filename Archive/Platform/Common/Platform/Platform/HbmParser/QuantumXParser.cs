using Apeiron.QuantumX;
using Apeiron.Frames;

namespace Apeiron.Platform.QuantumX
{
    /// <summary>
    /// Представляет класс разбора файлов регистрации QuantumX
    /// </summary>
    public class QuantumXParser
    {
        /// <summary>
        /// Представляет конфигурацию каналов кадра.
        /// </summary>
        private readonly QuantumParserOptions _FrameOptions;

        /// <summary>
        /// Представляет клиент устройства
        /// </summary>
        private readonly HbmDevice Devices = new();

        /// <summary>
        /// Возвращает время получения последних данных.
        /// </summary>
        internal DateTime LastReceiveDataTime { get; private set; } = DateTime.Now;

        /// <summary>
        /// Инициализирует объект класса.
        /// </summary>
        /// <param name="frameOptions">Конфигурация кадра</param>
        public QuantumXParser(QuantumParserOptions frameOptions)
        {

            //  Устанавливает конфиграцию кадра
            _FrameOptions = frameOptions;

            //  Инициализация устройства.
            Devices = new()
            {
            };
        }

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
        /// <exception cref="FormatException">
        /// Файл прочитан не до конца.
        /// </exception>
        public async Task LoadFromFolderAsync(string folderPath)
        {
            //  Проверка, что передана не пустая ссылка.
            folderPath = Check.IsNotNull(folderPath, nameof(folderPath));

            //  Получение информации о директории.
            DirectoryInfo directory = new(folderPath);

            //  Получение списка файлов в директории
            var directories = directory.EnumerateDirectories().OrderBy(f => f.LastWriteTime).ToList();

            //  Перебор всех файлов.
            foreach (DirectoryInfo dir in directories)
            {
                //  Работа с файлом.
                await LoadFromFolderAsync(dir.FullName);
            }

            //  Получение списка файлов в директории
            var files = directory.EnumerateFiles().OrderBy(f => f.LastWriteTime).ToList();

            //  Цикл по всем файлам.
            foreach (var file in files)
            {
                //  Чтение файла
                LoadFromFile(file.FullName);
            }
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
        /// <exception cref="FormatException">
        /// Файл прочитан не до конца.
        /// </exception>
        private void LoadFromFile(string filePath)
        {
            //  Проверка, что передана не пустая ссылка.
            filePath = Check.IsNotNull(filePath, nameof(filePath));

            //  Получение информации о файле.
            FileInfo file = new(filePath);

            //  Чтение из файла.
            var array = File.ReadAllBytes(file.FullName);

            //  Чтение масива файла
            LoadFromArray(file.Name, array);
        }

        /// <summary>
        /// Представляет функцию загрзуки из массива.
        /// </summary>
        /// <param name="name">Имя файла</param>
        /// <param name="array">Массив данных.</param>
        /// <exception cref="FormatException">Файл прочитан не до конца.</exception>
        private void LoadFromArray(string name,byte[] array)
        {
            //  Чтение потока данных
            var busyIndex = Devices.ReadFromArray(array);

            if (busyIndex != array.Length)
            {
             //   throw new FormatException();
            }

            //  Создание кадра
            Frame frame = new();


            int index = 0;
            //  Цикл по всем сигналам
            foreach (var one in Devices.Signals)
            {
                if (one.SignalReference.Contains("Signal1") == false)
                {
                    continue;
                }

                //  Получение опций канала
                var options = _FrameOptions.Channels[index];


                Console.WriteLine($"Name={name}:{index}: one.SignalValue.Count={one.SignalValue.Count}:");

                //  Создание канала
                Channel channel = new(options.Name, options.Unit, options.Sampling, options.Cutoff, one.SignalValue.Count)
                {
                    Vector = new Algebra.Vector<double>(one.SignalValue.ToArray())
                };

                //  Добавление канала
                frame.Channels.Add(channel);

                one.SignalValue.Clear();

                //  Инкремент индекса
                index++;
            }
            //  Сохранение кадра
            frame.Save(Path.Combine(_FrameOptions.SavePath, $"File_{name}.bin"),StorageFormat.Catman);
        }

      }
}
