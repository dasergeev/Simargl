using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Processing
{
    /// <summary>
    /// Представляет исполнителя.
    /// </summary>
    public abstract class Performer
    {
        /// <summary>
        /// Поле для хранения объекта, используемого для синхронизации доступа.
        /// </summary>
        private readonly object _SyncRoot;

        /// <summary>
        /// Поле для хранения количества действий.
        /// </summary>
        private long _Count;

        /// <summary>
        /// Поле для хранения текущего индекса.
        /// </summary>
        private long _Index;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public Performer()
        {
            //  Инициализация полей класса.
            _SyncRoot = new object();
            _Count = 0;
            _Index = 0;
        }

        /// <summary>
        /// Возвращает текущий прогресс в процентах.
        /// </summary>
        /// <returns>
        /// Текущий прогресс в процентах.
        /// </returns>
        public double GetProgress()
        {
            //  Получение количества элементов.
            var count = Interlocked.Read(ref _Count);

            //  Проверка кличества элементов.
            if (count == 0)
            {
                return 100;
            }

            //  Получение текущего индекса.
            var index = Interlocked.Read(ref _Index);

            //  Расчёт прогресса.
            var progress = 100.0 * index / count;

            //  Проверка значения.
            if (progress < 0)
            {
                progress = 0;
            }
            if (progress > 100)
            {
                progress = 100;
            }

            //  Возврат прогресса в процентах.
            return progress;
        }

        /// <summary>
        /// Трассирует указанное сообщение.
        /// </summary>
        /// <param name="message">
        /// Сообщение.
        /// </param>
        public void Trace(string message)
        {
            //  Расчёт прогресса.
            var progress = GetProgress();

            //  Вывод строки с сообщением.
            Console.WriteLine($"[{progress:000.0}%] {message}");
        }

        /// <summary>
        /// Выполняет построение действий.
        /// </summary>
        /// <returns>
        /// Коллекция действий.
        /// </returns>
        protected abstract IEnumerable<Action> BuildActions();

        /// <summary>
        /// Происходит при запуске обработки.
        /// </summary>
        protected virtual void OnStarted()
        {

        }

        /// <summary>
        /// Происходит при остановке обработки.
        /// </summary>
        protected virtual void OnStopped()
        {

        }

        /// <summary>
        /// Выполняет работу.
        /// </summary>
        public void Invoke() => Invoke(default);

        /// <summary>
        /// Выполняет работу.
        /// </summary>
        /// <param name="maxActions">
        /// Максимальное количество действий.
        /// </param>
        public void Invoke(int maxActions)
        {
            //  Запуск обработки.
            OnStarted();

            //  Блокировка критического объекта.
            lock (_SyncRoot)
            {
                //  Сброс индекса.
                Interlocked.Exchange(ref _Index, 0);

                //  Получение коллекции действий.
                var actions = BuildActions().ToArray();

                //  Отсечение действий.
                if (maxActions > 0 && actions.Length > maxActions)
                {
                    Array.Resize(ref actions, maxActions);
                }

                //  Определение количества действий.
                Interlocked.Exchange(ref _Count, actions.Length);

                //  Выполнение действий.
                Parallel.ForEach(
                    actions,
                    new ParallelOptions { MaxDegreeOfParallelism = Configuration.MaxDegreeOfParallelism },
                    (Action action) =>
                {
                    //  Изменение индекса.
                    Interlocked.Increment(ref _Index);

                    //  Выполнение действия.
                    action();
                });
            }

            //  Остановка обработки.
            OnStopped();
        }
    }
}
