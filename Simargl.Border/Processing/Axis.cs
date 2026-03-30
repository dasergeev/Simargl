namespace Simargl.Border.Processing;

/// <summary>
/// Представляет ось.
/// </summary>
public sealed class Axis
{
    /// <summary>
    /// Возвращает индекс оси.
    /// </summary>
    public required int Index { get; init; }

    /// <summary>
    /// Возвращает коллекцию взаимодействий оси.
    /// </summary>
    public AxisInteractionCollection Interactions { get; } = [];

    /// <summary>
    /// Выполняет построение.
    /// </summary>
    public void Build()
    {
        //  Получение списка взаимодействий.
        List<AxisInteraction> interactions = [.. Interactions
            .Where(x => x is not null)
            .Select(x => x!)];

        //  Проверка скорости взаимодействия.
        if (interactions.Count < 2) return;

        //  Определение скорости первого взаимодействия.
        interactions[0].Speed = getSpeed(interactions[0], interactions[1]);

        //  Перебор взаимодействий.
        for (int i = 1; i + 1 < interactions.Count; i++)
        {
            //  Определение скорости взаимодействия.
            interactions[i].Speed = 0.5 * (getSpeed(interactions[i - 1], interactions[i]) +
                getSpeed(interactions[i], interactions[i + 1]));
        }

        //  Определение скорости последнего взаимодействия.
        interactions[^1].Speed = getSpeed(interactions[^2], interactions[^1]);

        //  Определяет скорость.
        static double getSpeed(AxisInteraction first, AxisInteraction second)
        {
            //  Определение времени.
            double time = second.Time - first.Time;

            //  Определение расстояния.
            double distance = second.Position - first.Position;

            //  Возврат скорости.
            return distance / time;
        }
    }
}
