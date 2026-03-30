using RailTest.Algebra.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Drawing.OpenGL
{
    /// <summary>
    /// Базовый класс для всех объектов, реализующих область рендеринга.
    /// </summary>
    public abstract class RenderingRegion : Ancestor
    {
        /// <summary>
        /// Происходит при воспроизведении сцены.
        /// </summary>
        public event EventHandler Render;

        /// <summary>
        /// Вызывает событие <see cref="Render"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        internal protected virtual void OnRender(EventArgs e)
        {
            Render?.Invoke(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public abstract Vector2 GetScreenPoint(Vector3 source);

        ////
        //// Сводка:
        ////     Происходит, когда фокус ввода покидает элемент управления.
        //public event EventHandler Leave;

        ////
        //// Сводка:
        ////     Происходит при входе в элемент управления.
        //public event EventHandler Enter;

        ////
        //// Сводка:
        ////     Происходит, когда отпускается клавиша, если элемент управления имеет фокус.
        //public event KeyEventHandler KeyUp;

        ////
        //// Сводка:
        ////     Происходит при нажатии клавиши с буквой, пробела или клавиши BACKSPACE, если
        ////     фокус находится в элементе управления.
        //public event KeyPressEventHandler KeyPress;

        ////
        //// Сводка:
        ////     Происходит при нажатии клавиши, если элемент управления имеет фокус.
        //public event KeyEventHandler KeyDown;

        ////
        //// Сводка:
        ////     Вызывается при получении фокуса элементом управления.
        //public event EventHandler GotFocus;

        ////
        //// Сводка:
        ////     Происходит при двойном щелчке элемента управления.
        //public event EventHandler DoubleClick;

        ////
        //// Сводка:
        ////     Происходит, когда для отображения элемента управления требуется перерисовка.
        //public event InvalidateEventHandler Invalidated;

        ////
        //// Сводка:
        ////     Вызывается при щелчке мышью элемента управления.
        //public event MouseEventHandler MouseClick;

        ////
        //// Сводка:
        ////     Вызывается при двойном щелчке мышью элемента управления.
        //public event MouseEventHandler MouseDoubleClick;

        ////
        //// Сводка:
        ////     Происходит при потере захвата мыши элементом управления.
        //public event EventHandler MouseCaptureChanged;

        ////
        //// Сводка:
        ////     Происходит при нажатии кнопки мыши, если указатель мыши находится на элементе
        ////     управления.
        //public event MouseEventHandler MouseDown;

        ////
        //// Сводка:
        ////     Происходит, когда указатель мыши оказывается на элементе управления.
        //public event EventHandler MouseEnter;

        ////
        //// Сводка:
        ////     Происходит, когда указатель мыши покидает элемент управления.
        //public event EventHandler MouseLeave;

        ////
        //// Сводка:
        ////     Возникает, когда настройка DPI для элемента управления изменяется программным
        ////     образом, прежде чем возникает событие изменения DPI для соответствующего родительского
        ////     элемента управления или формы.
        //public event EventHandler DpiChangedBeforeParent;

        ////
        //// Сводка:
        ////     Происходит при перемещении указателя мыши по элементу управления.
        //public event MouseEventHandler MouseMove;

        ////
        //// Сводка:
        ////     Происходит при отпускании кнопки мыши, когда указатель мыши находится на элементе
        ////     управления.
        //public event MouseEventHandler MouseUp;

        ////
        //// Сводка:
        ////     Происходит при прокручивании колеса мыши, если данный элемент управления находится
        ////     в фокусе.
        //public event MouseEventHandler MouseWheel;

        ////
        //// Сводка:
        ////     Происходит при перемещении элемента управления.
        //public event EventHandler Move;

        ////
        //// Сводка:
        ////     Генерируется перед событием System.Windows.Forms.Control.KeyDown при нажатии
        ////     клавиши, когда элемент управления имеет фокус.
        //public event PreviewKeyDownEventHandler PreviewKeyDown;

        ////
        //// Сводка:
        ////     Происходит при изменении размеров элемента управления.
        //public event EventHandler Resize;

        ////
        //// Сводка:
        ////     Происходит в процессе удаления дескриптора элемента управления.
        //public event EventHandler HandleDestroyed;

        ////
        //// Сводка:
        ////     Происходит при создании дескриптора для элемента управления.
        //public event EventHandler HandleCreated;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.BackColor.
        //public event EventHandler BackColorChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.ClientSize.
        //public event EventHandler ClientSizeChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.ContextMenu.
        //public event EventHandler ContextMenuChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.ContextMenuStrip.
        //public event EventHandler ContextMenuStripChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.Cursor.
        //public event EventHandler CursorChanged;

        ////
        //// Сводка:
        ////     Происходит, если значение свойства System.Windows.Forms.Control.Enabled было
        ////     изменено.
        //public event EventHandler EnabledChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.Font.
        //public event EventHandler FontChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.ForeColor.
        //public event EventHandler ForeColorChanged;

        ////
        //// Сводка:
        ////     Происходит, если значение свойства System.Windows.Forms.Control.Location было
        ////     изменено.
        //public event EventHandler LocationChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.Region.
        //public event EventHandler RegionChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.Size.
        //public event EventHandler SizeChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.TabIndex.
        //public event EventHandler TabIndexChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.TabStop.
        //public event EventHandler TabStopChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.Text.
        //public event EventHandler TextChanged;

        ////
        //// Сводка:
        ////     Происходит при изменении значения свойства System.Windows.Forms.Control.Visible.
        //public event EventHandler VisibleChanged;

        ////
        //// Сводка:
        ////     Происходит при щелчке элемента управления.
        //public event EventHandler Click;

        ////
        //// Сводка:
        ////     Возникает, когда настройка DPI для элемента управления изменяется программным
        ////     образом после изменения DPI связанного родительского элемента управления или
        ////     формы.
        //public event EventHandler DpiChangedAfterParent;

        ////
        //// Сводка:
        ////     Происходит, когда указатель мыши задерживается на элементе управления.
        //public event EventHandler MouseHover;

    }
}
