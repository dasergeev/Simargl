using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindFrames
{
    class FindFramesProgram
    {
        const string _SourcePath = @"C:\OneDrive\Autonomic\Oriole";
        const string _TargerPath = @"C:\FindFrames";

        static void Main(string[] args)
        {
            var files = new DirectoryInfo(_SourcePath).GetFiles();
            foreach (var file in files)
            {
                try
                {
                    Console.Write($"{file.Name} ... ");
                    var frame = new Frame(file.FullName);
                    var Mo = frame.Channels["Mo"];
                    var Mr = frame.Channels["Mr"];
                    var Mir1 = frame.Channels["Mir1"];
                    var Mir2 = frame.Channels["Mir2"];
                    bool isFind = false;

                    foreach (var channel in new Channel[] { Mo, Mr, Mir1, Mir2 })
                    {
                        channel.Move(-channel.Average);

                        if (channel.Min < -0.001 && channel.Max > 0.001)
                        {
                            isFind = true;
                        }
                    }

                    if (isFind)
                    {
                        var fileName = file.FullName.Replace(_SourcePath, _TargerPath);
                        frame.Save(fileName, StorageFormat.TestLab);
                    }
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
