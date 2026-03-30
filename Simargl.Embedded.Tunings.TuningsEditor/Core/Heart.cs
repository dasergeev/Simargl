using Microsoft.Win32;
using Simargl.Embedded.Tunings.TuningsEditor.Core.Code;
using Simargl.Embedded.Tunings.TuningsEditor.Main;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Windows.Threading;

namespace Simargl.Embedded.Tunings.TuningsEditor.Core;

/// <summary>
/// Представляет сердце приложения.
/// </summary>
public sealed class Heart :
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения <see cref="SelectedNode"/>.
    /// </summary>
    public static event EventHandler? SelectedNodeChanged;

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения управляющего очередью рабочих элементов для потока.
    /// </summary>
    private readonly Dispatcher _Dispatcher;

    /// <summary>
    /// Поле для хранения проекта.
    /// </summary>
    private Project? _Project;

    /// <summary>
    /// Поле для хранения выбранного узла.
    /// </summary>
    private static Node? _SelectedNode;

    /// <summary>
    /// Поле для хранения значения, определяющего доступны ли элементы редактора.
    /// </summary>
    private bool _IsEditEnabled = true;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="dispatcher">
    /// Управляющий очередью рабочих элементов для потока.
    /// </param>
    public Heart(Dispatcher dispatcher)
    {
        _Dispatcher = dispatcher;

        NewProjectCommand = new(true, OnNewProjectCommand);
        OpenProjectCommand = new(true, OnOpenProjectCommand);
        SaveProjectCommand = new(false, OnSaveProjectCommand);
        SaveAsProjectCommand = new(false, OnSaveAsProjectCommand);
        StartLogCommand = new(false, OnStartLogCommand);
        StopLogCommand = new(true, OnStopLogCommand);
        ClearLogCommand = new(true, OnClearLogCommand);
        CheckCommand = new(false, OnCheckCommand);
        CppCommand = new(false, OnCppCommand);
        CSharpCommand = new(false, OnCSharpCommand);
        StartCommand = new(false, OnStartCommand);
        StopCommand = new(false, OnStopCommand);
    }

    /// <summary>
    /// Возвращает или задаёт проект.
    /// </summary>
    public Project? Project
    {
        get => _Project;
        set
        {
            //  Проверка изменения значения.
            if (!ReferenceEquals(_Project, value))
            {
                //  Установка нового значения.
                _Project = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(Project)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт выбранный узел.
    /// </summary>
    public Node? SelectedNode
    {
        get => _SelectedNode;
        set
        {
            //  Проверка изменения значения.
            if (!ReferenceEquals(_SelectedNode, value))
            {
                //  Установка нового значения.
                _SelectedNode = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(SelectedNode)));

                //  Вызов глобального события.
                Volatile.Read(ref SelectedNodeChanged)?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает выбранный узел.
    /// </summary>
    public static Node? GlobalSelectedNode => _SelectedNode;

    /// <summary>
    /// Поле для хранения генератора кода.
    /// </summary>
    public CodeGenerator? CodeGenerator;

    /// <summary>
    /// Поле для хранения источника токена отмены запущенных задач.
    /// </summary>
    private CancellationTokenSource? _CancellationTokenSource;

    /// <summary>
    /// Возвращает или задаёт значение, определяющее доступны ли элементы редактора.
    /// </summary>
    public bool IsEditEnabled
    {
        get => _IsEditEnabled;
        set
        {
            //  Проверка изменения значения.
            if (_IsEditEnabled != value)
            {
                //  Установка нового значения.
                _IsEditEnabled = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(IsEditEnabled)));
            }
        }
    }

    /// <summary>
    /// Проверяет необходимость сохранения проекта.
    /// </summary>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    public bool CheckSave()
    {
        //  Получение текущего проекта.
        if (Project is not Project project)
        {
            //  Нет данных для сохранения.
            return true;
        }

        //  Проверка необходимости сохранения проекта.
        if (!project.IsNeedSaving)
        {
            //  Нет данных для сохранения.
            return true;
        }

        //  Вызов диалогового окна.
        return MessageBox.Show("Сохранить текущий проект?", "Проект", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) switch
        {
            MessageBoxResult.Yes => SaveProject(),
            MessageBoxResult.No => true,
            _ => false,
        };
    }

    /// <summary>
    /// Сохраняет проект.
    /// </summary>
    /// <returns>
    /// Результат сохранения.
    /// </returns>
    private bool SaveProject()
    {
        //  Получение текущего проекта.
        if (Project is not Project project)
        {
            //  Нет данных для сохранения.
            return true;
        }

        //  Проверка текущего пути.
        if (project.Path is string path)
        {
            try
            {
                //  Сохранение проекта.
                project.Save(path);

                //  Проект сохранён.
                return true;
            }
            catch (Exception ex)
            {
                //  Вывод информации об ошибке.
                MessageBox.Show($"Ошибка при сохранении:\n{ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                //  Проект не сохранён.
                return false;
            }
        }
        else
        {
            //  Сохранение проекта под новым именем.
            return SaveAsProject();
        }
    }

    /// <summary>
    /// Сохраняет проект под новым именем.
    /// </summary>
    /// <returns>
    /// Результат сохранения.
    /// </returns>
    private bool SaveAsProject()
    {
        //  Получение текущего проекта.
        if (Project is not Project project)
        {
            //  Нет данных для сохранения.
            return true;
        }

        //  Создание диалога.
        SaveFileDialog dialog = new()
        {
            Title = "Сохранить проект как",
            Filter = _Filter,
            DefaultExt = _FileExtension,
            AddExtension = true,
        };

        //  Проверка текущего пути.
        if (project.Path is string path)
        {
            //  Настройка диалога.
            dialog.FileName = Path.GetFileName(path);
            dialog.InitialDirectory = Path.GetDirectoryName(path);
        }
        else
        {
            //  Настройка диалога.
            dialog.FileName = "Проект." + _FileExtension;
        }

        //  Отображение диалога.
        if (dialog.ShowDialog() == true)
        {
            try
            {
                //  Сохранение проекта.
                project.Save(dialog.FileName);

                //  Проект сохранён.
                return true;
            }
            catch (Exception ex)
            {
                //  Вывод информации об ошибке.
                MessageBox.Show($"Ошибка при сохранении:\n{ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //  Проект не сохранён.
        return false;
    }

    /// <summary>
    /// Создаёт проект.
    /// </summary>
    /// <param name="path">
    /// Путь к проекту.
    /// </param>
    private void NewProject(string? path)
    {
        //  Проверка текущего проекта.
        if (!CheckSave())
        {
            //  Завершение работы.
            return;
        }

        //  Создание проекта.
        Project project = new(path);

        //  Получение текущего проекта.
        using Project? oldProject = Project;

        //  Установка нового проекта.
        Project = project;

        //  Настройка команд.
        SaveProjectCommand.IsEnabled = true;
        SaveAsProjectCommand.IsEnabled = true;
        CheckCommand.IsEnabled = true;
        CppCommand.IsEnabled = true;
        CSharpCommand.IsEnabled = true;
        StartCommand.IsEnabled = true;
    }

    /// <summary>
    /// Возвращает команду создания нового проекта.
    /// </summary>
    public Command NewProjectCommand { get; }
    private void OnNewProjectCommand()
    {
        //  Создание проекта.
        NewProject(null);
    }

    /// <summary>
    /// Возвращает команду создания нового проекта.
    /// </summary>
    public Command OpenProjectCommand { get; }
    private void OnOpenProjectCommand()
    {
        //  Проверка текущего проекта.
        if (!CheckSave())
        {
            //  Завершение работы.
            return;
        }

        //  Создание диалога.
        OpenFileDialog dialog = new()
        {
            Title = "Открыть проект",
            Filter = _Filter,
            DefaultExt = _FileExtension,
            AddExtension = true,
        };

        //  Отображение диалога.
        if (dialog.ShowDialog() == true)
        {
            try
            {
                //  Создание проекта.
                NewProject(dialog.FileName);
            }
            catch (Exception ex)
            {
                //  Вывод информации об ошибке.
                MessageBox.Show($"Ошибка при открытии:\n{ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                throw;
            }
        }
    }

    /// <summary>
    /// Возвращает команду создания нового проекта.
    /// </summary>
    public Command SaveProjectCommand { get; }
    private void OnSaveProjectCommand()
    {
        //  Сохранение проекта.
        SaveProject();
    }

    private const string _FileExtension = "tune_spec";
    private const string _Filter = $"Проект спецификации настраиваемых параметров (*.{_FileExtension})|*.{_FileExtension}|Все файлы (*.*)|*.*";


    /// <summary>
    /// Возвращает команду создания нового проекта.
    /// </summary>
    public Command SaveAsProjectCommand { get; }
    private void OnSaveAsProjectCommand()
    {
        //  Сохранение проекта.
        SaveAsProject();
    }

    /// <summary>
    /// Возвращает команду запуска журнала.
    /// </summary>
    public Command StartLogCommand { get; }
    private void OnStartLogCommand()
    {
        //  Запуск журнала.
        IsLogEnable = true;

        //  Настройка интерфейса.
        StartLogCommand.IsEnabled = false;
        StopLogCommand.IsEnabled = true;
    }

    /// <summary>
    /// Возвращает команду остановки журнала.
    /// </summary>
    public Command StopLogCommand { get; }
    private void OnStopLogCommand()
    {
        //  Остановка журнала.
        IsLogEnable = false;

        //  Настройка интерфейса.
        StartLogCommand.IsEnabled = true;
        StopLogCommand.IsEnabled = false;
    }

    /// <summary>
    /// Возвращает команду очистки журнала.
    /// </summary>
    public Command ClearLogCommand { get; }
    private void OnClearLogCommand()
    {
        //  Очистка журнала.
        IsLogClear = true;
    }

    /// <summary>
    /// Начинает работу.
    /// </summary>
    /// <returns>
    /// Токен отмены.
    /// </returns>
    private CancellationToken StartWork()
    {
        //  Настройка команд.
        NewProjectCommand.IsEnabled = false;
        OpenProjectCommand.IsEnabled = false;
        SaveProjectCommand.IsEnabled = false;
        SaveAsProjectCommand.IsEnabled = false;
        CheckCommand.IsEnabled = false;
        CppCommand.IsEnabled = false;
        CSharpCommand.IsEnabled = false;
        StartCommand.IsEnabled = false;
        StopCommand.IsEnabled = true;

        //  Блокировка редактора.
        IsEditEnabled = false;

        //  Очистка журнала.
        OnClearLogCommand();

        //  Создание источника токена отмены.
        CancellationTokenSource cancellationTokenSource = new();

        //  Замена источника токена отмены.
        if (Interlocked.Exchange(ref _CancellationTokenSource, cancellationTokenSource) is CancellationTokenSource lastSource)
        {
            //  Отмена.
            Cancel(lastSource);
        }

        //  Возврат токена отмены.
        return cancellationTokenSource.Token;
    }

    /// <summary>
    /// Останавливает работу.
    /// </summary>
    private void StopWork()
    {
        //  Настройка команд.
        NewProjectCommand.IsEnabled = true;
        OpenProjectCommand.IsEnabled = true;
        SaveProjectCommand.IsEnabled = true;
        SaveAsProjectCommand.IsEnabled = true;
        CheckCommand.IsEnabled = true;
        CppCommand.IsEnabled = true;
        CSharpCommand.IsEnabled = true;
        StartCommand.IsEnabled = true;
        StopCommand.IsEnabled = false;

        //  Снятие блокировки редактора.
        IsEditEnabled = true;
    }

    /// <summary>
    /// Возвращает команду проверки.
    /// </summary>
    public Command CheckCommand { get; }
    private void OnCheckCommand()
    {
        //  Начало работы.
        CancellationToken cancellationToken = StartWork();

        //  Создание генератора кода.
        CodeGenerator = new CppCodeGenerator(this,
            async delegate(CodeGenerator codeGenerator, CancellationToken cancellationToken)
            {
                //  Выполнение в основном потоке.
                await _Dispatcher.InvokeAsync(delegate
                {
                    //  Остановка работы.
                    StopWork();
                });
            }, cancellationToken);
    }

    /// <summary>
    /// Возвращает команду генерации C++ кода.
    /// </summary>
    public Command CppCommand { get; }
    private void OnCppCommand()
    {
        //  Начало работы.
        CancellationToken cancellationToken = StartWork();

        //  Создание генератора кода.
        CodeGenerator = new CppCodeGenerator(this,
            async delegate (CodeGenerator codeGenerator, CancellationToken cancellationToken)
            {
                //  Выполнение в основном потоке.
                await _Dispatcher.InvokeAsync(delegate
                {
                    //  Остановка работы.
                    StopWork();

                    //  Проверка исходного текста.
                    if (codeGenerator.SourceCode is string sourceCode)
                    {
                        //  Проверка проекта.
                        if (Project is Project project)
                        {
                            //  Сохранение данных в файл.
                            File.WriteAllText(
                                project.RootNode.GeneratorNode.CppPath.Value,
                                sourceCode);
                        }
                    }
                });
            }, cancellationToken);
    }

    /// <summary>
    /// Возвращает команду генерации C# кода.
    /// </summary>
    public Command CSharpCommand { get; }
    private void OnCSharpCommand()
    {
        //  Начало работы.
        CancellationToken cancellationToken = StartWork();

        //  Создание генератора кода.
        CodeGenerator = new CSharpCodeGenerator(this,
            async delegate (CodeGenerator codeGenerator, CancellationToken cancellationToken)
            {
                //  Выполнение в основном потоке.
                await _Dispatcher.InvokeAsync(delegate
                {
                    //  Остановка работы.
                    StopWork();

                    //  Проверка исходного текста.
                    if (codeGenerator.SourceCode is string sourceCode)
                    {
                        //  Проверка проекта.
                        if (Project is Project project)
                        {
                            //  Сохранение данных в файл.
                            File.WriteAllText(
                                project.RootNode.GeneratorNode.CSharpPath.Value,
                                sourceCode);
                        }
                    }
                });
            }, cancellationToken);
    }

    /// <summary>
    /// Возвращает команду запуска работы.
    /// </summary>
    public Command StartCommand { get; }
    private void OnStartCommand()
    {

    }

    /// <summary>
    /// Возвращает команду остановки работы.
    /// </summary>
    public Command StopCommand { get; }
    private void OnStopCommand()
    {
        //  Чтение источника токена отмены.
        if (Interlocked.Exchange(ref _CancellationTokenSource, null) is CancellationTokenSource cancellationTokenSource)
        {
            //  Отмена.
            Cancel(cancellationTokenSource);
        }
    }

    /// <summary>
    /// Выполняет отмену задачи.
    /// </summary>
    /// <param name="cancellationTokenSource">
    /// Источник токена отмены.
    /// </param>
    private static void Cancel(CancellationTokenSource cancellationTokenSource)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Отправка запроса на отмену.
            cancellationTokenSource.Cancel();
        }
        catch { }

        //  Блок перехвата всех исключений.
        try
        {
            //  Разрушение источника токена отмены.
            cancellationTokenSource.Dispose();
        }
        catch { }
    }

    /// <summary>
    /// Поле для хранения текста журнала.
    /// </summary>
    private string _LogText = string.Empty;

    /// <summary>
    /// Возвращает очередь журнала.
    /// </summary>
    public ConcurrentQueue<string> LogQueue { get; } = [];

    /// <summary>
    /// Возвращает значение, определяющее включён ли журнал.
    /// </summary>
    public bool IsLogEnable { get; private set; } = true;

    /// <summary>
    /// Возвращает значение, определяющее очищен ли журнал.
    /// </summary>
    public bool IsLogClear { get; set; } = true;

    /// <summary>
    /// Возвращает текст журнала.
    /// </summary>
    public string LogText
    {
        get => _LogText;
        private set
        {
            //  Проверка изменения текста журнала.
            if (_LogText != value)
            {
                //  Установка нового значения.
                _LogText = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(LogText)));
            }
        }
    }

    /// <summary>
    /// Асинхронно устанавливает текст журнала.
    /// </summary>
    /// <param name="value">
    /// Новое значение текста журнала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая текст журнала.
    /// </returns>
    public async Task SetLogTextAsync(string value, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Выполнение в основном потоке.
        await _Dispatcher.InvokeAsync(delegate
        {
            //  Изменение значения.
            LogText = value;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    public void Test()
    {
        _ = _Dispatcher;
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
