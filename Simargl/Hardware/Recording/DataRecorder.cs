using Simargl.Hardware.Receiving;
using System.Collections.Concurrent;
using System.IO;

namespace Simargl.Hardware.Recording;

/// <summary>
/// Представляет средство записи данных.
/// </summary>
public sealed class DataRecorder
{
    /// <summary>
    /// Происходит при внутренней ошибке.
    /// </summary>
    public event FailedEventHandler? Failed;

    /// <summary>
    /// Поле для хранения очереди данных.
    /// </summary>
    private readonly ConcurrentQueue<DataReceiveResult> _Queue = [];

    /// <summary>
    /// Поле для хранения метода, распределяющего данные по файлам.
    /// </summary>
    private readonly DataRecorderDistributor _Distributor;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="distributor">
    /// Метод, распределяющий данные по файлам.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public DataRecorder(DataRecorderDistributor distributor, CancellationToken cancellationToken)
    {
        //  Установка метода, распределяющего данные по файлам.
        _Distributor = distributor;

        //  Запуск работы.
        _ = Task.Run(async delegate
        {
            //  Запуск работы.
            await ExecuteAsync(cancellationToken).ConfigureAwait(false);
        }, cancellationToken);
    }

    /// <summary>
    /// Добавляет данные для записи в очередь.
    /// </summary>
    /// <param name="data">
    /// Данные для записи.
    /// </param>
    public void AddData(DataReceiveResult data)
    {
        //  Добавление данных в очередь.
        _Queue.Enqueue(data);
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
    private async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение основной работы.
                await InvokeAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Создание аргументов события.
                    FailedEventArgs e = new(ex);

                    //  Вызов события.
                    OnFailed(e);
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
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
        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Извлечение результатов из очереди.
            while (_Queue.TryDequeue(out DataReceiveResult? data) &&
                !cancellationToken.IsCancellationRequested)
            {
                //  Получение пути к файлу.
                string path = _Distributor(data);

                //  Получение информации о каталоге.
                DirectoryInfo? directory = new FileInfo(path).Directory;

                //  Стек каталогов.
                Stack<DirectoryInfo> directories = [];

                //  Перебор каталогов.
                while (directory is not null)
                {
                    //  Добавление каталога в стек.
                    directories.Push(directory);

                    //  Переход к корневому каталогу.
                    directory = directory.Parent;
                }

                //  Извлечение каталогов из стека.
                while (directories.TryPop(out directory))
                {
                    //  Проверка каталога.
                    if (directory is not null)
                    {
                        //  Проверка существования каталога.
                        Directory.CreateDirectory(directory.FullName);
                    }
                }

                //  Открытие файла для записи.
                using FileStream stream = new(path, FileMode.Append, FileAccess.Write, FileShare.None);

                //  Сохранение данных в файл.
                await data.SaveAsync(stream, cancellationToken).ConfigureAwait(false);
            }

            //  Задержка перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="Failed"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnFailed(FailedEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref Failed)?.Invoke(this, e);
    }
}
