using System.Collections.ObjectModel;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет каталог необработанных данных.
/// </summary>
public class RawDirectory
{
    /// <summary>
    /// Постоянная, определяющая имя каталога, содержащего GPS-данные.
    /// </summary>
    private const string _GpsDirectoryName = "Gps";

    /// <summary>
    /// Постоянная, определяющая префикс имени каталога, содержащего пакеты данных.
    /// </summary>
    private const string _PackageDirectoryPrefix = "Adxl";

    /// <summary>
    /// Возвращает или инициализирует идентификатор каталога.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Возвращает или задаёт идентификатор регистратора.
    /// </summary>
    public int RegistrarId { get; set; }

    /// <summary>
    /// Возвращает или задаёт путь к каталогу.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт идентификатор внутреннего каталога.
    /// </summary>
    public long InternalDirectoryId { get; set; }

    /// <summary>
    /// Возвращает или задаёт регистратор.
    /// </summary>
    public Registrar Registrar { get; set; } = null!;

    /// <summary>
    /// Возвращает коллекцию файлов с пакетами данных.
    /// </summary>
    public ObservableCollection<PackageFile> PackageFiles { get; } = new();

    /// <summary>
    /// Возвращает коллекцию файлов с NMEA сообщениями.
    /// </summary>
    public ObservableCollection<NmeaFile> NmeaFiles { get; } = new();

    /// <summary>
    /// Возвращает коллекцию пакетов данных.
    /// </summary>
    public ObservableCollection<Package> Packages { get; } = new();

    /// <summary>
    /// Возвращает коллекцию сообщений GPS, содержащих данные местоположения.
    /// </summary>
    public ObservableCollection<GgaMessage> GgaMessages { get; } = new();

    /// <summary>
    /// Возвращает коллекцию сообщений GPS, содержащих минимальный рекомендованный набор данных.
    /// </summary>
    public ObservableCollection<RmcMessage> RmcMessages { get; } = new();

    /// <summary>
    /// Возвращает коллекцию сообщений GPS, содержащих данные о наземном курсе и скорости.
    /// </summary>
    public ObservableCollection<VtgMessage> VtgMessages { get; } = new();

    /// <summary>
    /// Выполняет разбор имени каталога.
    /// </summary>
    /// <param name="name">
    /// Имя каталога.
    /// </param>
    /// <param name="format">
    /// Формат файла.
    /// </param>
    /// <returns>
    /// Результат разбора.
    /// </returns>
    public static bool TryParseDirectoryFormat(string name, out int format)
    {
        //  Установка значения по умолчанию.
        format = -1;

        //  Проверка каталога GPS-данных.
        if (name == _GpsDirectoryName)
        {
            //  Установка формата файла.
            format = 0;

            //  Имя успешно разобрано.
            return true;
        }
        else if (
            name.Length == 7 &&
            name[..4] == _PackageDirectoryPrefix &&
            int.TryParse(name[4..], out int number))
        {
            //  Установка формата файла.
            format = number;

            //  Имя успешно разобрано.
            return true;
        }
        else
        {
            //  Не удалось разобрать имя.
            return false;
        }
    }

    /// <summary>
    /// Возвращает дату файла по его имени.
    /// </summary>
    /// <param name="name">
    /// Имя файла.
    /// </param>
    /// <param name="time">
    /// Дата файла.
    /// </param>
    /// <returns>
    /// Результат разбора.
    /// </returns>
    public static bool TryParseFileTime(string name, out DateTime time)
    {
        //  Установка времени по умолчанию.
        time = DateTime.Now;

        //  Проверка имени.
        if (name is null)
        {
            //  Неверное имя файла.
            return false;
        }

        //  Разбивка имени файла по разделителю.
        string[] parts = name.Split('.');

        //  Проверка количества частей при разбивке.
        if (parts.Length != 2)
        {
            //  Неверное имя файла.
            return false;
        }

        //  Установка основной части имени.
        name = parts[0];

        //  Разбивка основной части имени по разделителю.
        parts = name.Split('-');

        //  Проверка количества частей при разбивке.
        if (parts.Length != 7)
        {
            //  Неверное имя файла.
            return false;
        }

        //  Получение параметров времени.
        if (int.TryParse(parts[0], out int year) &&
            int.TryParse(parts[1], out int month) &&
            int.TryParse(parts[2], out int day) &&
            int.TryParse(parts[3], out int hour) &&
            int.TryParse(parts[4], out int minute) &&
            int.TryParse(parts[5], out int second) &&
            int.TryParse(parts[6], out int millisecond))
        {
            try
            {
                //  Определение времени.
                time = new DateTime(year, month, day, hour, minute, second, millisecond);

                //  Время определено.
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                //  Неверное имя файла.
                return false;
            }
        }
        else
        {
            //  Неверное имя файла.
            return false;
        }
    }
}
