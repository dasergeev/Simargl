namespace Simargl.Algorithms.Raw;

/// <summary>
/// Объект ALGLIB, родительский класс для всех внутренних объектов AlgoPascal, управляемых ALGLIB.
/// Любой внутренний объект AlgoPascal наследуется от этого класса.
/// Объекты, видимые пользователем, наследуются от объекта alglib (см. ниже).
/// ALGLIB object, parent  class  for  all  internal  AlgoPascal  objects managed by ALGLIB.
/// Any internal AlgoPascal object inherits from this class.
/// User-visible objects inherit from alglibobject (see below).
/// </summary>
public abstract class apobject
{
    /// <summary>
    /// Выполняет инициализацию объекта.
    /// </summary>
    public abstract void init();

    /// <summary>
    /// Создаёт копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public abstract apobject make_copy();
}
