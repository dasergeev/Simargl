using Simargl.AdxlRecorder.IO;
using Simargl.AdxlRecorder.Payload.Common;

namespace Simargl.AdxlRecorder.Payload;

/// <summary>
/// Представляет базовый класс для всех двоичных данных.
/// </summary>
public abstract class DataPackage
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="format">
    /// Значение, определяющее формат двоичных данных.
    /// </param>
    internal DataPackage(DataPackageFormat format)
    {
        //  Установка значения, определяющего формат двоичных данных.
        Format = format;
    }

    /// <summary>
    /// Возвращает значение, определяющее формат двоичных данных.
    /// </summary>
    public DataPackageFormat Format { get; }

    /// <summary>
    /// Асинхронно сохраняет данные в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток, в который необходимо сохранить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, сохранающая данные в поток.
    /// </returns>
    public abstract Task SaveAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно загружает данные из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая данные из потока.
    /// </returns>
    public static async Task<DataPackage> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        long position = stream.Position;

        

        //  Чтение сигнатуры.
        int signature = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Проверка сигнатуры.
        if (signature == Preamble.ActualSignature)
        {
            //  Чтение формата.
            PreambleFormat format = (PreambleFormat)await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

            //  Разбор формата.
            return format switch
            {
                PreambleFormat.RecordingUdpDatagram => await UdpDatagram.LoadAsync(spreader, cancellationToken).ConfigureAwait(false),
                PreambleFormat.RecordingTcpDataBlock => await TcpDataBlock.LoadAsync(spreader, cancellationToken).ConfigureAwait(false),
                _ => throw new InvalidDataException("Из потока данных прочитан недопустимый формат."),
            };
        }
        else
        {
            _ = position;

            //  Недопустимая сигнатура.
            throw new InvalidDataException($"Из потока данных прочитана недопустимая сигнатура. {position}");
        }
    }
}
