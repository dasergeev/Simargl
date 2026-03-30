using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Teamwork
{
    /// <summary>
    /// Представляет пакет данных.
    /// </summary>
    public class Package : Ancestor
    {



        //  508 байт


        //  8 байт     

        //  4 байта     Сигнатура
        //  4 байта     Размер пакета

        //  4 байта     Идентификатор ПК получателя
        //  4 байта     Идентификатор процесса получателя

        //  4 байта     Идентификатор ПК отправителя
        //  4 байта     Идентификатор процесса отправителя

        //  4 байта     Идентификатор группы пакетов
        //  2 байта     Количество пакетов
        //  2 байта     Номер пакета
        //
    }
}
