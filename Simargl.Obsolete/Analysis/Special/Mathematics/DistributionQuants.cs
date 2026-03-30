using System.Collections.Generic;

namespace Simargl.Mathematics;

/// <summary>
/// Структура, содержащая информацию о распределении случайной величины (количества попаданий в бины).
/// </summary>
public struct DistributionQuants
{
    /// <summary>
    /// Поле для хранения количества бинов.
    /// </summary>
    public int Bins;

    /// <summary>
    /// Поле для хранения границ бинов (длина BinBounds > 2).
    /// </summary>
    public double[] BinBounds;

    /// <summary>
    /// Поле для хранения количеств попаданий в бины.
    /// </summary>
    public long[] Quantities;

    /// <summary>
    /// Конструктор: инициализирует новый экземпляр структуры по границам бинов и массиву значений случайной величины.
    /// </summary>
    /// <param name="binBounds">
    /// Массив границ бинов.
    /// </param>
    /// <param name="data">
    /// Массив значений случайной величины.
    /// </param>
    /// <param name="rightCheck">
    /// Проверка попадания в крайне правую граничную точку.
    /// </param>
    public DistributionQuants(double[] binBounds, double[] data, bool rightCheck)
    {
        int dataLength = data.Length; // Объём выборки.            

        int bns = binBounds.Length - 1; // Число бинов.
            
        Bins = bns;
        BinBounds = binBounds;

        long[] qnt = new long[bns]; // Количества попаданий в бины.
        for (int k = 0; k < bns; k++)
        {
            qnt[k] = 0;
        }

        // Подсчитываем количества попаданий в бины...
        double leftPoint; double rightPoint;
        double currentValue;            
        for (int i = 0; i < dataLength; i++)
        {
            currentValue = data[i];
                
            for (int k = 0; k < bns; k++)
            {
                leftPoint = binBounds[k];
                rightPoint = binBounds[k + 1];
                if ( (currentValue >= leftPoint) && (currentValue < rightPoint) )
                {
                    qnt[k] += 1;                        
                    break;
                }
            }

            if (rightCheck)
            {                    
                if ( currentValue == binBounds[bns] )
                {
                    qnt[bns - 1] += 1;
                }
            }
        }
        // ...подсчитали количества попаданий в бины.

        Quantities = qnt;
    }

    /// <summary>
    /// Конструктор: инициализирует новый экземпляр структуры по границам бинов и массиву значений случайной величины.
    /// </summary>
    /// <param name="binBounds">
    /// Массив границ бинов.
    /// </param>
    /// <param name="data">
    /// Коллекция значений случайной величины.
    /// </param>
    /// <param name="rightCheck">
    /// Проверка попадания в крайне правую граничную точку.
    /// </param>
    public DistributionQuants(double[] binBounds, IEnumerable<double> data, bool rightCheck)
    {
        int bns = binBounds.Length - 1; // Число бинов.

        Bins = bns;
        BinBounds = binBounds;

        long[] qnt = new long[bns]; // Количества попаданий в бины.
        for (int k = 0; k < bns; k++)
        {
            qnt[k] = 0;
        }

        // Подсчитываем количества попаданий в бины...
        double leftPoint; double rightPoint;            
        foreach (double value in data)
        {
            for (int k = 0; k < bns; k++)
            {
                leftPoint = binBounds[k];
                rightPoint = binBounds[k + 1];
                if ((value >= leftPoint) && (value < rightPoint))
                {
                    qnt[k] += 1;
                    break;
                }
            }

            if (rightCheck)
            {
                if (value == binBounds[bns])
                {
                    qnt[bns - 1] += 1;
                }
            }
        }
        // ...подсчитали количества попаданий в бины.

        Quantities = qnt;
    }
}
