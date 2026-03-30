using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExternalPackage;


namespace UnitTest
{

    /// <summary>
    /// Представляет класс тестирования <see cref="GeolocationPackage"/>.
    /// </summary>
    [TestClass]
    public class GeolocationPackageTest
    {
        /// <summary>
        /// Представляет фунцкцию тестирования <see cref="GeolocationPackage"/>.
        /// </summary>
        [TestMethod]
        public void TestGeolocationPackage()
        {
            //  Создание генератора случайных чисел.
            Random random = new();

            //  Количество итераций проверки.
            int iterationCount = 50;

            //  Цикль проверки.
            for (int i = 0; i < iterationCount; i++)
            {

                //  Инициализация значения широты.
                double latitude = random.NextDouble();

                //  Инициализация значения долготы.
                double longitude = random.NextDouble();

                //  Инициализация значения скорости.
                int speed = random.Next();

                //  Инициализация значения флага
                bool isValid = (random.Next() & 0x01) == 0x01;

                //  Создание пакета.
                GeolocationPackage package1 = new(latitude, longitude, speed, isValid);

                //  Создание массив байт с данными пакета.
                byte[] data = package1.GetDatagram();

                // Проверяет что массив создан.
                Assert.IsNotNull(data);

                //  Получение пакета из массива.
                bool result = GeolocationPackage.TryParse(data, out GeolocationPackage package2);

                //  Проверка результата функции.
                Assert.IsTrue(result);

                //  Проверка что пакет получен.
                Assert.IsNotNull(package2);

                //  Проверка версии.
                Assert.AreEqual(package1.Version, package2.Version);

                //  Проверка равенства широты.
                Assert.AreEqual(package1.Latitude, package2.Latitude);

                //  Проверка значения широты.
                Assert.AreEqual(package2.Latitude, latitude);

                //  Проверка равенства долготы.
                Assert.AreEqual(package1.Longitude, package2.Longitude);

                //  Проверка значения долготы.
                Assert.AreEqual(package2.Longitude, longitude);

                //  Проверка равенства скорости.
                Assert.AreEqual(package1.Speed, package2.Speed);

                //  Проверка значения скорости.
                Assert.AreEqual(package2.Speed, speed);

                //  Проверка равенства флага достоверности данных.
                Assert.AreEqual(package1.IsNewAndValideGps, package2.IsNewAndValideGps);

                //  Проверка значения флага достоверности данных.
                Assert.AreEqual(package2.IsNewAndValideGps, isValid);
            }
        }
    }
}