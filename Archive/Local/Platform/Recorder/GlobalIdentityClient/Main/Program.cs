using Apeiron.Services.GlobalIdentity.Extensions;
using Apeiron.Services.GlobalIdentity.Tunings;
using Apeiron.Services.GlobalIdentity.Workers;

//  Создание инициализатора службы.
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureGlobalIdentity<ClientTuning>()
    .ConfigureServices((hostContext, services) =>
    {
        //  Регистрация служб:
        services.AddWorker<RealTimeWorker, ClientTuning>();
        services.AddWorker<HistoryWorker, ClientTuning>();
        services.AddWorker<AnswerWorker, ClientTuning>();
        services.AddWorker<UdpTeltonikaWorker, ClientTuning>();
    })
    .Build();

//  Запуск службы.
await host.RunAsync().ConfigureAwait(false);
