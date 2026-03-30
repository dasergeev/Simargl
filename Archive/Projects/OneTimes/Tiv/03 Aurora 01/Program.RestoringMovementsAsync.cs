using Simargl.Analysis;
using Simargl.Frames;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Simargl.Projects.OneTimes.Tiv.Aurora01;

partial class Program
{
    /// <summary>
    /// Асинхронно выполняет восстановление перемещений.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая восстановление перемещений.
    /// </returns>
    public static async Task RestoringMovementsAsync(CancellationToken cancellationToken)
    {
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);
        const string sourcePath = "C:\\Environs\\Data\\Simargl.Projects.OneTimes.Tiv.Aurora01\\Source";
        const string targetPath = "C:\\Environs\\Data\\Simargl.Projects.OneTimes.Tiv.Aurora01\\Target";
        List<(string First, string Second, string Result)> names = [
            ("BX11", "RBX11", "B-RBX11"),
            ("BX21", "RBX21", "B-RBX21"),
            ("BX101", "RBX101", "B-RBX101"),
            ("BX201", "RBX201", "B-RBX201"),
            ("RBX11", "BrusX1", "RB-BrusX11"),
            ("RBX21", "BrusX1", "RB-BrusX21"),
            ("RBX101", "BrusX01", "RB-BrusX101"),
            ("RBX201", "BrusX01", "RB-BrusX201"),
            ];

        foreach (FileInfo file in new DirectoryInfo(sourcePath).GetFiles("*", SearchOption.TopDirectoryOnly))
        {
            try
            {
                Frame frame = new(file.FullName);
                List<Channel> movings = [];
                foreach (var (first, second, result) in names)
                {
                    Channel firstCnannel = frame.Channels[first];
                    Channel secondCnannel = frame.Channels[second];
                    Channel moving = firstCnannel.Clone();
                    moving.Name = result;
                    for (int k = 0; k < moving.Length; k++)
                    {
                        moving[k] -= secondCnannel[k];
                    }
                    movings.Add(moving);
                }

                //Channel[] accelerations = frame.Channels.Where(channel => channel.Name.Contains('X')).ToArray();
                //for (int i = 0; i < accelerations.Length; i++)
                //{
                //    for (int j = i + 1; j < accelerations.Length; j++)
                //    {
                //        Channel firstCnannel = accelerations[i];
                //        Channel secondCnannel = accelerations[j];
                //        //if (secondCnannel.Name == "R" + firstCnannel.Name)
                //        {
                //            Channel moving = firstCnannel.Clone();
                //            moving.Name += "-" + secondCnannel.Name;
                //            for (int k = 0; k < moving.Length; k++)
                //            {
                //                moving[k] -= secondCnannel[k];
                //            }
                //            movings.Add(moving);
                //        }
                //    }
                //}

                foreach (Channel moving in movings)
                {
                    Spectrum spectrum = new(moving.Signal);
                    spectrum[0] = 0;
                    for (int i = 1; i < spectrum.Count; i++)
                    {
                        double frequency = i * spectrum.FrequencyStep;
                        if (frequency < 5)
                        {
                            spectrum[i] *= -1000 * (1 / 9.81) / (2 * Math.PI * frequency) / (2 * Math.PI * frequency);
                        }
                        else
                        {
                            spectrum[i] = 0;
                        }
                    }

                    spectrum.Restore(moving.Signal);
                }


                List<Channel> allChannels = [.. movings];
                allChannels.AddRange(frame.Channels);
                frame.Channels.Clear();
                frame.Channels.AddRange(allChannels);
                frame.Save(Path.Combine(targetPath, file.Name), StorageFormat.TestLab);

                Console.WriteLine($"{file.Name}: movings = {movings.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{file.Name}: {ex}");
            }
        }
    }
}
