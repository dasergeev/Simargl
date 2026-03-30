namespace Apeiron.Platform.Teltonika;

/// <summary>
/// Представляет класс управления модемом.
/// </summary>
public sealed class ModemLowLevel
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
    public ModemLowLevel(ICommandLine[] interfaces)
    {
        //  Проверка ссылки.
        Check.IsNotNull(interfaces, nameof(interfaces));

        //  Проверка длинны массива.
        Check.IsNotEmpty(interfaces, nameof(interfaces));

        //  Инициализация поля.
        _Interfaces = interfaces;
    }


    /// <summary>
    /// Представляет функцию, посылающую AT команду модему.
    /// </summary>
    /// <param name="command">
    /// Комманда.
    /// </param>
    /// <param name="response">
    /// Ответ модема.
    /// </param>
    /// <returns>
    /// <c>true</c> - команда выполнена, 
    /// <c>false</c> - команда не выполнена.
    /// </returns>
    public bool SendATCommand(string command,out string response)
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
                    // Выполнение команды
                    response = one.SendCommand($"gsmctl -A \"{command}\"");

                    return true;
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

            }

            //  Установка значения по умолчанию.
            response = string.Empty;

            //   Возврат результата
            return false;
        }
    }


    /// <summary>
    /// Представляет функцию, перезапуска модема программно. 
    /// </summary>
    /// <returns>
    /// <c>true</c> - модем перезапущен, 
    /// <c>false</c> - модем не перезапущен.
    /// </returns>
    public bool SoftResturtModem()
    {
        lock (_SyncRoot)
        {
            // Выполнение команды
            var result = SendATCommand("AT+CFUN=1,1", out string response);

            //  Проверка ответа.
            if (result && response.Contains("OK"))
            {
                //  Возврат результата
                return true;
            }

            //  Возврат результата
            return false;
        }
    }


    /// <summary>
    /// Представляет функцию, перезапуска модема аппаратно. 
    /// </summary>
    /// <returns>
    /// <c>true</c> - модем перезапущен, 
    /// <c>false</c> - модем не перезапущен.
    /// </returns>
    public bool HardResturtModem()
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
                    // Выполнение команды
                    var result = one.SendCommand($"gsmctl -D");

                    //  Проверка ответа.
                    if (result.Contains("OK"))
                    {
                        //  Возврат результата
                        return true;
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
                    //  Отключение инетрфейса
                    one.Disconnect();
                }

            }
            //   Возврат результата
            return false;
        }
    }



    /// <summary>
    /// Представляет функцию, посылающую AT команду модему.
    /// </summary>
    /// <returns>
    /// Результат выполнения.
    /// </returns>
    public ModemInfo? GetModemInfo()
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
                    // Выполнение команды
                    var response = one.SendCommand($"gsmctl -iJxq");

                    using StringReader reader = new(response);

                    var imei = Check.IsNotNull(reader.ReadLine(), nameof(reader));
                    var iccid = Check.IsNotNull(reader.ReadLine(), nameof(reader));
                    var imsi = Check.IsNotNull(reader.ReadLine(), nameof(reader));
                    var signal = int.Parse(Check.IsNotNull(reader.ReadLine(), nameof(reader)));

                    ModemInfo info = new(imei, iccid, imsi, signal);

                    return info;
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

            }

            //   Возврат результата
            return null;
        }
    }
}
