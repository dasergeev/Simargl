using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RailTest
{
    partial class OutputView
    {
        /// <summary>
        /// Представляет средство вывода текстовой информации.
        /// </summary>
        internal class OutputRelease : Output
        {
            /// <summary>
            /// Поле для хранения элемента управления, отображающего текстовую информацию.
            /// </summary>
            private readonly Controls.OutputView _View;

            /// <summary>
            /// Поле для хранения текущего уровня табуляции.
            /// </summary>
            private int _TabLevel;

            /// <summary>
            /// Поле для хранения символов табуляции для текущего уровня табуляции.
            /// </summary>
            private string _Tabs;

            /// <summary>
            /// Поле для хранения значения, определяющего находится ли текущее место вывода на новой строке.
            /// </summary>
            private bool _IsBeginLine;

            /// <summary>
            /// Инициализирует новый экземпляр класса.
            /// </summary>
            /// <param name="view">
            /// Элемент управления, отображающий текстовую информацию.
            /// </param>
            internal OutputRelease(Controls.OutputView view)
            {
                _View = view;
                _TabLevel = 0;
                _Tabs = "";
                _IsBeginLine = true;
            }

            /// <summary>
            /// Сбрасывает уровень табуляции.
            /// </summary>
            public override void TabLevelReset()
            {
                lock (SyncRoot)
                {
                    _TabLevel = 0;
                    _Tabs = "";
                }
            }

            /// <summary>
            /// Поднимает уровень табуляции.
            /// </summary>
            public override void TabLevelUp()
            {
                lock (SyncRoot)
                {
                    ++_TabLevel;
                    _Tabs = new string('\t', _TabLevel);
                }
            }

            /// <summary>
            /// Опускает уровень табуляции.
            /// </summary>
            public override void TabLevelDown()
            {
                lock (SyncRoot)
                {
                    --_TabLevel;
                    if (_TabLevel < 0)
                    {
                        _TabLevel = 0;
                    }
                    if (_TabLevel == 0)
                    {
                        _Tabs = "";
                    }
                    else
                    {
                        _Tabs = new string('\t', _TabLevel);
                    }
                }
            }

            /// <summary>
            /// Выполняет очистку средства вывода текстовой информации.
            /// </summary>
            public override void Clear()
            {
                _View.Clear();
            }

            /// <summary>
            /// Осуществляет переход на новую строку.
            /// </summary>
            public override void WriteLine()
            {
                lock (SyncRoot)
                {
                    _View.AppendText("\r\n");
                    _IsBeginLine = true;
                }
            }

            /// <summary>
            /// Производит выполнение всех команд.
            /// </summary>
            public override void Flush()
            {
                _View.ScrollToCaret();
            }

            /// <summary>
            /// Выводит текстовую строку.
            /// </summary>
            /// <param name="value">
            /// Текстовая строка.
            /// </param>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength")]
            public override void Write(string value)
            {
                if (value != null && value != "")
                {
                    if (!value.Contains('\r') && !value.Contains('\n'))
                    {
                        lock (SyncRoot)
                        {
                            if (_IsBeginLine)
                            {
                                _View.AppendText(_Tabs);
                                _IsBeginLine = false;
                            }
                            _View.AppendText(value);
                        }
                    }
                    else
                    {
                        List<string> parts = new List<string>();
                        foreach (string first in value.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                        {
                            foreach (string second in first.Split(new string[] { "\r" }, StringSplitOptions.None))
                            {
                                parts.AddRange(second.Split(new string[] { "\n" }, StringSplitOptions.None));
                            }
                        }
                        lock (SyncRoot)
                        {
                            for (int i = 0; i != parts.Count - 1; ++i)
                            {
                                WriteLine(parts[i]);
                            }
                            Write(parts[parts.Count - 1]);
                        }
                    }
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
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1820:TestForEmptyStringsUsingStringLength")]
            public override void Write(Color color, string value)
            {
                if (value != null && value != "")
                {
                    if (!value.Contains('\r') && !value.Contains('\n'))
                    {
                        lock (SyncRoot)
                        {
                            if (_IsBeginLine)
                            {
                                _View.AppendText(_Tabs);
                                _IsBeginLine = false;
                            }
                            _View.AppendText(value, color);
                        }
                    }
                    else
                    {
                        List<string> parts = new List<string>();
                        foreach (string first in value.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                        {
                            foreach (string second in first.Split(new string[] { "\r" }, StringSplitOptions.None))
                            {
                                parts.AddRange(second.Split(new string[] { "\n" }, StringSplitOptions.None));
                            }
                        }
                        lock (SyncRoot)
                        {
                            for (int i = 0; i != parts.Count - 2; ++i)
                            {
                                WriteLine(color, parts[i]);
                            }
                            Write(color, parts[parts.Count - 1]);
                        }
                    }
                }
            }
        }
    }
}