using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Changer
{
    /// <summary>
    /// Представляет приложение.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Количество попыток.
        /// </summary>
        const int Attempts = 10;

        /// <summary>
        /// Время ожидания между попытками.
        /// </summary>
        const int TimeOut = 100;

        /// <summary>
        /// Временное имя.
        /// </summary>
        const string TemplateName = "{52DC4173-5F30-42C9-B599-79B6CC15582B}";

        /// <summary>
        /// Путь к корневому каталогу.
        /// </summary>
        static readonly string Root = new FileInfo(Application.ExecutablePath).Directory.Parent.FullName;

        /// <summary>
        /// Входная точка приложения.
        /// </summary>
        static void Main()
        {
            bool result;

            try
            {
                result = Move("Release", TemplateName);
                if (result)
                {
                    result = Move("Candidate", "Release");
                    if (!result)
                    {
                        Move(TemplateName, "Release");
                    }
                }
                if (result)
                {
                    Move(TemplateName, "Candidate");
                }
            }
            catch
            {
                result = false;
            }

            try
            {
                using (var stream = new FileStream(
                    Path.Combine(Root, "Utilities", "Changer.result"),
                    FileMode.Create, FileAccess.ReadWrite))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.ASCII, true))
                    {
                        writer.Write((long)(result ? 1 : 0));
                        writer.Flush();
                    }
                    stream.Flush();
                }
            }
            catch
            {

            }

            Process.Start(Path.Combine(Root, "Release", "Binary", "Auditor.exe"));
        }

        /// <summary>
        /// Выполняет перемещение каталога.
        /// </summary>
        /// <param name="source">
        /// Исходный относительный путь.
        /// </param>
        /// <param name="target">
        /// Целевой относительный путь.
        /// </param>
        /// <returns>
        /// Результат перемещения.
        /// </returns>
        static bool Move(string source, string target)
        {
            bool result = false;
            for (int attempt = 0; attempt != Attempts; ++attempt)
            {
                try
                {
                    Directory.Move(Path.Combine(Root, source), Path.Combine(Root, target));
                    result = true;
                }
                catch
                {

                }
                if (result)
                {
                    break;
                }
                Thread.Sleep(TimeOut);
            }
            return result;
        }
    }
}
