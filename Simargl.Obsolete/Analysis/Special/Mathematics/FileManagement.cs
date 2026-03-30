using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simargl.Mathematics;

/// <summary>
/// Содержит функции для работы с файловой системой.
/// </summary>
public class FileManagement
{
    /// <summary>
    /// Возвращает список всех файлов из директории.
    /// </summary>
    /// <param name="path">
    /// Путь к директории.
    /// </param>
    /// <returns>
    /// Список файлов из директории.
    /// </returns>
    public static FileInfo[] GetAllFiles(string path)
    {
        // Создаём объект "директория":
        DirectoryInfo directory = new(path);

        // Получаем список всех файлов из директории:            
        FileInfo[] filesAll = directory.GetFiles();

        return filesAll;
    }

    /// <summary>
    /// Возвращает список всех файлов из директории, в именах которых есть заданная подстрока.
    /// </summary>
    /// <param name="path">
    /// Путь к директории.
    /// </param>
    /// <param name="label">
    /// Метка нужных файлов: подстрока в их именах.
    /// </param>
    /// <returns>
    /// Список файлов из директории, в именах которых есть заданная подстрока.
    /// </returns>
    public static FileInfo[] GetFilesWithLabel(string path, string label)
    {
        // Создаём объект "директория":
        DirectoryInfo directory = new(path);

        // Получаем список всех файлов из директории:            
        FileInfo[] filesAll = directory.GetFiles();
        FileInfo[] files = new FileInfo[filesAll.Length];

        string currentName;
        int n = 0; // Счётчик файлов.
        foreach (FileInfo file in filesAll)
        {
            currentName = file.Name;

            if (currentName.Contains(label))
            {
                files[n] = file;
                n++;
            }
        }

        Array.Resize(ref files, n);

        return files;
    }

    /// <summary>
    /// Возвращает список файлов из директории path1, которых нет в директории path2.
    /// </summary>
    /// <param name="path1">
    /// Путь к первой директории.
    /// </param>
    /// <param name="path2">
    /// Путь ко второй директории.
    /// </param>
    /// <returns>
    /// Список файлов из path1, которых нет в path2.
    /// </returns>
    public static FileInfo[] GetFilesSetMinus(string path1, string path2)
    {
        // Создаём объект "директория":
        DirectoryInfo directory1 = new(path1);            

        // Получаем список всех файлов из директории:            
        FileInfo[] files1 = directory1.GetFiles();            
        FileInfo[] files = new FileInfo[files1.Length];

        int n = 0; // Счётчик файлов.
        foreach (FileInfo currentFile1 in files1)
        {
            if (!File.Exists(Path.Combine(path2, currentFile1.Name)))
            {
                files[n++] = currentFile1;
            }
        }

        Array.Resize(ref files, n);

        return files;
    }

    /// <summary>
    /// Копирование в папку destinationPath файлов из папки path1, которых нет в папке path2.
    /// </summary>
    /// <param name="path1">
    /// Путь к первой директории.
    /// </param>
    /// <param name="path2">
    /// Путь ко второй директории.
    /// </param>
    /// <param name="destinationPath">
    /// Путь к папке, куда копировать файлы.
    /// </param>
    public static void GetCopyFilesSetMinus(string path1, string path2, string destinationPath)
    {
        // Создаём объект "директория":
        DirectoryInfo directory1 = new(path1);

        // Получаем список всех файлов из директории:            
        FileInfo[] files1 = directory1.GetFiles();
                        
        foreach (FileInfo currentFile1 in files1)
        {
            if (!File.Exists(Path.Combine(path2, currentFile1.Name)))
            {
                File.Copy(currentFile1.FullName, Path.Combine(destinationPath, currentFile1.Name));
            }
        }
    }

    /// <summary>
    /// Возвращает список файлов из директории path1, которых нет в директории path2, и в именах которых есть заданная подстрока.
    /// </summary>
    /// <param name="path1">
    /// Путь к первой директории.
    /// </param>
    /// <param name="path2">
    /// Путь ко второй директории.
    /// </param>
    /// <param name="label">
    /// Метка нужных файлов: подстрока в их именах.
    /// </param>
    /// <returns>
    /// Список файлов из path1, которых нет в path2, и в именах которых есть заданная подстрока.
    /// </returns>
    public static FileInfo[] GetFilesWithLabelSetMinus(string path1, string path2, string label)
    {
        // Создаём объекты "директория":
        DirectoryInfo directory1 = new(path1);

        // Получаем списки всех файлов из директорий:            
        FileInfo[] files1 = directory1.GetFiles();
        FileInfo[] files = new FileInfo[files1.Length];

        int n = 0; // Счётчик файлов.
        foreach (FileInfo currentFile1 in files1)
        {
            if (!File.Exists(Path.Combine(path2, currentFile1.Name)) && currentFile1.Name.Contains(label))
            {
                files[n] = currentFile1;
                n++;
            }
        }

        Array.Resize(ref files, n);

        return files;
    }

    /// <summary>
    /// Список всех поддиректорий заданной директории (только имена, не полные пути).
    /// </summary>
    /// <param name="path2Directory">
    /// Полный путь к директории.
    /// </param>
    /// <returns>
    /// Список имён всех поддиректорий заданной директории.
    /// </returns>
    public static IEnumerable<string> GetSubDirectories(string path2Directory)
    {
        return new DirectoryInfo(path2Directory).GetDirectories().Select(directory => directory.Name);

    }
    

}
