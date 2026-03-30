using System.Linq.Expressions;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет связь с сущностью.
/// </summary>
/// <typeparam name="TParentEntity">
/// Тип родительской сущности.
/// </typeparam>
/// <typeparam name="TChildEntity">
/// Тип дочерней сущности.
/// </typeparam>
internal sealed class Relation<TParentEntity, TChildEntity>
    where TParentEntity : Entity
    where TChildEntity : Entity
{
    public Relation(
        Expression<Func<TParentEntity, object>> syncRoot,
        Expression<Func<TParentEntity, long>> childId,
        Expression<Func<TParentEntity, TChildEntity?>> child)
    {

    }


    ///// <summary>
    ///// Поле для хранения родительской сущности.
    ///// </summary>
    //private readonly TParentEntity _Parent;

    ///// <summary>
    ///// Поле для хранения дочерней сущности.
    ///// </summary>
    //private TChildEntity? _Child;

    ///// <summary>
    ///// Инициализирует новый экземпляр класса.
    ///// </summary>
    ///// <param name="parent">
    ///// Родительская сущность.
    ///// </param>
    ///// <param name="childId">
    ///// Узел дерева выражений для доступа к свойству,
    ///// возвращающему
    ///// </param>
    //public Relation(TParentEntity parent, Expression<Func<TParentEntity, long>> )
    //{

    //}


}
