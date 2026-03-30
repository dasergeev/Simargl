using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommutatorMaui
{
    /// <summary>
    /// Представляет аккаунт пользователя.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Получает или задаёт имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задаёт пароль.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="name">
        /// Имя пользователя.
        /// </param>
        /// <param name="password">
        /// Пароль.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="name"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="password"/> передана пустая ссылка.
        /// </exception>
        public Account(string name, string password)
        {
            Name = IsNotNull(name, nameof(name));
            Password = IsNotNull(password, nameof(password));
        }
    }
}
