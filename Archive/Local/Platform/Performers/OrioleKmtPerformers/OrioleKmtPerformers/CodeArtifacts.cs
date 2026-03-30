namespace Apeiron.Platform.Performers.OrioleKmtPerformers
{
    internal class CodeArtifacts
    {
        //// Путь к папке с файлами проекта Oriole Voit.
        //private const string OrioleVoitPath = @"\\railtest.ru\Data\06-НТО\RawData\Oriole\Voit";

        ///// <summary>
        ///// Асинхронно выполняет поиск файлов Oriole Voit.
        ///// </summary>
        ///// <param name="cancellationToken">Токен отмены.</param>
        ///// <returns>Задача, выполняющая поиск.</returns>
        ///// <exception cref="OperationCanceledException">Операция отменена.</exception>
        //private async Task SearchAndRemoveVoitRawFilesAsync(CancellationToken cancellationToken)
        //{
        //    //  Проверка токена отмены.
        //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //    //  Создание контекста сеанса работы с базой данных.
        //    using OrioleKmtDatabaseContext database = new();

        //    var rawOrioleKmtFileList = await database.RawFrames
        //        .AsNoTracking()
        //        .Where(x => x.Time > DateTime.Parse("09.07.2021 0:00:00"))
        //        .ToListAsync(cancellationToken)
        //        .ConfigureAwait(false);

        //    //  Определение количества файлов.
        //    int fileOrioleVoitCount = rawOrioleKmtFileList.Count;

        //    //  Вывод информации в журнал.
        //    await Journal.LogInformationAsync(
        //        $"Найдено {fileOrioleVoitCount} файлов Oriole Voit",
        //        cancellationToken).ConfigureAwait(false);

        //    if (fileOrioleVoitCount > 0)
        //    {
        //        //  Начало транзакции.
        //        using var transaction = await database.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        //        database.RemoveRange(rawOrioleKmtFileList);

        //        //  Сохранение изменений в базу данных.
        //        int count = await database.SaveChangesAsync(cancellationToken);

        //        //  Фиксирование изменений.
        //        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        //    }
        //}




        //// Удаление из кадра не нужные каналы в соответствии со списком.
        //foreach (var item in _ChannelNameForDelete)
        //{
        //    if (frame.Channels.Contains(item))
        //        frame.Channels.Remove(frame.Channels.First(x => x.Name == item));
        //}

        ////List<string> channelNames1 = new List<string>();

        ////foreach (var item in frame.Channels)
        ////{
        ////    channelNames1.Add(item.Name);
        ////}

        ////var a = channelNames1;

        //// Поиск списка каналов для переименования
        //var channelRenameList = await database.TimeChunks
        //    .AsNoTracking()
        //    .Where(x => x.BeginTime <= rawFrameFileList[27330].Time && x.EndTime > rawFrameFileList[27330].Time)
        //    .Select(x => x)
        //    .Include(c => c.ChannelNames)
        //    .ToListAsync(cancellationToken)
        //    .ConfigureAwait(false);

        //foreach (var listItem in channelRenameList)
        //{
        //    foreach (var channelNameItem in listItem.ChannelNames)
        //    {
        //        frame.Channels[channelNameItem.Index].Name = channelNameItem.Name;
        //    }
        //}

        ////List<string> channelNames2 = new List<string>();

        ////foreach (var item in frame.Channels)
        ////{
        ////    channelNames2.Add(item.Name);
        ////}

        ////var b = channelNames2;

        //////  Асинхронное добавление информации о новых файлах в базу данных.
        ////await Parallel.ForEachAsync(filesInFileSystem, new ParallelOptions() { CancellationToken = cancellationToken },
        ////    async (fileInFileSystem, cancellationToken) =>
        ////    {
        ////        //  Проверка токена отмены.
        ////        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        ////        //  Безопасное выполнение операции.
        ////        await SafeCallAsync(async cancellationToken =>
        ////            {

        ////            }

        ////             }, cancellationToken).ConfigureAwait(false);

        ////     }).ConfigureAwait(false);



        //database.ChannelNames.Add(new () { TimeChunkId = 1, Index = 0, Name = "UXB1", });
        //database.ChannelNames.Add(new () { TimeChunkId = 1, Index = 1, Name = "UYB1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 2, Name = "UZB1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 3, Name = "UXK1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 4, Name = "UYK1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 5, Name = "UZK1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 6, Name = "UXK2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 7, Name = "UYK2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 8, Name = "UZK2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 9, Name = "UXR", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 10, Name = "UYR", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 11, Name = "UZR", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 12, Name = "Nk1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 13, Name = "Nk2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 14, Name = "CB_CH_15", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 15, Name = "CB_CH_16", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 16, Name = "Mo", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 17, Name = "Mr", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 18, Name = "Mir1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 19, Name = "Mir2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 20, Name = "KMT_CH_05", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 21, Name = "KMT_CH_06", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 22, Name = "KMT_CH_07", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 23, Name = "KMT_CH_08", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 24, Name = "KMT_CH_09", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 25, Name = "KMT_CH_10", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 26, Name = "KMT_CH_11", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 27, Name = "KMT_CH_12", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 28, Name = "KMT_CH_13", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 29, Name = "KMT_CH_14", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 30, Name = "KMT_CH_15", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 31, Name = "KMT_CH_16", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 32, Name = "V_GPS", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 33, Name = "Lon_GPS", });
        //database.ChannelNames.Add(new() { TimeChunkId = 1, Index = 34, Name = "Lat_GPS", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 0, Name = "UXB1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 1, Name = "UYB1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 2, Name = "UZB1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 3, Name = "UXR", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 4, Name = "UYR", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 5, Name = "UZR", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 6, Name = "UXK1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 7, Name = "UYK1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 8, Name = "UZK1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 9, Name = "UXK2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 10, Name = "UYK2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 11, Name = "UZK2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 12, Name = "Nk1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 13, Name = "Nk2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 14, Name = "CB_CH_15", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 15, Name = "CB_CH_16", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 16, Name = "Mo", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 17, Name = "Mr", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 18, Name = "Mir1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 19, Name = "Mir2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 20, Name = "KMT_CH_05", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 21, Name = "KMT_CH_06", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 22, Name = "KMT_CH_07", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 23, Name = "KMT_CH_08", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 24, Name = "KMT_CH_09", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 25, Name = "KMT_CH_10", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 26, Name = "KMT_CH_11", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 27, Name = "KMT_CH_12", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 28, Name = "KMT_CH_13", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 29, Name = "KMT_CH_14", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 30, Name = "KMT_CH_15", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 31, Name = "KMT_CH_16", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 32, Name = "V_GPS", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 33, Name = "Lon_GPS", });
        //database.ChannelNames.Add(new() { TimeChunkId = 3, Index = 34, Name = "Lat_GPS", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 0, Name = "UXB1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 1, Name = "UYB1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 2, Name = "UZB1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 3, Name = "UXR", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 4, Name = "UYR", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 5, Name = "UZR", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 6, Name = "UXK1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 7, Name = "UYK1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 8, Name = "UZK1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 9, Name = "UXK2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 10, Name = "UYK2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 11, Name = "UZK2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 12, Name = "Nk1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 13, Name = "Nk2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 14, Name = "CB_CH_15", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 15, Name = "CB_CH_16", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 16, Name = "Mr", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 17, Name = "Mo", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 18, Name = "Mir1", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 19, Name = "Mir2", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 20, Name = "KMT_CH_05", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 21, Name = "KMT_CH_06", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 22, Name = "KMT_CH_07", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 23, Name = "KMT_CH_08", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 24, Name = "KMT_CH_09", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 25, Name = "KMT_CH_10", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 26, Name = "KMT_CH_11", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 27, Name = "KMT_CH_12", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 28, Name = "KMT_CH_13", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 29, Name = "KMT_CH_14", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 30, Name = "KMT_CH_15", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 31, Name = "KMT_CH_16", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 32, Name = "V_GPS", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 33, Name = "Lon_GPS", });
        //database.ChannelNames.Add(new() { TimeChunkId = 4, Index = 34, Name = "Lat_GPS", });


        //// Удаление из кадра ненужные каналы в соответствии со списком.
        //foreach (var item in _ChannelNameForDelete)
        //{
        //    //  Проверка токена отмены.
        //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //    if (newFrame.Channels.Contains(item))
        //    {
        //        object locker = new();
        //        lock (locker)
        //        {
        //            newFrame.Channels.Remove(newFrame.Channels.First(x => x.Name == item));
        //        }
        //    }
        //}


        ///// <summary>
        ///// Класс для хранения информации о файле на файловой системе.
        ///// </summary>
        //private sealed class FileProp
        //{
        //    public string FilePath { get; init; } = null!;
        //    public DateTime Time { get; init; }

        //    //.Select(x => new FileProp()
        //    //{
        //    //    FilePath = x.FilePath,
        //    //    Time = x.Time,
        //    //})
        //}


    }
}
