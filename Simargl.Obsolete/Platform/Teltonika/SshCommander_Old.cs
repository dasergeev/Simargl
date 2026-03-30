//using System.Net.Sockets;
//using System.Security;
//using Renci.SshNet;
//using Renci.SshNet.Common;
//using Simargl.Designing;
//using System;
//using System.IO;

//namespace Simargl.Platform.Teltonika;

///// <summary>
///// Представляет класс командного интерфейса SSH.
///// </summary>
//public sealed class SshCommander :
//    ICommandLine,
//    IDisposable
//{
//    /// <summary>
//    /// Представляет объект синхронизации класса.
//    /// </summary>
//    private readonly object _SyncRoot = new();

//    /// <summary>
//    /// Представляет имя хоста подключения.
//    /// </summary>
//    private readonly string _Host;

//    /// <summary>
//    /// Представляет порт подключения.
//    /// </summary>
//    private readonly int _Port;

//    /// <summary>
//    /// Представляет имя пользователя соединения.
//    /// </summary>
//    private readonly string _Username;

//    /// <summary>
//    /// Представляет пароль пользователя соединения.
//    /// </summary>
//    private readonly string _Password;

//    /// <summary>
//    /// Представляет клиент командной оболочки.
//    /// </summary>
//    private readonly SshClient _SshClient;

//    /// <summary>
//    /// Представляет клиент передачи файлов.
//    /// </summary>
//    private readonly ScpClient _ScpClient;

//    /// <summary>
//    /// Представляет флаг освобождения ресурса.
//    /// </summary>
//    private bool _Disposed;

//    /// <summary>
//    /// Инициализироет эеземпляр класса.
//    /// </summary>
//    /// <param name="host">
//    /// Aдрес узла для подключения по SSH.
//    /// </param>
//    /// <param name="port">
//    /// Порт узла для подключения по SSH.
//    /// </param>
//    /// <param name="username">
//    /// Имя пользователя.
//    /// </param>
//    /// <param name="password">
//    /// Пароль пользователя.
//    /// </param>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В праметре <paramref name="port"/> передано недопустимое значение.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="host"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="username"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="password"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentException">
//    /// Недопустимый параметр <paramref name="host"/>.
//    /// </exception>
//    /// <exception cref="ArgumentException">
//    /// Обнаружены пробелы в параметр <paramref name="username"/>.
//    /// </exception>
//    public SshCommander(string host, int port, string username, string password)
//    {
//        //  Проверка максимального значения порта.
//        Simargl.Designing.Validation.IsNotLarger(port, ushort.MaxValue, nameof(port));

//        //  Проверка минимального значения порта.
//        Simargl.Designing.Validation.IsNotLess(port, ushort.MinValue, nameof(port));

//        //  Инициализация адреса удаленного узла.
//        _Host = Validation.IsNotNull(host, nameof(host));
        
//        //  Инициализация порта удаленного узла.
//        _Port = port;

//        //  Инициализация имени пользователя.
//        _Username = Validation.IsNotNull(username, nameof(username));

//        //  Инициализация пароля пользователя.
//        _Password = Validation.IsNotNull(password,nameof(password));
        
//        //  Создаёт SSH клиента.
//        _SshClient = new SshClient(_Host, _Port, _Username, _Password);

//        //  Создание SCP клиента.
//        _ScpClient = new ScpClient(_Host, _Port, _Username, _Password);

//    }

//    /// <summary>
//    /// Отправляет команду на удаленный узел по SSH и получает обратную связь в виде выходной строки.
//    /// </summary>
//    /// <param name="command">
//    /// Команда для выполенения на удаленном узле по SSH каналу.
//    /// </param>
//    /// <returns>
//    /// Возращает выходные строки вывода результата команд.
//    /// </returns>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="command"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentException">
//    /// В параметре <paramref name="command"/> передана пустая строка.
//    /// </exception>
//    /// <exception cref="InvalidOperationException">
//    /// Асинхронная операция уже выполняется.
//    /// </exception>
//    /// <exception cref="SshConnectionException">
//    /// Нет Ssh соединения.
//    /// </exception>
//    /// <exception cref="SshException">
//    /// Недопустимая операция - для выполнения этой команды через существующий канал.
//    /// </exception>
//    public string SendCommand(string command)
//    {
//        lock (_SyncRoot)
//        {
//            //  Проверка команды
//            Validation.IsNotNull(command, nameof(command));

//            //  Выполнение команды.
//            var sshCommsndAndResult = _SshClient.RunCommand(command);

//            //Возврат результата.
//            return sshCommsndAndResult.Result.ToString();
//        }
//    }

//    /// <summary>
//    /// Освобождает ресурсы.
//    /// </summary>
//    /// <param name="disposing">
//    /// <c>true</c> если нужно освободить управляемые и неуправляемые ресурсы;
//    /// <c>false</c> если нужно освободить только неуправляемые ресурсы.
//    /// </param>
//    private void Dispose(bool disposing)
//    {
//        // Проверка глобального флага
//        if (!_Disposed)
//        {
//            //  Проверка входного параметра
//            if (disposing)
//            {
//                // Отключаем клиентов.
//                Disconnect();

//                // Высвобождаем объекты.
//                _SshClient.Dispose();
//                _ScpClient.Dispose();
//            }
//            //  Установка состояние объектов в глобальный флаг
//            _Disposed = true;
//        }
//    }

//    /// <summary>
//    /// Освобождает ресурсы, занятые объектом.
//    /// </summary>
//    public void Dispose()
//    {
//        Dispose(disposing: true);

//        GC.SuppressFinalize(this);
//    }


//    /// <summary>
//    /// Представляет подключение к устройству.
//    /// </summary>
//    /// <exception cref="ObjectDisposedException">
//    /// Объект разрушен.
//    /// </exception>
//    /// <exception cref="SocketException">
//    /// Ошибка подключения, или неверное значение адреса.
//    /// </exception>
//    /// <exception cref="SshConnectionException">
//    /// Ошибка Ssh сессии.
//    /// </exception>
//    /// <exception cref="SshAuthenticationException">
//    /// Ошибка Ssh аутификации.
//    /// </exception>
//    public void Connect()
//    {
//        lock (_SyncRoot)
//        {
//            //  Проверка текущего состояния соединения Ssh.
//            if (!_SshClient.IsConnected)
//            {
//                //  Подключение по Ssh
//                _SshClient.Connect();
//            }
//            //  Проверка текущего состояния соединения Scp.
//            if (!_ScpClient.IsConnected)
//            {
//                //  Подключение по Scp
//                _ScpClient.Connect();
//            }
//        }
//    }

//    /// <summary>
//    /// Представляет функцию отключения от устройства.
//    /// </summary>
//    /// <exception cref="ObjectDisposedException">
//    /// Объект разрушен.
//    /// </exception>
//    public void Disconnect()
//    {
//        lock (_SyncRoot)
//        {
//            //  Проверка текущего состояния соединения Ssh.
//            if (!_SshClient.IsConnected)
//            {
//                //  Отключение от Ssh
//                _SshClient.Disconnect();
//            }
//            //  Проверка текущего состояния соединения Scp.
//            if (!_ScpClient.IsConnected)
//            {
//                //  Отключение от Scp
//                _ScpClient.Disconnect();
//            }
//        }
//    }


//    /// <summary>
//    /// Представляет функцию загрузки файла на удаленное устройство.
//    /// </summary>
//    /// <param name="localPath">
//    /// Путь на локальной машине.
//    /// </param>
//    /// <param name="remotePath">
//    /// Путь на удаленной машине.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="localPath"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="remotePath"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="SecurityException">
//    /// Недостаточно прав на файл.
//    /// </exception>
//    /// <exception cref="InvalidOperationException">
//    /// Файл для загрузки не существует.
//    /// </exception>
//    /// <exception cref="ArgumentException">
//    /// В параметре <paramref name="localPath"/> передана некоректная строка.
//    /// </exception>
//    /// <exception cref="UnauthorizedAccessException">
//    /// Недостаточно прав на файл.
//    /// </exception>
//    /// <exception cref="PathTooLongException">
//    /// Длина путь к файлу в параметре <paramref name="localPath"/> больше допустимого.
//    /// </exception>
//    /// <exception cref="NotSupportedException">
//    /// В параметре <paramref name="localPath"/> присутствует недопустимый символ ':'.
//    /// </exception>
//    /// <exception cref="ArgumentException">
//    /// В параметре <paramref name="remotePath"/> передана пустая строка.
//    /// </exception>
//    /// <exception cref="ScpException">
//    /// Директория в параметре <paramref name="remotePath"/> не существует.
//    /// </exception>
//    /// <exception cref="SshException">
//    /// Удаленный хост отверг передачу файла.
//    /// </exception>
//    public void UploadFile(string localPath, string remotePath)
//    {
//        lock (_SyncRoot)
//        {
//            //  Проверка пути файла.
//            Validation.IsNotNull(localPath, nameof(localPath));

//            //  Проверка пути удаленного узла.
//            Validation.IsNotNull(remotePath, nameof(remotePath));

//            //  Создание структыры информации о файле.
//            FileInfo fileInfo = new(localPath);

//            // Проверка файла на существование.
//            if (!fileInfo.Exists)
//            {
//                //  Выброс исключения.
//                throw new InvalidOperationException("Файла для загрузки на удаленный узел не существует: " + localPath);
//            }

//            // Загрузка файла на удалённый узел.
//            _ScpClient.Upload(fileInfo, remotePath);
//        }
//    }


//    /// <summary>
//    /// Представляет функцию загрузки файла с удаленного устройство.
//    /// </summary>
//    /// <param name="localPath">
//    /// Путь на локальной машине.
//    /// </param>
//    /// <param name="remotePath">
//    /// Путь на удаленной машине.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="localPath"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="remotePath"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="SecurityException">
//    /// Недостаточно прав на файл.
//    /// </exception>
//    /// <exception cref="InvalidOperationException">
//    /// Файл для загрузки не существует.
//    /// </exception>
//    /// <exception cref="ArgumentException">
//    /// В параметре <paramref name="localPath"/> передана некоректная строка.
//    /// </exception>
//    /// <exception cref="UnauthorizedAccessException">
//    /// Недостаточно прав на файл.
//    /// </exception>
//    /// <exception cref="PathTooLongException">
//    /// Длина путь к файлу в параметре <paramref name="localPath"/> больше допустимого.
//    /// </exception>
//    /// <exception cref="NotSupportedException">
//    /// В параметре <paramref name="localPath"/> присутствует недопустимый символ ':'.
//    /// </exception>
//    /// <exception cref="ArgumentException">
//    /// В параметре <paramref name="remotePath"/> передана пустая строка.
//    /// </exception>
//    /// <exception cref="ScpException">
//    /// Файл в параметре <paramref name="remotePath"/> не существует.
//    /// </exception>
//    /// <exception cref="SshException">
//    /// Удаленный хост отверг передачу файла.
//    /// </exception>
//    public void DowloadFile(string localPath, string remotePath)
//    {
//        lock (_SyncRoot)
//        {
//            //  Создание структыры информации о файле.
//            FileInfo fileInfo = new(localPath);

//            // Проверка файла на существование.
//            if (fileInfo.Exists)
//            {
//                //   Удаление существующего файла.
//                fileInfo.Delete();
//            }

//            // Скачиваем файл
//            _ScpClient.Download(remotePath, fileInfo);
//        }
//    }

//    /// <summary>
//    /// Представляет функцию получения потока командной строки
//    /// </summary>
//    /// <returns>
//    /// Поток.
//    /// </returns>
//    /// <exception cref="NotImplementedException">
//    /// Не поддерживается для данного командного интерфейса.
//    /// </exception>
//    public Stream GetStream()
//    {
//        throw new NotImplementedException();
//    }
//}
