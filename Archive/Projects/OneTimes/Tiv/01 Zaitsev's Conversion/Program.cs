using Simargl.Frames;
using System.Text;

const string sources = "D:\\Storage\\OneDrive\\МПТ\\103 Работы\\103 Испытания\\118 Измерение вибрации на пассажирском вагоне по договору МПТ-30-23 от 30.01.2024\\Файлы";
const string searchPattern = "*.bin";
const SearchOption searchOption = SearchOption.TopDirectoryOnly;
const string target = "E:\\Convert";
string[] names = ["UY1", "UY2", "UY3", "UY4", "UY5"];

foreach (FileInfo file in new DirectoryInfo(sources).GetFiles(searchPattern, searchOption))
{
	try
	{
		Console.WriteLine(file.Name);
		Frame frame = new(file.FullName);
		int length = frame.Channels[0].Length;
		double sampling = frame.Channels[0].Sampling;
		
        foreach (Channel channel in frame.Channels)
		{
            Console.WriteLine($"  {channel.Name}");
            if (length != channel.Length)
			{
				throw new InvalidDataException($"В процессе \"{channel.Name}\" отличается длина.");
			}
            if (sampling != channel.Sampling)
            {
                throw new InvalidDataException($"В процессе \"{channel.Name}\" отличается частота.");
            }
        }

        StringBuilder output = new();
		for (int i = 0; i < length; i++)
		{
            double time = i / sampling;
			output.Append(time);
			foreach (string name in names)
			{
                output.Append(';');
				output.Append(frame.Channels[name][i]);
            }
			output.AppendLine();
        }

		string path = file.FullName.Replace(sources, target).Replace(file.Extension, ".csv");
		DirectoryInfo directory = new FileInfo(path).Directory!;
		if (!directory.Exists)
		{
			directory.Create();
        }
		File.WriteAllText(path, output.ToString());
    }
	catch (Exception ex)
	{
		Console.WriteLine($"Произошла ошибка при обработке файла \"{file.FullName}\": {ex}");
	}
}
