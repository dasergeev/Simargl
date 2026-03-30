using Apeiron.Compute.Cpu;
using Apeiron.Frames;
using System;
using System.IO;
using System.Numerics;

namespace KmtRecovery
{
    class Program
    {
        static void Filtration(Channel channel)
        {
            double sampling = channel.Sampling;
            var spectrum = Array.ConvertAll(channel.Vector.ToArray(), (value) => new Complex(value, 0));
            Fft.Direct(spectrum);
            var blockLength = channel.Length;
            var halfLength = blockLength >> 1;
            double stepFrequency = sampling / blockLength;

            for (int i = 1; i < halfLength; ++i)
            {
                double frequency = stepFrequency * i;
                if (frequency > 5.0)
                {
                    spectrum[i] = Complex.Zero;
                    spectrum[blockLength - i] = Complex.Zero;
                }
            }

            Fft.Inverse(spectrum);
            for (int i = 0; i < blockLength; i++)
            {
                channel[i] = spectrum[i].Real;
            }
        }

        const string sourcePath = @"\\Snickers.railtest.ru\G\Oriole Archive\2021-06-13\Frames\Vp084_2 13.06.2021 22_11_25.00012570";

        static void Main()
        {
            Frame frame = new(sourcePath);

            Channel Mo = frame.Channels["Mo"];
            Filtration(Mo);

            Channel[] U = new Channel[12];

            {
                int index = 0;
                foreach (var channel in frame.Channels)
                {
                    if (channel.Name.Contains("U"))
                    {
                        U[index++] = channel;
                        Filtration(channel);
                    }
                }
            }

            frame.Save(@"E:\Vp0_0 KMTFrame.0001", StorageFormat.TestLab);
            using StreamWriter writer = new(@"E:\KmtRecoveryOutput.txt");

            writer.Write("Index\tMr\t");
            foreach (var channel in U)
            {
                writer.Write($"{channel.Name}\t");
            }
            writer.WriteLine();

            int length = Mo.Length;
            for (int i = 0; i < length; i += 240)
            {
                writer.Write($"{i}\t{Mo[i]}\t");
                foreach (var channel in U)
                {
                    writer.Write($"{channel[i]}\t");
                }
                writer.WriteLine();
            }


        }
    }
}
