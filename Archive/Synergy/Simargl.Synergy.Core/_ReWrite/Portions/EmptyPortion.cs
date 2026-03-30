//using Simargl.IO;

//namespace Simargl.Synergy.Core.Portions;

///// <summary>
///// Представляет пустую порцию данных.
///// </summary>
//internal sealed class EmptyPortion :
//    Portion
//{
//    /// <summary>
//    /// Инициализирует новый экземпляр.
//    /// </summary>
//    private EmptyPortion() :
//        base(PortionFormat.Empty)
//    {

//    }

//    /// <summary>
//    /// Возвращает уникальный экземпляр порции данных.
//    /// </summary>
//    public static EmptyPortion Unique { get; } = new();

//    /// <summary>
//    /// Асинхронно сохраняет данные в поток.
//    /// </summary>
//    /// <param name="spreader">
//    /// Распределитель потока.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, сохранающая данные в поток.
//    /// </returns>
//    protected override async Task SaveAsync(Spreader spreader, CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);
//    }
//}
