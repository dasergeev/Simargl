
using Apeiron.Platform.Communication;
using System.Collections.ObjectModel;
using System.Threading;
using System;
using Apeiron.Support;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using System.Collections.Specialized;

namespace CommutatorMaui;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MessagePage : ContentPage,
    IDisposable
{

    

    /// <summary>
    /// Поле для хранения источника основного токена отмены.
    /// </summary>
    private readonly CancellationTokenSource _TokenSource;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public MessagePage()
    {
        //  Создание источника основного токена отмены.
        _TokenSource = new();

        //  Установка основного токена отмены.
        CancellationToken = _TokenSource.Token;

        //Инициализация компонентов.
        InitializeComponent();

        HeaderDialog = "Поиск диалога";

        //Привязка свойств страницы.
        BindingContext = this;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="dialog">
    /// Диалог.
    /// </param>
    public MessagePage(Dialog dialog, CancellationToken cancellationToken)
    {
        //Инициализация компонентов.
        InitializeComponent();


        Dialog = dialog;

        HeaderDialog = dialog.Companion.Name;

        //  Создание источника основного токена отмены.
        _TokenSource = new();

        //  Установка основного токена отмены.
        CancellationToken = _TokenSource.Token;

        

        //Привязка свойств страницы.
        BindingContext = this;

        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            _ = Task.Run(async delegate
            {
                var dialog = Dialog;
                if (dialog is not null)
                {
                    await dialog.UpdateAsync(default, CancellationToken).ConfigureAwait(false);
                    
                }
            });
            return true;

        });

 
        

    }

    /// <summary>
    /// Получает или задаёт токен отмены.
    /// </summary>
    CancellationToken CancellationToken { get; set; }


    /// <summary>
    /// Получает или задаёт заголовок чата.
    /// </summary>
    public String HeaderDialog { get; set; }


    /// <summary>
    /// Возвращает или задаёт текущий диалог.
    /// </summary>
    public Dialog? Dialog { get; set; }


    /// <summary>
    /// Происходит при нажатии на кнопку sensButton.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SendButton_Click(object sender, EventArgs e)
    {
        string text = "";
        App.SafeInvokeInMainThread(() =>
        {
            text = inputFieldEditor.Text;
            inputFieldEditor.Text = string.Empty;
            inputFieldEditor.Focus();
        });
        //  Асинхронная отправка сообщения.
        _ = Task.Run(async delegate
        {
            if (!CancellationToken.IsCancellationRequested)
            {
                //  Блок перехвата всех некритических исключений.
                try
                {
                    //  Отправка сообщения.
                    if (Dialog is not null)
                    {
                        await Dialog.SendMessageAsync(text, CancellationToken).ConfigureAwait(false);
                        
                    }
                }
                catch (Exception ex)
                {
                    //  Проверка критического исключения.
                    if (ex.IsCritical())
                    {
                        //  Повторный выброс исключения.
                        throw;
                    }
                }
            }


        }, CancellationToken);

    }


    /// <summary>
    /// Происходит при изменении макета MessagePage.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Appearing_MessagePage(object sender, EventArgs e)
    {
        if (Dialog is not null)
        {
            if (Dialog.Messages.Count > 0)
            {
                App.SafeInvokeInMainThread(() =>
                {
                    _ListMessages.ScrollTo(Dialog.Messages.Last(), ScrollToPosition.Start);
                });
            }
        }
    }

    //  Разрушает объект.
    public void Dispose()
    {
        //  Разрушение источника токена отмены.
        ((IDisposable)_TokenSource).Dispose();
    }

    


}