namespace DatabaseConfigurator;

internal class CodeArfifacts
{
    //EntityControl sensorsUserControl = new();
    //EntityControl registrarUserControl = new();
    //EntityControl channelUserControl = new();
    //EntityControl rawDirectoriesUserControl = new();
    //EntityControl recordDirectoriesUserControl = new();
    //EntityControl sourceUserControl = new();

    //// Делаем привязку данных

    //PropertyInfo propertyInfoOfId = typeof(Sensor).GetProperty("SerialNumber");

    //var propertyPath = new PropertyPath(propertyInfoOfId);

    //var bind = new Binding();
    //bind.Source = _DBContext.Sensors.Local.ToObservableCollection();
    //bind.Path = propertyPath;

    //sensorsUserControl.EntityDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
    //{
    //    Source = _DBContext.Sensors.Local.ToObservableCollection()
    //});

    //registrarUserControl.EntityDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
    //{
    //    Source = _DBContext.Registrars.Local.ToObservableCollection()
    //});

    //channelUserControl.EntityDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
    //{
    //    Source = _DBContext.Channels.Local.ToObservableCollection()
    //});

    //rawDirectoriesUserControl.EntityDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
    //{
    //    Source = _DBContext.RawDirectories.Local.ToObservableCollection()
    //});

    //recordDirectoriesUserControl.EntityDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
    //{
    //    Source = _DBContext.RecordDirectories.Local.ToObservableCollection()
    //});

    //sourceUserControl.EntityDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
    //{
    //    Source = _DBContext.Sources.Local.ToObservableCollection()
    //});


    //// Создаём новый TabControl
    //MainTabControl.Items.Add(new TabItem
    //{
    //    Header = new TextBlock { Text = "Sensors" },
    //    Content = sensorsUserControl
    //});

    //MainTabControl.Items.Add(new TabItem
    //{
    //    Header = new TextBlock { Text = "Registrar" },
    //    Content = registrarUserControl
    //});

    //MainTabControl.Items.Add(new TabItem
    //{
    //    Header = new TextBlock { Text = "Channel" },
    //    Content = channelUserControl
    //});

    //MainTabControl.Items.Add(new TabItem
    //{
    //    Header = new TextBlock { Text = "RawDirectories" },
    //    Content = rawDirectoriesUserControl
    //});

    //MainTabControl.Items.Add(new TabItem
    //{
    //    Header = new TextBlock { Text = "RecordDirectories" },
    //    Content = recordDirectoriesUserControl
    //});

    //MainTabControl.Items.Add(new TabItem
    //{
    //    Header = new TextBlock { Text = "Source" },
    //    Content = sourceUserControl
    //});


    //// Формируем список с DataGrid
    //foreach (var itemTab in MainTabControl.Items)
    //{
    //    // Получаем текущий Tab
    //    var currentTab = (TabItem)itemTab;
    //    // Получаем текущий Control
    //    var currentEntityControl = (EntityControl)currentTab.Content;
    //    // Добавляем текущее DataGrid в список для сохранения.
    //    dataGridList.Add(currentEntityControl.EntityDataGrid);
    //}

    //Dictionary<string, ObservableCollection<ViewModelsClass>> dictionaryOfData = new();

    //EntityControl RecordDirectoriesEntityControl = new();

    //try
    //{
    //    // Получаем данные из базы.
    //    var queryRawDirectories = databaseConfigViewModel.Context.RawDirectories.ToArray();
    //    var queryRegistrars = databaseConfigViewModel.Context.Registrars.ToArray();
    //    var queryChannels = databaseConfigViewModel.Context.Channels.ToArray();
    //    var querySources = databaseConfigViewModel.Context.Sources.ToArray();
    //    var querySensors = databaseConfigViewModel.Context.Sensors.ToArray();


    //    ObservableCollection<ViewModelsClass> viewModelsRawDirectoriesCollection = new();

    //    foreach (var item in queryRawDirectories)
    //    {
    //        viewModelsRawDirectoriesCollection.Add( new RecordDirectoriesViewModel() { Path = item.Path });
    //    }

    //    // Добавляем в словарь.
    //    dictionaryOfData.Add("RowDirectories", viewModelsRawDirectoriesCollection);

    //}
    //catch (Microsoft.Data.SqlClient.SqlException ex)
    //{
    //    MessageBox.Show($"Ошибка подключения к базе данных!\n\n\r{ex.Message}", "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);
    //}

    ////this.Dispatcher.BeginInvoke(() =>
    ////{
    ////var a = dictionaryOfData["RowDirectories"];
    ////RecordDirectoriesEntityControl.EntityDataGrid.ItemsSource = dictionaryOfData["RowDirectories"];

    //RecordDirectoriesEntityControl.EntityDataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
    //{
    //    Source = dictionaryOfData["RowDirectories"],
    //});
    ////});


    //Label checkBase = new();
    //checkBase.Content = "Соединение с БД - " + CheckConnectionDB(_DBContext).ToString();

    //Label checkDBName = new Label();
    //checkDBName.Content = _DBContext.Database;

    //var dbConnectionString = _DBContext.Database.GetConnectionString()?.Split(';');

    //MainTabControl.Items.Add(new TabItem
    //{
    //    Header = new TextBlock { Text = "Таблица 1" },
    //    Content = checkBase
    //}) ;

    ///// <summary>
    ///// Проверяет соединение с локальной БД.
    ///// Если соединение не установлено, то засыпает на 4 сей. и повторяет снова.
    ///// </summary>
    ///// <param name="cancellationToken"></param>
    //public async Task CheckConnectionDB(CancellationToken cancellationToken)
    //{
    //    if (_LocalDBContext is not null)
    //    {
    //        //while (!cancellationToken.IsCancellationRequested)
    //        //{
    //        try
    //        {
    //            _Logger?.LogInformation($"Ожидаем соединения с локальной БД...");
    //            if (CheckConnectionDB(_LocalDBContext))
    //            {
    //                _Logger?.LogInformation($"Локальная база данных доступна.");
    //                await Task.Delay(10, cancellationToken);
    //                //break;
    //            }
    //            else
    //            {
    //                throw new InvalidOperationException();
    //            }
    //        }
    //        catch (TaskCanceledException)
    //        {
    //            //break;
    //        }
    //        //}
    //    }
    //}

    ///// <summary>
    ///// Проверяет есть ли подключение к БД.
    ///// </summary>
    ///// <param name="databaseContext">Контекст базы данных</param>
    ///// <returns>Если соединение успешно устновлено, то возвращает Истина, иначе ложь.</returns>
    //public static bool CheckConnectionDB(DbContext databaseContext)
    //{
    //    try
    //    {
    //        databaseContext.Database.OpenConnection();
    //        databaseContext.Database.CloseConnection();
    //        return true;
    //    }
    //    catch (SqlException)
    //    {
    //        return false;
    //    }
    //}
}
