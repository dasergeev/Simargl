using Simargl.Payload;
using Simargl.Payload.Recording;
using System.Globalization;
using System.IO;

namespace Simargl.Projects.Egypt.EgyptXXX.Converter.Core;

/// <summary>
/// Представляет средство чтения RS-данных.
/// </summary>
public sealed class RSReader
{
    private readonly List<Block> _Blocks = [];

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="packages">
    /// Коллекция пакетов.
    /// </param>
    public RSReader(IEnumerable<DataPackage> packages)
    {
        //  Разбор коллекции пакетов.
        IOrderedEnumerable<UdpDatagram> udpDatagrams = packages
            .Select(x => (x as UdpDatagram)!)
            .Where(x => x is not null)
            .OrderBy(x => x.ReceiptTime);

        //  Состояние разбора.
        State state = State.Default;
        int movingIndex = 0;

        //  Текущий блок.
        Block? block = null;

        //  Перебор датаграмм.
        foreach (UdpDatagram udpDatagram in udpDatagrams)
        {
            //  Проверка блока.
            if (block is null ||
                block.Values[^1].Count > 0)
            {
                //  Создание блока.
                block = new(udpDatagram.ReceiptTime);
                _Blocks.Add(block);
            }

            //  Корректировка времени.
            block.ReceiptTime = udpDatagram.ReceiptTime;

            //  Создание потока.
            using MemoryStream stream = new(udpDatagram.Datagram);

            //  Создание средства чтения двоичных данных.
            using StreamReader reader = new(stream, Encoding.ASCII);

            //  Чтение текста.
            string text = reader.ReadToEnd();

            //  Разбор значений.
            IEnumerable<string> lines = text.Split('\n')
                .Select(x => x.Trim())
                .Where(x => x.Length > 0);

            //  Перебор строк.
            foreach (string line in lines)
            {
                //  Заголовок перемещения.
                if (line == "[11,12,13,14,15,16,17,19]")
                {
                    //  Изменение состояния.
                    state = State.Moving;
                    movingIndex = 0;
                    continue;
                }

                //  Заголовок давления.
                if (line == "[21]")
                {
                    state = State.Pressure;
                    continue;
                }

                //  Проверка значения перемещения.
                if (state == State.Moving &&
                    line.StartsWith("[257]:") &&
                    movingIndex < 8)
                {
                    //  Получение значения.
                    if (!double.TryParse(
                        line[6..].Trim().Split(' ')[0],
                        CultureInfo.InvariantCulture, out double value))
                    {
                        //  Установка значения по умолчанию.
                        value = 0;
                    }

                    //  Проверка значения.
                    if (value > 50000.0)
                    {
                        //  Установка значения по умолчанию.
                        value = 0;
                    }
                    else
                    {
                        //  Нормализация значения.
                        value /= 50000.0;
                    }

                    //  Добавление значения.
                    block.Values[movingIndex].Add(value);

                    //  Изменение состояния.
                    ++movingIndex;
                    continue;
                }

                //  Проверка значения давления.
                if (state == State.Pressure && 
                    line.StartsWith("[8]:"))
                {
                    //  Получение значения.
                    if (!double.TryParse(
                        line[4..].Trim().Split(' ')[0],
                        CultureInfo.InvariantCulture, out double value))
                    {
                        //  Установка значения по умолчанию.
                        value = 0;
                    }

                    //  Добавление значения.
                    block.Values[^1].Add(value);
                }

                //  Сброс состояния.
                state = State.Default;
            }
        }

        //  Перебор блоков.
        for (int i = 1; i < _Blocks.Count; i++)
        {
            //  Определение длительности.
            TimeSpan duration = _Blocks[i].ReceiptTime - _Blocks[i - 1].ReceiptTime;

            if (duration > TimeSpan.Zero)
            {
                double count = _Blocks[i].Values.Index().Where(x => x.Index < _Blocks[i].Values.Length - 1).Sum(x => x.Item.Count) / (double)(_Blocks[i].Values.Length - 1);
                _Blocks[i].Sampling = count / duration.TotalSeconds;
            }
            else
            {
                _Blocks[i].Sampling = 0;
            }

            if (i == 1)
            {
                _Blocks[0].Sampling = _Blocks[1].Sampling;
            }

        }

        StringBuilder builder = new();

        //  Перебор блоков.
        foreach (Block item in _Blocks)
        {
            builder.AppendLine($"[{item.ReceiptTime}] Sampling = {item.Sampling}");
            for (int i = 0; i < item.Values.Length; i++)
            {
                builder.Append($"  [{i}]: ");
                foreach (double value in item.Values[i])
                {
                    builder.Append(value);
                    builder.Append(", ");
                }

                builder.AppendLine();
            }
        }
        //_Blocks

        Console.WriteLine(builder.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="action"></param>
    public void Enum(int id, Action<DateTime, double> action)
    {
        //  Получение индекса.
        int index = id switch
        {
            11 => 0,
            12 => 1,
            13 => 2,
            14 => 3,
            15 => 4,
            16 => 5,
            17 => 6,
            19 => 7,
            21 => 8,
            _ => -1
        };

        if (index == -1) return;

        if (index == 8)
        {
            foreach (Block block in _Blocks)
            {
                if (block.Values[^1].Count > 0)
                {
                    action(block.ReceiptTime, block.Values[^1][0]);
                }
            }
            return;
        }

        foreach (Block block in _Blocks)
        {
            List<double> values = block.Values[index];
            if (values.Count < 1) continue;
            TimeSpan step = TimeSpan.FromSeconds(1.0 / block.Sampling);
            DateTime time = block.ReceiptTime - step * (values.Count - 1);
            foreach (double value in values)
            {
                action(time, value);
                time += step;
            }
        }
    }

    enum State
    {
        //  По умолчанию.
        Default,

        //  Ожидание перемещения.
        Moving,

        //  Ожидание давления.
        Pressure,
    }

    class Block(DateTime receiptTime)
    {
        /// <summary>
        /// Возвращает время получения.
        /// </summary>
        public DateTime ReceiptTime { get; set; } = receiptTime;

        public double Sampling = 0;

        public List<double>[] Values = [[], [], [], [], [], [], [], [], []];
    }
}
