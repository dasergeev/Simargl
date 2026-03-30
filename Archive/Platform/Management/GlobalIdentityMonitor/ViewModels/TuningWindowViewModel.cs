using Apeiron.Services.GlobalIdentity.Commands;
using Apeiron.Services.GlobalIdentity.Tunings;
using System.Collections.Generic;
using System.Windows.Input;

namespace Apeiron.Services.GlobalIdentity.ViewModels;

/// <summary>
/// Представляет модель-представление для окна настроек.
/// </summary>
public class TuningWindowViewModel : ViewModel
{
    #region Свойства

    private int _RefreshPeriod;
    private int _KeepAlivePeriod;
    private AppTuning AppTuning { get; } = null!;

    /// <summary>
    /// Возвращает или инициализирует период формирования идентификационных данных в миллисекундах.
    /// </summary>
    public int RefreshPeriod
    {
        get => _RefreshPeriod;
        set => Set(ref _RefreshPeriod, value);
    }

    /// <summary>
    /// Возвращает или инициализирует период формирования идентификационных данных в миллисекундах.
    /// </summary>
    public int KeepAlivePeriod
    {
        get => _KeepAlivePeriod;
        set => Set(ref _KeepAlivePeriod, value);
    }

    /// <summary>
    /// Свойство возвращающее команду закрытия окна.
    /// </summary>
    public ICommand UpdateTuningsValuesCommand { get; set; }

    #endregion


    /// <summary>
    /// Инициализирует класс модели-представления.
    /// </summary>
    public TuningWindowViewModel()
    {
        // Создаёт команду.
        UpdateTuningsValuesCommand = new RelayCommand(OnUpdateTuningValuesCommandExecute, CanUpdateTuningsValuesCommandExecute);

        // Создаём объект в котором храняться настройки программы и загружаем настройки из файла.            
        AppTuning = AppTuning.Instance;

        if (AppTuning is not null)
        {
            RefreshPeriod = AppTuning.MainTuning.RefreshPeriod;
            KeepAlivePeriod = AppTuning.MainTuning.KeepAlivePeriod;
        }
    }


    #region Команды

    /// <summary>
    /// Возможность выполнения команды остановки опроса БД и обновления UI.
    /// </summary>
    /// <param name="parameter">Параметр</param>
    /// <returns>Возвращает доступность выполнения команды - True или False.</returns>
    private bool CanUpdateTuningsValuesCommandExecute(object? parameter) => parameter is not null;

    /// <summary>
    /// Выполняет команду остановки опроса БД и обновления UI.
    /// </summary>
    /// <param name="parameter">Параметр.</param>
    private void OnUpdateTuningValuesCommandExecute(object? parameter)
    {
        if (parameter is List<object> list)
        {
            if (int.TryParse(list[0].ToString(), out int refreshPeriodValue))
                AppTuning.MainTuning.RefreshPeriod = refreshPeriodValue;

            if (int.TryParse(list[1].ToString(), out int keepAlivePeriodValue))
                AppTuning.MainTuning.KeepAlivePeriod = keepAlivePeriodValue;
        }

        // Сохранение настроек в файл.
        _ = Task.Run(async () =>
        {
            await AppTuning.JsonSerializeAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));
        });
    }

    #endregion
}
