// See https://aka.ms/new-console-template for more information
using Apeiron.Platform.QuantumX;

Console.WriteLine("Hello, World!");


QuantumParserOptions options = new ();

for(int i = 0; i < 32; i++)
{
    ChannelOptions channel = new()
    {
        Name = $"Channel={i}",
        Unit = "a",
        Cutoff = 1.0,
        Sampling = 300.0
    };

    options.Channels.Add(channel);
}

options.SavePath = "D:/Frames/";


QuantumXParser parser = new(options);

await parser.LoadFromFolderAsync("D:/srv/Data/");
