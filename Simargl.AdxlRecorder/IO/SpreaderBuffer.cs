namespace Simargl.AdxlRecorder.IO;

/// <summary>
/// Представляет буфер распределителя данных потока.
/// </summary>
internal sealed class SpreaderBuffer
{
    /// <summary>
    /// Постоянная, определяющая размер буфера.
    /// </summary>
    private const int _BufferSize = 16;

    /// <summary>
    /// Поле для хранения буфера.
    /// </summary>
    private readonly byte[] _Buffer;

    /// <summary>
    /// Поле для хранения ядра распределителя данных потока.
    /// </summary>
    private readonly SpreaderCore _Core;

    /// <summary>
    /// Поле для хранения потока в памяти.
    /// </summary>
    private readonly MemoryStream _MemoryStream;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="core">
    /// Ядро распределителя данных потока.
    /// </param>
    public SpreaderBuffer([NoVerify] SpreaderCore core)
    {
        //  Установка ядра распределителя данных потока.
        _Core = core;

        //  Создание буфера.
        _Buffer = new byte[_BufferSize];

        //  Создание потока в памяти.
        _MemoryStream = new(_Buffer);

        //  Создание средства для чтения двоичных данных из буфера.
        Reader = new(_MemoryStream, Encoding.UTF8, true);

        //  Создание средства для записи двоичных данных в буфер.
        Writer = new(_MemoryStream, Encoding.UTF8, true);

        //  Установка значения, определяющего используется ли порядок байтов от старшего к младшему.
        IsBigEndian = false;
    }

    /// <summary>
    /// Возвращает средство для чтения двоичных данных из буфера.
    /// </summary>
    public BinaryReader Reader { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }

    /// <summary>
    /// Возвращает средство для записи двоичных данных в буфер.
    /// </summary>
    public BinaryWriter Writer { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее используется ли порядок байтов от старшего к младшему.
    /// </summary>
    public bool IsBigEndian { get; set; }

    /// <summary>
    /// Выполняет сброс буфера.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        //  Установка потока в памяти на начальную позицию.
        _MemoryStream.Position = 0;
    }

    /// <summary>
    /// Загружает данные в буфер из исходного потока.
    /// </summary>
    /// <param name="count">
    /// Количество байт, которые необходимо загрузить из исходного потока.
    /// </param>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Load([NoVerify] int count)
    {
        //  Чтение данных из исходного потока.
        _Core.ReadBytes(_Buffer, 0, count);

        //  Нормализация последовательности байт.
        EndianNormalization(count);
    }

    /// <summary>
    /// Асинхронно загружает данные в буфер из исходного потока.
    /// </summary>
    /// <param name="count">
    /// Количество байт, которые необходимо загрузить из исходного потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая загрузку данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task LoadAsync([NoVerify] int count, CancellationToken cancellationToken)
    {
        //  Чтение данных из исходного потока.
        await _Core.ReadBytesAsync(_Buffer, 0, count, cancellationToken).ConfigureAwait(false);

        //  Нормализация последовательности байт.
        EndianNormalization(count);
    }

    /// <summary>
    /// Сохраняет данные из буфера в исходный поток.
    /// </summary>
    /// <param name="count">
    /// Количество байт, которые необходимо сохранить в исходном потоке.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Save([NoVerify] int count)
    {
        //  Нормализация последовательности байт.
        EndianNormalization(count);

        //  Сохранение данных в поток.
        _Core.WriteBytes(_Buffer, 0, count);
    }

    /// <summary>
    /// Асинхронно сохраняет данные из буфера в исходный поток.
    /// </summary>
    /// <param name="count">
    /// Количество байт, которые необходимо сохранить в исходном потоке.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сохранение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task SaveAsync([NoVerify] int count, CancellationToken cancellationToken)
    {
        //  Нормализация последовательности байт.
        EndianNormalization(count);

        //  Сохранение данных в поток.
        await _Core.WriteBytesAsync(_Buffer, 0, count, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет нормализацию последовательности байт.
    /// </summary>
    /// <param name="count">
    /// Количество байт для нормализации.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EndianNormalization(int count)
    {
        //  Проверка значения, определяющего используется ли порядок байтов от старшего к младшему.
        if (IsBigEndian)
        {
            //  Проверка количества байт для нормализации.
            switch (count)
            {
                case 0:
                case 1:
                    //  Завершение проверки.
                    break;
                case 2:
                    //  Изменение порядка байт.
                    (_Buffer[0], _Buffer[1]) = (_Buffer[1], _Buffer[0]);

                    //  Завершение проверки.
                    break;
                case 4:
                    //  Изменение порядка байт.
                    (_Buffer[0], _Buffer[3]) = (_Buffer[3], _Buffer[0]);
                    (_Buffer[1], _Buffer[2]) = (_Buffer[2], _Buffer[1]);

                    //  Завершение проверки.
                    break;
                case 8:
                    //  Изменение порядка байт.
                    (_Buffer[0], _Buffer[7]) = (_Buffer[7], _Buffer[0]);
                    (_Buffer[1], _Buffer[6]) = (_Buffer[6], _Buffer[1]);
                    (_Buffer[2], _Buffer[5]) = (_Buffer[5], _Buffer[2]);
                    (_Buffer[3], _Buffer[4]) = (_Buffer[4], _Buffer[3]);

                    //  Завершение проверки.
                    break;
                case 16:
                    //  Изменение порядка байт.
                    (_Buffer[0], _Buffer[15]) = (_Buffer[15], _Buffer[0]);
                    (_Buffer[1], _Buffer[14]) = (_Buffer[14], _Buffer[1]);
                    (_Buffer[2], _Buffer[13]) = (_Buffer[13], _Buffer[2]);
                    (_Buffer[3], _Buffer[12]) = (_Buffer[12], _Buffer[3]);
                    (_Buffer[4], _Buffer[11]) = (_Buffer[11], _Buffer[4]);
                    (_Buffer[5], _Buffer[10]) = (_Buffer[10], _Buffer[5]);
                    (_Buffer[6], _Buffer[9]) = (_Buffer[9], _Buffer[6]);
                    (_Buffer[7], _Buffer[8]) = (_Buffer[8], _Buffer[7]);

                    //  Завершение проверки.
                    break;
                default:
                    //  Определение половины размера.
                    int half = count >> 1;

                    //  Перебор байт.
                    for (int i = 0; i < half >> 1; i++)
                    {
                        //  Определение второго индекса для обмена.
                        int j = count - i - 1;

                        //  Изменение порядка байт.
                        (_Buffer[i], _Buffer[j]) = (_Buffer[j], _Buffer[i]);
                    }

                    //  Завершение проверки.
                    break;
            }
        }
    }
}
