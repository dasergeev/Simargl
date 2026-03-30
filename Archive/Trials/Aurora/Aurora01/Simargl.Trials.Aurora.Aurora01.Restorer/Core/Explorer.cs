using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simargl.Trials.Aurora.Aurora01.Restorer.Core;

/// <summary>
/// Предоставляет методы для обзора файлов.
/// </summary>
public static class Explorer
{
    /// <summary>
    /// Асинхронно возвращает файлы с записями.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая файлы с записями.
    /// </returns>
    public static async Task<(FileInfo Info, int Index, int Speed)[]> GetFilesAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание списка файлов.
        List<(FileInfo Info, int Index, int Speed)> files = [];

        //  Перебор информации файлах.
        foreach (var x in new DirectoryInfo(
            Path.Combine(
                RestorerTunnings.RecordsPath,
                RestorerTunnings.Date.ToString("yyyy-MM-dd")))
            .GetFiles("*", SearchOption.TopDirectoryOnly)
            .Select(x => new
            {
                Info = x,
                Index = int.Parse(x.Extension[1..]),
                Speed = int.Parse(x.Name[2..5]),
            })
            .OrderBy(x => x.Index))
        {
            //  Добавление в список.
            files.Add((x.Info, x.Index, x.Speed));
        }

        //  Возврат информации о файлах.
        return [.. files];
    }

    /// <summary>
    /// Асинхронно выполняет поиск непрерывных последовательностей файлов.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск.
    /// </returns>
    public static async Task<(int Begin, int End)[]> SearchAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание списка фрагментов.
        List<(int Begin, int End)> fragments = [];

        //  Состояние поиска.
        int state = 0;  //  0 - нет вхождения во фрагмент.
                        //  1 - первая часть (с нулевой скоростью).
                        //  2 - основная часть (с ненулевой скоростью).
                        //  3 - последняя часть (с нулевой скоростью).

        //  Параметры фрагмента.
        int beginFirst = -1;    //  Индекс первого файла первой части.
        int endFirst = -1;      //  Индекс следующего за последним файлом первой части.
        int beginLast = -1;     //  Индекс первого файла последней части.
        int endLast;       //  Индекс следующего за последним файлом последней части.

        //  Предыдущий индекс.
        int last = -1;

        //  Перебор информации файлах.
        foreach (var item in await GetFilesAsync(cancellationToken).ConfigureAwait(false))
        {
            //  Разбор состояния.
            switch (state)
            {
                case 0: //  Нет вхождения во фрагмент.
                    //  Проверка скорости.
                    if (item.Speed == 0)
                    {
                        //  Начало фрагмента.
                        state = 1;
                        beginFirst = item.Index;
                        endFirst = -1;
                        beginLast = -1;
                        endLast = -1;
                    }

                    //  Завершение разбора.
                    break;
                case 1: //  Первая часть фрагмента.
                    //  Проверка предыдущего индекса.
                    if (last + 1 != item.Index)
                    {
                        //  Фрагмент обрывается.
                        state = 0;

                        //  Перход к проверке начала фрагмента.
                        goto case 0;
                    }
                    else
                    {
                        //  Проверка скорости.
                        if (item.Speed != 0)
                        {
                            //  Завершение первой части.
                            state = 2;
                            endFirst = item.Index;
                        }
                    }

                    //  Завершение разбора.
                    break;
                case 2: //  Основная часть.
                    //  Проверка предыдущего индекса.
                    if (last + 1 != item.Index)
                    {
                        //  Фрагмент обрывается.
                        state = 0;

                        //  Перход к проверке начала фрагмента.
                        goto case 0;
                    }
                    else
                    {
                        //  Проверка скорости.
                        if (item.Speed == 0)
                        {
                            //  Завершение основной части.
                            state = 3;
                            beginLast = item.Index;
                        }
                    }

                    //  Завершение разбора.
                    break;
                case 3: //  Последняя часть.
                    //  Проверка предыдущего индекса.
                    if (last + 1 != item.Index)
                    {
                        //  Фрагмент обрывается.
                        endLast = last + 1;

                        //  Учесть фрагмент.
                        take();

                        //  Перход к проверке начала фрагмента.
                        state = 0;
                        goto case 0;
                    }
                    else
                    {
                        //  Проверка скорости.
                        if (item.Speed != 0)
                        {
                            //  Завершение последней части.
                            endLast = item.Index;

                            //  Учесть фрагмент.
                            take();

                            //  Перекрывающийся фрагмет.
                            beginFirst = beginLast;
                            state = 1;
                            goto case 1;
                        }
                    }

                    //  Завершение разбора.
                    break;
                default:
                    break;
            }

            //  Установка предыдущего индекса.
            last = item.Index;
        }

        //  Возврат фрагментов.
        return [.. fragments];

        //  Учитывает найденный фрагмент.
        void take()
        {
            //  Проверка минимальной длительности записей с нулевой скоростью.
            if (endFirst - beginFirst >= RestorerTunnings.MinZeroCount &&
                endLast - beginLast >= RestorerTunnings.MinZeroCount)
            {
                //  Определение начала фрагмента.
                int begin = endFirst - RestorerTunnings.MinZeroCount;

                //  Определение конца фрагмента.
                int end = beginLast + RestorerTunnings.MinZeroCount;

                //  Добавление фрагмента в список.
                fragments.Add((begin, end));

                //  Вывод в консоль.
                Console.WriteLine(
                    $"{beginFirst:0000} - {endFirst:0000} - {beginLast:0000} - {endLast:0000} -> " +
                    $"{begin:0000} - {end:0000} : {beginLast - endFirst:0000} minutes");
            }
        }
    }
}
