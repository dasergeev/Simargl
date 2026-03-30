using System;
using System.IO;

namespace RailTest.Frames;

partial class Kernel
{
    unsafe public static Tuple<FrameHeader, ChannelCollection> LoadTestLabFrame(string path, FileReader reader, FileReadMode readMode)
    {
        TestLabFrameHeader frameHeader = new();
        ChannelCollection channels = new();
        reader.Position = 8;

        frameHeader.Title = ReadASCIIString(reader, 61);
        frameHeader.Character = ReadASCIIString(reader, 121);
        frameHeader.Region = ReadASCIIString(reader, 121);

        try
        {
            frameHeader.Time = DateTime.Parse(ReadASCIIString(reader, 11) + " " + ReadASCIIString(reader, 9));
        }
        catch
        {
            throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
        }


        int numberOfChannels = reader.ReadUInt16();
        reader.Position += 17;

        if (numberOfChannels == 0)
        {
            if (reader.FileSize != 350)
            {
                throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
            }
        }
        else
        {
            long fullSize = 350 + 192 * numberOfChannels;
            if (reader.FileSize < fullSize)
            {
                throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
            }

            for (int i = 0; i != numberOfChannels; ++i)
            {
                TestLabChannelHeader channelHeader = new()
                {
                    Name = ReadASCIIString(reader, 13),
                    Description = ReadASCIIString(reader, 121),
                    Unit = ReadASCIIString(reader, 13),
                    Offset = reader.ReadFloat32(),
                    Scale = reader.ReadFloat32(),
                    Cutoff = reader.ReadFloat32(),
                    Sampling = reader.ReadUInt16(),
                    Type = TestLabChannelHeader.Validation((TestLabChannelType)reader.ReadUInt8()),
                    DataFormat = TestLabChannelHeader.Validation((TestLabDataFormat)reader.ReadUInt8())
                };
                uint channelLength = reader.ReadUInt32();
                if (channelLength > int.MaxValue)
                {
                    throw new InvalidOperationException("Файл содержит канал слишеом большой длины.");
                }
                Channel channel = new(channelHeader, new Algebra.RealVector((int)channelLength));
                channels.Add(channel);
                reader.Position += 25;

                fullSize += channelLength * TestLabChannelHeader.GetItemSize(channelHeader.DataFormat) + 2;
            }


            if ((readMode & FileReadMode.DisableCheckExceedingFileSize) == 0)
            {
                if (reader.FileSize != fullSize)
                {
                    throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
                }
            }
            else
            {
                if (reader.FileSize < fullSize)
                {
                    throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
                }
            }

            for (int i = 0; i != numberOfChannels; ++i)
            {
                Channel channel = channels[i];
                TestLabChannelHeader header = (TestLabChannelHeader)channel.Header;
                int length = channel.Length;
                byte[] buffer = reader.ReadBytes(length * TestLabChannelHeader.GetItemSize(header.DataFormat));
                double offset = header.Offset;
                double scale = header.Scale;
                double* destination = (double*)channel.Vector.Pointer;

                switch (header.DataFormat)
                {
                    case TestLabDataFormat.UInt8:
                        fixed (void* pointer = buffer)
                        {
                            byte* source = (byte*)pointer;
                            for (int j = 0; j != length; ++j)
                            {
                                destination[j] = scale * (source[j] - offset);
                            }
                        }
                        break;
                    case TestLabDataFormat.UInt16:
                        fixed (void* pointer = buffer)
                        {
                            ushort* source = (ushort*)pointer;
                            for (int j = 0; j != length; ++j)
                            {
                                destination[j] = scale * (source[j] - offset);
                            }
                        }
                        break;
                    case TestLabDataFormat.UInt32:
                        fixed (void* pointer = buffer)
                        {
                            uint* source = (uint*)pointer;
                            for (int j = 0; j != length; ++j)
                            {
                                destination[j] = scale * (source[j] - offset);
                            }
                        }
                        break;
                    case TestLabDataFormat.Int8:
                        fixed (void* pointer = buffer)
                        {
                            sbyte* source = (sbyte*)pointer;
                            for (int j = 0; j != length; ++j)
                            {
                                destination[j] = scale * (source[j] - offset);
                            }
                        }
                        break;
                    case TestLabDataFormat.Int16:
                        fixed (void* pointer = buffer)
                        {
                            short* source = (short*)pointer;
                            for (int j = 0; j != length; ++j)
                            {
                                destination[j] = scale * (source[j] - offset);
                            }
                        }
                        break;
                    case TestLabDataFormat.Int32:
                        fixed (void* pointer = buffer)
                        {
                            int* source = (int*)pointer;
                            for (int j = 0; j != length; ++j)
                            {
                                destination[j] = scale * (source[j] - offset);
                            }
                        }
                        break;
                    case TestLabDataFormat.Float32:
                        fixed (void* pointer = buffer)
                        {
                            float* source = (float*)pointer;
                            for (int j = 0; j != length; ++j)
                            {
                                destination[j] = scale * (source[j] - offset);
                            }
                        }
                        break;
                    case TestLabDataFormat.Float64:
                        fixed (void* pointer = buffer)
                        {
                            double* source = (double*)pointer;
                            for (int j = 0; j != length; ++j)
                            {
                                destination[j] = scale * (source[j] - offset);
                            }
                        }
                        break;
                    default:
                        throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
                }

                if (reader.ReadUInt16() != 65535)
                {
                    throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
                }
            }
        }

        return new Tuple<FrameHeader, ChannelCollection>(frameHeader, channels);
    }

    unsafe public static void SaveTestLabFrame(Frame frame, FileWriter writer)
    {
        TestLabFrameHeader frameHeader = (TestLabFrameHeader)frame.Header.Convert(StorageFormat.TestLab);
        ChannelCollection channels = frame.Channels;
        writer.Position = 0;
        writer.WriteUInt64(0x42414C54534554UL);
        WriteASCIIString(writer, frameHeader.Title, 61, true);
        WriteASCIIString(writer, frameHeader.Character, 121, true);
        WriteASCIIString(writer, frameHeader.Region, 121, true);
        WriteASCIIString(writer, frameHeader.Time.ToString("dd.MM.yyyy"), 11, true);
        WriteASCIIString(writer, frameHeader.Time.ToString("HH:mm:ss"), 9, true);
        writer.WriteUInt16((ushort)channels.Count);
        for (int i = 0; i != 17; ++i)
        {
            writer.WriteUInt8(0);
        }

        foreach (Channel channel in channels)
        {
            TestLabChannelHeader channelHeader = (TestLabChannelHeader)channel.Header.Convert(StorageFormat.TestLab);
            WriteASCIIString(writer, channelHeader.Name, 13, true);
            WriteASCIIString(writer, channelHeader.Description, 121, true);
            WriteASCIIString(writer, channelHeader.Unit, 13, true);
            writer.WriteFloat32((float)channelHeader.Offset);
            writer.WriteFloat32((float)channelHeader.Scale);
            writer.WriteFloat32((float)channelHeader.Cutoff);
            writer.WriteUInt16((ushort)channelHeader.Sampling);
            writer.WriteUInt8((byte)channelHeader.Type);
            writer.WriteUInt8((byte)channelHeader.DataFormat);
            writer.WriteUInt32((uint)channel.Length);
            for (int i = 0; i != 25; ++i)
            {
                writer.WriteUInt8(0);
            }
        }

        foreach (Channel channel in channels)
        {
            TestLabChannelHeader header = (TestLabChannelHeader)channel.Header.Convert(StorageFormat.TestLab);
            int length = channel.Length;
            double offset = header.Offset;
            double factor = 0;
            if (header.Scale != 0)
            {
                factor = 1 / header.Scale;
            }
            double* source = (double*)channel.Vector.Pointer;
            byte[] buffer = new byte[length * TestLabChannelHeader.GetItemSize(header.DataFormat)];
            switch (header.DataFormat)
            {
                case TestLabDataFormat.UInt8:
                    fixed (void* pointer = buffer)
                    {
                        byte* destination = (byte*)pointer;
                        for (int i = 0; i != length; ++i)
                        {
                            destination[i] = (byte)(offset + factor * source[i]);
                        }
                    }
                    break;
                case TestLabDataFormat.UInt16:
                    fixed (void* pointer = buffer)
                    {
                        ushort* destination = (ushort*)pointer;
                        for (int i = 0; i != length; ++i)
                        {
                            destination[i] = (ushort)(offset + factor * source[i]);
                        }
                    }
                    break;
                case TestLabDataFormat.UInt32:
                    fixed (void* pointer = buffer)
                    {
                        uint* destination = (uint*)pointer;
                        for (int i = 0; i != length; ++i)
                        {
                            destination[i] = (uint)(offset + factor * source[i]);
                        }
                    }
                    break;
                case TestLabDataFormat.Int8:
                    fixed (void* pointer = buffer)
                    {
                        sbyte* destination = (sbyte*)pointer;
                        for (int i = 0; i != length; ++i)
                        {
                            destination[i] = (sbyte)(offset + factor * source[i]);
                        }
                    }
                    break;
                case TestLabDataFormat.Int16:
                    fixed (void* pointer = buffer)
                    {
                        short* destination = (short*)pointer;
                        for (int i = 0; i != length; ++i)
                        {
                            destination[i] = (short)(offset + factor * source[i]);
                        }
                    }
                    break;
                case TestLabDataFormat.Int32:
                    fixed (void* pointer = buffer)
                    {
                        int* destination = (int*)pointer;
                        for (int i = 0; i != length; ++i)
                        {
                            destination[i] = (int)(offset + factor * source[i]);
                        }
                    }
                    break;
                case TestLabDataFormat.Float32:
                    fixed (void* pointer = buffer)
                    {
                        float* destination = (float*)pointer;
                        for (int i = 0; i != length; ++i)
                        {
                            destination[i] = (float)(offset + factor * source[i]);
                        }
                    }
                    break;
                case TestLabDataFormat.Float64:
                    fixed (void* pointer = buffer)
                    {
                        double* destination = (double*)pointer;
                        for (int i = 0; i != length; ++i)
                        {
                            destination[i] = offset + factor * source[i];
                        }
                    }
                    break;
            }
            writer.Write(buffer, 0, buffer.Length);
            writer.WriteUInt16(65535);
        }
    }
}