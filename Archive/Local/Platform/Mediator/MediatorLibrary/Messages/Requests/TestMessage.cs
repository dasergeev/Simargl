using Apeiron.IO;
using System.Text;

namespace Apeiron.Platform.MediatorLibrary.Messages.Requests;

/// <summary>
/// Представляет тестовое текстовое сообщение.
/// </summary>
public class TestMessage : GeneralMessage
{
    /// <summary>
    /// Представляет идентифицирующую последовательность.
    /// </summary>
    public override long IdSequence { get; init; }
    /// <summary>
    /// Представляет тип сообшения.
    /// </summary>
    public override byte Type { get; init; }
    /// <summary>
    /// Представляет идентификатор хоста в строковом формате.
    /// </summary>
    public override string HostId { get; init; }

    /// <summary>
    /// Представляет текстовую информацию.
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public TestMessage()
    {
        // Устанавливаем идентификационную последовательность.
        IdSequence = 0xaa008888;

        // Установка формата сообщения.
        Type = (byte)MessageType.TestTextMessage;

        // Установка имени хоста.
        HostId = Environment.MachineName.ToLower();

        // Инициализация текстового поля.
        Text = string.Empty;
    }

    /// <summary>
    /// Асинхронно загружает данные объекта из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен для отслеживания запросов отмены.
    /// </param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию чтения.
    /// </returns>
    /// <remarks>
    /// Задача, должна возвращать объект, к которому обращаются.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Поток не поддерживает чтение.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="FormatException">
    /// Поступило сообшение с неподдерживаемым форматом.
    /// </exception>
    public override async Task<GeneralMessage> LoadPackageAsync(Stream stream, CancellationToken cancellationToken)
    {
        // Проверяем поток на возможность чтения.
        Check.IsReadable(stream, nameof(stream));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства чтения из потока.
        Spreader spreader = new(stream, Encoding.UTF8);

        try
        {
            // Считываем данные.
            long receiveIdSequence = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
            byte receiveFormat = await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);
            string receiveHostId = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);
            string receiveText = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

            // Проверяем, что пакет наш.
            if (receiveIdSequence != 0xaa008888)
                throw new FormatException("Поступил некорректный пакет.");

            // Формируем новый пакет.
            return new TestMessage
            {
                IdSequence = receiveIdSequence,
                Type = receiveFormat,
                HostId = receiveHostId,
                Text = receiveText
            };

        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Асинхронно сохраняет данные объекта в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток, в который необходимо сохранить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен для отслеживания запросов отмены.
    /// </param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию записи.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Поток не поддерживает запись.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public override async Task SavePackageAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Проверка потока.
        Check.IsWritable(stream, nameof(stream));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Создание средства записи в поток.
        Spreader spreader = new(stream, Encoding.UTF8);

        try
        {
            //  Запись данных в поток.
            await spreader.WriteInt64Async(IdSequence, cancellationToken).ConfigureAwait(false);
            await spreader.WriteUInt8Async(Type, cancellationToken).ConfigureAwait(false);
            await spreader.WriteStringAsync(HostId, cancellationToken).ConfigureAwait(false);
            await spreader.WriteStringAsync(Text, cancellationToken).ConfigureAwait(false);

            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
