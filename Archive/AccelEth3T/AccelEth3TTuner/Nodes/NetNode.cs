using Simargl.Designing;
using System;
using System.ComponentModel;
using System.Net;
using System.Threading;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет узел сети.
/// </summary>
public sealed class NetNode() :
    Node("Сеть")
{
    /// <summary>
    /// Поле для хранения первого адреса сети.
    /// </summary>
    private IPAddress _FirstAddress = IPAddress.Parse("192.168.1.100");

    /// <summary>
    /// Поле для хранения последнего адреса сети.
    /// </summary>
    private IPAddress _LastAddress = IPAddress.Parse("192.168.1.200");

    /// <summary>
    /// Поле для хранения таймаута.
    /// </summary>
    private int _Timeout = 500;

    /// <summary>
    /// Возвращает или задаёт первый адрес сети.
    /// </summary>
    [Category("Адреса")]
    [DisplayName("Первый")]
    [Description("Первый адрес сети.")]
    public string FirstAddress
    {
        get => _FirstAddress.ToString();
        set
        {
            try
            {
                //  Получение нового адреса.
                IPAddress ipAddress = IPAddress.Parse(value);

                //  Проверка адреса.
                if (ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    throw new InvalidOperationException("Введён некорректный IP-адрес.");
                }

                //  Установка нового значения.
                _FirstAddress = ipAddress;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(FirstAddress)));
            }
            catch
            {
                throw new InvalidOperationException("Введён некорректный IP-адрес.");
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт последний адрес сети.
    /// </summary>
    [Category("Адреса")]
    [DisplayName("Последний")]
    [Description("Последний адрес сети.")]
    public string LastAddress
    {
        get => _LastAddress.ToString();
        set
        {
            try
            {
                //  Получение нового адреса.
                IPAddress ipAddress = IPAddress.Parse(value);

                //  Проверка адреса.
                if (ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    throw new InvalidOperationException("Введён некорректный IP-адрес.");
                }

                //  Установка нового значения.
                _LastAddress = ipAddress;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(LastAddress)));
            }
            catch
            {
                throw new InvalidOperationException("Введён некорректный IP-адрес.");
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт последний адрес сети.
    /// </summary>
    [Category("Соединение")]
    [DisplayName("Таймаут")]
    [Description("Время ожидания ответа датчика.")]
    public TimeSpan Timeout
    {
        get => TimeSpan.FromMilliseconds(_Timeout);
        set
        {
            try
            {
                //  Получение нового таймаута.
                int timeout = IsTimeout(value, nameof(Timeout));

                //  Проверка изменения значения.
                if (_Timeout != timeout)
                {
                    //  Установка нового значения.
                    _Timeout = timeout;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(Timeout)));
                }
            }
            catch
            {
                throw new InvalidOperationException("Введён некорректный IP-адрес.");
            }
        }
    }
}
