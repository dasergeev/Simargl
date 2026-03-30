namespace Apeiron.Platform.Databases.CentralDatabase.Entities;

public partial class MicroserviceInfo
{
    /// <summary>
    /// Возвращает значение целочисленной настройки.
    /// </summary>
    /// <param name="name">
    /// Имя настройки.
    /// </param>
    /// <returns>
    /// Значение целочисленной настройки.
    /// </returns>
    public int GetInt32Setting(string name)
    {
        //  Возврат значения.
        return Int32Settings.First(int32Setting => int32Setting.Name == name).Value;
    }

    /// <summary>
    /// Асинхронно возвращает значение целочисленной настройки.
    /// </summary>
    /// <param name="name">
    /// Имя настройки.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая возврат значения.
    /// </returns>
    public async ValueTask<int> GetInt32SettingAsync(string name, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Возврат значения.
        return GetInt32Setting(name);
    }
}
