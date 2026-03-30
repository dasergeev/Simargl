using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Simargl.Designing.Utilities;

namespace Simargl.Office;

/// <summary>
/// Представляет оболочку вокруг COM-объекта.
/// </summary>
public abstract class ComWrapper :
    IDisposable
{
    /// <summary>
    /// Поле для хранения типа COM-объекта.
    /// </summary>
    private readonly Type _Type;

    /// <summary>
    /// Поле для хранения значения, определяющего был ли разрушен объект.
    /// </summary>
    private bool _DisposedValue;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="obj">
    /// COM-объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="obj"/> передана пустая ссылка.
    /// </exception>
    protected ComWrapper(object obj)
    {
        //  Установка COM-объекта.
        ComObject = IsNotNull(obj, nameof(obj));

        //  Установка типа COM-объекта.
        _Type = obj.GetType();

        //  Установка значения, определяющего был ли разрушен объект.
        _DisposedValue = false;

        //  Создание объекта, с помощью которого можно синхронизировать операцию разрушения.
        SyncRoot = new();

        //  Создание коллекции вложенных оболочек.
        NestedWrappers = new();
    }

    /// <summary>
    /// Возвращает COM-объект.
    /// </summary>
    public dynamic ComObject { get; }

    /// <summary>
    /// Возвращает объект, с помощью которого можно синхронизировать доступ к COM-объекту.
    /// </summary>
    public object SyncRoot { get; }

    /// <summary>
    /// Возвращает коллекцию вложенных оболочек.
    /// </summary>
    protected ComWrapperCollection NestedWrappers { get; }

    /// <summary>
    /// Выполняет действие с COM-объектом.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить с COM-объектом.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="ContextMarshalException">
    /// Член не найден.
    /// </exception>
    protected void Invoke(Action<dynamic> action)
    {
        //  Проверка ссылки на действие.
        action = IsNotNull(action, nameof(action));

        //  Блокировка критического объекта.
        lock (SyncRoot)
        {
            //  Проверка разушения объекта.
            CheckDisposing();

            //  Блок перехвата несистемных исключений.
            try
            {
                //  Вызов действия.
                action(ComObject);
            }
            catch (Exception ex)
            {
                //  Проверка системного исключения.
                if (ex.IsSystem())
                {
                    //  Повторный выброс исключения.
                    throw;
                }

                //  Не удалось найти член.
                throw ExceptionsCreator.MarshalMemberNotFound(ex);
            }
        }
    }

    /// <summary>
    /// Выполняет функцию COM-объекта.
    /// </summary>
    /// <param name="func">
    /// Функция, которую необходимо вызвать.
    /// </param>
    /// <returns>
    /// Результат вызова.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="func"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="ContextMarshalException">
    /// Член не найден.
    /// </exception>
    protected dynamic Invoke(Func<dynamic, dynamic> func)
    {
        //  Проверка ссылки на действие.
        func = IsNotNull(func, nameof(func));

        //  Блокировка критического объекта.
        lock (SyncRoot)
        {
            //  Проверка разушения объекта.
            CheckDisposing();

            //  Блок перехвата несистемных исключений.
            try
            {
                //  Вызов функции.
                return func(ComObject);
            }
            catch (Exception ex)
            {
                //  Проверка системного исключения.
                if (ex.IsSystem())
                {
                    //  Повторный выброс исключения.
                    throw;
                }

                //  Не удалось найти член.
                throw ExceptionsCreator.MarshalMemberNotFound(ex);
            }
        }
    }

    /// <summary>
    /// Создаёт новый объект с заданным идентификатором типа.
    /// </summary>
    /// <param name="progID">
    /// Идентификатор типа создаваемого объекта.
    /// </param>
    /// <returns>
    /// Новый объект с заданным идентификатором типа.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="progID"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ContextMarshalException">
    /// Объект не найден.
    /// </exception>
    protected static object CreateObjectFromProgID(string progID)
    {
        //  Проверка платформы.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            throw new InvalidOperationException("Работа с COM-объектами поддерживается только в Windows");
        }

        //  Проверка ссылки на идентификатор типа.
        progID = IsNotNull(progID, nameof(progID));

        //  Получение типа объекта.
        Type? type = Type.GetTypeFromProgID(progID) ?? throw ExceptionsCreator.MarshalObjectNotFound(progID);

        //  Получение объекта.
        object? obj = Activator.CreateInstance(type) ?? throw ExceptionsCreator.MarshalObjectNotFound(progID);

        //  Возврат созданного объекта.
        return obj;
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    /// <param name="disposing">
    /// Значение, определяющее требуется ли осовбодить управляемое состояние.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        //  Проверка платформы.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            //  Проверка необходимости освобождения COM-объекта.
            if (Marshal.IsComObject(ComObject))
            {
                //  Освобождение объекта.
                Marshal.ReleaseComObject(ComObject);

                //while (Marshal.ReleaseComObject(ComObject) > 0)
                //{

                //}
            }
        }
    }

    /// <summary>
    /// Разрушает объект с болкировкой.
    /// </summary>
    /// <param name="disposing">
    /// Значение, определяющее требуется ли осовбодить управляемое состояние.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void DisposeCore(bool disposing)
    {
        //  Блокировка критического объекта.
        lock (SyncRoot)
        {
            //  Разрушение вложенных оболочек.
            ((IDisposable)NestedWrappers).Dispose();

            //  Проверка необходимости разрушения объекта.
            if (!_DisposedValue)
            {
                //  Разрушение объекта.
                Dispose(disposing);

                //  Установка значения, определяющего, что объект разрушен.
                _DisposedValue = true;
            }
        }
    }

    /// <summary>
    /// Деструктор объекта.
    /// </summary>
    ~ComWrapper()
    {
        //  Освобождение неуправляемых ресурсов.
        DisposeCore(false);
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    void IDisposable.Dispose()
    {
        //  Освобождение всех ресурсов.
        DisposeCore(true);

        //  Сообщение среде CLR, что она на не должна вызывать метод завершения для указанного объекта.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Проверяет был ли разрушен объект.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    protected void CheckDisposing()
    {
        //  Блокировка критического объекта.
        lock (SyncRoot)
        {
            //  Проверка значения, определяющего был ли разрушен объект.
            if (_DisposedValue)
            {
                //  Выброс исключения.
                throw ExceptionsCreator.OperationObjectDisposed(GetType().Name);
            }
        }
    }
}
