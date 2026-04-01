using Simargl.Designing.Utilities;
using System.Collections;
using System.Collections.Generic;

namespace Simargl.Memory;

/// <summary>
/// Представляет перечислитель кольцевого буфера <see cref="CircularBuffer{T}"/>
/// </summary>
/// <typeparam name="T">
/// Тип элементов буфера.
/// </typeparam>
class CircularBufferEnumerator<T> :
    IEnumerator<T>
{
    /// <summary>
    /// Поле для хранения буфера.
    /// </summary>
    readonly CircularBuffer<T> _Buffer;

    /// <summary>
    /// Поле для хранения начальной позиции.
    /// </summary>
    readonly long _BeginPosition;

    /// <summary>
    /// Поле для хранения следующей за последней позиции.
    /// </summary>
    readonly long _EndPosition;

    /// <summary>
    /// Поле для хранения текущей позиции.
    /// </summary>
    long _CurrentPosition;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="buffer">
    /// Буфер.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    public CircularBufferEnumerator(CircularBuffer<T> buffer)
    {
        //  Проверка ссылки на буфер.
        IsNotNull(buffer, nameof(buffer));

        //  Блокировка критического ресурса.
        lock (buffer.SyncRoot)
        {
            //  Инициализация членов класса.
            _Buffer = buffer;
            _BeginPosition = buffer.BeginPosition;
            _EndPosition = buffer.EndPosition;
        }

        //  Инициализация членов класса.
        _CurrentPosition = _BeginPosition - 1;
        Current = default!;
    }

    /// <summary>
    /// Возвращает элемент коллекции, соответствующий текущей позиции перечислителя.
    /// </summary>
    public T Current { get; private set; }

    /// <summary>
    /// Возвращает элемент коллекции, соответствующий текущей позиции перечислителя.
    /// </summary>
    object IEnumerator.Current => Current!;

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    public void Dispose()
    {

    }

    /// <summary>
    /// Перемещает перечислитель к следующему элементу коллекции.
    /// </summary>
    /// <returns>
    /// Значение <see langword="true" />, если перечислитель был успешно перемещен к следующему элементу;
    /// значение <see langword="false" />, если перечислитель достиг конца коллекции.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// После создания перечислителя семейство было изменено.
    /// </exception>
    public bool MoveNext()
    {
        //  Блокировка критического ресурса.
        lock (_Buffer.SyncRoot)
        {
            //  Проверка изменения коллекции.
            CheckChange();

            //  Перемещение текущей позиции.
            ++_CurrentPosition;

            //  Получение следующего элемента коллекции.
            if (_CurrentPosition < _EndPosition)
            {
                Current = _Buffer.Read(_CurrentPosition);
                return true;
            }
            else
            {
                Current = default!;
                return false;
            }
        }
    }

    /// <summary>
    /// Устанавливает перечислитель в его начальное положение, т. е. перед первым элементом коллекции.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// После создания перечислителя семейство было изменено.
    /// </exception>
    public void Reset()
    {
        //  Блокировка критического ресурса.
        lock (_Buffer.SyncRoot)
        {
            //  Проверка изменения коллекции.
            CheckChange();

            //  Установка начальных параметров.
            _CurrentPosition = _BeginPosition - 1;
            Current = default!;
        }
    }

    /// <summary>
    /// Выполняет проверку изменения коллекции.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// После создания перечислителя семейство было изменено.
    /// </exception>
    void CheckChange()
    {
        //  Блокировка критического ресурса.
        lock (_Buffer.SyncRoot)
        {
            //  Получение текущих позиций.
            long beginPosition = _Buffer.BeginPosition;
            long endPosition = _Buffer.EndPosition;

            //  Проверка изменения коллекции.
            if (_BeginPosition != beginPosition || _EndPosition != endPosition)
            {
                //  После создания перечислителя семейство было изменено.
                throw ExceptionsCreator.OperationEnumerationChanged();
            }
        }
    }
}
