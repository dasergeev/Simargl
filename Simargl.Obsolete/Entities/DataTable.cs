using Simargl.Concurrent;
using Simargl.Performing;

namespace Simargl.Entities;

/// <summary>
/// Представляет таблицу данных базы данных.
/// </summary>
/// <typeparam name="TContext">
/// Тип контекста базы данных.
/// </typeparam>
/// <typeparam name="TEntityData">
/// Тип данных сущности.
/// </typeparam>
[CLSCompliant(false)]
public sealed class DataTable<TContext, TEntityData> :
    Performer
    where TContext : StorageContext, IDataTableProvider<TEntityData>, new()
    where TEntityData : EntityData
{
    /// <summary>
    /// Поле для хранения критического объекта.
    /// </summary>
    private readonly AsyncLock _Lock;

    /// <summary>
    /// Поле для хранения хранилища данных.
    /// </summary>
    private readonly Storage<TContext> _Storage;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="storage">
    /// Хранилище данных.
    /// </param>
    public DataTable(Storage<TContext> storage) :
        this(storage, CancellationToken.None)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public DataTable(CancellationToken cancellationToken) :
        this(new(cancellationToken), false, cancellationToken)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="storage">
    /// Хранилище данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public DataTable(Storage<TContext> storage, CancellationToken cancellationToken) :
        this(storage, false, cancellationToken)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="storage">
    /// Хранилище данных.
    /// </param>
    /// <param name="isNeedDestroyStorage">
    /// Значение, определяющее требуется ли разрушить хранилище данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    private DataTable(Storage<TContext> storage, bool isNeedDestroyStorage, CancellationToken cancellationToken) :
        base(storage.GetCancellationToken(), cancellationToken)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Создание критического объекта.
            _Lock = new();

            //  Установка хранилища данных.
            _Storage = storage;

            //  Замена токена отмены.
            cancellationToken = GetCancellationToken();

            //  Регистрация метода завершения.
            cancellationToken.Register(destroy);
        }
        catch
        {
            //  Разрушение данных.
            destroy();

            //  Повторный выброс исключения.
            throw;
        }

        //  Разрушает данные.
        void destroy()
        {
            //  Проверка необходимости разрушения хранилища данных.
            if (isNeedDestroyStorage)
            {
                //  Разрушение данных.
                DefyCritical(storage.Dispose);
            }
        }
    }
}
