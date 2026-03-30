//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет ссылку на сигнал.
//    /// </summary>
//    internal class SignalLinkOld : Ancestor
//    {
//        /// <summary>
//        /// Происходит при поступлении новых данных от устройства.
//        /// </summary>
//        public event EventHandler<ResponseEventArgsOld> Response;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="device">
//        /// Устройство, которому принадлежит сигнал.
//        /// </param>
//        /// <param name="signalIndex">
//        /// Индекс сигнала, с которым необходимо установить связь.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="device"/> передана пустая ссылка.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="signalIndex"/> передано отрицательное значение.
//        /// </exception>
//        public SignalLinkOld(DeviceOld device, int signalIndex)
//        {
//            Device = device ?? throw new ArgumentNullException("device", "Передана пустая ссылка.");
//            SignalIndex = signalIndex >= 0 ? signalIndex : throw new ArgumentOutOfRangeException("signalIndex", "Передано отрицательное значение.");
//            Device.IsConnectedChanged += (object sender, EventArgs e) => Update();
//            Update();
//        }

//        /// <summary>
//        /// Возвращает устройство, которому принадлежит сигнал.
//        /// </summary>
//        public DeviceOld Device { get; }

//        /// <summary>
//        /// Возвращает индекс сигнала, с которым необходимо установить связь.
//        /// </summary>
//        public int SignalIndex { get; }

//        /// <summary>
//        /// Возвращает сигнал.
//        /// </summary>
//        public SignalOld Signal { get; private set; }

//        /// <summary>
//        /// Выполняет операцию проверки на равенство.
//        /// </summary>
//        /// <param name="left">
//        /// Левый операнд.
//        /// </param>
//        /// <param name="right">
//        /// Правый операнд.
//        /// </param>
//        /// <returns>
//        /// Результат операции.
//        /// </returns>
//        public static bool operator == (SignalLinkOld left, SignalLinkOld right)
//        {
//            if (left is null || right is null)
//            {
//                return false;
//            }
//            return ReferenceEquals(left.Device, right.Device) && left.SignalIndex == right.SignalIndex;
//        }

//        /// <summary>
//        /// Выполняет операцию проверки на неравенство.
//        /// </summary>
//        /// <param name="left">
//        /// Левый операнд.
//        /// </param>
//        /// <param name="right">
//        /// Правый операнд.
//        /// </param>
//        /// <returns>
//        /// Результат операции.
//        /// </returns>
//        public static bool operator !=(SignalLinkOld left, SignalLinkOld right)
//        {
//            return !(left == right);
//        }

//        /// <summary>
//        /// Обновляет ссылку.
//        /// </summary>
//        private void Update()
//        {
//            SignalOld signal = null;
//            try
//            {
//                signal = Device.Signals[SignalIndex];
//            }
//            catch (ArgumentOutOfRangeException)
//            {

//            }
//            if (signal is null)
//            {
//                if (Signal is object)
//                {
//                    Signal.Response -= Signal_Response;
//                    Signal = null;
//                }
//            }
//            else
//            {
//                if (Signal is null)
//                {
//                    Signal = signal;
//                    Signal.Response += Signal_Response;
//                }
//                else
//                {
//                    if (!ReferenceEquals(Signal, signal))
//                    {
//                        Signal.Response -= Signal_Response;
//                        Signal = signal;
//                        Signal.Response += Signal_Response;
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="Response"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnResponse(ResponseEventArgsOld e)
//        {
//            Response?.Invoke(this, e);
//        }

//        /// <summary>
//        /// Обрабатывает событие поступления новых данных от устройства.
//        /// </summary>
//        /// <param name="sender">
//        /// Объект, создавший событие.
//        /// </param>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        private void Signal_Response(object sender, ResponseEventArgsOld e)
//        {
//            OnResponse(e);
//        }

//        /// <summary>
//        /// Определяет, равен ли заданный объект текущему объекту.
//        /// </summary>
//        /// <param name="obj">
//        /// Объект, который требуется сравнить с текущим объектом.
//        /// </param>
//        /// <returns>
//        /// Результат проверки.
//        /// </returns>
//        public override bool Equals(object obj)
//        {
//            if (obj is SignalLinkOld link)
//            {
//                return this == link;
//            }
//            return false;
//        }

//        /// <summary>
//        /// Служит хэш-функцией по умолчанию.
//        /// </summary>
//        /// <returns>
//        /// Хэш-код для текущего объекта.
//        /// </returns>
//        public override int GetHashCode()
//        {
//            return SignalIndex;
//        }
//    }
//}
