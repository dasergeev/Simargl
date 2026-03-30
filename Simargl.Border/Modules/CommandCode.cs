namespace Simargl.Border.Modules;

/// <summary>
/// Представляет значение, определяющее код команды.
/// </summary>
public enum CommandCode
{
    /// <summary>
    /// 1  Запрос пакета конфигурации Ответ –  текущая конфигурация
    /// </summary>
    /// <remarks>
    /// CMD_HELLO
    /// </remarks>
    Hello = 0x01,

    /// <summary>
    /// 2  Активация режима программирования
    /// </summary>
    /// <remarks>
    /// CMD_PROG_MODE
    /// </remarks>
    ProgMode = 0x02,

    /// <summary>
    /// 3 Запрос пакет синхронизации Ответ – текущий синхромаркер
    /// </summary>
    /// <remarks>
    /// CMD_SYNC
    /// </remarks>
    Sync = 0x03,

    /// <summary>
    /// 4  Запрос всех сохраненных данных
    /// </summary>
    /// <remarks>
    /// CMD_GET_USER_PAGE
    /// </remarks>
    GetUserPage = 0x04,

    /// <summary>
    /// 5  поддержание соединения Ethernet активным
    /// </summary>
    /// <remarks>
    /// CMD_KEEP_ALIVE
    /// </remarks>
    KeepAlive = 0x08,

    /// <summary>
    /// 6  сохранение конфигурации
    /// </summary>
    /// <remarks>
    /// CMD_SET_CONFIG
    /// </remarks>
    SetConfig = 0x09,

    /// <summary>
    /// 7  перезапуск АЦП
    /// </summary>
    /// <remarks>
    /// CMD_SET_CONFIG
    /// </remarks>
    CmdSetConfig = 0x10,
}
