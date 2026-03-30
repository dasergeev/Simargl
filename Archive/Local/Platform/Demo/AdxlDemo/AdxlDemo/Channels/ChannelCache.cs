using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Primitives;
using Apeiron.Threading;
using System.Collections.Concurrent;
using System.IO;

namespace Apeiron.Platform.Demo.AdxlDemo.Channels;

/// <summary>
/// Представляет кэш канала.
/// </summary>
public sealed class ChannelCache :
    Element
{
    /// <summary>
    /// Поле для хранения карты фрагментов канала.
    /// </summary>
    private readonly ConcurrentDictionary<long, ChannelFragment> _Map;

    /// <summary>
    /// Поле для хранения списка идентификаторов.
    /// </summary>
    private readonly List<long> _FragmentIDs;

    /// <summary>
    /// Поле для хранения примитива асинхронной блокировки.
    /// </summary>
    private readonly AsyncLock _Lock;

    /// <summary>
    /// Поле для хранения информации о канале.
    /// </summary>
    private readonly ChannelInfo _ChannelInfo;

    /// <summary>
    /// Поле для хранения активного фрагмента.
    /// </summary>
    private ChannelFragment? _ActiveFragment;

    /// <summary>
    /// Поле для хранения списка последних фрагментов.
    /// </summary>
    private readonly List<ChannelFragment> _LastFragments;

    /// <summary>
    /// Поле для хранения коллекции информации о фрагментах.
    /// </summary>
    private readonly List<ChannelFragmentInfo> _ChannelFragmentInfos;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="channelInfo">
    /// Информация о канале.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="channelInfo"/> передана пустая ссылка.
    /// </exception>
    public ChannelCache(Engine engine, ChannelInfo channelInfo) :
        base(engine)
    {
        //  Установка информации о канале.
        _ChannelInfo = IsNotNull(channelInfo, nameof(channelInfo));

        //  Создание карты фрагментов канала.
        _Map = new();

        //  Создание списка идентификаторов.
        _FragmentIDs = new();

        //  Создание примитива асинхронной блокировки.
        _Lock = new();

        //  Установка активного фрагмента.
        _ActiveFragment = null;

        //  Создание списка последних фрагментов.
        _LastFragments = new();

        //  Получение минимального времени.
        MinTime = _ChannelInfo.ChannelFragmentInfos.Count > 0 ? _ChannelInfo.ChannelFragmentInfos.Select(fragment => fragment.BeginTime).Min() : DateTime.Now;

        //  Создание коллекции информации о фрагментах.
        _ChannelFragmentInfos = new(_ChannelInfo.ChannelFragmentInfos);
    }

    /// <summary>
    /// Возвращает минимальное время данных.
    /// </summary>
    public DateTime MinTime { get; }

    /// <summary>
    /// Асинхронно возвращает данные.
    /// </summary>
    /// <param name="beginTime">
    /// Время начала данных.
    /// </param>
    /// <param name="duration">
    /// Длительность данных.
    /// </param>
    /// <param name="xOffset">
    /// Смещение значений по оси Ox.
    /// </param>
    /// <param name="color">
    /// Цвет линии.
    /// </param>
    /// <param name="width">
    /// Ширина линии.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая данные.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<PolylineCollection> GetDataAsync(DateTime beginTime, TimeSpan duration,
        double xOffset, System.Drawing.Color color, double width, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блокировка критического объекта.
        using AsyncLocking locking = await _Lock.LockAsync(cancellationToken).ConfigureAwait(false);

        //  Время окончания запрашиваемых данных.
        DateTime endTime = beginTime + duration;

        //  Информация о фрагментах.
        IEnumerable<ChannelFragmentInfo>? infos = null;

        ////  Блокировка информации о канале.
        //lock (_ChannelInfo)
        //{
        //    //  Поиск информации.
        //    infos = _ChannelInfo.ChannelFragmentInfos
        //        .Where(fragment => fragment.BeginTime < endTime && beginTime < fragment.EndTime);
        //}

        //  Блокировка информации о канале.
        lock (_ChannelFragmentInfos)
        {
            //  Поиск информации.
            infos = _ChannelFragmentInfos
                .Where(fragment => fragment.BeginTime < endTime && beginTime < fragment.EndTime);
        }

        //  Создание списка фрагментов.
        ConcurrentBag<ChannelFragment> fragments = new();

        //  Асинхронный поиск фрагментов.
        await Parallel.ForEachAsync(
            infos,
            cancellationToken,
            async (info, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Безопасное выполнение.
                await Invoker.CriticalAsync(async (cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Получение фрагмента.
                    ChannelFragment fragment = _Map.GetOrAdd(info.ID, id =>
                    {
                        //  Блокировка списка идентификаторов.
                        lock (_FragmentIDs)
                        {
                            //  Добавление в список идентификаторов.
                            _FragmentIDs.Add(info.ID);
                        }

                        //  Загрузка фрагмента.
                        return new(info);
                    });

                    //  Добавление фрагмента.
                    fragments.Add(fragment);
                }, cancellationToken).ConfigureAwait(false);
            });
        
        //  Проверка активного фрагмента.
        if (_ActiveFragment is not null && _ActiveFragment.BeginTime < endTime && beginTime < _ActiveFragment.EndTime)
        {
            //  Добавление активного фрагмента в список.
            fragments.Add(_ActiveFragment);
        }

        //  Блокировка критического объекта.
        lock (_LastFragments)
        {
            //  Поиск в списке последних фрагментов.
            foreach (ChannelFragment fragment in _LastFragments.Where(fragment => fragment.BeginTime < endTime && beginTime < fragment.EndTime))
            {
                //  Добавление фрагмента в список.
                fragments.Add(fragment);
            }
        }

        //  Чистка старых данных.
        while (_FragmentIDs.Count > Settings.ChannelCacheSize)
        {
            //  Получение первого идентификатора.
            long id = _FragmentIDs[0];

            //  Удаление из карты.
            _Map.TryRemove(id, out ChannelFragment _);

            //  Удаление идентификатора.
            _FragmentIDs.RemoveAt(0);
        }

        //  Создание коллекции ломаных линий.
        PolylineCollection polylines = new();

        //  Перебор найденных фрагментов.
        foreach (ChannelFragment fragment in fragments)
        {
            //  Создание ломаной линии.
            Polyline polyline = new(fragment.GetData(), fragment.Sampling,
                (fragment.BeginTime - beginTime).TotalSeconds + xOffset, color, width);

            //  Добавление линии в коллекцию.
            polylines.Add(polyline);
        }

        //  Возврат коллекции ломаных линий.
        return polylines;
    }

    /// <summary>
    /// Асинхронно добавляет данные.
    /// </summary>
    /// <param name="sampling">
    /// Частота дискретизации.
    /// </param>
    /// <param name="cutoff">
    /// Частота среза фильтра.
    /// </param>
    /// <param name="buffer">
    /// Буфер значений.
    /// </param>
    /// <param name="beginTime">
    /// Время начала записи данных.
    /// </param>
    /// <param name="duration">
    /// Длительность записи.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая добавление данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task AddDataAsync(
        double sampling, double cutoff, double[] buffer,
        DateTime beginTime, TimeSpan duration, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блокировка критического объекта.
        using AsyncLocking locking = await _Lock.LockAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка активного фрагмента.
        if (_ActiveFragment is not null)
        {
            //  Определение расхождения.
            TimeSpan deviation = beginTime - _ActiveFragment.EndTime;

            //  Проверка возможности продолжения фрагмента.
            if (_ActiveFragment.Sampling != sampling ||
                _ActiveFragment.Cutoff != cutoff ||
                Math.Abs(deviation.TotalSeconds) > 5 / sampling)
            {
                //  Запечатывание текущего фрагмента.
                await SealActiveAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        //  Создание нового фрагмента при необходимости.
        _ActiveFragment ??= new(sampling, cutoff, beginTime);

        //  Добавление данных в фрагмент.
        await _ActiveFragment.AddDataAsync(buffer, beginTime, duration, cancellationToken).ConfigureAwait(false);

        //  Проверка длины фрагмента.
        if (_ActiveFragment.Duration.TotalSeconds >= Engine.Application.Settings.FragmentDuration)
        {
            //  Запечатывание текущего фрагмента.
            await SealActiveAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно запечатывает активный фрагмент.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, запечатывающая активный фрагмент.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task SealActiveAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка активного фрагмента.
        if (_ActiveFragment is not null)
        {
            //  Получение активного фрагмента.
            ChannelFragment activeFragment = _ActiveFragment;

            //  Блокировка критического объекта.
            lock (_LastFragments)
            {
                //  Добавление фрагмента в список последних фрагментов.
                _LastFragments.Add(activeFragment);
            }

            //  Запуск асинхронной операции с фрагентом.
            _ = Task.Run(async delegate
            {
                //  Проверка длины фрагмента.
                if (activeFragment.Length > 0)
                {
                    //  Запечатывание текущего фрагмента.
                    await activeFragment.SealAsync(cancellationToken).ConfigureAwait(false);

                    //  Формирование пути к файлу с данными.
                    FileInfo file = new(Path.Combine(Settings.DataPath,
                        ChannelFragmentInfo.CreatePath(_ChannelInfo.ID, activeFragment.BeginTime)));

                    //  Информация о фрагменте.
                    ChannelFragmentInfo? fragmentInfo = null;

                    //  Безопасное выполнение.
                    await Invoker.CriticalAsync(async (cancellationToken) =>
                    {
                        //  Проверка каталога.
                        if (!file.Directory!.Exists)
                        {
                            //  Создание каталога.
                            file.Directory.Create();
                        }

                        //  Сохранение данных в файл.
                        await File.WriteAllBytesAsync(file.FullName, activeFragment.GetBinary(), cancellationToken).ConfigureAwait(false);

                        //  Получение статистики.
                        ChannelStatistics statistics = activeFragment.GetStatistics();

                        //  Блокировка информации о канале.
                        lock (_ChannelInfo)
                        {
                            //  Выполнение транзакции.
                            ContextManager.Transaction(context =>
                            {
                                //  Создание информации о фрагменте.
                                fragmentInfo = new()
                                {
                                    ChannelInfo = _ChannelInfo,
                                    Path = file.FullName,
                                    Sampling = activeFragment.Sampling,
                                    Cutoff = activeFragment.Cutoff,
                                    BeginTime = activeFragment.BeginTime,
                                    EndTime = activeFragment.EndTime,
                                    MinValue = statistics.MinValue,
                                    MaxValue = statistics.MaxValue,
                                    Average = statistics.Average,
                                    Deviation = statistics.Deviation,
                                    Count = statistics.Count,
                                    Sum = statistics.Sum,
                                    SumSquares = statistics.SumSquares,
                                };

                                //  Добавление фрагмента в базу данных.
                                _ChannelInfo.ChannelFragmentInfos.Add(fragmentInfo);

                                //  Блокировка коллекции фрагментов каналов.
                                lock (_ChannelFragmentInfos)
                                {
                                    //  Добавление фрагмента в коллекцию.
                                    _ChannelFragmentInfos.Add(fragmentInfo);
                                }
                            });
                        }

                    }, async (_, cancellationToken) =>
                    {
                        //  Проверка токена отмены.
                        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Сброс информации о фрагменте.
                        fragmentInfo = null;

                        //  Проверка файла.
                        if (file.Exists)
                        {
                            //  Безопасное выполнение.
                            Invoker.Critical(delegate
                            {
                                //  Удаление файла.
                                file.Delete();
                            });
                        }
                    }, cancellationToken).ConfigureAwait(false);

                    //  Проверка информации о фрагменте.
                    if (fragmentInfo is not null)
                    {
                        //  Добавление в список идентификаторов.
                        _FragmentIDs.Add(fragmentInfo.ID);

                        //  Добавление фрагмента в словарь.
                        _Map.TryAdd(fragmentInfo.ID, activeFragment);

                        //  Блокировка критического объекта.
                        lock (_LastFragments)
                        {
                            //  Удаление из списка последних фрагментов.
                            _LastFragments.Remove(activeFragment);
                        }
                    }
                }
            }, cancellationToken).ConfigureAwait(false);

            //  Сброс активного фрагмента.
            _ActiveFragment = null;
        }
    }
}
