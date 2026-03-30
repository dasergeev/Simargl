using Apeiron.Platform.Communication;
using Apeiron.Support;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace CommutatorMaui;

public partial class ChatsPage : ContentPage

{

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public ChatsPage()
    {
        //Инициализация компонентов.
        InitializeComponent();

        CancellationToken = new();

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public ChatsPage(Communicator communicator)
	{
        //Инициализация компонентов.
		InitializeComponent();

        CancellationToken = new();

        Dialogs = communicator.Dialogs;

        _listDialog.ItemsSource = Dialogs;

        //Привязка свойств страницы.
        BindingContext = this;

    }
    

    /// <summary>
    /// Получает или задаёт коллекцию диалогов.
    /// </summary>
    DialogCollection? Dialogs { get; set; }

    /// <summary>
    /// Возвращает токен отмены.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Получает или задаёт страницу с сообщениями.
    /// </summary>
    public MessagePage? MessagePage { get; set; }

    /// <summary>
    /// Получает или задаёт диалог.
    /// </summary>
    public Dialog? Dialog { get; set; }



    /// <summary>
    /// Обрабатывает событие выбора элемента _listAccount.
    /// </summary>
    /// <param name="sender">
    /// Объект, вызвавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private async void ListDialog_ItemTapped(object sender, ItemTappedEventArgs e)
    {
            //Создание страницы для диалога.
            App.SafeInvokeInMainThread(() =>
            {
                Dialog = (Dialog)e.Item;

                if (Dialog is not null)
                {
                    MessagePage = new(Dialog, CancellationToken);
                }
                
            });
            await Navigation.PushAsync(MessagePage);

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    private void SafeInvokeInMainThread(Action action)
    {
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            Dispatcher.Dispatch(action);
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(action);
        }
    }

   

   

}
