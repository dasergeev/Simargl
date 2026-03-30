//using Simargl.Designing;
//using System.Collections.Generic;

//namespace Simargl.Tests.Designing;

///// <summary>
///// Представляет средство тестирования типа <see cref="Verify"/>.
///// </summary>
//public sealed class VerifyTest
//{
//    /// <summary>
//    /// Возвращает данные для тестирования метода <see cref="Verify.IsNotNull"/>.
//    /// </summary>
//    public static IEnumerable<object?[]> IsNotNullTestData { get; } =
//        [
//            [123],
//            [true],
//            [3.14],
//            ["Test string"],
//            [null],
//            [new double?()],
//        ];

//    /// <summary>
//    /// Выполняет тестирование метода <see cref="Verify.IsNotNull"/>.
//    /// </summary>
//    /// <typeparam name="T">
//    /// Тип значения.
//    /// </typeparam>
//    /// <param name="value">
//    /// Тестовое значение.
//    /// </param>
//    [Theory]
//    [MemberData(nameof(IsNotNullTestData))]
//    public void IsNotNullTest<T>(T? value)
//    {
//        //  Проверка значения.
//        if (value is null)
//        {
//            //  Перехват исключения.
//            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
//                delegate
//                {
//                    //  Выполнение.
//                    Verify.IsNotNull(value);
//                });

//            //  Проверка.
//            Assert.Equal(nameof(value), exception.ParamName);

//            //  Перехват исключения.
//            exception = Assert.Throws<ArgumentNullException>(
//                delegate
//                {
//                    //  Выполнение.
//                    Verify.IsNotNull(value, "name");
//                });

//            //  Проверка.
//            Assert.Equal("name", exception.ParamName);
//        }
//        else
//        {
//            //  Выполнение.
//            T? result = Verify.IsNotNull(value);

//            //  Проверка значения.
//            Assert.Equal(result, result);
//        }
//    }
//}
