namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет схему.
/// </summary>
public abstract class Scheme
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="generalScheme">
    /// Общая схема.
    /// </param>
    internal Scheme([ParameterNoChecks] GeneralScheme generalScheme)
    {
        //  Проверка общей схемы.
        if (generalScheme is null)
        {
            //  Установка общей схемы.
            GeneralScheme = (GeneralScheme)this;
        }
        else
        {
            //  Установка общей схемы.
            GeneralScheme = generalScheme;
        }
    }

    /// <summary>
    /// Возвращает общую схему.
    /// </summary>
    internal GeneralScheme GeneralScheme { get; }
}
