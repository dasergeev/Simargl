//using Hbm.Api.Common.Entities.Problems;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Предоставляет механизмы вызова функций библиотеки HBM.
//    /// </summary>
//    internal static class Performing
//    {
//        /// <summary>
//        /// Постоянная, определяющая сообщение об ошибке, которое используется по умолчанию.
//        /// </summary>
//        public const string DefaultMessage = "Не удалось выполнить операцию.";

//        /// <summary>
//        /// Представляет вызываемый метод.
//        /// </summary>
//        /// <typeparam name="T">
//        /// Тип аргумента метода.
//        /// </typeparam>
//        /// <param name="argument">
//        /// Аргумент метода.
//        /// </param>
//        /// <param name="problems">
//        /// Список проблем.
//        /// </param>
//        /// <returns>
//        /// Результат вызова.
//        /// </returns>
//        public delegate bool MethodCalled<T>(T argument, out List<Problem> problems);

//        /// <summary>
//        /// Представляет вызываемый метод.
//        /// </summary>
//        /// <typeparam name="TFirst">
//        /// Тип первого аргумента.
//        /// </typeparam>
//        /// <typeparam name="TSecond">
//        /// Тип второго аргумента.
//        /// </typeparam>
//        /// <param name="first">
//        /// Первый аргумент метода.
//        /// </param>
//        /// <param name="second">
//        /// Второй аргумент метода.
//        /// </param>
//        /// <param name="problems">
//        /// Список проблем.
//        /// </param>
//        /// <returns>
//        /// Результат вызова.
//        /// </returns>
//        public delegate bool MethodCalled<TFirst, TSecond>(TFirst first, TSecond second, out List<Problem> problems);

//        /// <summary>
//        /// Выполняет действие.
//        /// </summary>
//        /// <param name="action">
//        /// Действие, которое необходимо выполнить.
//        /// </param>
//        /// <param name="message">
//        /// Сообщение об ошибке.
//        /// </param>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public static void Perform(Action action, string message = DefaultMessage)
//        {
//            try
//            {
//                action();
//            }
//            catch (Exception ex)
//            {
//                throw new InvalidOperationException(message, ex);
//            }
//        }

//        /// <summary>
//        /// Выполняет вызов функции.
//        /// </summary>
//        /// <typeparam name="T">
//        /// Тип возвращаемого значения.
//        /// </typeparam>
//        /// <param name="func">
//        /// Функция, которую необходимо выполнить.
//        /// </param>
//        /// <param name="message">
//        /// Сообщение об ошибке.
//        /// </param>
//        /// <returns>
//        /// Результат вызова.
//        /// </returns>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public static T Perform<T>(Func<T> func, string message = DefaultMessage)
//        {
//            T result = default;
//            Perform(() =>
//            {
//                result = func();
//            }, message);
//            return result;
//        }

//        /// <summary>
//        /// Вызывает метод.
//        /// </summary>
//        /// <typeparam name="T">
//        /// Тип аргумента метода.
//        /// </typeparam>
//        /// <param name="method">
//        /// Вызываемый метод.
//        /// </param>
//        /// <param name="argument">
//        /// Аргумент метода.
//        /// </param>
//        /// <param name="message">
//        /// Сообщение об ошибке.
//        /// </param>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public static void Perform<T>(MethodCalled<T> method, T argument, string message = DefaultMessage)
//        {
//            List<Problem> problems = new List<Problem>();
//            Exception exception = null;
//            bool result = false;
//            try
//            {
//                result = method(argument, out problems);
//            }
//            catch (Exception ex)
//            {
//                exception = ex;
//            }
//            CheckResults(exception, problems, result, message);
//        }

//        /// <summary>
//        /// Вызывает метод.
//        /// </summary>
//        /// <typeparam name="TFirst">
//        /// Тип первого аргумента.
//        /// </typeparam>
//        /// <typeparam name="TSecond">
//        /// Тип второго аргумента.
//        /// </typeparam>
//        /// <param name="method">
//        /// Вызываемый метод.
//        /// </param>
//        /// <param name="first">
//        /// Первый аргумент метода.
//        /// </param>
//        /// <param name="second">
//        /// Второй аргумент метода.
//        /// </param>
//        /// <param name="message">
//        /// Сообщение об ошибке.
//        /// </param>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        public static void Perform<TFirst, TSecond>(MethodCalled<TFirst, TSecond> method,
//            TFirst first, TSecond second, string message = DefaultMessage)
//        {
//            List<Problem> problems = new List<Problem>();
//            Exception exception = null;
//            bool result = false;
//            try
//            {
//                result = method(first, second, out problems);
//            }
//            catch (Exception ex)
//            {
//                exception = ex;
//            }
//            CheckResults(exception, problems, result, message);
//        }

//        /// <summary>
//        /// Выполняет проверку результатов.
//        /// </summary>
//        /// <param name="exception">
//        /// Исключение.
//        /// </param>
//        /// <param name="problems">
//        /// Список проблем.
//        /// </param>
//        /// <param name="result">
//        /// Результат.
//        /// </param>
//        /// <param name="message">
//        /// Сообщение об ошибке.
//        /// </param>
//        private static void CheckResults(Exception exception, IList<Problem> problems, bool result, string message)
//        {
//            if (exception is object)
//            {
//                throw new InvalidOperationException(message, exception);
//            }
//            if (problems.Count != 0)
//            {
//                throw new InvalidOperationException(message, ToException(problems));
//            }
//            if (!result)
//            {
//                throw new InvalidOperationException(message);
//            }
//        }

//        /// <summary>
//        /// Выполняет преобразование проблемы в исключение.
//        /// </summary>
//        /// <param name="problem">
//        /// Проблема.
//        /// </param>
//        /// <returns>
//        /// Исключение.
//        /// </returns>
//        private static Exception ToException(Problem problem)
//        {
//            return new Exception(problem.Message);
//        }

//        /// <summary>
//        /// Выполняет преобразование списка проблем в исключение.
//        /// </summary>
//        /// <param name="problems">
//        /// Список проблем.
//        /// </param>
//        private static Exception ToException(IEnumerable<Problem> problems)
//        {
//            List<Exception> innerExceptions = new List<Exception>();
//            foreach (var problem in problems)
//            {
//                innerExceptions.Add(ToException(problem));
//            }
//            return new AggregateException(innerExceptions);
//        }
//    }
//}
