using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;
using System.ComponentModel;
using System.IO;
using System.Text.Json;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main;

/// <summary>
/// Представляет проект.
/// </summary>
public sealed class Project :
    INotifyPropertyChanged,
    IDisposable
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения пути к файлу проекта.
    /// </summary>
    private string? _Path;

    /// <summary>
    /// Поле для хранения значения, определяющего требуется ли сохранить проект.
    /// </summary>
    private bool _IsNeedSaving;

    /// <summary>
    /// Поле для хранения потока файла.
    /// </summary>
    private FileStream? _Stream;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    public Project(string? path)
    {
        //  Установка пути к файлу проекта.
        _Path = path;

        //  Создание родительского узла.
        RootNode = new(this);

        //  Проверка пути.
        if (_Path is not null)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Открытие потока.
                _Stream = new(_Path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);

                //  Загрузка данных.
                RootNode.Load(_Stream);
            }
            catch
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Разрушение потока.
                    _Stream?.Dispose();
                }
                catch { }

                //  Повторный выброс исключения.
                throw;
            }
        }

        //  Установка значения, определяющего требуется ли сохранить проект.
        _IsNeedSaving = false;
    }

    /// <summary>
    /// Сохраняет проект.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу проекта.
    /// </param>
    public void Save(string path)
    {
        //  Проверка пути.
        if (_Path != path)
        {
            //  Поток данных.
            FileStream? stream = null;

            //  Блок перехвата всех исключений.
            try
            {
                //  Открытие потока.
                stream = new(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);

                //  Сохранение данных в поток.
                RootNode.Save(stream);
            }
            catch
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Разрушение потока.
                    stream?.Dispose();
                }
                catch { }

                //  Повторный выброс исключения.
                throw;
            }

            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение потока.
                _Stream?.Dispose();
            }
            catch { }

            //  Установка новых значений.
            _Path = path;
            _Stream = stream;
        }
        else
        {
            //  Проверка потока.
            if (_Stream is null)
            {
                //  Выброс исключения.
                throw new InvalidOperationException("Произошла ошибка при сохранении файла: поток не найден.");
            }

            //  Сохранение данных в поток.
            RootNode.Save(_Stream);
        }

        //  Установка значения, определяющего требуется ли сохранить проект.
        IsNeedSaving = false;
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    public void Dispose()
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Разрушение потока.
            _Stream?.Dispose();
        }
        catch { }
    }

    /// <summary>
    /// Возвращает или задаёт путь к файлу проекта.
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

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(Path)));
            }
        }
    }

    /// <summary>
    /// Возвращает родительский узел.
    /// </summary>
    public RootNode RootNode { get; }

    /// <summary>
    /// Возвращает значение, определяющее требуется ли сохранить проект.
    /// </summary>
    public bool IsNeedSaving
    {
        get => _IsNeedSaving;
        set
        {
            //  Проверка изменения значения.
            if (_IsNeedSaving != value)
            {
                //  Установка нового значения.
                _IsNeedSaving = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(IsNeedSaving)));
            }
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref PropertyChanged)?.Invoke(this, e);
    }
}
