//using Simargl.IO;

//namespace Simargl.Synergy.Core.Portions.Authorization;

///// <summary>
///// Представляет порцию именнованного входа.
///// </summary>
///// <param name="name">
///// Имя для входа.
///// </param>
//internal sealed class NamedEntryPortion(string name) :
//    Portion(PortionFormat.NamedEntry)
//{
//    /// <summary>
//    /// Возвращает имя для входа.
//    /// </summary>
//    public string Name { get; } = name;

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
//        //  Сохранение имени.
//        await spreader.WriteStringAsync(Name, cancellationToken);
//    }

//    /// <summary>
//    /// Асинхронно загружает данные из потока.
//    /// </summary>
//    /// <param name="spreader">
//    /// Распределитель потока.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, загружающая данные из потока.
//    /// </returns>
//    public static async Task<NamedEntryPortion> LoadAsync(Spreader spreader, CancellationToken cancellationToken)
//    {
//        //  Чтение имени.
//        string name = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

//        //  Возврат порции.
//        return new(name);
//    }
//}
