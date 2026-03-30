using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using System.Runtime.CompilerServices;

namespace Apeiron.Oriole.Server.Processing;

/// <summary>
/// Предоставляет методы для обработки пакетов данных.
/// </summary>
public static class PackagesProcessing
{
    /// <summary>
    /// Выполняет проверку последовательности пакетов.
    /// </summary>
    /// <param name="previousPackage">
    /// Предыдущий пакет.
    /// </param>
    /// <param name="nextPackage">
    /// Следующий пакет.
    /// </param>
    /// <param name="declaredSampling">
    /// Заявленная частота дискретизации.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если пакеты принадлежат одной последовательности;
    /// значение <c>false</c> - в противном случае.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPackagesChain(
        [ParameterNoChecks] Package previousPackage,
        [ParameterNoChecks] Package nextPackage,
        [ParameterNoChecks] double declaredSampling)
    {
        //  Постоянная, определяющая нижнее отклонение.
        const double lowerDeviation = 0.7;

        //  Постоянная, определяющая верхнее отклонение.
        const double upperDeviation = 1.3;

        //  Фактическая длительность в милисекундах.
        double actualDuration = (nextPackage.Synchromarker - previousPackage.Synchromarker) / TimeSpan.TicksPerMillisecond;

        //  Заявленная длительность в милисекундах.
        double declaredDuration = previousPackage.Length * 1000 / declaredSampling;

        //  Проверка последовательности по длительности.
        return lowerDeviation * declaredDuration < actualDuration &&
            actualDuration < upperDeviation * declaredDuration;
    }
}
