using System.Threading;
using System.Threading.Tasks;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет объект, отслеживающий историю.
/// </summary>
/// <param name="core">
/// Ядро.
/// </param>
public sealed class Store(Core core) :
    Worker(core)
{
    /// <summary>
    /// Возвращает или задаёт значение, определяющее сохраняется ли история.
    /// </summary>
    public bool Enable { get; set; }

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

        //  Предыдущее состояние.
        bool lastEnable = !Enable;

        //  Основной цикл отслеживания состояния.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Чтение текущего состояния.
            bool enable = Enable;

            //  Проверка изменения состояния.
            if (lastEnable != enable)
            {
                //  Установка предыдущего состояния.
                lastEnable = enable;

                //  Обработка изменения состояния.
                if (lastEnable)
                {
                    //  Вывод информации в журнал.
                    Journal.Add("Начало сбора данных о нормальном состоянии.");

                    //  Пребор устройств.
                    foreach (Device device in Core.Devices)
                    {
                        //  Перебор сигналов.
                        foreach (AccelEth3TSignal signal in device.Signals)
                        {
                            //  Начало сохранения истории о нормальном состоянии.
                            signal.BeginStore();
                        }
                    }
                }
                else
                {
                    //  Пребор устройств.
                    foreach (Device device in Core.Devices)
                    {
                        //  Перебор сигналов.
                        foreach (AccelEth3TSignal signal in device.Signals)
                        {
                            //  Завершение сохранения истории о нормальном состоянии.
                            signal.EndStore();
                        }
                    }

                    //  Вывод информации в журнал.
                    Journal.Add("Завершение сбора данных о нормальном состоянии.");
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(500, cancellationToken).ConfigureAwait(false);
        }
    }
}
