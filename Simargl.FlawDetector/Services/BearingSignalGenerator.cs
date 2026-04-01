using Simargl.FlawDetector.Models; // Подключает модели предметной области генератора.

namespace Simargl.FlawDetector.Services; // Определяет пространство имён сервисов генерации.

/// <summary> // Описывает назначение класса.
/// Генерирует правдоподобный трёхкоординатный вибросигнал буксового подшипника вагона с дефектами. // Уточняет функциональную ответственность сервиса.
/// </summary> // Завершает XML-документацию класса.
internal sealed class BearingSignalGenerator // Объявляет сервис генерации вибрации.
{ // Начинает тело класса.
    private static readonly Color XAxisColor = Color.FromRgb(0xE6, 0x39, 0x46); // Хранит цвет графика оси X.
    private static readonly Color YAxisColor = Color.FromRgb(0x2A, 0x9D, 0x8F); // Хранит цвет графика оси Y.
    private static readonly Color ZAxisColor = Color.FromRgb(0x1D, 0x35, 0x7A); // Хранит цвет графика оси Z.

    /// <summary> // Документирует метод генерации.
    /// Формирует набор временных рядов и диагностические частоты для выбранных параметров узла. // Уточняет результат работы метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="settings">Настройки геометрии, движения и дефектов.</param> // Документирует входные настройки.
    /// <returns>Результат симуляции трёхосевого сигнала.</returns> // Документирует возвращаемое значение.
    public SimulationResult Generate(SignalGenerationSettings settings) // Объявляет основной метод генерации.
    { // Начинает тело метода.
        return GenerateWindow(settings, settings.DurationSeconds); // Возвращает окно сигнала, заканчивающееся в точке длительности.
    } // Завершает основной метод генерации.

    /// <summary> // Документирует генерацию окна реального времени.
    /// Формирует скользящее окно сигнала, завершающееся в указанной точке модельного времени. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="settings">Настройки геометрии, движения и дефектов.</param> // Документирует входные настройки.
    /// <param name="windowEndTimeSeconds">Время конца отображаемого окна в секундах.</param> // Документирует конец окна по времени.
    /// <returns>Результат симуляции для текущего окна.</returns> // Документирует возвращаемое значение.
    /// <exception cref="ArgumentNullException">Возникает, если настройки отсутствуют.</exception> // Документирует ошибку отсутствия аргумента.
    /// <exception cref="ArgumentOutOfRangeException">Возникает, если параметры симуляции выходят за допустимые пределы.</exception> // Документирует ошибку диапазона.
    public SimulationResult GenerateWindow(SignalGenerationSettings settings, double windowEndTimeSeconds) // Объявляет метод генерации окна реального времени.
    { // Начинает тело метода генерации окна.
        ArgumentNullException.ThrowIfNull(settings); // Проверяет наличие настроек.
        ValidateSettings(settings); // Проверяет диапазоны настроек.

        double normalizedWindowEndTime = Math.Max(settings.DurationSeconds, windowEndTimeSeconds); // Нормализует конец окна, чтобы окно не было пустым.
        int sampleCount = Math.Max(64, (int)Math.Round(settings.DurationSeconds * settings.SampleRate, MidpointRounding.AwayFromZero)); // Вычисляет количество отсчётов в окне.
        double sampleInterval = settings.DurationSeconds / (sampleCount - 1d); // Вычисляет шаг дискретизации в окне отображения.
        BearingKinematics kinematics = CreateKinematics(settings); // Формирует кинематические характеристики подшипника.
        List<AxisSignalPoint> xPoints = new(sampleCount); // Создаёт буфер точек оси X.
        List<AxisSignalPoint> yPoints = new(sampleCount); // Создаёт буфер точек оси Y.
        List<AxisSignalPoint> zPoints = new(sampleCount); // Создаёт буфер точек оси Z.
        double xSquareSum = 0d; // Накопитель квадрата сигнала оси X.
        double ySquareSum = 0d; // Накопитель квадрата сигнала оси Y.
        double zSquareSum = 0d; // Накопитель квадрата сигнала оси Z.
        double xPeak = 0d; // Накопитель пикового значения оси X.
        double yPeak = 0d; // Накопитель пикового значения оси Y.
        double zPeak = 0d; // Накопитель пикового значения оси Z.

        for (int sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++) // Запускает цикл расчёта каждого отсчёта окна.
        { // Начинает тело цикла расчёта отсчётов.
            double absoluteTime = normalizedWindowEndTime - settings.DurationSeconds + (sampleIndex * sampleInterval); // Вычисляет абсолютное время отсчёта.
            absoluteTime = Math.Max(0d, absoluteTime); // Ограничивает время неотрицательной областью.
            SignalSample sample = CalculateSample(settings, kinematics, absoluteTime); // Вычисляет трёхосевой отсчёт в абсолютный момент времени.
            double plotTime = absoluteTime - (normalizedWindowEndTime - settings.DurationSeconds); // Преобразует абсолютное время в координату окна.
            xPoints.Add(new AxisSignalPoint(plotTime, sample.X)); // Сохраняет точку сигнала оси X.
            yPoints.Add(new AxisSignalPoint(plotTime, sample.Y)); // Сохраняет точку сигнала оси Y.
            zPoints.Add(new AxisSignalPoint(plotTime, sample.Z)); // Сохраняет точку сигнала оси Z.
            xSquareSum += sample.X * sample.X; // Накопляет квадрат сигнала оси X.
            ySquareSum += sample.Y * sample.Y; // Накопляет квадрат сигнала оси Y.
            zSquareSum += sample.Z * sample.Z; // Накопляет квадрат сигнала оси Z.
            xPeak = Math.Max(xPeak, Math.Abs(sample.X)); // Обновляет пик оси X.
            yPeak = Math.Max(yPeak, Math.Abs(sample.Y)); // Обновляет пик оси Y.
            zPeak = Math.Max(zPeak, Math.Abs(sample.Z)); // Обновляет пик оси Z.
        } // Завершает цикл расчёта отсчётов.

        AxisSignalSeries xAxis = new("Ось X", XAxisColor, xPoints, xPeak, Math.Sqrt(xSquareSum / sampleCount)); // Формирует результат по оси X.
        AxisSignalSeries yAxis = new("Ось Y", YAxisColor, yPoints, yPeak, Math.Sqrt(ySquareSum / sampleCount)); // Формирует результат по оси Y.
        AxisSignalSeries zAxis = new("Ось Z", ZAxisColor, zPoints, zPeak, Math.Sqrt(zSquareSum / sampleCount)); // Формирует результат по оси Z.
        string summary = BuildSummary(settings, normalizedWindowEndTime, kinematics, xAxis, yAxis, zAxis); // Формирует сводку по текущему окну.
        return new SimulationResult(xAxis, yAxis, zAxis, kinematics.WheelRotationFrequency, kinematics.Bpfi, kinematics.Bpfo, kinematics.Bsf, kinematics.Ftf, summary); // Возвращает итоговый результат генерации.
    } // Завершает метод генерации окна.

    /// <summary> // Документирует расчёт кинематики.
    /// Вычисляет характеристические частоты и коэффициенты узла по текущим настройкам. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="settings">Настройки геометрии и движения.</param> // Документирует входные настройки.
    /// <returns>Кинематические параметры подшипника.</returns> // Документирует возвращаемое значение.
    private static BearingKinematics CreateKinematics(SignalGenerationSettings settings) // Объявляет метод расчёта кинематики.
    { // Начинает тело метода расчёта кинематики.
        double contactAngleRadians = DegreesToRadians(settings.ContactAngleDegrees); // Преобразует контактный угол в радианы.
        double wheelRadiusMeters = settings.WheelDiameterMillimeters / 2000d; // Переводит диаметр колеса в радиус в метрах.
        double speedMetersPerSecond = settings.WagonSpeedKilometersPerHour / 3.6d; // Переводит скорость вагона в систему СИ.
        double wheelRotationFrequency = speedMetersPerSecond / (2d * Math.PI * wheelRadiusMeters); // Вычисляет частоту вращения колеса.
        double geometryRatio = settings.RollingElementDiameterMillimeters / settings.BearingPitchDiameterMillimeters; // Вычисляет относительную геометрию тела качения.
        double cosine = Math.Cos(contactAngleRadians); // Вычисляет косинус контактного угла.
        double bpfo = 0.5d * settings.RollingElementCount * wheelRotationFrequency * (1d - (geometryRatio * cosine)); // Вычисляет частоту дефекта наружного кольца.
        double bpfi = 0.5d * settings.RollingElementCount * wheelRotationFrequency * (1d + (geometryRatio * cosine)); // Вычисляет частоту дефекта внутреннего кольца.
        double bsf = (settings.BearingPitchDiameterMillimeters / (2d * settings.RollingElementDiameterMillimeters)) * wheelRotationFrequency * (1d - Math.Pow(geometryRatio * cosine, 2d)); // Вычисляет частоту дефекта тела качения.
        double ftf = 0.5d * wheelRotationFrequency * (1d - (geometryRatio * cosine)); // Вычисляет частоту дефекта сепаратора.
        double loadScale = 0.85d + (settings.AxleLoadKilonewtons / 250d); // Получает коэффициент роста вибрации от нагрузки.
        return new BearingKinematics(wheelRotationFrequency, bpfi, bpfo, bsf, ftf, loadScale); // Возвращает набор кинематических параметров.
    } // Завершает метод расчёта кинематики.

    /// <summary> // Документирует расчёт отсчёта.
    /// Вычисляет один трёхосевой отсчёт сигнала в заданный момент модельного времени. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="settings">Настройки генерации сигнала.</param> // Документирует входные настройки.
    /// <param name="kinematics">Кинематические параметры узла.</param> // Документирует вычисленную кинематику.
    /// <param name="timeSeconds">Абсолютное модельное время.</param> // Документирует момент времени отсчёта.
    /// <returns>Трёхкоординатный отсчёт сигнала.</returns> // Документирует возвращаемое значение.
    private static SignalSample CalculateSample(SignalGenerationSettings settings, BearingKinematics kinematics, double timeSeconds) // Объявляет метод расчёта отсчёта.
    { // Начинает тело метода расчёта отсчёта.
        SignalSourceDefinition wheelRotationSource = GetSource(settings, SignalSourceType.WheelRotation); // Получает настройки вращательной вибрации.
        SignalSourceDefinition trackExcitationSource = GetSource(settings, SignalSourceType.TrackExcitation); // Получает настройки возбуждения от пути.
        SignalSourceDefinition structuralResonanceSource = GetSource(settings, SignalSourceType.StructuralResonance); // Получает настройки структурного резонанса.
        SignalSourceDefinition defectImpulsesSource = GetSource(settings, SignalSourceType.DefectImpulses); // Получает настройки дефектных импульсов.
        SignalSourceDefinition measurementNoiseSource = GetSource(settings, SignalSourceType.MeasurementNoise); // Получает настройки измерительного шума.
        double rotationPhase = 2d * Math.PI * kinematics.WheelRotationFrequency * timeSeconds; // Вычисляет фазу вращения колесной пары.
        double xSignal = 0d; // Инициализирует продольную составляющую сигнала.
        double ySignal = 0d; // Инициализирует поперечную составляющую сигнала.
        double zSignal = 0d; // Инициализирует вертикальную составляющую сигнала.

        if (wheelRotationSource.IsEnabled) // Проверяет включение вращательной вибрации.
        { // Начинает ветку вращательной вибрации.
            double sourceRotationPhase = rotationPhase * wheelRotationSource.FrequencyScale; // Масштабирует фазу вращательной вибрации.
            double wheelUnroundness = wheelRotationSource.AmplitudeScale * ((0.18d * kinematics.LoadScale * Math.Sin(sourceRotationPhase)) + (0.06d * Math.Sin(2d * sourceRotationPhase))); // Формирует базовую вибрацию от вращения узла.
            xSignal += 0.55d * wheelUnroundness; // Добавляет продольный вклад вращательной вибрации.
            ySignal += 0.75d * wheelUnroundness; // Добавляет поперечный вклад вращательной вибрации.
            zSignal += 0.35d * wheelUnroundness; // Добавляет вертикальный вклад вращательной вибрации.
        } // Завершает ветку вращательной вибрации.

        if (trackExcitationSource.IsEnabled) // Проверяет включение возбуждения от пути.
        { // Начинает ветку возбуждения от пути.
            double trackExcitation = trackExcitationSource.AmplitudeScale * 0.12d * kinematics.LoadScale * Math.Sin((2d * Math.PI * 1.8d * trackExcitationSource.FrequencyScale * timeSeconds) + 0.15d); // Формирует низкочастотное возбуждение от пути.
            xSignal += 0.65d * trackExcitation; // Добавляет продольный вклад возбуждения от пути.
            ySignal += 0.45d * trackExcitation; // Добавляет поперечный вклад возбуждения от пути.
            zSignal += 0.95d * trackExcitation; // Добавляет вертикальный вклад возбуждения от пути.
        } // Завершает ветку возбуждения от пути.

        if (structuralResonanceSource.IsEnabled) // Проверяет включение структурного резонанса.
        { // Начинает ветку структурного резонанса.
            double structuralResonance = structuralResonanceSource.AmplitudeScale * 0.09d * kinematics.LoadScale * Math.Sin((2d * Math.PI * 38d * structuralResonanceSource.FrequencyScale * timeSeconds) + 0.7d); // Формирует колебание рамы тележки.
            xSignal += 0.35d * structuralResonance; // Добавляет продольный вклад структурного резонанса.
            ySignal += 0.25d * structuralResonance; // Добавляет поперечный вклад структурного резонанса.
            zSignal += 0.9d * structuralResonance; // Добавляет вертикальный вклад структурного резонанса.
        } // Завершает ветку структурного резонанса.

        if (defectImpulsesSource.IsEnabled) // Проверяет включение дефектных импульсов.
        { // Начинает ветку дефектных импульсов.
            foreach (FaultDefinition fault in settings.Faults.Where(static fault => fault.IsEnabled && fault.Severity > 0d)) // Перебирает активные дефекты.
            { // Начинает обработку одного дефекта.
                double excitationFrequency = GetFaultFrequency(fault.FaultType, kinematics.WheelRotationFrequency, kinematics.Bpfi, kinematics.Bpfo, kinematics.Bsf, kinematics.Ftf) * defectImpulsesSource.FrequencyScale; // Определяет базовую частоту дефекта.
                double excitationPhase = 2d * Math.PI * excitationFrequency * timeSeconds; // Вычисляет фазу повторения ударов дефекта.
                double impulseEnvelope = Math.Pow(Math.Max(0d, Math.Sin(excitationPhase)), 6d); // Формирует узкий ударный импульс в момент контакта.
                double resonanceCarrier = Math.Sin((2d * Math.PI * 620d * defectImpulsesSource.FrequencyScale * timeSeconds) + (0.35d * excitationFrequency)); // Формирует высокочастотный носитель резонанса.
                double modulation = 1d + (0.18d * Math.Sin(rotationPhase + (0.8d * fault.Severity))); // Добавляет модуляцию вращением узла.
                double faultAmplitude = defectImpulsesSource.AmplitudeScale * fault.ImpactFactor * fault.Severity * kinematics.LoadScale * modulation; // Вычисляет интенсивность отклика дефекта.

                switch (fault.FaultType) // Выбирает модель пространственного отклика по типу дефекта.
                { // Начинает ветвление по типу дефекта.
                    case BearingFaultType.OuterRace: // Обрабатывает дефект наружного кольца.
                        xSignal += 0.55d * faultAmplitude * impulseEnvelope * resonanceCarrier; // Добавляет продольный отклик дефекта наружного кольца.
                        ySignal += 0.42d * faultAmplitude * impulseEnvelope * Math.Cos((2d * Math.PI * 540d * defectImpulsesSource.FrequencyScale * timeSeconds) + 0.4d); // Добавляет поперечный отклик дефекта наружного кольца.
                        zSignal += 1.15d * faultAmplitude * impulseEnvelope * Math.Sin((2d * Math.PI * 700d * defectImpulsesSource.FrequencyScale * timeSeconds) + 0.2d); // Добавляет вертикальный отклик дефекта наружного кольца.
                        break; // Завершает обработку данного типа дефекта.

                    case BearingFaultType.InnerRace: // Обрабатывает дефект внутреннего кольца.
                        xSignal += 0.95d * faultAmplitude * impulseEnvelope * Math.Sin((2d * Math.PI * 650d * defectImpulsesSource.FrequencyScale * timeSeconds) + rotationPhase); // Добавляет продольный отклик дефекта внутреннего кольца.
                        ySignal += 0.75d * faultAmplitude * impulseEnvelope * Math.Cos((2d * Math.PI * 500d * defectImpulsesSource.FrequencyScale * timeSeconds) + (0.3d * rotationPhase)); // Добавляет поперечный отклик дефекта внутреннего кольца.
                        zSignal += 0.9d * faultAmplitude * impulseEnvelope * resonanceCarrier; // Добавляет вертикальный отклик дефекта внутреннего кольца.
                        break; // Завершает обработку данного типа дефекта.

                    case BearingFaultType.RollingElement: // Обрабатывает дефект тела качения.
                        xSignal += 0.48d * faultAmplitude * impulseEnvelope * Math.Sin((2d * Math.PI * 780d * defectImpulsesSource.FrequencyScale * timeSeconds) + 0.5d); // Добавляет продольный отклик дефекта ролика.
                        ySignal += 0.68d * faultAmplitude * impulseEnvelope * Math.Cos((2d * Math.PI * 810d * defectImpulsesSource.FrequencyScale * timeSeconds) + rotationPhase); // Добавляет поперечный отклик дефекта ролика.
                        zSignal += 0.88d * faultAmplitude * impulseEnvelope * Math.Sin((2d * Math.PI * 760d * defectImpulsesSource.FrequencyScale * timeSeconds) + 0.15d); // Добавляет вертикальный отклик дефекта ролика.
                        break; // Завершает обработку данного типа дефекта.

                    case BearingFaultType.Cage: // Обрабатывает дефект сепаратора.
                        xSignal += 0.25d * faultAmplitude * Math.Sin(excitationPhase) * (1d + (0.1d * resonanceCarrier)); // Добавляет продольный низкочастотный отклик сепаратора.
                        ySignal += 0.34d * faultAmplitude * Math.Sin(excitationPhase + 0.45d); // Добавляет поперечный низкочастотный отклик сепаратора.
                        zSignal += 0.52d * faultAmplitude * Math.Sin(excitationPhase + 0.9d); // Добавляет вертикальный низкочастотный отклик сепаратора.
                        break; // Завершает обработку данного типа дефекта.

                    case BearingFaultType.Unbalance: // Обрабатывает дисбаланс узла.
                        xSignal += 0.85d * faultAmplitude * Math.Sin(rotationPhase + 0.12d); // Добавляет продольный отклик дисбаланса.
                        ySignal += 1.1d * faultAmplitude * Math.Cos(rotationPhase + 0.3d); // Добавляет поперечный отклик дисбаланса.
                        zSignal += 0.3d * faultAmplitude * Math.Sin((2d * rotationPhase) + 0.15d); // Добавляет вертикальный отклик дисбаланса.
                        break; // Завершает обработку данного типа дефекта.

                    case BearingFaultType.Misalignment: // Обрабатывает несоосность узла.
                        xSignal += 0.72d * faultAmplitude * Math.Sin((2d * rotationPhase) + 0.15d); // Добавляет продольный отклик несоосности.
                        ySignal += 0.63d * faultAmplitude * Math.Cos((2d * rotationPhase) + 0.1d); // Добавляет поперечный отклик несоосности.
                        zSignal += 0.44d * faultAmplitude * Math.Sin(rotationPhase + 0.45d); // Добавляет вертикальный отклик несоосности.
                        break; // Завершает обработку данного типа дефекта.
                } // Завершает ветвление по типу дефекта.
            } // Завершает обработку активных дефектов.
        } // Завершает ветку дефектных импульсов.

        if (measurementNoiseSource.IsEnabled) // Проверяет включение измерительного шума.
        { // Начинает ветку измерительного шума.
            xSignal += GetPseudoNoise(timeSeconds * measurementNoiseSource.FrequencyScale, measurementNoiseSource.AmplitudeScale * 0.031d * kinematics.LoadScale, 0.73d); // Добавляет детерминированный шум измерения на ось X.
            ySignal += GetPseudoNoise(timeSeconds * measurementNoiseSource.FrequencyScale, measurementNoiseSource.AmplitudeScale * 0.028d * kinematics.LoadScale, 1.91d); // Добавляет детерминированный шум измерения на ось Y.
            zSignal += GetPseudoNoise(timeSeconds * measurementNoiseSource.FrequencyScale, measurementNoiseSource.AmplitudeScale * 0.035d * kinematics.LoadScale, 2.57d); // Добавляет детерминированный шум измерения на ось Z.
        } // Завершает ветку измерительного шума.

        return new SignalSample(xSignal, ySignal, zSignal); // Возвращает рассчитанный трёхосевой отсчёт.
    } // Завершает метод расчёта отсчёта.

    /// <summary> // Документирует получение источника сигнала.
    /// Возвращает настройки указанного источника сигнала или значение по умолчанию, если источник отсутствует. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="settings">Текущие настройки генерации.</param> // Документирует входные настройки генерации.
    /// <param name="sourceType">Тип искомого источника сигнала.</param> // Документирует тип искомого источника.
    /// <returns>Настройки указанного источника сигнала.</returns> // Документирует возвращаемое значение.
    private static SignalSourceDefinition GetSource(SignalGenerationSettings settings, SignalSourceType sourceType) // Объявляет метод получения источника сигнала.
    { // Начинает тело метода получения источника сигнала.
        return settings.Sources.FirstOrDefault(source => source.SourceType == sourceType) ?? new SignalSourceDefinition(sourceType, sourceType.ToString(), false, 1d, 1d); // Возвращает найденный источник или значение по умолчанию.
    } // Завершает метод получения источника сигнала.

    /// <summary> // Документирует псевдошум.
    /// Возвращает гладкий детерминированный шумовой компонент для непрерывной потоковой симуляции. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="timeSeconds">Абсолютное модельное время.</param> // Документирует момент времени сигнала.
    /// <param name="amplitude">Амплитуда шумовой компоненты.</param> // Документирует амплитуду шума.
    /// <param name="phaseSeed">Фазовый сдвиг оси.</param> // Документирует сдвиг для развязки осей.
    /// <returns>Значение шумовой компоненты.</returns> // Документирует возвращаемое значение.
    private static double GetPseudoNoise(double timeSeconds, double amplitude, double phaseSeed) // Объявляет метод вычисления псевдошума.
    { // Начинает тело метода вычисления псевдошума.
        double harmonicA = Math.Sin((2d * Math.PI * 113d * timeSeconds) + phaseSeed); // Вычисляет первую шумовую гармонику.
        double harmonicB = Math.Sin((2d * Math.PI * 173d * timeSeconds) + (phaseSeed * 1.7d)); // Вычисляет вторую шумовую гармонику.
        double harmonicC = Math.Cos((2d * Math.PI * 251d * timeSeconds) + (phaseSeed * 2.3d)); // Вычисляет третью шумовую гармонику.
        return amplitude * ((0.55d * harmonicA) + (0.3d * harmonicB) + (0.15d * harmonicC)); // Возвращает суммарную шумовую компоненту.
    } // Завершает метод вычисления псевдошума.

    /// <summary> // Документирует метод проверки параметров.
    /// Проверяет допустимость входных параметров генерации. // Уточняет назначение валидации.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="settings">Настройки генерации сигнала.</param> // Документирует набор настроек.
    /// <exception cref="ArgumentOutOfRangeException">Возникает, если найден недопустимый параметр.</exception> // Документирует ошибку диапазона.
    private static void ValidateSettings(SignalGenerationSettings settings) // Объявляет метод валидации.
    { // Начинает тело метода валидации.
        if (settings.WagonSpeedKilometersPerHour <= 0d) // Проверяет скорость вагона.
        { // Начинает ветку ошибки скорости.
            throw new ArgumentOutOfRangeException(nameof(settings.WagonSpeedKilometersPerHour), "Скорость вагона должна быть положительной."); // Сообщает об ошибке диапазона скорости.
        } // Завершает ветку ошибки скорости.

        if (settings.WheelDiameterMillimeters <= 100d) // Проверяет диаметр колеса.
        { // Начинает ветку ошибки диаметра колеса.
            throw new ArgumentOutOfRangeException(nameof(settings.WheelDiameterMillimeters), "Диаметр колеса должен быть больше 100 мм."); // Сообщает об ошибке диапазона диаметра колеса.
        } // Завершает ветку ошибки диаметра колеса.

        if (settings.AxleLoadKilonewtons <= 0d) // Проверяет нагрузку на ось.
        { // Начинает ветку ошибки нагрузки.
            throw new ArgumentOutOfRangeException(nameof(settings.AxleLoadKilonewtons), "Нагрузка на ось должна быть положительной."); // Сообщает об ошибке диапазона нагрузки.
        } // Завершает ветку ошибки нагрузки.

        if (settings.BearingPitchDiameterMillimeters <= 0d) // Проверяет средний диаметр подшипника.
        { // Начинает ветку ошибки среднего диаметра подшипника.
            throw new ArgumentOutOfRangeException(nameof(settings.BearingPitchDiameterMillimeters), "Средний диаметр подшипника должен быть положительным."); // Сообщает об ошибке диапазона среднего диаметра.
        } // Завершает ветку ошибки среднего диаметра подшипника.

        if (settings.RollingElementDiameterMillimeters <= 0d || settings.RollingElementDiameterMillimeters >= settings.BearingPitchDiameterMillimeters) // Проверяет диаметр тела качения.
        { // Начинает ветку ошибки диаметра тела качения.
            throw new ArgumentOutOfRangeException(nameof(settings.RollingElementDiameterMillimeters), "Диаметр тела качения должен быть положительным и меньше среднего диаметра подшипника."); // Сообщает об ошибке диапазона тела качения.
        } // Завершает ветку ошибки диаметра тела качения.

        if (settings.RollingElementCount < 4) // Проверяет количество тел качения.
        { // Начинает ветку ошибки количества тел качения.
            throw new ArgumentOutOfRangeException(nameof(settings.RollingElementCount), "Количество тел качения должно быть не меньше четырёх."); // Сообщает об ошибке диапазона количества тел качения.
        } // Завершает ветку ошибки количества тел качения.

        if (settings.SampleRate < 256) // Проверяет частоту дискретизации.
        { // Начинает ветку ошибки частоты дискретизации.
            throw new ArgumentOutOfRangeException(nameof(settings.SampleRate), "Частота дискретизации должна быть не ниже 256 Гц."); // Сообщает об ошибке диапазона частоты дискретизации.
        } // Завершает ветку ошибки частоты дискретизации.

        if (settings.DurationSeconds <= 0.1d) // Проверяет длительность сигнала.
        { // Начинает ветку ошибки длительности.
            throw new ArgumentOutOfRangeException(nameof(settings.DurationSeconds), "Длительность сигнала должна быть больше 0,1 секунды."); // Сообщает об ошибке диапазона длительности.
        } // Завершает ветку ошибки длительности.
    } // Завершает метод валидации.

    /// <summary> // Документирует преобразование угла.
    /// Переводит градусы в радианы. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="degrees">Угол в градусах.</param> // Документирует входной угол.
    /// <returns>Угол в радианах.</returns> // Документирует возвращаемое значение.
    private static double DegreesToRadians(double degrees) // Объявляет метод преобразования единиц.
    { // Начинает тело метода преобразования.
        return degrees * (Math.PI / 180d); // Возвращает значение в радианах.
    } // Завершает метод преобразования.

    /// <summary> // Документирует выбор частоты дефекта.
    /// Возвращает характеристическую частоту для указанного типа дефекта. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="faultType">Тип дефекта.</param> // Документирует тип дефекта.
    /// <param name="wheelRotationFrequency">Частота вращения колеса.</param> // Документирует частоту вращения.
    /// <param name="bpfi">Частота BPFI.</param> // Документирует частоту внутреннего кольца.
    /// <param name="bpfo">Частота BPFO.</param> // Документирует частоту наружного кольца.
    /// <param name="bsf">Частота BSF.</param> // Документирует частоту тела качения.
    /// <param name="ftf">Частота FTF.</param> // Документирует частоту сепаратора.
    /// <returns>Частота возбуждения для дефекта.</returns> // Документирует возвращаемое значение.
    private static double GetFaultFrequency(BearingFaultType faultType, double wheelRotationFrequency, double bpfi, double bpfo, double bsf, double ftf) // Объявляет метод выбора частоты.
    { // Начинает тело метода выбора частоты.
        return faultType switch // Выбирает частоту на основании типа дефекта.
        { // Начинает switch-выражение.
            BearingFaultType.OuterRace => bpfo, // Возвращает частоту наружного кольца.
            BearingFaultType.InnerRace => bpfi, // Возвращает частоту внутреннего кольца.
            BearingFaultType.RollingElement => bsf, // Возвращает частоту тела качения.
            BearingFaultType.Cage => ftf, // Возвращает частоту сепаратора.
            BearingFaultType.Unbalance => wheelRotationFrequency, // Возвращает частоту дисбаланса.
            BearingFaultType.Misalignment => 2d * wheelRotationFrequency, // Возвращает удвоенную частоту вращения для несоосности.
            _ => wheelRotationFrequency, // Возвращает запасной вариант для неизвестного дефекта.
        }; // Завершает switch-выражение.
    } // Завершает метод выбора частоты.

    /// <summary> // Документирует метод построения сводки.
    /// Формирует диагностическое описание параметров и результатов симуляции. // Уточняет назначение метода.
    /// </summary> // Завершает XML-документацию метода.
    /// <param name="settings">Настройки генерации сигнала.</param> // Документирует входные настройки.
    /// <param name="windowEndTimeSeconds">Время конца текущего окна.</param> // Документирует конец окна времени.
    /// <param name="kinematics">Кинематические параметры подшипника.</param> // Документирует кинематику узла.
    /// <param name="xAxis">Сигнал оси X.</param> // Документирует сигнал оси X.
    /// <param name="yAxis">Сигнал оси Y.</param> // Документирует сигнал оси Y.
    /// <param name="zAxis">Сигнал оси Z.</param> // Документирует сигнал оси Z.
    /// <returns>Текстовая диагностическая сводка.</returns> // Документирует возвращаемое значение.
    private static string BuildSummary( // Объявляет метод построения сводки.
        SignalGenerationSettings settings, // Принимает настройки генерации.
        double windowEndTimeSeconds, // Принимает конец отображаемого окна.
        BearingKinematics kinematics, // Принимает кинематические параметры узла.
        AxisSignalSeries xAxis, // Принимает сигнал оси X.
        AxisSignalSeries yAxis, // Принимает сигнал оси Y.
        AxisSignalSeries zAxis) // Принимает сигнал оси Z.
    { // Начинает тело метода построения сводки.
        string enabledFaults = string.Join(", ", settings.Faults.Where(static fault => fault.IsEnabled).Select(static fault => fault.DisplayName)); // Формирует список включённых дефектов.
        enabledFaults = string.IsNullOrWhiteSpace(enabledFaults) ? "дефекты не выбраны" : enabledFaults; // Подставляет текст по умолчанию при отсутствии дефектов.
        return string.Format( // Формирует текстовую сводку с инвариантной культурой.
            CultureInfo.InvariantCulture, // Использует инвариантную культуру форматирования.
            "Реальное время модели: {0:F2} с | Скорость: {1:F1} км/ч | Частота колеса: {2:F2} Гц | BPFO: {3:F2} Гц | BPFI: {4:F2} Гц | BSF: {5:F2} Гц | FTF: {6:F2} Гц | Пики XYZ: {7:F2} / {8:F2} / {9:F2} g | RMS XYZ: {10:F2} / {11:F2} / {12:F2} g | Активные дефекты: {13}", // Описывает шаблон итоговой сводки.
            windowEndTimeSeconds, // Передаёт модельное время конца окна.
            settings.WagonSpeedKilometersPerHour, // Передаёт скорость вагона.
            kinematics.WheelRotationFrequency, // Передаёт частоту вращения колеса.
            kinematics.Bpfo, // Передаёт частоту наружного кольца.
            kinematics.Bpfi, // Передаёт частоту внутреннего кольца.
            kinematics.Bsf, // Передаёт частоту тела качения.
            kinematics.Ftf, // Передаёт частоту сепаратора.
            xAxis.PeakAcceleration, // Передаёт пик оси X.
            yAxis.PeakAcceleration, // Передаёт пик оси Y.
            zAxis.PeakAcceleration, // Передаёт пик оси Z.
            xAxis.RootMeanSquare, // Передаёт RMS оси X.
            yAxis.RootMeanSquare, // Передаёт RMS оси Y.
            zAxis.RootMeanSquare, // Передаёт RMS оси Z.
            enabledFaults); // Передаёт список активных дефектов.
    } // Завершает метод построения сводки.

    /// <summary> // Документирует внутренний тип кинематики.
    /// Хранит рассчитанные характеристические частоты и коэффициент нагрузки. // Уточняет назначение внутреннего типа.
    /// </summary> // Завершает XML-документацию типа.
    /// <param name="WheelRotationFrequency">Частота вращения колеса.</param> // Документирует частоту вращения.
    /// <param name="Bpfi">Частота дефекта внутреннего кольца.</param> // Документирует частоту BPFI.
    /// <param name="Bpfo">Частота дефекта наружного кольца.</param> // Документирует частоту BPFO.
    /// <param name="Bsf">Частота дефекта тела качения.</param> // Документирует частоту BSF.
    /// <param name="Ftf">Частота дефекта сепаратора.</param> // Документирует частоту FTF.
    /// <param name="LoadScale">Коэффициент роста вибрации от нагрузки.</param> // Документирует коэффициент нагрузки.
    private sealed record BearingKinematics( // Объявляет тип кинематики подшипника.
        double WheelRotationFrequency, // Хранит частоту вращения колеса.
        double Bpfi, // Хранит частоту дефекта внутреннего кольца.
        double Bpfo, // Хранит частоту дефекта наружного кольца.
        double Bsf, // Хранит частоту дефекта тела качения.
        double Ftf, // Хранит частоту дефекта сепаратора.
        double LoadScale); // Хранит коэффициент нагрузки.

    /// <summary> // Документирует внутренний тип отсчёта.
    /// Хранит один рассчитанный трёхкоординатный отсчёт сигнала. // Уточняет назначение внутреннего типа.
    /// </summary> // Завершает XML-документацию типа.
    /// <param name="X">Ускорение по оси X.</param> // Документирует ускорение по оси X.
    /// <param name="Y">Ускорение по оси Y.</param> // Документирует ускорение по оси Y.
    /// <param name="Z">Ускорение по оси Z.</param> // Документирует ускорение по оси Z.
    private sealed record SignalSample( // Объявляет тип одного отсчёта сигнала.
        double X, // Хранит ускорение по оси X.
        double Y, // Хранит ускорение по оси Y.
        double Z); // Хранит ускорение по оси Z.
} // Завершает тело класса сервиса.




