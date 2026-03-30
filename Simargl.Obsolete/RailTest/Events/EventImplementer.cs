using System;
using System.Collections.Generic;

namespace RailTest.Events;

/// <summary>
/// Представляет потокобезопасного организатора событий.
/// </summary>
public class EventImplementer
{
    /// <summary>
    /// Представляет метод, который вызывает обработчик события.
    /// </summary>
    /// <param name="handler">
    /// Обработчик события.
    /// </param>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
    public delegate void InvokeHandler(EventHandler handler, object? sender, EventArgs e);

    /// <summary>
    /// Поле для хранения списка обработчиков событий.
    /// </summary>
    private readonly List<EventHandler> _Handlers;

    /// <summary>
    /// Поле для хранения метода, который вызывает обработчик события.
    /// </summary>
    private readonly InvokeHandler _InvokeHandler;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="invokeHandler">
    /// Метод, который вызывает обработчик события.
    /// </param>
    public EventImplementer(InvokeHandler invokeHandler)
    {
        _Handlers = new List<EventHandler>();
        _InvokeHandler = invokeHandler is not null ? invokeHandler : (EventHandler handler, object? sender, EventArgs e) =>
        {
            handler(sender, e);
        };
    }

    /// <summary>
    /// Добавляет обработчик события.
    /// </summary>
    /// <param name="handler">
    /// Делегат, который выполняет обработку события.
    /// </param>
    public void AddHandler(EventHandler? handler)
    {
        if (handler is not null)
        {
            lock (_Handlers)
            {
                if (!_Handlers.Contains(handler))
                {
                    _Handlers.Add(handler);
                }
            }
        }
    }

    /// <summary>
    /// Удаляет обработчик события.
    /// </summary>
    /// <param name="handler">
    /// Делегат, который выполняет обработку события.
    /// </param>
    public void RemoveHandler(EventHandler? handler)
    {
        if (handler is not null)
        {
            lock (_Handlers)
            {
                if (_Handlers.Contains(handler))
                {
                    _Handlers.Remove(handler);
                }
            }
        }
    }

    /// <summary>
    /// Вызывает событие.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    public void RaiseEvent(object? sender, EventArgs e)
    {
        lock (_Handlers)
        {
            foreach (EventHandler handler in _Handlers)
            {
                _InvokeHandler(handler, sender, e);
            }
        }
    }
}
