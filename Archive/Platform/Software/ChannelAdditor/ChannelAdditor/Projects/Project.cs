using System.ComponentModel;

namespace Apeiron.Platform.Software.ChannelAdditor.Projects;

/// <summary>
/// Представляет проект.
/// </summary>
public sealed class Project :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения пути к проекту.
    /// </summary>
    private string _ProjectPath;

    /// <summary>
    /// Поле для хранения пути к исходным кадрам.
    /// </summary>
    private string _SourcePath;

    /// <summary>
    /// Поле для хранения пути к записываемым кадрам.
    /// </summary>
    private string _TargetPath;

    /// <summary>
    /// Поле для хранения значени, определяющего следует ли включать вложенные каталоги.
    /// </summary>
    private bool _IsNested;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Project()
    {
        //  Установка значений полей по умолчанию.
        _ProjectPath = string.Empty;
        _SourcePath = string.Empty;
        _TargetPath = string.Empty;
        _IsNested = false;

        //  Создание коллекции правил.
        Rules = new();
    }

    /// <summary>
    /// Возвращает коллекцию правил.
    /// </summary>
    public RuleCollection Rules { get; }

    /// <summary>
    /// Возвращает путь к проекту.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string ProjectPath
    {
        get => _ProjectPath;
        set
        {
            //  Проверка ссылки на путь.
            Check.IsNotNull(value, nameof(ProjectPath));

            //  Проверка изменения значения свойства.
            if (_ProjectPath != value)
            {
                //  Изменение значения свойства.
                _ProjectPath = value;

                //  Вызов события об изменении значения свойства.
                PropertyChanged?.Invoke(this, new(nameof(ProjectPath)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт путь к исходным кадрам.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string SourcePath
    {
        get => _SourcePath;
        set
        {
            //  Проверка ссылки на путь.
            Check.IsNotNull(value, nameof(SourcePath));

            //  Проверка изменения значения свойства.
            if (_SourcePath != value)
            {
                //  Изменение значения свойства.
                _SourcePath = value;

                //  Вызов события об изменении значения свойства.
                PropertyChanged?.Invoke(this, new(nameof(SourcePath)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт путь к записываемым кадрам.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string TargetPath
    {
        get => _TargetPath;
        set
        {
            //  Проверка ссылки на путь.
            Check.IsNotNull(value, nameof(TargetPath));

            //  Проверка изменения значения свойства.
            if (_TargetPath != value)
            {
                //  Изменение значения свойства.
                _TargetPath = value;

                //  Вызов события об изменении значения свойства.
                PropertyChanged?.Invoke(this, new(nameof(TargetPath)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее следует ли просматривать вложенные каталоги.
    /// </summary>
    public bool IsNested
    {
        get => _IsNested;
        set
        {
            //  Проверка изменения значения свойства.
            if (_IsNested != value)
            {
                //  Изменение значения свойства.
                _IsNested = value;

                //  Вызов события об изменении значения свойства.
                PropertyChanged?.Invoke(this, new(nameof(IsNested)));
            }
        }
    }
}
