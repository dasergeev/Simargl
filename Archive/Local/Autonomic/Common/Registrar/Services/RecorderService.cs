using RailTest.Algebra;
using RailTest.Frames;
using RailTest.Satellite.Autonomic.Services;
using System;
using System.Threading;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет службу записи файлов.
    /// </summary>
    public class RecorderService : AutonomicService
    {
        /// <summary>
        /// Поле для хранения нулей.
        /// </summary>
        double[] _Zeros;

        /// <summary>
        /// Поле для хранения номера кадра.
        /// </summary>
        int _FrameNumber = 1;

        /// <summary>
        /// Поле для хранения буфера измерений.
        /// </summary>
        MeasuringBuffer _MeasuringBuffer = new MeasuringBuffer();

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public RecorderService() :
            base(ServiceID.Recorder)
        {

        }

        /// <summary>
        /// Выполняет основную работу.
        /// </summary>
        protected override void Invoke()
        {
            _MeasuringBuffer = new MeasuringBuffer();
            _Zeros = new double[Settings.CountSignals];
            const int length = Settings.Sampling * Settings.Duration;
            RecorderState state = RecorderState.Waiting;
            LocationBuffer locationBuffer = new LocationBuffer();
            DateTime startTime = _MeasuringBuffer.StartTime;
            long lastPosition = _MeasuringBuffer.Position;
            while (IsWork)
            {
                long position = _MeasuringBuffer.Position;
                if (startTime == _MeasuringBuffer.StartTime)
                {
                    switch (state)
                    {
                        case RecorderState.Waiting:
                            if (position < length && lastPosition > length)
                            {
                                state = RecorderState.First;
                            }
                            if (position >= length && lastPosition < length)
                            {
                                state = RecorderState.Second;
                            }
                            break;
                        case RecorderState.First:
                            if (position > length)
                            {
                                double[,] channels = _MeasuringBuffer.Read(0, length);
                                double[,] gps = locationBuffer.ReadLast(Settings.Duration);
                                Save(channels, gps);
                                state = RecorderState.Second;
                            }
                            break;
                        case RecorderState.Second:
                            if (position < length)
                            {
                                double[,] channels = _MeasuringBuffer.Read(length, length);
                                double[,] gps = locationBuffer.ReadLast(Settings.Duration);
                                Save(channels, gps);
                                state = RecorderState.First;
                            }
                            break;
                        default:
                            state = RecorderState.Waiting;
                            break;
                    }
                }
                else
                {
                    startTime = _MeasuringBuffer.StartTime;
                    state = RecorderState.Waiting;
                    position = _MeasuringBuffer.Position;
                }
                lastPosition = position;
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Сохраняет данные в файл.
        /// </summary>
        /// <param name="channels">
        /// Данные каналов.
        /// </param>
        /// <param name="gps">
        /// Данные GPS.
        /// </param>
        void Save(double[,] channels, double[,] gps)
        {
            try
            {
                var time = DateTime.Now;
                var header = new TestLabFrameHeader
                {
                    Time = time
                };
                Frame frame = new Frame(header);
                for (int i = 0; i != Settings.CountSignals; ++i)
                {
                    int length = Settings.Duration * Settings.Sampling;
                    var channelHeader = new TestLabChannelHeader
                    {
                        Name = Settings.ChannelNames[i],
                        Unit = Settings.ChannelUnits[i],
                        Sampling = Settings.Sampling,
                        Cutoff = Settings.Sampling >> 1
                    };

                    Channel channel = new Channel(channelHeader, new RealVector(length));
                    for (int j = 0; j != length; ++j)
                    {
                        channel[j] = channels[i, j];
                    }
                    frame.Channels.Add(channel);
                }
                for (int i = 0; i != 3; ++i)
                {
                    int length = Settings.Duration;
                    Channel channel = new Channel(string.Empty, string.Empty, 1, 1, length);
                    switch (i)
                    {
                        case 0:
                            channel.Name = "V_GPS";
                            channel.Unit = "kmph";
                            break;
                        case 1:
                            channel.Name = "Lon_GPS";
                            channel.Unit = "°";
                            break;
                        case 2:
                            channel.Name = "Lat_GPS";
                            channel.Unit = "°";
                            break;
                    }
                    for (int j = 0; j != length; ++j)
                    {
                        channel[j] = gps[i, j];
                    }
                    frame.Channels.Add(channel);
                }
                double speed = frame.Channels["V_GPS"].Average;
                if (Math.Abs(speed) < 1)
                {
                    for (int i = 0; i != Settings.CountSignals; ++i)
                    {
                        _Zeros[i] = frame.Channels[i].Average;
                        _MeasuringBuffer.SetZero(i, _Zeros[i]);
                    }
                }
                for (int i = 0; i != Settings.CountSignals; ++i)
                {
                    ((TestLabChannelHeader)frame.Channels[i].Header).Offset = _Zeros[i];
                    frame.Channels[i].Move(-_Zeros[i]);
                }

                string fileName = $"Vp{speed:000.0} ".Replace('.', '_').Replace(',', '_') + time.ToString().Replace(':', '_') + $".{_FrameNumber:00000000}";
                frame.Save(Settings.RegistrarTemporal + fileName, StorageFormat.TestLab);
                Logger.WriteLine(fileName);
                ++_FrameNumber;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.Message);
            }
        }
    }
}
