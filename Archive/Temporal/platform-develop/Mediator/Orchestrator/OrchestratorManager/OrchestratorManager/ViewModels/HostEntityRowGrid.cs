using System;

namespace Apeiron.Platform.OrchestratorManager.ViewModels;

/// <summary>
/// Класс описывающий выборку данных для дальнейшего отображения в DataGrid.
/// </summary>
public class HostEntityRowGrid : ViewModel
{
    private string _Hostname = string.Empty;
    private DateTime _RegTime;

    /// <summary>
    /// Возвращает имя хоста.
    /// </summary>
    public string Hostname
    {
        get => _Hostname ?? string.Empty;
        set => Set(ref _Hostname, value);
    }

    /// <summary>
    /// Возвращает или задаёт время получения сообщения на сервер.
    /// </summary>
    public DateTime RegTime
    {
        get => _RegTime;
        set => Set(ref _RegTime, value);
    }
}
