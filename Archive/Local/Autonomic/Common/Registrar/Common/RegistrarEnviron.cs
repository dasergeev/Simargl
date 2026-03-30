using System.IO;
using System.Windows.Forms;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет окружение регистратора.
    /// </summary>
    public static class RegistrarEnviron
    {
        /// <summary>
        /// Возвращает путь к каталогу исполняемых файлов.
        /// </summary>
        public static DirectoryInfo BinaryPath { get; }

        /// <summary>
        /// Инициализирует статические переменные.
        /// </summary>
        static RegistrarEnviron()
        {
            BinaryPath = new FileInfo(Application.ExecutablePath).Directory;
        }
    }
}
