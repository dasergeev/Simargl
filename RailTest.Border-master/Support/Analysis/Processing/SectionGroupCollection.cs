using RailTest.Frames;
using System.Collections;
using System.Collections.Generic;

namespace RailTest.Border.Support.Analysis
{
    /// <summary>
    /// Представляет коллекцию групп каналов по сечениям.
    /// </summary>
    public class SectionGroupCollection : IReadOnlyList<SectionGroup>
    {
        /// <summary>
        /// Постоянная, определяющая количество элементов в коллекции.
        /// </summary>
        private const int _Count = 21;

        /// <summary>
        /// Поле для хранения массива элементов.
        /// </summary>
        private readonly SectionGroup[] _Items;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="frame">
        /// Кадр регистрации.
        /// </param>
        /// <param name="file">
        /// Имя файла.
        /// </param>
        public SectionGroupCollection(Frame frame, string file)
        {
            File = file;
            _Items = new SectionGroup[_Count];
            for (int i = 0; i != _Count; ++i)
            {
                double position = i * 0.544;
                if (i > 15)
                {
                    position += 0.317;
                }
                _Items[i] = new SectionGroup(i, position, frame);
            }
        }

        /// <summary>
        /// Возвращает элемент по указанному индексу.
        /// </summary>
        /// <param name="index">
        /// Индекс элемента.
        /// </param>
        /// <returns>
        /// Элемент по указанному индексу.
        /// </returns>
        public SectionGroup this[int index] => _Items[index];

        /// <summary>
        /// Возвращает количество элементов в коллекции.
        /// </summary>
        public int Count => _Count;

        /// <summary>
        /// Возвращает имя файла.
        /// </summary>
        public string File { get; }

        /// <summary>
        /// Выполняет сброс каналов в кадр.
        /// </summary>
        /// <param name="frame">
        /// Кадр, в который необходимо поместить каналы.
        /// </param>
        public void Flush(Frame frame)
        {
            for (int i = 0; i != _Count; ++i)
            {
                _Items[i].Flush(frame);
            }
        }

        /// <summary>
        /// Выполняет локализацию пиков.
        /// </summary>
        /// <param name="level">
        /// Уровень чувствительности.
        /// </param>
        public void Localization(double level)
        {
            for (int i = 0; i != _Count; ++i)
            {
                _Items[i].Localization(level);
            }
            double[] leftLoad = new double[2];
            double[] rightLoad = new double[2];
            switch (File)
            {
                case "Vp0_0.0022":
                    leftLoad[0] = 11.9;
                    leftLoad[1] = 11.8;
                    rightLoad[0] = 12.16;
                    rightLoad[1] = 12.5;
                    break;
                case "Vp0_0.0023":
                    leftLoad[0] = 11.8;
                    leftLoad[1] = 11.9;
                    rightLoad[0] = 12.5;
                    rightLoad[1] = 12.16;
                    break;
                case "Vp0_0.0026":
                    leftLoad[0] = 52.59;
                    leftLoad[1] = 91.46;
                    rightLoad[0] = 39.41;
                    rightLoad[1] = 77.54;
                    break;
                case "Vp0_0.0027":
                    leftLoad[0] = 91.46;
                    leftLoad[1] = 52.59;
                    rightLoad[0] = 77.54;
                    rightLoad[1] = 39.41;
                    break;
            }
            for (int i = 0; i != _Count; ++i)
            {
                _Items[i].Left.Peaks[0].Load = leftLoad[0];
                _Items[i].Left.Peaks[1].Load = leftLoad[1];
                _Items[i].Right.Peaks[0].Load = rightLoad[0];
                _Items[i].Right.Peaks[1].Load = rightLoad[1];
            }
        }

        /// <summary>
        /// Определяет скорость.
        /// </summary>
        public void SpeedDetection()
        {
            for (int i = 0; i != 2; ++i)
            {
                double getSpeed(SectionGroup first, SectionGroup second)
                {
                    double distance = first.Position - second.Position;
                    return 0.5 * (distance / (first.Left.Peaks[i].Time - second.Left.Peaks[i].Time) +
                        distance / (first.Right.Peaks[i].Time - second.Right.Peaks[i].Time));
                }

                double speed = getSpeed(_Items[0], _Items[1]);
                _Items[0].Left.Peaks[i].Speed = speed;
                _Items[0].Right.Peaks[i].Speed = speed;
                for (int j = 1; j != _Count - 1; ++j)
                {
                    speed = 0.5 * (getSpeed(_Items[j], _Items[j - 1]) + getSpeed(_Items[j], _Items[j + 1]));
                    _Items[j].Left.Peaks[i].Speed = speed;
                    _Items[j].Right.Peaks[i].Speed = speed;
                }

                speed = getSpeed(_Items[_Count - 2], _Items[_Count - 1]);
                _Items[_Count - 1].Left.Peaks[i].Speed = speed;
                _Items[_Count - 1].Right.Peaks[i].Speed = speed;
            }
        }

        /// <summary>
        /// Выполняет нормализацию пиков.
        /// </summary>
        /// <param name="range">
        /// Область пика.
        /// </param>
        public void PeaksNormalization(double range)
        {
            for (int i = 0; i != _Count; ++i)
            {
                _Items[i].Left.Peaks[0].Normalization(range);
                _Items[i].Left.Peaks[1].Normalization(range);
                _Items[i].Right.Peaks[0].Normalization(range);
                _Items[i].Right.Peaks[1].Normalization(range);
            }
        }

        /// <summary>
        /// Выполняет обработку.
        /// </summary>
        public void Process()
        {
            Localization(500);
            SpeedDetection();
            PeaksNormalization(0.3);
        }

        /// <summary>
        /// Возвращает перечислитель коллекции.
        /// </summary>
        /// <returns>
        /// Перечислитель коллекции.
        /// </returns>
        public IEnumerator<SectionGroup> GetEnumerator()
        {
            return ((IReadOnlyList<SectionGroup>)_Items).GetEnumerator();
        }

        /// <summary>
        /// Возвращает перечислитель коллекции.
        /// </summary>
        /// <returns>
        /// Перечислитель коллекции.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Items.GetEnumerator();
        }
    }
}
