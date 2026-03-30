using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет объект, отслеживающий состояние.
/// </summary>
/// <param name="core">
/// Ядро.
/// </param>
public sealed class Status(Core core) :
    Worker(core)
{
    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл отслеживания.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Перебор всех устройств.
            for (int i = 0; i < Core.Devices.Count; i++)
            {
                //  Выполнение в основном потоке.
                await Core.Invoker.InvokeAsync(delegate
                {
                    //  Установка значения активности.
                    Core.MainForm.SetDeviceActive(i, Core.Devices[i].Active);
                }, cancellationToken).ConfigureAwait(false);
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }
    }
}
