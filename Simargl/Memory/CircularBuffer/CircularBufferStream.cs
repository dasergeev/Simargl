using Simargl.Designing.Utilities;
using Simargl.Designing;
using System.IO;

namespace Simargl.Memory;

/// <summary>
/// Предоставляет универсальное представление последовательности байтов циклического буфера.
/// </summary>
/// <seealso cref="Stream" />
public sealed class CircularBufferStream : Stream
{
    /// <summary>
    /// Поле для хранения буфера.
    /// </summary>
    private readonly CircularBuffer<byte> _Buffer;

    /// <summary>
    /// Поле для хранения значения, определяющего были ли освобождены ресурсы объекта.
    /// </summary>
    private bool _IsDisposed;

    /// <summary>
    /// Поле для хранения начальной позиции.
    /// </summary>
    private long _BeginPosition;

    /// <summary>
    /// Поле для хранения позиции следующей за последней.
    /// </summary>
    private long _EndPosition;

    /// <summary>
    /// Поле для хранения текущей позиции.
    /// </summary>
    private long _Position;

    /// <summary>
    /// Поле для хранения времени, в течение которого операция чтения блокирует ожидание данных.
    /// </summary>
    private long _ReadTimeout;

    /// <summary>
    /// Поле для хранения времени, в течение которого операция записи блокирует ожидание данных.
    /// </summary>
    private int _WriteTimeout;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, для которого создаётся универсальное представление последовательности байтов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    public CircularBufferStream(CircularBuffer<byte> buffer)
    {
        //  Проверка входного параметра.
        IsNotNull(buffer, nameof(buffer));

        //  Инициализация членов класса.
        _IsDisposed = false;

        //  Блокировка критического ресурса.
        lock (buffer.SyncRoot)
        {
            //  Инициализация членов класса.
            _Buffer = buffer;

            //  Определение позиций.
            _BeginPosition = buffer.BeginPosition;
            _EndPosition = buffer.EndPosition;
        }

        //  Определение текущей позиции.
        _Position = _EndPosition;

        //  Установка времени ожидания.
        _ReadTimeout = Timeout.Infinite;
        _WriteTimeout = Timeout.Infinite;

        //  Установка обработчика события изменения буфера.
        _Buffer.Changed += Buffer_Changed;
    }

    /// <summary>
    /// Возвращает объект, который можно использовать для синхронизации доступа.
    /// </summary>
    public object SyncRoot => _Buffer.SyncRoot;

    /// <summary>
    /// Возвращает значение, показывающее, поддерживает ли текущий поток возможность чтения.
    /// </summary>
    public override bool CanRead => true;

    /// <summary>
    /// Возвращает значение, которое показывает, поддерживается ли в текущем потоке возможность поиска.
    /// </summary>
    public override bool CanSeek => true;

    /// <summary>
    /// Возвращает значение, которое показывает, может ли для данного потока истечь время ожидания.
    /// </summary>
    public override bool CanTimeout => true;

    /// <summary>
    /// Возвращает значение, которое показывает, поддерживает ли текущий поток возможность записи.
    /// </summary>
    public override bool CanWrite => true;

    /// <summary>
    /// Возвращает длину потока в байтах.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// Произошла попытка обратиться к разрушенному объекту.
    /// </exception>
    public override long Length
    {
        get
        {
            //  Захват критического ресурса.
            lock (SyncRoot)
            {
                //  Проверка состояния.
                CheckDisposed();

                //  Определение длины.
                return _Buffer.EndPosition;
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт позицию в текущем потоке.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// Произошла попытка обратиться к разрушенному объекту.
    /// </exception>
    /// <exception cref="IOException">
    /// Значение не соответствует допустимому диапазону значений.
    /// </exception>
    public override long Position
    {
        get
        {
            //  Захват критического ресурса.
            lock (SyncRoot)
            {
                //  Проверка состояния.
                CheckDisposed();

                //  Возвращает позицию в текущем потоке.
                return _Position;
            }
        }
        set
        {
            //  Захват критического ресурса.
            lock (SyncRoot)
            {
                //  Проверка изменения текущего значения.
                if (_Position != value)
                {
                    //  Проверка состояния.
                    CheckDisposed();

                    //  Проверка нового значения.
                    if (value < _BeginPosition || value > _EndPosition)
                    {
                        //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
                        throw ExceptionsCreator.ArgumentOutOfRange(nameof(value));
                    }

                    //  Установка нового значения.
                    _Position = value;
                }
            }
        }
    }

    /// <summary>
    /// Возвращает или задает время, в течение которого операция чтения блокирует ожидание данных.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
    /// </exception>
    public override int ReadTimeout
    {
        get
        {
            //  Захват критического ресурса.
            lock (SyncRoot)
            {
                return (int)_ReadTimeout;
            }
        }
        set
        {
            //  Захват критического ресурса.
            lock (SyncRoot)
            {
                //  Проверка изменения значения.
                if (_ReadTimeout != value)
                {
                    //  Установка нового значения.
                    _ReadTimeout = IsTimeout(value, nameof(ReadTimeout));
                }
            }
        }
    }

    /// <summary>
    /// Возвращает или задает время, в течение которого операция записи блокирует ожидание данных.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение меньшее или равное нулю, и флаг <see cref="Timeout.Infinite"/> не установлен.
    /// </exception>
    public override int WriteTimeout
    {
        get
        {
            //  Захват критического ресурса.
            lock (SyncRoot)
            {
                return _WriteTimeout;
            }
        }
        set
        {
            //  Захват критического ресурса.
            lock (SyncRoot)
            {
                if (_WriteTimeout != value)
                {
                    //  Установка нового значения.
                    _WriteTimeout = IsTimeout(value, nameof(ReadTimeout));
                }
            }
        }
    }

    /// <summary>
    /// Очищает все буферы данного потока и вызывает запись данных буферов в базовое устройство.
    /// </summary>
    public override void Flush()
    {
            
    }


    /// <summary>
    /// Считывает последовательность байтов из текущего потока и перемещает позицию в потоке на число считанных байтов.
    /// </summary>
    /// <param name="buffer">
    /// Массив, в который необходимо поместить прочитанные элементы.
    /// </param>
    /// <param name="offset">
    /// Смещение в массиве <paramref name="buffer"/>, с которого начинается запись прочитанных элементов из текущего буфера.
    /// </param>
    /// <param name="count">
    /// Количество элементов, которые необходимо прочитать из текущего буфера.
    /// </param>
    /// <returns>
    /// Общее количество байтов, считанных в буфер.
    /// Это число может быть меньше количества запрошенных байтов,
    /// если столько байтов в настоящее время недоступно,
    /// а также равняться нулю, если был достигнут конец потока.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="offset"/>.
    /// </exception>
    /// <exception cref="OverflowException">
    /// Произошло переполнение буфера.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Произошла попытка обратиться к разрушенному объекту.
    /// </exception>
    public override int Read(byte[] buffer, int offset, int count)
    {
        //  Проверка входного массива.
        IsRange(buffer, offset, count, nameof(buffer), nameof(offset), nameof(count));

        //  Переменная для количества доступных для чтения данных.
        int readCount;

        //  Обновляет количество доступных байт для чтения данных.
        void updateReadCount()
        {
            //  Проверка существования объекта.
            CheckDisposed();

            //  Определение количества байт доступных для чтения.
            readCount = (int)(Interlocked.Read(ref _EndPosition) - _Position);

            //  Определение количества байт для чтения.
            readCount = Math.Min(count, readCount);
        }

        //  Обновление количества доступных байт для чтения данных.
        updateReadCount();

        //  Проверка доступности данных для чтения.
        if (readCount < count)
        {
            //  Получение таймаута.
            var timeout = (int)Interlocked.Read(ref _ReadTimeout);

            //  Проверка таймаута.
            if (timeout == Timeout.Infinite)
            {
                //  Ожидание получения данных.
                while (readCount < count)
                {
                    //  Ожидание.
                    Task.Delay(1).Wait();
                }
            }
            else
            {
                //  Максимальное время окончания цикла.
                DateTime endTime = DateTime.Now + new TimeSpan(10000 * _ReadTimeout);

                //  Ожидание получения данных.
                while (readCount < count && DateTime.Now < endTime)
                {
                    //  Ожидание.
                    Task.Delay(1).Wait();
                }
            }
        }

        //  Захват критического ресурса.
        lock (SyncRoot)
        {
            //  Проверка позиции.
            CheckPosition();

            //  Обновление количества доступных байт для чтения данных.
            updateReadCount();

            //  Чтение данных из буфера.
            _Buffer.Read(_Position, buffer, offset, readCount);

            //  Изменение текущей позиции.
            _Position += readCount;
        }

        //  Возврат количества байтов, считанных в буфер.
        return readCount;
    }

    /// <summary>
    /// Задает позицию в текущем потоке.
    /// </summary>
    /// <param name="offset">
    /// Смещение в байтах относительно параметра <paramref name="origin" />.
    /// </param>
    /// <param name="origin">
    /// Значение типа <see cref="SeekOrigin" />, указывающее точку ссылки, которая используется для получения новой позиции.
    /// </param>
    /// <returns>
    /// Новая позиция в текущем потоке.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    /// Произошла попытка обратиться к разрушенному объекту.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="origin"/> передано значение, которое не содержится в перечислении <see cref="SeekOrigin"/>.
    /// </exception>
    /// <exception cref="IOException">
    /// Значение не соответствует допустимому диапазону значений.
    /// </exception>
    public override long Seek(long offset, SeekOrigin origin)
    {
        //  Захват критического ресурса.
        lock (SyncRoot)
        {
            //  Обновление позиций.
            CheckDisposed();

            //  Расчёт нового значения позиции.
            var position = origin switch
            {
                SeekOrigin.Begin => _BeginPosition + offset,
                SeekOrigin.Current => _Position + offset,
                SeekOrigin.End => _EndPosition + offset,
                _ => throw ExceptionsCreator.ArgumentNotContainedInEnumeration<SeekOrigin>(nameof(origin)),
            };

            //  Установка нового значения позиции.
            Position = position;

            //  Возвращает новую позицию.
            return position;
        }
    }

    /// <summary>
    /// Задает длину потока.
    /// Этот метод всегда создает исключение <see cref="NotSupportedException"/>.
    /// </summary>
    /// <param name="value">
    /// Этот параметр не используется.
    /// </param>
    /// <exception cref="NotSupportedException">
    /// При любом использовании этого свойства.
    /// </exception>
    public override void SetLength(long value) => throw ExceptionsCreator.OperationNotSupported();

    /// <summary>
    /// Записывает последовательность байтов в текущий поток и перемещает текущую позицию в нем вперед на число записанных байтов.
    /// </summary>
    /// <param name="buffer">
    /// Массив, который содержит значения для записи.
    /// </param>
    /// <param name="offset">
    /// Смещение в массиве <paramref name="buffer"/>, с которого начинается копирование элементов в текущий буфер.
    /// </param>
    /// <param name="count">
    /// Количество элементов, которые необходимо записать в текущий буфер.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="offset"/>.
    /// </exception>
    /// <exception cref="OverflowException">
    /// Операция записи привела к переполнению буфера:
    /// следующе значение длины <see cref="Length"/> превышает значение <see cref="long.MaxValue"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Произошла попытка обратиться к разрушенному объекту.
    /// </exception>
    public override void Write(byte[] buffer, int offset, int count)
    {
        //  Захват критического ресурса.
        lock (SyncRoot)
        {
            //  Проверка объекта.
            CheckDisposed();

            //  Запись в буфер.
            _Buffer.Write(buffer, offset, count);
        }
    }

    /// <summary>
    /// Освобождает неуправляемые ресурсы, используемые объектом Stream, а при необходимости освобождает также управляемые ресурсы.
    /// </summary>
    /// <param name="disposing">
    /// Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы;
    /// значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        //  Захват критического ресурса.
        lock (SyncRoot)
        {
            //  Установка флага.
            _IsDisposed = true;

            //  Удаление обработчика события изменения буфера.
            _Buffer.Changed -= Buffer_Changed;

            //  Вызов метода базового класса.
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Проверяет разрушен ли объект.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    void CheckDisposed()
    {
        //  Проверка значения, определяющего были ли освобождены ресурсы объекта.
        if (_IsDisposed)
        {
            //  В результате операции произошло обращение к разрушенному объекту.
            throw ExceptionsCreator.OperationObjectDisposed(nameof(CircularBufferStream));
        }
    }

    /// <summary>
    /// Выполняет проверку текущей позиции.
    /// </summary>
    /// <exception cref="OverflowException">
    /// Произошло переполнение буфера.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Произошла попытка обратиться к разрушенному объекту.
    /// </exception>
    void CheckPosition()
    {
        //  Проверка объекта.
        CheckDisposed();

        //  Проверка текущей позиции.
        if (_Position < _BeginPosition || _Position > _EndPosition)
        {
            throw ExceptionsCreator.OperationOverflow();
        }
    }

    /// <summary>
    /// Обрабатывает событие <see cref="CircularBuffer{T}.Changed"/>.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    void Buffer_Changed(object sender, CircularBufferEventArgs e)
    {
        //  Захват критического ресурса.
        lock (SyncRoot)
        {
            //  Изменение позиций.
            Interlocked.Exchange(ref _BeginPosition, e.BeginPosition);
            Interlocked.Exchange(ref _EndPosition, e.EndPosition);
        }
    }
}
