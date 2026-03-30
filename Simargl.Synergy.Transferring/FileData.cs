using Simargl.IO;
using Simargl.Cryptography;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Synergy.Transferring;

/// <summary>
/// Представляет данные файла.
/// </summary>
public sealed class FileData :
    Message
{
    /// <summary>
    /// Поле для хранения первой контрольной суммы.
    /// </summary>
    private readonly uint _FirstChecksum;

    /// <summary>
    /// Поле для хранения второй контрольной суммы.
    /// </summary>
    private readonly uint _SecondChecksum;

    /// <summary>
    /// Поле для хранения третьей контрольной суммы.
    /// </summary>
    private readonly uint _ThirdChecksum;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <param name="data">
    /// Данные файла.
    /// </param>
    /// <param name="creationTimeUtc">
    /// Время создания файла
    /// в формате всеобщего скоординированного времени (UTC).
    /// </param>
    /// <param name="lastWriteTimeUtc">
    /// Время последней операции записи в файл
    /// в формате всеобщего скоординированного времени (UTC).
    /// </param>
    /// <param name="lastAccessTimeUtc">
    /// Время последнего доступа к файлу
    /// в формате всеобщего скоординированного времени (UTC).
    /// </param>
    public FileData(string path, byte[] data,
        DateTime creationTimeUtc, DateTime lastWriteTimeUtc, DateTime lastAccessTimeUtc) :
        base(MessageFormat.FileData)
    {
        //  Установка значений свойств.
        Path = IsNotNull(path);
        Data = IsNotNull(data);
        CreationTimeUtc = creationTimeUtc;
        LastWriteTimeUtc = lastWriteTimeUtc;
        LastAccessTimeUtc = lastAccessTimeUtc;

        //  Установка контрольных сумм.
        _FirstChecksum = GetFirstChecksum(Data);
        _SecondChecksum = GetSecondChecksum(Data);
        _ThirdChecksum = GetFirstChecksum(Data);
    }

    /// <summary>
    /// Возвращает локальный путь к файлу.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Возвращает данные файла.
    /// </summary>
    public byte[] Data { get; }

    /// <summary>
    /// Возвращает время создания файла
    /// в формате всеобщего скоординированного времени (UTC).
    /// </summary>
    public DateTime CreationTimeUtc { get; }

    /// <summary>
    /// Возвращает время последней операции записи в файл
    /// в формате всеобщего скоординированного времени (UTC).
    /// </summary>
    public DateTime LastWriteTimeUtc { get; }

    /// <summary>
    /// Возвращает время последнего доступа к файлу
    /// в формате всеобщего скоординированного времени (UTC).
    /// </summary>
    public DateTime LastAccessTimeUtc { get; }

    /// <summary>
    /// Асинхронно сохраняет сообщение в поток.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, сохраняющая данные в поток.
    /// </returns>
    protected override sealed async Task SaveAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        //  Сохранение пути.
        await spreader.WriteStringAsync(Path, cancellationToken).ConfigureAwait(false);

        //  Сохранение данных.
        await spreader.WriteInt32Async(Data.Length, cancellationToken).ConfigureAwait(false);
        await spreader.WriteBytesAsync(Data, cancellationToken).ConfigureAwait(false);

        //  Сохранение времён.
        await spreader.WriteDateTimeAsync(CreationTimeUtc, cancellationToken).ConfigureAwait(false);
        await spreader.WriteDateTimeAsync(LastWriteTimeUtc, cancellationToken).ConfigureAwait(false);
        await spreader.WriteDateTimeAsync(LastAccessTimeUtc, cancellationToken).ConfigureAwait(false);

        //  Сохранение контрольных сумм.
        await spreader.WriteUInt32Async(_FirstChecksum, cancellationToken).ConfigureAwait(false);
        await spreader.WriteUInt32Async(_SecondChecksum, cancellationToken).ConfigureAwait(false);
        await spreader.WriteUInt32Async(_ThirdChecksum, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно загружает сообщение из потока.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая данные из потока.
    /// </returns>
    internal static async Task<FileData> LoadAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        //  Чтение пути.
        string path = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение данных.
        int length = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);
        byte[] data =  await spreader.ReadBytesAsync(length, cancellationToken).ConfigureAwait(false);

        //  Чтение времён.
        DateTime creationTimeUtc = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);
        DateTime lastWriteTimeUtc = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);
        DateTime lastAccessTimeUtc = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение контрольных сумм.
        uint firstChecksum = await spreader.ReadUInt32Async(cancellationToken).ConfigureAwait(false);
        uint secondChecksum = await spreader.ReadUInt32Async(cancellationToken).ConfigureAwait(false);
        uint thirdChecksum = await spreader.ReadUInt32Async(cancellationToken).ConfigureAwait(false);

        //  Создание сообщения.
        FileData fileData = new(path, data, creationTimeUtc, lastWriteTimeUtc, lastAccessTimeUtc);

        //  Проверка контрольных сумм.
        if (fileData._FirstChecksum != firstChecksum ||
            fileData._SecondChecksum != secondChecksum ||
            fileData._ThirdChecksum != thirdChecksum)
        {
            throw new InvalidDataException("Сообщение содержит неправильный контрольные суммы.");
        }

        //  Возврат сообщения.
        return fileData;
    }

    /// <summary>
    /// Расчитывает первую контрольную сумму.
    /// </summary>
    /// <param name="data">
    /// Данные.
    /// </param>
    /// <returns>
    /// Контрольная сумма.
    /// </returns>
    private static uint GetFirstChecksum(byte[] data)
    {
        //  Возврат значения 32-битного хеш-алгоритма CRC.
        return Crc32.Compute(data);
    }

    /// <summary>
    /// Расчитывает вторую контрольную сумму.
    /// </summary>
    /// <param name="data">
    /// Данные.
    /// </param>
    /// <returns>
    /// Контрольная сумма.
    /// </returns>
    private static uint GetSecondChecksum(byte[] data)
    {
        //  Инициализация контрольной суммы.
        uint checksum = 0;

        //  Перебор данных.
        foreach (byte value in data)
        {
            //  Корректировка контрольной суммы.
            checksum = unchecked(checksum + value);
        }

        //  Возврат контрольной суммы.
        return checksum;
    }

    /// <summary>
    /// Расчитывает третью контрольную сумму.
    /// </summary>
    /// <param name="data">
    /// Данные.
    /// </param>
    /// <returns>
    /// Контрольная сумма.
    /// </returns>
    private static uint GetThirdChecksum(byte[] data)
    {
        //  Инициализация контрольной суммы.
        uint checksum = 0;

        //  Перебор данных.
        foreach (byte value in data)
        {
            //  Корректировка контрольной суммы.
            checksum = unchecked((checksum << 1) + (checksum ^ value));
        }

        //  Возврат контрольной суммы.
        return checksum;
    }
}
