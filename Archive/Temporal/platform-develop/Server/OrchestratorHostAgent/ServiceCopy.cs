using Apeiron.Support;

namespace Apeiron.Platform.Server.Services.Orchestrator.OrchestratorHostAgent;

/// <summary>
/// Представляет класс, реализующий копирование файлов по сети.
/// </summary>
public static class ServiceCopy
{

    /// <summary>
    /// Копирует файлы с общедоступного ресурс на локальный хост.
    /// </summary>
    /// <param name="localPath">Локальный путь к файлам служб.</param>
    /// <param name="serverPath">Путь к файлам служб на сервере.</param>
    public static void FilesCopyService(string serverPath, string localPath)
    {
        // Проверка входящих параметров.
        Check.IsNotNull(serverPath, nameof(serverPath));
        Check.IsNotNull(localPath, nameof(localPath));

        try
        {
            DirectoryInfo localDirectory = new(localPath);
            DirectoryInfo serverDirectory = new(serverPath);

            //  Проверка существования каталога.
            if (localDirectory.Exists && serverDirectory.Exists)
            {
                // Копирование всех файлов.
                foreach (FileInfo fileInfo in serverDirectory.GetFiles())
                {
                    fileInfo.CopyTo(Path.Combine(localDirectory.ToString(), fileInfo.Name), true);
                }

                // Копирование поддерикторий с помощью рекурсии.
                foreach (DirectoryInfo subDirectory in serverDirectory.GetDirectories())
                {
                    // Создаём поддерикторию
                    DirectoryInfo nextLocalSubDirectory = localDirectory.CreateSubdirectory(subDirectory.Name);

                    // Рекурсивно копируем файлы.
                    FilesCopyService(subDirectory.FullName, nextLocalSubDirectory.FullName);
                }
            }
            else
            {
                throw new IOException("Указанная директория не существует.");
            }
        }
        catch (Exception ex)
        {
            throw new IOException($"Ошибка связанная с копированием файлов. {ex}");
        }
    }
}

