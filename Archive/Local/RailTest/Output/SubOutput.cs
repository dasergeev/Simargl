using System;
using System.Collections.Concurrent;
using System.Drawing;

namespace RailTest
{
    partial class Output
    {
        /// <summary>
        /// Представляет дочернее средство вывода текстовой информации.
        /// </summary>
        internal class SubOutput : Output
        {
            /// <summary>
            /// Поле для хранения родительского средства вывода текстовой информации.
            /// </summary>
            private Output _Parent;

            /// <summary>
            /// Поле для хранения очереди действий.
            /// </summary>
            private readonly ConcurrentQueue<Action> _Actions;

            /// <summary>
            /// Инициализирует новый экземпляр класса.
            /// </summary>
            /// <param name="parent">
            /// Родительское средство вывода текстовой информации.
            /// </param>
            public SubOutput(Output parent)
            {
                _Parent = parent;
                _Actions = new ConcurrentQueue<Action>();
            }

            /// <summary>
            /// Сбрасывает уровень табуляции.
            /// </summary>
            public override void TabLevelReset()
            {
                lock (SyncRoot)
                {
                    _Actions.Enqueue(() => _Parent.TabLevelReset());
                }
            }

            /// <summary>
            /// Поднимает уровень табуляции.
            /// </summary>
            public override void TabLevelUp()
            {
                lock (SyncRoot)
                {
                    _Actions.Enqueue(() => _Parent.TabLevelUp());
                }
            }

            /// <summary>
            /// Опускает уровень табуляции.
            /// </summary>
            public override void TabLevelDown()
            {
                lock (SyncRoot)
                {
                    _Actions.Enqueue(() => _Parent.TabLevelDown());
                }
            }

            /// <summary>
            /// Выполняет очистку средства вывода текстовой информации.
            /// </summary>
            public override void Clear()
            {
                lock (SyncRoot)
                {
                    _Actions.Enqueue(() => _Parent.Clear());
                }
            }

            /// <summary>
            /// Производит выполнение всех команд.
            /// </summary>
            public override void Flush()
            {
                lock (SyncRoot)
                {
                    lock (_Parent.SyncRoot)
                    {
                        Action action = null;
                        while (_Actions.TryDequeue(out action))
                        {
                            action();
                        }
                    }
                }
            }

            /// <summary>
            /// Осуществляет переход на новую строку.
            /// </summary>
            public override void WriteLine()
            {
                lock (SyncRoot)
                {
                    _Actions.Enqueue(() => _Parent.WriteLine());
                }
            }

            /// <summary>
            /// Выводит текстовую строку.
            /// </summary>
            /// <param name="value">
            /// Текстовая строка.
            /// </param>
            public override void Write(string value)
            {
                lock (SyncRoot)
                {
                    _Actions.Enqueue(() => _Parent.Write(value));
                }
            }

            /// <summary>
            /// Выводит текстовую строку.
            /// </summary>
            /// <param name="color">
            /// Цвет.
            /// </param>
            /// <param name="value">
            /// Текстовая строка.
            /// </param>
            public override void Write(Color color, string value)
            {
                lock (SyncRoot)
                {
                    _Actions.Enqueue(() => _Parent.Write(color, value));
                }
            }
        }
    }
}