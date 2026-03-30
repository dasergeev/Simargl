using Simargl.Frames;
using Simargl.Frames.OldStyle;
using Simargl.Frames.TestLab;
using System.Text;

const string sourcePath = "N:\\Egypt-2024\\RawData\\Frames";
const string targetPath = "N:\\Egypt-2024\\Records";
const double tivSampling = 300;
const int tivLength = 60 * (int)tivSampling;

//const string tivPath = "N:\\Egypt-2024\\Tiv\\Part-01";
//DateTime begin = DateTime.Parse("03.11.2024 10:30");
//DateTime end = DateTime.Parse("03.11.2024 23:30");

//const string tivPath = "N:\\Egypt-2024\\Tiv\\Part-02";
//DateTime begin = DateTime.Parse("05.11.2024 11:00");
//DateTime end = DateTime.Parse("05.11.2024 23:46");

//const string tivPath = "N:\\Egypt-2024\\Tiv\\Part-03";
//DateTime begin = DateTime.Parse("07.11.2024 10:50");
//DateTime end = DateTime.Parse("07.11.2024 23:50");

//const string tivPath = "N:\\Egypt-2024\\Tiv\\Part-04";
//DateTime begin = DateTime.Parse("11.11.2024 11:07");
//DateTime end = DateTime.Parse("11.11.2024 23:35");

//const string tivPath = "N:\\Egypt-2024\\Tiv\\Part-05";
//DateTime begin = DateTime.Parse("20.11.2024 00:00");
//DateTime end = DateTime.Parse("20.11.2024 23:59");


//const string tivPath = "N:\\Egypt-2024\\Tiv\\Part-06";
//DateTime begin = DateTime.Parse("23.11.2024 00:00");
//DateTime end = DateTime.Parse("23.11.2024 23:59");


const string tivPath = "N:\\Egypt-2024\\Tiv\\Part-07";
DateTime begin = DateTime.Parse("25.11.2024 00:00");
DateTime end = DateTime.Parse("27.11.2024 23:59");


bool isInfo = false;

var files = new DirectoryInfo(sourcePath).GetFiles("*.*", SearchOption.AllDirectories)
	.Select(file => new
	{
		Time = tryGetTime(file.Name),
		Path = file.FullName,
	})
	.Where(x => x.Time.HasValue && x.Time.Value >= begin && x.Time.Value <= end)
	.Select(x => new
	{
		Time = x.Time!.Value,
		x.Path,
	})
	.OrderBy(x => x.Time)
	.ToArray();

int frameNumber = 0;

StringBuilder journal = new();

foreach (var file in files)
{
	Console.WriteLine($"{frameNumber}/{files.Length}");
	++frameNumber;

	try
	{
		Frame frame = new(file.Path);
		TestLabFrameHeader frameHeader = (TestLabFrameHeader)frame.Header;
		DateTime time = frameHeader.Time;
		if (time < begin || time > end)
		{
			continue;
		}

		for (int i = 0; i < frame.Channels.Count; i++)
		{
			Channel channel = frame.Channels[i];
			if (channel.Name.StartsWith("None"))
			{
				frame.Channels.Remove(channel);
				--i;
				continue;
			}
			TestLabChannelHeader channelHeader = (TestLabChannelHeader)channel.Header;
			channelHeader.DataFormat = TestLabDataFormat.Float64;
			//channel.Move(-channelHeader.Offset);
			channelHeader.Offset = 0;

			//Console.WriteLine($"{channelHeader.Offset}");
		}
		double speed = frame.Channels["V_GPS"].Average();
		string speedText = $"{speed:000.0}".Replace(',', '_').Replace('.', '_');
		string timeText = $"{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}-{time.Minute:00}-{time.Second:00}";
		int number = time.Minute + 1;
		string fileName = $"Vp{speedText} {timeText}.{number:0000}";
		string targetDirectory = Path.Combine(targetPath, $"{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}");
		if (!Directory.Exists(targetDirectory))
		{
			Directory.CreateDirectory(targetDirectory);
		}
		string fullName = Path.Combine(targetDirectory, fileName);
		frame.Save(fullName, StorageFormat.TestLab);
		Console.WriteLine($"{fileName}");

		StringBuilder output = new();

		if (!isInfo)
		{
			isInfo = true;
			output.AppendLine("t;s");
			foreach (Channel channel in frame.Channels)
			{
				output.AppendLine($"{channel.Name};{channel.Unit}");
			}

			File.WriteAllText(Path.Combine(tivPath, "_info.csv"), output.ToString());

			output.Clear();
		}

		for (int i = 0; i < tivLength; i++)
		{
			double t = i / tivSampling;
			output.Append(t);
			foreach (Channel channel in frame.Channels)
			{
				output.Append(';');
				if (channel.Sampling == tivSampling)
				{
					output.Append(channel[i]);
				}
				else
				{
					output.Append(channel[(int)t]);
				}
			}
			output.AppendLine();
		}

		fileName = $"{frameNumber:0000}.csv";// $"Vp{speedText} {timeText} {number:0000}.csv";

        string path = Path.Combine(tivPath, /*$"{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}",*/ fileName);
		DirectoryInfo directory = new FileInfo(path).Directory!;
		if (!directory.Exists)
		{
			directory.Create();
		}
		File.WriteAllText(path, output.ToString());

		journal.AppendLine($"{fileName};{file.Time};{speed}");

    }
	catch (Exception ex)
	{
		Console.WriteLine($"{file.Path}: {ex}");
	}
}

File.WriteAllText(Path.Combine(tivPath, "_journal.csv"), journal.ToString());

static DateTime? tryGetTime(string fileName)
{
    if (string.IsNullOrEmpty(fileName)) return null;
	if (fileName.Length < 27) return null;

    //	0000000000111111111122222222223333333333
    //	0123456789012345678901234567890123456789
    //	Vp000_0 03.11.2024 11_12_54.00000064

    if (!int.TryParse(fileName.AsSpan(8, 2), out int day)) return null;
    if (!int.TryParse(fileName.AsSpan(11, 2), out int month)) return null;
    if (!int.TryParse(fileName.AsSpan(14, 4), out int year)) return null;
    if (!int.TryParse(fileName.AsSpan(19, 2), out int hour)) return null;
    if (!int.TryParse(fileName.AsSpan(22, 2), out int minute)) return null;
    if (!int.TryParse(fileName.AsSpan(25, 2), out int second)) return null;

	try
	{
		return new(year, month, day, hour, minute, second);
    }
	catch
	{
		return null;
	}
}
