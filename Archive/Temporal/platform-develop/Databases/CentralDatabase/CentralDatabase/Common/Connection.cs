using Microsoft.Data.SqlClient;

namespace Apeiron.Platform.Databases.CentralDatabase;

/// <summary>
/// Представляет информацию о подключении к базе данных.
/// </summary>
public sealed class Connection
{
    /// <summary>
    /// Поле для хранения построителя строки подключения к серверу баз данных.
    /// </summary>
    private readonly SqlConnectionStringBuilder _ConnectionStringBuilder;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="syncRoot">
    /// Объект, с помощью которого можно синхронизировать доступ.
    /// </param>
    internal Connection(
        [ParameterNoChecks] object syncRoot)
    {
        //  Создание построителя строки подключения к серверу баз данных.
        _ConnectionStringBuilder = new();

        //  Установка объекта, с помощью которого можно синхронизировать доступ.
        SyncRoot = syncRoot;
    }

    /// <summary>
    /// Возвращает объект, с помощью которого можно синхронизировать доступ.
    /// </summary>
    public object SyncRoot { get; }

    /// <summary>
    /// Declares the application workload type when connecting to a database in an SQL Server Availability Group. You can set the value of this property with Microsoft.Data.SqlClient.ApplicationIntent.
    /// </summary>
    public ApplicationIntent ApplicationIntent
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.ApplicationIntent;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.ApplicationIntent = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the name of the application associated with the connection string.
    /// </summary>
    public string ApplicationName
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.ApplicationName;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.ApplicationName = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a string that contains the name of the primary data file. This includes the full path name of an attachable database.
    /// </summary>
    public string AttachDBFilename
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.AttachDBFilename;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.AttachDBFilename = value;
            }
        }
    }

    /// <summary>
    /// Gets the authentication of the connection string.
    /// </summary>
    public SqlAuthenticationMethod Authentication
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.Authentication;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.Authentication = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the column encryption settings for the connection string builder.
    /// </summary>
    public SqlConnectionColumnEncryptionSetting ColumnEncryptionSetting
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.ColumnEncryptionSetting;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.ColumnEncryptionSetting = value;
            }
        }
    }

    /// <summary>
    /// The default wait time (in seconds) before terminating the attempt to execute a command and generating an error. The default is 30 seconds.
    /// </summary>
    public int CommandTimeout
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.CommandTimeout;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.CommandTimeout = value;
            }
        }
    }

    /// <summary>
    /// The number of reconnections attempted after identifying that there was an idle connection failure. This must be an integer between 0 and 255. Default is 1. Set to 0 to disable reconnecting on idle connection failures. An System.ArgumentException will be thrown if set to a value outside of the allowed range.
    /// </summary>
    public int ConnectRetryCount
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.ConnectRetryCount;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.ConnectRetryCount = value;
            }
        }
    }

    /// <summary>
    /// Amount of time (in seconds) between each reconnection attempt after identifying that there was an idle connection failure. This must be an integer between 1 and 60. The default is 10 seconds. An System.ArgumentException will be thrown if set to a value outside of the allowed range.
    /// </summary>
    public int ConnectRetryInterval
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.ConnectRetryInterval;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.ConnectRetryInterval = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error.
    /// </summary>
    public int ConnectTimeout
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.ConnectTimeout;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.ConnectTimeout = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the SQL Server Language record name.
    /// </summary>
    public string CurrentLanguage
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.CurrentLanguage;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.CurrentLanguage = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the name or network address of the instance of SQL Server to connect to.
    /// </summary>
    public string DataSource
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.DataSource;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.DataSource = value;
            }
        }
    }

    /// <summary>
    /// Set/Get the value of Attestation Protocol.
    /// </summary>
    public SqlConnectionAttestationProtocol AttestationProtocol
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.AttestationProtocol;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.AttestationProtocol = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the enclave attestation Url to be used with enclave based Always Encrypted.
    /// </summary>
    public string EnclaveAttestationUrl
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.EnclaveAttestationUrl;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.EnclaveAttestationUrl = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a Boolean value that indicates whether SQL Server uses SSL encryption for all data sent between the client and server if the server has a certificate installed.
    /// </summary>
    public bool Encrypt
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.Encrypt;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.Encrypt = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a Boolean value that indicates whether the SQL Server connection pooler automatically enlists the connection in the creation thread's current transaction context.
    /// </summary>
    public bool Enlist
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.Enlist;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.Enlist = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the name or address of the partner server to connect to if the primary server is down.
    /// </summary>
    public string FailoverPartner
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.FailoverPartner;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.FailoverPartner = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the name of the database associated with the connection.
    /// </summary>
    public string InitialCatalog
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.InitialCatalog;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.InitialCatalog = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a Boolean value that indicates whether User ID and Password are specified in the connection (when false) or whether the current Windows account credentials are used for authentication (when true).
    /// </summary>
    public bool IntegratedSecurity
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.IntegratedSecurity;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.IntegratedSecurity = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the minimum time, in seconds, for the connection to live in the connection pool before being destroyed.
    /// </summary>
    public int LoadBalanceTimeout
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.LoadBalanceTimeout;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.LoadBalanceTimeout = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of connections allowed in the connection pool for this specific connection string.
    /// </summary>
    public int MaxPoolSize
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.MaxPoolSize;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.MaxPoolSize = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the minimum number of connections allowed in the connection pool for this specific connection string.
    /// </summary>
    public int MinPoolSize
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.MinPoolSize;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.MinPoolSize = value;
            }
        }
    }

    /// <summary>
    /// When true, an application can maintain multiple active result sets (MARS). When false, an application must process or cancel all result sets from one batch before it can execute any other batch on that connection. For more information, see [Multiple Active Result Sets (MARS)](https://msdn.microsoft.com//library/cfa084cz.aspx).
    /// </summary>
    public bool MultipleActiveResultSets
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.MultipleActiveResultSets;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.MultipleActiveResultSets = value;
            }
        }
    }

    /// <summary>
    /// If your application is connecting to an AlwaysOn availability group (AG) on different subnets, setting MultiSubnetFailover=true provides faster detection of and connection to the (currently) active server. For more information about SqlClient support for Always On Availability Groups, see [SqlClient Support for High Availability, Disaster Recovery](/dotnet/framework/data/adonet/sql/sqlclient-support-for-high-availability-disaster-recovery).
    /// </summary>
    public bool MultiSubnetFailover
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.MultiSubnetFailover;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.MultiSubnetFailover = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the size in bytes of the network packets used to communicate with an instance of SQL Server.
    /// </summary>
    public int PacketSize
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.PacketSize;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.PacketSize = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the password for the SQL Server account.
    /// </summary>
    public string Password
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.Password;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.Password = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a Boolean value that indicates if security-sensitive information, such as the password, is not returned as part of the connection if the connection is open or has ever been in an open state.
    /// </summary>
    public bool PersistSecurityInfo
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.PersistSecurityInfo;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.PersistSecurityInfo = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a Boolean value that indicates whether the connection will be pooled or explicitly opened every time that the connection is requested.
    /// </summary>
    public bool Pooling
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.Pooling;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.Pooling = value;
            }
        }
    }

    /// <summary>
    ///  Gets or sets a Boolean value that indicates whether replication is supported using the connection.
    /// </summary>
    public bool Replication
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.Replication;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.Replication = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a string value that indicates how the connection maintains its association with an enlisted System.Transactions transaction.
    /// </summary>
    public string TransactionBinding
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.TransactionBinding;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.TransactionBinding = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the channel will be encrypted while bypassing walking the certificate chain to validate trust.
    /// </summary>
    public bool TrustServerCertificate
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.TrustServerCertificate;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.TrustServerCertificate = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a string value that indicates the type system the application expects.
    /// </summary>
    public string TypeSystemVersion
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.TypeSystemVersion;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.TypeSystemVersion = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the user ID to be used when connecting to SQL Server.
    /// </summary>
    public string UserID
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.UserID;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.UserID = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether to redirect the connection from the default SQL Server Express instance to a runtime-initiated instance running under the account of the caller.
    /// </summary>
    public bool UserInstance
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.UserInstance;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.UserInstance = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the name of the workstation connecting to SQL Server.
    /// </summary>
    public string WorkstationID
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.WorkstationID;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.WorkstationID = value;
            }
        }
    }

    /// <summary>
    /// The blocking period behavior for a connection pool.
    /// </summary>
    public PoolBlockingPeriod PoolBlockingPeriod
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.PoolBlockingPeriod;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.PoolBlockingPeriod = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether the System.Data.Common.DbConnectionStringBuilder.ConnectionString property is visible in Visual Studio designers.
    /// </summary>
    public bool BrowsableConnectionString
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.BrowsableConnectionString;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.BrowsableConnectionString = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the connection string associated with the System.Data.Common.DbConnectionStringBuilder.
    /// </summary>
    public string ConnectionString
    {
        get
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Возврат значения.
                return _ConnectionStringBuilder.ConnectionString;
            }
        }
        set
        {
            //  Блокировка критического объекта.
            lock (SyncRoot)
            {
                //  Установка нового значения.
                _ConnectionStringBuilder.ConnectionString = value;
            }
        }
    }
}
