//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет измерительный сигнал.
//    /// </summary>
//    public abstract class SignalOld : Ancestor, INotifyPropertyChanged
//    {
//        /// <summary>
//        /// Происходит при изменении свойства.
//        /// </summary>
//        public event PropertyChangedEventHandler PropertyChanged;

//        /// <summary>
//        /// Происходит при изменении свойства <see cref="Name"/>.
//        /// </summary>
//        public event EventHandler NameChanged;

//        /// <summary>
//        /// Происходит при изменении свойства <see cref="Value"/>.
//        /// </summary>
//        public event EventHandler ValueChanged;

//        /// <summary>
//        /// Происходит при изменении свойства <see cref="Sampling"/>.
//        /// </summary>
//        public event EventHandler SamplingChanged;

//        /// <summary>
//        /// Происходит при изменении свойства <see cref="IsActive"/>.
//        /// </summary>
//        public event EventHandler IsActiveChanged;

//        /// <summary>
//        /// Происходит при поступлении новых данных от устройства.
//        /// </summary>
//        public event EventHandler<ResponseEventArgsOld> Response;

//        /// <summary>
//        /// Поле для хранения имени сигнала.
//        /// </summary>
//        private string _Name;

//        /// <summary>
//        /// Поле для хранения частоты дискретизации.
//        /// </summary>
//        private double _Sampling;

//        /// <summary>
//        /// Поле для хранения значения, определяющего активен ли сигнал.
//        /// </summary>
//        private bool _IsActive;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="name">
//        /// Имя сигнала.
//        /// </param>
//        /// <param name="sampling">
//        /// Частота дискретизации.
//        /// </param>
//        public SignalOld(string name, double sampling)
//        {
//            _Name = name;
//            _Sampling = sampling;
//            _IsActive = true;
//        }

//        /// <summary>
//        /// Возвращает или задаёт имя сигнала.
//        /// </summary>
//        public string Name
//        {
//            get
//            {
//                return _Name;
//            }
//            set
//            {
//                if (_Name != value)
//                {
//                    CoreSetName(value);
//                    _Name = value;
//                    OnNameChanged(EventArgs.Empty);
//                }
//            }
//        }

//        /// <summary>
//        /// Возвращает или задаёт частоту дискретизации.
//        /// </summary>
//        public double Sampling
//        {
//            get
//            {
//                return _Sampling;
//            }
//            set
//            {
//                if (_Sampling != value)
//                {
//                    CoreSetSampling(value);
//                    _Sampling = value;
//                    OnSamplingChanged(EventArgs.Empty);
//                }
//            }
//        }

//        /// <summary>
//        /// Возвращает текущее значение сигнала.
//        /// </summary>
//        public double Value
//        {
//            get
//            {
//                return CoreGetValue();
//            }
//        }

//        /// <summary>
//        /// Возвращает или задаёт значение, определяющее является ли сигнал активным.
//        /// </summary>
//        public bool IsActive
//        {
//            get
//            {
//                return _IsActive;
//            }
//            set
//            {
//                if (_IsActive != value)
//                {
//                    _IsActive = value;
//                    OnIsActiveChanged(EventArgs.Empty);
//                }
//            }
//        }

//        /// <summary>
//        /// Выполняет балансировку нуля.
//        /// </summary>
//        /// <param name="values">
//        /// Количество измерений, используемое при балансировке.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="values"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public void ZeroBalance(int values)
//        {
//            if (values < 1)
//            {
//                throw new ArgumentOutOfRangeException("values", "Передано отрицательное или равное нулю значение.");
//            }
//            CoreZeroBalance(values);
//        }

//        /// <summary>
//        /// Уставнавливает новое имя сигнала.
//        /// </summary>
//        /// <param name="name">
//        /// Новое имя сигнала.
//        /// </param>
//        protected abstract void CoreSetName(string name);

//        /// <summary>
//        /// Уставнавливает частоту дискретизации.
//        /// </summary>
//        /// <param name="sampling">
//        /// Частота дискретизации.
//        /// </param>
//        protected abstract void CoreSetSampling(double sampling);

//        /// <summary>
//        /// Возвращает текущее значение.
//        /// </summary>
//        /// <returns>
//        /// Текущее значение.
//        /// </returns>
//        protected abstract double CoreGetValue();

//        /// <summary>
//        /// Выполняет балансировку нуля.
//        /// </summary>
//        /// <param name="values">
//        /// Количество измерений, используемое при балансировке.
//        /// </param>
//        protected abstract void CoreZeroBalance(int values);

//        /// <summary>
//        /// Вызывает событие <see cref="PropertyChanged"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
//        {
//            PropertyChanged?.Invoke(this, e);
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="NameChanged"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnNameChanged(EventArgs e)
//        {
//            NameChanged?.Invoke(this, e);
//            OnPropertyChanged(new PropertyChangedEventArgs("Name"));
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="ValueChanged"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnValueChanged(EventArgs e)
//        {
//            ValueChanged?.Invoke(this, e);
//            OnPropertyChanged(new PropertyChangedEventArgs("Value"));
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="SamplingChanged"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnSamplingChanged(EventArgs e)
//        {
//            SamplingChanged?.Invoke(this, e);
//            OnPropertyChanged(new PropertyChangedEventArgs("Sampling"));
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="IsActiveChanged"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnIsActiveChanged(EventArgs e)
//        {
//            IsActiveChanged?.Invoke(this, e);
//            OnPropertyChanged(new PropertyChangedEventArgs("IsActive"));
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
//    }
//}
