using Apeiron.Services.GlobalIdentity.Tunings;
using Apeiron.Services.GlobalIdentity.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Apeiron.Services.GlobalIdentity.Extensions;

/// <summary>
/// Предоставляет методы расширения интерфейса <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет новый фоновый процесс службы глобальной идентификации.
    /// </summary>
    /// <typeparam name="TWorker">
    /// Тип фонового процесса службы глобальной идентификации.
    /// </typeparam>
    /// <typeparam name="TTuning">
    /// Тип настроек.
    /// </typeparam>
    /// <param name="services">
    /// Коллекция служб.
    /// </param>
    /// <returns>
    /// Коллекция служб.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="services"/> передана пустая ссылка.
    /// </exception>
    public static IServiceCollection AddWorker<TWorker, TTuning>(
        this IServiceCollection services)
        where TWorker : Worker<TWorker, TTuning>
        where TTuning : Tuning
    {
        //  Проверка ссылки на коллекцию служб.
        services = Check.IsNotNull(services, nameof(services));

        //  Получение конструктора.
        ConstructorInfo constructor = Check.IsNotNull(
            typeof(TWorker).GetConstructor(new[] { typeof(WorkerContext<TWorker, TTuning>) }),
            nameof(constructor));

        //  Создание делегата для вызова конструктора.
        TWorker newWorker(WorkerContext<TWorker, TTuning> context)
        {
            //  Вызов конструктора.
            return (TWorker)constructor.Invoke(new object?[] { context });
        }

        //  Регистрация службы.
        services.AddHostedService(serviceProvider =>
        {
            WorkerContext<TWorker, TTuning> context = new(
                serviceProvider.GetService<TTuning>()!,
                serviceProvider.GetService<ILogger<TWorker>>()!);
            return newWorker(context);
        });

        //  Возврат коллекции служб.
        return services;
    }
}
