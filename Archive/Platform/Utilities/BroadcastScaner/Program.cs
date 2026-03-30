// See https://aka.ms/new-console-template for more information



using Apeiron.Platform.Sensors;

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
    if (array.Length == 0)
    {
        //  Продолжение.
        continue;
    }

    //  Проверка команды
    if (array[0].Equals("exit"))
    {
        //  Продолжение.
        break;
    }

    SensorScaner.Scan(default);
}