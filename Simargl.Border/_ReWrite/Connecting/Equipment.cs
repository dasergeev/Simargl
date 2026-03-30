//using System;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Представляет доступ к аппаратуре.
//    /// </summary>
//    public class Equipment : IDisposable
//    {
//        /// <summary>
//        /// Поле для хранения построителя сигналов.
//        /// </summary>
//        private readonly SignalBuilder _SignalBuilder;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        public Equipment()
//        {
//            Receiver = new Receiver();
//            Modules = new ModuleCollection(Receiver);
//            Groups = new SectionGroupCollection();
//            _SignalBuilder = new SignalBuilder(Receiver, Groups);
//            Processing = new Processing(Groups);
//        }

//        /// <summary>
//        /// Возвращает приёмник данных.
//        /// </summary>
//        internal Receiver Receiver { get; private set; }

//        /// <summary>
//        /// Возвращает коллекцию модулей.
//        /// </summary>
//        public ModuleCollection Modules { get; }

//        /// <summary>
//        /// Возвращает коллекцию групп сигналов.
//        /// </summary>
//        public SectionGroupCollection Groups { get; }

//        /// <summary>
//        /// Возвращает объект, который выполняет обработку.
//        /// </summary>
//        public Processing Processing { get; }

//        /// <summary>
//        /// Разрушает объект.
//        /// </summary>
//        ~Equipment()
//        {
//            Stop();
//        }

//        /// <summary>
//        /// Выполняет определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов.
//        /// </summary>
//        public void Dispose()
//        {
//            Stop();
//            GC.SuppressFinalize(this);
//        }

//        /// <summary>
//        /// Останавливает работу.
//        /// </summary>
//        private void Stop()
//        {
//            if (Receiver is object)
//            {
//                Processing.Stop();
//                _SignalBuilder.Stop();
//                Receiver.Stop();
//                Receiver = null;
//            }
//        }
//    }
//}
