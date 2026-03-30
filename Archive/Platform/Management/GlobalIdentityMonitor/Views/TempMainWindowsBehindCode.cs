//using Apeiron.Services.GlobalIdentity.Database;
//using Apeiron.Services.GlobalIdentity.Tunings;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.ObjectModel;
//using System.Runtime.CompilerServices;
//using System.Windows.Controls;
//using System.Windows.Media;

//namespace Apeiron.Services.GlobalIdentity
//{

//    /// <summary>
//    /// Класс описывающий логику работы основного окна приложения MainWindow.
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        /// <summary>
//        /// Содержит настройки программы.
//        /// </summary>
//        private readonly AppTuning _AppTuning;

//        /// <summary>
//        /// Содержит источник для токена отмены текущего окна.
//        /// </summary>
//        private readonly CancellationTokenSource _LocalCancellationTokenSource;

//        /// <summary>
//        /// Содержит токен отмены в рамках текущего окна.
//        /// </summary>
//        private readonly CancellationToken _LocalCancellationToken;

//        /// <summary>
//        /// Содержит комбинированный токен всего приложения и текущего окна.
//        /// </summary>
//        private readonly CancellationToken _CombinedToken;

//        /// <summary>
//        /// Содержит контекст базы данных.
//        /// </summary>
//        private readonly DatabaseContext _DBContext;

//        /// <summary>
//        /// Содержит вспомогательный контекст базы данных с нестандартным параметром ConnectTimeout для тестирования подключения.
//        /// </summary>
//        private readonly DatabaseContext _TestDBContext;

//        ///// <summary>
//        ///// Содержит коллекцию наборов данных.
//        ///// </summary>
//        private ObservableCollection<EntityRowGrid>? _ResultDataSet;


//        /// <summary>
//        /// Конструктор. Инициализирует окно приложения.
//        /// </summary>
//        public MainWindow()
//        {
//            // Инициализирует окно.
//            InitializeComponent();

//            //DataMonitor dataMonitor = new();

//            // Создаём объект в котором храняться настройки программы и загружаем настройки из файла.            
//            _AppTuning = AppTuning.Instance.Load();

//            // Создаём локальный токен отмены.
//            _LocalCancellationTokenSource = new CancellationTokenSource();
//            _LocalCancellationToken = _LocalCancellationTokenSource.Token;

//            // Создаём комбинированный токен отмены.
//            if (App.Current.Properties["GlobalToken"] is CancellationToken globalToken)
//            {
//                // Соединяем локальный токен отмены с токеном отмены уровня приложения.
//                using CancellationTokenSource combinedTimeoutTokenSource = CancellationTokenSource.CreateLinkedTokenSource(globalToken, _LocalCancellationToken);
//                _CombinedToken = combinedTimeoutTokenSource.Token;
//            }

//            // Создаём контекст подключения к базе данных.
//            _DBContext = new();

//            // Создаём контекст подключения к базе данных с настройками для передачи в строке подключения небольшого таймаута.
//            _TestDBContext = _DBContext.GetCustomDBContext(3);
//        }


//        /// <summary>
//        /// Обработчик события загрузки окна.
//        /// </summary>
//        /// <param name="sender">Объект создавший событие.</param>
//        /// <param name="e">Аргументы события.</param>
//        private void GlobalIdentityMonitor_Loaded(object sender, RoutedEventArgs e)
//        {
//            // Запускаем основную задачу.
//            _ = Task.Run(() => MainJobAsync(_AppTuning.MainTuning.RefreshPeriod, _CombinedToken));
//        }

//        /// <summary>
//        /// Запускает с периодном основную задачу по обновлению данных в интрефейсе.
//        /// </summary>
//        /// <param name="timerPeriod">Период обновления, полученный из настроек.</param>
//        /// <param name="cancellationToken">Токен отмены.</param>
//        private async Task MainJobAsync(double timerPeriod, CancellationToken cancellationToken)
//        {
//            // Проверка токена отмены.
//            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//            //  Основной цикл поддержки выполнения.
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                // Блок перехвата всех некритических исключений.
//                try
//                {
//                    // Выполнение основной работы.
//                    await DataLoadDindingAsync(cancellationToken).ConfigureAwait(false);

//                    // Задаржка пред следующем получением данных и обновлением UI.
//                    await Task.Delay((int)timerPeriod, cancellationToken).ConfigureAwait(false);
//                }
//                catch (Exception ex)
//                {
//                    //  Проверка критического исключения.
//                    if (ex.IsCritical())
//                    {
//                        //  Повторный выброс исключения.
//                        throw;
//                    }

//                    throw;
//                }
//            }
//        }


//        /// <summary>
//        /// Ассинхронно проверяет подключение к БД и выводит статус в UI.
//        /// </summary>
//        /// <exception cref = "OperationCanceledException" >
//        /// Операция отменена.
//        /// </exception>
//        /// <returns>Если соединение успешно устновлено, то возвращает Истина, иначе Ложь.</returns>
//        private async Task<bool> CheckDatabaseConnectionAsync(CancellationToken cancellationToken)
//        {
//            // Проверка токена отмены.
//            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//            // Получение название БД.
//            var dbName = _TestDBContext.GetDBName();

//            // Заглушка для анализатора.
//            Action<Action> dispatcher = Dispatcher.Invoke;

//            // Проверяем доступ к БД
//            if (await _TestDBContext.CheckConnectionDBAsync(cancellationToken).ConfigureAwait(false))
//            {
//                //Получить диспетчер от текущего окна и использовать его для вызова кода обновления UI.
//                dispatcher(new Action(() => UpdateStatusBarUI(dbName, Colors.DarkGreen, "")));

//                return true;
//            }
//            else
//            {
//                //Получить диспетчер от текущего окна и использовать его для вызова кода обновления UI.
//                dispatcher(new Action(() => UpdateStatusBarUI(dbName, Colors.OrangeRed, "Ошибка подключения к базе данных!!!")));

//                return false;
//            }
//        }


//        /// <summary>
//        /// Функция для обновления UI, данных в строке состояния.
//        /// </summary>
//        /// <param name="dbName">Имя БД.</param>
//        /// <param name="color">Цвет заливки для сроки таблицы.</param>
//        /// <param name="notifyMessage">Выводимое в строку состояния сообщение.</param>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        private void UpdateStatusBarUI(string dbName, Color color, string notifyMessage)
//        {
//            ConnectionToDB.Text = $"БД {dbName}";
//            CircleStatus.Fill = new SolidColorBrush(color);
//            if (notifyMessage.Length > 0)
//            {
//                NotifyText.FontWeight = FontWeights.Bold;
//                NotifyText.Foreground = Brushes.Red;
//            }
//            NotifyText.Text = notifyMessage;
//        }

//        /// <summary>
//        /// Загрузка данных из БД.
//        /// </summary>
//        /// <exception cref = "OperationCanceledException" >
//        /// Операция отменена.
//        /// </exception>
//        private async Task DataLoadDindingAsync(CancellationToken cancellationToken)
//        {
//            // Проверка токена отмены.
//            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//            Action<Action> dispatcher = Dispatcher.Invoke;
//            // Получить диспетчер от текущего окна и использовать его для вызова кода обновления UI.
//            dispatcher(() =>
//            {
//                _ResultDataSet = ViewModel.ViewModelCollection;
//                //Очистка коллекции.
//                _ResultDataSet?.Clear();

//                //MessageBox.Show($"Task - {Task.CurrentId}", "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);
//            });

//            // Проверяем подключение к БД.
//            if (await CheckDatabaseConnectionAsync(cancellationToken).ConfigureAwait(false))
//            {
//                try
//                {
//                    // Получаем данные из базы.
//                    var resultQuery = await _DBContext.GlobalIdentifiers
//                        .AsNoTracking()
//                        .Where(x => x.Id != 3)
//                        .OrderBy(g => g.Name)
//                        .ToArrayAsync(cancellationToken)
//                        .ConfigureAwait(false);

//                    Action<Action> dispatcherAction = Dispatcher.Invoke;
//                    // Получить диспетчер от текущего окна и использовать его для вызова кода обновления UI.
//                    dispatcherAction(() =>
//                    {
//                        // Получаем коллекцию набора данных.
//                        _ResultDataSet = ViewModel.ViewModelCollection;

//                        // Формируем коллекцию из результата запроса для вывода в UI.
//                        foreach (var itemSystem in resultQuery)
//                        {
//                            _ResultDataSet?.Add(new EntityRowGrid()
//                            {
//                                Name = itemSystem.Name,
//                                ReceiptTime = itemSystem.LastTime,
//                                Value = itemSystem.Value,
//                                LastAddress = itemSystem.LastAddress,
//                                LastPort = itemSystem.LastPort,
//                                LastVersion = itemSystem.LastVersion
//                            });
//                        }
//                    });
//                }
//                catch (Microsoft.Data.SqlClient.SqlException ex)
//                {
//                    MessageBox.Show($"Ошибка подключения к базе данных!\n\n\r{ex.Message}", "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);
//                }
//            }
//        }


//        /// <summary>
//        /// Обработчик кнопки закрытия.
//        /// </summary>
//        /// <param name="sender">Объект создавший событие.</param>
//        /// <param name="e">Аргументы события.</param>
//        private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
//        {
//            // Закрываем подключение к базе и разрушаем контекст.
//            _DBContext?.Dispose();
//            _TestDBContext?.Dispose();

//            // Закрываем форму.
//            Close();
//        }

//        /// <summary>
//        /// Обработчик закрытия окна.
//        /// </summary>
//        /// <param name="sender">Объект создавший событие.</param>
//        /// <param name="e">Аргументы события.</param>
//        private void GlobalIdentityMonitor_Closed(object sender, EventArgs e)
//        {
//            // Закрываем подключение к базе и разрушаем контекст.
//            _DBContext?.Dispose();
//            _TestDBContext?.Dispose();

//        }

//        /// <summary>
//        /// Обработчик пункта меню. Обновляет DataGrid.
//        /// </summary>
//        /// <param name="sender">Объект создавший событие.</param>
//        /// <param name="e">Аргументы события.</param>
//        private void RefreshMenuItem_Click(object sender, RoutedEventArgs e)
//        {
//            // Подгружаем данные.
//            _ = Task.Run(() => DataLoadDindingAsync(_CombinedToken).ConfigureAwait(false));
//        }

//        /// <summary>
//        /// Обработчик события пиктограммы. Обновляет DataGrid.
//        /// </summary>
//        /// <param name="sender">Объект создавший событие.</param>
//        /// <param name="e">Аргументы события.</param>
//        private void ToolBarButtonReload_Click(object sender, RoutedEventArgs e)
//        {
//            // Подгружаем данные.
//            _ = Task.Run(() => DataLoadDindingAsync(_CombinedToken).ConfigureAwait(false));
//        }

//        /// <summary>
//        /// Обработчик события нажатия пиктограммы. Вызов окна с параметрами.
//        /// </summary>
//        /// <param name="sender">Объект создавший событие.</param>
//        /// <param name="e">Аргументы события.</param>
//        private void ToolBarButtonTuning_Click(object sender, RoutedEventArgs e)
//        {
//            // Создаёт и отображает окно настроек.
//            ShowTuningWindow();
//        }

//        /// <summary>
//        /// Обработчик меню открытия окна настроек.
//        /// </summary>
//        /// <param name="sender">Объект создавший событие.</param>
//        /// <param name="e">Аргументы события.</param>
//        private void TuningWindowMenuItem_Click(object sender, RoutedEventArgs e)
//        {
//            // Создаёт и отображает окно настроек.
//            ShowTuningWindow();
//        }

//        /// <summary>
//        /// Создаёт и отображает окно настроек.
//        /// </summary>
//        private void ShowTuningWindow()
//        {
//            // Создаёт окно и передаёт настройки.
//            TuningWindow tuningWindow = new(_AppTuning);
//            // Устанавливает для окна авторизации главным/родителем текущее окно.
//            tuningWindow.Owner = this;
//            // Подписывается на событие обновления настроек.
//            tuningWindow.TuningUpdateEvent += TuningWindow_TuningUpdateEvent;
//            // Показывает окно.
//            tuningWindow.Show();
//        }

//        /// <summary>
//        /// Обработчик события обновления настроек.
//        /// </summary>
//        /// <param name="sender">Объект создавший событие.</param>
//        /// <param name="e">Аргументы события, которые содержат новые настройки.</param>
//        private void TuningWindow_TuningUpdateEvent(object? sender, EventArgs e)
//        {
//            if (e is TuningEventArgs tuningEventArgs)
//            {
//                try
//                {
//                    //  Проверка настроек.
//                    _AppTuning.MainTuning.Validation();

//                    // Останавливаем текущую основную задачу.
//                    _LocalCancellationTokenSource.Cancel();

//                    // // Запускаем основную задачу.
//                    //_ = Task.Run(() => MainJobAsync(tuningEventArgs.AppTuning.MainTuning.RefreshPeriod, _CombinedToken)).ConfigureAwait(false);

//                    // Установка параметра указанного в XAML.
//                    Resources["KeepAlivePeriodParameter"] = tuningEventArgs.AppTuning.MainTuning.KeepAlivePeriod;
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка установки одного или нескольких параметров!\nПроверьте корректность настроек в файле Json\n\n\r{ex.Message}", "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);
//                }
//            }
//        }

//        /// <summary>
//        /// Отображает нумерированный список номеров строк в первой колонке элемента DataGrid.
//        /// </summary>
//        void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
//        {
//            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
//        }
//    }
//}


