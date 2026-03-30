using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// Класс для примера теста.
    /// </summary>
    [TestClass]
    public class FakeTest
    {
        /// <summary>
        /// Метод для проверки подсистемы тестирования.
        /// </summary>
        [TestMethod]
        public void FakeMethod()
        {
            var actual = true;
            Assert.IsTrue(actual);
        }
    }
}