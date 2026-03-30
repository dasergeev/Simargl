using Microsoft.CodeAnalysis;
using System.Text;

namespace Pulse.CodeGenerator;

/// <summary>
/// 
/// </summary>
[Generator]
[CLSCompliant(false)]
public class HelloGenerator : IIncrementalGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Генерация выполняется один раз, просто добавляем исходник
        context.RegisterPostInitializationOutput(static ctx =>
        {
            var source = new StringBuilder();
            source.AppendLine("namespace Generated;");
            source.AppendLine("/// <summary>Класс, сгенерированный HelloGenerator.</summary>");
            source.AppendLine("public static class Hello");
            source.AppendLine("{");
            source.AppendLine("    /// <summary>Сообщение от Source Generator.</summary>");
            source.AppendLine("    public static string Message => \"Hello from Incremental Source Generator!\";");
            source.AppendLine("}");
            ctx.AddSource("HelloGenerator.g.cs", source.ToString());
        });
    }
}
