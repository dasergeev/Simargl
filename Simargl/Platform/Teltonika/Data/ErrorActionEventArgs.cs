using System;

namespace Simargl.Platform.Teltonika;

/// <summary>
/// Представляет класс, предоставляющий событие ошибки сервиса, и позволяющий отметить необходимость действия.
/// </summary>
public sealed class ErrorActionEventArgs :
    EventArgs
{

    /// <summary>
    /// Константа выполнить не выполнять действия.
    /// </summary>
    public const int ConstNoAction = 0x00000000;


    /// <summary>
    /// Поле хранящее необходимые действия.
    /// </summary>
    private int _NeedAction = ConstNoAction;

    /// <summary>
    /// Возвращает или устанавливает действия (только включение).
    /// </summary>
    public int NeedAction
    {
        get
        { 
            //  Возврат значения.
            return _NeedAction; 
        }
        set
        {
            //  Установка значения.
            _NeedAction |= value;
        }
    }
}
