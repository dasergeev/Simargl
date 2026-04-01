using System.Text;
using System.IO;
using Simargl.IO;

namespace Simargl.Platform.Specialized.FileTransfer;

/// <summary>
/// Представляет описание пакета данных.
/// </summary>
public sealed class Relation
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Relation() :
        this(new(), string.Empty, default, string.Empty,
            default, default, default)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="response">
    /// Ответ на запрос соединения.
    /// </param>
    /// <param name="path">
    /// Локальный путь к файлу.
    /// </param>
    /// <param name="size">
    /// Размер файла.
    /// </param>
    /// <param name="fullName">
    /// Полное имя файла.
    /// </param>
    /// <param name="creationTime">
    /// Время создания файла.
    /// </param>
    /// <param name="lastAccessTime">
    /// Время последнего доступа к файлу.
    /// </param>
    /// <param name="lastWriteTime">
    /// Время последней операции записи в файл.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="response"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="size"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="fullName"/> передана пустая ссылка.
    /// </exception>
    public Relation(Response response, string path, long size, string fullName,
        DateTime creationTime, DateTime lastAccessTime, DateTime lastWriteTime)
    {
        //  Установка ответа на запрос соединения.
        Response = IsNotNull(response, nameof(response));

        //  Установка локального пути к файлу.
        Path = IsNotNull(path, nameof(path));

        //  Установка размера файла.
        Size = IsNotNegative(size, nameof(size));

        //  Установка полного имени файла.
        FullName = IsNotNull(fullName, nameof(fullName));

        //  Установка времени создания файла.
        CreationTime = creationTime;

        //  Установка времени последнего доступа к файлу.
        LastAccessTime = lastAccessTime;

        //  Установка времени последней операции записи в файл.
        LastWriteTime = lastWriteTime;
    }

    /// <summary>
    /// Возвращает ответ на запрос соединения.
    /// </summary>
    public Response Response { get; private set; }

    /// <summary>
    /// Возвращает локальный путь к файлу.
    /// </summary>
    public string Path { get; private set; }

    /// <summary>
    /// Возвращает размер файла.
    /// </summary>
    public long Size { get; private set; }

    /// <summary>
    /// Возвращает полное имя файла.
    /// </summary>
    public string FullName { get; private set; }

    /// <summary>
    /// Возвращает время создания файла.
    /// </summary>
    public DateTime CreationTime { get; private set; }

    /// <summary>
    /// Возвращает время последнего доступа к файлу.
    /// </summary>
    public DateTime LastAccessTime { get; private set; }

    /// <summary>
    /// Возвращает время последней операции записи в файл.
    /// </summary>
    public DateTime LastWriteTime { get; private set; }

    /// <summary>
    /// Выполняет операцию проверки на равенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator ==(Relation left, Relation right)
    {
        //  Проверка равенства полей.
        return left.Response == right.Response &&
            left.Path == right.Path &&
            left.Size == right.Size &&
            left.FullName == right.FullName &&
            left.CreationTime == right.CreationTime &&
            left.LastAccessTime == right.LastAccessTime &&
            left.LastWriteTime == right.LastWriteTime;
    }

    /// <summary>
    /// Выполняет операцию проверки на неравенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator !=(Relation left, Relation right)
    {
        //  Проверка равенства полей.
        return !(left == right);
    }

    /// <summary>
    /// Определяет, равен ли указанный объект текущему объекту.
    /// </summary>
    /// <param name="obj">
    /// Объект, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если указанный объект равен текущему объекту;
    /// в противном случае - значение <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        //  Проверка типа.
        if (obj is not Relation relation)
        {
            //  Не совпадают типы.
            return false;
        }

        //  Сравнение объектов.
        return this == relation;
    }


    /// <summary>
    /// Возвращает хэш-код для текущего объекта.
    /// </summary>
    /// <returns>
    /// Хэш-код для текущего объекта.
    /// </returns>
    public override int GetHashCode()
    {
        //  Возвращает комбинацию хэш-кодов полей.
        return HashCode.Combine(
            Response.GetHashCode(),
            Path.GetHashCode(),
            Size.GetHashCode(),
            FullName.GetHashCode(),
            CreationTime.GetHashCode(),
            LastAccessTime.GetHashCode(),
            LastWriteTime.GetHashCode());
    }

    ///// <summary>
    ///// Возвращает размер объекта в байтах.
    ///// </summary>
    //int ISizeable.Size =>
    //    SizeDeterminer.SizeOf(Response) +
    //    SizeDeterminer.SizeOf(Path, Encoding.UTF8) +
    //    SizeDeterminer.SizeOf(Size) +
    //    SizeDeterminer.SizeOf(FullName, Encoding.UTF8) +
    //    SizeDeterminer.SizeOf(CreationTime) +
    //    SizeDeterminer.SizeOf(LastAccessTime) +
    //    SizeDeterminer.SizeOf(LastWriteTime);

    /// <summary>
    /// Асинхронно сохраняет данные объекта в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток, в который необходимо сохранить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен для отслеживания запросов отмены.
    /// </param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию записи.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Поток не поддерживает запись.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task SaveAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        IsWritable(stream, nameof(stream));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства записи в поток.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Запись ответа на запрос соединения.
        await Response.SaveAsync(stream, cancellationToken).ConfigureAwait(false);

        //  Запись локального пути к файлу.
        await spreader.WriteStringAsync(Path, cancellationToken).ConfigureAwait(false);

        //  Запись размера файла.
        await spreader.WriteInt64Async(Size, cancellationToken).ConfigureAwait(false);

        //  Запись полного имени файла.
        await spreader.WriteStringAsync(FullName, cancellationToken).ConfigureAwait(false);

        //  Запись времени создания файла.
        await spreader.WriteDateTimeAsync(CreationTime, cancellationToken).ConfigureAwait(false);

        //  Запись времени последнего доступа к файлу.
        await spreader.WriteDateTimeAsync(LastAccessTime, cancellationToken).ConfigureAwait(false);

        //  Запись времени последней операции записи в файл.
        await spreader.WriteDateTimeAsync(LastWriteTime, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно загружает данные объекта из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен для отслеживания запросов отмены.
    /// </param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию чтения.
    /// </returns>
    /// <remarks>
    /// Задача, должна возвращать объект, к которому обращаются.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<Relation> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        IsReadable(stream, nameof(stream));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства чтения из потока.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Чтение ответа на запрос соединения.
        Response = await new Response().LoadAsync(stream, cancellationToken).ConfigureAwait(false);

        //  Чтение локального пути к файлу.
        Path = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение размера файла.
        Size = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);

        //  Чтение полного имени файла.
        FullName = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение времени создания файла.
        CreationTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение времени последнего доступа к файлу.
        LastAccessTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение времени последней операции записи в файл.
        LastWriteTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного объекта.
        return this;
    }
}
