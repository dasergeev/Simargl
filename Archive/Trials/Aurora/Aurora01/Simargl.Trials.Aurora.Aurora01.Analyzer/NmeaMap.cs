using Microsoft.EntityFrameworkCore;
using Simargl.Trials.Aurora.Aurora01.Storage;
using Simargl.Trials.Aurora.Aurora01.Storage.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Simargl.Trials.Aurora.Aurora01.Analyzer;

/// <summary>
/// Представляет карту данных NMEA.
/// </summary>
public sealed class NmeaMap
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="fragments">
    /// Список фрагментов.
    /// </param>
    private NmeaMap(List<NmeaFragment> fragments)
    {
        //  Установка списка фрагментов.
        NmeaFragments = fragments;
    }

    /// <summary>
    /// Возвращает список фрагментов.
    /// </summary>
    public List<NmeaFragment> NmeaFragments { get; }

    /// <summary>
    /// Асинхронно загружает карту данных NMEA.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая карту.
    /// </returns>
    public static async Task<NmeaMap> LoadAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание списка фрагментов.
        List<NmeaFragment> fragments = [];

        //  Подключение к базе данных.
        using Aurora01StorageContext context = new();

        //  Выполнение запроса.
        IAsyncEnumerable<NmeaData> nmeaSet = context
            .Nmeas
            .OrderBy(x => x.Timestamp)
            .AsAsyncEnumerable();

        //  Флаг вхождения в фрагмент.
        bool inEntry = false;

        //  Предыдущие данные.
        NmeaData previousData = null!;

        //  Данные фрагмента.
        List<NmeaData> fragmentData = [];

        //  Перебор данных.
        await foreach (NmeaData nmeaData in nmeaSet.WithCancellation(cancellationToken))
        {
            //  Проверка вхождения в фрагмент.
            if (inEntry)
            {
                //  Проверка скорости.
                if (nmeaData.Speed == 0 && previousData.Speed > 0)
                {
                    //  Добавление нового фрагмента.
                    await addFragment().ConfigureAwait(false);

                    //  Добавление данных.
                    fragmentData.Add(nmeaData);
                }
                //  Проверка обрыва фрагмента.
                else if ((NmeaData.ToTime(nmeaData.Timestamp) - NmeaData.ToTime(previousData.Timestamp)).TotalSeconds > 30)
                {
                    //  Добавление нового фрагмента.
                    await addFragment().ConfigureAwait(false);

                    //  Выход из фрагмента.
                    inEntry = false;
                }
                //  Продолжение фрагмента.
                else
                {
                    //  Добавление данных.
                    fragmentData.Add(nmeaData);
                }
            }
            else
            {
                //  Проверка скорости.
                if (nmeaData.Speed == 0)
                {
                    //  Вход в фрагмент.
                    inEntry = true;

                    //  Сброс данных.
                    fragmentData = [];
                }
            }

            //  Установка предыдущих данных.
            previousData = nmeaData;
        }

        //  Добавляет новый фрагмент.
        async Task addFragment()
        {
            //  Проверка данных.
            if (fragmentData.Count >= 2)
            {
                //  Создание фрагмента.
                NmeaFragment fragment = await NmeaFragment.LoadAsync(fragmentData, cancellationToken).ConfigureAwait(false);

                //  Добавление фарагмента в список.
                fragments.Add(fragment);

                //  Вывод информации в консоль.
                Console.WriteLine($"Новый фрагмент: {fragment.FirstTime}, {(fragment.LastTime - fragment.FirstTime).TotalHours}, {fragmentData.Count}.");
            }

            //  Сброс данных.
            fragmentData = [];
        }

        //  Создание карты.
        NmeaMap map = new(fragments);

        //  Возврат карты.
        return map;
    }
}
