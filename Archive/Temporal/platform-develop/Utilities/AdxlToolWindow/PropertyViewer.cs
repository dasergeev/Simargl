using System;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Windows;

namespace AdxlToolWindow
{
    /// <summary>
    /// Представляет класс свойсва.
    /// </summary>
    internal class PropertyViewer
    {
        /// <summary>
        /// Представляет ссылку на объект свойства.
        /// </summary>
        private readonly object _Source;

        /// <summary>
        /// Представляет интерфейс свойства.
        /// </summary>
        private readonly PropertyInfo _PropertyInfo;

        /// <summary>
        /// Возвращает имя свойства.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Устанавливает и возвращает значения свойства.
        /// </summary>
        public string Value
        {
            get
            {
                var value = _PropertyInfo.GetValue(_Source)?.ToString();

                if (value == null)
                {
                    return string.Empty;
                }

                return value;
            }
            set
            {
                try
                {
                    //  Проверка, что тип значения IP адресс
                    if (_PropertyInfo.PropertyType == typeof(IPAddress))
                    {
                        //  Установка значения.
                        _PropertyInfo.SetValue(_Source, IPAddress.Parse(value));
                    }
                    else
                    {
                        //  Получение конвертера.
                        var converter = TypeDescriptor.GetConverter(_PropertyInfo.PropertyType);

                        //  Получение значения.
                        var result = converter.ConvertFrom(value);

                        //  Установка значения.
                        _PropertyInfo.SetValue(_Source, result);
                    }
                }
                catch (Exception ex)
                {
                    //Вывод ошибки.
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Инициализирует объект.
        /// </summary>
        /// <param name="source">
        /// Источник совйства.
        /// </param>
        /// <param name="propertyInfo">
        /// Информация о свойстве.
        /// </param>
        internal PropertyViewer(object source, PropertyInfo propertyInfo)
        {
            //  Сохранение источник свойства
            _Source = source;

            //  Сохранение информации о свойстве
            _PropertyInfo = propertyInfo;

            //  Сохранение имени свойства.
            PropertyName = propertyInfo.Name;
        }

    }
}
