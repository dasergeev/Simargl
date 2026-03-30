using Microsoft.EntityFrameworkCore;
using Simargl.Projects.Oriole.Oriole01Storage;
using Simargl.Projects.Oriole.Oriole01Storage.Entities;
using System.Net;

namespace Simargl.Trials.Oriole.Oriole01.Manual.Core;

/// <summary>
/// Предоставляет вспомогательные методы для работы с базой данных.
/// </summary>
public static class StorageHelper
{
    /// <summary>
    /// Асинхронно находит данные канала.
    /// </summary>
    /// <param name="name">
    /// Имя канала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск данных канала.
    /// </returns>
    public static async Task<(long Key, int Axis, double Direct)> FindAdxlAsync(
        string name, CancellationToken cancellationToken)
    {
        //  Подключение к базе данных.
        await using Oriole01StorageContext context = new();

        //  Получение данных канала.
        ChannelNameData nameData = await context.ChannelNames
            .FirstAsync(x => x.Name == name, cancellationToken)
            .ConfigureAwait(false);

        //  Получение данных устройства.
        DeviceData deviceData = await context.Devices
            .FirstAsync(x => x.Serial == nameData.Serial, cancellationToken)
            .ConfigureAwait(false);

        //  Преобразование адреса.
        int address = BitConverter.ToInt32(
            IPAddress.Parse(deviceData.IPAddress).GetAddressBytes());

        //  Получение данных датчика.
        AdxlData adxlData = await context.Adxls
            .FirstAsync(x => x.Address == address, cancellationToken)
            .ConfigureAwait(false);

        //  Возврат данных.
        return (adxlData.Key, nameData.Axis, nameData.Direct);
    }
}
