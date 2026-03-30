using System.IO;
using System.Net;

namespace Simargl.QuantumX;

/// <summary>
/// Класс заголовка пакета транспортного уровня, который используется при обмене с устройствами HBM
/// </summary>
public class HbmHeader
{
    /// <summary>
    /// Типы сообщений
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Не определено
        /// </summary>
        UNKNOWN = 0, 
        /// <summary>
        /// Данные
        /// </summary>
        DATA = 1,
        /// <summary>
        /// Метаинформация
        /// </summary>
        META = 2
    }

    /// <summary>
    /// Размер сообщения в байтах
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Номер сигнала
    /// </summary>
    public int SignalNumber { get; private set; }

    /// <summary>
    /// Тип сообщения
    /// </summary>
    public MessageType Type { get; private set; }


    /// <summary>
    /// Чтение заголовка из потока
    /// </summary>
    /// <param name="stream">Поток памяти.</param>
    public static HbmHeader GetHeader(MemoryStream stream)
    {
        //  Экземпляр класса.
        HbmHeader hbmHeader = new ();

        // Создание читателя
        using BinaryReader reader = new(stream, System.Text.Encoding.UTF8, true);
        
        //  Чтение заголовка в сетевом порядке.
        int headerNet = reader.ReadInt32();
        
        //  Преобразование заголовка
        int  header = IPAddress.NetworkToHostOrder(headerNet);

        //  Получение номера сигнала
        hbmHeader.SignalNumber = (int)(header & 0x000fffff);

        //  Получение типа
        hbmHeader.Type = (MessageType)((header & 0x30000000) >> 28);

        //  Получение размера
        hbmHeader.Size = (int)((header & 0x0ff00000) >> 20);

        //  Если полученный размер 0, то размер передается в следующем за заголовком слове (32 бит)
        if (hbmHeader.Size == 0) 
        {
            // Получение размера
            int additionalSizeBig = reader.ReadInt32();

            //  Преобразование из сетевого порядка байт.
            hbmHeader.Size = (int)IPAddress.NetworkToHostOrder(additionalSizeBig);
        }

        //  Возврат значения.
        return hbmHeader;
    }
}
