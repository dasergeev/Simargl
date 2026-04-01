namespace Simargl.Concurrent;

/// <summary>
/// Представляет асинхронную функцию.
/// </summary>
/// <typeparam name="TResult">
/// Тип результата функции.
/// </typeparam>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
/// <returns>
/// Задача, выполняющая асинхронную функцию.
/// </returns>
public delegate Task<TResult> AsyncFunc<TResult>(CancellationToken cancellationToken);

/// <summary>
/// Представляет асинхронную функцию.
/// </summary>
/// <typeparam name="T">
/// Тип параметра функции, инкапсулируемого данным делегатом.
/// </typeparam>
/// <typeparam name="TResult">
/// Тип результата функции.
/// </typeparam>
/// <param name="obj">
/// Параметр функции, инкапсулируемый данным делегатом.
/// </param>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
/// <returns>
/// Задача, выполняющая асинхронную функцию.
/// </returns>
public delegate Task<TResult> AsyncFunc<in T, TResult>(T obj, CancellationToken cancellationToken);













//namespace Aekmot.Concurrent;

///// <summary>
///// Представляет асинхронную функцию.
///// </summary>
///// <typeparam name="TResult">
///// Тип результата функции.
///// </typeparam>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронную функцию.
///// </returns>
//public delegate Task<TResult> AsyncFunc<TResult>(CancellationToken cancellationToken);

///// <summary>
///// Представляет асинхронную функцию.
///// </summary>
///// <typeparam name="T">
///// Тип параметра функции, инкапсулируемого данным делегатом.
///// </typeparam>
///// <typeparam name="TResult">
///// Тип результата функции.
///// </typeparam>
///// <param name="obj">
///// Параметр функции, инкапсулируемый данным делегатом.
///// </param>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронную функцию.
///// </returns>
//public delegate Task<TResult> AsyncFunc<in T, TResult>(T obj, CancellationToken cancellationToken);






//namespace Aekmot.Concurrent;

///// <summary>
///// Представляет асинхронную функцию.
///// </summary>
///// <typeparam name="TResult">
///// Тип результата функции.
///// </typeparam>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронную функцию.
///// </returns>
//public delegate Task<TResult> AsyncFunc<TResult>(CancellationToken cancellationToken);

///// <summary>
///// Представляет асинхронную функцию.
///// </summary>
///// <typeparam name="T">
///// Тип параметра метода, инкапсулируемого данным делегатом.
///// </typeparam>
///// <typeparam name="TResult">
///// Тип результата функции.
///// </typeparam>
///// <param name="obj">
///// Параметр метода, инкапсулируемого данным делегатом.
///// </param>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронную функцию.
///// </returns>
//public delegate Task<TResult> AsyncFunc<in T, TResult>(T obj, CancellationToken cancellationToken);





