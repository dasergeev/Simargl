//using System.ComponentModel;

//namespace Simargl.Detecting.Bearing.Simulator;

///// <summary>
///// Представляет объект хоста.
///// </summary>
//public abstract class HostObject :
//    INotifyPropertyChanged
//{
//    /// <summary>
//    /// Прои
//    /// </summary>
//    public event PropertyChangedEventHandler? PropertyChanged;

//    /// <summary>
//    /// Поле для хранения контейнера зависимостей.
//    /// </summary>
//    private IHost? _Host;

//    /// <summary>
//    /// Возвращает контейнер зависимостей.
//    /// </summary>
//    public IHost Host
//    {
//        get
//        {
//            //  Проверка контейнера зависимостей.
//            _Host ??= ((App)Application.Current).Host;

//            //  Возврат хоста.
//            return _Host;
//        }
//    }
//}
