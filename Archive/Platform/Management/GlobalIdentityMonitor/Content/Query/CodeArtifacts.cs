namespace Apeiron.Services.GlobalIdentity.Query
{
    internal class CodeArtifacts
    {

        //Text="{Binding Path = KeepAlivePeriod, UpdateSourceTrigger=Explicit}" 

        // Загружаем данные в DataGrid первый раз при старте.
        //_ = Task.Run(() => DataLoadDindingAsync(_CombinedToken));
        //_ = Task.Run(() => DataLoadBinding(), _CombinedToken);

        // Запускаем таймер для обновления данных.
        //_Timer.Interval = timerPeriod;
        //_Timer.Elapsed += Timer_Elapsed;
        //_Timer.Enabled = true;


        ///// <summary>
        ///// Обработчик таймера.
        ///// </summary>
        ///// <param name="sender">Объект создавший событие.</param>
        ///// <param name="e">Аргументы события.</param>
        //private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        //{
        //    // Загружаем данные из БД.
        //    DataLoadBinding();
        //}

        ///// <summary>
        ///// Проверяет подключение к БД.
        ///// </summary>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //private bool CheckDatabaseConnection()
        //{
        //    var dbName = _TestDBContext.GetDBName();

        //    // Заглушка для анализатора.
        //    Action<Action> dispatcher = Dispatcher.Invoke;

        //    // Проверяем доступ к БД
        //    if (_TestDBContext.CheckConnectionDB())
        //    {
        //        //Получить диспетчер от текущего окна и использовать его для вызова кода обновления UI.
        //        dispatcher(new Action(() => UpdateStatusBarUI(dbName, Colors.DarkGreen, "")));

        //        return true;
        //    }
        //    else
        //    {
        //        //Получить диспетчер от текущего окна и использовать его для вызова кода обновления UI.
        //        dispatcher(new Action(() => UpdateStatusBarUI(dbName, Colors.OrangeRed, "Ошибка подключения к базе данных!!!")));

        //        return false;
        //    }
        //}


        ///// <summary>
        ///// Загружает данные из базы создаёт коллекцию для отображения в DataGrid.
        ///// </summary>
        ///// <exception cref="Microsoft.Data.SqlClient.SqlException">Ошибка связанная с подключением к БД MS SQL.</exception>
        //private void DataLoadBinding()
        //{
        //    Action<Action> dispatcher = Dispatcher.Invoke;
        //    // Получить диспетчер от текущего окна и использовать его для вызова кода обновления UI.
        //    dispatcher(() =>
        //    {
        //        _ResultDataSet = (ViewModelCollection)Resources["SystemDataSet"];
        //        // Очистка коллекции.
        //        _ResultDataSet.Clear();
        //    });

        //    // Проверяем подключение к БД.
        //    if (CheckDatabaseConnection())
        //    {
        //        try
        //        {
        //            // Получаем данные из базы.
        //            var resultQuery = _DBContext.GlobalIdentifiers.AsNoTracking().Where(x => x.Id != 3).OrderBy(g => g.Name).ToArray();

        //            Action<Action> dispatcherAction = Dispatcher.Invoke;
        //            // Получить диспетчер от текущего окна и использовать его для вызова кода обновления UI.
        //            dispatcherAction(() =>
        //            {
        //                // Получаем коллекцию набора данных.
        //                _ResultDataSet = (ViewModelCollection)Resources["SystemDataSet"];

        //                // Формируем коллекцию из результата запроса для вывода в UI.
        //                foreach (var itemSystem in resultQuery)
        //                {
        //                    _ResultDataSet.Add(new ViewModel()
        //                    {
        //                        Name = itemSystem.Name,
        //                        ReceiptTime = itemSystem.LastTime,
        //                        Value = itemSystem.Value,
        //                        LastAddress = itemSystem.LastAddress,
        //                        LastPort = itemSystem.LastPort,
        //                        LastVersion = itemSystem.LastVersion
        //                    });
        //                }
        //            });
        //        }
        //        catch (Microsoft.Data.SqlClient.SqlException ex)
        //        {
        //            MessageBox.Show($"Ошибка подключения к базе данных!\n\n\r{ex.Message}", "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}


        //_Timer.Enabled = false;
        //_Timer?.Dispose();


        // Применяем новые настройки к таймеру.
        //_Timer.Enabled = false;
        //_Timer.Interval = tuningEventArgs.AppTuning.MainTuning.RefreshPeriod;
        //_Timer.Enabled = true;

        ///// <summary>
        ///// Объект выполнения задачи в потоке интерфейса.
        ///// </summary>
        ////private readonly JoinableTaskFactory _JoinableTaskFactory = new(new JoinableTaskContext());

        //// Получаем имя БД.
        //var dbName = _TestDBContext.GetDBName();
        //// Выводим информацию в UI.
        //_ = Task.Run(async () =>
        //{
        //    await _JoinableTaskFactory.SwitchToMainThreadAsync(default);

        //    ConnectionToDB.Text = $"БД {dbName} - Ok";
        //    CircleStatus.Fill = new SolidColorBrush(Colors.DarkGreen);
        //    NotifyText.Text = "";
        //});

        //Получить диспетчер от текущего окна и использовать его для вызова кода обновления UI.
        //this.Dispatcher.BeginInvoke(new Action(() =>
        //{
        //    ConnectionToDB.Text = $"БД {_DBContext.GetDBName()} - Ok";
        //    CircleStatus.Fill = new SolidColorBrush(Colors.DarkGreen);
        //    NotifyText.Text = "";
        //}));

        //Microsoft.VisualStudio.Shell.ThreadHelper.JoinableTaskFactory.Run(async delegate
        //{
        //    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
        //    /*UI code here*/
        //});

        //this.Dispatcher.BeginInvoke(new Action(() =>
        //{
        //    ConnectionToDB.Text = $"БД {_DBContext.GetDBName()} - No";
        //    CircleStatus.Fill = new SolidColorBrush(Colors.OrangeRed);

        //    NotifyText.FontWeight = FontWeights.Bold;
        //    NotifyText.Foreground = Brushes.Red;
        //    NotifyText.Text = $"Ошибка подключения к базе данных!!!";
        //}));

        //this.Dispatcher.BeginInvoke(() =>
        //{
        //    _ResultDataSet = (ViewModelCollection)Resources["SystemDataSet"];

        //    // Формируем коллекцию из результата запроса для вывода в UI.
        //    foreach (var itemSystem in resultQuery)
        //    {
        //        _ResultDataSet.Add(new ViewModel()
        //        {
        //            Name = itemSystem.Name,
        //            ReceiptTime = itemSystem.LastTime,
        //            Value = itemSystem.Value,
        //            LastAddress = itemSystem.LastAddress,
        //            LastPort = itemSystem.LastPort,
        //            LastVersion = itemSystem.LastVersion
        //        });
        //    }
        //});

        ///// <summary>
        ///// Возвращает экземпляр класса, реализует Singleton.
        ///// </summary>
        //public static AppTuning Instance// { get; } = new();
        //{
        //    get
        //    {
        //        if (Instance == null)
        //            _Instance = Interlocked.CompareExchange(ref _Instance, new(), null)!;

        //        return _Instance;
        //    }
        //}


        ///// <summary>
        ///// Содержит экземпляр класса.
        ///// </summary>
        /////// <remarks>
        /////// Для синъронного доступа использовать <see cref="_SyncInstance"/>.
        /////// </remarks>
        //private static volatile AppTuning? _Instance;

        ///// <summary>
        ///// Объект синхронизации для доступа к полю <see cref="_Instance"/>.
        ///// </summary>
        //private static readonly object _SyncInstance = new();

        //        /// <summary>
        //        /// Возвращает экземпляр класса, реализует Singleton.
        //        /// </summary>
        //        public static AppTuning Instance// { get; } = new();
        //        {
        //            get 
        //            { 
        //                if (_Instance is null)
        //                {
        //                    _Instance = Interlocked.CompareExchange(ref _Instance, new(), null)!;


        ////                    lock (_SyncInstance)
        ////                    {
        ////                        if (_Instance is null)
        ////                        {
        ////                            _Instance = new ();
        ////                        }
        ////}
        //                }

        //                return _Instance;
        //            }            
        //        }

        //// Получаем настройки из соответствующей секции.
        //if (!double.TryParse(configuration["MainTuning:RefreshPeriod"], out double timerPeriod))
        //    timerPeriod =  ;

        //// Получаем настройки из соответствующей секции.
        //if (!int.TryParse(configuration["MainTuning:KeepAlivePeriod"], out int keepAlivePeriod))
        //    keepAlivePeriod = 60;


        //// Проверяем доступ к БД
        //if (_TestDBContext.CheckConnectionDB())
        //{
        //    // Получить диспетчер от текущего окна и использовать его для вызова кода обновления UI.
        //    this.Dispatcher.BeginInvoke(() =>
        //    {
        //        ConnectionToDB.Text = $"БД {_DBContext.GetDBName()} - Ok";
        //        CircleStatus.Fill = new SolidColorBrush(Colors.DarkGreen);
        //        NotifyText.Text = "";

        //        _ResultDataSet = (ViewModelCollection)Resources["SystemDataSet"];
        //        _ResultDataSet.Clear();
        //    });
        //}
        //else
        //{
        //    this.Dispatcher.BeginInvoke(() =>
        //    {
        //        ConnectionToDB.Text = $"БД {_DBContext.GetDBName()} - No";
        //        CircleStatus.Fill = new SolidColorBrush(Colors.OrangeRed);

        //        _ResultDataSet = (ViewModelCollection)Resources["SystemDataSet"];
        //        _ResultDataSet.Clear();

        //        NotifyText.FontWeight = FontWeights.Bold;
        //        NotifyText.Foreground = Brushes.Red;
        //        NotifyText.Text = $"Ошибка подключения к базе данных!!!";
        //    });

        //    // Завершаем работу метода.
        //    return;
        //}



        //var taskResult = Task.Run(async () =>
        //        {
        //            await databaseContext.Database.OpenConnectionAsync();
        //            await databaseContext.Database.CloseConnectionAsync();
        //        }).Wait(TimeSpan.FromSeconds(timeout));

        //return taskResult;

        //// Удаляем все привязки для таблицы датчиков
        //BindingOperations.ClearAllBindings(SystemDataGrid);

        //ICollectionView resultDataSet = CollectionViewSource.GetDefaultView(SystemDataGrid.ItemsSource);
        //if (resultDataSet != null && resultDataSet.CanSort == true)
        //{
        //    resultDataSet.SortDescriptions.Clear();
        //    resultDataSet.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        //    resultDataSet.SortDescriptions.Add(new SortDescription("ReceiptTime", ListSortDirection.Ascending));
        //    resultDataSet.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
        //    resultDataSet.SortDescriptions.Add(new SortDescription("LastAddress", ListSortDirection.Ascending));
        //    resultDataSet.SortDescriptions.Add(new SortDescription("LastPort", ListSortDirection.Ascending));
        //    resultDataSet.SortDescriptions.Add(new SortDescription("LastVersion", ListSortDirection.Ascending));
        //}


        // Отключить отслеживание в целом для объекта контекста.
        //_DBContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        //var resultQuery = _DBContext.IdentityMessages
        //               .GroupBy(i => i.GlobalIdentifier.Id)
        //               .Select(
        //                  g =>
        //                     new
        //                     {
        //                         //Подзапрос для получения имени
        //                         Name = _DBContext.GlobalIdentifiers
        //                           .Where(x => (x.Id == g.Key))
        //                           .Select(x => x.Name)
        //                           .FirstOrDefault(),
        //                         // Получение последнего времени
        //                         LastReceiptTime = g.Max(n => n.ReceiptTime)
        //                     }
        //               )
        //               .OrderBy(n => n.Name) // Сортировка по имени
        //               .Where(x => (x.Name != "Неизвестный идентификатор"))
        //               .AsNoTracking()
        //               .ToArray();

        //foreach (var itemSystem in resultQuery)
        //{
        //    resultDataSet.Add(new ViewModel()
        //    {
        //        Name = itemSystem.Name,
        //        ReceiptTime = itemSystem.LastReceiptTime
        //    });
        //}

        //string connectionString = _DBContext.Database.GetConnectionString() ?? throw new NullReferenceException("Передана пустая строка подключения.");
        //using (SqlConnection connection = new(connectionString))
        //{
        //    connection.Open();
        //    SqlCommand command = new();
        //    command.CommandText = @"-- Region Parameters
        //                            DECLARE @p0 NVarChar(1000) = N'Неизвестный идентификатор'
        //                            -- EndRegion
        //                            SELECT [t2].[Name], [t2].[value] AS [ReceiptTime]
        //                            FROM (
        //                                SELECT [t0].[Name], (
        //                                    SELECT MAX([t1].[ReceiptTime])
        //                                    FROM [IdentityMessages] AS [t1]
        //                                    WHERE [t0].[Id] = [t1].[GlobalIdentifierId]
        //                                    ) AS [value]
        //                                FROM [GlobalIdentifiers] AS [t0]
        //                                ) AS [t2]
        //                            WHERE [t2].[Name] <> @p0
        //                            ORDER BY [t2].[Name]";
        //    command.Connection = connection;

        //    SqlDataReader reader = command.ExecuteReader();

        //    if (reader.HasRows)
        //    {
        //        while (reader.Read()) // построчно считываем данные
        //        {
        //            resultDataSet.Add(new ViewModel()
        //            {
        //                Name = reader.GetValue(0).ToString(),
        //                ReceiptTime = reader.GetValue(1) !=null ? (DateTime)reader.GetValue(1) : DateTime.MinValue ,
        //            });
        //        }
        //    }               

        //var queryResult = from i in _Database.GlobalIdentifiers
        //                  select new { i.Id, i.Name } into resultSelect
        //                  join m in _Database.IdentityMessages on resultSelect.Id equals m.GlobalIdentifier.Id into joinResult
        //                  select new
        //                  {
        //                      Name = resultSelect.Name,
        //                      LastReceiptTime = joinResult.Max(t => t.ReceiptTime)
        //                  };

        //var queryResult = _Database.GlobalIdentifiers
        //    .GroupJoin
        //    (_Database.IdentityMessages,
        //        i => i.Id,
        //        m => m.GlobalIdentifier.Id,
        //        (i, im) => new
        //        {
        //            Name = i.Name,
        //            ReceiptTime = im.Max(x => x.ReceiptTime)
        //        }
        //    ).OrderBy(n => n.Name);

        //var queryResult = _Database.GlobalIdentifiers
        //                 .Select(
        //                     i =>
        //                         new
        //                         {
        //                             Id = i.Id,
        //                             Name = i.Name
        //                         }
        //                 )
        //                 .GroupJoin(
        //                     _Database.IdentityMessages,
        //                     resultSelect => resultSelect.Id,
        //                     m => m.GlobalIdentifierId,
        //                     (resultSelect, joinResult) =>
        //                         new
        //                         {
        //                             Name = resultSelect.Name,
        //                             LastReceiptTime = joinResult.Max(t => t.ReceiptTime)
        //                         }
        //                 );

        //var a = queryResult.ToArray();


        //}
        //// Получаем список систем.
        //var querySystem = from i in _Database.GlobalIdentifiers
        //                  where i.Value != 1
        //                  select new { i.Id, i.Value, i.Name };             

        //foreach (var itemSystem in querySystem)
        //{
        //    // Получаем последнее время.
        //    var lastReceiptTime = _Database.IdentityMessages
        //        .AsNoTracking()
        //        .Where(x => x.GlobalIdentifier.Id == itemSystem.Id)
        //        .OrderByDescending(x => x.ReceiptTime)
        //        .Take(1)
        //        .Select(x => x.ReceiptTime)
        //        .FirstOrDefault();                        ;

        //    resultDataSet.Add(new ViewModel() 
        //    {
        //        Value = itemSystem.Id,
        //        Name = itemSystem.Name,
        //        ReceiptTime = lastReceiptTime
        //    });
        //}
    }
}
