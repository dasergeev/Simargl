using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Simargl.Hardware.Strain.Demo.UI;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public class TargetChart : Chart
{
    /// <summary>
    /// 
    /// </summary>
    public TargetChart()
    {
        this.SetStyle(ControlStyles.Selectable, false);
        this.TabStop = false;
    }

    /// <summary>
    /// 
    /// </summary>
    protected override bool ShowFocusCues => false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnEnter(EventArgs e)
    {
        // Предотвращаем фокус
        base.OnLeave(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnGotFocus(EventArgs e)
    {
        // Блокируем фокус
        base.OnLeave(e);
    }
}

