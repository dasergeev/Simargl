//using Simargl.Synergy.Core.Portions;

//namespace Simargl.Synergy.Core.Bonds;

//partial class Bond
//{
//    /// <summary>
//    /// Представляет данные о порции.
//    /// </summary>
//    /// <param name="portion">
//    /// Порция данных.
//    /// </param>
//    private sealed class PortionInfo(Portion portion)
//    {
//        /// <summary>
//        /// Поле для хранения порции.
//        /// </summary>
//        private Portion? _Portion = portion;

//        /// <summary>
//        /// Возвращает источник завершения задачи ожидания.
//        /// </summary>
//        public TaskCompletionSource CompletionSource { get; } = new();

//        /// <summary>
//        /// Выполняет попытку получить порцию.
//        /// </summary>
//        /// <returns>
//        /// Ссыла на текущее значение.
//        /// </returns>
//        public Portion? TryGetPortion()
//        {
//            //  Чтение поля.
//            return Interlocked.Exchange(ref _Portion, null);
//        }
//    }
//}
