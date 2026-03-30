using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет пути к корневому каталогу.
/// </summary>
public class PathData :
    BasePathData
{
    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<PathData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => x.Key);
    }
}
