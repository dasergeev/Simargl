using Apeiron.Services.GlobalIdentity.Packets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest;

/// <summary>
/// Представляет класс теста для <see cref="StatusPacket"/>
/// </summary>
[TestClass]
public class StatusPacketTest
{
    /// <summary>
    /// Представляет фунцкцию тестирования <see cref="StatusPacket"/> версии от #3 и выше.
    /// </summary>
    [TestMethod]
    public void TestSaveAndLoadVersion3()
    {
        //  Создание генератора случайных чисел.
        Random random = new();

        //  Количество итераций проверки.
        int iterationCount = 50;

        //  Цикль проверки.
        for(int i = 0; i < iterationCount; i++)
        {
            //  Инициализация идентификатора.
            long globalIdentifier = random.NextInt64();

            //  Инициализация значения широты.
            double latitude = random.NextDouble();

            //  Инициализация значения долготы.
            double longitude = random.NextDouble();

            //  Инициализация значения скорости.
            int speed = random.Next();

            //  Инициализация значения флага
            bool isValid = (random.Next() & 0x01) == 0x01;

            //  Создание пакета, сообщающего состояние.
            StatusPacket packet1 = new(globalIdentifier)
            {
                Source = StatusPacketSource.RealTime,
                Latitude = latitude,
                Longitude = longitude,
                Speed = speed,
                IsValid = isValid,
            };

            //  Создание массива байт с данными пакета.
            var array = packet1.GetDatagram();

            // Проверяет что массив создан.
            Assert.IsNotNull(array);

            //  Получение пакета из массива.
            bool result = StatusPacket.TryParce(array, out StatusPacket packet2);

            //  Проверка результата функции.
            Assert.IsTrue(result);

            //  Проверка что пакет получен.
            Assert.IsNotNull(packet2);

            //  Проверка равенства глобального идентификатора.
            Assert.AreEqual(packet1.GlobalIdentifier, packet2.GlobalIdentifier);

            //  Проверка значения глобального идентификатора
            Assert.AreEqual(packet2.GlobalIdentifier, globalIdentifier);

            //  Проверка версии.
            Assert.AreEqual(packet1.Version, packet2.Version);

            //  Проверка идентификатора пакета.
            Assert.AreEqual(packet1.PacketIdentifier, packet2.PacketIdentifier);

            //  Проверка равенства типа пакета.
            Assert.AreEqual(packet1.Source, packet2.Source);

            //  Проверка значения типа пакета.
            Assert.AreEqual(packet2.Source, StatusPacketSource.RealTime);

            //  Проверка равенства широты.
            Assert.AreEqual(packet1.Latitude, packet2.Latitude);

            //  Проверка значения широты.
            Assert.AreEqual(packet2.Latitude, latitude);

            //  Проверка равенства долготы.
            Assert.AreEqual(packet1.Longitude, packet2.Longitude);

            //  Проверка значения долготы.
            Assert.AreEqual(packet2.Longitude, longitude);

            //  Проверка равенства скорости.
            Assert.AreEqual(packet1.Speed, packet2.Speed);

            //  Проверка значения скорости.
            Assert.AreEqual(packet2.Speed, speed);

            //  Проверка равенства флага достоверности данных.
            Assert.AreEqual(packet1.IsValid, packet2.IsValid);

            //  Проверка значения флага достоверности данных.
            Assert.AreEqual(packet2.IsValid, isValid);

        }
    }
}
