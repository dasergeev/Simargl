using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Simargl.Server.Hub.Core;

enum HostedServiceState
{
    Starting, Running, Stopping, Stopped, Faulted
}

sealed class HostedServicesRegistry
{
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, (string Name, Type Type, HostedServiceState State, DateTimeOffset Changed)> _map
        = new();

    public event Action? Changed;

    public IReadOnlyList<(string Id, string Name, Type Type, HostedServiceState State, DateTimeOffset Changed)> Snapshot() =>
        _map.Select(kv => (kv.Key, kv.Value.Name, kv.Value.Type, kv.Value.State, kv.Value.Changed))
            .OrderBy(x => x.Name).ToList();

    public string Register(Type type, string? displayName = null)
    {
        var id = $"{type.Name}-{Guid.NewGuid():N}";
        _map[id] = (displayName ?? type.Name, type, HostedServiceState.Starting, DateTimeOffset.UtcNow);
        Changed?.Invoke();
        return id;
    }

    public void SetState(string id, HostedServiceState state)
    {
        if (_map.TryGetValue(id, out var v))
        {
            _map[id] = (v.Name, v.Type, state, DateTimeOffset.UtcNow);
            Changed?.Invoke();
        }
    }

    public void Unregister(string id)
    {
        _map.TryRemove(id, out _);
        Changed?.Invoke();
    }
}

sealed class TrackingHostedService : IHostedService, IDisposable
{
    private readonly IHostedService _inner;
    private readonly HostedServicesRegistry _registry;
    private readonly string _id;

    public TrackingHostedService(IHostedService inner, HostedServicesRegistry registry)
    {
        _inner = inner;
        _registry = registry;
        _id = _registry.Register(inner.GetType(), inner.GetType().Name);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _registry.SetState(_id, HostedServiceState.Starting);
        try
        {
            await _inner.StartAsync(cancellationToken);
            _registry.SetState(_id, HostedServiceState.Running);
        }
        catch
        {
            _registry.SetState(_id, HostedServiceState.Faulted);
            throw;
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _registry.SetState(_id, HostedServiceState.Stopping);
        try
        {
            await _inner.StopAsync(cancellationToken);
            _registry.SetState(_id, HostedServiceState.Stopped);
        }
        catch
        {
            _registry.SetState(_id, HostedServiceState.Faulted);
            throw;
        }
        finally
        {
            _registry.Unregister(_id);
        }
    }

    public void Dispose() => (_inner as IDisposable)?.Dispose();
}

static class HostedServiceDecorationExtensions
{
    public static IServiceCollection DecorateAllHostedServices(this IServiceCollection services)
    {
        var descriptors = services.Where(d => d.ServiceType == typeof(IHostedService)).ToList();

        foreach (var d in descriptors)
        {
            services.Remove(d);

            services.Add(new ServiceDescriptor(
                typeof(IHostedService),
                sp =>
                {
                    // Воссоздаём оригинальный сервис
                    var original = (IHostedService)(d.ImplementationInstance
                        ?? (d.ImplementationFactory != null ? d.ImplementationFactory(sp)
                        : ActivatorUtilities.CreateInstance(sp, d.ImplementationType!)));

                    // Оборачиваем в трекинг без изменения чужого кода
                    var registry = sp.GetRequiredService<HostedServicesRegistry>();
                    return new TrackingHostedService(original, registry);
                },
                d.Lifetime));
        }

        return services;
    }
}

sealed class MemoryLogProvider : ILoggerProvider
{
    public event Action<string>? Appended; // category

    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, System.Collections.Concurrent.ConcurrentQueue<string>> _store = new();

    public ILogger CreateLogger(string categoryName) => new MemoryLogger(categoryName, _store, cat => Appended?.Invoke(cat));

    public IReadOnlyList<string> Get(string category, int max = 500)
    {
        if (!_store.TryGetValue(category, out var q)) return Array.Empty<string>();
        return q.Reverse().Take(max).Reverse().ToList();
    }

    public void Dispose() { }

    private sealed class MemoryLogger : ILogger
    {
        private readonly string _category;
        private readonly System.Collections.Concurrent.ConcurrentDictionary<string, System.Collections.Concurrent.ConcurrentQueue<string>> _store;
        private readonly Action<string> _onAppend;

        public MemoryLogger(string category,
            System.Collections.Concurrent.ConcurrentDictionary<string, System.Collections.Concurrent.ConcurrentQueue<string>> store,
            Action<string> onAppend)
        {
            _category = category;
            _store = store;
            _onAppend = onAppend;
        }

#pragma warning disable CS8633 // Допустимость значения NULL в ограничениях для параметра типа не соответствует ограничениям параметра типа в явно реализованном методе интерфейса.
        public IDisposable BeginScope<TState>(TState state) => default!;
#pragma warning restore CS8633 // Допустимость значения NULL в ограничениях для параметра типа не соответствует ограничениям параметра типа в явно реализованном методе интерфейса.
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            
            var msg = $"[{DateTime.Now:HH:mm:ss}] {logLevel}: {formatter(state, exception)}";
            if (exception != null) msg += $" | {exception.GetType().Name}: {exception.Message}";
            var q = _store.GetOrAdd(_category, _ => new System.Collections.Concurrent.ConcurrentQueue<string>());
            q.Enqueue(msg);
            while (q.Count > 1000 && q.TryDequeue(out _)) { }
            _onAppend(_category);
            Console.WriteLine($"[DEBUG] Log category: {_category}");
        }
    }
}
