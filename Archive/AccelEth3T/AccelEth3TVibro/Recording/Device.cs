using Simargl.Recording.AccelEth3T;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет устройство.
/// </summary>
/// <param name="core">
/// Ядро.
/// </param>
/// <param name="number">
/// Номер устройства.
/// </param>
/// <param name="name">
/// Имя устройства.
/// </param>
public sealed class Device(Core core, int number, string name) :
    Worker(core)
{
    /// <summary>
    /// Поле для хранения очереди пакетов.
    /// </summary>
    private readonly ConcurrentQueue<AccelEth3TDataPackage> _Packages = [];

    /// <summary>
    /// Возвращает номер устройства.
    /// </summary>
    public int Number { get; } = number;

    /// <summary>
    /// Возвращает имя устройства.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Возвращает значение, определяющее, активен ли датчик.
    /// </summary>
    public bool Active { get; private set; } = false;

    /// <summary>
    /// Возвращает коллекцию сигналов.
    /// </summary>
    public AccelEth3TSignalCollection Signals { get; } = [];

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Блок с гарантированным завершением.
        try
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Время последнего получения данных.
            DateTime lastTime = DateTime.Now;

            //  Длина пакета.
            const int length = 50;

            //  Создание массивов данных.
            double[] x = new double[length];
            double[] y = new double[length];
            double[] z = new double[length];
            double[] yz = new double[length];
            double[] xz = new double[length];
            double[] xy = new double[length];
            double[] _3d = new double[length];

            //  Основной цикл работы.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Разбор очереди пакетов.
                while (_Packages.TryDequeue(out AccelEth3TDataPackage? package))
                {
                    //  Обновление времени последнего получения данных.
                    lastTime = DateTime.Now;

                    //  Получение данных.
                    package.Signals.XSignal.GetData(x, 9.81);
                    package.Signals.YSignal.GetData(y, 9.81);
                    package.Signals.ZSignal.GetData(z, 9.81);

                    //  Перебор значений в пакете.
                    for (int i = 0; i < length; i++)
                    {
                        //  Получение данных.
                        double xValue = x[i];
                        double yValue = y[i];
                        double zValue = z[i];

                        //  Расчёт квадратов.
                        double x_2 = xValue * xValue;
                        double y_2 = yValue * yValue;
                        double z_2 = zValue * zValue;

                        //  Расчёт дополнительных значений.
                        yz[i] = Math.Sqrt(y_2 + z_2);
                        xz[i] = Math.Sqrt(x_2 + z_2);
                        xy[i] = Math.Sqrt(x_2 + y_2);
                        _3d[i] = Math.Sqrt(x_2 + y_2 + z_2);
                    }

                    //  Установка значений в сигналы.
                    Signals[0].AddData(x);
                    Signals[1].AddData(y);
                    Signals[2].AddData(z);
                    Signals[3].AddData(yz);
                    Signals[4].AddData(xz);
                    Signals[5].AddData(xy);
                    Signals[6].AddData(_3d);
                }

                //  Обновление значения, определяющего, активен ли датчик.
                Active = (DateTime.Now - lastTime).TotalMilliseconds < 500;

                //  Ожидание перед следующим проходом.
                await Task.Delay(100, cancellationToken).ConfigureAwait(false);
            }
        }
        finally
        {
            //  Сброс значения, определяющего, активен ли датчик.
            Active = false;
        }
    }

    /// <summary>
    /// Добавляет новый пакет данных.
    /// </summary>
    /// <param name="package">
    /// Новый пакет данных.
    /// </param>
    public void AddPackage(AccelEth3TDataPackage package)
    {
        //  Добавление пакета в очередь.
        _Packages.Enqueue(package);
    }
}
