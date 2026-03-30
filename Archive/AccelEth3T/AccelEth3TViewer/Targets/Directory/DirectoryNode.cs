using Simargl.AccelEth3T.AccelEth3TViewer.Nodes;
using Simargl.AccelEth3T.AccelEth3TViewer.Targets.File;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simargl.AccelEth3T.AccelEth3TViewer.Targets.Directory;

/// <summary>
/// Представляет узел, отображающий каталог.
/// </summary>
public class DirectoryNode :
    Node
{
    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Path"/>.
    /// </summary>
    public event EventHandler? PathChanged;

    /// <summary>
    /// Поле для хранения пути к каталогу.
    /// </summary>
    private volatile string? _Path;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="parent">
    /// Родительский узел.
    /// </param>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    public DirectoryNode(Node? parent, string name) :
        base(parent, name)
    {
        //  Добавление основной задачи в механизм поддержки.
        Entry.Keeper.Add(InvokeAsync);
    }

    /// <summary>
    /// Возвращает или задаёт путь к каталогу.
    /// </summary>
    public string? Path
    {
        get => _Path;
        set
        {
            //  Проверка изменения значения.
            if (_Path != value)
            {
                //  Установка нового значения.
                _Path = value;

                //  Вызовы событий об изменении значения.
                OnPropertyChanged(new(nameof(Path)));
                OnPathChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="PathChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPathChanged(EventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref PathChanged)?.Invoke(this, e);
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Текущий путь.
        string? path = null;

        //  Время последнего обновления.
        DateTime update = DateTime.MinValue;

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Проверка необходимости обновления данных.
            if (path != _Path || (DateTime.Now - update).TotalSeconds > 10)
            {
                //  Изменение текущего пути.
                 path = _Path;

                //  Проверка пути.
                if (path is null)
                {
                    //  Выполнение в основном потоке.
                    await Entry.Invoker.InvokeAsync(
                        Provider.Clear, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    //  Получение текущих подкаталогов.
                    List<DirectoryNode> removeDirectories = [.. Provider
                        .Where(x => x is DirectoryNode)
                        .Select(x => (DirectoryNode)x)];

                    //  Перебор подкаталогов.
                    foreach (string subPath in System.IO.Directory
                        .GetDirectories(path, "*", SearchOption.TopDirectoryOnly)
                        .Order())
                    {
                        //  Поиск подкаталога среди текущих.
                        DirectoryNode? node = removeDirectories.FirstOrDefault(
                            x => x.Path is not null &&
                            x.Path.Equals(subPath, StringComparison.CurrentCultureIgnoreCase));

                        //  Проверка найденного подкаталога.
                        if (node is not null)
                        {
                            //  Каталог уже добавлен.
                            removeDirectories.Remove(node);
                        }
                        else
                        {
                            //  Выполнение в основном потоке.
                            await Entry.Invoker.InvokeAsync(delegate
                            {
                                //  Создание узла.
                                node = new(this, new DirectoryInfo(subPath).Name)
                                {
                                    Path = subPath,
                                };

                                //  Добавление узла.
                                Provider.Add(node);
                            }, cancellationToken).ConfigureAwait(false);
                        }
                    }

                    //  Проверка каталогов для удаления.
                    if (removeDirectories.Count > 0)
                    {
                        //  Выполнение в основном потоке.
                        await Entry.Invoker.InvokeAsync(delegate
                        {
                            //  Перебор удаляемых каталогов.
                            foreach (DirectoryNode node in removeDirectories)
                            {
                                //  Удаление узла.
                                Provider.Remove(node);
                            }
                        }, cancellationToken).ConfigureAwait(false);
                    }

                    //  Получение текущих файлов.
                    List<FileNode> removeFiles = [.. Provider
                        .Where(x => x is FileNode)
                        .Select(x => (FileNode)x)];

                    //  Перебор файлов.
                    foreach (string subPath in System.IO.Directory
                        .GetFiles(path, "*", SearchOption.TopDirectoryOnly)
                        .Order())
                    {
                        //  Поиск файла среди текущих.
                        FileNode? node = removeFiles.FirstOrDefault(
                            x => x.Path is not null &&
                            x.Path.Equals(subPath, StringComparison.CurrentCultureIgnoreCase));

                        //  Проверка найденного файла.
                        if (node is not null)
                        {
                            //  Файл уже добавлен.
                            removeFiles.Remove(node);
                        }
                        else
                        {
                            //  Выполнение в основном потоке.
                            await Entry.Invoker.InvokeAsync(delegate
                            {
                                //  Создание узла.
                                node = new(this, new FileInfo(subPath).Name, subPath);

                                //  Добавление узла.
                                Provider.Add(node);
                            }, cancellationToken).ConfigureAwait(false);
                        }
                    }

                    //  Проверка файлов для удаления.
                    if (removeFiles.Count > 0)
                    {
                        //  Выполнение в основном потоке.
                        await Entry.Invoker.InvokeAsync(delegate
                        {
                            //  Перебор удаляемых каталогов.
                            foreach (FileNode node in removeFiles)
                            {
                                //  Удаление узла.
                                Provider.Remove(node);
                            }
                        }, cancellationToken).ConfigureAwait(false);
                    }
                }

                //  Выполнение в основном потоке.
                await Entry.Invoker.InvokeAsync(delegate
                {
                    //  Реализовать свою коллекцию!!!
                    //Provider.Mo

                    //Provider.sor
                }, cancellationToken).ConfigureAwait(false);

                //  Установка времени обновления.
                update = DateTime.Now;
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }
}
