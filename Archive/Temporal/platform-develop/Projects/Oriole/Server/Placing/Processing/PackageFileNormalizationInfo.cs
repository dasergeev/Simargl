using Apeiron.Platform.Databases.OrioleDatabase;
using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Apeiron.Oriole.Server.Processing;

/// <summary>
/// Представляет информацию для нормализации файла с пакетами данных.
/// </summary>
public class PackageFileNormalizationInfo
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="packageFile">
    /// Нормализуемый файл с пакетами данных.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="packageFile"/> передана пустая ссылка.
    /// </exception>
    private PackageFileNormalizationInfo(PackageFile packageFile)
    {
        //  Установка нормализуемого файла с пакетами данных.
        PackageFile = Check.IsNotNull(packageFile, nameof(packageFile));
    }

    /// <summary>
    /// Возвращает нормализуемый файл с пакетами данных.
    /// </summary>
    public PackageFile PackageFile { get; }

    /// <summary>
    /// Возвращает время начала записи в файл.
    /// </summary>
    public DateTime Time => PackageFile.Time;

    /// <summary>
    /// Возвращает количество пакетов в файле.
    /// </summary>
    public int PackageCount { get; private set; }

    /// <summary>
    /// Возвращает информацию о предыдущем файле.
    /// </summary>
    public PackageFileNormalizationInfo? Previous { get; private set; }

    /// <summary>
    /// Возвращает информацию о следующем файле.
    /// </summary>
    public PackageFileNormalizationInfo? Next { get; private set; }

    /// <summary>
    /// Возвращает значение, определяющее тип расположения файла в последовательности.
    /// </summary>
    public PackageFileLocationType LocationType { get; private set; }

    /// <summary>
    /// Возвращает длину синхронных сигналов.
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Возвращает индекс файла в последовательности.
    /// </summary>
    public int SequentialIndex { get; private set; }

    /// <summary>
    /// Возвращает первый синхромаркер в тактах.
    /// </summary>
    public long FirstSynchromarker { get; private set; }

    /// <summary>
    /// Возвращает последний синхромаркер в тактах.
    /// </summary>
    public long LastSynchromarker { get; private set; }

    /// <summary>
    /// Возвращает первый пакет.
    /// </summary>
    public Package? FirstPackage { get; private set; }

    /// <summary>
    /// Возвращает последний пакет.
    /// </summary>
    public Package? LastPackage { get; private set; }

    /// <summary>
    /// Возвращает частоту дискретизации.
    /// </summary>
    public double Sampling { get; private set; }

    /// <summary>
    /// Возвращает продолжительность синхронных сигналов в секундах.
    /// </summary>
    public double Duration { get; private set; }

    /// <summary>
    /// Возвращает нормализованное время начала файла.
    /// </summary>
    public DateTime NormalizedBeginTime { get; private set; }

    /// <summary>
    /// Возвращает нормализованное время конца файла.
    /// </summary>
    public DateTime NormalizedEndTime { get; private set; }

    /// <summary>
    /// Асинхронно нормализует файл.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая нормализацию файла.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task NormalizationAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка необходимости обновления файла.
        if (PackageFile.IsNormalized)
        {
            //  Установка значения, определяющего нормализована ли запись.
            PackageFile.IsNormalized = PackageFile.PackageCount != PackageCount ||
                PackageFile.Length != Length ||
                PackageFile.LocationType != LocationType ||
                PackageFile.SequentialIndex != SequentialIndex ||
                PackageFile.FirstSynchromarker != FirstSynchromarker ||
                PackageFile.LastSynchromarker != LastSynchromarker ||
                PackageFile.Sampling != Sampling ||
                PackageFile.Duration != Duration ||
                PackageFile.NormalizedBeginTime != NormalizedBeginTime ||
                PackageFile.NormalizedEndTime != NormalizedEndTime;
        }

        //  Установка полей файла.
        PackageFile.PackageCount = PackageCount;
        PackageFile.Length = Length;
        PackageFile.LocationType = LocationType;
        PackageFile.SequentialIndex = SequentialIndex;
        PackageFile.FirstSynchromarker = FirstSynchromarker;
        PackageFile.LastSynchromarker = LastSynchromarker;
        PackageFile.Sampling = Sampling;
        PackageFile.Duration = Duration;
        PackageFile.NormalizedBeginTime = NormalizedBeginTime;
        PackageFile.NormalizedEndTime = NormalizedEndTime;
    }

    /// <summary>
    /// Асинхронно обрывает последовательность на данном файле:
    /// предыдущий файл завершает последовательность,
    /// этот файл начинает новую последовательность.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, обрывающая последовательность.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async ValueTask CutOffAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка предыдущего файла.
        if (Previous is not null)
        {
            //  Корректировка значения, определяющего тип расположения файла в последовательности.
            switch (Previous.LocationType)
            {
                case PackageFileLocationType.First:
                    Previous.LocationType = PackageFileLocationType.Single;
                    break;
                case PackageFileLocationType.Internal:
                    Previous.LocationType = PackageFileLocationType.Last;
                    break;
                case PackageFileLocationType.Incorrect:
                case PackageFileLocationType.Single:
                case PackageFileLocationType.Last:
                default:
                    break;
            }
        }

        //  Проверка следующего файла.
        if (Next is not null)
        {
            //  Корректировка значения, определяющего тип расположения файла в последовательности.
            switch (Next.LocationType)
            {
                case PackageFileLocationType.Incorrect:
                    //  Установка значения, определяющего тип расположения файла в последовательности.
                    LocationType = PackageFileLocationType.Single;
                    break;
                case PackageFileLocationType.Internal:
                case PackageFileLocationType.Last:
                    //  Установка значения, определяющего тип расположения файла в последовательности.
                    LocationType = PackageFileLocationType.First;
                    break;
                case PackageFileLocationType.Single:
                case PackageFileLocationType.First:
                default:
                    break;
            }
        }
        else
        {
            //  Установка значения, определяющего тип расположения файла в последовательности.
            LocationType = PackageFileLocationType.Single;
        }
    }

    /// <summary>
    /// Асинхронно исключает файл из последовательности:
    /// предыдущий файл завершает последовательность,
    /// следующий файл начинает новую последовательность.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, исключающая файл из последовательности.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async Task ExcludeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Установка значения, определяющего тип расположения файла в последовательности.
        LocationType = PackageFileLocationType.Incorrect;

        //  Проверка предыдущего файла.
        if (Previous is not null)
        {
            //  Корректировка значения, определяющего тип расположения файла в последовательности.
            switch (Previous.LocationType)
            {
                case PackageFileLocationType.First:
                    Previous.LocationType = PackageFileLocationType.Single;
                    break;
                case PackageFileLocationType.Internal:
                    Previous.LocationType = PackageFileLocationType.Last;
                    break;
                case PackageFileLocationType.Incorrect:
                case PackageFileLocationType.Single:
                case PackageFileLocationType.Last:
                default:
                    break;
            }
        }

        //  Проверка следующего файла.
        if (Next is not null)
        {
            //  Корректировка значения, определяющего тип расположения файла в последовательности.
            switch (Next.LocationType)
            {
                case PackageFileLocationType.Internal:
                    Next.LocationType = PackageFileLocationType.First;
                    break;
                case PackageFileLocationType.Last:
                    Next.LocationType = PackageFileLocationType.Single;
                    break;
                case PackageFileLocationType.Incorrect:
                case PackageFileLocationType.Single:
                case PackageFileLocationType.First:
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Асинхронно нормализует файл, если он находится внутри последовательности.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, нормализующая файл.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async Task NormalizationIfInternalAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка ссылки на следующий файл.
        if (Next is not null)
        {
            //  Определение длительности файла.
            double duration = (Next.Time - Time).TotalSeconds;

            //  Установка нормализованного времени начала файла.
            NormalizedBeginTime = Time;

            //  Установка нормализованного времени конца файла.
            NormalizedEndTime = Next.Time;

            //  Установка продолжительности синхронных сигналов в секундах.
            Duration = duration;

            //  Установка частоты дискретизации.
            Sampling = Length / duration;
        }
    }

    /// <summary>
    /// Асинхронно нормализует файл, если он находится в конце последовательности.
    /// </summary>
    /// <param name="declaredSampling">
    /// Заявленная частота дискретизации.
    /// </param>
    /// <param name="allowableEndTime">
    /// Допустимое время окончания файла.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, нормализующая файл.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async Task NormalizationIfLastAsync(
        double declaredSampling, DateTime allowableEndTime, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка ссылки на следующий файл.
        if (Next is not null)
        {
            //  Определение длительности файла.
            double duration = (Next.Time - Time).TotalSeconds;

            //  Установка нормализованного времени начала файла.
            NormalizedBeginTime = Time;

            //  Установка нормализованного времени конца файла.
            NormalizedEndTime = Next.Time;

            //  Установка продолжительности синхронных сигналов в секундах.
            Duration = duration;

            //  Установка частоты дискретизации.
            Sampling = Length / duration;
        }
        else
        {
            //  Определение длительности файла.
            double duration = Length / declaredSampling;

            //  Установка нормализованного времени начала файла.
            NormalizedBeginTime = Time;

            //  Установка нормализованного времени конца файла.
            NormalizedEndTime = Time.AddSeconds(duration);

            //  Проверка допустимого времени.
            if (NormalizedEndTime > allowableEndTime)
            {
                //  Корректировка нормализованного времени конца файла.
                NormalizedEndTime = allowableEndTime;

                //  Корректировка длительности файла.
                duration = (NormalizedEndTime - NormalizedBeginTime).TotalSeconds;
            }

            //  Установка продолжительности синхронных сигналов в секундах.
            Duration = duration;

            //  Установка частоты дискретизации.
            Sampling = Length / duration;
        }
    }

    /// <summary>
    /// Асинхронно нормализует файл, если он находится в начале последовательности.
    /// </summary>
    /// <param name="allowableBeginTime">
    /// Допустимое время начала файла.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, нормализующая файл.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async Task NormalizationIfFirstAsync(DateTime allowableBeginTime, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка предыдущего файла.
        if (Previous is not null)
        {
            //  Проверка необходимости корректировки времени.
            if (Previous.LocationType == PackageFileLocationType.Last &&
                Previous.NormalizedEndTime > allowableBeginTime)
            {
                //  Корректировка допустимого времени начала файла.
                allowableBeginTime = Previous.NormalizedEndTime;
            }
        }

        //  Проверка ссылки на следующий файл.
        if (Next is not null)
        {
            //  Получение частоты дискретизации.
            double sampling = Next.Sampling;

            //  Определение длительности файла.
            double duration = Length / sampling;

            //  Установка нормализованного времени конца файла.
            NormalizedEndTime = Next.NormalizedBeginTime;

            //  Установка нормализованного времени начала файла.
            NormalizedBeginTime = NormalizedEndTime.AddSeconds(-duration);

            //  Проверка времени начала файла.
            if (NormalizedBeginTime < allowableBeginTime)
            {
                //  Установка нормализованного времени начала файла.
                NormalizedBeginTime = allowableBeginTime;

                //  Корректировка длительности файла.
                duration = (NormalizedEndTime - NormalizedBeginTime).TotalSeconds;
            }

            //  Установка продолжительности синхронных сигналов в секундах.
            Duration = duration;

            //  Установка частоты дискретизации.
            Sampling = Length / duration;
        }
    }

    /// <summary>
    /// Асинхронно нормализует файл, если он одиночный.
    /// </summary>
    /// <param name="declaredSampling">
    /// Заявленная частота дискретизации.
    /// </param>
    /// <param name="allowableBeginTime">
    /// Допустимое время начала файла.
    /// </param>
    /// <param name="allowableEndTime">
    /// Допустимое время конца файла.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, нормализующая файл.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async ValueTask NormalizationIfSingleAsync(
        double declaredSampling,
        DateTime allowableBeginTime, DateTime allowableEndTime,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка предыдущего файла.
        if (Previous is not null)
        {
            //  Проверка необходимости корректировки времени.
            if (Previous.LocationType == PackageFileLocationType.Last &&
                Previous.NormalizedEndTime > allowableBeginTime)
            {
                //  Корректировка допустимого времени начала файла.
                allowableBeginTime = Previous.NormalizedEndTime;
            }
        }

        //  Проверка следующего файла.
        if (Next is not null)
        {
            //  Проверка необходимости корректировки времени.
            if (Next.LocationType == PackageFileLocationType.First &&
                Next.NormalizedBeginTime < allowableEndTime)
            {
                //  Корректировка допустимого времени конца файла.
                allowableEndTime = Next.NormalizedBeginTime;
            }
        }

        //  Определение длительности файла.
        double duration = Length / declaredSampling;

        //  Предварительная оценка времени начала файла.
        DateTime beginTime = Time;

        //  Предварительная оценка времени окончания файла.
        DateTime endTime = beginTime.AddSeconds(duration);

        //  Нормализация времени.
        timeNormalization(ref beginTime, ref endTime);

        //  Обновление длительности файла.
        duration = (endTime - beginTime).TotalSeconds;

        //  Определение фактической частоты дискретизации.
        double sampling = Length / duration;

        //  Установка значений файла.
        NormalizedBeginTime = beginTime;
        NormalizedEndTime = endTime;
        Duration = duration;
        Sampling = sampling;

        //  Выполняет нормализацию времени.
        void timeNormalization(ref DateTime beginTime, ref DateTime endTime)
        {
            //  Проверка выхода времени окончания за допустимое значение.
            if (endTime > allowableEndTime)
            {
                //  Смещение времени начала.
                beginTime -= endTime - allowableEndTime;

                //  Смещение времени окончания.
                endTime = allowableEndTime;
            }

            //  Проверка выхода времени начала за допустимое значение.
            if (beginTime < allowableBeginTime)
            {
                //  Смещение времени начала.
                beginTime = allowableBeginTime;
            }
        }
    }

    /// <summary>
    /// Асинхронно загружает информацию для нормализации файлов с пакетами данных.
    /// </summary>
    /// <param name="registrarId">
    /// Идентификатор регистратора.
    /// </param>
    /// <param name="format">
    /// Формат данных.
    /// </param>
    /// <param name="beginTime">
    /// Время начала действия источника данных.
    /// </param>
    /// <param name="endTime">
    /// Время окончания действия источника данных.
    /// </param>
    /// <param name="declaredSampling">
    /// Заявленная частота дискретизации.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, асинхронно выполняющая загрузку файлов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<PackageFileNormalizationInfo[]> LoadInfosAsync(
        int registrarId,
        int format,
        DateTime beginTime,
        DateTime endTime,
        double declaredSampling,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос файлов, связанных с источником.
        PackageFile[] packageFiles = await RequestFilesAsync(
            registrarId, format, beginTime, endTime,
            cancellationToken).ConfigureAwait(false);

        //  Преобразование в массив информации о файлах.
        PackageFileNormalizationInfo[] normalizationInfos = await FromFilesAsync(
            packageFiles, cancellationToken).ConfigureAwait(false);

        //  Установка предварительных связей.
        await PreliminaryRelationsAsync(normalizationInfos, cancellationToken).ConfigureAwait(false);

        //  Анализ пакетов в файлах.
        await AnalysisPackagesAsync(normalizationInfos, declaredSampling,
            cancellationToken).ConfigureAwait(false);

        //  Анализ последовательности файлов.
        await AnalysisSequenceAsync(normalizationInfos, declaredSampling, cancellationToken).ConfigureAwait(false);

        //  Список некорректных файлов.
        List<PackageFileNormalizationInfo> incorrects = new();

        //  Список одиночных файлов.
        List<PackageFileNormalizationInfo> singles = new();

        //  Список первых файлов.
        List<PackageFileNormalizationInfo> firsts = new();

        //  Список внутренних файлов.
        List<PackageFileNormalizationInfo> internals = new();

        //  Список последних файлов.
        List<PackageFileNormalizationInfo> lasts = new();

        //  Перебор файлов.
        foreach (PackageFileNormalizationInfo normalizationInfo in normalizationInfos)
        {
            //  Анализ значения, определяющего тип расположения файла в последовательности.
            switch (normalizationInfo.LocationType)
            {
                case PackageFileLocationType.Incorrect:
                    //  Добавление файла в список неккоректных файлов.
                    incorrects.Add(normalizationInfo);
                    break;
                case PackageFileLocationType.Single:
                    //  Добавление файла в список одиночных файлов.
                    singles.Add(normalizationInfo);
                    break;
                case PackageFileLocationType.First:
                    //  Добавление файла в список первых файлов.
                    firsts.Add(normalizationInfo);
                    break;
                case PackageFileLocationType.Internal:
                    //  Добавление файла в список внутреннх файлов.
                    internals.Add(normalizationInfo);
                    break;
                case PackageFileLocationType.Last:
                    //  Добавление файла в список последних файлов.
                    lasts.Add(normalizationInfo);
                    break;
                default:
                    break;
            }
        }

        //  Нормализация внутренних файлов.
        foreach (PackageFileNormalizationInfo normalizationInfo in internals)
        {
            //  Нормализация внутреннего файла.
            await normalizationInfo.NormalizationIfInternalAsync(cancellationToken).ConfigureAwait(false);
        }

        //  Нормализация последних файлов.
        foreach (PackageFileNormalizationInfo normalizationInfo in lasts)
        {
            //  Нормализация последнего файла.
            await normalizationInfo.NormalizationIfLastAsync(declaredSampling, endTime, cancellationToken).ConfigureAwait(false);
        }

        //  Нормализация первых файлов.
        foreach (PackageFileNormalizationInfo normalizationInfo in firsts)
        {
            //  Нормализация первого файла.
            await normalizationInfo.NormalizationIfFirstAsync(endTime, cancellationToken).ConfigureAwait(false);
        }

        //  Нормализация одиночных файлов.
        foreach (PackageFileNormalizationInfo normalizationInfo in singles)
        {
            //  Нормализация одиночного файла.
            await normalizationInfo.NormalizationIfSingleAsync(
                declaredSampling, beginTime, endTime, cancellationToken).ConfigureAwait(false);
        }

        //  Возврат массива информации для нормализации файлов с пакетами данных.
        return normalizationInfos;
    }

    /// <summary>
    /// Асинхронно создаёт информацию для нормализации файла с пакетами данных.
    /// </summary>
    /// <param name="packageFile">
    /// Нормализуемый файл с пакетами данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая информацию для нормализации файла с пакетами данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<PackageFileNormalizationInfo> FromPackageFileAsync(
        PackageFile packageFile, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание информации для нормализации файла с пакетами данных.
        PackageFileNormalizationInfo normalizationInfo = new(packageFile);

        //  Возврат созданной информации для нормализации файла с пакетами данных.
        return normalizationInfo;
    }

    /// <summary>
    /// Асинхронно запрашивает файлы с пакетами данных.
    /// </summary>
    /// <param name="registrarId">
    /// Идентификатор регистратора.
    /// </param>
    /// <param name="format">
    /// Формат данных.
    /// </param>
    /// <param name="beginTime">
    /// Время начала действия источника данных.
    /// </param>
    /// <param name="endTime">
    /// Время окончания действия источника данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, асинхронно выполняющая загрузку файлов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<PackageFile[]> RequestFilesAsync(
        int registrarId,
        int format,
        DateTime beginTime,
        DateTime endTime,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Поиск первого ненормализованного файла.
        PackageFile? firstFile = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) =>
            await database.PackageFiles
                .Where(packageFile => packageFile.IsLoaded &&
                    !packageFile.IsNormalized &&
                    packageFile.RawDirectory.RegistrarId == registrarId &&
                    packageFile.Format == format &&
                    beginTime <= packageFile.Time &&
                    packageFile.Time < endTime)
                .OrderBy(packageFile => packageFile.Time)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false), cancellationToken).ConfigureAwait(false);

        //  Проверка найденного файла.
        if (firstFile is null)
        {
            //  Возврат пустого массива.
            return Array.Empty<PackageFile>();
        }

        //  Запрос файлов, связанных с источником.
        return await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) =>
            await database.PackageFiles
                .Where(packageFile => packageFile.IsLoaded &&
                    packageFile.RawDirectory.RegistrarId == registrarId &&
                    packageFile.Format == format &&
                    firstFile.Time <= packageFile.Time &&
                    packageFile.Time < endTime)
                .OrderBy(packageFile => packageFile.Time)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно преобразовывает массив файлов в массив информации о файлах.
    /// </summary>
    /// <param name="packageFiles">
    /// Массив файлов.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая преобразование.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<PackageFileNormalizationInfo[]> FromFilesAsync(
        PackageFile[] packageFiles, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Определение количества файлов.
        int count = packageFiles.Length;

        //  Проверка наличия файлов.
        if (count == 0)
        {
            //  Возврат пустого массива.
            return Array.Empty<PackageFileNormalizationInfo>();
        }

        //  Создание списка информации для нормализации файлов с пакетами данных.
        List<PackageFileNormalizationInfo> normalizationInfos = new();

        //  Перебор файлов с пакетами данных.
        foreach (PackageFile packageFile in packageFiles)
        {
            //  Получение информации о файле.
            PackageFileNormalizationInfo normalizationInfo = await FromPackageFileAsync(
                packageFile, cancellationToken).ConfigureAwait(false);

            //  Добавление информации в список.
            normalizationInfos.Add(normalizationInfo);
        }

        //  Возврат массива информации для нормализации файлов с пакетами данных.
        return normalizationInfos.ToArray();
    }

    /// <summary>
    /// Асинхронно устанавливает предварительный связи.
    /// </summary>
    /// <param name="normalizationInfos">
    /// Массив информации для нормализации файлов с пакетами данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая установку связей.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task PreliminaryRelationsAsync(
        PackageFileNormalizationInfo[] normalizationInfos,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Определение количества элементов.
        int count = normalizationInfos.Length;

        //  Проверка наличия информации.
        if (count == 0)
        {
            //  Нет информации для разбора.
            return;
        }

        //  Проверка одиночного файла.
        if (count == 1)
        {
            //  Получение информации о файле.
            PackageFileNormalizationInfo normalizationInfo = normalizationInfos[0];

            //  Установка значения, определяющего тип расположения файла в последовательности.
            normalizationInfo.LocationType = PackageFileLocationType.Single;

            //  Установка связей файла.
            normalizationInfo.Next = null;
            normalizationInfo.Previous = null;

            //  Предварительный связи установлены.
            return;
        }

        //  Предыдущий файл.
        PackageFileNormalizationInfo previousNormalizationInfo = normalizationInfos[0];

        //  Установка значения, определяющего тип расположения файла в последовательности.
        previousNormalizationInfo.LocationType = PackageFileLocationType.First;

        //  Перебор остальных элементов массива.
        for (int i = 1; i < count; i++)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение информации о файле.
            PackageFileNormalizationInfo normalizationInfo = normalizationInfos[i];

            //  Установка значения, определяющего тип расположения файла в последовательности.
            normalizationInfo.LocationType = PackageFileLocationType.Internal;

            //  Установка связей файла.
            previousNormalizationInfo.Next = normalizationInfo;
            normalizationInfo.Previous = previousNormalizationInfo;

            //  Установка предыдущего файла.
            previousNormalizationInfo = normalizationInfo;
        }

        //  Установка значения, определяющего тип расположения последнего файла в последовательности.
        previousNormalizationInfo.LocationType = PackageFileLocationType.Last;
    }

    /// <summary>
    /// Асинхронно выполняет анализ пакетов в файлах.
    /// </summary>
    /// <param name="normalizationInfos">
    /// Массив информации для нормализации файлов с пакетами данных.
    /// </param>
    /// <param name="declaredSampling">
    /// Заявленная частота дискретизации.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая анализ пакетов в файлах.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task AnalysisPackagesAsync(
        PackageFileNormalizationInfo[] normalizationInfos,
        double declaredSampling,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Перебор информации для нормализации файлов с пакетами данных.
        foreach (PackageFileNormalizationInfo normalizationInfo in normalizationInfos)
        {
            //  Загрузка пакетов.
            Package[] packages = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) =>
                await database.Packages
                .Where(package => package.RawDirectoryId == normalizationInfo.PackageFile.RawDirectoryId &&
                    package.Format == normalizationInfo.PackageFile.Format &&
                    package.FileTime == normalizationInfo.Time)
                .OrderBy(package => package.FileOffset)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

            //  Определение количества пакетов.
            int packageCount = packages.Length;

            //  Проверка количества пакетов.
            if (packageCount > 0)
            {
                //  Установка синхромаркеров файла.
                normalizationInfo.FirstSynchromarker = packages[0].Synchromarker;
                normalizationInfo.LastSynchromarker = packages[packageCount - 1].Synchromarker;

                //  Установка пакетов файла.
                normalizationInfo.FirstPackage = packages[0];
                normalizationInfo.LastPackage = packages[packageCount - 1];

                //  Длина синхронных сигналов.
                int length = 0;

                //  Предыдущий пакет.
                Package? previousPackage = null;

                //  Перебор всех пакетов.
                foreach (Package package in packages)
                {
                    //  Корректировка длины синхронных сигналов.
                    length += package.Length;

                    //  Проверка ссылки на предыдущий пакет.
                    if (previousPackage is not null)
                    {
                        //  Проверка обрыва последовательности пакетов.
                        if (!PackagesProcessing.IsPackagesChain(previousPackage, package, declaredSampling))
                        {
                            //  Исключение файла из последовательности.
                            await normalizationInfo.ExcludeAsync(cancellationToken).ConfigureAwait(false);
                        }
                    }

                    //  Установка предыдущего пакета.
                    previousPackage = package;
                }

                //  Установка длины синхронных сигналов.
                normalizationInfo.Length = length;
            }
            else
            {
                //  Установка длины синхронных сигналов.
                normalizationInfo.Length = 0;

                //  Установка синхромаркеров файла.
                normalizationInfo.FirstSynchromarker = -1;
                normalizationInfo.LastSynchromarker = -1;

                //  Установка пакетов файла.
                normalizationInfo.FirstPackage = null;
                normalizationInfo.LastPackage = null;

                //  Исключение файла из последовательности.
                await normalizationInfo.ExcludeAsync(cancellationToken).ConfigureAwait(false);
            }

            //  Установка количества пакетов.
            normalizationInfo.PackageCount = packageCount;
        }
    }

    /// <summary>
    /// Асинхронно выполняет анализ последовательности файлов.
    /// </summary>
    /// <param name="normalizationInfos">
    /// Массив информации для нормализации файлов с пакетами данных.
    /// </param>
    /// <param name="declaredSampling">
    /// Заявленная частота дискретизации.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая анализ последовательности файлов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task AnalysisSequenceAsync(
        PackageFileNormalizationInfo[] normalizationInfos,
        double declaredSampling,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Определение количества элементов в массиве.
        int count = normalizationInfos.Length;

        //  Проверка наличия элементов.
        if (count == 0)
        {
            //  Нет элементов для анализа.
            return;
        }

        //  Информация о предыдущем файле.
        PackageFileNormalizationInfo previousInfo = normalizationInfos[0];

        //  Установка индекса файла в последовательности.
        previousInfo.SequentialIndex = 0;

        //  Проверка последовательности из одного файла.
        if (count == 1)
        {
            //  Последовательность из одного файла.
            return;
        }

        //  Индекс файла в последовательности.
        int sequentialIndex = 0;

        //  Перебор элементов.
        for (int i = 1; i < count; i++)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение текущего элемента.
            PackageFileNormalizationInfo normalizationInfo = normalizationInfos[i];

            //  Проверка обрыва цепочки.
            if (previousInfo.LastPackage is not null &&
                normalizationInfo.FirstPackage is not null &&
                !PackagesProcessing.IsPackagesChain(
                    previousInfo.LastPackage, normalizationInfo.FirstPackage,
                    declaredSampling))
            {
                //  Обрыв цепочки.
                await normalizationInfo.CutOffAsync(cancellationToken).ConfigureAwait(false);
            }

            //  Корректировка индекса файла в последовательности.
            sequentialIndex = normalizationInfo.LocationType switch
            {
                PackageFileLocationType.Incorrect or PackageFileLocationType.Single or PackageFileLocationType.First => 0,
                PackageFileLocationType.Internal or PackageFileLocationType.Last => sequentialIndex + 1,
                _ => sequentialIndex,
            };

            //  Установка индекса файла в последовательности.
            normalizationInfo.SequentialIndex = sequentialIndex;

            //  Установка информации о предыдущем файле.
            previousInfo = normalizationInfo;
        }
    }
}
