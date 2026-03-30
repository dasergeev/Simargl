namespace Apeiron.Platform.Server.Services.Orchestrator.Packages;

///// <summary>
///// Представляет заголовок пакета, содержащий основную информацию для идентификации системы и пакета.
///// </summary>
//public class GeneralPackage //: ISizeable/*, IAsyncStreamObject<HeaderPackage>*/
//{
//    /// <summary>
//    /// Последовательность идентифицирующая Host.
//    /// </summary>
//    public long IdSequence { get; private set; }

//    /// <summary>
//    /// Возвращает формат пакета.
//    /// </summary>
//    public byte Format { get; private set; }

//    /// <summary>
//    /// Представляет идентификатор хоста в строковом формате.
//    /// </summary>
//    public string HostId { get; private set; }

//    ///// <summary>
//    ///// Возвращает размер пакета.
//    ///// </summary>
//    //public int Size { get; private set; }

//    /// <summary>
//    /// Список хостов для консоли.
//    /// </summary>
//    public string HostsList { get; private set; }


//    /// <summary>
//    /// Инициализирует класс.
//    /// </summary>
//    public GeneralPackage()
//    {
//        HostId = string.Empty;
//        HostsList = string.Empty;
//    }

//    /// <summary>
//    /// Инициализирует класс.
//    /// </summary>
//    /// <param name="packageFormat">Формат пакета.</param>
//    /// <param name="hostsList"></param>
//    public GeneralPackage(PackageFormat packageFormat, string hostsList)
//    {
//        // Устанавливаем идентификационную последовательность.
//        IdSequence = 0xaa008888;

//        // Установка формата пакета.
//        Format = (byte)packageFormat;

//        // Установка имени хоста.
//        HostId = Environment.MachineName.ToLower();

//        // Создаёт пустой 
//        HostsList = hostsList;

//        //// Устанавливает размер пакета.
//        //Size = SizeDeterminer.SizeOf(IdSequence)
//        //        + SizeDeterminer.SizeOf(Format)
//        //        + SizeDeterminer.SizeOf(HostId, Encoding.UTF8)
//        //        + SizeDeterminer.SizeOf(HostsList, Encoding.UTF8);
//    }

//    /// <summary>
//    /// Ассинхронно сохраняет данные объекта в поток.
//    /// </summary>
//    /// <param name="stream">
//    /// Поток, в который необходимо сохранить данные.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен для отслеживания запросов отмены.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="stream"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentException">
//    /// Поток не поддерживает запись.
//    /// </exception>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <returns>Поток Stream</returns>
//    public async Task PackageToStreamAsync(Stream stream, CancellationToken cancellationToken)
//    {
//        // Проверяем поток на возможность чтения.
//        Check.IsWritable(stream, nameof(stream));

//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        // Создание средства записи в поток.
//        using AsyncBinaryWriter writer = new(stream, Encoding.UTF8, true);

//        try
//        {
//            //  Запись данных в поток.
//            await writer.WriteAsync(IdSequence, cancellationToken).ConfigureAwait(false);
//            await writer.WriteAsync(Format, cancellationToken).ConfigureAwait(false);
//            await writer.WriteAsync(HostId, cancellationToken).ConfigureAwait(false);
//            await writer.WriteAsync(Size, cancellationToken).ConfigureAwait(false);
//            await writer.WriteAsync(HostsList, cancellationToken).ConfigureAwait(false);

//            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
//        }
//        catch (Exception)/* when (!e.IsCritical() && !e.IsSystem()*/
//        {
//            throw;
//        }
//    }

//    /// <summary>
//    /// Асинхронно загружает данные объекта из потока.
//    /// </summary>
//    /// <param name="stream">
//    /// Поток, из которого необходимо загрузить данные.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен для отслеживания запросов отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, представляющая асинхронную операцию чтения.
//    /// </returns>
//    /// <remarks>
//    /// Задача, должна возвращать объект, к которому обращаются.
//    /// </remarks>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="stream"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentException">
//    /// Поток не поддерживает чтение.
//    /// </exception>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="FormatException">
//    /// Чтение пакета некорректного формата.
//    /// </exception>
//    public static async Task<GeneralPackage> PackageFromStreamAsync(Stream stream, CancellationToken cancellationToken)
//    {
//        // Проверяем поток на возможность чтения.
//        Check.IsReadable(stream, nameof(stream));

//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //  Создание средства чтения из потока.
//        using var reader = new AsyncBinaryReader(stream, Encoding.UTF8, true);

//        try
//        {
//            // Считываем данные.
//            long receiveIdSequence = await reader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
//            byte receiveFormat = await reader.ReadByteAsync(cancellationToken).ConfigureAwait(false);
//            string receiveHostId = await reader.ReadStringAsync(cancellationToken).ConfigureAwait(false);
//            int receiveSize = await reader.ReadInt32Async(cancellationToken).ConfigureAwait(false);
//            string receiveHostsList = await reader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

//            // Формируем новый пакет.
//            if (receiveIdSequence == 0xaa008888)
//            {
//                var package = new GeneralPackage
//                {
//                    IdSequence = receiveIdSequence,
//                    Format = receiveFormat,
//                    HostId = receiveHostId,
//                    Size = receiveSize,
//                    HostsList = receiveHostsList
//                };
//                return package;
//            }
//            else
//            {
//                throw new FormatException("Поступил некорректный пакет.");
//            }
//        }
//        catch (Exception e) when (!e.IsCritical() && !e.IsSystem())
//        {
//            throw;
//        }
//    }
//}

