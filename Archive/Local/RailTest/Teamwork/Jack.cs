using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Teamwork
{
    /// <summary>
    /// Представляет соединение.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class Jack
    {
        /// <summary>
        /// Поле для хранения сокета.
        /// </summary>
        private readonly Socket _Socket;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public Jack()
        {
            _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                _Socket.UseOnlyOverlappedIO = true;
            }
            catch (InvalidOperationException)
            {
                //  Сокет привязан к порту завершения.
                throw;
            }
        }



        /// <summary>
        /// Возвращает значение, указывающее количество полученных из сети и доступных для чтения данных.
        /// </summary>
        /// <exception cref="SocketException">
        /// Произошла ошибка при попытке доступа к сокету.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// Сокет был закрыт.
        /// </exception>
        public int Available => _Socket.Available;

        /// <summary>
        /// Возвращает локальную конечную точку.
        /// </summary>
        /// <exception cref="SocketException">
        /// Произошла ошибка при попытке доступа к сокету.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// Сокет был закрыт.
        /// </exception>
        public EndPoint LocalEndPoint => _Socket.LocalEndPoint;

        /// <summary>
        /// Возвращает удаленную конечную точку.
        /// </summary>
        /// <exception cref="SocketException">
        /// Произошла ошибка при попытке доступа к сокету.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// Сокет был закрыт.
        /// </exception>
        public EndPoint RemoteEndPoint => _Socket.RemoteEndPoint;




        //// Сводка:
        ////     Получает значение, указывающее, подключается ли объект System.Net.Sockets.Socket
        ////     к удаленному узлу в результате последней операции Overload:System.Net.Sockets.Socket.Send
        ////     или Overload:System.Net.Sockets.Socket.Receive.
        ////
        //// Возврат:
        ////     Значение true, если объект System.Net.Sockets.Socket в результате последней операции
        ////     был подключен к удаленному ресурсу; в противном случае — значение false.
        //public bool Connected { get; }

        //// Сводка:
        ////     Получает значение, указывающее, привязан ли объект System.Net.Sockets.Socket
        ////     к конкретному локальному порту.
        ////
        //// Возврат:
        ////     Значение true, если объект System.Net.Sockets.Socket привязан к локальному порту;
        ////     в противном случае — значение false.
        //public bool IsBound { get; }

        //// Сводка:
        ////     Возвращает или задает значение System.Boolean, указывающее, разрешает ли объект
        ////     System.Net.Sockets.Socket привязку к порту только одного процесса.
        ////
        //// Возврат:
        ////     Значение true, если объект System.Net.Sockets.Socket разрешает привязку только
        ////     одного сокета к определенному порту; в противном случае — значение false. По
        ////     умолчанию используется true для Windows Server 2003 и Windows XP с пакетом обновления
        ////     2 и false для всех остальных версий.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.InvalidOperationException:
        ////     Объект System.Net.Sockets.Socket.Bind(System.Net.EndPoint) вызван для этого объекта
        ////     System.Net.Sockets.Socket.
        //public bool ExclusiveAddressUse { get; set; }

        //// Сводка:
        ////     Получает или задает значение, задающее размер приемного буфера объекта System.Net.Sockets.Socket.
        ////
        //// Возврат:
        ////     Объект System.Int32, который содержит значение размера приемного буфера в байтах.
        ////     Значение по умолчанию — 8192.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение, указанное для операции установки, меньше 0.
        //public int ReceiveBufferSize { get; set; }

        //// Сводка:
        ////     Получает или задает значение, определяющее размер буфера передачи объекта System.Net.Sockets.Socket.
        ////
        //// Возврат:
        ////     Объект System.Int32, который содержит значение размера буфера передачи в байтах.
        ////     Значение по умолчанию — 8192.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение, указанное для операции установки, меньше 0.
        //public int SendBufferSize { get; set; }

        //// Сводка:
        ////     Получает или устанавливает значение, указывающее промежуток времени, после которого
        ////     для синхронного вызова Overload:System.Net.Sockets.Socket.Receive истечет время
        ////     тайм-аута.
        ////
        //// Возврат:
        ////     Значение времени ожидания в миллисекундах. По умолчанию используется значение
        ////     0, указывающее на бесконечное значение интервала для тайм-аута Задание значения
        ////     -1 также указывает на бесконечное значение интервала для тайм-аута.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение, указанное для операции задания, меньше –1.
        //public int ReceiveTimeout { get; set; }

        //// Сводка:
        ////     Получает или устанавливает значение, указывающее промежуток времени, после которого
        ////     для синхронного вызова Overload:System.Net.Sockets.Socket.Send истечет время
        ////     тайм-аута.
        ////
        //// Возврат:
        ////     Значение времени ожидания в миллисекундах. Если для этого свойства задать значение
        ////     от 1 до 499, значение будет изменено на 500. По умолчанию используется значение
        ////     0, указывающее на бесконечное значение интервала для тайм-аута Задание значения
        ////     -1 также указывает на бесконечное значение интервала для тайм-аута.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение, указанное для операции задания, меньше –1.
        //public int SendTimeout { get; set; }

        //// Сводка:
        ////     Возвращает или задает значение, указывающее, будет ли объект System.Net.Sockets.Socket
        ////     задерживать закрытие сокета при попытке отправки всех отложенных данных.
        ////
        //// Возврат:
        ////     Объект System.Net.Sockets.LingerOption, указывающий задержку при закрытии сокета.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public LingerOption LingerState { get; set; }

        //// Сводка:
        ////     Возвращает или задает значение System.Boolean, указывающее, используется ли поток
        ////     System.Net.Sockets.Socket в алгоритме Nagle.
        ////
        //// Возврат:
        ////     Значение false, если объект System.Net.Sockets.Socket использует алгоритм Nagle;
        ////     в противном случае — значение true. Значение по умолчанию — false.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к объекту System.Net.Sockets.Socket.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public bool NoDelay { get; set; }

        //// Сводка:
        ////     Получает или задает значение, задающее время существования (TTL) IP-пакетов,
        ////     отправленных объектом System.Net.Sockets.Socket.
        ////
        //// Возврат:
        ////     Значение времени существования TTL.
        ////
        //// Исключения:
        ////   T:System.ArgumentOutOfRangeException:
        ////     В качестве величины срока жизни нельзя задать отрицательное число.
        ////
        ////   T:System.NotSupportedException:
        ////     Это свойство может быть установлено только для сокетов в семействах System.Net.Sockets.AddressFamily.InterNetwork
        ////     или System.Net.Sockets.AddressFamily.InterNetworkV6.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. Эта ошибка также возвращается
        ////     при попытке задать срок жизни больше, чем 255.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public short Ttl { get; set; }

        //// Сводка:
        ////     Возвращает или задает значение System.Boolean, указывающее, разрешает ли объект
        ////     System.Net.Sockets.Socket выполнение фрагментации датаграмм протокола IP.
        ////
        //// Возврат:
        ////     Значение true, если объект System.Net.Sockets.Socket разрешает фрагментацию датаграмм;
        ////     в противном случае — значение false. Значение по умолчанию — true.
        ////
        //// Исключения:
        ////   T:System.NotSupportedException:
        ////     Это свойство может быть установлено только для сокетов в семействах System.Net.Sockets.AddressFamily.InterNetwork
        ////     или System.Net.Sockets.AddressFamily.InterNetworkV6.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public bool DontFragment { get; set; }

        //// Сводка:
        ////     Возвращает или задает значение, указывающее, могут ли доставляться исходящие
        ////     пакеты многоадресной рассылки в передающем приложении.
        ////
        //// Возврат:
        ////     Значение true, если объект System.Net.Sockets.Socket получает исходящие пакеты
        ////     многоадресной рассылки; в противном случае — значение false.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public bool MulticastLoopback { get; set; }

        //// Сводка:
        ////     Возвращает тип службы System.Net.Sockets.Socket.
        ////
        //// Возврат:
        ////     Одно из значений System.Net.Sockets.SocketType.
        //public SocketType SocketType { get; }

        //// Сводка:
        ////     Возвращает или задает значение System.Boolean, указывающее, может ли объект System.Net.Sockets.Socket
        ////     производить отправку или прием широковещательных пакетов.
        ////
        //// Возврат:
        ////     Значение true, если объект System.Net.Sockets.Socket разрешает использование
        ////     широковещательных пакетов; в противном случае — значение false. Значение по умолчанию
        ////     — false.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Эта функция применима только для сокета датаграмм.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public bool EnableBroadcast { get; set; }

        //// Сводка:
        ////     Возвращает или задает значение System.Boolean, указывающее, является ли System.Net.Sockets.Socket
        ////     сокетом с двойным режимом, используемым для IPv4 и IPv6.
        ////
        //// Возврат:
        ////     Значение true, если System.Net.Sockets.Socket — сокет с двойным режимом. В противном
        ////     случае — значение false. Значение по умолчанию — false.
        //public bool DualMode { get; set; }

        //// Сводка:
        ////     Отменяет выполнение асинхронного запроса для подключения к удаленному узлу.
        ////
        //// Параметры:
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs, используемый для запроса соединения
        ////     с удаленным узлом путем вызова одного из методов System.Net.Sockets.Socket.ConnectAsync(System.Net.Sockets.SocketType,System.Net.Sockets.ProtocolType,System.Net.Sockets.SocketAsyncEventArgs).
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     Параметр e и System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint не могут
        ////     иметь значение NULL.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий объект, находящийся выше в стеке вызовов, не имеет разрешения на запрошенную
        ////     операцию.
        //public static void CancelConnectAsync(SocketAsyncEventArgs e);

        //// Сводка:
        ////     Начинает выполнение асинхронного запроса для подключения к удаленному узлу.
        ////
        //// Параметры:
        ////   socketType:
        ////     Одно из значений System.Net.Sockets.SocketType.
        ////
        ////   protocolType:
        ////     Одно из значений System.Net.Sockets.ProtocolType.
        ////
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs для использования в данной асинхронной
        ////     операции сокета.
        ////
        //// Возврат:
        ////     true, если операция ввода-вывода находится в состоянии ожидания. По завершении
        ////     операции создается событие System.Net.Sockets.SocketAsyncEventArgs.Completed
        ////     в параметре e. false, если операция ввода-вывода завершена синхронно. В данном
        ////     случае событие System.Net.Sockets.SocketAsyncEventArgs.Completed на параметре
        ////     e не будет создано и объект e, передаваемый как параметр, можно изучить сразу
        ////     после получения результатов вызова метода для извлечения результатов операции.
        ////
        //// Исключения:
        ////   T:System.ArgumentException:
        ////     Аргумент является недопустимым. Это исключение возникает, если задано несколько
        ////     буферов, свойство System.Net.Sockets.SocketAsyncEventArgs.BufferList не имеет
        ////     значение "null".
        ////
        ////   T:System.ArgumentNullException:
        ////     Параметр e и System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint не могут
        ////     иметь значение NULL.
        ////
        ////   T:System.InvalidOperationException:
        ////     System.Net.Sockets.Socket ведет прослушивание или работа с сокетом уже выполняется
        ////     с использованием объекта System.Net.Sockets.SocketAsyncEventArgs, указанного
        ////     параметром e.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows XP и более поздних версиях. Это исключение
        ////     возникает также в том случае, если локальная конечная точка и объект System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint
        ////     не принадлежат к одному семейству адресов.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий объект, находящийся выше в стеке вызовов, не имеет разрешения на запрошенную
        ////     операцию.
        //public static bool ConnectAsync(SocketType socketType, ProtocolType protocolType, SocketAsyncEventArgs e);

        //// Сводка:
        ////     Определяет состояние одного или нескольких сокетов.
        ////
        //// Параметры:
        ////   checkRead:
        ////     System.Collections.IList экземпляров System.Net.Sockets.Socket для проверки удобства
        ////     чтения.
        ////
        ////   checkWrite:
        ////     System.Collections.IList экземпляров System.Net.Sockets.Socket для проверки удобства
        ////     ведения записи.
        ////
        ////   checkError:
        ////     System.Collections.IList экземпляров System.Net.Sockets.Socket для проверки ошибок.
        ////
        ////   microSeconds:
        ////     Значение времени ожидания в миллисекундах. Значение -1 указывает на бесконечное
        ////     время ожидания.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     Параметр checkRead имеет значение null или является пустым. и - Параметр checkWrite
        ////     имеет значение null или является пустым. и - Параметр checkError имеет значение
        ////     null или является пустым.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public static void Select(IList checkRead, IList checkWrite, IList checkError, int microSeconds);

        //// Сводка:
        ////     Создает новый объект System.Net.Sockets.Socket для заново созданного подключения.
        ////
        //// Возврат:
        ////     Объект System.Net.Sockets.Socket для заново созданного подключения.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.InvalidOperationException:
        ////     Принимающий сокет не производит прослушивание подключений. Необходимо вызвать
        ////     System.Net.Sockets.Socket.Bind(System.Net.EndPoint) и System.Net.Sockets.Socket.Listen(System.Int32)
        ////     до вызова объекта System.Net.Sockets.Socket.Accept.
        //public Socket Accept();

        //// Сводка:
        ////     Начинает асинхронную операцию, чтобы принять попытку входящего подключения.
        ////
        //// Параметры:
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs для использования в данной асинхронной
        ////     операции сокета.
        ////
        //// Возврат:
        ////     true, если операция ввода-вывода находится в состоянии ожидания. По завершении
        ////     операции создается событие System.Net.Sockets.SocketAsyncEventArgs.Completed
        ////     в параметре e. false, если операция ввода-вывода завершена синхронно. Событие
        ////     System.Net.Sockets.SocketAsyncEventArgs.Completed на параметре e не произойдет
        ////     и объект e, передаваемый как параметр, можно изучить сразу после получения результатов
        ////     вызова метода для извлечения результатов операции.
        ////
        //// Исключения:
        ////   T:System.ArgumentException:
        ////     Аргумент является недопустимым. Это исключение возникает, если обеспечиваемый
        ////     буфер имеет недостаточный размер. Буфер должен иметь размер, равный, по крайней
        ////     мере, 2 * (размер(SOCKADDR_STORAGE + 16) байт. Это исключение также возникает,
        ////     если задано несколько буферов, свойство System.Net.Sockets.SocketAsyncEventArgs.BufferList
        ////     не имеет значение "null".
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Аргумент вне диапазона. Исключение возникает, если объект System.Net.Sockets.SocketAsyncEventArgs.Count
        ////     имеет значение меньше 0.
        ////
        ////   T:System.InvalidOperationException:
        ////     Предпринят запрос выполнения недопустимой операции. Это исключение возникает,
        ////     если принимающий объект System.Net.Sockets.Socket не производит прослушивание
        ////     подключений или принимающий сокет является связанным. Требуется вызвать объект
        ////     System.Net.Sockets.Socket.Bind(System.Net.EndPoint) и метод System.Net.Sockets.Socket.Listen(System.Int32)
        ////     перед вызовом метода System.Net.Sockets.Socket.AcceptAsync(System.Net.Sockets.SocketAsyncEventArgs).
        ////     Это исключение также происходит, если сокет уже подключен или работа с сокетом
        ////     уже выполнялась с использованием указанного параметра e.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows XP и более поздних версиях.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public bool AcceptAsync(SocketAsyncEventArgs e);

        //// Сводка:
        ////     Начинает асинхронную операцию, чтобы принять попытку входящего подключения с
        ////     указанного сокета и получить первый блок данных, посланных клиентским приложением.
        ////
        //// Параметры:
        ////   acceptSocket:
        ////     Принятый объект System.Net.Sockets.Socket. Это значение может быть равно null.
        ////
        ////   receiveSize:
        ////     Максимальное число принимаемых байтов.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное создание объекта
        ////     System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows NT.
        ////
        ////   T:System.InvalidOperationException:
        ////     Принимающий сокет не производит прослушивание подключений. Необходимо вызвать
        ////     System.Net.Sockets.Socket.Bind(System.Net.EndPoint) и System.Net.Sockets.Socket.Listen(System.Int32)
        ////     до вызова объекта System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object).
        ////     -или- Производится связывание принимающего сокета.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра receiveSize меньше 0.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public IAsyncResult BeginAccept(Socket acceptSocket, int receiveSize, AsyncCallback callback, object state);

        //// Сводка:
        ////     Начинает асинхронную операцию, чтобы принять попытку входящего подключения и
        ////     получить первый блок данных, посланных клиентским приложением.
        ////
        //// Параметры:
        ////   receiveSize:
        ////     Число байтов, которые необходимо принять от отправителя.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное создание объекта
        ////     System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows NT.
        ////
        ////   T:System.InvalidOperationException:
        ////     Принимающий сокет не производит прослушивание подключений. Необходимо вызвать
        ////     System.Net.Sockets.Socket.Bind(System.Net.EndPoint) и System.Net.Sockets.Socket.Listen(System.Int32)
        ////     до вызова объекта System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object).
        ////     -или- Производится связывание принимающего сокета.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра receiveSize меньше 0.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public IAsyncResult BeginAccept(int receiveSize, AsyncCallback callback, object state);

        //// Сводка:
        ////     Начинает асинхронную операцию, чтобы принять попытку входящего подключения.
        ////
        //// Параметры:
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное создание объекта
        ////     System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows NT.
        ////
        ////   T:System.InvalidOperationException:
        ////     Принимающий сокет не производит прослушивание подключений. Необходимо вызвать
        ////     System.Net.Sockets.Socket.Bind(System.Net.EndPoint) и System.Net.Sockets.Socket.Listen(System.Int32)
        ////     до вызова объекта System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object).
        ////     -или- Производится связывание принимающего сокета.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра receiveSize меньше 0.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public IAsyncResult BeginAccept(AsyncCallback callback, object state);

        //// Сводка:
        ////     Начинает выполнение асинхронного запроса для подключения к удаленному узлу.
        ////
        //// Параметры:
        ////   remoteEP:
        ////     Объект System.Net.EndPoint, представляющий удаленный узел.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное подключение.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     remoteEP — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий объект, находящийся выше в стеке вызовов, не имеет разрешения на запрошенную
        ////     операцию.
        ////
        ////   T:System.InvalidOperationException:
        ////     Сокет System.Net.Sockets.Socket был переведен в состояние прослушивания путем
        ////     вызова System.Net.Sockets.Socket.Listen(System.Int32), либо асинхронная операция
        ////     уже выполняется.
        //public IAsyncResult BeginConnect(EndPoint remoteEP, AsyncCallback callback, object state);

        //// Сводка:
        ////     Начинает выполнение асинхронного запроса для подключения к удаленному узлу. Узел
        ////     задается объектом System.Net.IPAddress и номером порта.
        ////
        //// Параметры:
        ////   address:
        ////     Адрес System.Net.IPAddress удаленного узла.
        ////
        ////   port:
        ////     Номер порта удаленного узла.
        ////
        ////   requestCallback:
        ////     Делегат System.AsyncCallback, ссылающийся на метод, вызываемый по завершении
        ////     операции подключения.
        ////
        ////   state:
        ////     Пользовательский объект, содержащий информацию об операции подключения. Этот
        ////     объект передается делегату requestCallback по завершении операции.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное подключение.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     address — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Объект System.Net.Sockets.Socket не входит в семейство сокетов.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Недействительный номер порта.
        ////
        ////   T:System.ArgumentException:
        ////     Длина параметра address равна нулю.
        ////
        ////   T:System.InvalidOperationException:
        ////     Сокет System.Net.Sockets.Socket был переведен в состояние прослушивания путем
        ////     вызова System.Net.Sockets.Socket.Listen(System.Int32), либо асинхронная операция
        ////     уже выполняется.
        //public IAsyncResult BeginConnect(IPAddress address, int port, AsyncCallback requestCallback, object state);

        //// Сводка:
        ////     Начинает выполнение асинхронного запроса для подключения к удаленному узлу. Узел
        ////     задается именем узла и номером порта.
        ////
        //// Параметры:
        ////   host:
        ////     Имя удаленного узла.
        ////
        ////   port:
        ////     Номер порта удаленного узла.
        ////
        ////   requestCallback:
        ////     Делегат System.AsyncCallback, ссылающийся на метод, вызываемый по завершении
        ////     операции подключения.
        ////
        ////   state:
        ////     Пользовательский объект, содержащий информацию об операции подключения. Этот
        ////     объект передается делегату requestCallback по завершении операции.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное подключение.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     host — null.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод допустим для сокетов в семействах System.Net.Sockets.AddressFamily.InterNetwork
        ////     или System.Net.Sockets.AddressFamily.InterNetworkV6.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Недействительный номер порта.
        ////
        ////   T:System.InvalidOperationException:
        ////     Сокет System.Net.Sockets.Socket был переведен в состояние прослушивания путем
        ////     вызова System.Net.Sockets.Socket.Listen(System.Int32), либо асинхронная операция
        ////     уже выполняется.
        //public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state);

        //// Сводка:
        ////     Начинает выполнение асинхронного запроса для подключения к удаленному узлу. Узел
        ////     задается массивом System.Net.IPAddress и номером порта.
        ////
        //// Параметры:
        ////   addresses:
        ////     По крайней мере один System.Net.IPAddress, обозначающий удаленный узел.
        ////
        ////   port:
        ////     Номер порта удаленного узла.
        ////
        ////   requestCallback:
        ////     Делегат System.AsyncCallback, ссылающийся на метод, вызываемый по завершении
        ////     операции подключения.
        ////
        ////   state:
        ////     Пользовательский объект, содержащий информацию об операции подключения. Этот
        ////     объект передается делегату requestCallback по завершении операции.
        ////
        //// Возврат:
        ////     System.IAsyncResult, который ссылается на асинхронное подключение.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     addresses — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод допустим для сокетов, использующих System.Net.Sockets.AddressFamily.InterNetwork
        ////     или System.Net.Sockets.AddressFamily.InterNetworkV6.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Недействительный номер порта.
        ////
        ////   T:System.ArgumentException:
        ////     Длина параметра address равна нулю.
        ////
        ////   T:System.InvalidOperationException:
        ////     Сокет System.Net.Sockets.Socket был переведен в состояние прослушивания путем
        ////     вызова System.Net.Sockets.Socket.Listen(System.Int32), либо асинхронная операция
        ////     уже выполняется.
        //public IAsyncResult BeginConnect(IPAddress[] addresses, int port, AsyncCallback requestCallback, object state);

        //// Сводка:
        ////     Начинает выполнение асинхронного запроса для отключения от удаленной конечной
        ////     точки.
        ////
        //// Параметры:
        ////   reuseSocket:
        ////     Значение true, если этот сокет может быть повторно использован после закрытия
        ////     подключения; в противном случае — значение false.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронную операцию.
        ////
        //// Исключения:
        ////   T:System.NotSupportedException:
        ////     Используется операционная система Windows 2000 или более ранняя версия, а для
        ////     этого метода необходима операционная система Windows XP.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public IAsyncResult BeginDisconnect(bool reuseSocket, AsyncCallback callback, object state);

        //// Сводка:
        ////     Начинает выполнение асинхронного приема данных с подключенного объекта System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   buffers:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback, ссылающийся на метод, вызываемый по завершении
        ////     данной операции.
        ////
        ////   state:
        ////     Пользовательский объект, содержащий информацию об операции приема. Этот объект
        ////     передается делегату System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)
        ////     по завершении операции.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное чтение.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        //public IAsyncResult BeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, AsyncCallback callback, object state);

        //// Сводка:
        ////     Начинает выполнение асинхронного приема данных с подключенного объекта System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        ////   offset:
        ////     Отсчитываемая с нуля позиция в параметре buffer, начиная с которой хранятся принятые
        ////     данные.
        ////
        ////   size:
        ////     Количество байтов, которые необходимо получить.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback, ссылающийся на метод, вызываемый по завершении
        ////     данной операции.
        ////
        ////   state:
        ////     Пользовательский объект, содержащий информацию об операции приема. Этот объект
        ////     передается делегату System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)
        ////     по завершении операции.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное чтение.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра offset.
        //public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state);

        //// Сводка:
        ////     Начинает выполнение асинхронного приема данных с подключенного объекта System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        ////   offset:
        ////     Место в объекте buffer, выделенное для хранения принимаемых данных.
        ////
        ////   size:
        ////     Количество байтов, которые необходимо получить.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   errorCode:
        ////     Объект System.Net.Sockets.SocketError, содержащий ошибку сокета.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback, ссылающийся на метод, вызываемый по завершении
        ////     данной операции.
        ////
        ////   state:
        ////     Пользовательский объект, содержащий информацию об операции приема. Этот объект
        ////     передается делегату System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)
        ////     по завершении операции.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное чтение.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра offset.
        //public IAsyncResult BeginReceive(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state);

        //// Сводка:
        ////     Начинает выполнение асинхронного приема данных с подключенного объекта System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   buffers:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   errorCode:
        ////     Объект System.Net.Sockets.SocketError, содержащий ошибку сокета.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback, ссылающийся на метод, вызываемый по завершении
        ////     данной операции.
        ////
        ////   state:
        ////     Пользовательский объект, содержащий информацию об операции приема. Этот объект
        ////     передается делегату System.Net.Sockets.Socket.EndReceive(System.IAsyncResult)
        ////     по завершении операции.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное чтение.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        //public IAsyncResult BeginReceive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state);

        //// Сводка:
        ////     Начинает выполнение асинхронного приема данных с указанного сетевого устройства.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        ////   offset:
        ////     Отсчитываемая с нуля позиция в параметре buffer, начиная с которой хранятся данные.
        ////
        ////   size:
        ////     Количество байтов, которые необходимо получить.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   remoteEP:
        ////     Объект System.Net.EndPoint, представляющий источник данных.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное чтение.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра offset.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий объект, находящийся выше в стеке вызовов, не имеет разрешения на запрошенную
        ////     операцию.
        //public IAsyncResult BeginReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP, AsyncCallback callback, object state);

        //// Сводка:
        ////     Начинает асинхронный прием заданного числа байтов данных в указанное место буфера
        ////     данных, используя заданный объект System.Net.Sockets.SocketFlags, а также сохраняет
        ////     конечную точку и информацию пакета.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        ////   offset:
        ////     Отсчитываемая с нуля позиция в параметре buffer, начиная с которой хранятся данные.
        ////
        ////   size:
        ////     Количество байтов, которые необходимо получить.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   remoteEP:
        ////     Объект System.Net.EndPoint, представляющий источник данных.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронное чтение.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра offset.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Используется операционная система Windows 2000 или более ранняя версия, а для
        ////     этого метода необходима операционная система Windows XP.
        //public IAsyncResult BeginReceiveMessageFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP, AsyncCallback callback, object state);

        //// Сводка:
        ////     Выполняет асинхронную передачу данных на подключенный объект System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который содержит передаваемые данные.
        ////
        ////   offset:
        ////     Отсчитываемая с нуля позиция в параметре buffer, с которой начинается отправка
        ////     данных.
        ////
        ////   size:
        ////     Количество байтов для отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   errorCode:
        ////     Объект System.Net.Sockets.SocketError, содержащий ошибку сокета.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронную передачу.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. См. ниже примeчания к данному
        ////     разделу.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение параметра offset меньше значения
        ////     длины, указанного в параметре buffer. -или- Значение параметра size меньше 0.
        ////     -или- Значение size превышает значение, полученное, если отнять от длины buffer
        ////     значение параметра offset.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state);

        //// Сводка:
        ////     Выполняет асинхронную передачу данных на подключенный объект System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который содержит передаваемые данные.
        ////
        ////   offset:
        ////     Отсчитываемая с нуля позиция в параметре buffer, с которой начинается отправка
        ////     данных.
        ////
        ////   size:
        ////     Количество байтов для отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронную передачу.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. См. ниже примeчания к данному
        ////     разделу.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение параметра offset меньше значения
        ////     длины, указанного в параметре buffer. -или- Значение параметра size меньше 0.
        ////     -или- Значение size превышает значение, полученное, если отнять от длины buffer
        ////     значение параметра offset.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public IAsyncResult BeginSend(byte[] buffer, int offset, int size, SocketFlags socketFlags, AsyncCallback callback, object state);

        //// Сводка:
        ////     Выполняет асинхронную передачу данных на подключенный объект System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   buffers:
        ////     Массив типа System.Byte, который содержит передаваемые данные.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   errorCode:
        ////     Объект System.Net.Sockets.SocketError, содержащий ошибку сокета.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронную передачу.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffers — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр buffers пуст.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. См. ниже примeчания к данному
        ////     разделу.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public IAsyncResult BeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode, AsyncCallback callback, object state);

        //// Сводка:
        ////     Выполняет асинхронную передачу данных на подключенный объект System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   buffers:
        ////     Массив типа System.Byte, который содержит передаваемые данные.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронную передачу.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffers — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр buffers пуст.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. См. ниже примeчания к данному
        ////     разделу.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public IAsyncResult BeginSend(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, AsyncCallback callback, object state);

        //// Сводка:
        ////     Отправляет файл fileName на подключенный объект System.Net.Sockets.Socket, используя
        ////     флаг System.Net.Sockets.TransmitFileOptions.UseDefaultWorkerThread.
        ////
        //// Параметры:
        ////   fileName:
        ////     Строка, содержащая путь и имя отправляемого файла. Этот параметр может иметь
        ////     значение null.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который представляет асинхронную передачу.
        ////
        //// Исключения:
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Сокет не подключен к удаленному узлу.
        ////
        ////   T:System.IO.FileNotFoundException:
        ////     Файл fileName не найден.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. См. ниже примeчания к данному
        ////     разделу.
        //public IAsyncResult BeginSendFile(string fileName, AsyncCallback callback, object state);

        //// Сводка:
        ////     Выполняет асинхронную передачу файла и буферов данных на подключенный объект
        ////     System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   fileName:
        ////     Строка, содержащая путь и имя отправляемого файла. Этот параметр может иметь
        ////     значение null.
        ////
        ////   preBuffer:
        ////     Массив System.Byte, содержащий данные, отправляемые перед передачей файла. Этот
        ////     параметр может иметь значение null.
        ////
        ////   postBuffer:
        ////     Массив System.Byte, содержащий данные, отправляемые после передачи файла. Этот
        ////     параметр может иметь значение null.
        ////
        ////   flags:
        ////     Побитовое сочетание значений System.Net.Sockets.TransmitFileOptions.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback, который должен быть вызван, когда эта операция
        ////     завершается. Этот параметр может иметь значение null.
        ////
        ////   state:
        ////     Определенный пользователем объект, содержащий сведения о состоянии для этого
        ////     запроса. Этот параметр может иметь значение null.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронную операцию.
        ////
        //// Исключения:
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. См. ниже примeчания к данному
        ////     разделу.
        ////
        ////   T:System.NotSupportedException:
        ////     Операционной системой не является Windows NT или более поздняя версия. -или-
        ////     Сокет не подключен к удаленному узлу.
        ////
        ////   T:System.IO.FileNotFoundException:
        ////     Файл fileName не найден.
        //public IAsyncResult BeginSendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags, AsyncCallback callback, object state);

        //// Сводка:
        ////     Асинхронно передает данные на конкретный удаленный узел.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который содержит передаваемые данные.
        ////
        ////   offset:
        ////     Отсчитываемая с нуля позиция в параметре buffer, с которой начинается отправка
        ////     данных.
        ////
        ////   size:
        ////     Количество байтов для отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   remoteEP:
        ////     Объект System.Net.EndPoint, представляющий удаленное устройство.
        ////
        ////   callback:
        ////     Делегат System.AsyncCallback.
        ////
        ////   state:
        ////     Объект, содержащий сведения о состоянии для этого запроса.
        ////
        //// Возврат:
        ////     Объект System.IAsyncResult, который ссылается на асинхронную передачу.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра offset.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий объект, находящийся выше в стеке вызовов, не имеет разрешения на запрошенную
        ////     операцию.
        //public IAsyncResult BeginSendTo(byte[] buffer, int offset, int size, SocketFlags socketFlags, EndPoint remoteEP, AsyncCallback callback, object state);

        //// Сводка:
        ////     Связывает объект System.Net.Sockets.Socket с локальной конечной точкой.
        ////
        //// Параметры:
        ////   localEP:
        ////     Локальный объект System.Net.EndPoint, который необходимо связать с объектом System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     localEP — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий объект, находящийся выше в стеке вызовов, не имеет разрешения на запрошенную
        ////     операцию.
        //public void Bind(EndPoint localEP);

        //// Сводка:
        ////     Закрывает подключение System.Net.Sockets.Socket и освобождает все связанные ресурсы
        ////     с заданным временем ожидания, чтобы разрешить отправку данных в очереди.
        ////
        //// Параметры:
        ////   timeout:
        ////     Процесс ожидает указанное число секунд timeout, прежде чем отправить оставшиеся
        ////     данные, а затем закрывает сокет.
        //public void Close(int timeout);

        //// Сводка:
        ////     Закрывает подключение System.Net.Sockets.Socket и освобождает все связанные ресурсы.
        //public void Close();

        //// Сводка:
        ////     Устанавливает подключение к удаленному узлу.
        ////
        //// Параметры:
        ////   remoteEP:
        ////     Объект System.Net.EndPoint, представляющий удаленное устройство.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     remoteEP — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий объект, находящийся выше в стеке вызовов, не имеет разрешения на запрошенную
        ////     операцию.
        ////
        ////   T:System.InvalidOperationException:
        ////     Сокет System.Net.Sockets.Socket был переведен в состояние прослушивания путем
        ////     вызова System.Net.Sockets.Socket.Listen(System.Int32).
        //public void Connect(EndPoint remoteEP);

        //// Сводка:
        ////     Устанавливает подключение к удаленному узлу. Узел задается IP-адресом и номером
        ////     порта.
        ////
        //// Параметры:
        ////   address:
        ////     IP-адрес удаленного узла.
        ////
        ////   port:
        ////     Номер порта удаленного узла.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     address — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Недействительный номер порта.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод допустим для сокетов в семействах System.Net.Sockets.AddressFamily.InterNetwork
        ////     или System.Net.Sockets.AddressFamily.InterNetworkV6.
        ////
        ////   T:System.ArgumentException:
        ////     Длина параметра address равна нулю.
        ////
        ////   T:System.InvalidOperationException:
        ////     Сокет System.Net.Sockets.Socket был переведен в состояние прослушивания путем
        ////     вызова System.Net.Sockets.Socket.Listen(System.Int32).
        //public void Connect(IPAddress address, int port);

        //// Сводка:
        ////     Устанавливает подключение к удаленному узлу. Узел задается именем узла и номером
        ////     порта.
        ////
        //// Параметры:
        ////   host:
        ////     Имя удаленного узла.
        ////
        ////   port:
        ////     Номер порта удаленного узла.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     host — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Недействительный номер порта.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод допустим для сокетов в семействах System.Net.Sockets.AddressFamily.InterNetwork
        ////     или System.Net.Sockets.AddressFamily.InterNetworkV6.
        ////
        ////   T:System.InvalidOperationException:
        ////     Сокет System.Net.Sockets.Socket был переведен в состояние прослушивания путем
        ////     вызова System.Net.Sockets.Socket.Listen(System.Int32).
        //public void Connect(string host, int port);

        //// Сводка:
        ////     Устанавливает подключение к удаленному узлу. Узел задается массивом IP-адресов
        ////     и номером порта.
        ////
        //// Параметры:
        ////   addresses:
        ////     IP-адрес удаленного узла.
        ////
        ////   port:
        ////     Номер порта удаленного узла.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     addresses — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Недействительный номер порта.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод допустим для сокетов в семействах System.Net.Sockets.AddressFamily.InterNetwork
        ////     или System.Net.Sockets.AddressFamily.InterNetworkV6.
        ////
        ////   T:System.ArgumentException:
        ////     Длина параметра address равна нулю.
        ////
        ////   T:System.InvalidOperationException:
        ////     Сокет System.Net.Sockets.Socket был переведен в состояние прослушивания путем
        ////     вызова System.Net.Sockets.Socket.Listen(System.Int32).
        //public void Connect(IPAddress[] addresses, int port);

        //// Сводка:
        ////     Начинает выполнение асинхронного запроса для подключения к удаленному узлу.
        ////
        //// Параметры:
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs для использования в данной асинхронной
        ////     операции сокета.
        ////
        //// Возврат:
        ////     true, если операция ввода-вывода находится в состоянии ожидания. По завершении
        ////     операции создается событие System.Net.Sockets.SocketAsyncEventArgs.Completed
        ////     в параметре e. false, если операция ввода-вывода завершена синхронно. В данном
        ////     случае событие System.Net.Sockets.SocketAsyncEventArgs.Completed на параметре
        ////     e не будет создано и объект e, передаваемый как параметр, можно изучить сразу
        ////     после получения результатов вызова метода для извлечения результатов операции.
        ////
        //// Исключения:
        ////   T:System.ArgumentException:
        ////     Аргумент является недопустимым. Это исключение возникает, если задано несколько
        ////     буферов, свойство System.Net.Sockets.SocketAsyncEventArgs.BufferList не имеет
        ////     значение "null".
        ////
        ////   T:System.ArgumentNullException:
        ////     Параметр e и System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint не могут
        ////     иметь значение NULL.
        ////
        ////   T:System.InvalidOperationException:
        ////     System.Net.Sockets.Socket ведет прослушивание или работа с сокетом уже выполняется
        ////     с использованием объекта System.Net.Sockets.SocketAsyncEventArgs, указанного
        ////     параметром e.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows XP и более поздних версиях. Это исключение
        ////     возникает также в том случае, если локальная конечная точка и объект System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint
        ////     не принадлежат к одному семейству адресов.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий объект, находящийся выше в стеке вызовов, не имеет разрешения на запрошенную
        ////     операцию.
        //public bool ConnectAsync(SocketAsyncEventArgs e);

        //// Сводка:
        ////     Закрывает подключение к сокету и позволяет повторно его использовать.
        ////
        //// Параметры:
        ////   reuseSocket:
        ////     Значение true, если этот сокет может быть повторно использован после закрытия
        ////     текущего подключения; в противном случае — значение false.
        ////
        //// Исключения:
        ////   T:System.PlatformNotSupportedException:
        ////     Для этого метода необходима операционная система Windows 2000 или более ранняя
        ////     версия или будет создано исключение.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public void Disconnect(bool reuseSocket);

        //// Сводка:
        ////     Начинает выполнение асинхронного запроса для отключения от удаленной конечной
        ////     точки.
        ////
        //// Параметры:
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs для использования в данной асинхронной
        ////     операции сокета.
        ////
        //// Возврат:
        ////     true, если операция ввода-вывода находится в состоянии ожидания. По завершении
        ////     операции создается событие System.Net.Sockets.SocketAsyncEventArgs.Completed
        ////     в параметре e. false, если операция ввода-вывода завершена синхронно. В данном
        ////     случае событие System.Net.Sockets.SocketAsyncEventArgs.Completed на параметре
        ////     e не будет создано и объект e, передаваемый как параметр, можно изучить сразу
        ////     после получения результатов вызова метода для извлечения результатов операции.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     Параметр e не может иметь значение null.
        ////
        ////   T:System.InvalidOperationException:
        ////     Операция сокета уже выполнялась с использованием объекта System.Net.Sockets.SocketAsyncEventArgs,
        ////     указанного в параметре e.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows XP и более поздних версиях.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public bool DisconnectAsync(SocketAsyncEventArgs e);

        //// Сводка:
        ////     Освобождает все ресурсы, используемые текущим экземпляром класса System.Net.Sockets.Socket.
        //public void Dispose();
        ////
        //// Сводка:
        ////     Дублирует ссылку сокета для конечного процесса и закрывает сокет для этого процесса.
        ////
        //// Параметры:
        ////   targetProcessId:
        ////     Идентификатор конечного процесса, в котором создается дубликат ссылки сокета.
        ////
        //// Возврат:
        ////     Ссылка сокета, передаваемая в конечный процесс.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Параметр targetProcessID не является допустимым идентификатором процесса. -или-
        ////     Дубликат ссылки сокета не создан.
        //public SocketInformation DuplicateAndClose(int targetProcessId);

        //// Сводка:
        ////     Асинхронно принимает входящие попытки подключения и создает новый объект System.Net.Sockets.Socket
        ////     для связи с удаленным узлом. Этот метод возвращает буфер, который содержит начальные
        ////     данные и число переданных байтов.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который содержит переданные байты.
        ////
        ////   bytesTransferred:
        ////     Количество переданных байтов.
        ////
        ////   asyncResult:
        ////     Объект System.IAsyncResult, в котором хранятся сведения о состоянии для этой
        ////     асинхронной операции, а также любые данные, определенные пользователем.
        ////
        //// Возврат:
        ////     Объект System.Net.Sockets.Socket для связи с удаленным узлом.
        ////
        //// Исключения:
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows NT.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.ArgumentNullException:
        ////     Параметр asyncResult пуст.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был создан вызовом метода System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Ранее был вызван метод System.Net.Sockets.Socket.EndAccept(System.IAsyncResult).
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к объекту System.Net.Sockets.Socket.
        //public Socket EndAccept(out byte[] buffer, out int bytesTransferred, IAsyncResult asyncResult);

        //// Сводка:
        ////     Асинхронно принимает входящие попытки подключения и создает новый объект System.Net.Sockets.Socket
        ////     для связи с удаленным узлом. Этот метод возвращает буфер, который содержит начальные
        ////     данные для передачи.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который содержит переданные байты.
        ////
        ////   asyncResult:
        ////     Объект System.IAsyncResult, в котором хранятся сведения о состоянии для этой
        ////     асинхронной операции, а также любые данные, определенные пользователем.
        ////
        //// Возврат:
        ////     Объект System.Net.Sockets.Socket для связи с удаленным узлом.
        ////
        //// Исключения:
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows NT.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.ArgumentNullException:
        ////     Параметр asyncResult пуст.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был создан вызовом метода System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Ранее был вызван метод System.Net.Sockets.Socket.EndAccept(System.IAsyncResult).
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к объекту System.Net.Sockets.Socket.
        //public Socket EndAccept(out byte[] buffer, IAsyncResult asyncResult);

        //// Сводка:
        ////     Асинхронно принимает входящие попытки подключения и создает новый объект System.Net.Sockets.Socket
        ////     для связи с удаленным узлом.
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, в котором хранятся сведения о состоянии для этой
        ////     асинхронной операции, а также любые данные, определенные пользователем.
        ////
        //// Возврат:
        ////     ОбъектSystem.Net.Sockets.Socket для связи с удаленным узлом.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     asyncResult — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был создан вызовом метода System.Net.Sockets.Socket.BeginAccept(System.AsyncCallback,System.Object).
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. Дополнительные сведения см. в
        ////     разделе "Примечания".
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.InvalidOperationException:
        ////     Ранее был вызван метод System.Net.Sockets.Socket.EndAccept(System.IAsyncResult).
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows NT.
        //public Socket EndAccept(IAsyncResult asyncResult);

        //// Сводка:
        ////     Завершает ожидающий асинхронный запрос на подключение.
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, в котором хранятся сведения о состоянии и любые данные,
        ////     определенные пользователем, для этой асинхронной операции.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     asyncResult — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был возвращен вызовом метода System.Net.Sockets.Socket.BeginConnect(System.Net.EndPoint,System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Метод System.Net.Sockets.Socket.EndConnect(System.IAsyncResult) был ранее вызван
        ////     для асинхронного подключения.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public void EndConnect(IAsyncResult asyncResult);

        //// Сводка:
        ////     Завершает ожидающий асинхронный запрос на разъединение.
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, в котором хранятся сведения о состоянии и любые данные,
        ////     определенные пользователем, для этой асинхронной операции.
        ////
        //// Исключения:
        ////   T:System.NotSupportedException:
        ////     Используется операционная система Windows 2000 или более ранняя версия, а для
        ////     этого метода необходима операционная система Windows XP.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.ArgumentNullException:
        ////     asyncResult — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был возвращен вызовом метода System.Net.Sockets.Socket.BeginDisconnect(System.Boolean,System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Метод System.Net.Sockets.Socket.EndDisconnect(System.IAsyncResult) был ранее
        ////     вызван для асинхронного подключения.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.Net.WebException:
        ////     Истекло время ожидания для запроса на разъединение.
        //public void EndDisconnect(IAsyncResult asyncResult);

        //// Сводка:
        ////     Завершает отложенное асинхронное чтение.
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, в котором хранятся сведения о состоянии и любые данные,
        ////     определенные пользователем, для этой асинхронной операции.
        ////
        ////   errorCode:
        ////     Объект System.Net.Sockets.SocketError, содержащий ошибку сокета.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     asyncResult — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был возвращен вызовом метода System.Net.Sockets.Socket.BeginReceive(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Метод System.Net.Sockets.Socket.EndReceive(System.IAsyncResult) был ранее вызван
        ////     для асинхронного чтения.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int EndReceive(IAsyncResult asyncResult, out SocketError errorCode);

        //// Сводка:
        ////     Завершает отложенное асинхронное чтение.
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, в котором хранятся сведения о состоянии и любые данные,
        ////     определенные пользователем, для этой асинхронной операции.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     asyncResult — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был возвращен вызовом метода System.Net.Sockets.Socket.BeginReceive(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Метод System.Net.Sockets.Socket.EndReceive(System.IAsyncResult) был ранее вызван
        ////     для асинхронного чтения.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int EndReceive(IAsyncResult asyncResult);

        //// Сводка:
        ////     Завершает отложенное асинхронное чтение с определенной конечной точки.
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, в котором хранятся сведения о состоянии и любые данные,
        ////     определенные пользователем, для этой асинхронной операции.
        ////
        ////   endPoint:
        ////     Источник System.Net.EndPoint.
        ////
        //// Возврат:
        ////     Количество полученных байтов, если операция успешно выполнена. Возвращает значение
        ////     0, если операция завершилась неудачей.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     asyncResult — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был возвращен вызовом метода System.Net.Sockets.Socket.BeginReceiveFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint@,System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Метод System.Net.Sockets.Socket.EndReceiveFrom(System.IAsyncResult,System.Net.EndPoint@)
        ////     был ранее вызван для асинхронного чтения.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int EndReceiveFrom(IAsyncResult asyncResult, ref EndPoint endPoint);

        //// Сводка:
        ////     Завершает отложенное асинхронное чтение с определенной конечной точки. Этот метод
        ////     также показывает больше информации о пакете, чем метод System.Net.Sockets.Socket.EndReceiveFrom(System.IAsyncResult,System.Net.EndPoint@).
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, в котором хранятся сведения о состоянии и любые данные,
        ////     определенные пользователем, для этой асинхронной операции.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений перечисления System.Net.Sockets.SocketFlags для
        ////     принятого пакета.
        ////
        ////   endPoint:
        ////     Источник System.Net.EndPoint.
        ////
        ////   ipPacketInformation:
        ////     Объект System.Net.IPAddress и интерфейс полученного пакета.
        ////
        //// Возврат:
        ////     Количество полученных байтов, если операция успешно выполнена. Возвращает значение
        ////     0, если операция завершилась неудачей.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     asyncResult равно null -или- endPoint — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был возвращен вызовом метода System.Net.Sockets.Socket.BeginReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint@,System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Метод System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)
        ////     был ранее вызван для асинхронного чтения.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int EndReceiveMessageFrom(IAsyncResult asyncResult, ref SocketFlags socketFlags, ref EndPoint endPoint, out IPPacketInformation ipPacketInformation);

        //// Сводка:
        ////     Завершает отложенную операцию асинхронной передачи.
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, хранящий сведения о состоянии этой асинхронной операции.
        ////
        //// Возврат:
        ////     Если операция завершилась успешно — значение количества байтов, переданных в
        ////     объект System.Net.Sockets.Socket; в противном случае — ошибка, указывающая на
        ////     недопустимость объекта System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     asyncResult — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был возвращен вызовом метода System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Метод System.Net.Sockets.Socket.EndSend(System.IAsyncResult) был ранее вызван
        ////     для асинхронной передачи.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int EndSend(IAsyncResult asyncResult);

        //// Сводка:
        ////     Завершает отложенную операцию асинхронной передачи.
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, хранящий сведения о состоянии этой асинхронной операции.
        ////
        ////   errorCode:
        ////     Объект System.Net.Sockets.SocketError, содержащий ошибку сокета.
        ////
        //// Возврат:
        ////     Если операция завершилась успешно — значение количества байтов, переданных в
        ////     объект System.Net.Sockets.Socket; в противном случае — ошибка, указывающая на
        ////     недопустимость объекта System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     asyncResult — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был возвращен вызовом метода System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Метод System.Net.Sockets.Socket.EndSend(System.IAsyncResult) был ранее вызван
        ////     для асинхронной передачи.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int EndSend(IAsyncResult asyncResult, out SocketError errorCode);

        //// Сводка:
        ////     Завершает отложенную операцию асинхронной передачи файла.
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, хранящий сведения о состоянии этой асинхронной операции.
        ////
        //// Исключения:
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows NT.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.ArgumentNullException:
        ////     Параметр asyncResult пуст.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был возвращен вызовом метода System.Net.Sockets.Socket.BeginSendFile(System.String,System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Метод System.Net.Sockets.Socket.EndSendFile(System.IAsyncResult) был ранее вызван
        ////     для асинхронной передачи объекта System.Net.Sockets.Socket.BeginSendFile(System.String,System.AsyncCallback,System.Object).
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. См. ниже примeчания к данному
        ////     разделу.
        //public void EndSendFile(IAsyncResult asyncResult);

        //// Сводка:
        ////     Завершает отложенную операцию асинхронной отправки в определенное местоположение.
        ////
        //// Параметры:
        ////   asyncResult:
        ////     Объект System.IAsyncResult, в котором хранятся сведения о состоянии и любые данные,
        ////     определенные пользователем, для этой асинхронной операции.
        ////
        //// Возврат:
        ////     Если операция завершилась успешно — значение количества отправленных байтов;
        ////     в противном случае — ошибка, указывающая на недопустимость объекта System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     asyncResult — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр asyncResult не был возвращен вызовом метода System.Net.Sockets.Socket.BeginSendTo(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.Net.EndPoint,System.AsyncCallback,System.Object).
        ////
        ////   T:System.InvalidOperationException:
        ////     Метод System.Net.Sockets.Socket.EndSendTo(System.IAsyncResult) был ранее вызван
        ////     для асинхронной передачи.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int EndSendTo(IAsyncResult asyncResult);

        //// Сводка:
        ////     Возвращает значение указанного параметра System.Net.Sockets.Socket в массиве.
        ////
        //// Параметры:
        ////   optionLevel:
        ////     Одно из значений System.Net.Sockets.SocketOptionLevel.
        ////
        ////   optionName:
        ////     Одно из значений System.Net.Sockets.SocketOptionName.
        ////
        ////   optionLength:
        ////     Длина ожидаемого возвращаемого значения, указанная в байтах.
        ////
        //// Возврат:
        ////     Массив типа System.Byte, который содержит значение параметра сокета.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. -или- В приложениях .NET Compact
        ////     Framework для размера буферного пространства Windows CE установлено по умолчанию
        ////     значение 32768 байт. Можно изменить размер буферного пространства сокета, вызвав
        ////     свойство Overload:System.Net.Sockets.Socket.SetSocketOption.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public byte[] GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionLength);

        //// Сводка:
        ////     Возвращает значение указанного параметра System.Net.Sockets.Socket, представленного
        ////     в виде объекта.
        ////
        //// Параметры:
        ////   optionLevel:
        ////     Одно из значений System.Net.Sockets.SocketOptionLevel.
        ////
        ////   optionName:
        ////     Одно из значений System.Net.Sockets.SocketOptionName.
        ////
        //// Возврат:
        ////     Объект, который представляет значение параметра. Когда для параметра optionName
        ////     установлено значение System.Net.Sockets.SocketOptionName.Linger, возвращаемое
        ////     значение является экземпляром класса System.Net.Sockets.LingerOption. Когда для
        ////     параметра optionName задано значение System.Net.Sockets.SocketOptionName.AddMembership
        ////     или System.Net.Sockets.SocketOptionName.DropMembership, возвращаемое значение
        ////     является экземпляром класса System.Net.Sockets.MulticastOption. Когда для параметра
        ////     optionName задано любое другое значение, возвращаемое значение является целым
        ////     числом.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. -или- Для параметра optionName
        ////     было установлено неподдерживаемое значение System.Net.Sockets.SocketOptionName.MaxConnections.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public object GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName);

        //// Сводка:
        ////     Возвращает указанное значение параметра System.Net.Sockets.Socket, представленного
        ////     в виде байтового массива.
        ////
        //// Параметры:
        ////   optionLevel:
        ////     Одно из значений System.Net.Sockets.SocketOptionLevel.
        ////
        ////   optionName:
        ////     Одно из значений System.Net.Sockets.SocketOptionName.
        ////
        ////   optionValue:
        ////     Массив типа System.Byte, который используется для отправки значения параметра.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. -или- В приложениях .NET Compact
        ////     Framework для размера буферного пространства Windows CE установлено по умолчанию
        ////     значение 32768 байт. Можно изменить размер буферного пространства сокета, вызвав
        ////     свойство Overload:System.Net.Sockets.Socket.SetSocketOption.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public void GetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, byte[] optionValue);

        //// Сводка:
        ////     Задает низкоуровневые операционные режимы для объекта System.Net.Sockets.Socket,
        ////     используя перечисление System.Net.Sockets.IOControlCode, чтобы указать коды элементов
        ////     управления.
        ////
        //// Параметры:
        ////   ioControlCode:
        ////     Значение System.Net.Sockets.IOControlCode, задающее код элемента управления для
        ////     выполняемой операции.
        ////
        ////   optionInValue:
        ////     Массив типа System.Byte, который содержит входные данные, необходимые для операции.
        ////
        ////   optionOutValue:
        ////     Массив типа System.Byte, который содержит выходные данные, возвращенные операцией.
        ////
        //// Возврат:
        ////     Число байтов в параметре optionOutValue.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.InvalidOperationException:
        ////     Была сделана попытка изменения блокирующего режима без использования свойства
        ////     System.Net.Sockets.Socket.Blocking.
        //public int IOControl(IOControlCode ioControlCode, byte[] optionInValue, byte[] optionOutValue);

        //// Сводка:
        ////     Задает низкоуровневые операционные режимы для объекта System.Net.Sockets.Socket,
        ////     используя цифровые коды элементов управления.
        ////
        //// Параметры:
        ////   ioControlCode:
        ////     Значение System.Int32, задающее код элемента управления для выполняемой операции.
        ////
        ////   optionInValue:
        ////     Массив System.Byte, который содержит входные данные, необходимые для операции.
        ////
        ////   optionOutValue:
        ////     Массив System.Byte, который содержит выходные данные, необходимые для операции.
        ////
        //// Возврат:
        ////     Число байтов в параметре optionOutValue.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.InvalidOperationException:
        ////     Была сделана попытка изменения блокирующего режима без использования свойства
        ////     System.Net.Sockets.Socket.Blocking.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий оператор в стеке вызовов не имеет необходимых разрешений.
        //public int IOControl(int ioControlCode, byte[] optionInValue, byte[] optionOutValue);

        //// Сводка:
        ////     Устанавливает объект System.Net.Sockets.Socket в состояние прослушивания.
        ////
        //// Параметры:
        ////   backlog:
        ////     Максимальная длина очереди ожидающих подключений.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public void Listen(int backlog);

        //// Сводка:
        ////     Определяет состояние объекта System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   microSeconds:
        ////     Время ожидания ответа, заданное в микросекундах.
        ////
        ////   mode:
        ////     Одно из значений System.Net.Sockets.SelectMode.
        ////
        //// Возврат:
        ////     Состояние объекта System.Net.Sockets.Socket, основанное на значении режима опроса,
        ////     переданного в параметре mode. Режим Возвращаемое значение System.Net.Sockets.SelectMode.SelectReadЗначение
        ////     true, если был вызван метод System.Net.Sockets.Socket.Listen(System.Int32) и
        ////     подключение отложено; -или- Значение true, если данные доступны для чтения; -или-
        ////     Значение true, если подключение закрыто, сброшено или завершено. В противном
        ////     случае, возвращает значение false. System.Net.Sockets.SelectMode.SelectWriteЗначение
        ////     true, если обработка метода System.Net.Sockets.Socket.Connect(System.Net.EndPoint)
        ////     и подключения завершились успешно; -или- Значение true, если данные могут быть
        ////     посланы; В противном случае, возвращает значение false. System.Net.Sockets.SelectMode.SelectErrorЗначение
        ////     true, если не блокируется обработка метода System.Net.Sockets.Socket.Connect(System.Net.EndPoint)
        ////     и попытка подключения завершилась неудачей; -или- Значение true, если не установлен
        ////     объект System.Net.Sockets.SocketOptionName.OutOfBandInline и доступны экстренные
        ////     данные; В противном случае, возвращает значение false.
        ////
        //// Исключения:
        ////   T:System.NotSupportedException:
        ////     Параметр mode не является одним из значений System.Net.Sockets.SelectMode.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. См. примечания ниже.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public bool Poll(int microSeconds, SelectMode mode);

        //// Сводка:
        ////     Получает данные из связанного объекта System.Net.Sockets.Socket в приемный буфер,
        ////     используя заданный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий оператор в стеке вызовов не имеет необходимых разрешений.
        //public int Receive(byte[] buffer, SocketFlags socketFlags);

        //// Сводка:
        ////     Получает данные из связанного объекта System.Net.Sockets.Socket в список приемных
        ////     буферов, используя заданный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffers:
        ////     Список объектов System.ArraySegment`1 типа System.Byte, содержащих полученные
        ////     данные.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   errorCode:
        ////     Объект System.Net.Sockets.SocketError, содержащий ошибку сокета.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffers — null. -или- Значение buffers. Отсчет равен нулю.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode);

        //// Сводка:
        ////     Получает данные из связанного объекта System.Net.Sockets.Socket в список приемных
        ////     буферов, используя заданный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffers:
        ////     Список объектов System.ArraySegment`1 типа System.Byte, содержащих полученные
        ////     данные.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffers — null. -или- Значение buffers. Отсчет равен нулю.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Receive(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags);

        //// Сводка:
        ////     Получает указанное число байтов данных из связанного объекта System.Net.Sockets.Socket
        ////     в приемный буфер, используя заданный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        ////   size:
        ////     Количество байтов, которые необходимо получить.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение size превышает размер параметра buffer.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий оператор в стеке вызовов не имеет необходимых разрешений.
        //public int Receive(byte[] buffer, int size, SocketFlags socketFlags);

        //// Сводка:
        ////     Получает данные из связанного объекта System.Net.Sockets.Socket в список приемных
        ////     буферов.
        ////
        //// Параметры:
        ////   buffers:
        ////     Список объектов System.ArraySegment`1 типа System.Byte, содержащих полученные
        ////     данные.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     Параметр buffer имеет значение null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Receive(IList<ArraySegment<byte>> buffers);

        //// Сводка:
        ////     Возвращает данные из связанного объекта System.Net.Sockets.Socket в приемный
        ////     буфер.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий оператор в стеке вызовов не имеет необходимых разрешений.
        //public int Receive(byte[] buffer);

        //// Сводка:
        ////     Получает указанное число байтов данных из связанного объекта System.Net.Sockets.Socket
        ////     в приемный буфер с указанной позиции смещения, используя заданный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив объекта типа System.Byte, который является местом хранения полученных
        ////     данных.
        ////
        ////   offset:
        ////     Место в объекте buffer, выделенное для хранения принимаемых данных.
        ////
        ////   size:
        ////     Количество байтов, которые необходимо получить.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра offset.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     socketFlags — недопустимое сочетание значений. -или- Свойство System.Net.Sockets.Socket.LocalEndPoint
        ////     не задано. -или- Произошла ошибка операционной системы при доступе к System.Net.Sockets.Socket.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий оператор в стеке вызовов не имеет необходимых разрешений.
        //public int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags);

        //// Сводка:
        ////     Получает данные из связанного объекта System.Net.Sockets.Socket в приемный буфер,
        ////     используя заданный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        ////   offset:
        ////     Позиция в параметре buffer для хранения полученных данных.
        ////
        ////   size:
        ////     Количество байтов, которые необходимо получить.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   errorCode:
        ////     Объект System.Net.Sockets.SocketError, содержащий ошибку сокета.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра offset.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     socketFlags — недопустимое сочетание значений. -или- Свойство System.Net.Sockets.Socket.LocalEndPoint
        ////     не задано. -или- Произошла ошибка операционной системы при доступе к System.Net.Sockets.Socket.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий оператор в стеке вызовов не имеет необходимых разрешений.
        //public int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode);

        //// Сводка:
        ////     Начинает выполнение асинхронного запроса, чтобы получить данные из подключенного
        ////     объекта System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs для использования в данной асинхронной
        ////     операции сокета.
        ////
        //// Возврат:
        ////     true, если операция ввода-вывода находится в состоянии ожидания. По завершении
        ////     операции создается событие System.Net.Sockets.SocketAsyncEventArgs.Completed
        ////     в параметре e. false, если операция ввода-вывода завершена синхронно. В данном
        ////     случае событие System.Net.Sockets.SocketAsyncEventArgs.Completed на параметре
        ////     e не будет создано и объект e, передаваемый как параметр, можно изучить сразу
        ////     после получения результатов вызова метода для извлечения результатов операции.
        ////
        //// Исключения:
        ////   T:System.ArgumentException:
        ////     Аргумент был недопустимым. Свойства System.Net.Sockets.SocketAsyncEventArgs.Buffer
        ////     или System.Net.Sockets.SocketAsyncEventArgs.BufferList на параметре e должны
        ////     ссылаться на допустимые буферы. Может быть установлено одно из этих свойств,
        ////     но нельзя одновременно устанавливать оба свойства.
        ////
        ////   T:System.InvalidOperationException:
        ////     Операция сокета уже выполнялась с использованием объекта System.Net.Sockets.SocketAsyncEventArgs,
        ////     указанного в параметре e.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows XP и более поздних версиях.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public bool ReceiveAsync(SocketAsyncEventArgs e);

        //// Сводка:
        ////     Принимает датаграмму в буфер данных и сохраняет конечную точку.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив объекта типа System.Byte, который является местом хранения полученных
        ////     данных.
        ////
        ////   remoteEP:
        ////     Переданный по ссылке объект System.Net.EndPoint, представляющий удаленный сервер.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий оператор в стеке вызовов не имеет необходимых разрешений.
        //public int ReceiveFrom(byte[] buffer, ref EndPoint remoteEP);

        //// Сводка:
        ////     Принимает датаграмму в буфер данных, используя заданный объект System.Net.Sockets.SocketFlags,
        ////     и сохраняет конечную точку.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, который является местоположением памяти для полученных
        ////     данных.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   remoteEP:
        ////     Переданный по ссылке объект System.Net.EndPoint, представляющий удаленный сервер.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий оператор в стеке вызовов не имеет необходимых разрешений.
        //public int ReceiveFrom(byte[] buffer, SocketFlags socketFlags, ref EndPoint remoteEP);

        //// Сводка:
        ////     Получает указанное число байтов во входной буфер, используя заданный объект System.Net.Sockets.SocketFlags,
        ////     и сохраняет конечную точку.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив объекта типа System.Byte, который является местом хранения полученных
        ////     данных.
        ////
        ////   size:
        ////     Количество байтов, которые необходимо получить.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   remoteEP:
        ////     Переданный по ссылке объект System.Net.EndPoint, представляющий удаленный сервер.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра size меньше 0. -или- Значение size превышает длину buffer.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     socketFlags — недопустимое сочетание значений. -или- Свойство System.Net.Sockets.Socket.LocalEndPoint
        ////     не задано. -или- Произошла ошибка операционной системы при доступе к System.Net.Sockets.Socket.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий оператор в стеке вызовов не имеет необходимых разрешений.
        //public int ReceiveFrom(byte[] buffer, int size, SocketFlags socketFlags, ref EndPoint remoteEP);

        //// Сводка:
        ////     Получает указанное число байтов данных в заданном расположении буфера данных
        ////     с использованием определенного параметра System.Net.Sockets.SocketFlags и сохраняет
        ////     конечную точку.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив объекта типа System.Byte, который является местом хранения полученных
        ////     данных.
        ////
        ////   offset:
        ////     Позиция в параметре buffer для хранения полученных данных.
        ////
        ////   size:
        ////     Количество байтов, которые необходимо получить.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   remoteEP:
        ////     Переданный по ссылке объект System.Net.EndPoint, представляющий удаленный сервер.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра смещения.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     socketFlags — недопустимое сочетание значений. -или- Свойство System.Net.Sockets.Socket.LocalEndPoint
        ////     не задано. -или- Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int ReceiveFrom(byte[] buffer, int offset, int size, SocketFlags socketFlags, ref EndPoint remoteEP);

        //// Сводка:
        ////     Начинает выполнение асинхронного приема данных с указанного сетевого устройства.
        ////
        //// Параметры:
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs для использования в данной асинхронной
        ////     операции сокета.
        ////
        //// Возврат:
        ////     true, если операция ввода-вывода находится в состоянии ожидания. По завершении
        ////     операции создается событие System.Net.Sockets.SocketAsyncEventArgs.Completed
        ////     в параметре e. false, если операция ввода-вывода завершена синхронно. В данном
        ////     случае событие System.Net.Sockets.SocketAsyncEventArgs.Completed на параметре
        ////     e не будет создано и объект e, передаваемый как параметр, можно изучить сразу
        ////     после получения результатов вызова метода для извлечения результатов операции.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint не может иметь
        ////     значение "null".
        ////
        ////   T:System.InvalidOperationException:
        ////     Операция сокета уже выполнялась с использованием объекта System.Net.Sockets.SocketAsyncEventArgs,
        ////     указанного в параметре e.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows XP и более поздних версиях.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public bool ReceiveFromAsync(SocketAsyncEventArgs e);

        //// Сводка:
        ////     Получает указанное число байтов данных в указанное расположение буфера данных
        ////     с помощью заданного System.Net.Sockets.SocketFlags и сохраняет конечную точку
        ////     и сведения о пакете.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив объекта типа System.Byte, который является местом хранения полученных
        ////     данных.
        ////
        ////   offset:
        ////     Позиция в параметре buffer для хранения полученных данных.
        ////
        ////   size:
        ////     Количество байтов, которые необходимо получить.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   remoteEP:
        ////     Переданный по ссылке объект System.Net.EndPoint, представляющий удаленный сервер.
        ////
        ////   ipPacketInformation:
        ////     System.Net.Sockets.IPPacketInformation сохраняет адрес и сведения об интерфейсе.
        ////
        //// Возврат:
        ////     Количество полученных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. - или - remoteEP — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра смещения.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     socketFlags — недопустимое сочетание значений. -или- Свойство System.Net.Sockets.Socket.LocalEndPoint
        ////     не задано. -или- Платформа .NET Framework выполняется на 64-разрядном процессоре
        ////     AMD. -или- Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.NotSupportedException:
        ////     Используется операционная система Windows 2000 или более ранняя версия, а для
        ////     этого метода необходима операционная система Windows XP.
        //public int ReceiveMessageFrom(byte[] buffer, int offset, int size, ref SocketFlags socketFlags, ref EndPoint remoteEP, out IPPacketInformation ipPacketInformation);

        //// Сводка:
        ////     Начинает асинхронный прием заданного числа байтов данных в указанное место буфера
        ////     данных, используя заданный объект System.Net.Sockets.SocketAsyncEventArgs.SocketFlags,
        ////     а также сохраняет конечную точку и информацию пакета.
        ////
        //// Параметры:
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs для использования в данной асинхронной
        ////     операции сокета.
        ////
        //// Возврат:
        ////     true, если операция ввода-вывода находится в состоянии ожидания. По завершении
        ////     операции создается событие System.Net.Sockets.SocketAsyncEventArgs.Completed
        ////     в параметре e. false, если операция ввода-вывода завершена синхронно. В данном
        ////     случае событие System.Net.Sockets.SocketAsyncEventArgs.Completed на параметре
        ////     e не будет создано и объект e, передаваемый как параметр, можно изучить сразу
        ////     после получения результатов вызова метода для извлечения результатов операции.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint не может иметь
        ////     значение "null".
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows XP и более поздних версиях.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public bool ReceiveMessageFromAsync(SocketAsyncEventArgs e);

        //// Сводка:
        ////     Передает данные в подключенный объект System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, содержащий данные для отправки.
        ////
        //// Возврат:
        ////     Количество байтов, отправленных в System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Send(byte[] buffer);

        //// Сводка:
        ////     Посылает указанное число байтов данных на подключенный объект System.Net.Sockets.Socket,
        ////     используя заданный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, содержащий данные для отправки.
        ////
        ////   size:
        ////     Количество байтов для отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        //// Возврат:
        ////     Количество байтов, отправленных в System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра size меньше 0 или превышает размер буфера.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     socketFlags — недопустимое сочетание значений. -или- Сбой операционной системы
        ////     при доступе к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Send(byte[] buffer, int size, SocketFlags socketFlags);

        //// Сводка:
        ////     Передает данные в подключенный объект System.Net.Sockets.Socket, используя заданный
        ////     объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, содержащий данные для отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        //// Возврат:
        ////     Количество байтов, отправленных в System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Send(byte[] buffer, SocketFlags socketFlags);

        //// Сводка:
        ////     Посылает указанное число байтов данных на подключенный объект System.Net.Sockets.Socket,
        ////     начиная с указанного смещения и используя заданный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, содержащий данные для отправки.
        ////
        ////   offset:
        ////     Положение в буфере данных, с которого начинается отправка данных.
        ////
        ////   size:
        ////     Количество байтов для отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   errorCode:
        ////     Объект System.Net.Sockets.SocketError, содержащий ошибку сокета.
        ////
        //// Возврат:
        ////     Количество байтов, отправленных в System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра offset.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     socketFlags — недопустимое сочетание значений. -или- Произошла ошибка операционной
        ////     системы при доступе к System.Net.Sockets.Socket.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags, out SocketError errorCode);

        //// Сводка:
        ////     Отправляет указанное количество байтов данных в подключенный System.Net.Sockets.Socket,
        ////     начиная с заданного смещения и используя заданный параметр System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, содержащий данные для отправки.
        ////
        ////   offset:
        ////     Положение в буфере данных, с которого начинается отправка данных.
        ////
        ////   size:
        ////     Количество байтов для отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        //// Возврат:
        ////     Количество байтов, отправленных в System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра offset.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     socketFlags — недопустимое сочетание значений. -или- Произошла ошибка операционной
        ////     системы при доступе к System.Net.Sockets.Socket.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags);

        //// Сводка:
        ////     Отправляет набор буферов в список на подключенный объект System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   buffers:
        ////     Список объектов System.ArraySegment`1 типа System.Byte, содержащих данные для
        ////     отправки.
        ////
        //// Возврат:
        ////     Количество байтов, отправленных в System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffers — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр buffers пуст.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету. См. ниже примeчания к данному
        ////     разделу.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Send(IList<ArraySegment<byte>> buffers);

        //// Сводка:
        ////     Отправляет набор буферов в список на подключенный объект System.Net.Sockets.Socket,
        ////     используя указанный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffers:
        ////     Список объектов System.ArraySegment`1 типа System.Byte, содержащих данные для
        ////     отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   errorCode:
        ////     Объект System.Net.Sockets.SocketError, содержащий ошибку сокета.
        ////
        //// Возврат:
        ////     Количество байтов, отправленных в System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffers — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр buffers пуст.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags, out SocketError errorCode);

        //// Сводка:
        ////     Отправляет набор буферов в список на подключенный объект System.Net.Sockets.Socket,
        ////     используя указанный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffers:
        ////     Список объектов System.ArraySegment`1 типа System.Byte, содержащих данные для
        ////     отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        //// Возврат:
        ////     Количество байтов, отправленных в System.Net.Sockets.Socket.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffers — null.
        ////
        ////   T:System.ArgumentException:
        ////     Параметр buffers пуст.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int Send(IList<ArraySegment<byte>> buffers, SocketFlags socketFlags);

        //// Сводка:
        ////     Выполняет асинхронную передачу данных на подключенный объект System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs для использования в данной асинхронной
        ////     операции сокета.
        ////
        //// Возврат:
        ////     true, если операция ввода-вывода находится в состоянии ожидания. По завершении
        ////     операции создается событие System.Net.Sockets.SocketAsyncEventArgs.Completed
        ////     в параметре e. false, если операция ввода-вывода завершена синхронно. В данном
        ////     случае событие System.Net.Sockets.SocketAsyncEventArgs.Completed на параметре
        ////     e не будет создано и объект e, передаваемый как параметр, можно изучить сразу
        ////     после получения результатов вызова метода для извлечения результатов операции.
        ////
        //// Исключения:
        ////   T:System.ArgumentException:
        ////     Свойства System.Net.Sockets.SocketAsyncEventArgs.Buffer или System.Net.Sockets.SocketAsyncEventArgs.BufferList
        ////     на параметре e должны ссылаться на допустимые буферы. Может быть установлено
        ////     одно из этих свойств, но нельзя одновременно устанавливать оба свойства.
        ////
        ////   T:System.InvalidOperationException:
        ////     Операция сокета уже выполнялась с использованием объекта System.Net.Sockets.SocketAsyncEventArgs,
        ////     указанного в параметре e.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows XP и более поздних версиях.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Объект System.Net.Sockets.Socket уже не подключен или он был получен посредством
        ////     метода System.Net.Sockets.Socket.Accept, System.Net.Sockets.Socket.AcceptAsync(System.Net.Sockets.SocketAsyncEventArgs)
        ////     или Overload:System.Net.Sockets.Socket.BeginAccept.
        //public bool SendAsync(SocketAsyncEventArgs e);

        //// Сводка:
        ////     Отправляет файл fileName на подключенный объект System.Net.Sockets.Socket, используя
        ////     флаг передачи System.Net.Sockets.TransmitFileOptions.UseDefaultWorkerThread.
        ////
        //// Параметры:
        ////   fileName:
        ////     Параметр типа System.String, содержащий имя отправляемого файла и путь к нему.
        ////     Этот параметр может иметь значение null.
        ////
        //// Исключения:
        ////   T:System.NotSupportedException:
        ////     Сокет не подключен к удаленному узлу.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.InvalidOperationException:
        ////     Объект System.Net.Sockets.Socket не находится в режиме блокировки и не может
        ////     принять этот синхронный вызов.
        ////
        ////   T:System.IO.FileNotFoundException:
        ////     Файл fileName не найден.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public void SendFile(string fileName);

        //// Сводка:
        ////     Отправляет файл fileName и буферы данных в подключенный объект System.Net.Sockets.Socket,
        ////     используя указанное значение System.Net.Sockets.TransmitFileOptions.
        ////
        //// Параметры:
        ////   fileName:
        ////     Параметр типа System.String, содержащий имя отправляемого файла и путь к нему.
        ////     Этот параметр может иметь значение null.
        ////
        ////   preBuffer:
        ////     Массив System.Byte, содержащий данные, отправляемые перед передачей файла. Этот
        ////     параметр может иметь значение null.
        ////
        ////   postBuffer:
        ////     Массив System.Byte, содержащий данные, отправляемые после передачи файла. Этот
        ////     параметр может иметь значение null.
        ////
        ////   flags:
        ////     Одно или несколько значений System.Net.Sockets.TransmitFileOptions.
        ////
        //// Исключения:
        ////   T:System.NotSupportedException:
        ////     Операционной системой не является Windows NT или более поздняя версия. -или-
        ////     Сокет не подключен к удаленному узлу.
        ////
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.InvalidOperationException:
        ////     Объект System.Net.Sockets.Socket не находится в режиме блокировки и не может
        ////     принять этот синхронный вызов.
        ////
        ////   T:System.IO.FileNotFoundException:
        ////     Файл fileName не найден.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public void SendFile(string fileName, byte[] preBuffer, byte[] postBuffer, TransmitFileOptions flags);

        //// Сводка:
        ////     Выполняет асинхронную передачу набора файла или буферов данных в памяти на подключенный
        ////     объект System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs для использования в данной асинхронной
        ////     операции сокета.
        ////
        //// Возврат:
        ////     true, если операция ввода-вывода находится в состоянии ожидания. По завершении
        ////     операции создается событие System.Net.Sockets.SocketAsyncEventArgs.Completed
        ////     в параметре e. false, если операция ввода-вывода завершена синхронно. В данном
        ////     случае событие System.Net.Sockets.SocketAsyncEventArgs.Completed на параметре
        ////     e не будет создано и объект e, передаваемый как параметр, можно изучить сразу
        ////     после получения результатов вызова метода для извлечения результатов операции.
        ////
        //// Исключения:
        ////   T:System.IO.FileNotFoundException:
        ////     Файл, указанный в свойстве System.Net.Sockets.SendPacketsElement.FilePath, не
        ////     найден.
        ////
        ////   T:System.InvalidOperationException:
        ////     Операция сокета уже выполнялась с использованием объекта System.Net.Sockets.SocketAsyncEventArgs,
        ////     указанного в параметре e.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows XP и более поздних версиях. Это исключение
        ////     возникает также в том случае, когда объект System.Net.Sockets.Socket не подключен
        ////     к удаленному узлу.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Используется объект System.Net.Sockets.Socket, функционирующий без установления
        ////     соединения, и размер отправляемого файла превышает максимальный размер пакета
        ////     основного транспортного средства.
        //public bool SendPacketsAsync(SocketAsyncEventArgs e);

        //// Сводка:
        ////     Посылает указанное число байтов данных на указанную конечную точку, начиная с
        ////     заданной позиции буфера и используя указанный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, содержащий данные для отправки.
        ////
        ////   offset:
        ////     Положение в буфере данных, с которого начинается отправка данных.
        ////
        ////   size:
        ////     Количество байтов для отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   remoteEP:
        ////     Объект System.Net.EndPoint, представляющий пункт назначения для данных.
        ////
        //// Возврат:
        ////     Число отправленных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра offset меньше 0. -или- Значение offset превышает длину buffer.
        ////     -или- Значение параметра size меньше 0. -или- Значение size превышает значение,
        ////     полученное, если отнять от длины buffer значение параметра offset.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     socketFlags — недопустимое сочетание значений. -или- Произошла ошибка операционной
        ////     системы при доступе к System.Net.Sockets.Socket.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Security.SecurityException:
        ////     Вызывающий оператор в стеке вызовов не имеет необходимых разрешений.
        //public int SendTo(byte[] buffer, int offset, int size, SocketFlags socketFlags, EndPoint remoteEP);

        //// Сводка:
        ////     Посылает указанное число байтов данных на указанную конечную точку, используя
        ////     заданный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, содержащий данные для отправки.
        ////
        ////   size:
        ////     Количество байтов для отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   remoteEP:
        ////     Объект System.Net.EndPoint, представляющий пункт назначения для данных.
        ////
        //// Возврат:
        ////     Число отправленных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Заданное значение size превышает размер параметра buffer.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int SendTo(byte[] buffer, int size, SocketFlags socketFlags, EndPoint remoteEP);

        //// Сводка:
        ////     Передает данные на указанную конечную точку, используя заданный объект System.Net.Sockets.SocketFlags.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, содержащий данные для отправки.
        ////
        ////   socketFlags:
        ////     Поразрядное сочетание значений System.Net.Sockets.SocketFlags.
        ////
        ////   remoteEP:
        ////     Объект System.Net.EndPoint, представляющий пункт назначения для данных.
        ////
        //// Возврат:
        ////     Число отправленных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int SendTo(byte[] buffer, SocketFlags socketFlags, EndPoint remoteEP);

        //// Сводка:
        ////     Посылает данные на указанную конечную точку.
        ////
        //// Параметры:
        ////   buffer:
        ////     Массив типа System.Byte, содержащий данные для отправки.
        ////
        ////   remoteEP:
        ////     Объект System.Net.EndPoint, представляющий пункт назначения для данных.
        ////
        //// Возврат:
        ////     Число отправленных байтов.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     buffer — null. -или- remoteEP — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public int SendTo(byte[] buffer, EndPoint remoteEP);

        //// Сводка:
        ////     Асинхронно передает данные на конкретный удаленный узел.
        ////
        //// Параметры:
        ////   e:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs для использования в данной асинхронной
        ////     операции сокета.
        ////
        //// Возврат:
        ////     true, если операция ввода-вывода находится в состоянии ожидания. По завершении
        ////     операции создается событие System.Net.Sockets.SocketAsyncEventArgs.Completed
        ////     в параметре e. false, если операция ввода-вывода завершена синхронно. В данном
        ////     случае событие System.Net.Sockets.SocketAsyncEventArgs.Completed на параметре
        ////     e не будет создано и объект e, передаваемый как параметр, можно изучить сразу
        ////     после получения результатов вызова метода для извлечения результатов операции.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     Объект System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint не может иметь
        ////     значение "null".
        ////
        ////   T:System.InvalidOperationException:
        ////     Операция сокета уже выполнялась с использованием объекта System.Net.Sockets.SocketAsyncEventArgs,
        ////     указанного в параметре e.
        ////
        ////   T:System.NotSupportedException:
        ////     Этот метод доступен только в Windows XP и более поздних версиях.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Указанный протокол работает с установлением соединения, но объект System.Net.Sockets.Socket
        ////     еще не подключен.
        //public bool SendToAsync(SocketAsyncEventArgs e);

        //// Сводка:
        ////     Задается стандартный уровень защиты IP для сокета.
        ////
        //// Параметры:
        ////   level:
        ////     Уровень защиты IP, который надо установить для сокета.
        ////
        //// Исключения:
        ////   T:System.ArgumentException:
        ////     Параметр level не может иметь значение System.Net.Sockets.IPProtectionLevel.Unspecified.
        ////     Уровень защиты IP не может быть неопределенным.
        ////
        ////   T:System.NotSupportedException:
        ////     System.Net.Sockets.AddressFamily сокета должен быть либо System.Net.Sockets.AddressFamily.InterNetworkV6,
        ////     либо System.Net.Sockets.AddressFamily.InterNetwork.
        //public void SetIPProtectionLevel(IPProtectionLevel level);

        //// Сводка:
        ////     Устанавливает заданное целое значение для указанного параметра System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   optionLevel:
        ////     Одно из значений System.Net.Sockets.SocketOptionLevel.
        ////
        ////   optionName:
        ////     Одно из значений System.Net.Sockets.SocketOptionName.
        ////
        ////   optionValue:
        ////     Значение параметра.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, int optionValue);

        //// Сводка:
        ////     Устанавливает для указанного параметра System.Net.Sockets.Socket заданное значение,
        ////     представленное в виде байтового массива.
        ////
        //// Параметры:
        ////   optionLevel:
        ////     Одно из значений System.Net.Sockets.SocketOptionLevel.
        ////
        ////   optionName:
        ////     Одно из значений System.Net.Sockets.SocketOptionName.
        ////
        ////   optionValue:
        ////     Массив типа System.Byte, который представляет значение параметра.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, byte[] optionValue);

        //// Сводка:
        ////     Устанавливает для заданного параметра System.Net.Sockets.Socket указанное значение
        ////     System.Boolean.
        ////
        //// Параметры:
        ////   optionLevel:
        ////     Одно из значений System.Net.Sockets.SocketOptionLevel.
        ////
        ////   optionName:
        ////     Одно из значений System.Net.Sockets.SocketOptionName.
        ////
        ////   optionValue:
        ////     Значение параметра, представленное в виде объекта System.Boolean.
        ////
        //// Исключения:
        ////   T:System.ObjectDisposedException:
        ////     Объект System.Net.Sockets.Socket закрыт.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        //public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, bool optionValue);

        //// Сводка:
        ////     Устанавливает для указанного параметра System.Net.Sockets.Socket заданное значение,
        ////     представленное в виде объекта.
        ////
        //// Параметры:
        ////   optionLevel:
        ////     Одно из значений System.Net.Sockets.SocketOptionLevel.
        ////
        ////   optionName:
        ////     Одно из значений System.Net.Sockets.SocketOptionName.
        ////
        ////   optionValue:
        ////     Объект System.Net.Sockets.LingerOption или System.Net.Sockets.MulticastOption,
        ////     содержащий значение параметра.
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     optionValue — null.
        ////
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public void SetSocketOption(SocketOptionLevel optionLevel, SocketOptionName optionName, object optionValue);

        //// Сводка:
        ////     Блокирует передачу и получение данных для объекта System.Net.Sockets.Socket.
        ////
        //// Параметры:
        ////   how:
        ////     Одно из значений System.Net.Sockets.SocketShutdown, указывающее на то, что операция
        ////     более не разрешена.
        ////
        //// Исключения:
        ////   T:System.Net.Sockets.SocketException:
        ////     Произошла ошибка при попытке доступа к сокету.
        ////
        ////   T:System.ObjectDisposedException:
        ////     System.Net.Sockets.Socket был закрыт.
        //public void Shutdown(SocketShutdown how);


    }
}
