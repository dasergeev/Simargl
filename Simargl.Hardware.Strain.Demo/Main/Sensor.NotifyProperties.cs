using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Simargl.Hardware.Strain.Demo.Main;

partial class Sensor
{
    private DateTime _ReceivingTime;
    /// <summary/>
    public DateTime ReceivingTime
    {
        get => _ReceivingTime;
        set => SetProperty(ref _ReceivingTime, value);
    }

    private bool _IsRegistration;
    /// <summary/>
    public bool IsRegistration
    {
        get => _IsRegistration;
        set => SetProperty(ref _IsRegistration, value);
    }


    private string _IsRegistrationText = string.Empty;
    /// <summary/>
    public string IsRegistrationText
    {
        get => _IsRegistrationText;
        set => SetProperty(ref _IsRegistrationText, value);
    }

    private byte _SyncFlag;
    /// <summary/>
    public byte SyncFlag
    {
        get => _SyncFlag;
        set => SetProperty(ref _SyncFlag, value);
    }

    private ulong _TimeUnix;
    /// <summary/>
    [CLSCompliant(false)]
    public ulong TimeUnix
    {
        get => _TimeUnix;
        set => SetProperty(ref _TimeUnix, value);
    }

    private uint _TimeNano;
    /// <summary/>
    [CLSCompliant(false)]
    public uint TimeNano
    {
        get => _TimeNano;
        set => SetProperty(ref _TimeNano, value);
    }

    private float _CpuTemp;
    /// <summary/>
    public float CpuTemp
    {
        get => _CpuTemp;
        set => SetProperty(ref _CpuTemp, value);
    }

    private float _SensorTemp;
    /// <summary/>
    public float SensorTemp
    {
        get => _SensorTemp;
        set => SetProperty(ref _SensorTemp, value);
    }

    private float _CpuPower;
    /// <summary/>
    public float CpuPower
    {
        get => _CpuPower;
        set => SetProperty(ref _CpuPower, value);
    }

    private void BindProperties()
    {
        SetBind(nameof(IsConnected), updateSignals);
        SetBind(nameof(IsRegistration), delegate
        {
            IsRegistrationText = IsRegistration ? "Выполняется" : "Остановлена";
            updateSignals();
        });

        void updateSignals()
        {
            bool isConnected = IsConnected && IsRegistration;
            foreach (Signal signal in Signals)
            {
                signal.IsConnected = isConnected;
            }
        }
    }

    private void SetBind(string name, Action action)
    {
        PropertyChanged += delegate (object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == name)
            {
                action();
            }
        };

        action();
    }

    /// <summary>
    /// Устанавливает значение свойства.
    /// </summary>
    private void SetProperty<T>(ref T source, T value, [CallerMemberName] string callerName = null!)
    {
        if (!EqualityComparer<T>.Default.Equals(source, value))
        {
            source = value;
            Volatile.Read(ref PropertyChanged)?.Invoke(this, new(callerName));
        }
    }
}
