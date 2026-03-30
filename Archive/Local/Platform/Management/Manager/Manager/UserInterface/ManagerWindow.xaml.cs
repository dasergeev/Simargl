using Apeiron.Platform.Management.Models;
using Apeiron.Platform.Management.Models.Entities;

namespace Apeiron.Platform.Management.Manager.UserInterface;

/// <summary>
/// Представляет главное окно приложения.
/// </summary>
public partial class ManagerWindow :
    Window
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public ManagerWindow()
    {
        //  Инициализация основных компонент.
        InitializeComponent();

        //  Установка модели.
        Model = _MainControl.Model;

        //  Добавление узла с таблицами базы данных.
        Model.Nodes.Add(new DatabaseTablesNode());

        //  Добавление узла с файловыми хранилищами.
        Model.Nodes.Add(new FileStoragesNode());
    }

    /// <summary>
    /// Возвращает модель.
    /// </summary>
    public Model Model { get; }
}
