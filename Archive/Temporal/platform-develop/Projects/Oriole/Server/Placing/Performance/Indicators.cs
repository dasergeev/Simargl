using Apeiron.Platform.Databases.OrioleDatabase;
using System.Diagnostics;
using System.Text;

namespace Apeiron.Oriole.Server.Performance;

/// <summary>
/// Представляет показатели эффективности.
/// </summary>
public readonly struct Indicators
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="database">
    /// Контекст сеанса работы с базой данных.
    /// </param>
    /// <param name="previous">
    /// Предыдущие показатели.
    /// </param>
    public Indicators(
        [ParameterNoChecks] OrioleDatabaseContext database,
        //[ParameterNoChecks] DatabaseBalancer balancer,
        [ParameterNoChecks] Indicators previous)
    {
        //  Получение текущего процесса.
        Process process = Process.GetCurrentProcess();

        //  Получение количества потоков.
        ThreadCount = new(ThreadPool.ThreadCount, previous.ThreadCount);

        //  Получение объёма используемой памяти.
        MemorySize = new((int)(process.PrivateMemorySize64 / (1024 * 1024)), previous.MemorySize);

        //  Получение количества файлов с пакетами в базе данных.
        AllPackageFilesCount = new(database.PackageFiles.Count(), previous.AllPackageFilesCount);
        
        //  Получение количества загруженных файлов с пакетами в базе данных.
        LoadedPackageFilesCount = new(
            database.PackageFiles.Where(packageFile => packageFile.IsLoaded).Count(),
            previous.LoadedPackageFilesCount);

        //  Получение количества нормализованных файлов с пакетами в базе данных.
        NormalizedPackageFilesCount = new(
            database.PackageFiles.Where(packageFile => packageFile.IsNormalized).Count(),
            previous.NormalizedPackageFilesCount);

        //  Получение количества проанализированных файлов с пакетами в базе данных.
        AnalyzedPackageFilesCount = new(
            database.PackageFiles.Where(packageFile => packageFile.IsAnalyzed).Count(),
            previous.AnalyzedPackageFilesCount);

        //  Получение количества файлов с NMEA сообщениями в базе данных.
        AllNmeaFilesCount = new(database.NmeaFiles.Count(), previous.AllNmeaFilesCount);

        //  Получение количества загруженных файлов с NMEA сообщениями в базе данных.
        LoadedNmeaFilesCount = new(
            database.NmeaFiles.Where(nmeaFile => nmeaFile.IsLoaded).Count(),
            previous.LoadedNmeaFilesCount);

        //  Получение количества сообщений GPS, содержащих данные местоположения.
        AllGgaMessagesCount = new(database.GgaMessages.Count(), previous.AllGgaMessagesCount);

        //  Получение количества проанализированных сообщений GPS, содержащих данные местоположения.
        AnalyzedGgaMessagesCount = new(
            database.GgaMessages.Where(ggaMessage => ggaMessage.IsAnalyzed).Count(),
            previous.AnalyzedGgaMessagesCount);

        //  Получение количества сообщений GPS, содержащих минимальный рекомендованный набор данных.
        AllRmcMessagesCount = new(database.RmcMessages.Count(), previous.AllRmcMessagesCount);

        //  Получение количества проанализированных сообщений GPS, содержащих минимальный рекомендованный набор данных.
        AnalyzedRmcMessagesCount = new(
            database.RmcMessages.Where(rmcMessage => rmcMessage.IsAnalyzed).Count(),
            previous.AnalyzedRmcMessagesCount);

        //  Получение количества сообщений GPS, содержащих данные о наземном курсе и скорости.
        AllVtgMessagesCount = new(database.VtgMessages.Count(), previous.AllVtgMessagesCount);

        //  Получение количества проанализированных сообщений GPS, содержащих данные о наземном курсе и скорости.
        AnalyzedVtgMessagesCount = new(
            database.VtgMessages.Where(vtgMessage => vtgMessage.IsAnalyzed).Count(),
            previous.AnalyzedVtgMessagesCount);

        //  Получение количества геолокационных данных.
        AllGeolocationsCount = new(database.Geolocations.Count(), previous.AllGeolocationsCount);

        //  Получение количества проанализированных геолокационных данных.
        AnalyzedGeolocationsCount = new(
            database.Geolocations.Where(geolocation => geolocation.IsAnalyzed).Count(),
            previous.AnalyzedGeolocationsCount);

        //  Получение количества кадров.
        AllFramesCount = new(database.Frames.Count(), previous.AllFramesCount);

        //  Получение количества проанализированных кадров.
        SpectrumFramesCount = new(
            database.Frames.Where(frame => frame.IsSpectrum).Count(),
            previous.SpectrumFramesCount);
        StatisticFramesCount = new(
            database.Frames.Where(frame => frame.IsStatistic).Count(),
            previous.StatisticFramesCount);
        ExtremumFramesCount = new(
            database.Frames.Where(frame => frame.IsExtremum).Count(),
            previous.ExtremumFramesCount);

        //  Установка времени получения индикаторов.
        Time = DateTime.Now;

        //  Установка интервала, за который изменились индикаторы.
        Interval = Time - previous.Time;
    }

    /// <summary>
    /// Возвращает время получения индикаторов.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Возвращает интервал, за который изменились индикаторы.
    /// </summary>
    public TimeSpan Interval { get; }

    /// <summary>
    /// Возвращает количество потоков.
    /// </summary>
    public Indicator<int> ThreadCount { get; }

    /// <summary>
    /// Возвращает объём используемой памяти.
    /// </summary>
    public Indicator<int> MemorySize { get; }

    /// <summary>
    /// Возвращает общее количество файлов с пакетами в базе данных.
    /// </summary>
    public Indicator<int> AllPackageFilesCount { get; }

    /// <summary>
    /// Возвращает количество загруженных файлов с пакетами в базе данных.
    /// </summary>
    public Indicator<int> LoadedPackageFilesCount { get; }

    /// <summary>
    /// Возвращает количество нормализованных файлов с пакетами в базе данных.
    /// </summary>
    public Indicator<int> NormalizedPackageFilesCount { get; }

    /// <summary>
    /// Возвращает количество проанализированных файлов с пакетами в базе данных.
    /// </summary>
    public Indicator<int> AnalyzedPackageFilesCount { get; }

    /// <summary>
    /// Возвращает общее количество файлов с NMEA сообщениями в базе данных.
    /// </summary>
    public Indicator<int> AllNmeaFilesCount { get; }

    /// <summary>
    /// Возвращает количество загруженных файлов с NMEA сообщениями в базе данных.
    /// </summary>
    public Indicator<int> LoadedNmeaFilesCount { get; }

    /// <summary>
    /// Возвращает общее количество сообщений GPS, содержащих данные местоположения.
    /// </summary>
    public Indicator<int> AllGgaMessagesCount { get; }

    /// <summary>
    /// Возвращает количество проанализированных сообщений GPS, содержащих данные местоположения.
    /// </summary>
    public Indicator<int> AnalyzedGgaMessagesCount { get; }

    /// <summary>
    /// Возвращает общее количество сообщений GPS, содержащих минимальный рекомендованный набор данных.
    /// </summary>
    public Indicator<int> AllRmcMessagesCount { get; }

    /// <summary>
    /// Возвращает количество проанализированных сообщений GPS, содержащих минимальный рекомендованный набор данных.
    /// </summary>
    public Indicator<int> AnalyzedRmcMessagesCount { get; }

    /// <summary>
    /// Возвращает общее количество сообщений GPS, содержащих данные о наземном курсе и скорости.
    /// </summary>
    public Indicator<int> AllVtgMessagesCount { get; }

    /// <summary>
    /// Возвращает количество проанализированных сообщений GPS, содержащих данные о наземном курсе и скорости.
    /// </summary>
    public Indicator<int> AnalyzedVtgMessagesCount { get; }

    /// <summary>
    /// Возвращает общее количество геолокационных данных.
    /// </summary>
    public Indicator<int> AllGeolocationsCount { get; }

    /// <summary>
    /// Возвращает количество проанализированных геолокационных данных.
    /// </summary>
    public Indicator<int> AnalyzedGeolocationsCount { get; }

    /// <summary>
    /// Возвращает общее количество кадров.
    /// </summary>
    public Indicator<int> AllFramesCount { get; }

    /// <summary>
    /// Возвращает количество проанализированных кадров для спестов.
    /// </summary>
    public Indicator<int> SpectrumFramesCount { get; }

    /// <summary>
    /// Возвращает количество проанализированных кадров для статистики.
    /// </summary>
    public Indicator<int> StatisticFramesCount { get; }

    /// <summary>
    /// Возвращает количество проанализированных кадров для Экстремумов.
    /// </summary>
    public Indicator<int> ExtremumFramesCount { get; }

    /// <summary>
    /// Возвращает текстовое представление объекта.
    /// </summary>
    /// <returns>
    /// Текстовое представление объекта.
    /// </returns>
    public override string ToString()
    {
        //  Создание построителя строки.
        StringBuilder builder = new();

        builder.Append("Производительность:");
        builder.Append(Environment.NewLine);

        builder.Append($"  - память:      {MemorySize.Stable()} (МБ)");
        builder.Append(Environment.NewLine);

        builder.Append($"  - потоков:     {ThreadCount.Stable()}");
        builder.Append(Environment.NewLine);

        builder.Append("Файлы с пакетами:");
        builder.Append(Environment.NewLine);

        builder.Append($"  - всего:             {AllPackageFilesCount.Stable()}");
        builder.Append(Environment.NewLine);

        builder.Append($"  - загружено:         {LoadedPackageFilesCount.Process(AllPackageFilesCount, Interval)}");
        builder.Append(Environment.NewLine);

        builder.Append($"  - нормализовано:     {NormalizedPackageFilesCount.Process(AllPackageFilesCount, Interval)}");
        builder.Append(Environment.NewLine);

        builder.Append($"  - проанализировано:  {AnalyzedPackageFilesCount.Process(AllPackageFilesCount, Interval)}");
        builder.Append(Environment.NewLine);

        builder.Append("Файлы с NMEA:");
        builder.Append(Environment.NewLine);

        builder.Append($"  - всего:         {AllNmeaFilesCount.Stable()}");
        builder.Append(Environment.NewLine);

        builder.Append($"  - загружено:     {LoadedNmeaFilesCount.Process(AllNmeaFilesCount, Interval)}");
        builder.Append(Environment.NewLine);

        builder.Append("Сообщения GPS:");
        builder.Append(Environment.NewLine);

        builder.Append("  GGA:");
        builder.Append(Environment.NewLine);

        builder.Append($"    - всего:               {AllGgaMessagesCount.Stable()}");
        builder.Append(Environment.NewLine);

        builder.Append($"    - проанализировано:    {AnalyzedGgaMessagesCount.Process(AllGgaMessagesCount, Interval)}");
        builder.Append(Environment.NewLine);

        builder.Append("  RMC:");
        builder.Append(Environment.NewLine);

        builder.Append($"    - всего:               {AllRmcMessagesCount.Stable()}");
        builder.Append(Environment.NewLine);

        builder.Append($"    - проанализировано:    {AnalyzedRmcMessagesCount.Process(AllRmcMessagesCount, Interval)}");
        builder.Append(Environment.NewLine);

        builder.Append("  VTG:");
        builder.Append(Environment.NewLine);

        builder.Append($"    - всего:               {AllVtgMessagesCount.Stable()}");
        builder.Append(Environment.NewLine);

        builder.Append($"    - проанализировано:    {AnalyzedVtgMessagesCount.Process(AllVtgMessagesCount, Interval)}");
        builder.Append(Environment.NewLine);

        builder.Append("Геолокационные данные:");
        builder.Append(Environment.NewLine);

        builder.Append($"  - всего:             {AllGeolocationsCount.Stable()}");
        builder.Append(Environment.NewLine);

        builder.Append($"  - проанализировано:  {AnalyzedGeolocationsCount.Process(AllGeolocationsCount, Interval)}");
        builder.Append(Environment.NewLine);

        builder.Append("Кадры:");
        builder.Append(Environment.NewLine);

        builder.Append($"  - всего:         {AllFramesCount.Stable()}");
        builder.Append(Environment.NewLine);

        builder.Append($"  - спектров:      {SpectrumFramesCount.Process(AllFramesCount, Interval)}");
        builder.Append(Environment.NewLine);

        builder.Append($"  - статистика:    {StatisticFramesCount.Process(AllFramesCount, Interval)}");
        builder.Append(Environment.NewLine);

        builder.Append($"  - экстремумы:    {ExtremumFramesCount.Process(AllFramesCount, Interval)}");
        builder.Append(Environment.NewLine);

        //  Возврат тектового представления.
        return builder.ToString();
    }
}
