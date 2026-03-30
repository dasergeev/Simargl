namespace Apeiron.Platform.Utilities;

/// <summary>
/// Представляет класс для хранения настроек.
/// </summary>
internal static class Settings
{
    /// <summary>
    /// Путь к директории хранения кадров.
    /// </summary>
    internal static string SourcePath = Path.GetDirectoryName(Environment.ProcessPath)!; //@"E:\FrameSource";

    /// <summary>
    /// Путь к директории сконвертированных файлов.
    /// </summary>
    internal static string TargetPath = Path.GetDirectoryName(Environment.ProcessPath)!; //@"E:\FrameTarget";
}
