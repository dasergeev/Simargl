using System.Runtime.CompilerServices;

namespace Simargl;

/// <summary>
/// Представляет объект.
/// </summary>
public abstract class Anything :
    IAnything
{
    /// <summary>
    /// Поле для хранения базы объекта.
    /// </summary>
    private AnythingBasis? _Basis;

    /// <summary>
    /// Выполняет внутреннее обращение к объекту.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void Lay()
    {
        //  Для анализатора.
        _ = this;
    }

    /// <summary>
    /// Возвращает базу объекта.
    /// </summary>
    /// <returns>
    /// База объекта.
    /// </returns>
    public virtual AnythingBasis GetBasis()
    {
        //  Проверка базы объекта.
        if (_Basis is not AnythingBasis basis)
        {
            //  Получение исходной базы объекта.
            basis = this.GetOriginalBasis();

            //  Замена базы.
            basis = Interlocked.CompareExchange(ref _Basis, basis, null) ?? basis;
        }

        //  Возврат базы.
        return basis;
    }
}
