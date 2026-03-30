using Microsoft.VisualStudio.TestTools.UnitTesting;
using Apeiron.Platform.OsmLibrary;

namespace UnitTest;

/// <summary>
/// Представляет класс для тестирования класса точки с координатами.
/// </summary>
[TestClass]
public class GeolocationPointTest
{

    /// <summary>
    /// Представляет функцию тестирования фукнции созданя точки. <see cref="GeolocationPoint.CreatePoint"/>.
    /// </summary>
    [TestMethod]
    [DataTestMethod]
    [DataRow(91, 181)]
    [DataRow(-91, -181)]
    [DataRow(91, 180)]
    [DataRow(90, -181)]
    [DataRow(double.PositiveInfinity, 1)]
    [DataRow(1, double.PositiveInfinity)]
    [DataRow(double.PositiveInfinity, 1)]
    [DataRow(1, double.PositiveInfinity)]
    [DataRow(double.NaN, 1)]
    [DataRow(1, double.NaN)]
    public void GeolocationPointCreatePointTest(double lat, double lon)
    {
        // Проверка, что при передачи некорретных параметров выбрасывается правильное исключение.
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => GeolocationPoint.CreatePoint(lat, lon));

        //55.83952 37.19427
    }
}

