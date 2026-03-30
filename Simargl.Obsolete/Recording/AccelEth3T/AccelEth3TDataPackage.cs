using Simargl.Cryptography;
using Simargl.Recording.Stm32;
using System.Text;
using System.IO;
using Simargl.IO;
using Simargl.Designing.Utilities;

namespace Simargl.Recording.AccelEth3T;

/// <summary>
/// Представляет пакет данных датчика AccelEth3T.
/// </summary>
public sealed class AccelEth3TDataPackage
{
    /// <summary>
    /// Постоянная, определяющая сигнатуру пакета.
    /// </summary>
    internal const long Signature = 0x545345544C494152;

    /// <summary>
    /// Постоянная, определяющая размер заголовка пакета в байтах.
    /// </summary>
    private const int _HeaderSize = 20;

    /// <summary>
    /// Постоянная, определяющая размер контрольной суммы в байтах.
    /// </summary>
    private const int _ChecksumSize = 4;

    /// <summary>
    /// Постоянная, определяющая максимальную длину синхронного сигнала.
    /// </summary>
    private const int _MaxLength = 5592319;

    /// <summary>
    /// Поле для хранения массива данных сигнала X.
    /// </summary>
    private float[] _XSignalData;

    /// <summary>
    /// Поле для хранения массива данных сигнала Y.
    /// </summary>
    private float[] _YSignalData;

    /// <summary>
    /// Поле для хранения массива данных сигнала Z.
    /// </summary>
    private float[] _ZSignalData;

    /// <summary>
    /// Поле для хранения длины синхронных сигналов.
    /// </summary>
    private int _Length;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public AccelEth3TDataPackage()
    {
        //  Инициализация массивов синхронных данных.
        _XSignalData = [];
        _YSignalData = [];
        _ZSignalData = [];

        //  Установка длины синхронных сигналов.
        _Length = 0;

        //  Создание коллекции асинхронных значений.
        AsyncValues = [];

        //  Создание коллекции сигналов ускорений.
        Signals = [];
    }

    /// <summary>
    /// Возвращает или задаёт синхромаркер.
    /// </summary>
    public Stm32Synchromarker Synchromarker { get; set; }

    /// <summary>
    /// Возвращает коллекцию асинхронных значений.
    /// </summary>
    public AccelEth3TAsyncValueCollection AsyncValues { get; }

    /// <summary>
    /// Возвращает коллекцию сигналов.
    /// </summary>
    public AccelEth3TSignalCollection Signals { get; }

    /// <summary>
    /// Возвращает размер пакета в байтах.
    /// </summary>
    public int Size => _HeaderSize + (AsyncValues.Count + _Length * Signals.Count) * sizeof(float) + _ChecksumSize;

    /// <summary>
    /// Возвращает или задаёт длину синхронных сигналов.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    /// <exception cref="OverflowException">
    /// Операция привела к переполнению.
    /// </exception>
    public int Length
    {
        get => _Length;
        set
        {
            //  Проверка изменения длины.
            if (_Length != value)
            {
                //  Проверка нового значения длины.
                IsNotNegative(value, nameof(Length));

                //  Проверка переполнения.
                if (value > _MaxLength)
                {
                    //  Операция привела к переполнению.
                    throw ExceptionsCreator.OperationOverflow();
                }

                //  Установка нового значения.
                _Length = value;

                //  Проверка новой длины на нулевое значения.
                if (value != 0)
                {
                    //  Изменение размеров массивов.
                    Array.Resize(ref _XSignalData, value);
                    Array.Resize(ref _YSignalData, value);
                    Array.Resize(ref _ZSignalData, value);
                }
                else
                {
                    //  Установка массивов нулевой длины.
                    _XSignalData = [];
                    _YSignalData = [];
                    _ZSignalData = [];
                }

                //  Установка данных сигналов.
                Signals.XSignal.SetData(_XSignalData);
                Signals.YSignal.SetData(_YSignalData);
                Signals.ZSignal.SetData(_ZSignalData);
            }
        }
    }

    /// <summary>
    /// Сохраняет данные объекта в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток, в который необходимо сохранить данные.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Поток не поддерживает запись.
    /// </exception>
    public void Save(Stream stream)
    {
        //  Проверка потока.
        stream = IsWritable(stream, nameof(stream));

        //  Создание буфера для записи данных пакета.
        byte[] buffer = new byte[Size - _ChecksumSize];

        //  Создание потока для записи в память.
        using (MemoryStream memoryStream = new(buffer))
        {
            //  Создание средства записи двоичных данных в память.
            using BinaryWriter writer = new(memoryStream);

            //  Запись сигнатуры.
            writer.Write(Signature);

            //  Получение количества синхронных значений.
            int length = _Length;

            //  Получение количества асинхронных значений.
            int countAsyncValues = AsyncValues.Count;

            //  Расчёт длины блока передаваемых данных.
            int dataSize = length * 3 + countAsyncValues << 2;

            //  Расчёт описания данных.
            uint description = ((uint)countAsyncValues << 24) + (uint)(0x00FFFFFF & dataSize);

            //  Запись описания данных.
            writer.Write(description);

            //  Запись синхромаркера.
            writer.Write(Synchromarker.Value);

            //  Запись асинхронных данных.
            foreach (float value in AsyncValues)
            {
                //  Запись асинхронного значения.
                writer.Write(value);
            }

            //  Получение массивов синхронных данных.
            float[] xSignal = _XSignalData;
            float[] ySignal = _YSignalData;
            float[] zSignal = _ZSignalData;

            //  Запись синхронных данных.
            for (int i = 0; i < length; i++)
            {
                writer.Write(xSignal[i]);
                writer.Write(ySignal[i]);
                writer.Write(zSignal[i]);
            }
        }

        //  Создание средства записи двоичных данных в целевой поток.
        using (BinaryWriter writer = new(stream, Encoding.UTF8, true))
        {
            //  Запись буфера.
            writer.Write(buffer);

            //  Запись контрольной суммы.
            writer.Write(Crc32.Compute(buffer));
        }
    }

    /// <summary>
    /// Загружает пакет данных из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить данные.
    /// </param>
    /// <returns>
    /// Загруженный пакет данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверная сигнатура пакета данных Adxl.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат пакета данных Adxl.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверная контрольная сумма пакета данных Adxl.
    /// </exception>
    public static AccelEth3TDataPackage Load(Stream stream)
    {
        //  Проверка потока.
        stream = IsReadable(stream, nameof(stream));

        //  Создание буфера для чтения данных пакета.
        byte[] buffer = new byte[_HeaderSize];

        //  Загрузка заголовка пакета.
        stream.ReadExactly(buffer, 0, _HeaderSize);

        //  Количество асинхронных значений.
        int countAsyncValues;

        //  Длина блока передаваемых данных.
        int dataSize;

        //  Количество синхронных значений.
        int length;

        //  Синхромаркер.
        Stm32Synchromarker synchromarker;

        //  Создание потока для чтения из памяти.
        using (MemoryStream memoryStream = new(buffer, false))
        {
            //  Создание средства чтения двоичных данных из памяти.
            using BinaryReader reader = new(memoryStream);

            //  Чтение сигнатуры.
            long signature = reader.ReadInt64();

            //  Проверка сигнатуры.
            if (signature != Signature)
            {
                //  Неверная сигнатура пакета данных Adxl.
                throw new InvalidDataException("Неверная сигнатура пакета данных Adxl.");
            }

            //  Загрузка описания данных.
            uint description = reader.ReadUInt32();

            //  Определение количества асинхронных значений.
            countAsyncValues = (int)(description >> 24);

            //  Определение длины блока передаваемых данных.
            dataSize = (int)(0x00FFFFFF & description);

            //  Расчёт количества синхронных значений.
            length = ((dataSize >> 2) - countAsyncValues) / 3;

            //  Проверка количества синхронных значений.
            if (dataSize != length * 3 + countAsyncValues << 2)
            {
                //  Неверный формат пакета данных Adxl.
                throw new InvalidDataException("Неверный формат пакета данных Adxl.");
            }

            //  Загрузка синхромаркера.
            synchromarker = new(reader.ReadInt64());
        }

        //  Изменение размера буфера.
        Array.Resize(ref buffer, _HeaderSize + dataSize);

        //  Загрузка данных пакета.
        stream.ReadExactly(buffer, _HeaderSize, dataSize);

        //  Создание средства чтения двоичных данных из исходного потока.
        using (BinaryReader reader = new(stream, Encoding.UTF8, true))
        {
            //  Чтение контрольной суммы.
            uint checksum = reader.ReadUInt32();

            //  Проверка контрольной суммы.
            if (checksum != Crc32.Compute(buffer))
            {
                //  Неверная контрольная сумма пакета данных Adxl.
                throw new InvalidDataException("Неверная контрольная сумма пакета данных Adxl.");
            }
        }

        //  Создание пакета.
        AccelEth3TDataPackage package = new();

        //  Создание потока для чтения из памяти.
        using (MemoryStream memoryStream = new(buffer, _HeaderSize, dataSize))
        {
            //  Создание средства чтения двоичных данных из памяти.
            using BinaryReader reader = new(memoryStream);

            //  Создание массива асинхронных данных.
            float[] asyncValues = new float[countAsyncValues];

            //  Чтение массива асинхронных данных.
            for (int i = 0; i < countAsyncValues; i++)
            {
                //  Чтение асинхронного значения.
                asyncValues[i] = reader.ReadSingle();
            }

            //  Создание массивов синхронных данных.
            float[] xSignal = new float[length];
            float[] ySignal = new float[length];
            float[] zSignal = new float[length];

            //  Чтение синхронных данных.
            for (int i = 0; i < length; i++)
            {
                xSignal[i] = reader.ReadSingle();
                ySignal[i] = reader.ReadSingle();
                zSignal[i] = reader.ReadSingle();
            }

            //  Установка массивов данных синхронных сигналов.
            package._XSignalData = xSignal;
            package._YSignalData = ySignal;
            package._ZSignalData = zSignal;

            //  Установка синхромаркера.
            package.Synchromarker = synchromarker;

            //  Установка длины синхронных сигналов.
            package._Length = length;

            //  Установка асинхронных значений.
            package.AsyncValues.Clear();
            package.AsyncValues.AddRange(asyncValues);

            //  Установка данных сигналов.
            package.Signals.XSignal.SetData(xSignal);
            package.Signals.YSignal.SetData(ySignal);
            package.Signals.ZSignal.SetData(zSignal);
        }

        //  Возврат прочитанного пакета.
        return package;
    }

    /// <summary>
    /// Асинхронно сохраняет данные объекта в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток, в который необходимо сохранить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая операцию сохранения.
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
        stream = IsWritable(stream, nameof(stream));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание буфера для записи данных пакета.
        byte[] buffer = new byte[Size - _ChecksumSize];

        //  Создание потока для записи в память.
        using (MemoryStream memoryStream = new(buffer))
        {
            //  Создание средства записи двоичных данных в память.
            Spreader spreader = new(memoryStream);

            //  Запись сигнатуры.
            await spreader.WriteInt64Async(Signature, cancellationToken).ConfigureAwait(false);

            //  Получение количества синхронных значений.
            int length = _Length;

            //  Получение количества асинхронных значений.
            int countAsyncValues = AsyncValues.Count;

            //  Расчёт длины блока передаваемых данных.
            int dataSize = length * 3 + countAsyncValues << 2;

            //  Расчёт описания данных.
            uint description = ((uint)countAsyncValues << 24) + (uint)(0x00FFFFFF & dataSize);

            //  Запись описания данных.
            await spreader.WriteUInt32Async(description, cancellationToken).ConfigureAwait(false);

            //  Запись синхромаркера.
            await spreader.WriteInt64Async(Synchromarker.Value, cancellationToken).ConfigureAwait(false);

            //  Запись асинхронных данных.
            foreach (float value in AsyncValues)
            {
                //  Запись асинхронного значения.
                await spreader.WriteFloat32Async(value, cancellationToken).ConfigureAwait(false);
            }

            //  Получение массивов синхронных данных.
            float[] xSignal = _XSignalData;
            float[] ySignal = _YSignalData;
            float[] zSignal = _ZSignalData;

            //  Запись синхронных данных.
            for (int i = 0; i < length; i++)
            {
                await spreader.WriteFloat32Async(xSignal[i], cancellationToken).ConfigureAwait(false);
                await spreader.WriteFloat32Async(ySignal[i], cancellationToken).ConfigureAwait(false);
                await spreader.WriteFloat32Async(zSignal[i], cancellationToken).ConfigureAwait(false);
            }
        }

        //  Запись данных в целевой поток.
        {
            //  Создание средства записи двоичных данных в целевой поток.
            Spreader spreader = new(stream);

            //  Запись буфера.
            await spreader.WriteBytesAsync(buffer, cancellationToken).ConfigureAwait(false);

            //  Запись контрольной суммы.
            await spreader.WriteUInt32Async(Crc32.Compute(buffer), cancellationToken).ConfigureAwait(false);
        }
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Обращение к потоку произошло после его закрытия.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверная сигнатура пакета данных Adxl.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат пакета данных Adxl.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверная контрольная сумма пакета данных Adxl.
    /// </exception>
    public static async Task<AccelEth3TDataPackage> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        stream = IsReadable(stream, nameof(stream));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание буфера для чтения данных пакета.
        byte[] buffer = new byte[_HeaderSize];

        {
            //  Создание средства чтения двоичных данных из исходного потока.
            Spreader spreader = new(stream);

            //  Загрузка заголовка пакета.
            await spreader.ReadBytesAsync(buffer, 0, _HeaderSize, cancellationToken).ConfigureAwait(false);
        }

        //  Количество асинхронных значений.
        int countAsyncValues;

        //  Длина блока передаваемых данных.
        int dataSize;

        //  Количество синхронных значений.
        int length;

        //  Синхромаркер.
        Stm32Synchromarker synchromarker;

        //  Создание потока для чтения из памяти.
        using (MemoryStream memoryStream = new(buffer, false))
        {
            //  Создание средства чтения двоичных данных из памяти.
            Spreader spreader = new(memoryStream);

            //  Чтение сигнатуры.
            long signature = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);

            //  Проверка сигнатуры.
            if (signature != Signature)
            {
                //  Неверная сигнатура пакета данных Adxl.
                throw new InvalidDataException("Неверная сигнатура пакета данных Adxl.");
            }

            //  Загрузка описания данных.
            uint description = await spreader.ReadUInt32Async(cancellationToken).ConfigureAwait(false);

            //  Определение количества асинхронных значений.
            countAsyncValues = (int)(description >> 24);

            //  Определение длины блока передаваемых данных.
            dataSize = (int)(0x00FFFFFF & description);

            //  Расчёт количества синхронных значений.
            length = ((dataSize >> 2) - countAsyncValues) / 3;

            //  Проверка количества синхронных значений.
            if (dataSize != length * 3 + countAsyncValues << 2)
            {
                //  Неверный формат пакета данных Adxl.
                throw new InvalidDataException("Неверный формат пакета данных Adxl.");
            }

            //  Загрузка синхромаркера.
            synchromarker = new(await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false));
        }

        //  Изменение размера буфера.
        Array.Resize(ref buffer, _HeaderSize + dataSize);


        {
            //  Создание средства чтения двоичных данных из исходного потока.
            Spreader spreader = new(stream);

            //  Загрузка данных пакета.
            await spreader.ReadBytesAsync(buffer, _HeaderSize, dataSize, cancellationToken).ConfigureAwait(false);

            //  Чтение контрольной суммы.
            uint checksum = await spreader.ReadUInt32Async(cancellationToken).ConfigureAwait(false);

            //  Проверка контрольной суммы.
            if (checksum != Crc32.Compute(buffer))
            {
                //  Неверная контрольная сумма пакета данных Adxl.
                throw new InvalidDataException("Неверная контрольная сумма пакета данных Adxl.");
            }
        }

        //  Создание пакета.
        AccelEth3TDataPackage package = new();

        //  Создание потока для чтения из памяти.
        using (MemoryStream memoryStream = new(buffer, _HeaderSize, dataSize))
        {
            //  Создание средства чтения двоичных данных из памяти.
            Spreader spreader = new(memoryStream);

            //  Создание массива асинхронных данных.
            float[] asyncValues = new float[countAsyncValues];

            //  Чтение массива асинхронных данных.
            for (int i = 0; i < countAsyncValues; i++)
            {
                //  Чтение асинхронного значения.
                asyncValues[i] = await spreader.ReadFloat32Async(cancellationToken).ConfigureAwait(false);
            }

            //  Создание массивов синхронных данных.
            float[] xSignal = new float[length];
            float[] ySignal = new float[length];
            float[] zSignal = new float[length];

            //  Чтение синхронных данных.
            for (int i = 0; i < length; i++)
            {
                xSignal[i] = await spreader.ReadFloat32Async(cancellationToken).ConfigureAwait(false);
                ySignal[i] = await spreader.ReadFloat32Async(cancellationToken).ConfigureAwait(false);
                zSignal[i] = await spreader.ReadFloat32Async(cancellationToken).ConfigureAwait(false);
            }

            //  Установка массивов данных синхронных сигналов.
            package._XSignalData = xSignal;
            package._YSignalData = ySignal;
            package._ZSignalData = zSignal;

            //  Установка синхромаркера.
            package.Synchromarker = synchromarker;

            //  Установка длины синхронных сигналов.
            package._Length = length;

            //  Установка асинхронных значений.
            package.AsyncValues.Clear();
            package.AsyncValues.AddRange(asyncValues);

            //  Установка данных сигналов.
            package.Signals.XSignal.SetData(xSignal);
            package.Signals.YSignal.SetData(ySignal);
            package.Signals.ZSignal.SetData(zSignal);
        }

        //  Возврат прочитанного пакета.
        return package;
    }
}
