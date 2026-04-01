using System.IO;

namespace Simargl.Analysis.Calibrations;

/// <summary>
/// Представляет информацию о калибровочном кадре.
/// </summary>
public sealed class CalibrationFrameInfo
{
    /// <summary>
    /// Инициализирует новый экземплря класса.
    /// </summary>
    /// <param name="fileInfo">
    /// Информация о файле регистрации.
    /// </param>
    /// <param name="modifiers">
    /// Модификаторы каналов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="fileInfo"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="modifiers"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="modifiers"/> передана коллекция, содержащая пустую ссылку.
    /// </exception>
    public CalibrationFrameInfo(FileInfo fileInfo, params ChannelModifier[] modifiers)
    {
        //  Установка информации о файле регистрации.
        FileInfo = IsNotNull(fileInfo, nameof(fileInfo));

        //  Создание коллекции модификаторов каналов.
        Modifiers = new(modifiers);
    }

    /// <summary>
    /// Возвращает информацию о файле регистрации.
    /// </summary>
    public FileInfo FileInfo { get; }

    /// <summary>
    /// Возвращает коллекцию модификаторов каналов.
    /// </summary>
    public ChannelModifierCollection Modifiers { get; }

    //intera async Task<Frame> LoadAsync(Calibration)
}
