using Apeiron.Platform.Modbus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// Представляет класс тестирования пакетов Modbus
    /// </summary>
    [TestClass]
    public class ReadMultipleHoldingRegisterResponseTest
    {

        /// <summary>
        /// Представляет фунцкцию тестирования <see cref="ReadMultipleHoldingRegisterResponse"/>.
        /// </summary>
        [TestMethod]
        public void TestReadMultipleHoldingRegisterResponse()
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

                //  Инициализация количества регистров.
                ushort countRegister = ((ushort)(random.Next()% ReadMultipleHoldingRegisterResponse.MaximumRegisterCount));

                //  Создание массива регистров.
                ushort[] registers = new ushort[countRegister];

                //  Цикл по всем регистрам.
                for(int j = 0; j < countRegister; j++)
                {
                    //  Присвоение регистра.
                    registers[j] = (ushort)random.Next();
                }

                //  Инициализация команды.
                var sourcePackage = new ReadMultipleHoldingRegisterResponse(transactionIdentifier,modbusAddress, registers);

                //  Создание потока.
                using MemoryStream stream = new ();

                //  Запись в поток.
                sourcePackage.Save(stream);

                //  Сброс позиции потока.
                stream.Position = 0;

                //  Создание пакета получателя.
                var resultPackage = new ReadMultipleHoldingRegisterResponse();

                //  Получение пакета.
                resultPackage.Load(stream);

                //  Проверка равенства идентификатора транзакции.
                Assert.AreEqual(sourcePackage.TransactionIdentifier, resultPackage.TransactionIdentifier);

                //  Проверка равенства адреса устройства.
                Assert.AreEqual(sourcePackage.Address, resultPackage.Address);

                //  Проверка значения функционального кода.
                Assert.AreEqual(sourcePackage.FunctionalCode, resultPackage.FunctionalCode);

                //  Проверка равенства количества регистров.
                Assert.AreEqual(sourcePackage.Registers.Length, resultPackage.Registers.Length);

                //  Проверка значения идентификатора транзакции.
                Assert.AreEqual(transactionIdentifier, resultPackage.TransactionIdentifier);

                //  Проверка значения адреса устройства.
                Assert.AreEqual(modbusAddress, resultPackage.Address);

                //  Проверка значения функционального кода.
                Assert.AreEqual(ReadMultipleHoldingRegisterResponse.CodeCommand, resultPackage.FunctionalCode);

                //  Проверка значения количества регистров.
                Assert.AreEqual(countRegister, resultPackage.Registers.Length);

                //  Цикл по всем регистрам.
                for (int j = 0; j < countRegister; j++)
                {
                    //  Проверка равенства регистров.
                    Assert.AreEqual(sourcePackage.Registers[j], resultPackage.Registers[j]);

                    //  Проверка значения регистров.
                    Assert.AreEqual(registers[j], resultPackage.Registers[j]);
                }
            }

        }
    }
}
