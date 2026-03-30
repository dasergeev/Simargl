using Apeiron.Analysis.Transforms;
using Apeiron.Analysis;
using System.IO;

namespace Apeiron.Platform.Demo.AdxlDemo.Channels;

/// <summary>
/// Представляет фрагмент канала.
/// </summary>
public sealed class ChannelFragment
{
    /// <summary>
    /// Поле для хранения массива значений.
    /// </summary>
    private double[] _Items;

    /// <summary>
    /// Поле для хранения текущей статистики.
    /// </summary>
    private ChannelStatistics _Statistics;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="info">
    /// Информация о фрагменте.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="info"/> передана пустая ссылка.
    /// </exception>
    public ChannelFragment(ChannelFragmentInfo info)
    {
        //  Проверка ссылки на информацию.
        IsNotNull(info, nameof(info));

        //  Загрузка двоичных данных.
        byte[] bytes = File.ReadAllBytes(info.Path);

        //  Создание массива данных.
        _Items = new double[bytes.Length / sizeof(double)];

        //  Копирование данных.
        Buffer.BlockCopy(bytes, 0, _Items, 0, bytes.Length);

        //  Получение статистики.
        _Statistics = new()
        {
            Count = info.Count,
            Sum = info.Sum,
            SumSquares = info.SumSquares,
            MinValue = info.MinValue,
            MaxValue = info.MaxValue,
            Average = info.Average,
            Deviation = info.Deviation,
        };

        //  Получение информации о фрагменте.
        Sampling = info.Sampling;
        Cutoff = info.Cutoff;
        Duration = info.EndTime - info.BeginTime;
        BeginTime = info.BeginTime;

        //  Установка значения, определяющего, запечатан ли фрагмент.
        IsSealed = true;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sampling">
    /// Частота дискретизации.
    /// </param>
    /// <param name="cutoff">
    /// Частота среза фильтра.
    /// </param>
    /// <param name="beginTime">
    /// Время начала записи фрагмента.
    /// </param>
    public ChannelFragment(double sampling, double cutoff, DateTime beginTime)
    {
        //  Установка пустого массива значений.
        _Items = Array.Empty<double>();

        //  Установка значений свойств.
        Sampling = sampling;
        Cutoff = cutoff;
        BeginTime = beginTime;
        Duration = TimeSpan.Zero;
    }

    /// <summary>
    /// Возвращает частоту дискретизации.
    /// </summary>
    public double Sampling { get; }

    /// <summary>
    /// Возвращает частоту среза фильтра.
    /// </summary>
    public double Cutoff { get; }

    /// <summary>
    /// Возвращает длительность фрагмента.
    /// </summary>
    public TimeSpan Duration { get; private set; }

    /// <summary>
    /// Возвращает время начала фрагмента.
    /// </summary>
    public DateTime BeginTime { get; }

    /// <summary>
    /// Возвращает время окончания фрагмента.
    /// </summary>
    public DateTime EndTime => BeginTime + Duration;

    /// <summary>
    /// Возвращает значение, определяющее, запечатан ли фрагмент.
    /// </summary>
    public bool IsSealed { get; private set; }

    /// <summary>
    /// Возвращает длину фрагмента.
    /// </summary>
    public int Length => _Items.Length;

    /// <summary>
    /// Возвращает двоичные данные.
    /// </summary>
    /// <returns>
    /// Двоичные данные.
    /// </returns>
    public byte[] GetBinary()
    {
        //  Создание массива двоичных данных.
        byte[] bytes = new byte[Length * sizeof(double)];

        //  Копирование данных.
        Buffer.BlockCopy(_Items, 0, bytes, 0, bytes.Length);

        //  Возврат массива данных.
        return bytes;
    }

    /// <summary>
    /// Возвращает текущие данные.
    /// </summary>
    /// <returns>
    /// Текущие данные.
    /// </returns>
    public double[] GetData()
    {
        //  Получение копии элементов.
        double[] data = (double[])_Items.Clone();

        //  Проверка запечатанных данных.
        if (!IsSealed)
        {
            //  Проверка длины массива данных.
            if (data.Length > 1)
            {
                //  Создание сигнала.
                Signal signal = new(Sampling, new(data));

                //  Создание фильтра.
                ButterworthFilter filter = new(Cutoff, 4);

                //  Фильтрация.
                filter.Invoke(signal);

                //  Получение фильтрованных данных.
                data = signal.Items;
            }
        }

        //  Возврат массива данных.
        return data;
    }

    /// <summary>
    /// Возвращает текущую статистику.
    /// </summary>
    /// <returns>
    /// Текущая статистика.
    /// </returns>
    public ChannelStatistics GetStatistics()
    {
        //  Проверка запечатанных данных.
        if (!IsSealed)
        {
            //  Получение статистики.
            _Statistics = ChannelStatistics.FromData(_Items);
        }

        //  Возврат статистики.
        return _Statistics;
    }

    /// <summary>
    /// Асинхронно запечатывает фрагмент.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, запечатывающая фрагмент.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task SealAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка значения, определяющего, запечатан ли фрагмент.
        if (!IsSealed)
        {
            //  Проверка длины массива данных.
            if (_Items.Length > 1)
            {
                //  Создание сигнала.
                Signal signal = new(Sampling, new(_Items));

                //  Создание фильтра.
                ButterworthFilter filter = new(Cutoff, 4);

                //  Фильтрация.
                filter.Invoke(signal);

                //  Получение фильтрованных данных.
                _Items = signal.Items;

                //  Получение статистики.
                _Statistics = ChannelStatistics.FromData(_Items);
            }

            //  Установка значения, определяющего, запечатан ли фрагмент.
            IsSealed = true;
        }
    }

    /// <summary>
    /// Асинхронно добавляет данные.
    /// </summary>
    /// <param name="buffer">
    /// Буфер значений.
    /// </param>
    /// <param name="beginTime">
    /// Время начала записи данных.
    /// </param>
    /// <param name="duration">
    /// Длительность записи.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая добавление данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task AddDataAsync(double[] buffer, DateTime beginTime, TimeSpan duration, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Корректировка длительности.
        Duration = (beginTime + duration) - BeginTime;

        //  Определение новой длины массива.
        int length = (int)Math.Round(Duration.TotalSeconds * Sampling);

        //  Определение количества новых элементов.
        int count = length - _Items.Length;

        //  Корректировка буфера.
        buffer = Resampling(buffer, count);

        //  Изменение массива элементов.
        Array.Resize(ref _Items, length);

        //  Копирование новых элементов.
        Array.Copy(buffer, 0, _Items, length - count, count);
    }

    /// <summary>
    /// Выполняет изменение частоты дискретизации данных.
    /// </summary>
    /// <param name="buffer">
    /// Исходный буфер данных.
    /// </param>
    /// <param name="length">
    /// Новая длина данных.
    /// </param>
    /// <returns>
    /// Буфер после коррекции.
    /// </returns>
    private static double[] Resampling(double[] buffer, int length)
    {
        //  Проверка необходимости коррекции.
        if (buffer.Length == length)
        {
            //  Возврат исходного буфера.
            return buffer;
        }

        //  Создание нового буфера.
        double[] newBuffer = new double[length];

        //  Проверка исходного буфера.
        if (buffer.Length > 0)
        {
            //  Заполнение буфера.
            for (int i = 0; i < length; i++)
            {
                //  Расчёт исходного индекса.
                int sourceIndex = (int)Math.Round((i / (double)length) * buffer.Length);

                //  Корректировка исходного индекса.
                if (sourceIndex < 0) sourceIndex = 0;
                if (sourceIndex >= buffer.Length) sourceIndex = buffer.Length - 1;

                //  Установка значения.
                newBuffer[i] = buffer[sourceIndex];
            }
        }

        //  Возврат нового буфера.
        return newBuffer;
    }
}
