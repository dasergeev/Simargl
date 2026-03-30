using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет объект, контролирующий состояние.
/// </summary>
/// <param name="core">
/// Ядро.
/// </param>
public sealed class Controller(Core core) :
    Worker(core)
{
    /// <summary>
    /// Возвращает или задаёт значение, определяющее выполняется ли контроль.
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

        //  Поле для хранения времени последней проверки.
        DateTime lastTime = DateTime.Now;

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
                    Journal.Add("Начало контроля.");

                    //  Сброс времени.
                    lastTime = DateTime.Now;

                    //  Пребор устройств.
                    foreach (Device device in Core.Devices)
                    {
                        //  Перебор сигналов.
                        foreach (AccelEth3TSignal signal in device.Signals)
                        {
                            //  Начало контроля.
                            signal.BeginControl();
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
                            //  Завершение контроля.
                            signal.EndControl();
                        }
                    }

                    //  Вывод информации в журнал.
                    Journal.Add("Завершение контроля.");
                }
            }

            //  Проверка необходимости контроля.
            if (lastEnable && ((DateTime.Now - lastTime).TotalSeconds > Settings.ControlInterval))
            {
                //  Выполнение контроля.
                await ControlAsync(cancellationToken).ConfigureAwait(false);

                //  Сброс времени.
                lastTime = DateTime.Now;
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет контроль.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая контроль.
    /// </returns>
    private async Task ControlAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание построителя текста.
        StringBuilder builder = new();

        //  Уровень отступа.
        int tabs = 0;

        //  Вывод заголовка.
        write($"Контроль {DateTime.Now}:");
        ++tabs;

        //  Перебор устройств.
        foreach (Device device in Core.Devices)
        {
            //  Вывод заголовка.
            write($"Устройство {device.Name} (192.168.1.{device.Number}):");
            ++tabs;

            //  Перебор сигналов.
            foreach (AccelEth3TSignal signal in device.Signals)
            {
                //  Вывод заголовка.
                write($"Сигнал {signal.Name}:");
                ++tabs;

                //  Проверка превышения аплитуды.
                if (signal.GetViolation(out var violation))
                {
                    write($"Значение: отклонение {violation.Actual} от {violation.Permissible}, количество {violation.Count}.");
                }
                else
                {
                    write($"Значение: нет отклонений.");
                }

                //  Проверка превышения спетра.
                if (signal.Spectrum.GetViolation(out var spectrumViolation))
                {
                    write($"Амплитуда: отклонение {spectrumViolation.Actual} от {spectrumViolation.Permissible} на частоте {spectrumViolation.Frequency}, количество {spectrumViolation.Count}.");
                }
                else
                {
                    write($"Амплитуда: нет отклонений.");
                }

                //  Проверка превышения мощности.
                if (signal.Power.GetViolation(out violation))
                {
                    write($"Мощность: отклонение {violation.Actual} от {violation.Permissible}, количество {violation.Count}.");
                }
                else
                {
                    write($"Мощность: нет отклонений.");
                }

                --tabs;
            }
            --tabs;
        }

        //  Добавляет информацию в вывод.
        void write(string text)
        {
            builder.Append(new string(' ', 2 * tabs));
            builder.AppendLine(text);
        }

        //  Вывод информации в журнал.
        Journal.Add(builder.ToString());
    }
}
