using Simargl.Journaling;
using Simargl.Performing;

namespace Simargl.Engine;

/// <summary>
/// Представляет вход в приложение.
/// </summary>
public sealed class Entry :
    Performer
{
    /// <summary>
    /// Поле для хранения уникального экземпляра входа в приложение.
    /// </summary>
    private static volatile Entry _Unique = null!;

    /// <summary>
    /// Возвращает уникальный экземпляр входа в приложение.
    /// </summary>
    public static Entry Unique => _Unique;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="context">
    /// Контекст входа в приложение.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка повторной инициализации входа в приложение.
    /// </exception>
    public Entry(EntryContext context) :
        base(context.CancellationToken)
    {
        //  Установка уникального экземпляра входа в приложение.
        if (Interlocked.CompareExchange(ref _Unique, this, null) is not null)
        {
            //  Произошла попытка повторной инициализации входа в приложение.
            throw new InvalidOperationException("Произошла попытка повторной инициализации входа в приложение.");
        }

        //  Установка главного токена отмены.
        CancellationToken = GetCancellationToken();

        //  Установка журнала.
        Journal = Journal.Default;

        //  Создание механизма поддержки.
        Keeper = new(context.KeeperPeriod, CancellationToken);

        //  Создание средства вызова методов в основном потоке.
        Invoker = new(context.PrimarySender, CancellationToken);

        //  Вывод сообщения в журнал.
        Journal.Add("Окружение запущено.");
    }

    /// <summary>
    /// Возвращает главный токен отмены.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Возвращает журнал.
    /// </summary>
    public Journal Journal { get; }

    /// <summary>
    /// Возвращает механизм поддержки.
    /// </summary>
    public Keeper Keeper { get; }

    /// <summary>
    /// Возвращает средство вызова методов в основном потоке.
    /// </summary>
    public Invoker Invoker { get; }
}
