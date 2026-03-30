using Apeiron.Recording.Adxl357;
using System.IO;
using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет файл, содержащий пакеты данных.
/// </summary>
public sealed class PackageFile :
    File<SourceDirectory>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="dispatcher">
    /// Диспетчер.
    /// </param>
    /// <param name="parent">
    /// Родительский узел.
    /// </param>
    /// <param name="path">
    /// Путь к каталогу.
    /// </param>
    /// <param name="time">
    /// Время начала записи в файл.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dispatcher"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="parent"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    public PackageFile(Dispatcher dispatcher, SourceDirectory parent, string path, DateTime time) :
        base(dispatcher, new DirectoryInfo(path).Name, parent, path, time)
    {

    }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected override sealed void LoadCore()
    {
        //  Чтение массива данных.
        byte[] data = File.ReadAllBytes(Path);

        //  Создание потока для чтения.
        using MemoryStream stream = new(data);

        //  Создание средства чтения двоичных данных из памяти.
        using BinaryReader reader = new(stream);

        //  Блок перехвата некритических исключений.
        Invoker.SafeNotCritical(() =>
        {
            //  Чтение всех пакетов.
            while (true)
            {
                //  Определение положения следующего пакета.
                int position = (int)stream.Position;

                //  Чтение очередного пакета.
                Adxl357DataPackage dataPackage = Adxl357DataPackage.Load(stream);

                //  Создание пакета.
                Package package = new(Dispatcher, $"{position:00 000 000}", this, position, dataPackage);

                //  Загрузка пакета.
                package.Load();

                //  Добавление пакета в дерево.
                NodesProvider.Add(package);
            }
        });
    }

    /////// <summary>
    /////// Постоянная, определяющая сигнатуру пакета.
    /////// </summary>
    ////private const long _Signature = 0x545345544C494152;

    /////// <summary>
    /////// Постоянная, определяющая размер заголовка пакета в байтах.
    /////// </summary>
    ////private const int _HeaderSize = 20;

    /////// <summary>
    /////// Постоянная, определяющая размер контрольной суммы в байтах.
    /////// </summary>
    ////private const int _ChecksumSize = 4;

    /////// <summary>
    /////// Постоянная, определяющая максимальную длину синхронного сигнала.
    /////// </summary>
    ////private const int _MaxLength = 5592319;

    //private static bool TryParsePackage()
    //{
    //    ////  Проверка потока.
    //    //stream = Check.IsReadable(stream, nameof(stream));

    //    ////  Создание буфера для чтения данных пакета.
    //    //byte[] buffer = new byte[_HeaderSize];

    //    ////  Загрузка заголовка пакета.
    //    //stream.Read(buffer, 0, _HeaderSize);

    //    ////  Количество асинхронных значений.
    //    //int countAsyncValues;

    //    ////  Длина блока передаваемых данных.
    //    //int dataSize;

    //    ////  Количество синхронных значений.
    //    //int length;

    //    ////  Синхромаркер.
    //    //Adxl357Synchromarker synchromarker;

    //    ////  Создание потока для чтения из памяти.
    //    //using (MemoryStream memoryStream = new(buffer, false))
    //    //{
    //    //    //  Создание средства чтения двоичных данных из памяти.
    //    //    using BinaryReader reader = new(memoryStream);

    //    //    //  Чтение сигнатуры.
    //    //    long signature = reader.ReadInt64();

    //    //    //  Проверка сигнатуры.
    //    //    if (signature != _Signature)
    //    //    {
    //    //        //  Неверная сигнатура пакета данных Adxl.
    //    //        throw Exceptions.AdxlInvalidSignature();
    //    //    }

    //    //    //  Загрузка описания данных.
    //    //    uint description = reader.ReadUInt32();

    //    //    //  Определение количества асинхронных значений.
    //    //    countAsyncValues = (int)(description >> 24);

    //    //    //  Определение длины блока передаваемых данных.
    //    //    dataSize = (int)(0x00FFFFFF & description);

    //    //    //  Расчёт количества синхронных значений.
    //    //    length = ((dataSize >> 2) - countAsyncValues) / 3;

    //    //    //  Проверка количества синхронных значений.
    //    //    if (dataSize != ((length * 3 + countAsyncValues) << 2))
    //    //    {
    //    //        //  Неверный формат пакета данных Adxl.
    //    //        throw Exceptions.AdxlInvalidFormat();
    //    //    }

    //    //    //  Загрузка синхромаркера.
    //    //    synchromarker = reader.ReadObject<Adxl357Synchromarker>();
    //    //}

    //    ////  Изменение размера буфера.
    //    //Array.Resize(ref buffer, _HeaderSize + dataSize);

    //    ////  Загрузка данных пакета.
    //    //stream.Read(buffer, _HeaderSize, dataSize);

    //    ////  Создание средства чтения двоичных данных из исходного потока.
    //    //using (BinaryReader reader = new(stream, StreamHelper.DefaultEncoding, true))
    //    //{
    //    //    //  Чтение контрольной суммы.
    //    //    uint checksum = reader.ReadUInt32();

    //    //    //  Проверка контрольной суммы.
    //    //    if (checksum != Crc32.Compute(buffer))
    //    //    {
    //    //        //  Неверная контрольная сумма пакета данных Adxl.
    //    //        throw Exceptions.AdxlInvalidChecksum();
    //    //    }
    //    //}

    //    ////  Создание потока для чтения из памяти.
    //    //using (MemoryStream memoryStream = new(buffer, _HeaderSize, dataSize))
    //    //{
    //    //    //  Создание средства чтения двоичных данных из памяти.
    //    //    using BinaryReader reader = new(memoryStream);

    //    //    //  Создание массива асинхронных данных.
    //    //    float[] asyncValues = new float[countAsyncValues];

    //    //    //  Чтение массива асинхронных данных.
    //    //    for (int i = 0; i < countAsyncValues; i++)
    //    //    {
    //    //        //  Чтение асинхронного значения.
    //    //        asyncValues[i] = reader.ReadSingle();
    //    //    }

    //    //    //  Создание массивов синхронных данных.
    //    //    float[] xSignal = new float[length];
    //    //    float[] ySignal = new float[length];
    //    //    float[] zSignal = new float[length];

    //    //    //  Чтение синхронных данных.
    //    //    for (int i = 0; i < length; i++)
    //    //    {
    //    //        xSignal[i] = reader.ReadSingle();
    //    //        ySignal[i] = reader.ReadSingle();
    //    //        zSignal[i] = reader.ReadSingle();
    //    //    }

    //    //    //  Установка массивов данных синхронных сигналов.
    //    //    _XSignalData = xSignal;
    //    //    _YSignalData = ySignal;
    //    //    _ZSignalData = zSignal;

    //    //    //  Установка синхромаркера.
    //    //    Synchromarker = synchromarker;

    //    //    //  Установка длины синхронных сигналов.
    //    //    _Length = length;

    //    //    //  Установка асинхронных значений.
    //    //    AsyncValues.Clear();
    //    //    AsyncValues.AddRange(asyncValues);

    //    //    //  Установка данных сигналов.
    //    //    Signals.XSignal.SetData(xSignal);
    //    //    Signals.YSignal.SetData(ySignal);
    //    //    Signals.ZSignal.SetData(zSignal);
    //    //}

    //    ////  Возврат прочитанного объекта.
    //    //return this;

    //}
}
