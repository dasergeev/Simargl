using Simargl.Engine;
using Simargl.Web.Browsing.Core.Controls;
using System.Collections;
using System.Collections.Generic;

namespace Simargl.Web.Browsing;

/// <summary>
/// Представляет коллекцию веб-страниц.
/// </summary>
public sealed class WebPageCollection :
    Something,
    IEnumerable<WebPage>
{
    /// <summary>
    /// Поле для хранения управляющего веб-содержимым.
    /// </summary>
    private readonly WebManager _Manager;

    /// <summary>
    /// Поле для хранения списка элементов.
    /// </summary>
    private volatile List<WebPage> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="manager">
    /// Управляющий веб-содержимым.
    /// </param>
    internal WebPageCollection(WebManager manager)
    {
        //  Установка управляющего веб-содержимым.
        _Manager = manager;

        //  Создание списка элементов.
        _Items = [];
    }

    /// <summary>
    /// Асинхронно создаёт новую страницу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, добавляющая создающая новую страницу.
    /// </returns>
    public async Task<WebPage> CreateAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Блокировка критического объекта.
        using (await _Manager.LayoutLock.LockAsync(cancellationToken).ConfigureAwait(false))
        {
            //  Выполнение в основном потоке.
            WebShell shell = await Entry.Invoker.InvokeAsync(delegate
            {
                //  Добавление оболочки.
                return _Manager.Container.AddShell();
            }, cancellationToken).ConfigureAwait(false);

            //  Создание новой страницы.
            WebPage page = new(_Manager, shell);

            //  Создание копии списка элементов.
            List<WebPage> items = [.. _Items];

            //  Добавление новой страницы.
            items.Add(page);

            //  Замена списка элементов.
            _Items = items;

            //  Возврат новой страницы.
            return page;
        }
    }

    /// <summary>
    /// Асинхронно удаляет страницу из коллекции.
    /// </summary>
    /// <param name="page">
    /// Веб-страница, которую необходимо удалить.
    /// </param>
    /// <param name="shell">
    /// Оболочка, которую необходимо удалить из коллекции.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, удаляющая страницу из коллекции.
    /// </returns>
    internal async Task RemoveAsync(WebPage page, WebShell shell, CancellationToken cancellationToken)
    {
        //  Проверка страницы.
        IsNotNull(shell);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Блокировка критического объекта.
        using (await _Manager.LayoutLock.LockAsync(cancellationToken).ConfigureAwait(false))
        {
            //  Выполнение в основном потоке.
            await Entry.Invoker.InvokeAsync(delegate
            {
                //  Удаление оболочки.
                _Manager.Container.RemoveShell(shell);
            }, cancellationToken).ConfigureAwait(false);

            //  Создание копии списка элементов.
            List<WebPage> items = [.. _Items];

            //  Удаление страницы.
            if (items.Remove(page))
            {
                //  Замена списка элементов.
                _Items = items;
            }
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<WebPage> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable<WebPage>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable)_Items).GetEnumerator();
    }
}
