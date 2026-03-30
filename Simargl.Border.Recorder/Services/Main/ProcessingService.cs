using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Simargl.Border.Processing;
using Simargl.Border.Storage;
using Simargl.Border.Storage.Entities;
using System.IO;

namespace Simargl.Border.Recorder.Services.Main;

/// <summary>
/// Представляет службу обработки.
/// </summary>
/// <param name="program">
/// Программа.
/// </param>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class ProcessingService(Program program, ILogger<ProcessingService> logger) :
    Service(program, logger)
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
    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Получение устройства обработки.
        Processor processor = await Program.GetProcessorAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Создание контекста для работы с базой данных.
            await using BorderStorageContext context = new(BasisConstants.Storage);

            //  Запрос необработанных данных.
            PassageData? passage = await context.Passages
                .Where(x => x.State == PassageState.Registered)
                .Include(x => x.Axes)
                .ThenInclude(x => x.Interactions)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Проверка данных.
            if (passage is not null)
            {
                //  Определение каталога исходных данных.
                string sourcePath = Path.Combine(BasisConstants.RawFramesPath, $"0x{passage.Key:X16}");

                //  Определение целевого каталога.
                string targetPath = Path.Combine(BasisConstants.ProcessedFramesPath, $"0x{passage.Key:X16}");

                //  Создание устройства предобработки.
                Preprocessor preprocessor = new(processor, sourcePath, targetPath);

                //  Выполнение предобработки.
                await preprocessor.InvokeAsync(cancellationToken).ConfigureAwait(false);

                //  Начало транзакции.
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                //  Блок перехвата всех исключений.
                try
                {
                    //  Установка общих значений.
                    passage.AxesCount = preprocessor.PressureMap.AxesCount;
                    passage.AxesCommits = preprocessor.PressureMap.AxesCommits;

                    //  Очистка данных.
                    passage.Axes.Clear();

                    //  Перебор осей.
                    foreach (Axis axis in preprocessor.Axes)
                    {
                        //  Создание данных оси.
                        AxisData axisData = new()
                        {
                            Index = axis.Index
                        };

                        //  Перебор взаимодействий.
                        foreach (AxisInteraction? interaction in axis.Interactions)
                        {
                            //  Проверка взаимодействия.
                            if (interaction is null)
                            {
                                //  Переход к следующему взаимодействию.
                                continue;
                            }

                            //  Создание данных взаимодействия.
                            AxisInteractionData interactionData = new()
                            {
                                Section = interaction.Section,
                                Position = interaction.Position,
                                Time = interaction.Time,
                                Speed = interaction.Speed,
                                Length = interaction.Length,
                                SpeedSum = interaction.SpeedSum,
                                SpeedSquaresSum = interaction.SpeedSquaresSum,
                                SpeedAverage = interaction.SpeedAverage,
                                SpeedDeviation = interaction.SpeedDeviation,
                                LeftSum = interaction.LeftSum,
                                LeftSquaresSum = interaction.LeftSquaresSum,
                                LeftAverage = interaction.LeftAverage,
                                LeftDeviation = interaction.LeftDeviation,
                                LeftMax = interaction.LeftMax,
                                RightSum = interaction.RightSum,
                                RightSquaresSum = interaction.RightSquaresSum,
                                RightMax = interaction.RightMax,
                                RightAverage = interaction.RightAverage,
                                RightDeviation = interaction.RightDeviation,
                            };

                            //  Добавление данных.
                            axisData.Interactions.Add(interactionData);
                        }

                        //  Добавление данных.
                        passage.Axes.Add(axisData);
                    }

                    //  Установка флага обработки.
                    passage.State = PassageState.Processed;

                    //  Сохранение изменений.
                    await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                    //  Применение транзакции.
                    await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
                }
                catch
                {
                    //  Отмена транзакции.
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);

                    //  Повторный выброс исключения.
                    throw;
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(BasisConstants.MediumServicePeriod, cancellationToken).ConfigureAwait(false);
        }
    }
}
