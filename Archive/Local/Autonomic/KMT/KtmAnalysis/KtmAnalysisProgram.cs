using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtmAnalysis
{
    class KtmAnalysisProgram
    {
        const string _SourcePath = @"\\railtest.ru\Data\Транзит\Файлы Иволга\Файлы с Mr, Mir1, Mir2";

        static void Main(string[] args)
        {
            bool isFirst = true;
            using (var writer = new StreamWriter(@"E:\KtmAnalysis.txt"))
            {
                var files = new DirectoryInfo(_SourcePath).GetFiles();
                foreach (var file in files)
                {
                    try
                    {
                        Console.Write($"{file.Name} ... ");
                        var frame = new Frame(file.FullName);
                        var outputBuilder = new StringBuilder();
                        var frameFeader = (TestLabFrameHeader)frame.Header;

                        void write(object obj)
                        {
                            outputBuilder.Append(obj.ToString());
                            outputBuilder.Append('\t');
                        }

                        if (isFirst)
                        {
                            write("File");
                            write("Time");
                            foreach (var channel in frame.Channels)
                            {
                                if (channel.Name.Contains("CB_CH") || channel.Name.Contains("KMT_CH"))
                                {
                                    continue;
                                }

                                write($"{channel.Name}Average");
                                write($"{channel.Name}Deviation");
                                write($"{channel.Name}Min");
                                write($"{channel.Name}Max");

                                if (!channel.Name.Contains("GPS"))
                                {
                                    for (int i = 0; i < 600; i++)
                                    {
                                        write($"{channel.Name}Fr{i}");
                                    }
                                }
                            }

                            writer.WriteLine(outputBuilder);
                            writer.Flush();
                            outputBuilder = new StringBuilder();
                        }

                        write(file.Name);
                        write(frameFeader.Time.ToOADate());
                        foreach (var channel in frame.Channels)
                        {
                            if (channel.Name.Contains("CB_CH") || channel.Name.Contains("KMT_CH"))
                            {
                                continue;
                            }

                            if (!channel.Name.Contains("GPS"))
                            {
                                channel.FourierFiltering(-1, 20);
                            }

                            write(channel.Average);
                            write(channel.StandardDeviation);
                            write(channel.Min);
                            write(channel.Max);

                            if (!channel.Name.Contains("GPS"))
                            {
                                var density = RailTest.Analysis.Spectral.SpectralDensity(channel, 16);
                                density.Resampling(1, 600);
                                for (int i = 0; i < 600; i++)
                                {
                                    write(density[i]);
                                }
                            }
                        }

                        isFirst = false;
                        writer.WriteLine(outputBuilder);
                        writer.Flush();
                        Console.WriteLine("ok");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

            }


        }
    }
}
