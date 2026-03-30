using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Core.Code;

namespace Simargl.Embedded.Tunings.TuningsEditor.Services;

/// <summary>
/// Представляет службу генерации кода.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public sealed class GenerationService(ILogger<GenerationService> logger, Heart heart) :
    BackgroundService
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
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Извлечение генератора кода.
            if (Interlocked.Exchange(ref heart.CodeGenerator, null) is CodeGenerator codeGenerator)
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Выполнение основной работы генератора кода.
                    await codeGenerator.InvokeAsync(logger, cancellationToken);
                }
                catch (Exception ex)
                {
                    //  Вывод информации в журнал.
                    logger.LogError("{ex}", ex.Message);
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }
    }
}
