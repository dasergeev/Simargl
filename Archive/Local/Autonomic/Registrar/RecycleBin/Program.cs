using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет приложение.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Определяет точку входа в приложение.
        /// </summary>
        static void Main()
        {
            //  Бесконечный цикл.
            while (true)
            {
                //  Очистка корзины.
                EmptyRecycleBin();

                //  Ожидание.
                Thread.Sleep(60000);
            }
        }

        /// <summary>
        /// Выполняет очистку корзины.
        /// </summary>
        static void EmptyRecycleBin()
        {
            try
            {
                SHEmptyRecycleBin(IntPtr.Zero, IntPtr.Zero, 7);
                Console.WriteLine($"[{DateTime.Now}] Корзина очищена.");
            }
            catch
            {

            }
        }

        [DllImport("Shell32")]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, IntPtr pszRootPath, uint dwFlags);
    }
}
