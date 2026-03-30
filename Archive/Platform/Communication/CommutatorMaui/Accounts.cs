using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommutatorMaui
{
    /// <summary>
    /// Представляет коллекцию аккаунтов.
    /// </summary>
    internal class Accounts:
        ObservableCollection<Account>
    {
        /// <summary>
        /// Представляет коллекцию аккаунтов.
        /// </summary>
        public Accounts() { }

    }
}
