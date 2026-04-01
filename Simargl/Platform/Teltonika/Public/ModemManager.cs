using System.ComponentModel;
using System.Runtime.CompilerServices;
using Simargl.Designing.Utilities;

namespace Simargl.Platform.Teltonika;

/// <summary>
/// Представляет класс управления модемом и получения данных модема.
/// </summary>
public sealed class ModemManager: INotifyPropertyChanged,IDisposable
{
    /// <summary>
    /// Представляет объект синхронизации класса.
    /// </summary>
    private readonly object _SyncRoot = new();

    /// <summary>
    /// Представляет флаг выполнения освобождения ресурсов.
    /// </summary>
    private bool _Disposed;

    /// <summary>
    /// Представляет поле данных модема.
    /// </summary>
    private ModemInfo? _Info;

    /// <summary>
    /// Представляет ссылку на задачу получения Nmea.
    /// </summary>
    private Task? _ModemInfoTask;

    /// <summary>
    /// Константа минимального значения свойства <see cref="UpdateTime"/>"/>
    /// </summary>
    private const int _ConstMimimumUpdateTimeMs = 30000;

    /// <summary>
    /// Поле хранящее последние данные геолокации.
    /// </summary>
    private volatile int _UpdateTime = _ConstMimimumUpdateTimeMs;

    /// <summary>
    /// Источник токена отмены задач объекта.
    /// </summary>
    private CancellationTokenSource? _CancellationTokenSource;

    /// <summary>
    /// Константа переключения сим карты.
    /// </summary>
    public const byte ConstChangeSim = 0x01;

    /// <summary>
    /// Константа перезагрузки модема.
    /// </summary>
    public const byte ConstResetModem = 0x02;

    /// <summary>
    /// Константа перезагрузки Teltonika.
    /// </summary>
    public const byte ConstResetTeltonika = 0x04;

    /// <summary>
    /// Представляет событие изменеиния свойства данных геолокации.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле хранящее интерфейса управления процессами.
    /// </summary>
    private readonly ProcessManager _ProcessManager;

    /// <summary>
    /// Поле хранящее драйвер интерфейса Modbus.
    /// </summary>
    private readonly ModbusManager _ModbusManager;

    /// <summary>
    /// Возвращает низкий уровень модема.
    /// </summary>
    internal ModemLowLevel LowLevel { get; private set; }

    /// <summary>
    /// Возвращает последнии данные геолокации
    /// </summary>
    public ModemInfo? Info
    {
        get => _Info;
        private set
        {
            _Info = value;
            OnNotifyPropertyChanged();
        }
    }

    /// <summary>
    /// Возвращает время получения данных геолокации.
    /// </summary>
    public DateTime ModemInfoLastReceiveTime { get; private set; } = DateTime.Now;

    /// <summary>
    /// Возвращает флаг состояния запуска.
    /// </summary>
    public bool Started { get; private set; } = false;

    /// <summary>
    /// Возвращает и устанавливает значение времени обновления свойства <see cref="GeolocationManager.Geolocation"/>
    /// </summary>
    /// <remarks>Минимальное значение 300000 мс.</remarks>
    public int UpdateTime
    {
        //  Возврат значения.
        get => _UpdateTime;
        set
        {
            //  Проверка на минимальное значение.
            if (value < _ConstMimimumUpdateTimeMs)
            {
                //  Установка значения.
                _UpdateTime = _ConstMimimumUpdateTimeMs;
            }
            else
            {
                //  Установка значения.
                _UpdateTime = value;
            }
        }
    }

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="modemLowLevel">
    /// Интерфейс низкого уровня модема.
    /// </param>
    /// <param name="modbusManager">
    /// Драйвер интерфейса Modbus.
    /// </param>
    /// <param name="processManager">
    /// Интерфейс управления процессами.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="modbusManager"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="modemLowLevel"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="processManager"/> передана пустая ссылка.
    /// </exception>
    internal ModemManager(ModbusManager modbusManager, ModemLowLevel modemLowLevel, ProcessManager processManager)
    {
        //  Установка интерфейса Modbus
        _ModbusManager = IsNotNull(modbusManager, nameof(modbusManager));

        //  Установка интерфейса низкого уровня.
        LowLevel = IsNotNull(modemLowLevel,nameof(modemLowLevel));

        //  Установка интерфейса Process
        _ProcessManager = IsNotNull(processManager, nameof(processManager));
    }

    /// <summary>
    /// Представляет метод освобождения ресурсов.
    /// </summary>
    public void Dispose()
    {
        //  Выполнения завершения операций.
        Dispose(disposing: true);

        //  Подавления выполнения диструктора.
        GC.SuppressFinalize(this);
    }


    /// <summary>
    /// Представляет метод освобождения ресурсов (виртуальный)
    /// </summary>
    /// <param name="disposing">
    /// Флаг завершения внутренных ресурсов.
    /// </param>
    private void Dispose(bool disposing)
    {
        //   Проверка выполнения операции.
        if (_Disposed)
        {
            return;
        }

        //  Проверка флага
        if (disposing)
        {
            //   Остановка работы класса, и освобождение ресурсов.
            Stop();
        }

        //  Установка флага выполнения операции.
        _Disposed = true;
    }


    /// <summary>
    /// Представляет функцию запуса сервисов объекта.
    /// </summary>
    public void Start()
    {
        lock (_SyncRoot)
        {
            //  Проверка флага
            if (Started == false)
            {
                //  Создание источника токена
                _CancellationTokenSource = new CancellationTokenSource();

                //  Запуск задачи
                _ModemInfoTask = Task.Run(async () => await UpdateModemInfoRequestedAsync(_CancellationTokenSource.Token).ConfigureAwait(false));

                //  Установка флага
                Started = true;
            }
        }
        
    }

    /// <summary>
    /// Представляет функцию остановки сервисов объекта.
    /// </summary>
    public void Stop()
    {
        lock (_SyncRoot)
        {
            //  Проврека флага
            if (Started == true)
            {
                //  Отмена токена
                _CancellationTokenSource?.Cancel();

                //  Ожидание завершения задачи
                _ModemInfoTask?.Wait();

                //  Освобождение источника токена
                _CancellationTokenSource?.Dispose();

                //  Установка флага.
                Started = false;
            }
        }
    }

    /// <summary>
    /// Представляет функцию восстановления работы сервиса.
    /// </summary>
    /// <param name="options">
    /// Опции.
    /// </param>
    public void Reset(byte options)
    {
        lock (_SyncRoot)
        {
            //  Проверка опции переключения сим карты.
            if ((options & ConstChangeSim) != 0)
            {
                //_ModbusManager.ChangeActiveSimAsync(default).Wait();
            }

            //  Проверка опции перезагрузки модема.
            if ((options & ConstResetModem) != 0)
            {
                //  Перезагрузка модема.
                ResetModem();
            }

            //  Проверка опции перезагрузки Teltonika.
            if ((options & ConstResetTeltonika) != 0)
            {
                //  Перезагрузка Teltonika.
                _ProcessManager.RebootDevice();
            }
        }
    }


    /// <summary>
    /// Представляет функцию перезагрузки модема.
    /// </summary>
    /// <returns>
    /// Результат выполнения.
    /// </returns>
    internal bool ResetModem()
    {
        //  Попытка программной перезагрузки.
        var result = LowLevel.SoftResturtModem();

        //  Проверка результата
        if (result)
        {
            //  Возврат результата.
            return true;
        }

        //  Попытка аппаратной перезагрузки
        result =  LowLevel.HardResturtModem(); 

        //  Возврат результата.
        return result;
    }

    /// <summary>
    /// Выполняет получения данных модема и связи.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача.
    /// </returns>
    private async Task UpdateModemInfoRequestedAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Ожидaние.
            await Task.Delay(_UpdateTime, cancellationToken).ConfigureAwait(false);
            try
            {
                //  Получение данных от процесса.
                var info = LowLevel.GetModemInfo();

                //  Проверка результата
                if (info != null)
                {
                    //  Установка нового значения.
                    Info = info;
                    continue;
                }
            }
            catch (Exception ex)
            {
                //  Проверка типа исключение
                if (ex.IsSystem())
                {
                    //  Перенаправление исключения.
                    throw;
                }
            }

            try
            {
                //  Обновление данных
                await _ModbusManager.UpdateAsync(cancellationToken).ConfigureAwait(false);

                //  Сохранение полученных данных.
                Info = new ModemInfo(
                    string.Empty,
                    string.Empty,
                    _ModbusManager.IMSI,
                    _ModbusManager.MobileSignalStrength);

                //  Продолжение цикла.
                continue;
            }
            catch (Exception ex)
            {
                //  Проверка типа исключение
                if (ex.IsSystem())
                {
                    //  Перенаправление исключения.
                    throw;
                }

            }

            ModemInfoLastReceiveTime = DateTime.Now;
        }
    }

    /// <summary>
    /// Представляет функцию вызова события <see cref="GeolocationManager.PropertyChanged"/>.
    /// </summary>
    private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        //Вызов события.
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
