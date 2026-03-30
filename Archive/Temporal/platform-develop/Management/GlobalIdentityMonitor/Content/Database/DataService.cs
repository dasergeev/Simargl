using Apeiron.Platform.Databases.GlobalIdentityDatabase;
using Apeiron.Platform.Databases.GlobalIdentityDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apeiron.Services.GlobalIdentity.Content;

/// <summary>
/// Содержит логику получения данных.
/// </summary>
internal sealed class DataService : IDisposable
{
    /// <summary>
    /// Возвращает имя базы данных;
    /// </summary>
    public string DbName { get; }

    /// <summary>
    /// Содержит контекст базы данных.
    /// </summary>
    private readonly GlobalIdentityDatabaseContext _DBContext;

    /// <summary>
    /// Содержит вспомогательный контекст базы данных с нестандартным параметром ConnectTimeout для тестирования подключения.
    /// </summary>
    private readonly GlobalIdentityDatabaseContext _TestDBContext;

    /// <summary>
    /// Флаг для реализации шаблона IDisposable.
    /// </summary>
    private bool disposedValue;

    /// <summary>
    /// Инициализирует класс.
    /// </summary>
    public DataService()
    {
        // Создаём контекст подключения к базе данных.
        _DBContext = new();

        // Создаём контекст подключения к базе данных с настройками для передачи в строке подключения небольшого таймаута.
        _TestDBContext = _DBContext.GetCustomDBContext(3);

        // Устанавливаем имя базы данных.
        DbName = _TestDBContext.GetDBName();
    }


    /// <summary>
    /// Загрузка данных из БД.
    /// </summary>
    /// <exception cref = "OperationCanceledException" >
    /// Операция отменена.
    /// </exception>
    public async Task<GlobalIdentifier[]?> DataLoadAsync(CancellationToken cancellationToken)
    {
        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Проверяем подключение к БД.
        if (await _TestDBContext.CheckConnectionDBAsync(cancellationToken).ConfigureAwait(false))
        {
            try
            {
                // Получаем данные из базы.
                return await _DBContext.GlobalIdentifiers
                    .AsNoTracking()
                    .Where(x => x.Id != 3)
                    .OrderBy(g => g.Name)
                    .ToArrayAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
             catch (Exception ex)
            {
                // Если исключение связанное с подключением к БД.
                if (ex is System.Data.SqlClient.SqlException || ex is Microsoft.Data.SqlClient.SqlException)
                {
                    MessageBox.Show($"Ошибка подключения к базе данных!\n\n\r{ex.Message}", "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                // Проброс исключения на дальнейшую обработку.
                throw;
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Проверяет соединение с БД.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <exception cref = "OperationCanceledException" >
    /// Операция отменена.
    /// </exception>
    /// <returns>Возвращает статус соединения</returns>
    public async Task<(bool,string)> DbStatusDisplayAsync(CancellationToken cancellationToken)
    {
        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        if (await _TestDBContext.CheckConnectionDBAsync(cancellationToken).ConfigureAwait(false))
        {
            return (true,"Подключение установлено!");
        }
        else
        {
            return (false,"Ошибка подключения к базе данных!!!");
        }
    }


    /// <summary>
    /// Реализует шаблон IDisposable.
    /// </summary>
    /// <param name="disposing">Флаг.</param>
    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _DBContext?.Dispose();
                _TestDBContext?.Dispose();
            }

            // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
            // TODO: установить значение NULL для больших полей
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

