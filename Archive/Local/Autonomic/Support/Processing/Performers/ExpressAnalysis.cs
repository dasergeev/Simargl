using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing
{
    /// <summary>
    /// Выполняет экспресс анализ.
    /// </summary>
    /// <seealso cref="Performer"/>
    public class ExpressAnalysis : Performer
    {
        /// <summary>
        /// Поле для хранения пути к файлам.
        /// </summary>
        readonly string _Path;

        /// <summary>
        /// Поле для хранения результатов.
        /// </summary>
        readonly List<string> _Results = new List<string>();

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="path">
        /// Путь к файлам.
        /// </param>
        public ExpressAnalysis(string path) => _Path = path;

        /// <summary>
        /// Происходит при остановке обработки.
        /// </summary>
        protected override void OnStopped()
        {
            Console.Write("Сохранение результатов .. ");
            try
            {
                using (var writer = new StreamWriter(Configuration.TraceFilePath))
                {
                    foreach (var result in _Results)
                    {
                        writer.WriteLine(result);
                    }
                }
                Console.WriteLine("завершено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ошибка \"{ex.Message}\".");
            }
            base.OnStopped();
        }

        /// <summary>
        /// Выполняет построение действий.
        /// </summary>
        /// <returns>
        /// Коллекция действий.
        /// </returns>
        protected override IEnumerable<Action> BuildActions()
        {
            //  Создание списка действий.
            var actions = new List<Action>();

            //  Перебор всех файлов.
            foreach (var file in new DirectoryInfo(_Path).GetFiles())
            {
                //  Исключение файлов.
                if (Configuration.RemovedFiles.Contains(file.Name))
                {
                    continue;
                }

                actions.Add(() =>
                {
                    //  Строка вывода.
                    var trace = new StringBuilder();

                    //  Вывод имени файла.
                    trace.Append($"{file.Name}");

                    while (trace.Length < 32)
                    {
                        trace.Append(' ');
                    }

                    try
                    {
                        //  Открытие кадра.
                        var frame = new Frame(file.FullName);

                        //  Показатели.
                        var result = new StringBuilder(frame.Channels.Count * 16 * 6);

                        //  Запись значения.
                        void write(object value)
                        {
                            //  Запись значения.
                            result.Append(value);

                            //  Запись символа табуляции.
                            result.Append('\t');
                        }

                        lock (_Results)
                        {
                            if (_Results.Count == 0)
                            {
                                //  Запись имени файла.
                                write("File");

                                //  Обработка каналов.
                                foreach (var channel in frame.Channels)
                                {
                                    //  Имя канала.
                                    var channelName = channel.Name;

                                    //  Среднее значение.
                                    write(channelName + "Average");

                                    //  СКО.
                                    write(channelName + "Deviation");

                                    //  Минимальное значение.
                                    write(channelName + "Min");

                                    //  Максимальное значение.
                                    write(channelName + "Max");
                                }

                                //  Добавление заголовка.
                                _Results.Add(result.ToString());

                                //  Очистка результата.
                                result.Clear();
                            }
                        }


                        //  Запись имени файла.
                        write(file.Name);

                        //  Обработка каналов.
                        foreach (var channel in frame.Channels)
                        {
                            //  Проверка служебных каналов.
                            if (channel.Name != "V_GPS" && channel.Name != "Lon_GPS" && channel.Name != "Lat_GPS")
                            {
                                //  Проверка необходимости фильтрации.
                                if (Configuration.IsFilter)
                                {
                                    //  Фильтрация.
                                    channel.FourierFiltering(-1, Configuration.FilteringFrequency);
                                }

                                //  Смещение канала.
                                channel.Move(-channel.Average);
                            }

                            //  Среднее значение.
                            write(channel.Average);

                            //  СКО.
                            write(channel.StandardDeviation);

                            //  Минимальное значение.
                            write(channel.Min);

                            //  Максимальное значение.
                            write(channel.Max);
                        }

                        lock (_Results)
                        {
                            //  Добавление результата.
                            _Results.Add(result.ToString());
                        }

                        trace.Append("успешно");
                    }
                    catch (Exception ex)
                    {
                        trace.Append($"ошибка \"{ex.Message}\"");
                    }

                    //  Вывод строки.
                    Trace(trace.ToString());
                });
            }

            //  Возврат коллекции действий.
            return actions;
        }
    }
}
