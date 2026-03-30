//namespace Apeiron.Platform.Expanse;

///// <summary>
///// Представляет контекст выполнения метода.
///// </summary>
//internal sealed class MethodContext
//{
//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    /// <param name="reader">
//    /// Средство для чтения параметров метода.
//    /// </param>
//    /// <param name="writer">
//    /// Средство для записи результатов метода.
//    /// </param>
//    public MethodContext(
//        [ParameterNoChecks] BinaryReader reader,
//        [ParameterNoChecks] BinaryWriter writer)
//    {
//        //  Установка средствадля чтения параметров метода.
//        Reader = reader;

//        //  Установка средства записи результатов метода.
//        Writer = writer;
//    }

//    /// <summary>
//    /// Возвращает средство для чтения параметров метода.
//    /// </summary>
//    public BinaryReader Reader { get; }

//    /// <summary>
//    /// Возвращает средство для записи результатов метода.
//    /// </summary>
//    public BinaryWriter Writer { get; }
//}
