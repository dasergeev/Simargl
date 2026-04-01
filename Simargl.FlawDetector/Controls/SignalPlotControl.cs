using Simargl.FlawDetector.Models; // Подключает типы временных рядов для отрисовки.

namespace Simargl.FlawDetector.Controls; // Определяет пространство имён пользовательских контролов.

/// <summary> // Описывает назначение класса.
/// Отрисовывает временной ряд вибросигнала по одной оси акселерометра. // Уточняет назначение пользовательского контрола.
/// </summary> // Завершает XML-документацию класса.
internal sealed class SignalPlotControl : FrameworkElement // Объявляет пользовательский контрол графика.
{ // Начинает тело класса.
    private const double LeftPadding = 72d; // Задаёт левый отступ под подписи вертикальной шкалы.
    private const double TopPadding = 14d; // Задаёт верхний отступ области графика.
    private const double RightPadding = 18d; // Задаёт правый отступ области графика.
    private const double BottomPadding = 24d; // Задаёт нижний отступ под подпись времени.

    /// <summary> // Документирует свойство зависимости с данными графика.
    /// Идентификатор свойства зависимости, содержащего временной ряд для отрисовки. // Уточняет назначение свойства зависимости.
    /// </summary> // Завершает XML-документацию поля.
    public static readonly DependencyProperty SeriesProperty = DependencyProperty.Register( // Регистрирует свойство зависимости с серией сигнала.
        nameof(Series), // Указывает имя свойства зависимости.
        typeof(AxisSignalSeries), // Указывает тип значения свойства.
        typeof(SignalPlotControl), // Указывает тип владельца свойства.
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)); // Настраивает автоматическую перерисовку при изменении данных.

    /// <summary> // Документирует свойство зависимости цвета переднего плана.
    /// Идентификатор свойства зависимости, задающего основной тематический цвет графика. // Уточняет назначение свойства зависимости.
    /// </summary> // Завершает XML-документацию поля.
    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register( // Регистрирует свойство зависимости цвета переднего плана.
        nameof(Foreground), // Указывает имя свойства зависимости.
        typeof(Brush), // Указывает тип значения свойства.
        typeof(SignalPlotControl), // Указывает тип владельца свойства.
        new FrameworkPropertyMetadata(SystemColors.ControlTextBrush, FrameworkPropertyMetadataOptions.AffectsRender)); // Настраивает цвет переднего плана по умолчанию.

    /// <summary> // Документирует свойство зависимости цвета фона.
    /// Идентификатор свойства зависимости, задающего фон области графика. // Уточняет назначение свойства зависимости.
    /// </summary> // Завершает XML-документацию поля.
    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register( // Регистрирует свойство зависимости цвета фона.
        nameof(Background), // Указывает имя свойства зависимости.
        typeof(Brush), // Указывает тип значения свойства.
        typeof(SignalPlotControl), // Указывает тип владельца свойства.
        new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender)); // Настраивает прозрачный фон по умолчанию.

    /// <summary> // Документирует свойство зависимости цвета рамки.
    /// Идентификатор свойства зависимости, задающего цвет рамки графика. // Уточняет назначение свойства зависимости.
    /// </summary> // Завершает XML-документацию поля.
    public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register( // Регистрирует свойство зависимости цвета рамки.
        nameof(BorderBrush), // Указывает имя свойства зависимости.
        typeof(Brush), // Указывает тип значения свойства.
        typeof(SignalPlotControl), // Указывает тип владельца свойства.
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)); // Настраивает отсутствие явной рамки по умолчанию.

    /// <summary> // Документирует свойство данных графика.
    /// Получает или задаёт временной ряд для отрисовки. // Уточняет назначение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public AxisSignalSeries? Series // Объявляет свойство серии сигнала.
    { // Начинает тело свойства серии.
        get => (AxisSignalSeries?)GetValue(SeriesProperty); // Возвращает текущую серию.
        set => SetValue(SeriesProperty, value); // Обновляет текущую серию.
    } // Завершает тело свойства серии.

    /// <summary> // Документирует цвет переднего плана.
    /// Получает или задаёт основной цвет графика, используемый для сетки и подписей. // Уточняет назначение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public Brush Foreground // Объявляет свойство цвета переднего плана.
    { // Начинает тело свойства цвета переднего плана.
        get => (Brush)GetValue(ForegroundProperty); // Возвращает текущую кисть переднего плана.
        set => SetValue(ForegroundProperty, value); // Обновляет текущую кисть переднего плана.
    } // Завершает тело свойства цвета переднего плана.

    /// <summary> // Документирует цвет фона.
    /// Получает или задаёт фон области графика. // Уточняет назначение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public Brush Background // Объявляет свойство цвета фона.
    { // Начинает тело свойства цвета фона.
        get => (Brush)GetValue(BackgroundProperty); // Возвращает текущую кисть фона.
        set => SetValue(BackgroundProperty, value); // Обновляет текущую кисть фона.
    } // Завершает тело свойства цвета фона.

    /// <summary> // Документирует цвет рамки.
    /// Получает или задаёт цвет рамки графика. // Уточняет назначение свойства.
    /// </summary> // Завершает XML-документацию свойства.
    public Brush? BorderBrush // Объявляет свойство цвета рамки.
    { // Начинает тело свойства цвета рамки.
        get => (Brush?)GetValue(BorderBrushProperty); // Возвращает текущую кисть рамки.
        set => SetValue(BorderBrushProperty, value); // Обновляет текущую кисть рамки.
    } // Завершает тело свойства цвета рамки.

    /// <summary> // Документирует метод отрисовки.
    /// Выполняет отрисовку осей, сетки, подписей и кривой сигнала. // Уточняет назначение переопределения.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="drawingContext">Контекст рисования WPF.</param> // Документирует контекст рисования.
    protected override void OnRender(DrawingContext drawingContext) // Объявляет переопределение отрисовки.
    { // Начинает тело метода отрисовки.
        base.OnRender(drawingContext); // Вызывает базовую реализацию отрисовки.
        Rect controlBounds = new(0d, 0d, ActualWidth, ActualHeight); // Определяет внешние границы контрола.
        Brush backgroundBrush = ResolveBackgroundBrush(); // Получает тематическую кисть фона графика.
        Pen borderPen = new(ResolveBorderBrush(), 1d); // Создаёт перо тематической рамки графика.
        borderPen.Freeze(); // Фиксирует перо рамки для повышения производительности.
        drawingContext.DrawRectangle(backgroundBrush, borderPen, controlBounds); // Рисует фон и внешнюю рамку графика с учётом темы.

        if (Series is null || Series.Points.Count < 2 || controlBounds.Width <= 0d || controlBounds.Height <= 0d) // Проверяет наличие достаточного числа точек для отрисовки.
        { // Начинает ветку отсутствия данных.
            DrawPlaceholder(drawingContext, controlBounds); // Рисует заглушку отсутствующих данных.
            return; // Завершает метод при отсутствии данных.
        } // Завершает ветку отсутствия данных.

        Rect plotArea = CreatePlotArea(controlBounds); // Формирует внутреннюю область построения графика.

        if (plotArea.Width <= 1d || plotArea.Height <= 1d) // Проверяет пригодность внутренней области для построения.
        { // Начинает ветку слишком маленькой области.
            DrawPlaceholder(drawingContext, controlBounds); // Рисует заглушку при недостаточной области построения.
            return; // Завершает метод при слишком маленькой области.
        } // Завершает ветку слишком маленькой области.

        AxisScale scale = CreateAxisScale(Series.Points); // Вычисляет автоматический вертикальный масштаб по реальному диапазону значений.
        Pen gridPen = new(CreateDerivedBrush(Foreground, 0.22d), 1d); // Создаёт перо сетки на основе тематического переднего плана.
        gridPen.Freeze(); // Фиксирует перо для повышения производительности.
        DrawGrid(drawingContext, plotArea, gridPen); // Рисует сетку внутри внутренней области графика.
        DrawSeries(drawingContext, plotArea, scale); // Рисует кривую сигнала с отсечением по области графика.
        DrawScaleLabels(drawingContext, controlBounds, plotArea, scale, Series.Points[^1].TimeSeconds); // Рисует подписи шкал и времени вокруг области графика.
    } // Завершает метод отрисовки.

    /// <summary> // Документирует получение кисти фона.
    /// Возвращает кисть фона графика с приоритетом пользовательского и тематического значения. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <returns>Кисть фона графика.</returns> // Документирует возвращаемое значение.
    private Brush ResolveBackgroundBrush() // Объявляет метод получения кисти фона.
    { // Начинает тело метода получения кисти фона.
        return Background; // Возвращает текущую кисть фона графика.
    } // Завершает метод получения кисти фона.

    /// <summary> // Документирует получение кисти рамки.
    /// Возвращает кисть рамки графика на основе явного значения или тематического переднего плана. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <returns>Кисть рамки графика.</returns> // Документирует возвращаемое значение.
    private Brush ResolveBorderBrush() // Объявляет метод получения кисти рамки.
    { // Начинает тело метода получения кисти рамки.
        return BorderBrush ?? CreateDerivedBrush(Foreground, 0.35d); // Возвращает явную рамку или производную кисть от тематического переднего плана.
    } // Завершает метод получения кисти рамки.

    /// <summary> // Документирует создание производной кисти.
    /// Формирует полупрозрачную кисть на основе базового тематического цвета. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="baseBrush">Базовая кисть темы.</param> // Документирует базовую кисть темы.
    /// <param name="opacity">Требуемая непрозрачность производной кисти.</param> // Документирует непрозрачность кисти.
    /// <returns>Производная кисть с заданной непрозрачностью.</returns> // Документирует возвращаемое значение.
    private static Brush CreateDerivedBrush(Brush baseBrush, double opacity) // Объявляет метод создания производной кисти.
    { // Начинает тело метода создания производной кисти.
        SolidColorBrush solidBrush = baseBrush as SolidColorBrush ?? SystemColors.ControlTextBrush as SolidColorBrush ?? Brushes.Black as SolidColorBrush ?? new SolidColorBrush(Colors.Black); // Выбирает базовую однотонную кисть для производного цвета.
        SolidColorBrush derivedBrush = new(solidBrush.Color) // Создаёт производную кисть на основе базового цвета.
        { // Начинает инициализацию производной кисти.
            Opacity = opacity, // Устанавливает непрозрачность производной кисти.
        }; // Завершает инициализацию производной кисти.
        derivedBrush.Freeze(); // Фиксирует кисть для повышения производительности.
        return derivedBrush; // Возвращает готовую производную кисть.
    } // Завершает метод создания производной кисти.

    /// <summary> // Документирует создание области графика.
    /// Вычисляет внутреннюю область для построения линии и сетки с учётом служебных подписей. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="controlBounds">Внешние границы контрола.</param> // Документирует внешние границы контрола.
    /// <returns>Внутренняя область построения графика.</returns> // Документирует возвращаемое значение.
    private static Rect CreatePlotArea(Rect controlBounds) // Объявляет метод вычисления внутренней области графика.
    { // Начинает тело метода вычисления области графика.
        return new Rect( // Возвращает область построения с внутренними отступами.
            controlBounds.Left + LeftPadding, // Вычисляет левую границу области построения.
            controlBounds.Top + TopPadding, // Вычисляет верхнюю границу области построения.
            Math.Max(0d, controlBounds.Width - LeftPadding - RightPadding), // Вычисляет ширину области построения.
            Math.Max(0d, controlBounds.Height - TopPadding - BottomPadding)); // Вычисляет высоту области построения.
    } // Завершает метод вычисления области графика.

    /// <summary> // Документирует вычисление масштаба.
    /// Строит автоматический вертикальный масштаб по фактическим минимуму и максимуму сигнала с запасом. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="points">Точки сигнала текущего окна.</param> // Документирует набор точек сигнала.
    /// <returns>Рассчитанный масштаб вертикальной оси.</returns> // Документирует возвращаемое значение.
    private static AxisScale CreateAxisScale(IReadOnlyList<AxisSignalPoint> points) // Объявляет метод вычисления вертикального масштаба.
    { // Начинает тело метода вычисления масштаба.
        double minValue = points.Min(static point => point.Acceleration); // Находит минимальное значение сигнала.
        double maxValue = points.Max(static point => point.Acceleration); // Находит максимальное значение сигнала.
        double range = maxValue - minValue; // Вычисляет фактический диапазон сигнала.

        if (range < 0.05d) // Проверяет вырожденный или слишком малый диапазон сигнала.
        { // Начинает ветку малого диапазона.
            double center = (maxValue + minValue) / 2d; // Вычисляет центр диапазона сигнала.
            minValue = center - 0.5d; // Формирует нижнюю границу минимального диапазона.
            maxValue = center + 0.5d; // Формирует верхнюю границу минимального диапазона.
            range = maxValue - minValue; // Пересчитывает диапазон сигнала.
        } // Завершает ветку малого диапазона.

        double margin = Math.Max(0.05d, range * 0.1d); // Добавляет десятипроцентный запас сверху и снизу.
        double scaledMin = minValue - margin; // Вычисляет нижнюю границу отображения.
        double scaledMax = maxValue + margin; // Вычисляет верхнюю границу отображения.
        return new AxisScale(scaledMin, scaledMax); // Возвращает рассчитанный масштаб вертикальной оси.
    } // Завершает метод вычисления вертикального масштаба.

    /// <summary> // Документирует отрисовку сетки.
    /// Рисует вспомогательную сетку внутри области построения графика. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="drawingContext">Контекст рисования WPF.</param> // Документирует контекст рисования.
    /// <param name="plotArea">Внутренняя область построения графика.</param> // Документирует область графика.
    /// <param name="gridPen">Перо служебной сетки.</param> // Документирует перо сетки.
    private static void DrawGrid(DrawingContext drawingContext, Rect plotArea, Pen gridPen) // Объявляет метод отрисовки сетки.
    { // Начинает тело метода отрисовки сетки.
        for (int lineIndex = 0; lineIndex <= 5; lineIndex++) // Перебирает линии сетки по горизонтали и вертикали.
        { // Начинает цикл отрисовки линий сетки.
            double x = plotArea.Left + ((plotArea.Width / 5d) * lineIndex); // Вычисляет координату вертикальной линии сетки.
            double y = plotArea.Top + ((plotArea.Height / 5d) * lineIndex); // Вычисляет координату горизонтальной линии сетки.
            drawingContext.DrawLine(gridPen, new Point(x, plotArea.Top), new Point(x, plotArea.Bottom)); // Рисует вертикальную линию сетки.
            drawingContext.DrawLine(gridPen, new Point(plotArea.Left, y), new Point(plotArea.Right, y)); // Рисует горизонтальную линию сетки.
        } // Завершает цикл отрисовки линий сетки.
    } // Завершает метод отрисовки сетки.

    /// <summary> // Документирует отрисовку серии.
    /// Рисует линию сигнала с жёстким отсечением по внутренней области графика. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="drawingContext">Контекст рисования WPF.</param> // Документирует контекст рисования.
    /// <param name="plotArea">Внутренняя область построения графика.</param> // Документирует область графика.
    /// <param name="scale">Вертикальный масштаб графика.</param> // Документирует рассчитанный масштаб.
    private void DrawSeries(DrawingContext drawingContext, Rect plotArea, AxisScale scale) // Объявляет метод отрисовки линии сигнала.
    { // Начинает тело метода отрисовки линии сигнала.
        double firstTime = Series!.Points[0].TimeSeconds; // Получает начальное время ряда.
        double lastTime = Series.Points[^1].TimeSeconds; // Получает конечное время ряда.
        double duration = Math.Max(0.001d, lastTime - firstTime); // Вычисляет длительность отображаемого окна.
        StreamGeometry geometry = new(); // Создаёт геометрию кривой сигнала.

        using (StreamGeometryContext context = geometry.Open()) // Открывает контекст записи геометрии.
        { // Начинает блок записи геометрии.
            for (int index = 0; index < Series.Points.Count; index++) // Перебирает все точки ряда.
            { // Начинает цикл записи точек.
                AxisSignalPoint point = Series.Points[index]; // Получает текущую точку сигнала.
                double normalizedX = (point.TimeSeconds - firstTime) / duration; // Нормирует координату времени.
                double normalizedY = scale.Normalize(point.Acceleration); // Нормирует координату амплитуды по реальному диапазону сигнала.
                Point screenPoint = new( // Формирует экранную координату точки графика.
                    plotArea.Left + (normalizedX * plotArea.Width), // Вычисляет экранную координату X.
                    plotArea.Bottom - (Math.Clamp(normalizedY, 0d, 1d) * plotArea.Height)); // Вычисляет экранную координату Y внутри области построения.

                if (index == 0) // Проверяет, является ли точка первой в кривой.
                { // Начинает ветку первой точки.
                    context.BeginFigure(screenPoint, false, false); // Начинает новую фигуру геометрии.
                } // Завершает ветку первой точки.
                else // Обрабатывает последующие точки.
                { // Начинает ветку последующих точек.
                    context.LineTo(screenPoint, true, false); // Добавляет сегмент линии к следующей точке.
                } // Завершает ветку последующих точек.
            } // Завершает цикл записи точек.
        } // Завершает блок записи геометрии.

        geometry.Freeze(); // Фиксирует геометрию для повышения производительности.
        Pen signalPen = new(new SolidColorBrush(Series.Stroke), 1.5d) // Создаёт перо линии сигнала.
        { // Начинает инициализацию пера сигнала.
            StartLineCap = PenLineCap.Round, // Скругляет начало линии для более аккуратного вида.
            EndLineCap = PenLineCap.Round, // Скругляет конец линии для более аккуратного вида.
            LineJoin = PenLineJoin.Round, // Скругляет соединения сегментов линии.
        }; // Завершает инициализацию пера сигнала.
        signalPen.Freeze(); // Фиксирует перо сигнала для повышения производительности.
        drawingContext.PushClip(new RectangleGeometry(plotArea)); // Включает отсечение рисования по внутренней области графика.
        drawingContext.DrawGeometry(null, signalPen, geometry); // Отрисовывает кривую сигнала с отсечением по области графика.
        drawingContext.Pop(); // Снимает отсечение после отрисовки сигнала.
    } // Завершает метод отрисовки линии сигнала.

    /// <summary> // Документирует отрисовку заглушки.
    /// Рисует текст, информирующий об отсутствии данных для графика. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="drawingContext">Контекст рисования WPF.</param> // Документирует контекст рисования.
    /// <param name="controlBounds">Внешняя область построения графика.</param> // Документирует внешнюю область графика.
    private void DrawPlaceholder(DrawingContext drawingContext, Rect controlBounds) // Объявляет метод отрисовки заглушки.
    { // Начинает тело метода отрисовки заглушки.
        FormattedText text = CreateText("Нет данных для отображения", 14d, ResolveTextBrush()); // Формирует подпись отсутствия данных.
        Point location = new( // Определяет координаты расположения подписи.
            controlBounds.Left + ((controlBounds.Width - text.Width) / 2d), // Центрирует текст по горизонтали.
            controlBounds.Top + ((controlBounds.Height - text.Height) / 2d)); // Центрирует текст по вертикали.
        drawingContext.DrawText(text, location); // Отрисовывает подпись отсутствия данных.
    } // Завершает метод отрисовки заглушки.

    /// <summary> // Документирует получение кисти текста.
    /// Возвращает основную тематическую кисть для подписей графика. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <returns>Кисть текста подписей.</returns> // Документирует возвращаемое значение.
    private Brush ResolveTextBrush() // Объявляет метод получения кисти текста.
    { // Начинает тело метода получения кисти текста.
        return Foreground; // Возвращает кисть тематического переднего плана.
    } // Завершает метод получения кисти текста.

    /// <summary> // Документирует отрисовку шкал.
    /// Рисует подписи времени и вертикальной шкалы вокруг внутренней области графика. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="drawingContext">Контекст рисования WPF.</param> // Документирует контекст рисования.
    /// <param name="controlBounds">Внешние границы контрола.</param> // Документирует внешние границы контрола.
    /// <param name="plotArea">Внутренняя область построения графика.</param> // Документирует внутреннюю область графика.
    /// <param name="scale">Вертикальный масштаб графика.</param> // Документирует вертикальный масштаб.
    /// <param name="duration">Длительность сигнала в окне.</param> // Документирует длительность сигнала.
    private void DrawScaleLabels(DrawingContext drawingContext, Rect controlBounds, Rect plotArea, AxisScale scale, double duration) // Объявляет метод отрисовки подписей.
    { // Начинает тело метода отрисовки подписей.
        Brush textBrush = ResolveTextBrush(); // Получает тематическую кисть текста для подписей графика.
        FormattedText topLabel = CreateText($"{scale.MaxValue:F2} g", 11d, textBrush); // Формирует верхнюю подпись амплитуды.
        FormattedText middleLabel = CreateText($"{((scale.MaxValue + scale.MinValue) / 2d):F2} g", 11d, textBrush); // Формирует среднюю подпись амплитуды.
        FormattedText bottomLabel = CreateText($"{scale.MinValue:F2} g", 11d, textBrush); // Формирует нижнюю подпись амплитуды.
        FormattedText zeroLabel = CreateText("0 с", 11d, textBrush); // Формирует подпись начала временной оси.
        FormattedText timeLabel = CreateText($"{duration:F2} с", 11d, textBrush); // Формирует подпись конца временной оси.
        drawingContext.DrawText(topLabel, new Point(controlBounds.Left + 8d, plotArea.Top - (topLabel.Height / 2d))); // Рисует верхнюю подпись амплитуды.
        drawingContext.DrawText(middleLabel, new Point(controlBounds.Left + 8d, plotArea.Top + ((plotArea.Height - middleLabel.Height) / 2d))); // Рисует среднюю подпись амплитуды.
        drawingContext.DrawText(bottomLabel, new Point(controlBounds.Left + 8d, plotArea.Bottom - (bottomLabel.Height / 2d))); // Рисует нижнюю подпись амплитуды.
        drawingContext.DrawText(zeroLabel, new Point(plotArea.Left, plotArea.Bottom + 4d)); // Рисует подпись начала временной оси.
        drawingContext.DrawText(timeLabel, new Point(plotArea.Right - timeLabel.Width, plotArea.Bottom + 4d)); // Рисует подпись конца временной оси.
    } // Завершает метод отрисовки подписей.

    /// <summary> // Документирует создание форматированного текста.
    /// Создаёт объект текста с корректным DPI для отрисовки в WPF. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="text">Строка текста.</param> // Документирует содержимое текста.
    /// <param name="fontSize">Размер шрифта.</param> // Документирует размер шрифта.
    /// <param name="brush">Кисть текста.</param> // Документирует кисть текста.
    /// <returns>Подготовленный объект форматированного текста.</returns> // Документирует возвращаемое значение.
    private static FormattedText CreateText(string text, double fontSize, Brush brush) // Объявляет метод создания текста.
    { // Начинает тело метода создания текста.
        return new FormattedText( // Создаёт форматированный текст для отрисовки.
            text, // Передаёт исходную строку текста.
            CultureInfo.CurrentUICulture, // Передаёт текущую культуру интерфейса.
            FlowDirection.LeftToRight, // Передаёт направление письма.
            new Typeface("Segoe UI"), // Передаёт гарнитуру шрифта.
            fontSize, // Передаёт размер шрифта.
            brush, // Передаёт кисть текста.
            1.25d); // Передаёт коэффициент DPI для корректной отрисовки.
    } // Завершает метод создания текста.

    /// <summary> // Документирует внутренний тип масштаба.
    /// Хранит минимальное и максимальное значения вертикальной оси и умеет нормировать значения сигнала. // Уточняет назначение внутреннего типа.
    /// </summary> // Завершает XML-документацию типа.
    /// <param name="MinValue">Минимум вертикальной оси.</param> // Документирует минимум оси.
    /// <param name="MaxValue">Максимум вертикальной оси.</param> // Документирует максимум оси.
    private sealed record AxisScale(double MinValue, double MaxValue) // Объявляет тип вертикального масштаба.
    { // Начинает тело типа вертикального масштаба.
        /// <summary> // Документирует нормирование значения.
        /// Преобразует исходное значение сигнала в нормированную координату от нуля до единицы. // Уточняет назначение метода.
        /// </summary> // Завершает XML-документацию метода.
        /// <param name="value">Исходное значение сигнала.</param> // Документирует исходное значение.
        /// <returns>Нормированная координата значения.</returns> // Документирует возвращаемое значение.
        public double Normalize(double value) // Объявляет метод нормирования значения.
        { // Начинает тело метода нормирования.
            return (value - MinValue) / Math.Max(0.0001d, MaxValue - MinValue); // Возвращает нормированную координату в диапазоне масштаба.
        } // Завершает метод нормирования.
    } // Завершает тело типа вертикального масштаба.
} // Завершает тело пользовательского контрола.
