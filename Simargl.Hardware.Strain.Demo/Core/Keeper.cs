using Simargl.Concurrent;

namespace Simargl.Hardware.Strain.Demo.Core;

/// <summary>
/// Представляет механизм поддержки.
/// </summary>
public sealed class Keeper
{
    /// <summary>
    /// Поле для хранения токена отмены.
    /// </summary>
    private readonly CancellationToken _CancellationToken;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public Keeper(CancellationToken cancellationToken)
    {
        //  Установка токена отмены.
        _CancellationToken = cancellationToken;
    }

    /// <summary>
    /// Добавляет действие в механизм поддержки.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо добавить в механизм поддержки.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void Add(AsyncAction action)
    {
        //  Проверка действия.
        IsNotNull(action);

        //  Запуск асинхронной задачи.
        _ = Task.Run(async delegate
        {
            //  Основной цикл поддержки.
            while (!_CancellationToken.IsCancellationRequested)
            {
                //  Флаг выполнения.
                bool isCompleted = false;

                //  Безопасное выполнение.
                await DefyCriticalAsync(async delegate(CancellationToken cancellationToken)
                {
                    //  Выполнение действия.
                    await action(cancellationToken).ConfigureAwait(false);

                    //  Установка флага выполнения.
                    isCompleted = true;
                }, _CancellationToken).ConfigureAwait(false);

                //  Проверка выполнения.
                if (isCompleted)
                {
                    //  Завершение поддержки.
                    break;
                }

                //  Ожидание перед следующей попыткой выполнения.
                await Task.Delay(100, _CancellationToken).ConfigureAwait(false);
            }
        });
    }
}
