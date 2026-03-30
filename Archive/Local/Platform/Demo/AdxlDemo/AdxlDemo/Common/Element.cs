using Apeiron.Platform.Demo.AdxlDemo.Database;
using Apeiron.Platform.Demo.AdxlDemo.Logging;
using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo;

/// <summary>
/// Представляет элемент, связанный с основным активным объектом.
/// </summary>
public abstract class Element
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public Element(Engine engine)
    {
        //  Установка основного активного объекта.
        Engine = IsNotNull(engine, nameof(engine));
    }

    /// <summary>
    /// Возвращает основной активный объект.
    /// </summary>
    [Browsable(false)]
    public Engine Engine { get; }

    /// <summary>
    /// Возвращает текущее приложение.
    /// </summary>
    [Browsable(false)]
    public App Application => Engine.Application;

    /// <summary>
    /// Возвращает общие настройки приложения.
    /// </summary>
    [Browsable(false)]
    public Settings Settings => Application.Settings;

    /// <summary>
    /// Возвращает средство вызова методов.
    /// </summary>
    [Browsable(false)]
    public Invoker Invoker => Engine.Invoker;

    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    [Browsable(false)]
    public Logger Logger => Engine.Logger;

    /// <summary>
    /// Возвращает управляющего контекстом базы данных.
    /// </summary>
    [Browsable(false)]
    public ContextManager ContextManager => Engine.ContextManager;
}
