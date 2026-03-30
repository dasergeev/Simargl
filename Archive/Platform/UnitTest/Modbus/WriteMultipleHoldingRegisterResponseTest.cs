using Apeiron.Platform.Modbus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// Представляет класс тестирования пакетов Modbus
    /// </summary>
    [TestClass]
    public class WriteMultipleHoldingRegisterResponseTest
    {

        /// <summary>
        /// Представляет фунцкцию тестирования <see cref="WriteMultipleHoldingRegisterResponse"/>.
        /// </summary>
        [TestMethod]
        public void TestReadMultipleHoldingRegisterCommand()
        {
            //  Создание генератора случайных чисел.
            Random random = new();

            //  Количество итераций проверки.
            int iterationCount = 50;

            //  Цикль проверки.
            for (int i = 0; i < iterationCount; i++)
            {
                //  Инициализация идентификатора транзакции.
                ushort transactionIdentifier = (ushort)random.Next();

                //  Инициализация адреса устройства.
                byte modbusAddress = (byte)random.Next();

                //  Инициализация начального адреса.
                ushort startAddress = (ushort)random.Next();

                //  Инициализация количества регистров.
                ushort countRegister = ((ushort)(random.Next()% WriteMultipleHoldingRegisterResponse.MaximumRegisterCount));

                //  Инициализация команды.
                var sourcePackage = new WriteMultipleHoldingRegisterResponse(transactionIdentifier,modbusAddress,startAddress, countRegister);

                //  Создание потока.
                using MemoryStream stream = new ();

                //  Запись в поток.
                sourcePackage.Save(stream);

                //  Сброс позиции потока.
                stream.Position = 0;

                //  Создание пакета получателя.
                var resultPackage = new WriteMultipleHoldingRegisterResponse();

                //  Получение пакета.
                resultPackage.Load(stream);

                //  Проверка равенства идентификатора транзакции.
                Assert.AreEqual(sourcePackage.TransactionIdentifier, resultPackage.TransactionIdentifier);

                //  Проверка равенства адреса устройства.
                Assert.AreEqual(sourcePackage.Address, resultPackage.Address);

                //  Проверка значения функционального кода.
                Assert.AreEqual(sourcePackage.FunctionalCode, resultPackage.FunctionalCode);

                //  Проверка равенства начального адреса.
                Assert.AreEqual(sourcePackage.StartAddress, resultPackage.StartAddress);

                //  Проверка равенства количества регистров.
                Assert.AreEqual(sourcePackage.CountRegister, resultPackage.CountRegister);

                //  Проверка значения идентификатора транзакции.
                Assert.AreEqual(transactionIdentifier, resultPackage.TransactionIdentifier);

                //  Проверка значения адреса устройства.
                Assert.AreEqual(modbusAddress, resultPackage.Address);

                //  Проверка значения функционального кода.
                Assert.AreEqual(WriteMultipleHoldingRegisterResponse.CodeCommand, resultPackage.FunctionalCode);

                //  Проверка значения начального адреса.
                Assert.AreEqual(startAddress, resultPackage.StartAddress);

                //  Проверка значения количества регистров.
                Assert.AreEqual(countRegister, resultPackage.CountRegister);
            }

        }
    }
}
