// Начальный вывод меню.

// Отображение меню программы.
MenuDisplay();

static void MenuDisplay()
{
    Console.Clear();
    const string welcomeText = "КОНСОЛЬ УПРАВЛЕНИЯ ОРКЕСТРАТОРОМ.\n\n\n\r";
    // Выводим строку в центре экрана.
    int centerX = (Console.WindowWidth / 2) - (welcomeText.Length / 2);
    Console.SetCursorPosition(centerX, 2);
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write(welcomeText);
    Console.ResetColor();
    // Вывод меню консоли.
    Console.WriteLine("\n\tМЕНЮ РАБОТЫ ПРОГРАММЫ\n");
    Console.WriteLine("\t1) Проверка подключения к центральному узлу оркестратора - \"1\"");
    Console.WriteLine("\t2) Получение списка подключенных хостов - \"2\"");
    Console.WriteLine("\t3) Остановить все управляемые службы - \"3\"");
    Console.WriteLine("\t4) Запустить все управляемые службы - \"4\"");
    Console.WriteLine("\t5) Обновить все управляемые службы на хостах - \"5\"");
    Console.WriteLine("\t6) Остановить работу и выйти - \"ESC\"");
    Console.SetCursorPosition(8, 14);
}

///// <summary>
///// Оснвная функция запуска обработки задач.
///// </summary>
//static void OrchestratorAction(ConsoleClient consoleClient, string headerTopic, string footerTopic, PackageFormat packageFormat, CancellationTokenSource cancellationTokenSource)
//{
//    // Проверки входящих параметров.
//    Check.IsNotNull(consoleClient, nameof(consoleClient));
//    Check.IsNotNull(headerTopic, nameof(headerTopic));
//    Check.IsNotNull(footerTopic, nameof(headerTopic));
//    Check.IsNotNull(cancellationTokenSource, nameof(cancellationTokenSource));

//    // Вывод заголовка
//    Console.Clear();
//    Console.ForegroundColor = ConsoleColor.DarkMagenta;
//    Console.WriteLine($"\n{headerTopic}\n");
//    Console.ResetColor();

//    // Запуск задачи на пуле потоков.
//    _ = Task.Run(async () =>
//    {
//        await consoleClient.SimpleTcpClientAsync(packageFormat, cancellationTokenSource.Token);
//    }, cancellationTokenSource.Token).ConfigureAwait(true);

//    // Выводим текст после выполнения задачи.
//    if (footerTopic.Length > 0)
//        Console.WriteLine($"{footerTopic}");

//    Console.ReadKey();
//    // Отменяем запущенные задачи.
//    cancellationTokenSource.Cancel();
//    consoleClient.Dispose();
//    // Отображение меню программы.
//    MenuDisplay();
//}

//// Выбор действия.
//ConsoleKeyInfo key;
//do
//{
//    key = Console.ReadKey();

//    switch (key.Key)
//    {
//        case ConsoleKey.D1:
//            {
//                // Создаём токен отмены.
//                using var cancellationTokenSourceD1 = new CancellationTokenSource();
//                CancellationToken cancellationToken = cancellationTokenSourceD1.Token;

//                // Создаём клиента.
//                ConsoleClient consoleClient = new();

//                OrchestratorAction(consoleClient,
//                    "Проверка подключения к центральному узлу оркестратора.",
//                    "Проверка подключения к регистратору прошла успешно.", 
//                    PackageFormat.GeneralConsolePackage, 
//                    cancellationTokenSourceD1);

//                break;
//            }
//        case ConsoleKey.D2:
//            {
//                // Создаём токен отмены.
//                using var cancellationTokenSourceD2 = new CancellationTokenSource();
                
//                ConsoleClient consoleClient = new();

//                OrchestratorAction(consoleClient,
//                    "Получение списка подключенных хостов.",
//                    "",
//                    PackageFormat.GetHostListConsolePackage,
//                    cancellationTokenSourceD2);              

//                break;
//            }
//        case ConsoleKey.D3:
//            {
//                // Создаём токен отмены.
//                using var cancellationTokenSourceD3 = new CancellationTokenSource();

//                ConsoleClient consoleClient = new();

//                OrchestratorAction(consoleClient,
//                    "Остановить все управляемые службы.",
//                    "Отправлен пакет на остановку служб.",
//                    PackageFormat.StopServicesConsolePackage,
//                    cancellationTokenSourceD3);

//                break;
//            }
//        case ConsoleKey.D4:
//            {
//                // Создаём токен отмены.
//                using var cancellationTokenSourceD4 = new CancellationTokenSource();

//                ConsoleClient consoleClient = new();

//                OrchestratorAction(consoleClient,
//                   "Запустить все управляемые службы.",
//                   "Отправлен пакет на запуск служб.",
//                   PackageFormat.StartServicesConsolePackage,
//                   cancellationTokenSourceD4);

//                break;
//            }
//        case ConsoleKey.D5:
//            {
//                // Создаём токен отмены.
//                using var cancellationTokenSourceD5 = new CancellationTokenSource();

//                ConsoleClient consoleClient = new();

//                OrchestratorAction(consoleClient,
//                   "Обновить управляемые службы на всех хостах.",
//                   "Отправлен пакет на обновление управляемых служб.",
//                   PackageFormat.CopyServicesConsolePackage,
//                   cancellationTokenSourceD5);

//                break;
//            }
//        default:
//            break;
//    }
//}
//while (key.Key != ConsoleKey.Escape); // по нажатию на Escape завершаем цикл