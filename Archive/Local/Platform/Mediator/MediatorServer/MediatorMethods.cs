namespace Apeiron.Platform.MediatorServer;

/// <summary>
/// Представляет класс с методами для выполнения на сервере.
/// </summary>
internal static class MediatorMethods
{
    /// <summary>
    /// Тестовый метод для выполнения на сервере.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Возвращает дату.</returns>
    internal static async Task<string> TestConnectionToMediatorAsync(CancellationToken cancellationToken)
    {
        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //throw new ArgumentException("Test");

        return Environment.MachineName.ToLower() + " : " + DateTime.Now.ToString();
    }
}
