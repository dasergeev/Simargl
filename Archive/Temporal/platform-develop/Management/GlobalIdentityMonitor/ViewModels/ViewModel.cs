using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Apeiron.Services.GlobalIdentity;

/// <summary>
/// Представляет базовый класс для моделей-представлений.
/// </summary>
public abstract class ViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Событие возникающее при изменении свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;


    /// <summary>
    /// Вызывает событие о изменении свойства.
    /// </summary>
    /// <param name="propertyName">Имя свойства.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Устанавливает значение свойства. 
    /// Решает проблему кольцевого обновления свойств.
    /// </summary>
    /// <typeparam name="T">Тип свойства.</typeparam>
    /// <param name="field">Ссылка на поддерживающее поле для свойства.</param>
    /// <param name="value">Значение свойства.</param>
    /// <param name="propertyName">Имя свойства. Может самостоятельно определяться компилятором за счет анотации CallerMemberName.</param>
    /// <returns>Если значение поля, которое мы хотим обновить уже соответствует значению которое передали, то false иначе вызывает событие и возвращает true.</returns>
    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        // Проверяет, что текущее значение свойства не равно переданному.
        if(Equals(field, value)) 
            return false;

        // Обновляет поле новым значением.
        field = value;

        // Генерирует событие обновления.
        OnPropertyChanged(propertyName);

        return true;
    }
}

