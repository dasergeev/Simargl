using Apeiron.Platform.Software.ChannelAdditor.Projects;
using System.IO;

namespace Apeiron.Platform.Software.ChannelAdditor.UserInterface;

/// <summary>
/// Представляет главное окно приложения.
/// </summary>
public partial class ChannelAdditorWindow :
    Window
{
    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private CancellationTokenSource? _TokenSource;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public ChannelAdditorWindow()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Подписка на изменение выбранного элемента.
        _Editor.SelectedRuleChanged += (obj, e) =>
        {
            //  Настройка кнопки удаления элемента.
            _RemoveButton.Visibility = _Editor.SelectedRule is null ? Visibility.Collapsed : Visibility.Visible;
        };
    }

    /// <summary>
    /// Возвращает или задаёт проект.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public Project Project
    {
        get => _Editor.Project;
        set => _Editor.Project = value;
    }

    /// <summary>
    /// Обрабатывает событие нажатия кнопки добавления канала.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        //  Добавление канала.
        Project.Rules.Add(new()
        {
            Name = "Новый канал",
            Value = 0,
            Unit = "-",
            Sampling = 1,
            Cutoff = 0,
        });
    }

    /// <summary>
    /// Обрабатывает событие нажатия кнопки удаления канала.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
        //  Проверка возможности удаления.
        if (_Editor.SelectedRule is not null)
        {
            //  Удаление.
            Project.Rules.Remove(_Editor.SelectedRule);
        }
    }

    /// <summary>
    /// Обрабатывает событие нажатия кнопки начала записи кадров.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        //  Выводит сообщение об ошибке.
        static void message(string text) =>
            MessageBox.Show(text, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        //  Проверка существования исходного каталога.
        if (!Directory.Exists(Project.SourcePath))
        {
            //  Вывод сообщения об ошибке.
            message($"Исходный каталог \"{Project.SourcePath}\" не найден.");

            //  Завершение процедуры запуска.
            return;
        }

        //  Проверка существования каталога для записи.
        if (!Directory.Exists(Project.TargetPath))
        {
            //  Вывод сообщения об ошибке.
            message($"Каталог для записи \"{Project.TargetPath}\" не найден.");

            //  Завершение процедуры запуска.
            return;
        }

        //  Проверка расположения каталога для записи.
        if (Project.TargetPath.Length < 2 || Project.TargetPath[..2] == "\\\\" || Project.TargetPath[..2] == "//")
        {
            //  Вывод сообщения об ошибке.
            message($"Недопустимое расположение каталога для записи \"{Project.TargetPath}\". Укажите расположение на локальном компьютере.");

            //  Завершение процедуры запуска.
            return;
        }

        //  Проверка совпадения каталогов.
        if (Project.SourcePath.ToUpper() == Project.TargetPath.ToUpper())
        {
            //  Вывод сообщения об ошибке.
            message($"Каталог для записи не может совпадать с исходным каталогом. Укажите другое расположение каталога для записи.");

            //  Завершение процедуры запуска.
            return;
        }

        //  Проверка наличия правил.
        if (Project.Rules.Count == 0)
        {
            //  Вывод сообщения об ошибке.
            message("Не найдены каналы для добавления. Добавьте хотя бы один канал.");

            //  Завершение процедуры запуска.
            return;
        }

        //  Список имён каналов.
        List<string> names = new();

        //  Перебор правил.
        foreach (Rule rule in Project.Rules)
        {
            //  Проверка имени канала.
            if (names.Contains(rule.Name))
            {
                //  Вывод сообщения об ошибке.
                message($"Найдено несколько каналов с именем {rule.Name}. Каждый канал должен иметь уникальное имя.");

                //  Завершение процедуры запуска.
                return;
            }

            //  Проверка частоты дискретизации.
            if (rule.Sampling <= 0)
            {
                //  Вывод сообщения об ошибке.
                message($"У канала {rule.Name} указана отрицательная или равная нулю частота дискретизации. Установите положительное значение.");

                //  Завершение процедуры запуска.
                return;
            }

            //  Проверка частоты дискретизации.
            if (rule.Cutoff < 0)
            {
                //  Вывод сообщения об ошибке.
                message($"У канала {rule.Name} указана отрицательная частота среза фильтра. Установите неотрицательное значение.");

                //  Завершение процедуры запуска.
                return;
            }
        }

        //  Настройка интефейса.
        _AddButton.Visibility = Visibility.Collapsed;
        _RemoveButton.Visibility = Visibility.Collapsed;
        _StartButton.Visibility = Visibility.Collapsed;
        _StopButton.Visibility = Visibility.Visible;
        _Editor.IsEnabled = false;
        _ProgressBar.Value = 0;

        //  Создание источника токена отмены.
        _TokenSource = new();

        //  Запуск задачи.
        _ = Task.Run(async () => await WorkAsync(_TokenSource.Token));
    }

    /// <summary>
    /// Обрабатывает событие нажатия кнопки остановки записи кадров.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        //  Отмена задачи.
        _TokenSource?.Cancel();
        _TokenSource?.Dispose();
        _TokenSource = null;

        //  Настройка интефейса.
        _AddButton.Visibility = Visibility.Visible;
        _RemoveButton.Visibility = _Editor.SelectedRule is null ? Visibility.Collapsed : Visibility.Visible;
        _StartButton.Visibility = Visibility.Visible;
        _StopButton.Visibility = Visibility.Collapsed;
        _Editor.IsEnabled = true;
    }

    /// <summary>
    /// Асинхронно выполняет работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу.
    /// </returns>
    private async Task WorkAsync(CancellationToken cancellationToken)
    {
        //  Получение делегата для вызова действия в потоке интерфейса.
        Action<Action> invoke = Dispatcher.Invoke;

        //  Получение пути к лог-файлу.
        string logPath = Path.Combine(
            new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Directory!.FullName,
            "log.txt");

        //  Средства записи в лог-файл.
        using StreamWriter logWriter = new(logPath);

        //  Блок с гарантированным завершением.
        try
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Список исходных файлов.
            List<string> files = new();

            //  Выполняет загрузку файлов.
            async Task loadFilesAsync(DirectoryInfo directory, CancellationToken cancellationToken)
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Добавление файлов из текущего каталога.
                files.AddRange(directory.GetFiles().Select(x => x.FullName));

                //  Проверка необходимости загрузки из подкаталога.
                if (Project.IsNested)
                {
                    //  Перебор подкаталогов.
                    foreach (var subDirectory in directory.GetDirectories())
                    {
                        //  Загрузка из подкаталога.
                        await loadFilesAsync(subDirectory, cancellationToken).ConfigureAwait(false);
                    }
                }
            }

            //  Загрузка файлов.
            await loadFilesAsync(new(Project.SourcePath), cancellationToken).ConfigureAwait(false);

            //  Настройка строки состояния.
            invoke(() => _ProgressBar.Maximum = files.Count - 1);

            //  Количество обработанных кадров.
            int count = 0;

            //  Асинхронная работа с кадрами.
            await Parallel.ForEachAsync(
                files,
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                },
                async (file, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Блок перехвата исключений.
                    try
                    {
                        //  Открытие кадра.
                        Frames.Frame frame = new(file);

                        //  Проверка количества каналов.
                        if (frame.Channels.Count == 0)
                        {
                            //  Ошибка.
                            throw new InvalidDataException("Пустой кадр");
                        }

                        //  Перебор новых каналов.
                        foreach (Rule rule in Project.Rules)
                        {
                            //  Канал.
                            Frames.Channel channel;

                            //  Поиск канала.
                            if (frame.Channels.Contains(rule.Name))
                            {
                                //  Получение канала по имени.
                                channel = frame.Channels[rule.Name];

                                //  Исключение канала из кадра.
                                frame.Channels.Remove(channel);
                            }
                            else
                            {
                                //  Получение копии первого канала.
                                channel = frame.Channels[0].Clone();
                            }

                            //  Длительность канала.
                            double duration = channel.Length / channel.Sampling;

                            //  Длина канала.
                            int length = (int)(duration * rule.Sampling);

                            //  Установка свойств канала.
                            channel.Name = rule.Name;
                            channel.Unit = rule.Unit;
                            channel.Sampling = rule.Sampling;
                            channel.Cutoff = rule.Cutoff;
                            channel.Length = length;

                            //  Установка значений канала.
                            for (int i = 0; i < length; i++)
                            {
                                //  Установка значения.
                                channel[i] = rule.Value;
                            }

                            //  Добавление канала в конец кадра.
                            frame.Channels.Add(channel);
                        }

                        //  Формирование пути к перезаписанному файлу.
                        string path = file.Replace(Project.SourcePath, "");

                        //  Корректировка начальных символов относительного пути.
                        while (path.Length > 0 && (path[0] == '\\' || path[0] == '/'))
                        {
                            //  Отбрасывание первого символа.
                            path = path[1..];
                        }

                        //  Получение полного пути.
                        path = Path.Combine(Project.TargetPath, path);

                        //  Получение каталога для перезаписанного файла.
                        DirectoryInfo directory = new FileInfo(path).Directory!;

                        //  Проверка существования каталога.
                        if (!directory.Exists)
                        {
                            //  Создание каталога.
                            directory.Create();
                        }

                        //  Сохранение кадра.
                        frame.Save(path, frame.Format);
                    }
                    catch (Exception ex)
                    {
                        //  Блокировка лог-файла.
#pragma warning disable VSTHRD103 // Call async methods when in an async method
                        lock (logWriter)
                        {
                            //  Запись в лог-файл.
                            logWriter.WriteLine($"Исключение при работе с файлом \"{file}:");
                            logWriter.WriteLine(ex.ToString());
                            logWriter.WriteLine();
                        }
#pragma warning restore VSTHRD103 // Call async methods when in an async method
                    }

                    //  Увеличение счётчика.
                    int actualCount = Interlocked.Increment(ref count);

                    //  Настройка строки состояния.
                    invoke(() => _ProgressBar.Value = actualCount);
                }).ConfigureAwait(false);
        }
        finally
        {
            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Имитация нажатия кнопки остановки выполнения.
                invoke(() => StopButton_Click(this, new()));
            }
        }
    }
}
