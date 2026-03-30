using Hbm.Api.Common;
using Hbm.Api.Common.Entities.Problems;
using Hbm.Api.Common.Entities.Signals;
using Hbm.Api.Common.Enums;
using Hbm.Api.QuantumX;
using RailTest.Satellite.Autonomic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет службу для работы с устройством QuantumX.
    /// </summary>
    public class QuantumXService : AutonomicService
    {
        /// <summary>
        /// Поле для харения состояния системы.
        /// </summary>
        readonly SystemState _SystemState;

        /// <summary>
        /// Поле для хранения состояния.
        /// </summary>
        QuantumXState _State;

        /// <summary>
        /// Поле для хранения измерительного буфера.
        /// </summary>
        MeasuringBuffer _MeasuringBuffer;

        /// <summary>
        /// Поле для хранения средства подключения к QuantumX.
        /// </summary>
        DaqEnvironment _DaqEnvironment;

        /// <summary>
        /// Поле для хранения устройства QuantumX.
        /// </summary>
        QuantumXDevice[] _QuantumXDevices;

        /// <summary>
        /// Основной объект для выполнения измерений
        /// </summary>
        DaqMeasurement _DaqMeasurement;

        /// <summary>
        /// Поле для хранения списка сигналов.
        /// </summary>
        List<Signal> _Signals = new List<Signal>();

        /// <summary>
        /// Поле для хранения времени получения последних данных.
        /// </summary>
        DateTime _LastTime;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public QuantumXService() :
            base(ServiceID.QuantumX)
        {
            _SystemState = new SystemState();
        }

        /// <summary>
        /// Выполняет основную работу.
        /// </summary>
        protected override void Invoke()
        {
            Thread.Sleep(30000);

            State = QuantumXState.NotPrepared;
            _MeasuringBuffer = new MeasuringBuffer();
            _MeasuringBuffer.Reset();
            _DaqEnvironment = null;

            string[] addresses = Settings.QuantumXIPAddresses;
            int countDevices = addresses.Length;
            _QuantumXDevices = new QuantumXDevice[countDevices];
            bool[] connected = new bool[countDevices];

            while (IsWork)
            {
                switch (State)
                {
                    case QuantumXState.NotPrepared:
                        if (_DaqEnvironment is null)
                        {
                            _DaqEnvironment = DaqEnvironment.GetInstance();
                            connected = new bool[countDevices];
                        }
                        try
                        {
                            for (int i = 0; i != countDevices; ++i)
                            {
                                _QuantumXDevices[i] = new QuantumXDevice(addresses[i]);
                            }
                            State = QuantumXState.NotConnected;
                            Logger.WriteLine("Подготовка работы: успешно.");
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine($"Подготовка работы: ошибка: \"{ex.Message}\".");
                            throw;
                        }
                        break;
                    case QuantumXState.NotConnected:
                        for (int i = 0; i != countDevices; ++i)
                        {
                            var device = _QuantumXDevices[i];
                            var address = addresses[i];
                            if (!connected[i])
                            {
                                try
                                {
                                    List<Problem> problems = new List<Problem>();
                                    bool result = _DaqEnvironment.Connect(device, out problems);
                                    if (!result || problems.Count != 0)
                                    {
                                        throw new InvalidOperationException($"Устройство не отвечает.");
                                    }

                                    Logger.WriteLine($"Подключение устройства {address}: успешно.");
                                    connected[i] = true;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        _DaqEnvironment.Disconnect(device);
                                    }
                                    catch
                                    {

                                    }
                                    Logger.WriteLine($"Подключение устройства {address}: ошибка: \"{ex.Message}\".");
                                }
                            }
                        }
                        {
                            bool result = true;
                            for (int i = 0; i != countDevices; ++i)
                            {
                                result = result && connected[i];
                            }
                            if (result)
                            {
                                State = QuantumXState.Connected;
                            }
                        }
                        break;
                    case QuantumXState.Connected:
                        try
                        {
                            _DaqMeasurement = new DaqMeasurement();
                            _Signals.Clear();
                            foreach (var device in _QuantumXDevices)
                            {
                                foreach (var connector in device.Connectors)
                                {
                                    var signal = connector.Channels[0].Signals[0];
                                    _Signals.Add(signal);

                                    List<Problem> problems = new List<Problem>();
                                    signal.SampleRate = Settings.Sampling;
                                    bool result = device.AssignSignal(signal, out problems);
                                    if (!result || problems.Count != 0)
                                    {
                                        throw new InvalidOperationException("Не удалось установить частоту дискретизации.");
                                    }
                                    _DaqMeasurement.AddSignals(device, signal);
                                }
                            }
                            _DaqMeasurement.PrepareDaq();
                            _DaqMeasurement.StartDaq(DataAcquisitionMode.TimestampSynchronized);
                            Logger.WriteLine($"Найдено {_Signals.Count} каналов.");
                            State = QuantumXState.Registration;
                            _LastTime = DateTime.Now;
                            Logger.WriteLine("Запуск регистрации: успешно.");
                        }
                        catch (Exception ex)
                        {
                            _Signals.Clear();
                            if (_DaqMeasurement is object)
                            {
                                try
                                {
                                    _DaqMeasurement.Dispose();
                                    _DaqMeasurement = null;
                                }
                                catch
                                {

                                }
                            }
                            Logger.WriteLine($"Запуск регистрации: ошибка: \"{ex.Message}\".");
                        }
                        break;
                    case QuantumXState.Registration:
                        try
                        {
                            _DaqMeasurement.FillMeasurementValues();
                            int count = _Signals[0].ContinuousMeasurementValues.UpdatedValueCount;

                            bool flag = true;
                            StringBuilder builder = new StringBuilder();
                            for (int i = 0; i < Settings.CountSignals; i++)
                            {
                                builder.Append($" {_Signals[i].ContinuousMeasurementValues.UpdatedValueCount}");
                                flag = flag && (count == _Signals[i].ContinuousMeasurementValues.UpdatedValueCount);
                            }
                            if (flag)
                            {
                                builder.Append(" ok");
                            }
                            else
                            {
                                builder.Append(" error");
                                throw new InvalidOperationException("Ошибка данных.");
                            }
                            Logger.WriteLine(builder.ToString());

                            if (count != 0)
                            {
                                double[,] measurements = new double[_Signals.Count, count];
                                for (int i = 0; i != _Signals.Count; ++i)
                                {
                                    var values = _Signals[i].ContinuousMeasurementValues.Values;
                                    for (int j = 0; j < count; j++)
                                    {
                                        double value = values[j];
                                        if (value == _DaqEnvironment.Overflow)
                                        {
                                            value = 0;
                                        }
                                        measurements[i, j] = value;
                                    }
                                }
                                _MeasuringBuffer.Write(measurements, count);
                                _LastTime = DateTime.Now;
                            }
                            if ((DateTime.Now - _LastTime).TotalMilliseconds > Settings.QuantumXTimeout)
                            {
                                throw new InvalidOperationException("Истекло время ожидания.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Reset();
                            State = QuantumXState.NotPrepared;
                            Logger.WriteLine($"Произошла ошибка при получении данных: \"{ex.Message}\".");
                        }
                        break;
                    default:
                        State = QuantumXState.NotPrepared;
                        break;
                }
                Thread.Sleep(Settings.QuantumXSurveyPeriod);
            }
            Reset();
        }

        /// <summary>
        /// Поле для хранения состояния.
        /// </summary>
        QuantumXState State
        {
            get => _State;
            set
            {
                _State = value;
                _SystemState.QuantumXState = value;
            }
        }

        /// <summary>
        /// Выполняет сброс настроек QuantumX.
        /// </summary>
        void Reset()
        {
            try
            {
                if (_DaqMeasurement is object)
                {
                    try
                    {
                        _DaqMeasurement.StopDaq();
                    }
                    catch
                    {

                    }
                    try
                    {
                        _DaqMeasurement.Dispose();
                        _DaqMeasurement = null;
                    }
                    catch
                    {

                    }
                }
                if (_QuantumXDevices is object)
                {
                    for (int i = 0; i != _QuantumXDevices.Length; ++i)
                    {
                        try
                        {
                            _DaqEnvironment.Disconnect(_QuantumXDevices[i]);
                        }
                        catch
                        {

                        }
                        _QuantumXDevices[i] = null;
                    }
                }
                if (_DaqEnvironment is object)
                {
                    try
                    {
                        _DaqEnvironment.Dispose();
                    }
                    catch
                    {

                    }
                    _DaqEnvironment = null;
                }
            }
            catch
            {

            }
            _MeasuringBuffer.Reset();
        }
    }
}
