using Simargl.Frames;
using Simargl.Frames.Gantner;
using Simargl.Frames.Gantner.Raw;
using Simargl.Frames.Mera.Raw;
using Simargl.Frames.TestLab;
using Simargl.Recording.Geolocation;
using Simargl.Recording.Geolocation.Nmea;
using Simargl.Text;
using System.Collections.Concurrent;
using System.Text;

const string rawPath = @"\\SMALLSTORAGE\NvcTrain\01 Raw\";
const string targetPath = @"\\SMALLSTORAGE\NvcTrain\03 Results";

long meraNumber = 0;
long gantnerNumber = 0;

bool isNMEA = true;

ConcurrentBag<(DateTime Time, int Index, NmeaGgaMessage Message)> ggaBag = [];
ConcurrentBag<(DateTime Time, int Index, NmeaRmcMessage Message)> rmcBag = [];

CancellationToken cancellationToken = default;

await workDirectory(new(rawPath));
StringBuilder output = new();
int pathIndex = 0;
const int pathSize = 1000000;



//  GGA
if (isNMEA)
{
    output.Clear();
    pathIndex = 0;
    int fileIndex = 0;

    var items = ggaBag.OrderBy(x => x.Time.Ticks + x.Index);

    foreach ((DateTime fileTime, int index, NmeaGgaMessage message) in items)
    {
        if (message.Time is not TimeOnly time ||
            message.Latitude is not double latitude ||
            message.Longitude is not double longitude ||
            message.Altitude is not double altitude)
        {
            continue;
        }

        if (pathIndex == 0)
        {
            output.Clear();
            output.AppendLine("FileTime;Index;Time;Latitude;Longitude;Altitude;X;Y;Z");
        }

        output.Append(fileTime); output.Append(';');
        output.Append(index); output.Append(';');
        output.Append($"{time:HH:mm:ss}"); output.Append(';');
        output.Append(latitude); output.Append(';');
        output.Append(longitude); output.Append(';');
        output.Append(altitude); output.Append(';');
        GpsPointInfo info = new(latitude, longitude, altitude);
        output.Append(info.X); output.Append(';');
        output.Append(info.Y); output.Append(';');
        output.Append(info.Z);

        output.AppendLine();

        ++pathIndex;
        if (pathIndex == pathSize)
        {
            pathIndex = 0;
            File.WriteAllText(Path.Combine(targetPath, $"Gga{fileIndex:00}.csv"), output.ToString());
            ++fileIndex;
        }
    }

    if (pathIndex > 0)
    {
        File.WriteAllText(Path.Combine(targetPath, $"Gga{fileIndex:00}.csv"), output.ToString());
    }

}

//  RMC
if (isNMEA)
{
    output.Clear();
    pathIndex = 0;
    int fileIndex = 0;

    var items = rmcBag.OrderBy(x => x.Time.Ticks + x.Index);

    foreach ((DateTime fileTime, int index, NmeaRmcMessage message) in items)
    {
        if (message.Time is not TimeOnly time ||
            message.Latitude is not double latitude ||
            message.Longitude is not double longitude ||
            message.Speed is not double speed ||
            message.PoleCourse is not double course)
        {
            continue;
        }

        if (pathIndex == 0)
        {
            output.Clear();
            output.AppendLine("FileTime;Index;Time;Latitude;Longitude;Speed;PoleCourse;X;Y;Z");
        }

        output.Append(fileTime); output.Append(';');
        output.Append(index); output.Append(';');
        output.Append($"{time:HH:mm:ss}"); output.Append(';');
        output.Append(latitude); output.Append(';');
        output.Append(longitude); output.Append(';');
        output.Append(speed); output.Append(';');
        output.Append(course); output.Append(';');

        GpsPointInfo info = new(latitude, longitude, 0);
        output.Append(info.X); output.Append(';');
        output.Append(info.Y); output.Append(';');
        output.Append(info.Z);

        output.AppendLine();

        ++pathIndex;
        if (pathIndex == pathSize)
        {
            pathIndex = 0;
            File.WriteAllText(Path.Combine(targetPath, $"Rmc{fileIndex:00}.csv"), output.ToString());
            ++fileIndex;
        }
    }

    if (pathIndex > 0)
    {
        File.WriteAllText(Path.Combine(targetPath, $"Rmc{fileIndex:00}.csv"), output.ToString());
    }

}

async Task workDirectory(DirectoryInfo directory)
{
    //  Поиск файлов Мера.
    FileInfo[] files = directory.GetFiles("*.mera");
    if (files.Length != 0)
    {
        foreach (FileInfo file in files)
            await workMera(file);
        return;
    }

    //  Поиск файлов Gatner.
    files = directory.GetFiles("*.dat");
    await Parallel.ForEachAsync(
        files, async (f, c) => await workGatner(f));

    //  Поиск файлов Nmea.
    files = directory.GetFiles("*.nmea");
    await Parallel.ForEachAsync(
        files, async (f, c) => await workNmea(f));

    //  Работа с подкаталогами.
    DirectoryInfo[] subDirectories = directory.GetDirectories();
    await Parallel.ForEachAsync(
        subDirectories, async (d, c) => await workDirectory(d));
}

async Task workMera(FileInfo file)
{
    try
    {
        using FileStream stream = new(file.FullName, FileMode.Open, FileAccess.Read);

        RawMeraFrame rawMeraFrame = await RawMeraFrame.LoadFrameAsync(stream, new CyrillicEncoding(), cancellationToken).ConfigureAwait(false);

        Frame frame = new();
        

        foreach (RawMeraChannelHeader channelHeader in rawMeraFrame.ChannelHeaders)
        {
            Channel channel = await channelHeader.LoadChannelAsync(file.Directory!, cancellationToken);
            frame.Channels.Add(channel);
        }

        Channel priznak = frame.Channels[0].Clone();
        priznak.Name = "Priznak";
        priznak.Unit = string.Empty;
        Array.Clear(priznak.Items);
        frame.Channels.Add(priznak);

        long number = Interlocked.Increment(ref meraNumber);
        int dirNumber = (int)(number / 999);
        int fileNumber = (int)((number % 999) + 1);

        string dirPath = $"\\\\SMALLSTORAGE\\NvcTrain\\02 Source\\01 Mera\\{dirNumber:0000}";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        string fileName = Path.Combine(dirPath, $"Vp0_0.{fileNumber:0000}");
        frame.Save(fileName, StorageFormat.TestLab);

        DateTime time = rawMeraFrame.Header.Time;

        File.SetCreationTime(fileName, time);
        File.SetLastAccessTime(fileName, time);
        File.SetLastWriteTime(fileName, time);

        Console.WriteLine($"[Mera]: {fileNumber:0000} << {file.FullName}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{file.FullName}: {ex}");
    }
}

async Task workGatner(FileInfo file)
{
    try
    {
        Encoding encoding = new CyrillicEncoding();

        using FileStream stream = new(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
        long size = stream.Length;

        RawGantnerReader reader = new(stream, encoding);

        reader.IsBigEndian = await reader.ReadBooleanAsync(cancellationToken);

        ushort version = await reader.ReadUInt16Async(cancellationToken);

        if (version != 107)
        {
            return;
        }


        string typeVendor = await reader.ReadStringAsync(cancellationToken);
        //Console.WriteLine($"  TypeVendor = \"{typeVendor}\"");

        bool withCheckSum = await reader.ReadBooleanAsync(cancellationToken);
        //Console.WriteLine($"  WithCheckSum = {withCheckSum}");

        if (withCheckSum)
        {
            return;
        }

        ushort additionalDataLen = await reader.ReadUInt16Async(cancellationToken);
        await reader.SkipAsync(additionalDataLen, cancellationToken);
        //Console.WriteLine($"  AdditionalDataLen = {additionalDataLen}");

        double startTimeToDayFactor = await reader.ReadFloat64Async(cancellationToken);
        //Console.WriteLine($"  StartTimeToDayFactor = {startTimeToDayFactor}");

        GantnerDataFormat dActTimeDataType = (GantnerDataFormat)(await reader.ReadUInt16Async(cancellationToken));
        //Console.WriteLine($"  dActTimeDataType = {dActTimeDataType}");

        double dActTimeToSecondFactor = await reader.ReadFloat64Async(cancellationToken);
        //Console.WriteLine($"  dActTimeToSecondFactor = {dActTimeToSecondFactor}");

        double startTimestamp = await reader.ReadFloat64Async(cancellationToken);
        //Console.WriteLine($"  StartTime = {startTimestamp}");

        double sampleRate = await reader.ReadFloat64Async(cancellationToken);
        //Console.WriteLine($"  SampleRate = {sampleRate}");

        ushort variableCount = await reader.ReadUInt16Async(cancellationToken);
        //Console.WriteLine($"  VariableCount = {variableCount}");

        long positionSize = dActTimeDataType switch
        {
            GantnerDataFormat.Boolean => 1,
            GantnerDataFormat.Int8 => 1,
            GantnerDataFormat.UInt8 => 1,
            GantnerDataFormat.Int16 => 2,
            GantnerDataFormat.UInt16 => 2,
            GantnerDataFormat.Int32 => 4,
            GantnerDataFormat.UInt32 => 4,
            GantnerDataFormat.Float32 => 4,
            GantnerDataFormat.BitSet8 => 1,
            GantnerDataFormat.BitSet16 => 2,
            GantnerDataFormat.BitSet32 => 4,
            GantnerDataFormat.Float64 => 8,
            GantnerDataFormat.Int64 => 8,
            GantnerDataFormat.UInt64 => 8,
            GantnerDataFormat.BitSet64 => 8,
            _ => throw new InvalidDataException(
                $"Произошла попытка чтения значения в недопустимом формате {dActTimeDataType}."),
        };

        var data = new
        (
            string Name,
            ushort DataDirection,
            GantnerDataFormat DataType,
            ushort FieldLen,
            ushort Precision,
            string Unit,
            double[] Data
        )[variableCount];

        for (int i = 0; i < variableCount; i++)
        {
            //Console.WriteLine($"  Variable[{i}]");

            data[i].Name = await reader.ReadStringAsync(cancellationToken);
            //Console.WriteLine($"    Name = \"{data[i].Name}\"");

            data[i].DataDirection = await reader.ReadUInt16Async(cancellationToken);
            //Console.WriteLine($"    DataDirection = {data[i].DataDirection}");

            data[i].DataType = (GantnerDataFormat)(await reader.ReadUInt16Async(cancellationToken));
            //Console.WriteLine($"    DataType = {data[i].DataType}");

            positionSize += data[i].DataType switch
            {
                GantnerDataFormat.Boolean => 1,
                GantnerDataFormat.Int8 => 1,
                GantnerDataFormat.UInt8 => 1,
                GantnerDataFormat.Int16 => 2,
                GantnerDataFormat.UInt16 => 2,
                GantnerDataFormat.Int32 => 4,
                GantnerDataFormat.UInt32 => 4,
                GantnerDataFormat.Float32 => 4,
                GantnerDataFormat.BitSet8 => 1,
                GantnerDataFormat.BitSet16 => 2,
                GantnerDataFormat.BitSet32 => 4,
                GantnerDataFormat.Float64 => 8,
                GantnerDataFormat.Int64 => 8,
                GantnerDataFormat.UInt64 => 8,
                GantnerDataFormat.BitSet64 => 8,
                _ => throw new InvalidDataException(
                    $"Произошла попытка чтения значения в недопустимом формате {dActTimeDataType}."),
            };


            data[i].FieldLen = await reader.ReadUInt16Async(cancellationToken);
            //Console.WriteLine($"    FieldLen = {data[i].FieldLen}");

            data[i].Precision = await reader.ReadUInt16Async(cancellationToken);
            //Console.WriteLine($"    Precision = {data[i].Precision}");

            data[i].Unit = await reader.ReadStringAsync(cancellationToken);
            //Console.WriteLine($"    Unit = {data[i].Unit}");

            ushort variableAdditionalDataLen = await reader.ReadUInt16Async(cancellationToken);
            await reader.SkipAsync(variableAdditionalDataLen, cancellationToken);
            //Console.WriteLine($"    VariableAdditionalDataLen = {variableAdditionalDataLen}");

        }

        await reader.SkipAsync(8, cancellationToken);
        while (reader.Position % 16 != 0)
        {
            await reader.SkipAsync(1, cancellationToken);
        }

        size -= reader.Position;

        //Console.WriteLine($"  positionSize = {positionSize}");
        //Console.WriteLine($"  size = {size}");

        int count = (int)(size / positionSize);

        double[] time = new double[count];

        for (int i = 0; i < variableCount; i++)
        {
            data[i].Data = new double[count];
        }

        DateTime beginTime = default;

        for (int i = 0; i < count; i++)
        {
            double timestamp = await reader.ReadDataAsync(dActTimeDataType, cancellationToken);
            timestamp = timestamp * dActTimeToSecondFactor / 86400 + startTimestamp * startTimeToDayFactor;
            if (i == 0)
            {
                beginTime = DateTime.FromOADate(timestamp);
            }

            time[i] = (DateTime.FromOADate(timestamp) - beginTime).TotalSeconds;


            //Console.WriteLine($"    {time:yyyy.MM.dd HH:mm:ss.ffff}");


            for (int j = 0; j < variableCount; j++)
            {
                data[j].Data[i] = await reader.ReadDataAsync(data[j].DataType, cancellationToken);
            }
        }

        //Console.WriteLine($"    {beginTime:yyyy.MM.dd HH:mm:ss.ffff}");

        Frame frame = new();
        frame.Channels.Add(new(
            new TestLabChannelHeader("Time", "s", 0),
            new(sampleRate, time)));

        for (int j = 0; j < variableCount; j++)
        {
            frame.Channels.Add(new(
                new TestLabChannelHeader(data[j].Name, data[j].Unit, 0),
                new(sampleRate, data[j].Data)));
        }


        Channel priznak = frame.Channels[0].Clone();
        priznak.Name = "Priznak";
        priznak.Unit = string.Empty;
        Array.Clear(priznak.Items);
        frame.Channels.Add(priznak);

        long number = Interlocked.Increment(ref gantnerNumber);
        int dirNumber = (int)(number / 999);
        int fileNumber = (int)((number % 999) + 1);

        string dirPath = $"\\\\SMALLSTORAGE\\NvcTrain\\02 Source\\02 Gantner\\{dirNumber:0000}";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        string fileName = Path.Combine(dirPath, $"Vp0_0.{fileNumber:0000}");
        frame.Save(fileName, StorageFormat.TestLab);

        //  0         1         2         3
        //  0123456789012345678901234567890123456789
        //  Datalogger_#1__0_2024-02-21_03-45-02_454000
        //int year = 
        int year = getInt(17, 4);
        int month = getInt(22, 2);
        int day = getInt(25, 2);
        int hour = getInt(28, 2);
        int minute = getInt(31, 2);
        int second = getInt(34, 2);
        int millisecond = 0;

        int getInt(int start, int count) => int.Parse(file.Name.Substring(start, count));

        DateTime fileTime = new(year, month, day, hour, minute, second, millisecond);

        File.SetCreationTime(fileName, fileTime);
        File.SetLastAccessTime(fileName, fileTime);
        File.SetLastWriteTime(fileName, fileTime);

        Console.WriteLine($"[Gantner]: {fileNumber:0000} << {file.FullName}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{file.FullName}: {ex}");
    }
}

async Task workNmea(FileInfo file)
{
    if (!isNMEA)
    {
        return;
    }

    await Task.CompletedTask;
    try
    {
        string fileName = file.Name;
        int year = int.Parse(fileName.Substring(4, 4));
        int month = int.Parse(fileName.Substring(9, 2));
        int day = int.Parse(fileName.Substring(12, 2));
        int hour = int.Parse(fileName.Substring(15, 2));
        int minute = int.Parse(fileName.Substring(17, 2));
        int second = int.Parse(fileName.Substring(19, 2));


        //012345678901234567890123456789
        //Log 2024-03-18 051246.nmea


        DateTime fileTime = new(year, month, day, hour, minute, second);
        string[] lines = await File.ReadAllLinesAsync(file.FullName);
        IEnumerable<NmeaMessage> allMessages = lines.Select(x =>
        {
            try
            {
                return NmeaMessage.Parse(x);
            }
            catch
            {
                return null!;
            }
        });

        var ggaMessages = allMessages.Where(x => x is NmeaGgaMessage).Select(x => (NmeaGgaMessage)x).ToArray();
        for (int i = 0; i < ggaMessages.Length; i++)
        {
            ggaBag.Add((fileTime, i, ggaMessages[i]));
        }

        var rmcMessages = allMessages.Where(x => x is NmeaRmcMessage).Select(x => (NmeaRmcMessage)x).ToArray();
        for (int i = 0; i < rmcMessages.Length; i++)
        {
            rmcBag.Add((fileTime, i, rmcMessages[i]));
        }

        Console.WriteLine($"[NMEA] {fileTime}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[{file.FullName}]: {ex}");
    }
}
