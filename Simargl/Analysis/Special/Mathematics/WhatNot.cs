using Simargl.Frames;
using System;
using System.IO;

namespace Simargl.Mathematics;

/// <summary>
/// Всякая всячина.
/// </summary>
public static class WhatNot
{
    /// <summary>
    /// Определение диапазона скоростей.
    /// </summary>
    /// <param name="path2Cadrs">
    /// Путь к папке с кадрами.
    /// </param>
    /// <param name="speedName">
    /// Имя процесса "Скорость".
    /// </param>
    /// <param name="label">
    /// Метка нужных файлов: подстрока в их именах.
    /// </param>
    /// <returns>
    /// Два значения скорости: (наименьшее, наибольшее).
    /// </returns>
    public static double[] SpeedRange(string path2Cadrs, string speedName, string label)
    {
        FileInfo[] files = FileManagement.GetFilesWithLabel(path2Cadrs, label); // Список кадров регистрации.

        int nCadrs = files.Length; int n = 0; // УБРАТЬ потом !!!!!!!!!

        double speedMin = 1000.0; double speedMax = 0.0; // Возвращаемые значения.
        double currentSpeed;
        int nSites; // Число отсчётов.
        foreach (FileInfo file in files)
        {
            Frame cadr = new(file.FullName); //  Открыли кадр.

            double[] speed = cadr.Channels[speedName].Vector.ToArray(); // Загрузили процесс скорость.
                
            nSites = speed.Length;
            for (int i = 0; i < nSites; i++)
            {
                currentSpeed = speed[i];

                if ( currentSpeed < speedMin)
                {
                    speedMin = currentSpeed;
                }

                if (currentSpeed > speedMax)
                {
                    speedMax = currentSpeed;
                }
            }

            n += 1; Console.WriteLine($"Просмотрено {n} кадров из {nCadrs}.   minSpeed = {speedMin},   maxSpeed = {speedMax}"); // УБРАТЬ потом !!!!!!!!!
        }

        double[] speedRange = new double[2] { speedMin, speedMax };
        return speedRange;
    }

    /// <summary>
    /// Определение диапазона скоростей.
    /// </summary>
    /// <param name="path2Cadrs">
    /// Путь к папке с кадрами.
    /// </param>
    /// <param name="speedName">
    /// Имя процесса "Скорость".
    /// </param>        
    /// <returns>
    /// Два значения скорости: (наименьшее, наибольшее).
    /// </returns>
    public static double[] SpeedRange(string path2Cadrs, string speedName)
    {
        FileInfo[] files = FileManagement.GetAllFiles(path2Cadrs); // Список кадров регистрации.

        int nCadrs = files.Length; int n = 0; // УБРАТЬ потом !!!!!!!!!

        double speedMin = 1000.0; double speedMax = 0.0; // Возвращаемые значения.
        double currentSpeed;
        int nSites; // Число отсчётов.
        foreach (FileInfo file in files)
        {
            Frame cadr = new(file.FullName); //  Открыли кадр.

            double[] speed = cadr.Channels[speedName].Vector.ToArray(); // Загрузили процесс скорость.

            nSites = speed.Length;
            for (int i = 0; i < nSites; i++)
            {
                currentSpeed = speed[i];

                if (currentSpeed < speedMin)
                {
                    speedMin = currentSpeed;
                }

                if (currentSpeed > speedMax)
                {
                    speedMax = currentSpeed;
                }
            }

            n += 1; Console.WriteLine($"Просмотрено {n} кадров из {nCadrs}.   minSpeed = {speedMin},   maxSpeed = {speedMax}"); // УБРАТЬ потом !!!!!!!!!
        }

        double[] speedRange = new double[2] { speedMin, speedMax };
        return speedRange;
    }

    /// <summary>
    /// Деление пополам.
    /// </summary>
    /// <param name="n">
    /// Целое число.
    /// </param>
    /// <returns>
    /// n/2, если n - чётное; (n + 1)/2 в противном случае.
    /// </returns>
    public static int Halving(int n)
    {
        if (n % 2 == 1)
        {
            return (n + 1) / 2;
        }
        else
        {
            return n / 2;
        }
    }

    /// <summary>
    /// Округление до нужного десятичного знака.        
    /// </summary>
    /// <param name="x">
    /// Объект округления.
    /// </param>
    /// <param name="n">
    /// Требуемое число десятичных знаков после запятой.
    /// </param>
    /// <returns>
    /// Результат округления.
    /// </returns>
    public static double Rounding(double x, int n)
    {
        double factor = Math.Pow(10, n);
        return Math.Round(x * factor) / factor;            
    }

    /// <summary>
    /// Приведение к диапазону.
    /// </summary>
    /// <param name="x">
    /// Приводимое значение.
    /// </param>
    /// <param name="r">
    /// Характерный масштаб диапазона.
    /// </param>
    /// <returns>
    /// Приведённое значение.
    /// </returns>
    public static double SetToRange(double x, double r)
    {
        return r * Math.Round(x / r);
    }

    /// <summary>
    /// Приведение к диапазону.
    /// </summary>
    /// <param name="x">
    /// Приводимое значение.
    /// </param>
    /// <param name="r">
    /// Характерный масштаб диапазона.
    /// </param>
    /// <returns>
    /// Приведённое значение.
    /// </returns>
    public static int SetToRange(double x, int r)
    {
        return r * (int)Math.Round(x / r);
    }

    /// <summary>
    /// Тестовая функция.
    /// </summary>
    /// <param name="matrix">
    /// Двумерный массив.
    /// </param>
    /// <param name="m">
    /// Число строк.
    /// </param>
    /// <param name="n">
    /// Число столбцов.
    /// </param>
    public static void Test4Handles(double[,] matrix, int m, int n)
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] += 1.0;
            }
        }
    }
}
