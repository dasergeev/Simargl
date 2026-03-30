using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Processing
{
    /// <summary>
    /// Выполняет восстановление сил.
    /// </summary>
    /// <seealso cref="Performer"/>
    public class RestoreForces : Performer
    {
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

            //  Получение списка файлов.
            var files = new DirectoryInfo(Configuration.SourcePath).GetFiles();

            //  Перебор всех файлов.
            foreach (var file in files)
            {
                actions.Add(() =>
                {
                    //  Строка вывода.
                    var trace = new StringBuilder();

                    //  Вывод имени файла.
                    trace.Append($"{file.Name}\t");

                    try
                    {
                        //  Открытие кадра.
                        var frame = new Frame(file.FullName);

                        //  Проверка кадра.
                        CheckFrame(frame);

                        //  Восстановление сил.
                        {
                            //  Получение исходных сигналов.
                            Channel[] S = new Channel[12];
                            for (int i = 0; i != 12; ++i)
                            {
                                S[i] = frame.Channels[$"S{i + 1}"];
                            }

                            //  Длина канала.
                            var length = S[0].Length;
                            //var S1 = frame.Channels["S1"]

                            var Qx1 = S[0].Clone(); Qx1.Name = "Qx1"; Qx1.Unit = "kN"; frame.Channels.Add(Qx1);
                            var Qy1 = S[0].Clone(); Qy1.Name = "Qy1"; Qy1.Unit = "kN"; frame.Channels.Add(Qy1);
                            var Qz1 = S[0].Clone(); Qz1.Name = "Qz1"; Qz1.Unit = "kN"; frame.Channels.Add(Qz1);
                            var Mx1 = S[0].Clone(); Mx1.Name = "Mx1"; Mx1.Unit = "kN*m"; frame.Channels.Add(Mx1);
                            var My1 = S[0].Clone(); My1.Name = "My1"; My1.Unit = "kN*m"; frame.Channels.Add(My1);
                            var Mz1 = S[0].Clone(); Mz1.Name = "Mz1"; Mz1.Unit = "kN*m"; frame.Channels.Add(Mz1);

                            var Qx2 = S[0].Clone(); Qx2.Name = "Qx2"; Qx2.Unit = "kN"; frame.Channels.Add(Qx2);
                            var Qy2 = S[0].Clone(); Qy2.Name = "Qy2"; Qy2.Unit = "kN"; frame.Channels.Add(Qy2);
                            var Qz2 = S[0].Clone(); Qz2.Name = "Qz2"; Qz2.Unit = "kN"; frame.Channels.Add(Qz2);
                            var Mx2 = S[0].Clone(); Mx2.Name = "Mx2"; Mx2.Unit = "kN*m"; frame.Channels.Add(Mx2);
                            var My2 = S[0].Clone(); My2.Name = "My2"; My2.Unit = "kN*m"; frame.Channels.Add(My2);
                            var Mz2 = S[0].Clone(); Mz2.Name = "Mz2"; Mz2.Unit = "kN*m"; frame.Channels.Add(Mz2);

                            var Qx = S[0].Clone(); Qx.Name = "Qx"; Qx.Unit = "kN"; frame.Channels.Add(Qx);
                            var Qy = S[0].Clone(); Qy.Name = "Qy"; Qy.Unit = "kN"; frame.Channels.Add(Qy);
                            var Qz = S[0].Clone(); Qz.Name = "Qz"; Qz.Unit = "kN"; frame.Channels.Add(Qz);
                            var Mx = S[0].Clone(); Mx.Name = "Mx"; Mx.Unit = "kN*m"; frame.Channels.Add(Mx);
                            var My = S[0].Clone(); My.Name = "My"; My.Unit = "kN*m"; frame.Channels.Add(My);
                            var Mz = S[0].Clone(); Mz.Name = "Mz"; Mz.Unit = "kN*m"; frame.Channels.Add(Mz);

                            for (int i = 0; i != length; ++i)
                            {
                                Qx1[i] = 12944.0696056745 * S[0][i] + 4239.65253924838 * S[1][i] + 3856.68080989779 * S[2][i] + -12950.3666324331 * S[3][i] + -3919.6818469342 * S[4][i] + -3142.74563726971 * S[5][i];
                                Qy1[i] = 344.84149668288 * S[0][i] + 230.778838554749 * S[1][i] + -26.8919245124647 * S[2][i] + -361.290743678933 * S[3][i] + -246.80329802613 * S[4][i] + 54.9338423530864 * S[5][i];
                                Qz1[i] = -496.568060338092 * S[0][i] + -710.570728767513 * S[1][i] + -711.114997527347 * S[2][i] + 1406.64009182765 * S[3][i] + -70.8480905176993 * S[4][i] + -127.425707574732 * S[5][i];
                                Mx1[i] = 183.293507160948 * S[0][i] + 61.8902734412923 * S[1][i] + 51.5731706040664 * S[2][i] + 10.8570582126571 * S[3][i] + 1.06673684205793 * S[4][i] + 0.570153757601301 * S[5][i];
                                My1[i] = 231.752933418982 * S[0][i] + 80.6609416021844 * S[1][i] + 69.490972402651 * S[2][i] + -228.247447201492 * S[3][i] + -76.1767341972989 * S[4][i] + -66.7155514302084 * S[5][i];
                                Mz1[i] = -29.9798060424296 * S[0][i] + -6.16228766883679 * S[1][i] + -13.2530780051881 * S[2][i] + 31.323715636172 * S[3][i] + 13.2057283764055 * S[4][i] + 3.69531124059953 * S[5][i];

                                Qx2[i] = 12944.0696056745 * S[6][i] + 4239.65253924838 * S[7][i] + 3856.68080989779 * S[8][i] + -12950.3666324331 * S[9][i] + -3919.6818469342 * S[10][i] + -3142.74563726971 * S[11][i];
                                Qy2[i] = 344.84149668288 * S[6][i] + 230.778838554749 * S[7][i] + -26.8919245124647 * S[8][i] + -361.290743678933 * S[9][i] + -246.80329802613 * S[10][i] + 54.9338423530864 * S[11][i];
                                Qz2[i] = -496.568060338092 * S[6][i] + -710.570728767513 * S[7][i] + -711.114997527347 * S[8][i] + 1406.64009182765 * S[9][i] + -70.8480905176993 * S[10][i] + -127.425707574732 * S[11][i];
                                Mx2[i] = 183.293507160948 * S[6][i] + 61.8902734412923 * S[7][i] + 51.5731706040664 * S[8][i] + 10.8570582126571 * S[9][i] + 1.06673684205793 * S[10][i] + 0.570153757601301 * S[11][i];
                                My2[i] = 231.752933418982 * S[6][i] + 80.6609416021844 * S[7][i] + 69.490972402651 * S[8][i] + -228.247447201492 * S[9][i] + -76.1767341972989 * S[10][i] + -66.7155514302084 * S[11][i];
                                Mz2[i] = -29.9798060424296 * S[6][i] + -6.16228766883679 * S[7][i] + -13.2530780051881 * S[8][i] + 31.323715636172 * S[9][i] + 13.2057283764055 * S[10][i] + 3.69531124059953 * S[11][i];

                                Qx[i] = Qx1[i] - Qx2[i];
                                Qy[i] = Qy1[i] + Qy2[i];
                                Qz[i] = -Qz1[i] + Qz2[i];

                                Mx[i] = Mx1[i] - Mx2[i];
                                My[i] = My1[i] + My2[i] - 0.3 * (Qz1[i] + Qz2[i]);
                                Mz[i] = -Mz1[i] + Mz2[i] + 0.3 * (Qy1[i] - Qy2[i]);
                            }



                        }

                        //  Получение скорости.
                        var speed = frame.Channels["V_GPS"].Average;

                        //  Сохранение кадра.
                        frame.Save(file.FullName.Replace(Configuration.SourcePath, Configuration.RestorePath)
                            .Replace("Vp0_0", $"Vp{speed:0.0}".Replace(',', '_')));

                        trace.Append(" успешно");
                    }
                    catch (Exception ex)
                    {
                        trace.Append($" ошибка \"{ex.Message}\"");
                    }

                    //  Вывод строки.
                    Trace(trace.ToString());
                });
            }

            //  Возврат коллекции действий.
            return actions;
        }

        /// <summary>
        /// Возвращает новое имя файла.
        /// </summary>
        /// <param name="frame">
        /// Кадр.
        /// </param>
        /// <param name="sourceFileName">
        /// Имя исходного файла.
        /// </param>
        /// <returns>
        /// Новое имя файла.
        /// </returns>
        /// <remarks>
        /// Метод находит фрагмент "Vp0_0" и заполняет этот фрагмент значением скорости.
        /// </remarks>
        public static string GetNewFileName(Frame frame, string sourceFileName)
        {
            //  Получение скорости.
            var speed = frame.Channels["V_GPS"].Average;

            //  Формирование имени файла.
            return sourceFileName.Replace("Vp0_0", $"Vp{speed:0.0}".Replace(',', '_'));
        }

        /// <summary>
        /// Безопасно (не выбрасывает усключения) выполняет проверку кадра.
        /// </summary>
        /// <param name="frame">Кадр.</param>
        /// <param name="minSpeed">Минимальная скорость.</param>
        /// <param name="minLongitude">Минимальная широта.</param>
        /// <param name="minLatitude">Минимальная долгота.</param>
        /// <returns></returns>
        public static bool CheckFrameSafe(Frame frame, double minSpeed, double minLongitude, double minLatitude)
        {
            try
            {
                //  Проверка кадра.
                CheckFrame(frame, minSpeed, minLongitude, minLatitude);

                //  Проверка прошла успешно.
                return true;
            }
            catch (InvalidOperationException)
            {
                //  Кадр не прошёл проверку.
                return false;
            }
        }

        /// <summary>
        /// Выполняет проверку кадра.
        /// </summary>
        /// <param name="frame">Кадр.</param>
        /// <param name="minSpeed">Минимальная скорость.</param>
        /// <param name="minLongitude">Минимальная широта.</param>
        /// <param name="minLatitude">Минимальная долгота.</param>
        /// <exception cref="InvalidOperationException">
        /// Скорость меньше допустимой.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Неверные GPS координаты.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Неверные GPS координаты.
        /// </exception>
        public static void CheckFrame(Frame frame, double minSpeed, double minLongitude, double minLatitude)
        {
            //  Проверка скорости.
            if (frame.Channels["V_GPS"].Average < minSpeed)
            {
                throw new InvalidOperationException("Скорость меньше допустимой.");
            }

            //  Проверка долготы.
            var longitude = frame.Channels["Lon_GPS"];
            for (int i = 0; i != longitude.Length; ++i)
            {
                if (longitude[i] < minLongitude)
                {
                    throw new InvalidOperationException("Неверные GPS координаты.");
                }
            }

            //  Проверка широты.
            var latitude = frame.Channels["Lat_GPS"];
            for (int i = 0; i != longitude.Length; ++i)
            {
                if (latitude[i] < minLatitude)
                {
                    throw new InvalidOperationException("Неверные GPS координаты.");
                }
            }
        }


        /// <summary>
        /// Выполняет проверку кадра.
        /// </summary>
        private void CheckFrame(Frame frame)
        {
            //  Проверка скорости.
            if (frame.Channels["V_GPS"].Average < Configuration.MinSpeed)
            {
                throw new InvalidOperationException("Скорость меньше допустимой.");
            }

            //  Проверка долготы.
            var longitude = frame.Channels["Lon_GPS"];
            for (int i = 0; i != longitude.Length; ++i)
            {
                if (longitude[i] < Configuration.MinLongitude)
                {
                    throw new InvalidOperationException("Неверные GPS координаты.");
                }
            }

            //  Проверка широты.
            var latitude = frame.Channels["Lat_GPS"];
            for (int i = 0; i != longitude.Length; ++i)
            {
                if (latitude[i] < Configuration.MinLatitude)
                {
                    throw new InvalidOperationException("Неверные GPS координаты.");
                }
            }
        }

        
    }
}
