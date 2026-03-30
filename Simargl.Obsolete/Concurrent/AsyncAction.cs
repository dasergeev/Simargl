namespace Simargl.Concurrent;

/// <summary>
/// Представляет асинхронное действие.
/// </summary>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
/// <returns>
/// Задача, выполняющая асинхронное действие.
/// </returns>
/// <exception cref="OperationCanceledException">
/// Операция отменена.
/// </exception>
public delegate Task AsyncAction(CancellationToken cancellationToken);

/// <summary>
/// Представляет асинхронное действие.
/// </summary>
/// <typeparam name="T">
/// Тип параметра метода, инкапсулируемого данным делегатом.
/// </typeparam>
/// <param name="obj">
/// Параметр метода, инкапсулируемого данным делегатом.
/// </param>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
/// <returns>
/// Задача, выполняющая асинхронное действие.
/// </returns>
/// <exception cref="OperationCanceledException">
/// Операция отменена.
/// </exception>
public delegate Task AsyncAction<in T>(T obj, CancellationToken cancellationToken);








//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Ably.Concurrent;

///// <summary>
///// Представляет асинхронное действие.
///// </summary>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронное действие.
///// </returns>
///// <exception cref="OperationCanceledException">
///// Операция отменена.
///// </exception>
//public delegate Task AsyncAction(CancellationToken cancellationToken);

///// <summary>
///// Представляет асинхронное действие.
///// </summary>
///// <typeparam name="T">
///// Тип параметра метода, инкапсулируемого данным делегатом.
///// </typeparam>
///// <param name="obj">
///// Параметр метода, инкапсулируемого данным делегатом.
///// </param>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронное действие.
///// </returns>
///// <exception cref="OperationCanceledException">
///// Операция отменена.
///// </exception>
//public delegate Task AsyncAction<in T>(T obj, CancellationToken cancellationToken);








//namespace Aekmot.Concurrent;

///// <summary>
///// Представляет асинхронное действие.
///// </summary>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронное действие.
///// </returns>
///// <exception cref="OperationCanceledException">
///// Операция отменена.
///// </exception>
//public delegate Task AsyncAction(CancellationToken cancellationToken);

///// <summary>
///// Представляет асинхронное действие.
///// </summary>
///// <typeparam name="T">
///// Тип параметра метода, инкапсулируемого данным делегатом.
///// </typeparam>
///// <param name="obj">
///// Параметр метода, инкапсулируемого данным делегатом.
///// </param>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронное действие.
///// </returns>
///// <exception cref="OperationCanceledException">
///// Операция отменена.
///// </exception>
//public delegate Task AsyncAction<in T>(T obj, CancellationToken cancellationToken);






//namespace Aekmot.Concurrent;

///// <summary>
///// Представляет асинхронное действие.
///// </summary>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронное действие.
///// </returns>
///// <exception cref="OperationCanceledException">
///// Операция отменена.
///// </exception>
//public delegate Task AsyncAction(CancellationToken cancellationToken);

///// <summary>
///// Представляет асинхронное действие.
///// </summary>
///// <typeparam name="T">
///// Тип параметра метода, инкапсулируемого данным делегатом.
///// </typeparam>
///// <param name="obj">
///// Параметр метода, инкапсулируемого данным делегатом.
///// </param>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронное действие.
///// </returns>
///// <exception cref="OperationCanceledException">
///// Операция отменена.
///// </exception>
//public delegate Task AsyncAction<in T>(T obj, CancellationToken cancellationToken);






//namespace Aekmot.Planning.Core;

///// <summary>
///// Представляет асинхронное действие.
///// </summary>
///// <param name="cancellationToken">
///// Токен отмены.
///// </param>
///// <returns>
///// Задача, выполняющая асинхронное действие.
///// </returns>
//public delegate Task AsyncAction(CancellationToken cancellationToken);

