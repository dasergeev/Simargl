using System.Globalization;
using System.IO;
using Simargl.Designing.Utilities;

namespace Simargl.Platform.Teltonika;

/// <summary>
/// Представляет класс управления процессами.
/// </summary>
public sealed class ProcessManager
{
    /// <summary>
    /// Представляет объект синхронизации класса.
    /// </summary>
    private readonly object _SyncRoot = new();

    /// <summary>
    /// Поле массива интерфейсов командой строки.
    /// </summary>
    private readonly ICommandLine[] _Interfaces;

    /// <summary>
    /// Инициализирует объект класса.
    /// </summary>
    /// <param name="interfaces">
    /// Интерфейсы командной строки.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="interfaces"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="interfaces"/> передан массив нулевой длинны.
    /// </exception>
    public ProcessManager(ICommandLine[] interfaces)
    {
        //  Проверка ссылки.
        IsNotNull(interfaces, nameof(interfaces));

        //  Проверка длинны массива.
        IsNotEmpty(interfaces, nameof(interfaces));

        //  Инициализация поля.
        _Interfaces = interfaces;
    }

    /// <summary>
    /// Представляет функцию, получающую идентификатор процесса.
    /// </summary>
    /// <param name="pathOfProcess">
    /// Путь до файла процесса.
    /// </param>
    /// <param name="pid">
    /// Идентификатор.
    /// </param>
    /// <returns>
    /// <c>true</c> - получен идентификатор, 
    /// <c>false</c> - идентификатор не получен, или не удалось выполнить команду.
    /// </returns>
    private bool GetPidProcess(string pathOfProcess, out uint pid)
    {
        lock (_SyncRoot)
        {
            //  Цикл по всем интерфейсам
            foreach (var one in _Interfaces)
            {
                try
                {
                    //  Подключение.
                    one.Connect();
                }
                catch (Exception ex)
                {
                    //  Проверка исключения
                    if (ex.IsSystem())
                    {
                        //  Перенаправление исключения
                        throw;
                    }

                    //  Продолжение цикла.
                    continue;
                }

                try
                {
                    //  Получение состовляющей пути к файлу запуска
                    var subString = pathOfProcess.Split('/');

                    //  Получение имени процесса
                    var processName = subString[^1];

                    //  Выполнение команды на получение PID
                    var result = one.SendCommand($"/bin/ps | awk '/{processName}/ && !/awk/ {{print $1}}'");

                    //  Проверка ссылки
                    if (result != null)
                    {
                        //  Проверка результата.
                        if (result.Length > 0)
                        {
                            // Получение массива строк результата.
                            var subArray = result.Split('\n');

                            //  Получение PID первого в массиве
                            pid = uint.Parse(subArray[0]);

                            //  Возврат результата
                            return true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    //  Проверка исключения
                    if (ex.IsSystem())
                    {
                        //  Перенаправление исключения
                        throw;
                    }

                    //  Продолжение цикла.
                    continue;
                }
                finally
                {
                    //  Отключение от интерфейса.
                    one.Disconnect();
                }

            }

            //  Установка значение по умолчанию
            pid = 0;

            //  Возврат результата.
            return false;
        }
    }


    /// <summary>
    /// Представляет функцию перезагрузки устройства подключения.
    /// </summary>
    public void RebootDevice()
    {
        lock (_SyncRoot)
        {
            //  Цикл по всем интерфейсам
            foreach (var one in _Interfaces)
            {
                try
                {
                    //  Подключение.
                    one.Connect();
                }
                catch (Exception ex)
                {
                    //  Проверка исключения
                    if (ex.IsSystem())
                    {
                        //  Перенаправление исключения
                        throw;
                    }

                    //  Продолжение цикла.
                    continue;
                }
                try
                {
                    //  Выполнения команды остановки.
                    one.SendCommand($"reboot");
                }
                finally
                {
                    //  Отключение интерфейса
                    one.Disconnect();
                }

            }
        }
    }

    /// <summary>
    /// Представляет функцию проверки запущен ли процесс.
    /// </summary>
    /// <param name="pathOfProcess">
    /// Путь до файла запуска.
    /// </param>
    /// <returns>
    /// <c>true</c> - процесс запущен, 
    /// <c>false</c> - процесс не запущен.
    /// </returns>
    public bool CheckProcess(string pathOfProcess)
    {
        //  Проверка и возврат результата.
        return (GetPidProcess(pathOfProcess, out _));
    }


    /// <summary>
    /// Представляет функцию, завершение процесса. 
    /// </summary>
    /// <param name="pathOfProcess">
    /// Путь до файла процесса.
    /// </param>
    /// <returns>
    /// <c>true</c> - процесс завершен, 
    /// <c>false</c> - процесс не завершен.
    /// </returns>
    /// <remarks>
    /// В случае завершение сервиса, сервис перезапуститься.
    /// </remarks>
    public bool KillService(string pathOfProcess)
    {
        lock (_SyncRoot)
        {
            //  Проверка запущен ли процесс, и получение PID
            if (GetPidProcess(pathOfProcess, out uint pidStart))
            {
                //  Цикл по всем интерфейсам
                foreach (var one in _Interfaces)
                {
                    try
                    {
                        //  Подключение.
                        one.Connect();
                    }
                    catch (Exception ex)
                    {
                        //  Проверка исключения
                        if (ex.IsSystem())
                        {
                            //  Перенаправление исключения
                            throw;
                        }

                        //  Продолжение цикла.
                        continue;
                    }

                    try
                    {
                        // Выполнение команды
                        one.SendCommand($"kill -9 {pidStart}");
                    }
                    catch (Exception ex)
                    {
                        //  Проверка исключения
                        if (ex.IsSystem())
                        {
                            //  Перенаправление исключения
                            throw;
                        }

                        //  Продолжение цикла.
                        continue;
                    }
                    finally
                    {
                        //  Отключение инетрфейса
                        one.Disconnect();
                    }

                    //  Получение PID
                    GetPidProcess(pathOfProcess, out uint pidEnd);

                    //  Проверка измениения PID
                    if (pidStart != pidEnd)
                    {
                        //   Возврат результата
                        return true;
                    }
                }
                //   Возврат результата
                return false;
            }
        }
        //   Возврат результата
        return true;
    }


    /// <summary>
    /// Представляет функцию перезапуска сервиса.
    /// </summary>
    /// <param name="pathOfProcess">
    /// Путь до файла управления сервисом.
    /// </param>
    /// <returns>
    /// <c>true</c> - служба перезапущена, 
    /// <c>false</c> - служба не перезапущена.
    /// </returns>
    public bool RestartService(string pathOfProcess)
    {
        lock (_SyncRoot)
        {
            //  Получение PID
            GetPidProcess(pathOfProcess, out uint pidStart);

            //  Цикл по всем интерфейсам
            foreach (var one in _Interfaces)
            {
                try
                {
                    //  Подключение.
                    one.Connect();
                }
                catch (Exception ex)
                {
                    //  Проверка исключения
                    if (ex.IsSystem())
                    {
                        //  Перенаправление исключения
                        throw;
                    }

                    //  Продолжение цикла.
                    continue;
                }

                try
                {
                    //  Выполнения команды остановки.
                    one.SendCommand($"{pathOfProcess} restart");
                }
                finally
                {
                    //  Отключение интерфейса
                    one.Disconnect();
                }

                //  Получение PID
                GetPidProcess(pathOfProcess, out uint pidEnd);

                //  Проверка измениения PID
                if (pidStart != pidEnd)
                {
                    //   Возврат результата
                    return true;
                }
            }
        }
        //   Возврат результата
        return false;
    }


    /// <summary>
    /// Представляет функцию получения геолокации.
    /// </summary>
    /// <returns>
    /// Возвращает данные геолокации.
    /// </returns>
    public GeolocationInfo? GetGeolocation()
    {
        lock (_SyncRoot)
        {
            //  Цикл по всем интерфейсам
            foreach (var one in _Interfaces)
            {
                try
                {
                    //  Подключение.
                    one.Connect();
                }
                catch (Exception ex)
                {
                    //  Проверка исключения
                    if (ex.IsSystem())
                    {
                        //  Перенаправление исключения
                        throw;
                    }

                    //  Продолжение цикла.
                    continue;
                }

                try
                {
                    //  Выполнения команды получения данных.
                    string result = one.SendCommand($"gpsctl -ixaveup");

                    //  Создание читателя
                    using var reader = new StringReader(result);

                    //  Разбор широты
                    float latitude = float.Parse(IsNotNull(reader.ReadLine(), nameof(reader)), CultureInfo.InvariantCulture);

                    //  Разбор долготы
                    float longitude = float.Parse(IsNotNull(reader.ReadLine(), nameof(reader)), CultureInfo.InvariantCulture);

                    //  Разбор высоты
                    float altitude = float.Parse(IsNotNull(reader.ReadLine(), nameof(reader)), CultureInfo.InvariantCulture);

                    //  Разбор скорости
                    float speed = float.Parse(IsNotNull(reader.ReadLine(), nameof(reader)), CultureInfo.InvariantCulture);

                    //  Чтение времени.
                    string time = IsNotNull(reader.ReadLine(), nameof(reader));

                    //  Разбор точности
                    float accuracy = float.Parse(IsNotNull(reader.ReadLine(), nameof(reader)), CultureInfo.InvariantCulture);

                    //  Разбор количества спутников.
                    int satelite = int.Parse(IsNotNull(reader.ReadLine(), nameof(reader)));

                    //  Создание класса данных
                    GeolocationInfo info = new(latitude, longitude, altitude, time, (int)speed, satelite, accuracy);

                    //  Возврат значения.
                    return info;
                }
                finally
                {
                    //  Отключение интерфейса
                    one.Disconnect();
                }
            }
        }
        //   Возврат результата
        return null;
    }
}
