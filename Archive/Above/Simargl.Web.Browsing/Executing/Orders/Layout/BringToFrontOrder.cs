//using Ably.Web.Browsing.Core;

//namespace Ably.Web.Browsing.Executing.Orders.Layout;

///// <summary>
///// Представляет предписание перемещающая элемент в начало z-порядка.
///// </summary>
//public sealed class BringToFrontOrder :
//    WebPageAction
//{
//    /// <summary>
//    /// Асинхронно выполняет предписание.
//    /// </summary>
//    /// <param name="controller">
//    /// Контроллер веб-страницы.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая предписание.
//    /// </returns>
//    internal override async Task InvokeAsync(WebPageController controller, CancellationToken cancellationToken)
//    {
//        //  Выполнение предписания.
//        await controller.WebShell.BringToFrontAsync(cancellationToken).ConfigureAwait(false);
//    }
//}
