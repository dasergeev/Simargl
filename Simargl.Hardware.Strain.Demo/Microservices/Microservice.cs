using Simargl.Hardware.Strain.Demo.Core;
using Simargl.Hardware.Strain.Demo.Journaling;

namespace Simargl.Hardware.Strain.Demo.Microservices;

/// <summary>
/// Представляет микросервис.
/// </summary>
public abstract class Microservice
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="heart">
    /// Сердце приложения.
    /// </param>
    public Microservice(Heart heart)
    {
        //  Установка сердца приложения.
        Heart = heart;
    }

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    protected Heart Heart { get; }

    /// <summary>
    /// Возвращает приложение.
    /// </summary>
    protected App Application => Heart.Application;

    /// <summary>
    /// Возвращает журнал.
    /// </summary>
    protected Journal Journal => Application.Journal;

    /// <summary>
    /// Возвращает средство вызова методов в основном потоке.
    /// </summary>
    protected Invoker Invoker => Application.Invoker;

    /// <summary>
    /// Возвращает механизм поддержки.
    /// </summary>
    protected Keeper Keeper => Application.Keeper;
}
