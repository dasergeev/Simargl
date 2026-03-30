using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Primitives;
using System.Windows.Forms;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Plotters;

/// <summary>
/// Представляет метрики рабочего пространства рендеринга графиков.
/// </summary>
public sealed class PlotterWorkspaceMetrics
{
    /// <summary>
    /// Постоянная, определяющая минимальный шаг сетки.
    /// </summary>
    private const int _GridStepMin = 64;

    /// <summary>
    /// Поле для хранения рабочего пространства.
    /// </summary>
    private readonly PlotterWorkspace _Workspace;

    /// <summary>
    /// Поле для хранения значения, определяющего заблокирован ли объект.
    /// </summary>
    private volatile int _IsLock;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="workspace">
    /// Рабочее пространство.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="workspace"/> передана пустая ссылка.
    /// </exception>
    public PlotterWorkspaceMetrics(PlotterWorkspace workspace)
    {
        //  Установка рабочего пространства.
        _Workspace = IsNotNull(workspace, nameof(workspace));

        //  Создание значения, определяющего заблокирован ли объект.
        _IsLock = 0;

        //  Установка значений по умолчанию.
        Width = 640;
        Height = 480;
        XMin = 0;
        XMax = 60;
        YMin = -1;
        YMax = 1;

        //  Обновление коэффициентов.
        UpdateFactors();
    }

    /// <summary>
    /// Возвращает отступы рабочего пространства.
    /// </summary>
    public Padding Padding => _Workspace.Padding;

    /// <summary>
    /// Возвращает значение, определяющее, является ли область действительной для отображения.
    /// </summary>
    public bool IsValid { get; private set; }

    /// <summary>
    /// Возвращает ширину окна.
    /// </summary>
    public double Width { get; private set; }

    /// <summary>
    /// Возвращает высоту окна.
    /// </summary>
    public double Height { get; private set; }

    /// <summary>
    /// Возвращает минимальное значение по оси Ox.
    /// </summary>
    public double XMin { get; private set; }

    /// <summary>
    /// Возвращает максимальное значение по оси Ox.
    /// </summary>
    public double XMax { get; private set; }

    /// <summary>
    /// Возвращает минимальное значение по оси Oy.
    /// </summary>
    public double YMin { get; private set; }

    /// <summary>
    /// Возвращает максимальное значение по оси Oy.
    /// </summary>
    public double YMax { get; private set; }

    /// <summary>
    /// Возвращает масштаб по оси Ox.
    /// </summary>
    public double XScale { get; private set; }

    /// <summary>
    /// Возвращает масштаб по оси Oy.
    /// </summary>
    public double YScale { get; private set; }

    /// <summary>
    /// Возвращает смещение по оси Ox.
    /// </summary>
    public double XOffset { get; private set; }

    /// <summary>
    /// Возвращает смещение по оси Oy.
    /// </summary>
    public double YOffset { get; private set; }

    /// <summary>
    /// Возвращает шаг сетки по оси Ox.
    /// </summary>
    public double XGridStep { get; private set; }

    /// <summary>
    /// Возвращает шаг сетки по оси Oy.
    /// </summary>
    public double YGridStep { get; private set; }

    /// <summary>
    /// Возвращает начальное положение сетки по оси Ox.
    /// </summary>
    public double XGridBegin { get; private set; }

    /// <summary>
    /// Возвращает начальное положение сетки по оси Ox.
    /// </summary>
    public double YGridBegin { get; private set; }

    /// <summary>
    /// Установка размеров окна.
    /// </summary>
    /// <param name="width">
    /// Ширина.
    /// </param>
    /// <param name="height">
    /// Высота.
    /// </param>
    public void SetSize(double width, double height)
    {
        //  Вполнение с блокировкой.
        Invoke(delegate
        {
            //  Установка свойств.
            Width = width;
            Height = height;

            //  Обновление коэффициентов.
            UpdateFactors();
        });
    }

    /// <summary>
    /// Установка диапазано значений по оси Ox.
    /// </summary>
    /// <param name="xMin">
    /// Минимальное значение по оси Ox.
    /// </param>
    /// <param name="xMax">
    /// Максимальное значение по оси Ox.
    /// </param>
    public void SetXRange(double xMin, double xMax)
    {
        //  Вполнение с блокировкой.
        Invoke(delegate
        {
            //  Установка свойств.
            XMin = xMin;
            XMax = xMax;

            //  Обновление коэффициентов.
            UpdateFactors();
        });
    }

    /// <summary>
    /// Установка диапазано значений по оси Oy.
    /// </summary>
    /// <param name="yMin">
    /// Минимальное значение по оси Oy.
    /// </param>
    /// <param name="yMax">
    /// Максимальное значение по оси Oy.
    /// </param>
    public void SetYRange(double yMin, double yMax)
    {
        //  Вполнение с блокировкой.
        Invoke(delegate
        {
            //  Установка свойств.
            YMin = yMin;
            YMax = yMax;

            //  Обновление коэффициентов.
            UpdateFactors();
        });
    }

    /// <summary>
    /// Устанавливает диапазон значений по оси Oy.
    /// </summary>
    /// <param name="polylines">
    /// Коллекция ломаных линий.
    /// </param>
    public void SetYRange(PolylineCollection? polylines)
    {
        //  Значения диапазона.
        double yMin = -1;
        double yMax = 1;

        //  Проверка ссылки на коллекцию ломаных линий.
        if (polylines is not null && !polylines.IsEmpty)
        {
            //  Расчёт значений диапазона.
            yMin = 1.025 * polylines.YMin - 0.025 * polylines.YMax;
            yMax = 1.025 * polylines.YMax - 0.025 * polylines.YMin;
        }

        //  Определение амплитуды.
        double yAmplitude = 0.5 * (yMax - yMin);

        //  Проверка амплитуды.
        if (yAmplitude < 0.001)
        {
            //  Корректировка амплитуды.
            yAmplitude = 0.001;

            //  Определение центрального значения.
            double yCenter = 0.5 * (yMax + yMin);

            //  Расчёт значений диапазона.
            yMin = yCenter - yAmplitude;
            yMax = yCenter + yAmplitude;
        }

        //  Установка значений.
        SetYRange(yMin, yMax);
    }

    /// <summary>
    /// Возвращает копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public PlotterWorkspaceMetrics Clone()
    {
        //  Создание объекта.
        PlotterWorkspaceMetrics metrics = new(_Workspace);

        //  Вполнение с блокировкой.
        Invoke(delegate
        {
            //  Установка свойств.
            metrics.IsValid = IsValid;
            metrics.Width = Width;
            metrics.Height = Height;
            metrics.XMin = XMin;
            metrics.XMax = XMax;
            metrics.YMin = YMin;
            metrics.YMax = YMax;
            metrics.XScale = XScale;
            metrics.YScale = YScale;
            metrics.XOffset = XOffset;
            metrics.YOffset = YOffset;
            metrics.XGridStep = XGridStep;
            metrics.YGridStep = YGridStep;
            metrics.XGridBegin = XGridBegin;
            metrics.YGridBegin = YGridBegin;
        });

        //  Возврат копии объекта.
        return metrics;
    }

    /// <summary>
    /// Обновляет коэффициенты.
    /// </summary>
    private void UpdateFactors()
    {
        //  Расчёт предварительных значений.
        double dWidth = Width - _Workspace.Padding.Left - _Workspace.Padding.Right;
        double dHeight = Height - _Workspace.Padding.Top - _Workspace.Padding.Bottom;
        double xRange = XMax - XMin;
        double yRange = YMin - YMax;

        //  Определение значения, определяющего, является ли область действительной для отображения.
        IsValid = (dWidth > 0) && (dHeight > 0) && (xRange > 0) && (yRange < 0);

        //  Проверка полученного значения.
        if (IsValid)
        {
            //  Расчёт масштабов и смещений.
            XScale = dWidth / xRange;
            YScale = dHeight / yRange;
            XOffset = _Workspace.Padding.Left - XScale * XMin;
            YOffset = _Workspace.Padding.Top - YScale * YMax;

            //  Расчёт параметров сетки.
            XGridStep = normalizeStep(2 * _GridStepMin / XScale);
            XGridBegin = Math.Ceiling(XMin / XGridStep) * XGridStep;
            YGridStep = normalizeStep(-2 * _GridStepMin / YScale);
            YGridBegin = Math.Ceiling(YMin / YGridStep) * YGridStep;
        }

        //  Выполняет нормализацию шага сетки.
        static double normalizeStep(double value)
        {
            //  Определение показателя степени.
            double power = Math.Floor(Math.Log10(value));

            //  Расчёт амплитуды.
            double amplitude = value / Math.Pow(10, power);

            //  Нормализация амплитуды.
            if (amplitude < 2)
            {
                amplitude = 1;
            }
            else if (amplitude < 2.5)
            {
                amplitude = 2;
            }
            else if (amplitude < 5)
            {
                amplitude = 2.5;
            }
            else
            {
                amplitude = 5;
            }

            //  Возврат шага сетки.
            return amplitude * Math.Pow(10, power);
        }
    }

    /// <summary>
    /// Выполняет действие с блокировкой.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить с блокировкой.
    /// </param>
    private void Invoke([ParameterNoChecks] Action action)
    {
        //  Создание объекта, обеспечивающего поддержку ожидания.
        SpinWait spinWait = new();

        //  Ожидание блокировки.
        while (Interlocked.CompareExchange(ref _IsLock, 1, 0) != 0)
        {
            //  Выполнение одной прокрутки.
            spinWait.SpinOnce();
        }

        //  Блок с гарантированным завершением.
        try
        {
            //  Выполнение действия.
            action();
        }
        finally
        {
            //  Снятие блокировки.
            _IsLock = 0;
        }
    }
}
