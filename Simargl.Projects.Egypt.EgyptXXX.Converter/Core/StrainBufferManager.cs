using Microsoft.Extensions.Logging;
using Simargl.Payload.Recording;
using System.Collections.Concurrent;

namespace Simargl.Projects.Egypt.EgyptXXX.Converter.Core;

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
    public async Task AddAsync(TcpDataBlock result, CancellationToken cancellationToken)
    {
        //  Получение буфера.
        StrainBuffer buffer = _Buffers.GetOrAdd(result.ConnectionKey, key => new StrainBuffer(key));

        //  Добавление данных.
        await buffer.AddAsync(result, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет чтение пакета.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение пакета.
    /// </returns>
    [CLSCompliant(false)]
    public async Task<StrainData[]> TryReadAsync(CancellationToken cancellationToken)
    {
        List<StrainData> data = [];

        //  Перебор буферов.
        foreach (KeyValuePair<long, StrainBuffer> pair in _Buffers)
        {
            //  Чтение данных из буфера.
            while (await pair.Value.TryReadAsync(cancellationToken).ConfigureAwait(false) is StrainData strainData)
            {
                data.Add(strainData);
            }
        }

        //  Нет прочитанных пакетов.
        return [.. data];
    }
}
