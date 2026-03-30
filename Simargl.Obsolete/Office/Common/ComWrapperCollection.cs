using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections;
using Simargl.Designing.Utilities;

namespace Simargl.Office;

/// <summary>
/// Представляет коллекцию оболочек вокруг COM-объекта.
/// </summary>
public sealed class ComWrapperCollection :
    IEnumerable<ComWrapper>,
    IDisposable
{
    /// <summary>
    /// Поле для хранения значения, определяющего был ли разрушен объект.
    /// </summary>
    private bool _DisposedValue;

    /// <summary>
    /// Поле для хранения объекта, с помощью которого можно синхронизировать операцию разрушения.
    /// </summary>
    private readonly object _SyncDispose;

    /// <summary>
    /// Поле для хранения списка элементов.
    /// </summary>
    private readonly List<ComWrapper> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal ComWrapperCollection()
    {
        //  Установка значения, определяющего был ли разрушен объект.
        _DisposedValue = false;

        //  Создание объекта, с помощью которого можно синхронизировать операцию разрушения.
        _SyncDispose = new();

        //  Создание списка элементов.
        _Items = new();
    }

    /// <summary>
    /// Добавляет элемент в коллекцию.
    /// </summary>
    /// <param name="wrapper">
    /// Элемент, добавляемый в коллекцию.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="wrapper"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="wrapper"/> передан объект, который уже содержится в коллекции.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    public void Add(ComWrapper wrapper)
    {
        //  Проверка ссылки на элемент.
        wrapper = IsNotNull(wrapper, nameof(wrapper));

        //  Блокировка критического объекта.
        lock (_SyncDispose)
        {
            //  Проверка объекта.
            CheckDisposing();

            //  Проверка элемента.
            if (_Items.Contains(wrapper))
            {
                //  Значение уже содержится коллекции.
                throw new ArgumentOutOfRangeException(nameof(wrapper), $"Элемент с указанным именем уже находится в коллекции."); ;
            }

            //  Добавление элемента в коллекцию.
            _Items.Add(wrapper);
        }
    }

    /// <summary>
    /// Добавляет коллекцию элементов.
    /// </summary>
    /// <param name="wrappers">
    /// Колллекция элементов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="wrappers"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Коллекция <paramref name="wrappers"/> содержит значение, являющееся пустой ссылкой.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Коллекция <paramref name="wrappers"/> содержит значение, которое уже содержится коллекции.
    /// </exception>
    public void AddRange(IEnumerable<ComWrapper> wrappers)
    {
        //  Проверка ссылки на коллекцию.
        wrappers = IsNotNull(wrappers, nameof(wrappers));

        //  Блокировка критического объекта.
        lock (_SyncDispose)
        {
            //  Перебор добавляемых элементов.
            foreach (var wrapper in wrappers)
            {
                //  Добавление элемента.
                Add(wrapper);
            }
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    public IEnumerator<ComWrapper> GetEnumerator()
    {
        //  Блокировка критического объекта.
        lock (_SyncDispose)
        {
            //  Проверка объекта.
            CheckDisposing();

            //  Создание массива, содержащего элементы коллекции.
            var items = _Items.ToArray();

            //  Возврат перечислителя массива, содержащего элементы коллекции.
            return ((IEnumerable<ComWrapper>)items).GetEnumerator();
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    /// <param name="disposing">
    /// Значение, определяющее требуется ли осовбодить управляемое состояние.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Dispose(bool disposing)
    {
        //  Блокировка критического объекта.
        lock (_SyncDispose)
        {
            //  Проверка необходимости разрушения объекта.
            if (!_DisposedValue)
            {
                //  Перебор элементов коллекции.
                foreach (var item in _Items)
                {
                    //  Проверка интерфейса.
                    if (item is IDisposable disposable)
                    {
                        //  Разрушение объекта.
                        disposable.Dispose();
                    }
                }

                //  Проверка необходимости освобождения управляемого состояния.
                if (disposing)
                {
                    //  Очистка коллекции элементов.
                    _Items.Clear();
                }

                //  Установка значения, определяющего, что объект разрушен.
                _DisposedValue = true;
            }
        }
    }

    /// <summary>
    /// Деструктор объекта.
    /// </summary>
    ~ComWrapperCollection()
    {
        //  Освобождение неуправляемых ресурсов.
        Dispose(false);
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    void IDisposable.Dispose()
    {
        //  Освобождение всех ресурсов.
        Dispose(true);

        //  Сообщение среде CLR, что она на не должна вызывать метод завершения для указанного объекта.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Проверяет был ли разрушен объект.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CheckDisposing()
    {
        //  Блокировка критического объекта.
        lock (_SyncDispose)
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
