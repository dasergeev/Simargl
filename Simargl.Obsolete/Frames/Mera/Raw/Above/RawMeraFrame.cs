using Simargl.Designing;
using System.IO;
using System.Text;

namespace Simargl.Frames.Mera.Raw;

/// <summary>
/// Представляет сырой кадр в формате <see cref="StorageFormat.Mera"/>.
/// </summary>
public sealed class RawMeraFrame
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="header">
    /// Заголовок кадра.
    /// </param>
    private RawMeraFrame([NoVerify] MeraFrameHeader header)
    {
        //  Установка заголовка кадра.
        Header = header;

        //  Создание коллекции сырых заголовков канала.
        ChannelHeaders = [];
    }

    /// <summary>
    /// Возвращает заголовок кадра.
    /// </summary>
    public MeraFrameHeader Header { get; }

    /// <summary>
    /// Возвращает коллекцию сырых заголовков канала.
    /// </summary>
    public RawMeraChannelHeaderCollection ChannelHeaders { get; }

    /// <summary>
    /// Асинхронно загружает сырой кадр в формате <see cref="StorageFormat.Mera"/>.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить кадр.
    /// </param>
    /// <param name="encoding">
    /// Кодировка символов, которую нужно использовать.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая сырой кадр в формате <see cref="StorageFormat.Mera"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="encoding"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Обнаружен неизвестный элемент информационного файла в формате <see cref="StorageFormat.Mera"/>.
    /// </exception>
    public static async Task<RawMeraFrame> LoadFrameAsync(Stream stream, Encoding encoding, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на поток.
        IsNotNull(stream, nameof(stream));

        //  Проверка ссылки на кодировку.
        IsNotNull(encoding, nameof(encoding));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства чтения.
        using RawMeraInfoReader reader = new(stream, encoding, true);

        //  Чтение заголовка кадра.
        MeraFrameHeader frameHeader = await reader.ReadFrameHeader(cancellationToken).ConfigureAwait(false);

        //  Создание сырого кадра.
        RawMeraFrame frame = new(frameHeader);

        //  Цикл чтения заголовков каналов.
        while (true)
        {
            //  Чтение заголовка канала.
            RawMeraChannelHeader? channelHeader = await reader.ReadChannelHeaderAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка прочитанного заголовка канала.
            if (channelHeader is null)
            {
                //  Завершение чтения.
                break;
            }

            //  Получение имени канала.
            string name = channelHeader.Header.Name;

            //  Перебор предыдущих заголовков каналов.
            foreach (RawMeraChannelHeader header in frame.ChannelHeaders)
            {
                //  Проверка имени канала.
                if (header.Header.Name == name)
                {
                    throw new InvalidDataException($"Обнаружено несколько каналов с именем \"{name}\".");
                }
            }

            //  Добавление заголовка канала в коллекцию.
            frame.ChannelHeaders.Add(channelHeader);
        }

        //  Возврат сырого кадра.
        return frame;
    }
}
