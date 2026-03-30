using System.Globalization;
using System.Windows.Controls;

namespace Simargl.Embedded.Tunings.TuningsEditor.UI.Controls;

/// <summary>
/// 
/// </summary>
public class NodeAttributeValidationRule : ValidationRule
{
    private readonly NodeAttributeEditor _editor;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="editor"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public NodeAttributeValidationRule(NodeAttributeEditor editor)
    {
        _editor = editor ?? throw new ArgumentNullException(nameof(editor));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string str = value as string ?? string.Empty;
        if (_editor.NodeAttribute is null)
            return ValidationResult.ValidResult;

        bool ok = _editor.NodeAttribute.IsValid(str);
        return ok
            ? ValidationResult.ValidResult
            : new ValidationResult(false, "Недопустимое значение атрибута.");
    }
}
