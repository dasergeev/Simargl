using Microsoft.Extensions.Logging;
using Simargl.Hardware.Receiving.Net;
using System.Collections.Concurrent;

namespace Simargl.Hardware.Recorder.Core;

/// <summary>
/// Представляет управляющего буферами данных тензомодулей.
/// </summary>
public sealed class StrainBufferManager
{
    /// <summary>
    /// Поле для хранения буферов.
    /// </summary>
    private readonly ConcurrentDictionary<long, StrainBuffer> _Buffers = [];

    /// <summary>
    /// Асинхронно добавляет данные.
    /// </summary>
    /// <param name="result">
    /// Данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, добавляющая данные.
    /// </returns>
    public async Task AddAsync(TcpDataReceiveResult result, CancellationToken cancellationToken)
    {
        //  Получение буфера.
        StrainBuffer buffer = _Buffers.GetOrAdd(result.ConnectionKey, key => new StrainBuffer(key));

        //  Добавление данных.
        await buffer.AddAsync(result, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет нормализацию.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая нормализацию.
    /// </returns>
    public async Task NormalizationAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение ключей простаивающих буферов.
        IEnumerable<long> keys = _Buffers
            .Where(x => x.Value.Outage > TimeSpan.FromSeconds(10))
            .Select(x => x.Key);

        //  Перебор ключей.
        foreach (long key in keys)
        {
            //  Удаление буфера.
            _Buffers.TryRemove(key, out var _);
        }

        //  Получение ключей переполненных буферов.
        keys = _Buffers
            .Where(x => x.Value.IsOverflow)
            .Select(x => x.Key);

        //  Перебор ключей.
        foreach (long key in keys)
        {
            //  Добавление в очередь плохих потоков.
            Heart.Unique.BadStrainKeys.Enqueue(key);

            //  Удаление буфера.
            _Buffers.TryRemove(key, out var _);
        }
    }

    /// <summary>
    /// Асинхронно выполняет чтение пакета.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение пакета.
    /// </returns>
    public async Task<bool> TryReadAsync(ILogger logger, CancellationToken cancellationToken)
    {
        //  Перебор буферов.
        foreach (KeyValuePair<long, StrainBuffer> pair in _Buffers)
        {
            //  Чтение данных из буфера.
            bool result = await pair.Value.TryReadAsync(logger, cancellationToken).ConfigureAwait(false);

            //  Проверка результата.
            if (result)
            {
                //  Прочитан пакет данных.
                return true;
            }
        }

        //  Нет прочитанных пакетов.
        return false;
    }
}
