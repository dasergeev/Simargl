//using System;
//using System.Collections.Generic;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Представляет коллекцию осей.
//    /// </summary>
//    public class AxisCollection
//    {
//        /// <summary>
//        /// Происходит при добавлении новой оси в коллекцию.
//        /// </summary>
//        public event EventHandler<AxisCollectionEventArgs> Added;

//        /// <summary>
//        /// Поле для хранения словаря осей.
//        /// </summary>
//        private SortedDictionary<int, Axis> _Axes;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="groups">
//        /// Коллекция групп сечений.
//        /// </param>
//        internal AxisCollection(SectionGroupCollection groups)
//        {
//            _Axes = new SortedDictionary<int, Axis>();
//            Groups = groups;
//        }
        
//        /// <summary>
//        /// Очищает коллекцию.
//        /// </summary>
//        internal void Clear()
//        {
//            _Axes.Clear();
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
//        /// Возвращает коллекцию групп сечений.
//        /// </summary>
//        internal SectionGroupCollection Groups { get; }

//        /// <summary>
//        /// Возвращает ось с заданным номером.
//        /// </summary>
//        /// <param name="number">
//        /// Номер оси.
//        /// </param>
//        /// <returns>
//        /// Ось с заданным номером.
//        /// </returns>
//        public Axis this[int number]
//        {
//            get
//            {
//                if (_Axes.ContainsKey(number))
//                {
//                    return _Axes[number];
//                }
//                Axis axis = new Axis(Groups, number);
//                _Axes.Add(number, axis);
//                OnAdded(new AxisCollectionEventArgs(axis));
//                return axis;
//            }
//        }

//        /// <summary>
//        /// Возвращает коллекцию номеров осей.
//        /// </summary>
//        public IEnumerable<int> Numbers
//        {
//            get
//            {
//                return _Axes.Keys;
//            }
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="Added"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnAdded(AxisCollectionEventArgs e)
//        {
//            Added?.Invoke(this, e);
//        }
//    }
//}
