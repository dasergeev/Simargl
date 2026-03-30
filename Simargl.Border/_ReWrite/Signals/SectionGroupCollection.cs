//using RailTest.Frames;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Представляет коллекцию групп сигналов.
//    /// </summary>
//    public class SectionGroupCollection : IEnumerable<SectionGroup>
//    {
//        /// <summary>
//        /// Постоянная, определяющая размер блока данных.
//        /// </summary>
//        public const int BlockSize = SignalBuilder.BlockSize;

//        /// <summary>
//        /// Постоянная, определяющая количество блоков.
//        /// </summary>
//        public const int BlockCount = SignalBuilder.BlockCount;

//        /// <summary>
//        /// Поле для хранения массива групп.
//        /// </summary>
//        private readonly SectionGroup[] _Groups;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        internal SectionGroupCollection()
//        {
//            _Groups = new SectionGroup[Count];
//            Signals = new List<Signal>();

//            for (int i = 0; i != Count; ++i)
//            {
//                var group = new SectionGroup(i + 1)
//                {
//                    Position = i * 0.544
//                };
//                if (i > 15)
//                {
//                    group.Position += 0.317;
//                }
//                _Groups[i] = group;
//                Signals.AddRange(group.Signals);
//            }

//            Frame = new Frame();
//            foreach (var signal in Signals)
//            {
//                Frame.Channels.Add(signal.Channel);
//            }

//            FrameNumber = 0;
//            while (File.Exists(GetFileName(FrameNumber)))
//            {
//                ++FrameNumber;
//            }

//            BlockIndex = 0;
//        }

//        /// <summary>
//        /// Возвращает текущего номер кадра.
//        /// </summary>
//        internal int FrameNumber { get; private set; }

//        /// <summary>
//        /// Возвращает имя файла.
//        /// </summary>
//        /// <param name="index">
//        /// Индекс файла.
//        /// </param>
//        /// <returns>
//        /// Имя файла.
//        /// </returns>
//        private static string GetFileName(int index)
//        {
//            return $"C:\\Source\\Vp0_0.{index:0000}";
//        }

//        /// <summary>
//        /// Возвращает кадр.
//        /// </summary>
//        internal Frame Frame { get; }

//        /// <summary>
//        /// Возвращает последний записанный индекс.
//        /// </summary>
//        public int BlockIndex { get; internal set; }
        
//        /// <summary>
//        /// Сохраняет текущее состояние.
//        /// </summary>
//        internal void Save()
//        {
//            Frame.Save(GetFileName(FrameNumber), StorageFormat.TestLab);
//            ++FrameNumber;
//        }

//        /// <summary>
//        /// Возвращает количество групп в коллекции.
//        /// </summary>
//        public int Count
//        {
//            get
//            {
//                return 21;
//            }
//        }

//        /// <summary>
//        /// Возвращает объект, который может быть использован для синхронизации доступа.
//        /// </summary>
//        public object SyncRoot
//        {
//            get
//            {
//                return this;
//            }
//        }

//        /// <summary>
//        /// Возвращает группу с указанным индексом.
//        /// </summary>
//        /// <param name="index">
//        /// Индекс группы.
//        /// </param>
//        /// <returns>
//        /// Группа.
//        /// </returns>
//        public SectionGroup this[int index]
//        {
//            get
//            {
//                return _Groups[index];
//            }
//        }

//        /// <summary>
//        /// Возвращает группу, которая принадлежит заданному сечению.
//        /// </summary>
//        /// <param name="section">
//        /// Номер сечения.
//        /// </param>
//        /// <returns>
//        /// Группа.
//        /// </returns>
//        public SectionGroup BySection(int section)
//        {
//            return this[section - 1];
//        }

//        /// <summary>
//        /// Возвращает все сигналы.
//        /// </summary>
//        public List<Signal> Signals { get; }

//        /// <summary>
//        /// Возвращает группу каналов, принадлежащую модулю с указанным индексом.
//        /// </summary>
//        /// <param name="index">
//        /// Индекс модуля.
//        /// </param>
//        /// <returns>
//        /// Группа.
//        /// </returns>
//        internal SideGroup ByModule(int index)
//        {
//            int position = index % 4;
//            SectionGroup sectionGroup = _Groups[(index - position) / 4];
//            switch (position)
//            {
//                case 0:
//                    return sectionGroup.Right.Internal;
//                case 1:
//                    return sectionGroup.Right.External;
//                case 2:
//                    return sectionGroup.Left.Internal;
//                default:
//                    return sectionGroup.Left.External;
//            }
//        }

//        /// <summary>
//        /// Обновляет данные.
//        /// </summary>
//        /// <param name="blockIndex">
//        /// Индекс чтения.
//        /// </param>
//        internal void Update(int blockIndex)
//        {
//            foreach (var group in this)
//            {
//                group.Update(blockIndex);
//            }
//        }

//        /// <summary>
//        /// Выставляет ноль.
//        /// </summary>
//        public void Zero()
//        {
//            lock (SyncRoot)
//            {
//                foreach (var group in this)
//                {
//                    group.Zero();
//                }
//            }
//        }

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        public IEnumerator<SectionGroup> GetEnumerator()
//        {
//            return ((IEnumerable<SectionGroup>)_Groups).GetEnumerator();
//        }

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return ((IEnumerable<SectionGroup>)_Groups).GetEnumerator();
//        }
//    }
//}
