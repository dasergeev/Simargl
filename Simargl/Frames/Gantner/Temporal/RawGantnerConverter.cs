using Simargl.Frames.Gantner.Raw;
using Simargl.Frames.Mera.Raw;
using Simargl.Frames.TestLab;
using Simargl.Text;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Frames.Gantner.Temporal
{
    internal static class RawGantnerConverter
    {
        public static async Task ConvertAsync(CancellationToken cancellationToken)
        {

            try
            {
                Encoding encoding = new CyrillicEncoding();
                //CancellationToken cancellationToken = default;
                int number = 1;


                string[] meraPaths = Directory.GetFiles("C:\\Users\\user\\Desktop\\Тарировки\\102 Исходные данные\\101 Mera", "*.mera", SearchOption.AllDirectories);
                string targetPath = "C:\\Users\\user\\Desktop\\Тарировки\\103 Кадры для обработки";
                using StreamWriter output = new("D:\\Anal.txt");

                foreach (var path in meraPaths)
                {
                    FileInfo file = new(path);

                    using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);

                    var rawFrame = await RawMeraFrame.LoadFrameAsync(stream, encoding, cancellationToken);
                    Frame frame = new();
                    int indexCh = 0;

                    foreach (RawMeraChannelHeader header in rawFrame.ChannelHeaders)
                    {
                        Channel channel = await header.LoadChannelAsync(file.Directory!, cancellationToken);

                        await output.WriteLineAsync(
                            $"{file.Name};{header.Header.Name};{indexCh};{header.Header.ModuleName};{header.Header.ModuleSerialNumber};{header.Header.Address}".AsMemory(), cancellationToken);
                        ++indexCh;
                        frame.Channels.Add(channel);
                    }

                    string filePath = Path.Combine(targetPath, $"Vp0_0.{number:0000}");
                    //frame.Save(filePath, StorageFormat.TestLab);

                    Console.WriteLine($"{number};{file.Name}");
                    number++;
                }

                string[] gantnerPaths = Directory.GetFiles("C:\\Users\\user\\Desktop\\Тарировки\\102 Исходные данные\\102 Gantner", "*.dat", SearchOption.AllDirectories);

                foreach (var path in gantnerPaths)
                {
                    FileInfo file = new(path);

                    //Console.WriteLine($"Анализ файла {Path.GetFileName(path)}");
                    using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    long size = stream.Length;

                    RawGantnerReader reader = new(stream, encoding);

                    reader.IsBigEndian = await reader.ReadBooleanAsync(cancellationToken);
                    //Console.WriteLine($"  IsBigEndian = {reader.IsBigEndian}");

                    ushort version = await reader.ReadUInt16Async(cancellationToken);
                    //Console.WriteLine($"  Version = {version}");

                    if (version != 107)
                    {
                        continue;
                    }

                    string typeVendor = await reader.ReadStringAsync(cancellationToken);
                    //Console.WriteLine($"  TypeVendor = \"{typeVendor}\"");

                    bool withCheckSum = await reader.ReadBooleanAsync(cancellationToken);
                    //Console.WriteLine($"  WithCheckSum = {withCheckSum}");

                    if (withCheckSum)
                    {
                        continue;
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

                    string filePath = Path.Combine(targetPath, $"Vp0_0.{number:0000}");
                    //frame.Save(filePath, StorageFormat.TestLab);

                    Console.WriteLine($"{number};{file.Name}");
                    number++;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
