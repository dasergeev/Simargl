using Apeiron.Recording.Adxl357;
using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет пакет.
/// </summary>
public sealed class Package :
    Data<PackageFile>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="dispatcher">
    /// Диспетчер.
    /// </param>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    /// <param name="parent">
    /// Родительский узел.
    /// </param>
    /// <param name="position">
    /// Положение в файле.
    /// </param>
    /// <param name="data">
    /// Данные пакета.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dispatcher"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="parent"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="position"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="data"/> передана пустая ссылка.
    /// </exception>
    public Package(Dispatcher dispatcher, string name, PackageFile parent, int position, Adxl357DataPackage data) :
        base(dispatcher, name, parent, position)
    {
        //  Установка данных пакета.
        Data = Check.IsNotNull(data, nameof(data));
    }

    /// <summary>
    /// Возвращает данные пакета.
    /// </summary>
    public Adxl357DataPackage Data { get; }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected override sealed void LoadCore()
    {

    }
}
