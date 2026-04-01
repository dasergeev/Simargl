using Simargl.Analysis;
using Simargl.Frames.Catman;
using Simargl.Frames.Simple;
using Simargl.Frames.TestLab;
using Simargl.Text;
using System.Globalization;
using System.Security;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Simargl.Frames.Zero;
using Simargl.Designing.Utilities;
using Simargl.IO;

namespace Simargl.Frames;

/// <summary>
/// Представляет кадр регистрации.
/// </summary>
public class Frame
{
    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Format"/>.
    /// </summary>
    public event EventHandler? FormatChanged;

    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Header"/>.
    /// </summary>
    public event EventHandler? HeaderChanged;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Frame() :
        this(new SimpleFrameHeader())
    {
        //  Создание коллекции каналов.
        Channels = new();
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить кадр.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    public Frame(Stream stream) :
        this()
    {
        //  Загрузка данных из потока.
        LoadFromStream(stream);
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    public Frame(string path) :
        this()
    {
        //  Проверка ссылки на путь.
        IsNotNull(path, nameof(path));

        //  Создание потока.
        using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        //  Загрузка данных из потока.
        LoadFromStream(stream);
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="header"/> передана пустая ссылка.
    /// </exception>
    internal Frame(FrameHeader header)
    {
        //  Установка заголовка кадра.
        Header = IsNotNull(header, nameof(header));

        //  Создание коллекции каналов.
        Channels = [];
    }

    /// <summary>
    /// Возвращает или задаёт формат кадра.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое не содержится в перечислении <see cref="StorageFormat"/>.
    /// </exception>
    public StorageFormat Format
    {
        get => Header.Format;
        set
        {
            //  Проверка нового значения.
            IsDefined(value, nameof(Format));

            //  Проверка изменения значения.
            if (Header.Format != value)
            {
                //  Изменение заголовка кадра.
                Header = FrameHeaderConverter.Convert(Header, value);

                //  Вызов события об изменении формата.
                OnFormatChanged(EventArgs.Empty);

                //  Вызов события об изменении заголовка кадра.
                OnHeaderChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает заголовок кадра.
    /// </summary>
    public FrameHeader Header { get; private set; }

    /// <summary>
    /// Возвращает коллекцию каналов.
    /// </summary>
    public ChannelCollection Channels { get; }

    /// <summary>
    /// Сохраняет кадр в файл.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    internal void Save(string path)
    {
        Save(path, Format);
    }

    /// <summary>
    /// Сохраняет кадр в файл.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <param name="format">
    /// Формат хранения элементов кадра регистрации.
    /// </param>
    public void Save(string path, StorageFormat format)
    {
        SaveFrame(path, format, this);
    }

    /// <summary>
    /// Сохраняет кадр в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток, в который необходимо сохранить кадр.
    /// </param>
    /// <param name="format">
    /// Формат хранения элементов кадра регистрации.
    /// </param>
    public void Save(Stream stream, StorageFormat format)
    {
        SaveToStream(stream, format, this);
    }

    private static void SaveFrame(string path, StorageFormat format, Frame frame)
    {
        using FileStream stream = new(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        SaveToStream(stream, format, frame);
    }

    private static void SaveToStream(Stream stream, StorageFormat format, Frame frame)
    {
        //  Создание респределителя данных потока.
        Spreader spreader = new(stream, new CyrillicEncoding());

        try
        {
            switch (format)
            {
                case StorageFormat.Simple:
                    throw new Exception();
                case StorageFormat.TestLab:
                    SaveTestLabFrame(frame, spreader);
                    break;
                case StorageFormat.Catman:
                    SaveCatmanFrame(frame, spreader);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), "Произошла попытка сохранить кадр в неизвестном формате.");
            }
        }
        finally
        {

        }
    }

    /// <summary>
    /// Загружает данные из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить данные.
    /// </param>
    private void LoadFromStream(Stream stream)
    {
        //  Проверка ссылки на поток.
        IsNotNull(stream, nameof(stream));

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream, new CyrillicEncoding());

        //  Блок перехвата всех несистемных исключений.
        try
        {
            //  Чтение сигнатуры.
            short signature = spreader.ReadInt16();

            //  Проверка формата Catman.
            if (signature == 0x1394)
            {
                Header = LoadCatmanFrame(spreader);
                return;
            }

            //  Проверка формата TestLab.
            if (signature == 0x4554 &&
                spreader.ReadInt16() == 0x5453 &&
                spreader.ReadInt16() == 0x414C &&
                spreader.ReadInt16() == 0x0042)
            {
                Header = LoadTestLabFrame(spreader);
                return;
            }
        }
        catch (Exception ex)
        {
            //  Проверка системного исключения.
            if (ex.IsSystem())
            {
                //  Повторный выброс исключения.
                throw;
            }
            throw new InvalidDataException("Произошла попытка загрузить файл неизвестного формата.", ex);
        }

        throw new InvalidDataException("Произошла попытка загрузить файл неизвестного формата.");
    }

    CatmanFrameHeader LoadCatmanFrame(Spreader spreader)
    {
        Format = StorageFormat.Catman;
        CatmanFrameHeader frameHeader = new();


        try
        {
            int dataOffset = spreader.ReadInt32();
            frameHeader.Comment = ReadASCIIString(spreader, spreader.ReadInt16());
            for (int i = 0; i != 32; ++i)
            {
                frameHeader.Reserve[i] = ReadASCIIString(spreader, spreader.ReadInt16());
            }

            int numberOfChannels = spreader.ReadInt16();
            int[] offsetChannelLength = new int[numberOfChannels];

            frameHeader.MaxChannelLength = spreader.ReadInt32();
            for (int i = 0; i != numberOfChannels; ++i)
            {
                offsetChannelLength[i] = spreader.ReadInt32();
            }

            //  Проверка компресии данных.
            if (spreader.ReadInt32() != 0)
            {
                throw new NotSupportedException("Произошла попытка чтения файла с компресией данных.");
            }

            if (numberOfChannels > 0)
            {
                for (int i = 0; i != numberOfChannels; ++i)
                {
                    int channelLength = 0;
                    while (spreader.ReadPosition < offsetChannelLength[i] - 2)
                    {
                        spreader.ReadUInt8();
                    }

                    //spreader.Position = offsetChannelLength[i] - 2;
                    CatmanChannelHeader channelHeader = new(string.Empty, string.Empty, 0)
                    {
                        LocationInDatabase = spreader.ReadInt16()
                    };
                    channelLength = spreader.ReadInt32();
                    Signal signal = new(1, channelLength);

                    channelHeader.Name = ReadASCIIString(spreader, spreader.ReadInt16());
                    channelHeader.Unit = ReadASCIIString(spreader, spreader.ReadInt16());
                    channelHeader.Comment = ReadASCIIString(spreader, spreader.ReadInt16());
                    channelHeader.DataFormat = (CatmanDataFormat)spreader.ReadInt16();
                    channelHeader.DataWidth = spreader.ReadInt16();
                    channelHeader.Time = DateTime.FromOADate(spreader.ReadFloat64());
                    channelHeader.SizeOfExtendedHeader = spreader.ReadInt32();

                    if (channelHeader.SizeOfExtendedHeader < 148)
                    {
                        throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
                    }

                    channelHeader.StartTime = DateTime.FromOADate(spreader.ReadFloat64());
                    signal.Sampling = 1000 / spreader.ReadFloat64();
                    channelHeader.CodeOfSensorType = (CatmanCodeOfSensorType)spreader.ReadInt16();
                    channelHeader.CodeOfSupplyVoltage = (CatmanCodeOfSupplyVoltage)spreader.ReadInt16();
                    channelHeader.CodeOfFilterCharacteristics = (CatmanCodeOfFilterCharacteristic)spreader.ReadInt16();
                    channelHeader.CodeOfFilterFrequency = (CatmanCodeOfFilterFrequency)spreader.ReadInt16();
                    channelHeader.TareValue = spreader.ReadFloat32();
                    channelHeader.ZeroValue = spreader.ReadFloat32();
                    channelHeader.CodeOfMeasuringRange = (CatmanCodeOfMeasuringRange)spreader.ReadFloat32();
                    channelHeader.InputCharacteristics = new double[4];
                    channelHeader.InputCharacteristics[0] = spreader.ReadFloat32();
                    channelHeader.InputCharacteristics[1] = spreader.ReadFloat32();
                    channelHeader.InputCharacteristics[2] = spreader.ReadFloat32();
                    channelHeader.InputCharacteristics[3] = spreader.ReadFloat32();
                    channelHeader.AmplifierSerialNumber = ReadASCIIString(spreader, 32);
                    channelHeader.PhysicalUnit = ReadASCIIString(spreader, 8);
                    channelHeader.NativeUnit = ReadASCIIString(spreader, 8);
                    channelHeader.HardwareSlotNumber = spreader.ReadInt16();
                    channelHeader.HardwareSubSlotNumber = spreader.ReadInt16();
                    channelHeader.CodeOfAmplifierType = spreader.ReadInt16();
                    channelHeader.CodeOfAPConnectorType = spreader.ReadInt16();
                    channelHeader.GageFactor = spreader.ReadFloat32();
                    channelHeader.BridgeFactor = spreader.ReadFloat32();
                    channelHeader.CodeOfMeasurementSignal = spreader.ReadInt16();
                    channelHeader.CodeOfAmplifierInput = spreader.ReadInt16();
                    channelHeader.CodeOfHighpassFilter = spreader.ReadInt16();
                    channelHeader.OnlineImportInfo = spreader.ReadUInt8();
                    channelHeader.CodeOfScaleType = spreader.ReadUInt8();
                    channelHeader.SoftwareZeroValue = spreader.ReadFloat32();
                    channelHeader.WriteProtected = spreader.ReadUInt8();
                    channelHeader.Alignment = new byte[3];
                    channelHeader.Alignment[0] = spreader.ReadUInt8();
                    channelHeader.Alignment[1] = spreader.ReadUInt8();
                    channelHeader.Alignment[2] = spreader.ReadUInt8();
                    channelHeader.NominalRange = spreader.ReadFloat32();
                    channelHeader.CableLengthCompensation = spreader.ReadFloat32();
                    channelHeader.ExportFormat = spreader.ReadUInt8();
                    channelHeader.ChannelType = spreader.ReadUInt8();
                    channelHeader.EDaqConnectorOnLayer = spreader.ReadUInt8();
                    channelHeader.EDaqLayer = spreader.ReadUInt8();
                    channelHeader.ContentType = spreader.ReadUInt8();
                    channelHeader.Reserved = new byte[3];
                    channelHeader.Reserved[0] = spreader.ReadUInt8();
                    channelHeader.Reserved[1] = spreader.ReadUInt8();
                    channelHeader.Reserved[2] = spreader.ReadUInt8();

                    channelHeader.LinearisationMode = spreader.ReadUInt8();
                    channelHeader.UserScaleType = spreader.ReadUInt8();
                    channelHeader.NumberOfPointsScaleTable = spreader.ReadUInt8();
                    if (channelHeader.NumberOfPointsScaleTable != 0)
                    {
                        channelHeader.PointsScaleTable = new double[channelHeader.NumberOfPointsScaleTable];
                        for (int j = 0; j != channelHeader.NumberOfPointsScaleTable; ++j)
                        {
                            channelHeader.PointsScaleTable[j] = spreader.ReadFloat64();
                        }
                    }

                    channelHeader.ThermoType = spreader.ReadInt16();
                    channelHeader.Formula = ReadASCIIString(spreader, spreader.ReadInt16());
                    channelHeader.SensorInfo = new CatmanSensorInfo();
                    int sensorInfoSize = spreader.ReadInt32();
                    if (sensorInfoSize != 68)
                    {
                        throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
                    }
                    channelHeader.SensorInfo.InUse = spreader.ReadInt16() != 0;
                    channelHeader.SensorInfo.Description = ReadASCIIString(spreader, 50);
                    channelHeader.SensorInfo.Tid = ReadASCIIString(spreader, 16);

                    Channels.Add(new Channel(channelHeader, signal));
                }

                while (spreader.ReadPosition < dataOffset)
                {
                    spreader.ReadUInt8();
                }

                foreach (Channel channel in Channels)
                {
                    CatmanChannelHeader channelHeader = (CatmanChannelHeader)channel.Header;
                    switch (channelHeader.ExportFormat)
                    {
                        case 0:
                            for (int i = 0; i != channel.Length; ++i)
                            {
                                channel[i] = spreader.ReadFloat64();
                            }
                            break;
                        case 1:
                            for (int i = 0; i != channel.Length; ++i)
                            {
                                channel[i] = spreader.ReadFloat32();
                            }
                            break;
                        case 2:
                            channelHeader.MinValueFactor = spreader.ReadFloat64();
                            channelHeader.MinValueFactor = spreader.ReadFloat64();
                            for (int i = 0; i != channel.Length; ++i)
                            {
                                channel[i] = spreader.ReadInt16() / 32767 * (channelHeader.MinValueFactor - channelHeader.MinValueFactor) + channelHeader.MinValueFactor;
                            }
                            break;
                        default:
                            throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
                    }
                }

                //  Проверка Id Post data ares
                if (spreader.ReadInt16() != 4001)
                {
                    throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
                }

                foreach (Channel channel in Channels)
                {
                    ((CatmanChannelHeader)channel.Header).FormatString = ReadASCIIString(spreader, spreader.ReadInt16());
                }
            }
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Произошла ошибка при чтении файла.", ex);
        }
        return frameHeader;
    }

    static void SaveCatmanFrame(Frame frame, Spreader spreader)
    {
        void writeString(string text)
        {
            spreader.WriteInt16((short)text.Length);
            if (text.Length > 0)
            {
                WriteASCIIString(spreader, text, text.Length, false);
            }
        }

        try
        {
            CatmanFrameHeader frameHeader = FrameHeaderConverter.ToCatman(frame.Header);

            int numberOfChannels = frame.Channels.Count;
            int[] offsetChannelLength = new int[numberOfChannels];
            List<CatmanChannelHeader> channelHeaders = new();

            int dataOffset = 2 + 4 + 2 + frameHeader.Comment.Length;
            for (int i = 0; i != 32; ++i)
            {
                dataOffset += 2 + frameHeader.Reserve[i].Length;
            }
            dataOffset += 2 + 4 + numberOfChannels * 4 + 4;

            for (int i = 0; i != numberOfChannels; ++i)
            {
                Channel channel = frame.Channels[i];
                CatmanChannelHeader channelHeader = ChannelHeaderConverter.ToCatman(channel.Header);
                channelHeaders.Add(channelHeader);
                offsetChannelLength[i] = dataOffset + 2;

                dataOffset += 2 + 4 + 2 + channelHeader.Name.Length
                        + 2 + channelHeader.Unit.Length + 2 + channelHeader.Comment.Length;

                dataOffset += 2 + 2 + 8 + 4 + 8 + 8 + 2 + 2 + 2 + 2 + 4 + 4 + 4 + 4 * 4 + 32
                    + 8 + 8 + 2 * 4 + 2 * 4 + 2 * 3 + 2 + 4 + 4 + 4 * 2 + 2 + 9 + channelHeader.NumberOfPointsScaleTable * 8
                    + 2 + 2 + channelHeader.Formula.Length + 4 + 2 + 50 + 16;
            }

            spreader.WriteUInt16((ushort)0x1394); //  2
            spreader.WriteInt32((int)dataOffset);  //  4

            writeString(frameHeader.Comment);
            for (int i = 0; i != 32; ++i)
            {
                writeString(frameHeader.Reserve[i]);
            }

            spreader.WriteInt16((short)numberOfChannels);    //  2
            spreader.WriteInt32((int)frameHeader.MaxChannelLength);    //  4
            for (int i = 0; i != numberOfChannels; ++i)
            {
                spreader.WriteInt32((int)offsetChannelLength[i]);  //  4
            }

            spreader.WriteInt32((int)0);   //  4

            if (numberOfChannels > 0)
            {
                for (int i = 0; i != numberOfChannels; ++i)
                {
                    if (offsetChannelLength[i] != spreader.WritePosition + 2)
                    {
                        throw new Exception();
                    }

                    Channel channel = frame.Channels[i];
                    CatmanChannelHeader channelHeader = channelHeaders[i];

                    int channelLength = channel.Length;
                    spreader.WriteInt16((short)channelHeader.LocationInDatabase);    //  2
                    spreader.WriteInt32((int)channelLength);   //  4
                    writeString(channelHeader.Name);
                    writeString(channelHeader.Unit);
                    writeString(channelHeader.Comment);

                    spreader.WriteInt16((short)channelHeader.DataFormat);    //  2
                    spreader.WriteInt16((short)channelHeader.DataWidth);    //  2
                    spreader.WriteFloat64((double)channelHeader.Time.ToOADate());    //  8

                    spreader.WriteInt32((int)148);    //  4
                    spreader.WriteFloat64((double)channelHeader.StartTime.ToOADate());    //  8
                    spreader.WriteFloat64((double)(1000 / channel.Sampling));    //  8

                    spreader.WriteInt16((short)channelHeader.CodeOfSensorType);  //  2
                    spreader.WriteInt16((short)channelHeader.CodeOfSupplyVoltage);  //  2
                    spreader.WriteInt16((short)channelHeader.CodeOfFilterCharacteristics);  //  2
                    spreader.WriteInt16((short)channelHeader.CodeOfFilterFrequency);  //  2
                    spreader.WriteFloat32((float)channelHeader.TareValue);    //  4
                    spreader.WriteFloat32((float)channelHeader.ZeroValue);    //  4
                    spreader.WriteFloat32((float)channelHeader.CodeOfMeasuringRange);    //  4

                    spreader.WriteFloat32((float)channelHeader.InputCharacteristics[0]);    //  4
                    spreader.WriteFloat32((float)channelHeader.InputCharacteristics[1]);    //  4
                    spreader.WriteFloat32((float)channelHeader.InputCharacteristics[2]);    //  4
                    spreader.WriteFloat32((float)channelHeader.InputCharacteristics[3]);    //  4

                    WriteASCIIString(spreader, channelHeader.AmplifierSerialNumber, 32, false);   //  32
                    WriteASCIIString(spreader, channelHeader.PhysicalUnit, 8, false); //  8
                    WriteASCIIString(spreader, channelHeader.NativeUnit, 8, false);   //  8

                    spreader.WriteInt16((short)channelHeader.HardwareSlotNumber);  //  2
                    spreader.WriteInt16((short)channelHeader.HardwareSubSlotNumber);  //  2
                    spreader.WriteInt16((short)channelHeader.CodeOfAmplifierType);  //  2
                    spreader.WriteInt16((short)channelHeader.CodeOfAPConnectorType);  //  2

                    spreader.WriteFloat32((float)channelHeader.GageFactor);    //  4
                    spreader.WriteFloat32((float)channelHeader.BridgeFactor);    //  4

                    spreader.WriteInt16((short)channelHeader.CodeOfMeasurementSignal);  //  2
                    spreader.WriteInt16((short)channelHeader.CodeOfAmplifierInput);  //  2
                    spreader.WriteInt16((short)channelHeader.CodeOfHighpassFilter);  //  2

                    spreader.WriteUInt8((byte)channelHeader.OnlineImportInfo);  //  1
                    spreader.WriteUInt8((byte)channelHeader.CodeOfScaleType);   //  1

                    spreader.WriteFloat32((float)channelHeader.SoftwareZeroValue);   //  4

                    spreader.WriteUInt8((byte)channelHeader.WriteProtected);   //  1
                    spreader.WriteUInt8((byte)0);   //  1
                    spreader.WriteUInt8((byte)0);   //  1
                    spreader.WriteUInt8((byte)0);   //  1

                    spreader.WriteFloat32((float)channelHeader.NominalRange);    //  4
                    spreader.WriteFloat32((float)channelHeader.CableLengthCompensation);    //  4

                    spreader.WriteUInt8((byte)channelHeader.ExportFormat);  //  1
                    spreader.WriteInt8((sbyte)channelHeader.ChannelType);    //  1

                    spreader.WriteUInt8((byte)channelHeader.EDaqConnectorOnLayer);  //  1
                    spreader.WriteUInt8((byte)channelHeader.EDaqLayer);  //  1
                    spreader.WriteUInt8((byte)channelHeader.ContentType);  //  1
                    spreader.WriteUInt8((byte)0);  //  1
                    spreader.WriteUInt8((byte)0);  //  1
                    spreader.WriteUInt8((byte)0);  //  1
                    spreader.WriteUInt8((byte)channelHeader.LinearisationMode);  //  1
                    spreader.WriteUInt8((byte)channelHeader.UserScaleType);  //  1
                    spreader.WriteUInt8((byte)channelHeader.NumberOfPointsScaleTable);  //  1
                    if (channelHeader.NumberOfPointsScaleTable != 0)
                    {
                        for (int j = 0; j != channelHeader.NumberOfPointsScaleTable; ++j)
                        {
                            spreader.WriteFloat64((double)channelHeader.PointsScaleTable[j]); //  8
                        }
                    }

                    spreader.WriteInt16((System.Int16)channelHeader.ThermoType);    //  2
                    writeString(channelHeader.Formula);

                    spreader.WriteInt32((System.Int32)68);  //  4
                    spreader.WriteInt16((System.Int16)(channelHeader.SensorInfo.InUse ? 1 : 0));    //  2

                    WriteASCIIString(spreader, channelHeader.SensorInfo.Description, 50, false);    //  50
                    WriteASCIIString(spreader, channelHeader.SensorInfo.Tid, 16, false);    //  16
                }

                //    dataOffset = stream.Position
                for (int i = 0; i != numberOfChannels; ++i)
                {
                    Channel channel = frame.Channels[i];
                    CatmanChannelHeader channelHeader = channelHeaders[i];
                    switch (channelHeader.ExportFormat)
                    {
                        case 0:
                            for (int j = 0; j != channel.Length; ++j)
                            {
                                spreader.WriteFloat64((double)channel[j]);
                            }
                            break;
                        case 1:
                            for (int j = 0; j != channel.Length; ++j)
                            {
                                spreader.WriteFloat32((float)channel[j]);
                            }
                            break;
                        case 2:
                            spreader.WriteFloat64((double)channelHeader.MinValueFactor);
                            spreader.WriteFloat64((double)channelHeader.MinValueFactor);
                            for (int j = 0; j != channel.Length; ++j)
                            {
                                spreader.WriteInt16((short)(32767 * (channel[j] - channelHeader.MinValueFactor) / (channelHeader.MinValueFactor - channelHeader.MinValueFactor)));
                            }
                            break;
                        default:
                            throw new InvalidDataException("Произошла попытка сохранить файл некорректного формата.");
                    }
                }
                spreader.WriteInt16((short)4001);


                for (int i = 0; i != numberOfChannels; ++i)
                {
                    CatmanChannelHeader channelHeader = channelHeaders[i];
                    writeString(channelHeader.FormatString);
                }
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Произошла ошибка при записи файла.", ex);
        }
    }

    unsafe TestLabFrameHeader LoadTestLabFrame(Spreader spreader)
    {
        //  Чтение заголовка кадра и количества зарегистрированных каналов.
        (TestLabFrameHeader frameHeader, int channelCount) = TestLabFrameHeader.Load(spreader);


        if (channelCount == 0)
        {
            //if (stream.Length != 350)
            //{
            //    throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
            //}
        }
        else
        {
            long fullSize = 350 + 192 * channelCount;
            //if (stream.Length < fullSize)
            //{
            //    throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
            //}

            for (int i = 0; i != channelCount; ++i)
            {

                TestLabChannelHeader channelHeader = new(string.Empty, string.Empty, 0)
                {
                    Name = ReadASCIIString(spreader, 13),
                    Description = ReadASCIIString(spreader, 121),
                    Unit = ReadASCIIString(spreader, 13),
                    Offset = spreader.ReadFloat32(),
                    Scale = spreader.ReadFloat32(),
                    Cutoff = spreader.ReadFloat32(),
                };

                Signal signal = new(spreader.ReadUInt16());

                channelHeader.Type = TestLabChannelHeader.Validation((TestLabChannelType)spreader.ReadUInt8());
                channelHeader.DataFormat = TestLabChannelHeader.Validation((TestLabDataFormat)spreader.ReadUInt8());


                uint channelLength = spreader.ReadUInt32();
                if (channelLength > int.MaxValue)
                {
                    throw new InvalidOperationException("Файл содержит канал слишком большой длины.");
                }
                signal.Length = (int)channelLength;

                Channel channel = new(channelHeader, signal);
                Channels.Add(channel);
                _ = spreader.ReadBytes(25);
                //spreader.Position += 25;

                fullSize += channelLength * TestLabChannelHeader.GetItemSize(channelHeader.DataFormat) + 2;
            }

            //if (stream.Length < fullSize)
            //{
            //    throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
            //}

            for (int i = 0; i != channelCount; ++i)
            {
                Channel channel = Channels[i];
                TestLabChannelHeader header = (TestLabChannelHeader)channel.Header;
                int length = channel.Length;
                byte[] buffer = spreader.ReadBytes(length * TestLabChannelHeader.GetItemSize(header.DataFormat));
                double offset = header.Offset;
                double scale = header.Scale;
                var destination = channel.Vector;

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

                if (spreader.ReadUInt16() != 65535)
                {
                    throw new InvalidDataException("Произошла попытка загрузить файл некорректного формата.");
                }
            }
        }
        return frameHeader;
    }

    unsafe static void SaveTestLabFrame(Frame frame, Spreader spreader)
    {
        TestLabFrameHeader frameHeader = FrameHeaderConverter.ToTestLab(frame.Header);
        ChannelCollection channels = frame.Channels;

        spreader.WriteUInt64((System.UInt64)0x42414C54534554UL);
        WriteASCIIString(spreader, frameHeader.Title, 61, true);
        WriteASCIIString(spreader, frameHeader.Character, 121, true);
        WriteASCIIString(spreader, frameHeader.Region, 121, true);
        WriteASCIIString(spreader, frameHeader.Time.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture), 11, true);
        WriteASCIIString(spreader, frameHeader.Time.ToString("HH:mm:ss", CultureInfo.InvariantCulture), 9, true);
        spreader.WriteUInt16((System.UInt16)channels.Count);
        for (int i = 0; i != 17; ++i)
        {
            spreader.WriteUInt8((System.Byte)0);
        }

        foreach (Channel channel in channels)
        {
            TestLabChannelHeader channelHeader = ChannelHeaderConverter.ToTestLab(channel.Header);
            WriteASCIIString(spreader, channelHeader.Name, 13, true);
            WriteASCIIString(spreader, channelHeader.Description, 121, true);
            WriteASCIIString(spreader, channelHeader.Unit, 13, true);
            spreader.WriteFloat32((float)channelHeader.Offset);
            spreader.WriteFloat32((float)channelHeader.Scale);
            spreader.WriteFloat32((float)channelHeader.Cutoff);
            spreader.WriteUInt16((System.UInt16)channel.Sampling);
            spreader.WriteUInt8((byte)channelHeader.Type);
            spreader.WriteUInt8((byte)channelHeader.DataFormat);
            spreader.WriteUInt32((System.UInt32)channel.Length);
            for (int i = 0; i != 25; ++i)
            {
                spreader.WriteUInt8((System.Byte)0);
            }
        }

        foreach (Channel channel in channels)
        {
            TestLabChannelHeader header = ChannelHeaderConverter.ToTestLab(channel.Header);
            int length = channel.Length;
            double offset = header.Offset;
            double factor = 0;
            if (header.Scale != 0)
            {
                factor = 1 / header.Scale;
            }
            var source = channel.Vector;
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
            spreader.WriteBytes(buffer, 0, buffer.Length);
            spreader.WriteUInt16((System.UInt16)65535);
        }
    }

    [SuppressUnmanagedCodeSecurity]
    static string ReadASCIIString(Spreader spreader, int length)
    {
        StringBuilder text = new();
        byte[] bytes = spreader.ReadBytes(length);
        {
            int index = 0;
            while (index < length && bytes[index] != 0)
            {
                switch (bytes[index])
                {
                    case 0x20: text.Append(' '); break;
                    case 0x21: text.Append('!'); break;
                    case 0x22: text.Append('\"'); break;
                    case 0x23: text.Append('#'); break;
                    case 0x24: text.Append('$'); break;
                    case 0x25: text.Append('%'); break;
                    case 0x26: text.Append('&'); break;
                    case 0x27: text.Append('\''); break;
                    case 0x28: text.Append('('); break;
                    case 0x29: text.Append(')'); break;
                    case 0x2A: text.Append('*'); break;
                    case 0x2B: text.Append('+'); break;
                    case 0x2C: text.Append(','); break;
                    case 0x2D: text.Append('-'); break;
                    case 0x2E: text.Append('.'); break;
                    case 0x2F: text.Append('/'); break;

                    case 0x30: text.Append('0'); break;
                    case 0x31: text.Append('1'); break;
                    case 0x32: text.Append('2'); break;
                    case 0x33: text.Append('3'); break;
                    case 0x34: text.Append('4'); break;
                    case 0x35: text.Append('5'); break;
                    case 0x36: text.Append('6'); break;
                    case 0x37: text.Append('7'); break;
                    case 0x38: text.Append('8'); break;
                    case 0x39: text.Append('9'); break;
                    case 0x3A: text.Append(':'); break;
                    case 0x3B: text.Append(';'); break;
                    case 0x3C: text.Append('<'); break;
                    case 0x3D: text.Append('='); break;
                    case 0x3E: text.Append('>'); break;
                    case 0x3F: text.Append('?'); break;

                    case 0x40: text.Append('@'); break;
                    case 0x41: text.Append('A'); break;
                    case 0x42: text.Append('B'); break;
                    case 0x43: text.Append('C'); break;
                    case 0x44: text.Append('D'); break;
                    case 0x45: text.Append('E'); break;
                    case 0x46: text.Append('F'); break;
                    case 0x47: text.Append('G'); break;
                    case 0x48: text.Append('H'); break;
                    case 0x49: text.Append('I'); break;
                    case 0x4A: text.Append('J'); break;
                    case 0x4B: text.Append('K'); break;
                    case 0x4C: text.Append('L'); break;
                    case 0x4D: text.Append('M'); break;
                    case 0x4E: text.Append('N'); break;
                    case 0x4F: text.Append('O'); break;

                    case 0x50: text.Append('P'); break;
                    case 0x51: text.Append('Q'); break;
                    case 0x52: text.Append('R'); break;
                    case 0x53: text.Append('S'); break;
                    case 0x54: text.Append('T'); break;
                    case 0x55: text.Append('U'); break;
                    case 0x56: text.Append('V'); break;
                    case 0x57: text.Append('W'); break;
                    case 0x58: text.Append('X'); break;
                    case 0x59: text.Append('Y'); break;
                    case 0x5A: text.Append('Z'); break;
                    case 0x5B: text.Append('['); break;
                    case 0x5C: text.Append('\\'); break;
                    case 0x5D: text.Append(']'); break;
                    case 0x5E: text.Append('^'); break;
                    case 0x5F: text.Append('_'); break;

                    case 0x60: text.Append('`'); break;
                    case 0x61: text.Append('a'); break;
                    case 0x62: text.Append('b'); break;
                    case 0x63: text.Append('c'); break;
                    case 0x64: text.Append('d'); break;
                    case 0x65: text.Append('e'); break;
                    case 0x66: text.Append('f'); break;
                    case 0x67: text.Append('g'); break;
                    case 0x68: text.Append('h'); break;
                    case 0x69: text.Append('i'); break;
                    case 0x6A: text.Append('j'); break;
                    case 0x6B: text.Append('k'); break;
                    case 0x6C: text.Append('l'); break;
                    case 0x6D: text.Append('m'); break;
                    case 0x6E: text.Append('n'); break;
                    case 0x6F: text.Append('o'); break;

                    case 0x70: text.Append('p'); break;
                    case 0x71: text.Append('q'); break;
                    case 0x72: text.Append('r'); break;
                    case 0x73: text.Append('s'); break;
                    case 0x74: text.Append('t'); break;
                    case 0x75: text.Append('u'); break;
                    case 0x76: text.Append('v'); break;
                    case 0x77: text.Append('w'); break;
                    case 0x78: text.Append('x'); break;
                    case 0x79: text.Append('y'); break;
                    case 0x7A: text.Append('z'); break;
                    case 0x7B: text.Append('{'); break;
                    case 0x7C: text.Append('|'); break;
                    case 0x7D: text.Append('}'); break;
                    case 0x7E: text.Append('~'); break;
                    case 0x7F: text.Append(' '); break;

                    case 0x80: text.Append('Ђ'); break;
                    case 0x81: text.Append('Ѓ'); break;
                    case 0x82: text.Append('‚'); break;
                    case 0x83: text.Append('ѓ'); break;
                    case 0x84: text.Append('„'); break;
                    case 0x85: text.Append('…'); break;
                    case 0x86: text.Append('†'); break;
                    case 0x87: text.Append('‡'); break;
                    case 0x88: text.Append('€'); break;
                    case 0x89: text.Append('‰'); break;
                    case 0x8A: text.Append('Љ'); break;
                    case 0x8B: text.Append('‹'); break;
                    case 0x8C: text.Append('Њ'); break;
                    case 0x8D: text.Append('Ќ'); break;
                    case 0x8E: text.Append('Ћ'); break;
                    case 0x8F: text.Append('Џ'); break;

                    case 0x90: text.Append('ђ'); break;
                    case 0x91: text.Append('‘'); break;
                    case 0x92: text.Append('’'); break;
                    case 0x93: text.Append('“'); break;
                    case 0x94: text.Append('”'); break;
                    case 0x95: text.Append('•'); break;
                    case 0x96: text.Append('–'); break;
                    case 0x97: text.Append('—'); break;
                    case 0x98: text.Append(' '); break;
                    case 0x99: text.Append('™'); break;
                    case 0x9A: text.Append('љ'); break;
                    case 0x9B: text.Append('›'); break;
                    case 0x9C: text.Append('њ'); break;
                    case 0x9D: text.Append('ќ'); break;
                    case 0x9E: text.Append('ћ'); break;
                    case 0x9F: text.Append('џ'); break;

                    case 0xA0: text.Append(' '); break;
                    case 0xA1: text.Append('Ў'); break;
                    case 0xA2: text.Append('ў'); break;
                    case 0xA3: text.Append('Ј'); break;
                    case 0xA4: text.Append('¤'); break;
                    case 0xA5: text.Append('Ґ'); break;
                    case 0xA6: text.Append('¦'); break;
                    case 0xA7: text.Append('§'); break;
                    case 0xA8: text.Append('Ё'); break;
                    case 0xA9: text.Append('©'); break;
                    case 0xAA: text.Append('Є'); break;
                    case 0xAB: text.Append('«'); break;
                    case 0xAC: text.Append('¬'); break;
                    case 0xAD: text.Append(' '); break;
                    case 0xAE: text.Append('®'); break;
                    case 0xAF: text.Append('Ї'); break;

                    case 0xB0: text.Append('°'); break;
                    case 0xB1: text.Append('±'); break;
                    case 0xB2: text.Append('І'); break;
                    case 0xB3: text.Append('і'); break;
                    case 0xB4: text.Append('ґ'); break;
                    case 0xB5: text.Append('µ'); break;
                    case 0xB6: text.Append('¶'); break;
                    case 0xB7: text.Append('·'); break;
                    case 0xB8: text.Append('ё'); break;
                    case 0xB9: text.Append('№'); break;
                    case 0xBA: text.Append('є'); break;
                    case 0xBB: text.Append('»'); break;
                    case 0xBC: text.Append('ј'); break;
                    case 0xBD: text.Append('Ѕ'); break;
                    case 0xBE: text.Append('ѕ'); break;
                    case 0xBF: text.Append('ї'); break;

                    case 0xC0: text.Append('А'); break;
                    case 0xC1: text.Append('Б'); break;
                    case 0xC2: text.Append('В'); break;
                    case 0xC3: text.Append('Г'); break;
                    case 0xC4: text.Append('Д'); break;
                    case 0xC5: text.Append('Е'); break;
                    case 0xC6: text.Append('Ж'); break;
                    case 0xC7: text.Append('З'); break;
                    case 0xC8: text.Append('И'); break;
                    case 0xC9: text.Append('Й'); break;
                    case 0xCA: text.Append('К'); break;
                    case 0xCB: text.Append('Л'); break;
                    case 0xCC: text.Append('М'); break;
                    case 0xCD: text.Append('Н'); break;
                    case 0xCE: text.Append('О'); break;
                    case 0xCF: text.Append('П'); break;

                    case 0xD0: text.Append('Р'); break;
                    case 0xD1: text.Append('С'); break;
                    case 0xD2: text.Append('Т'); break;
                    case 0xD3: text.Append('У'); break;
                    case 0xD4: text.Append('Ф'); break;
                    case 0xD5: text.Append('Х'); break;
                    case 0xD6: text.Append('Ц'); break;
                    case 0xD7: text.Append('Ч'); break;
                    case 0xD8: text.Append('Ш'); break;
                    case 0xD9: text.Append('Щ'); break;
                    case 0xDA: text.Append('Ъ'); break;
                    case 0xDB: text.Append('Ы'); break;
                    case 0xDC: text.Append('Ь'); break;
                    case 0xDD: text.Append('Э'); break;
                    case 0xDE: text.Append('Ю'); break;
                    case 0xDF: text.Append('Я'); break;

                    case 0xE0: text.Append('а'); break;
                    case 0xE1: text.Append('б'); break;
                    case 0xE2: text.Append('в'); break;
                    case 0xE3: text.Append('г'); break;
                    case 0xE4: text.Append('д'); break;
                    case 0xE5: text.Append('е'); break;
                    case 0xE6: text.Append('ж'); break;
                    case 0xE7: text.Append('з'); break;
                    case 0xE8: text.Append('и'); break;
                    case 0xE9: text.Append('й'); break;
                    case 0xEA: text.Append('к'); break;
                    case 0xEB: text.Append('л'); break;
                    case 0xEC: text.Append('м'); break;
                    case 0xED: text.Append('н'); break;
                    case 0xEE: text.Append('о'); break;
                    case 0xEF: text.Append('п'); break;

                    case 0xF0: text.Append('р'); break;
                    case 0xF1: text.Append('с'); break;
                    case 0xF2: text.Append('т'); break;
                    case 0xF3: text.Append('у'); break;
                    case 0xF4: text.Append('ф'); break;
                    case 0xF5: text.Append('х'); break;
                    case 0xF6: text.Append('ц'); break;
                    case 0xF7: text.Append('ч'); break;
                    case 0xF8: text.Append('ш'); break;
                    case 0xF9: text.Append('щ'); break;
                    case 0xFA: text.Append('ъ'); break;
                    case 0xFB: text.Append('ы'); break;
                    case 0xFC: text.Append('ь'); break;
                    case 0xFD: text.Append('э'); break;
                    case 0xFE: text.Append('ю'); break;
                    case 0xFF: text.Append('я'); break;

                    default: text.Append(' '); break;
                }
                ++index;
            }
        }
        return text.ToString();
    }


    [SuppressUnmanagedCodeSecurity]
    static void WriteASCIIString(Spreader spreader, string value, int length, bool isLastNull)
    {
        byte[] bytes = new byte[length];
        {
            if (value != null)
            {
                char[] chars = value.ToCharArray();
                int copyLength = chars.Length;
                if (copyLength > 0)
                {
                    if (copyLength > length)
                    {
                        copyLength = length;
                    }

                    {
                        for (int i = 0; i != copyLength; ++i)
                        {
                            bytes[i] = chars[i] switch
                            {
                                ' ' => 0x20,
                                '!' => 0x21,
                                '\"' => 0x22,
                                '#' => 0x23,
                                '$' => 0x24,
                                '%' => 0x25,
                                '&' => 0x26,
                                '\'' => 0x27,
                                '(' => 0x28,
                                ')' => 0x29,
                                '*' => 0x2A,
                                '+' => 0x2B,
                                ',' => 0x2C,
                                '-' => 0x2D,
                                '.' => 0x2E,
                                '/' => 0x2F,
                                '0' => 0x30,
                                '1' => 0x31,
                                '2' => 0x32,
                                '3' => 0x33,
                                '4' => 0x34,
                                '5' => 0x35,
                                '6' => 0x36,
                                '7' => 0x37,
                                '8' => 0x38,
                                '9' => 0x39,
                                ':' => 0x3A,
                                ';' => 0x3B,
                                '<' => 0x3C,
                                '=' => 0x3D,
                                '>' => 0x3E,
                                '?' => 0x3F,
                                '@' => 0x40,
                                'A' => 0x41,
                                'B' => 0x42,
                                'C' => 0x43,
                                'D' => 0x44,
                                'E' => 0x45,
                                'F' => 0x46,
                                'G' => 0x47,
                                'H' => 0x48,
                                'I' => 0x49,
                                'J' => 0x4A,
                                'K' => 0x4B,
                                'L' => 0x4C,
                                'M' => 0x4D,
                                'N' => 0x4E,
                                'O' => 0x4F,
                                'P' => 0x50,
                                'Q' => 0x51,
                                'R' => 0x52,
                                'S' => 0x53,
                                'T' => 0x54,
                                'U' => 0x55,
                                'V' => 0x56,
                                'W' => 0x57,
                                'X' => 0x58,
                                'Y' => 0x59,
                                'Z' => 0x5A,
                                '[' => 0x5B,
                                '\\' => 0x5C,
                                ']' => 0x5D,
                                '^' => 0x5E,
                                '_' => 0x5F,
                                '`' => 0x60,
                                'a' => 0x61,
                                'b' => 0x62,
                                'c' => 0x63,
                                'd' => 0x64,
                                'e' => 0x65,
                                'f' => 0x66,
                                'g' => 0x67,
                                'h' => 0x68,
                                'i' => 0x69,
                                'j' => 0x6A,
                                'k' => 0x6B,
                                'l' => 0x6C,
                                'm' => 0x6D,
                                'n' => 0x6E,
                                'o' => 0x6F,
                                'p' => 0x70,
                                'q' => 0x71,
                                'r' => 0x72,
                                's' => 0x73,
                                't' => 0x74,
                                'u' => 0x75,
                                'v' => 0x76,
                                'w' => 0x77,
                                'x' => 0x78,
                                'y' => 0x79,
                                'z' => 0x7A,
                                '{' => 0x7B,
                                '|' => 0x7C,
                                '}' => 0x7D,
                                '~' => 0x7E,
                                'Ђ' => 0x80,
                                'Ѓ' => 0x81,
                                '‚' => 0x82,
                                'ѓ' => 0x83,
                                '„' => 0x84,
                                '…' => 0x85,
                                '†' => 0x86,
                                '‡' => 0x87,
                                '€' => 0x88,
                                '‰' => 0x89,
                                'Љ' => 0x8A,
                                '‹' => 0x8B,
                                'Њ' => 0x8C,
                                'Ќ' => 0x8D,
                                'Ћ' => 0x8E,
                                'Џ' => 0x8F,
                                'ђ' => 0x90,
                                '‘' => 0x91,
                                '’' => 0x92,
                                '“' => 0x93,
                                '”' => 0x94,
                                '•' => 0x95,
                                '–' => 0x96,
                                '—' => 0x97,
                                '™' => 0x99,
                                'љ' => 0x9A,
                                '›' => 0x9B,
                                'њ' => 0x9C,
                                'ќ' => 0x9D,
                                'ћ' => 0x9E,
                                'џ' => 0x9F,
                                'Ў' => 0xA1,
                                'ў' => 0xA2,
                                'Ј' => 0xA3,
                                '¤' => 0xA4,
                                'Ґ' => 0xA5,
                                '¦' => 0xA6,
                                '§' => 0xA7,
                                'Ё' => 0xA8,
                                '©' => 0xA9,
                                'Є' => 0xAA,
                                '«' => 0xAB,
                                '¬' => 0xAC,
                                '®' => 0xAE,
                                'Ї' => 0xAF,
                                '°' => 0xB0,
                                '±' => 0xB1,
                                'І' => 0xB2,
                                'і' => 0xB3,
                                'ґ' => 0xB4,
                                'µ' => 0xB5,
                                '¶' => 0xB6,
                                '·' => 0xB7,
                                'ё' => 0xB8,
                                '№' => 0xB9,
                                'є' => 0xBA,
                                '»' => 0xBB,
                                'ј' => 0xBC,
                                'Ѕ' => 0xBD,
                                'ѕ' => 0xBE,
                                'ї' => 0xBF,
                                'А' => 0xC0,
                                'Б' => 0xC1,
                                'В' => 0xC2,
                                'Г' => 0xC3,
                                'Д' => 0xC4,
                                'Е' => 0xC5,
                                'Ж' => 0xC6,
                                'З' => 0xC7,
                                'И' => 0xC8,
                                'Й' => 0xC9,
                                'К' => 0xCA,
                                'Л' => 0xCB,
                                'М' => 0xCC,
                                'Н' => 0xCD,
                                'О' => 0xCE,
                                'П' => 0xCF,
                                'Р' => 0xD0,
                                'С' => 0xD1,
                                'Т' => 0xD2,
                                'У' => 0xD3,
                                'Ф' => 0xD4,
                                'Х' => 0xD5,
                                'Ц' => 0xD6,
                                'Ч' => 0xD7,
                                'Ш' => 0xD8,
                                'Щ' => 0xD9,
                                'Ъ' => 0xDA,
                                'Ы' => 0xDB,
                                'Ь' => 0xDC,
                                'Э' => 0xDD,
                                'Ю' => 0xDE,
                                'Я' => 0xDF,
                                'а' => 0xE0,
                                'б' => 0xE1,
                                'в' => 0xE2,
                                'г' => 0xE3,
                                'д' => 0xE4,
                                'е' => 0xE5,
                                'ж' => 0xE6,
                                'з' => 0xE7,
                                'и' => 0xE8,
                                'й' => 0xE9,
                                'к' => 0xEA,
                                'л' => 0xEB,
                                'м' => 0xEC,
                                'н' => 0xED,
                                'о' => 0xEE,
                                'п' => 0xEF,
                                'р' => 0xF0,
                                'с' => 0xF1,
                                'т' => 0xF2,
                                'у' => 0xF3,
                                'ф' => 0xF4,
                                'х' => 0xF5,
                                'ц' => 0xF6,
                                'ч' => 0xF7,
                                'ш' => 0xF8,
                                'щ' => 0xF9,
                                'ъ' => 0xFA,
                                'ы' => 0xFB,
                                'ь' => 0xFC,
                                'э' => 0xFD,
                                'ю' => 0xFE,
                                'я' => 0xFF,
                                _ => 0x20,
                            };
                        }
                    }
                    if (copyLength < length)
                    {
                        for (int i = copyLength; i != length; ++i)
                        {
                            bytes[i] = 0;
                        }
                    }
                }
            }
            if (isLastNull)
            {
                bytes[length - 1] = 0;
            }
        }
        spreader.WriteBytes(bytes, 0, length);
    }

    /// <summary>
    /// Вызывает событие <see cref="FormatChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnFormatChanged(EventArgs e)
    {
        //  Вызов события.
        FormatChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="HeaderChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnHeaderChanged(EventArgs e)
    {
        //  Вызов события.
        HeaderChanged?.Invoke(this, e);
    }
}
