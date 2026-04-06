using Simargl.Designing.Utilities;

namespace Simargl.Performing;

/// <summary>
/// Представляет исполнителя.
/// </summary>
public class Performer :
    IDisposable
{
    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private CancellationTokenSource? _TokenSource;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public Performer(CancellationToken cancellationToken)
    {
        //  Создание источника токена отмены.
        _TokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        //  Регистрация действия при отмене.
        _TokenSource.Token.Register(Dispose);
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="tokens">
    /// Массив токенов отмены.
    /// </param>
    public Performer(params CancellationToken[] tokens)
    {
        //  Проверка массива.
        IsNotNull(tokens);

        //  Создание источника токена отмены.
        _TokenSource = CancellationTokenSource.CreateLinkedTokenSource(tokens);

        //  Регистрация действия при отмене.
        _TokenSource.Token.Register(Dispose);
    }

    /// <summary>
    /// Возвращает токен отмены.
    /// </summary>
    /// <returns>
    /// Токен отмены.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    public CancellationToken GetCancellationToken()
    {
        //  Получение источника токена отмены.
        CancellationTokenSource tokenSource =
            Volatile.Read(ref _TokenSource) ??
            throw ExceptionsCreator.OperationObjectDisposed(GetType().Name);

        //  Возврат токена отмены.
        return tokenSource.Token;
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    /// <param name="disposing">
    /// Значение, определяющее требуется ли освободить управляемое состояние.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {

    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    ~Performer()
    {
        //  Освобождение неуправляемых ресурсов.
        DisposeCore(false);
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    public void Dispose()
    {
        //  Полное освобождение.
        DisposeCore(true);

        //  Уведомление среды выполнения о том, чтобы она не вызывала средство завершения для объекта.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    /// <param name="disposing">
    /// Значение, определяющее требуется ли освободить управляемое состояние.
    /// </param>
    private void DisposeCore(bool disposing)
    {
        //  Подавление некритических исключений.
        DefyCritical(delegate
        {
            //  Получение источника токена отмены.
            CancellationTokenSource? tokenSource =
                Interlocked.Exchange(ref _TokenSource, null);

            //  Проверка источника токена отмены.
            if (tokenSource is not null)
            {
                //  Подавление некритических исключений.
                DefyCritical(delegate
                {
                    //  Разрушение объектов потомка.
                    Dispose(disposing);
                });

                //  Проверка необходимости освободить управляемое состояние.
                if (disposing)
                {
                    //  Отправка запроса на отмену.
                    DefyCritical(tokenSource.Cancel);

                    //  Разрушение источника токена отмены.
                    DefyCritical(tokenSource.Dispose);
                }
            }
        });
    }
}
