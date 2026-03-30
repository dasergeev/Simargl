// See https://aka.ms/new-console-template for more information
using Apeiron.Platform.Sensors;
using Apeiron.Support;
using System.Text;

//  Функция вывода справки.
static void PrintHelp()
{
    //  Выводит справку.
    Console.WriteLine("Команды:");
    Console.WriteLine("help: вывод справки.");
    Console.WriteLine("params: вывести список параметров.");
    Console.WriteLine("list [param] [param] [param]: сканирование сети в параметрах на наличие датчиков.");
    Console.WriteLine("set [index] [param] [value]: установить значение свойства");
    Console.WriteLine("get [index] [param]: прочитать значение свойства");
    Console.WriteLine("read [index]: обновить значение регистров");
    Console.WriteLine("write [index]: сохранить значение регистров в датчик.");
    Console.WriteLine("start [index]: запуск получения и пересылки данных");
    Console.WriteLine("stop [index]: остановка получения и пересылки данных.");
    Console.WriteLine("reset [index]: перезагрузка датчика.");
    Console.WriteLine("clear [index]: очистка статистики.");
    Console.WriteLine("print [index]: выгружает значение всех свойств.");
    Console.WriteLine("exit: выход из утилиты.");
}


//  Создание интерфейса
AdxlManager manager = new (default);

//  Цикл до команды выхода.
while (true)
{


    //  Вывод состояния
    Console.WriteLine("Введите команду:");

    //  Чтение команды
    var line = Console.ReadLine();

    //  Проверка получения команды.
    if (line == null)
    {
        //  Продолжение цикла.
        continue;
    }

    //  Расбор команды.
    var array = line.Split(' ');    

    //  Проверка длинны команды.
    if(array.Length == 0)
    {
        //  Продолжение.
        continue;
    }

    try
    {
        //  Проверка команды
        if (array[0].Equals("help"))
        {
            //  Вывод справки.
            PrintHelp();

            //  Продолжение.
            continue;
        }


        //  Проверка команды
        if (array[0].Equals("params"))
        {
            //  Получения списка свойств.
            var infos = AdxlSensor.GetAllProperty().OrderBy(x => x.Name);

            //  Создание буфера.
            var result = new StringBuilder(1024);

            //  Цикл по всем свойствам.
            foreach (var info in infos)
            {
                //  Добавление строки.
                result.Append($"{info.Name}: {info.PropertyType}\r\n");
            }

            //  Вывод ответа.
            Console.WriteLine(result.ToString());

            //  Продолжение.
            continue;
        }

        //  Проверка команды
        if (array[0].Equals("list"))
        {
            //  Проверка количества параметров.
            if (array.Length == 4)
            {
                //  Получение маски.
                string mask = $"{array[1]}.{array[2]}.{array[3]}.";

                //  Сканирование
                await manager.ScanAsync(mask).ConfigureAwait(false);

                //  Инициализация индекса
                int index = 0;

                //  Цикл по всем найденым
                foreach (var one in manager.ScanedList)
                {
                    //  Вывод ответа.
                    Console.WriteLine($"{index}: {one.Address}");

                    //  Инкремент индекса.
                    index++;
                }

            }
            else
            {
                //  Вывод в консоль.
                Console.WriteLine("Не корректное количество параметров.");
                //  Вывод справки.
                PrintHelp();
            }
            //  Продолжение.
            continue;
        }

        //  Проверка команды
        if (array[0].Equals("set"))
        {
            //  Проверка количества параметров.
            if (array.Length == 4)
            {
                //  Установка значения.
                manager.ScanedList[int.Parse(array[1])].SetValue(array[2], array[3]);
            }
            else
            {
                //  Вывод в консоль.
                Console.WriteLine("Не корректное количество параметров.");
                //  Вывод справки.
                PrintHelp();
            }
            //  Продолжение.
            continue;
        }

        //  Проверка команды
        if (array[0].Equals("get"))
        {
            //  Проверка количества параметров.
            if (array.Length == 3)
            {
                //  Чтение значения
                var value = manager.ScanedList[int.Parse(array[1])].GetValue(array[2]);

                //  Вывод в консоль.
                Console.WriteLine(value);
            }
            else
            {
                //  Вывод в консоль.
                Console.WriteLine("Не корректное количество параметров.");
                //  Вывод справки.
                PrintHelp();
            }
            //  Продолжение.
            continue;
        }

        //  Проверка команды
        if (array[0].Equals("read"))
        {
            //  Проверка количества параметров.
            if (array.Length == 2)
            {
                //  Создание источника токена.
                CancellationTokenSource tokenSource = new();
                
                //  Отмена токена через 10 секунд
                tokenSource.CancelAfter(10000);

                //  Загрузка данных
                await manager.ScanedList[int.Parse(array[1])].LoadAsync(tokenSource.Token);
                
                //  Вывод в консоль.
                Console.WriteLine("Загружено успешно.");
            }
            else
            {
                //  Вывод в консоль.
                Console.WriteLine("Не корректное количество параметров.");
                //  Вывод справки.
                PrintHelp();
            }
            //  Продолжение.
            continue;
        }

        //  Проверка команды
        if (array[0].Equals("write"))
        {
            //  Проверка количества параметров.
            if (array.Length == 2)
            {
                //  Создание источника токена.
                CancellationTokenSource tokenSource = new ();

                //  Отмена токена через 10 секунд
                tokenSource.CancelAfter(10000);

                //  Сохранение свойств
                await manager.ScanedList[int.Parse(array[1])].UpdateAsync(tokenSource.Token);

                //  Вывод в консоль.
                Console.WriteLine("Записано успешно.");
            }
            else
            {
                //  Вывод в консоль.
                Console.WriteLine("Не корректное количество параметров.");
                //  Вывод справки.
                PrintHelp();
            }
            //  Продолжение.
            continue;
        }

        //  Проверка команды
        if (array[0].Equals("start"))
        {
            //  Проверка количества параметров.
            if (array.Length == 2)
            {
                //  Создание источника токена.
                CancellationTokenSource tokenSource = new ();

                //  Отмена токена через 10 секунд
                tokenSource.CancelAfter(10000);

                //  Запуск датчика
                await manager.ScanedList[int.Parse(array[1])].StartAsync(tokenSource.Token);

                //  Вывод в консоль.
                Console.WriteLine("Записано успешно.");
            }
            else
            {
                //  Вывод в консоль.
                Console.WriteLine("Не корректное количество параметров.");
                //  Вывод справки.
                PrintHelp();
            }
            //  Продолжение.
            continue;
        }


        //  Проверка команды
        if (array[0].Equals("stop"))
        {
            //  Проверка количества параметров.
            if (array.Length == 2)
            {
                //  Создание источника токена.
                CancellationTokenSource tokenSource = new ();

                //  Отмена токена через 10 секунд
                tokenSource.CancelAfter(10000);

                //  Остановка датчика
                await manager.ScanedList[int.Parse(array[1])].StopAsync(tokenSource.Token);

                //  Вывод в консоль.
                Console.WriteLine("Записано успешно.");
            }
            else
            {
                //  Вывод в консоль.
                Console.WriteLine("Не корректное количество параметров.");
                //  Вывод справки.
                PrintHelp();
            }
            //  Продолжение.
            continue;
        }


        //  Проверка команды
        if (array[0].Equals("reset"))
        {
            //  Проверка количества параметров.
            if (array.Length == 2)
            {
                //  Создание источника токена.
                CancellationTokenSource tokenSource = new ();

                //  Отмена токена через 10 секунд
                tokenSource.CancelAfter(10000);

                //  Перезагрузка датчика
                await manager.ScanedList[int.Parse(array[1])].ResetAsync(tokenSource.Token);

                //  Вывод в консоль.
                Console.WriteLine("Записано успешно.");
            }
            else
            {
                //  Вывод в консоль.
                Console.WriteLine("Не корректное количество параметров.");
                //  Вывод справки.
                PrintHelp();
            }
            //  Продолжение.
            continue;
        }


        //  Проверка команды
        if (array[0].Equals("clear"))
        {
            //  Проверка количества параметров.
            if (array.Length == 2)
            {
                //  Создание источника токена.
                CancellationTokenSource tokenSource = new();

                //  Отмена токена через 10 секунд
                tokenSource.CancelAfter(10000);

                //  Очистка информации
                await manager.ScanedList[int.Parse(array[1])].ClearErrorAsync(tokenSource.Token);

                //  Очистка информации
                await manager.ScanedList[int.Parse(array[1])].ClearTempAsync(tokenSource.Token);

                //  Очистка информации
                await manager.ScanedList[int.Parse(array[1])].ClearVoltageAsync(tokenSource.Token);

                //  Вывод в консоль.
                Console.WriteLine("Записано успешно.");
            }
            else
            {
                //  Вывод в консоль.
                Console.WriteLine("Не корректное количество параметров.");
                //  Вывод справки.
                PrintHelp();
            }
            //  Продолжение.
            continue;
        }

        //  Проверка команды
        if (array[0].Equals("print"))
        {
            //  Проверка количества параметров.
            if (array.Length == 2)
            {
                //  Получения списка свойств.
                var infos = AdxlSensor.GetAllProperty().OrderBy(x => x.Name);

                //  Создание буфера.
                var result = new StringBuilder(1024);

                //  Цикл по всем свойствам.
                foreach (var info in infos)
                {
                    //  Добавление строки.
                    result.Append($"{info.Name}: {manager.ScanedList[int.Parse(array[1])].GetValue(info.Name)}\r\n");
                }

                //  Вывод ответа.
                Console.WriteLine(result.ToString());

                //  Продолжение.
                continue;
            }
            else
            {
                //  Вывод в консоль.
                Console.WriteLine("Не корректное количество параметров.");
                //  Вывод справки.
                PrintHelp();
            }
            //  Продолжение.
            continue;
        }
        //  Проверка команды
        if (array[0].Equals("exit"))
        {
            break;
        }


        //  Очистка коносоли.
        Console.Clear();

        //  Вывод в консоль.
        Console.WriteLine("Не корректная команда.");
    }
    catch (Exception ex)
    {
        //  Проверка исключения
        if (ex.IsSystem())
        {
            if(ex is not OperationCanceledException)
            {
                //  Выброс исключения.
                throw;
            }
        }

        //  Вывод ошибки в консоль
        Console.WriteLine(ex.Message);

        //  Проверка исключения.
        if(ex.InnerException != null)
        {
            //  Вывод ошибки в консоль
            Console.WriteLine(ex.InnerException.Message);
        }
    }
}


