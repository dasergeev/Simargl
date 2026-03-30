using Simargl.Hardware.Modbus.Core;

namespace Simargl.Hardware.Strain.Demo.Core;

/// <summary>
/// Представляет транзакцию по Modbus.
/// </summary>
public sealed class ModbusTransaction :
    Anything
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="request">
    /// Запрос.
    /// </param>
    /// <param name="taskCompletionSource">
    /// Источник завершения задачи.
    /// </param>
    public ModbusTransaction(TcpAduPackage request, TaskCompletionSource<TcpAduPackage> taskCompletionSource)
    {
        //  Обращение к объекту.
        Lay();

        //  Установка значений.
        Request = request;
        TaskCompletionSource = taskCompletionSource;
    }

    /// <summary>
    /// Возвращает запрос.
    /// </summary>
    public TcpAduPackage Request { get; }

    /// <summary>
    /// Возвращает источник завершения задачи.
    /// </summary>
    public TaskCompletionSource<TcpAduPackage> TaskCompletionSource { get; }
}
