//using Microsoft.Extensions.DependencyInjection;

//namespace Simargl.Platform.Performers;

///// <summary>
///// Предоставляет методы расширения для типа <see cref="IServiceCollection"/>.
///// </summary>
//public static class PerformerServiceExtensions
//{
//    /// <summary>
//    /// Добавляет фоновый процесс, выполняющий работу исполнителя.
//    /// </summary>
//    /// <typeparam name="TPerformer">
//    /// Тип исполнителя.
//    /// </typeparam>
//    /// <param name="services">
//    /// Коллекция служб.
//    /// </param>
//    /// <returns>
//    /// Коллекция служб.
//    /// </returns>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="services"/> передана пустая ссылка.
//    /// </exception>
//    public static IServiceCollection AddPerformer<TPerformer>(this IServiceCollection services)
//        where TPerformer : Performer
//    {
//        //  Проверка ссылки на коллекцию служб.
//        services = IsNotNull(services, nameof(services));

//        //  Добавление фонового процесса, выполняющего работу исполнителя.
//        services.AddHostedService<PerformerService<TPerformer>>();

//        //  Возврат коллекции служб.
//        return services;
//    }
//}
