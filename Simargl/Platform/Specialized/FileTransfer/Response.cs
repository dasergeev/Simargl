using System.Text;
using System.IO;
using Simargl.IO;

namespace Simargl.Platform.Specialized.FileTransfer;

/// <summary>
/// Представляет ответ на запрос соединения.
/// </summary>
public sealed class Response
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Response()
    {
        //  Установка исходного запроса.
        Request = new();

        //  Установка идентификатора.
        UniqueID = string.Empty;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="request">
    /// Исходный запрос.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="request"/> передана пустая ссылка.
    /// </exception>
    public Response(Request request)
    {
        //  Установка исходного запроса.
        Request = IsNotNull(request, nameof(request));

        //  Создание идентификатора.
        UniqueID = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Возвращает исходный запрос.
    /// </summary>
    public Request Request { get; private set; }

    /// <summary>
    /// Возвращает уникальный идентификатор.
    /// </summary>
    public string UniqueID { get; private set; }

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
    public static bool operator ==(Response left, Response right)
    {
        //  Проверка равенства полей.
        return left.Request == right.Request && left.UniqueID == right.UniqueID;
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
    public static bool operator !=(Response left, Response right)
    {
        //  Проверка неравенства полей.
        return left.Request != right.Request || left.UniqueID != right.UniqueID;
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
        if (obj is not Response response)
        {
            //  Не совпадают типы.
            return false;
        }

        //  Сравнение объектов.
        return this == response;
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
        return HashCode.Combine(Request.GetHashCode(), UniqueID.GetHashCode());
    }

    ///// <summary>
    ///// Возвращает размер объекта в байтах.
    ///// </summary>
    //int ISizeable.Size =>
    //    SizeDeterminer.SizeOf(Request) +
    //    SizeDeterminer.SizeOf(UniqueID, Encoding.UTF8);

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

        //  Запись исходного запроса.
        await Request.SaveAsync(stream, cancellationToken).ConfigureAwait(false);

        //  Запись уникального идентификатора.
        await spreader.WriteStringAsync(UniqueID, cancellationToken).ConfigureAwait(false);
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
    public async Task<Response> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        IsReadable(stream, nameof(stream));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства чтения из потока.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Чтение исходного запроса.
        await Request.LoadAsync(stream, cancellationToken).ConfigureAwait(false);

        //  Чтение уникального идентификатора.
        UniqueID = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного объекта.
        return this;
    }
}
