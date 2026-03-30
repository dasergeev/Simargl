using Ape90ExpressAnalysis;

////  Нормализация кадров.
Analyzer.Normalization();

////  Сжатие кадров.
//Analyzer.Compactification();

////  Построение петель.
//Analyzer.BuildingLoops();

//Frame frame = new(@"C:\Users\dsergeev\Desktop\ПА проверка по скоростям нагружения\10 мм с\Кадр 1.BIN");

//Channel force = frame.Channels["Act3 F"];
//Channel move = frame.Channels["L1"];

////  Создание средства записи в текстовый файл.
//using StreamWriter writer = new(Path.Combine(@"E:\", $"Source.csv"));


//writer.WriteLine("Move;Force");

//for (int i = 0; i < force.Length; i += 600)
//{
//    writer.WriteLine($"{move[i]};{force[i]}");
//}


