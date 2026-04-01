//using Simargl.Synergy.Core;
//using System.Net.Sockets;

//namespace Simargl.Synergy;

///// <summary>
///// Представляет именованное соединение.
///// </summary>
//internal sealed class NamedJoin :
//    Join
//{
//    /// <summary>
//    /// Инициализирует новый экземпляр.
//    /// </summary>
//    /// <param name="host">
//    /// DNS-имя удаленного узла.
//    /// </param>
//    /// <param name="port">
//    /// Номер порта удаленного узла.
//    /// </param>
//    /// <param name="name">
//    /// Имя соединения.
//    /// </param>
//    internal NamedJoin(string host, int port, string name)
//    {
//        //  Установка основных свойств.
//        Host = IsNotEmpty(host);
//        Port = IsNotNegative(port);
//        Name = IsNotEmpty(name);
//    }

//    /// <summary>
//    /// Возвращает DNS-имя удаленного узла.
//    /// </summary>
//    public string Host { get; }

//    /// <summary>
//    /// Возвращает номер порта удаленного узла.
//    /// </summary>
//    public int Port { get; }

//    /// <summary>
//    /// Возвращает имя соединения.
//    /// </summary>
//    public string Name { get; }

//}
