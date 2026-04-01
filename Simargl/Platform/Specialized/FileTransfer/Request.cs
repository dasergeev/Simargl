using System.Text;
using System.IO;
using Simargl.IO;

namespace Simargl.Platform.Specialized.FileTransfer;

/// <summary>
/// Представляет запрос на соединение.
/// </summary>
public sealed class Request
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Request() :
        this(0, string.Empty)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="identifier">
    /// Идентификатор клиента.
    /// </param>
    /// <param name="name">
    /// Имя клиента.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="identifier"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    public Request(int identifier, string name)
    {
        //  Установка идентификатора.
        Identifier = IsNotNegative(identifier, nameof(identifier));

        //  Установка имени правила.
        Name = IsNotNull(name, nameof(name));
    }

    /// <summary>
    /// Возвращает идентификатор клиента.
    /// </summary>
    public int Identifier { get; private set; }

    /// <summary>
    /// Возвращает имя клиента.
    /// </summary>
    public string Name { get; private set; }

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
    public static bool operator == (Request left, Request right)
    {
        //  Проверка равенства полей.
        return left.Identifier == right.Identifier && left.Name == right.Name;
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
    public static bool operator !=(Request left, Request right)
    {
        //  Проверка неравенства полей.
        return left.Identifier != right.Identifier || left.Name != right.Name;
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
        if (obj is not Request request)
        {
            //  Не совпадают типы.
            return false;
        }

        //  Сравнение объектов.
        return this == request;
    }

    /// <summary>
    /// Возвращает хэш-код для текущего объекта.
    /// </summary>
    /// <returns>
    /// Хэш-код для текущего объекта.
    /// </returns>
    public override int GetHashCode()
    {
        //  Возвращает идентификатор.
        return Identifier;
    }

    ///// <summary>
    ///// Возвращает размер объекта в байтах.
    ///// </summary>
    //public int ISizeable.Size =>
    //    SizeDeterminer.SizeOf(Identifier) +
    //    SizeDeterminer.SizeOf(Name, Encoding.UTF8);

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

        //  Запись идентификатора.
        await spreader.WriteInt32Async(Identifier, cancellationToken).ConfigureAwait(false);

        //  Запись имени.
        await spreader.WriteStringAsync(Name, cancellationToken).ConfigureAwait(false);
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
    public async Task<Request> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        IsReadable(stream, nameof(stream));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства чтения из потока.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Чтение идентификатора.
        Identifier = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Чтение имени.
        Name = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного объекта.
        return this;
    }
}
