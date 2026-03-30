using System;

namespace Simargl.Mathematics
{
    /// <summary>
    /// Класс, представляющий матрицы и операции с ними.
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// Число строк.
        /// </summary>
        public int RowsCount { get; private set; }

        /// <summary>
        /// Число столбцов.
        /// </summary>
        public int ColumnsCount { get; private set; }

        /// <summary>
        /// Массив элементов.
        /// </summary>
        public double[,] Entries { get; private set; }

        /// <summary>
        /// Задать или считать элемент матрицы с заданной позицией (строки и столбцы нумеруются начиная с 0).
        /// </summary>
        /// <param name="row">
        /// Номер строки.
        /// </param>
        /// <param name="column">
        /// Номер столбца.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public double this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= RowsCount)
                {
                    throw new ArgumentOutOfRangeException("rowsCount", "Номер строки не корректен!");
                }

                if (column < 0 || column >= ColumnsCount)
                {
                    throw new ArgumentOutOfRangeException("columnsCount", "Номер столбца не корректен!");
                }

                return Entries[row, column];
            }

            set
            {
                if (row < 0 || row >= RowsCount)
                {
                    throw new ArgumentOutOfRangeException("rowsCount", "Номер строки не корректен!");
                }

                if (column < 0 || column >= ColumnsCount)
                {
                    throw new ArgumentOutOfRangeException("columnsCount", "Номер столбца не корректен!");
                }

                Entries[row, column] = value;
            }

        }

        /// <summary>
        /// Конструктор. Создаёт новую матрицу заданного размера с заданным массивом элементов.
        /// </summary>
        /// <param name="rowsCount">
        /// Число строк.
        /// </param>
        /// <param name="columnsCount">
        /// Число столбцов.
        /// </param>
        /// <param name="array">
        /// Массив элементов.
        /// </param>
        public Matrix(int rowsCount, int columnsCount, double[,] array)
        {
            if (rowsCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rowsCount), "Передано не положительное число строк!");
            }

            if (columnsCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columnsCount), "Передано не положительное число столбцов!");
            }

            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            Entries = array;
        }

        /// <summary>
        /// Конструктор. Создаёт новую матрицу заданного размера с нулевыми элементами.
        /// </summary>
        /// <param name="rowsCount">
        /// Число строк.
        /// </param>
        /// <param name="columnsCount">
        /// Число столбцов.
        /// </param>
        public Matrix(int rowsCount, int columnsCount)
        {
            if (rowsCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rowsCount), "Передано не положительное число строк!");
            }

            if (columnsCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columnsCount), "Передано не положительное число столбцов!");
            }

            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            Entries = new double[rowsCount, columnsCount];
        }
        
        /// <summary>
        /// Сложение матриц.
        /// </summary>
        /// <param name="matrixLeft">
        /// Левое слагаемое.
        /// </param>
        /// <param name="matrixRight">
        /// Правое слагаемое.
        /// </param>
        /// <returns>
        /// Сумма входящих матриц.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Если размеры матриц-аргументов не совпадают, выдаётся сообщение об ошибке.
        /// </exception>
        public static Matrix operator +(Matrix matrixLeft, Matrix matrixRight)
        {
            int m = matrixLeft.RowsCount;
            int n = matrixLeft.ColumnsCount;

            if (m != matrixRight.RowsCount || n != matrixRight.ColumnsCount)
            {
                throw new ArgumentOutOfRangeException("Сложение матриц", "Размеры слагаемых не совпадают!");
            }

            Matrix matrixSum = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrixSum[i, j] = matrixLeft[i, j] + matrixRight[i, j];
                }
            }

            return matrixSum;
        }

        /// <summary>
        /// Сложение матрицы с числом (матрицей с одинаковыми компонентами).
        /// </summary>
        /// <param name="matrix">
        /// Матрица.
        /// </param>
        /// <param name="scalar">
        /// Число.
        /// </param>
        /// <returns>
        /// Сумма исходной матрицы и матрицы с одинаковыми компонентами.
        /// </returns>
        public static Matrix operator +(Matrix matrix, double scalar)
        {
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;
            Matrix matrixSum = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrixSum[i, j] = matrix[i, j] + scalar;
                }
            }

            return matrixSum;
        }

        /// <summary>
        /// Сложение матрицы с числом (матрицей с одинаковыми компонентами).
        /// </summary>
        /// <param name="scalar">
        /// Число.
        /// </param>
        /// <param name="matrix">
        /// Матрица.
        /// </param>
        /// <returns>
        /// Сумма исходной матрицы и матрицы с одинаковыми компонентами.
        /// </returns>
        public static Matrix operator +(double scalar, Matrix matrix)
        {
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;
            Matrix matrixSum = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrixSum[i, j] = scalar + matrix[i, j];
                }
            }

            return matrixSum;
        }

        /// <summary>
        /// Вычитание матриц.
        /// </summary>
        /// <param name="matrixLeft">
        /// Уменьшаемое.
        /// </param>
        /// <param name="matrixRight">
        /// Вычитаемое.
        /// </param>
        /// <returns>
        /// Разность входящих матриц.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Если размеры матриц-аргументов не совпадают, выдаётся сообщение об ошибке.
        /// </exception>
        public static Matrix operator -(Matrix matrixLeft, Matrix matrixRight)
        {
            int m = matrixLeft.RowsCount;
            int n = matrixLeft.ColumnsCount;

            if (m != matrixRight.RowsCount || n != matrixRight.ColumnsCount)
            {
                throw new ArgumentOutOfRangeException("Вычитание матриц", "Размеры аргументов не совпадают!");
            }

            Matrix matrixDiff = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrixDiff[i, j] = matrixLeft[i, j] - matrixRight[i, j];
                }
            }

            return matrixDiff;
        }

        /// <summary>
        /// Вычитание числа из матрицы.
        /// </summary>
        /// <param name="matrix">
        /// Матрица.
        /// </param>
        /// <param name="scalar">
        /// Число.
        /// </param>
        /// <returns>
        /// Разность исходной матрицы и матрицы с одинаковыми компонентами.
        /// </returns>
        public static Matrix operator -(Matrix matrix, double scalar)
        {
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;

            Matrix matrixDiff = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrixDiff[i, j] = matrix[i, j] - scalar;
                }
            }

            return matrixDiff;
        }

        /// <summary>
        /// Противоположная (обратная по сложению) матрица.
        /// </summary>
        /// <param name="matrix">
        /// Исходная матрица.
        /// </param>
        /// <returns>
        /// Противоположная матрица.
        /// </returns>
        public static Matrix operator -(Matrix matrix)
        {
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;
            
            Matrix oppositeMatrix = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    oppositeMatrix[i, j] = -matrix[i, j];
                }
            }

            return oppositeMatrix;
        }

        /// <summary>
        /// Умножение матриц.
        /// </summary>
        /// <param name="matrixLeft">
        /// Левый сомножитель.
        /// </param>
        /// <param name="matrixRight">
        /// Правый сомножитель.
        /// </param>
        /// <returns>
        /// Произведение двух матриц.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Если размеры матриц-аргументов не согласованы, выдаётся сообщение об ошибке.
        /// </exception>
        public static Matrix operator *(Matrix matrixLeft, Matrix matrixRight)
        {
            int m = matrixLeft.RowsCount;
            int c = matrixLeft.ColumnsCount;
            int n = matrixRight.ColumnsCount;

            if (c != matrixRight.RowsCount)
            {
                throw new ArgumentOutOfRangeException("Умножение матриц", "Размеры сомножителей не согласованы!");
            }

            Matrix matrixProduct = new(m, n);

            double currentSum;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    currentSum = 0.0;
                    for (int k = 0; k < c; k++)
                    {
                        currentSum += matrixLeft[i, k] * matrixRight[k, j];
                    }

                    matrixProduct[i, j] = currentSum;
                }
            }

            return matrixProduct;
        }

        /// <summary>
        /// Умножение матрицы на число.
        /// </summary>
        /// <param name="scalar">
        /// Числовой множитель.
        /// </param>
        /// <param name="matrix">
        /// Матричный множитель.
        /// </param>
        /// <returns>
        /// Произведение матрицы на число.
        /// </returns>
        public static Matrix operator *(double scalar, Matrix matrix)
        {
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;
            Matrix scaledMatrix = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    scaledMatrix[i, j] = scalar * matrix[i, j];
                }
            }

            return scaledMatrix;
        }

        /// <summary>
        /// Умножение матрицы на число.
        /// </summary>
        /// <param name="matrix">
        /// Матричный множитель.
        /// </param>
        /// <param name="scalar">
        /// Числовой множитель.
        /// </param>
        /// <returns>
        /// Произведение матрицы на число.
        /// </returns>
        public static Matrix operator *(Matrix matrix, double scalar)
        {
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;
            Matrix scaledMatrix = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    scaledMatrix[i, j] = matrix[i, j] * scalar;
                }
            }

            return scaledMatrix;
        }

        /// <summary>
        /// Деление матрицы на число.
        /// </summary>
        /// <param name="matrix">
        /// Матричный аргумент.
        /// </param>
        /// <param name="scalar">
        /// Скалярный аргумент.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Matrix operator /(Matrix matrix, double scalar)
        {
            if (Math.Abs(scalar) <= Double.Epsilon)
            {
                throw new ArgumentOutOfRangeException("Матрица/число", "Делитель близок к нулю!");
            }

            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;
            Matrix scaledMatrix = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    scaledMatrix[i, j] = matrix[i, j] / scalar;
                }
            }

            return scaledMatrix;
        }

        /// <summary>
        /// Копия объекта (матрица).        
        /// </summary>
        /// <param name="matrix">
        /// Исходная матрица.
        /// </param>
        /// <returns>
        /// Копия исходной матрицы.
        /// </returns>
        public static Matrix Copy(Matrix matrix)
        {
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;

            Matrix newMatrix = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }
            
            return newMatrix;
        }

        /// <summary>
        /// Вывод матрицы на консоль.
        /// </summary>
        /// <returns>
        /// Строка
        /// </returns>
        public override string ToString()
        {
            string result = "[";

            for (int row = 0; row < RowsCount; row++)
            {
                if (row != 0)
                {
                    result += "\n";
                }
                for (int column = 0; column < ColumnsCount; column++)
                {
                    if (column != 0)
                    {
                        result += ", ";
                    }
                    result += Entries[row, column].ToString("0.0000000");
                }
            }
            return result + "]";
        }

        /// <summary>
        /// Транспонирование матрицы с созданием нового объекта.
        /// </summary>
        /// <param name="matrix">
        /// Исходная матрица.
        /// </param>
        /// <returns>
        /// Матрица, транспонированная по отношению к исходной.
        /// </returns>
        public static Matrix Transpose(Matrix matrix)
        {
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;

            Matrix matrixTransposed = new(n, m);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrixTransposed[i, j] = matrix[j, i];
                }
            }

            return matrixTransposed;
        }

        /// <summary>
        /// Транспонирование матрицы без создания нового объекта.
        /// </summary>
        public void Transpose()
        {
            int m = RowsCount;
            int n = ColumnsCount;

            double[,] newEntries = new double[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    newEntries[i, j] = Entries[j, i];
                }
            }

            RowsCount = n;
            ColumnsCount = m;
            Entries = newEntries;
        }

        /// <summary>
        /// Горизонтальная конкатенация матриц.
        /// </summary>
        /// <param name="leftMatrix">
        /// Левый операнд.
        /// </param>
        /// <param name="rightMatrix">
        /// Правый операнд.
        /// </param>
        /// <returns>
        /// Результат горизонтальной конкатенации.
        /// </returns>
        public static Matrix HorizontalConcatenation(Matrix leftMatrix, Matrix rightMatrix)
        {
            int m1 = leftMatrix.RowsCount;     // число строк левого операнда
            int n1 = leftMatrix.ColumnsCount;  // число столбцов левого операнда

            int m2 = rightMatrix.RowsCount;     // число строк правого операнда
            int n2 = rightMatrix.ColumnsCount;  // число столбцов правого операнда

            int m = Math.Min(m1, m2);
            int n = n1 + n2;

            Matrix resultMatrix = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j < n1)
                    {
                        resultMatrix[i, j] = leftMatrix[i, j];
                    }
                    else
                    {
                        resultMatrix[i, j] = rightMatrix[i, j - n1];
                    }
                }
            }

            return resultMatrix;
        }

        /// <summary>
        /// Вертикальная конкатенация матриц.
        /// </summary>
        /// <param name="topMatrix">
        /// Верхний операнд.
        /// </param>
        /// <param name="bottomMatrix">
        /// Нижний операнд.
        /// </param>
        /// <returns>
        /// Результат вертикальной конкатенации.
        /// </returns>
        public static Matrix VerticalConcatenation(Matrix topMatrix, Matrix bottomMatrix)
        {
            int m1 = topMatrix.RowsCount;     // число строк верхнего операнда
            int n1 = topMatrix.ColumnsCount;  // число столбцов верхнего операнда

            int m2 = bottomMatrix.RowsCount;     // число строк нижнего операнда
            int n2 = bottomMatrix.ColumnsCount;  // число столбцов нижнего операнда

            int m = m1 + m2;
            int n = Math.Min(n1, n2);

            Matrix resultMatrix = new(m, n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i < m1)
                    {
                        resultMatrix[i, j] = topMatrix[i, j];
                    }
                    else
                    {
                        resultMatrix[i, j] = bottomMatrix[i - m1, j];
                    }
                }
            }

            return resultMatrix;
        }

        /// <summary>
        /// Возвращает матрицу заданного размера, состоящую из единиц на главной диагонали и нулей на остальных позициях.
        /// </summary>
        /// <param name="m">
        /// Число строк.
        /// </param>
        /// <param name="n">
        /// Число столбцов.
        /// </param>
        /// <returns>
        /// Матрица, состоящая из единиц на главной диагонали и нулей на остальных позициях.
        /// </returns>
        public static Matrix Eye(int m, int n)
        {
            Matrix eye = new(m, n);

            int k = Math.Min(m, n);

            for (int i = 0; i < k; i++)
            {
                eye[i, i] = 1;
            }
                        
            return eye;
        }

        /// <summary>
        /// Возвращает матрицу размера как у входного аргумента, состоящую из единиц на главной диагонали и нулей на остальных позициях.
        /// </summary>
        /// <param name="matrix">
        /// Матрица, задающая размер.
        /// </param>
        /// <returns>
        /// Матрица, состоящая из единиц на главной диагонали и нулей на остальных позициях.
        /// </returns>
        public static Matrix Eye(Matrix matrix)
        {
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;
                        
            return Eye(m, n);
        }

        /// <summary>
        /// Возвращает подматрицу исходной матрицы с заданными номерами строк и столбцов.
        /// </summary>
        /// <param name="matrix">
        /// Исходная матрица.
        /// </param>
        /// <param name="rowNumbers">
        /// Номера строк.
        /// </param>
        /// <param name="columnNumbers">
        /// Номера столбцов.
        /// </param>
        /// <returns>
        /// Подматрица исходной матрицы с заданными номерами строк и столбцов.
        /// </returns>
        public static Matrix ExtractSubMatrix(Matrix matrix, int[] rowNumbers, int[] columnNumbers)
        {
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;

            int ms = rowNumbers.Length;
            int ns = columnNumbers.Length;

            Matrix submatrix = new(ms, ns);
            for (int i = 0; i < ms; i++)
            {
                for (int j = 0; j < ns; j++)
                {
                    submatrix[i, j] = matrix[rowNumbers[i], columnNumbers[j]];
                }
            }
            return submatrix;
        }

        /// <summary>
        /// Возвращает строку матрицы с заданным номером.
        /// </summary>
        /// <param name="matrix">
        /// Исходная матрица.
        /// </param>
        /// <param name="rowNumber">
        /// Номер строки.
        /// </param>
        /// <returns>
        /// Строка матрицы с заданным номером.
        /// </returns>
        /// <exception cref="Exception">
        /// Исключение на случай некорректно заданной строки.
        /// </exception>
        public static Matrix ExtractRow(Matrix matrix, int rowNumber)
        {
            int m = matrix.RowsCount;
            if (rowNumber < 0 || rowNumber >= m)
            {
                throw new Exception("Некорректный номер строки.");
            }

            int n = matrix.ColumnsCount;
            Matrix rowMatrix = new(1, n);

            for (int i = 0; i < n; i++)
            {
                rowMatrix[0, i] = matrix[rowNumber, i];
            }

            return rowMatrix;
        }

        /// <summary>
        /// Возвращает столбец матрицы с заданным номером.
        /// </summary>
        /// <param name="matrix">
        /// Исходная матрица.
        /// </param>
        /// <param name="columnNumber">
        /// Номер столбца.
        /// </param>
        /// <returns>
        /// Столбец матрицы с заданным номером.
        /// </returns>
        /// <exception cref="Exception">
        /// Исключение на случай некорректно заданного столбца.
        /// </exception>
        public static Matrix ExtractColumn(Matrix matrix, int columnNumber)
        {
            int n = matrix.ColumnsCount;
            if (columnNumber < 0 || columnNumber >= n)
            {
                throw new Exception("Некорректный номер столбца.");
            }

            int m = matrix.RowsCount;
            Matrix columnMatrix = new(m, 1);

            for (int i = 0; i < m; i++)
            {
                columnMatrix[i, 0] = matrix[i, columnNumber];
            }

            return columnMatrix;
        }

        /// <summary>
        /// Скалярное произведение матриц.
        /// </summary>
        /// <param name="matrix1">
        /// Первый сомножитель.
        /// </param>
        /// <param name="matrix2">
        /// Второй сомножитель.
        /// </param>
        /// <returns>
        /// Сумма произведений одноимённых компонент.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Если размеры матриц-аргументов не совпадают, выдаётся сообщение об ошибке.
        /// </exception>
        public static double DotProduct(Matrix matrix1, Matrix matrix2)
        {
            int m = matrix1.RowsCount;
            int n = matrix1.ColumnsCount;

            int m2 = matrix2.RowsCount;
            int n2 = matrix2.ColumnsCount;

            if (m != m2 || n != n2)
            {
                throw new ArgumentOutOfRangeException("DotProduct", "Размеры сомножителей не совпадают!");
            }

            double s = 0.0;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    s += matrix1[i, j] * matrix2[i, j];
                }
            }

            return s;
        }

        /// <summary>
        /// Скалярный квадрат матрицы.
        /// </summary>
        /// <returns>
        /// Сумма квадратов компонент.
        /// </returns>
        public double DotSquare()
        {
            double s = 0.0;

            double currentEntry;

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    currentEntry = Entries[i, j];
                    s += currentEntry * currentEntry;
                }
            }

            return s;
        }

        /// <summary>
        /// Скалярный квадрат матрицы.
        /// </summary>
        /// <param name="matrix">
        /// Матрица.
        /// </param>
        /// <returns>
        /// Сумма квадратов компонент.
        /// </returns>
        public static double DotSquare(Matrix matrix)
        {
            return DotProduct(matrix, matrix);
        }

        /// <summary>
        /// Определитель 2-на-2 матрицы.
        /// </summary>
        /// <param name="A">
        /// Исходная матрица.
        /// </param>
        /// <returns>
        /// Детерминант заданной матрицы.
        /// </returns>
        public static double Det2(Matrix A)
        {
            if (A.RowsCount != 2 || A.ColumnsCount != 2)
            {
                throw new ArgumentOutOfRangeException("Det2", "Размеры матрицы должны быть 2 на 2!");
            }

            return A[0, 0] * A[1, 1] - A[1, 0] * A[0, 1];
        }

        /// <summary>
        /// Определитель 2-на-2 матрицы.
        /// </summary>
        /// <returns>
        /// Детерминант матрицы.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public double Det2()
        {
            if (RowsCount != 2 || ColumnsCount != 2)
            {
                throw new ArgumentOutOfRangeException("Det2", "Размеры матрицы должны быть 2 на 2!");
            }

            return Entries[0, 0] * Entries[1, 1] - Entries[1, 0] * Entries[0, 1];
        }

        /// <summary>
        /// Определитель 3-на-3 матрицы.
        /// </summary>
        /// <param name="A">
        /// Исходная матрица.
        /// </param>
        /// <returns>
        /// Детерминант заданной матрицы.
        /// </returns>
        public static double Det3(Matrix A)
        {
            if (A.RowsCount != 3 || A.ColumnsCount != 3)
            {
                throw new ArgumentOutOfRangeException("Det3", "Размеры матрицы должны быть 3 на 3!");
            }

            double dp = A[0, 0] * A[1, 1] * A[2, 2] + A[2, 0] * A[0, 1] * A[1, 2] + A[1, 0] * A[0, 2] * A[2, 1];
            double dm = A[2, 0] * A[1, 1] * A[0, 2] + A[0, 0] * A[1, 2] * A[2, 1] + A[2, 2] * A[1, 0] * A[0, 1];
            return dp - dm;
        }

        /// <summary>
        /// Определитель 3-на-3 матрицы.
        /// </summary>
        /// <returns>
        /// Детерминант матрицы.
        /// </returns>
        public double Det3()
        {
            if (RowsCount != 3 || ColumnsCount !=3)
            {
                throw new ArgumentOutOfRangeException("Det3", "Размеры матрицы должны быть 3 на 3!");
            }

            double dp = Entries[0, 0] * Entries[1, 1] * Entries[2, 2] + Entries[2, 0] * Entries[0, 1] * Entries[1, 2] + Entries[1, 0] * Entries[0, 2] * Entries[2, 1];
            double dm = Entries[2, 0] * Entries[1, 1] * Entries[0, 2] + Entries[0, 0] * Entries[1, 2] * Entries[2, 1] + Entries[2, 2] * Entries[1, 0] * Entries[0, 1];
            return dp - dm;
        }

        /// <summary>
        /// Обращение матрицы 2-го порядка.
        /// </summary>
        /// <param name="A">
        /// Исходная матрица.
        /// </param>
        /// <returns>
        /// Матрица, обратная к исходной.
        /// </returns>
        public static Matrix Inv2(Matrix A)
        {
            if (A.RowsCount != 2 || A.ColumnsCount != 2)
            {
                throw new ArgumentOutOfRangeException("Inv2", "Размеры матрицы должны быть 2 на 2!");
            }

            double d = Det2(A);

            if (Math.Abs(d) < Double.Epsilon)
            {
                throw new Exception("Inv2: Матрица близка к вырожденной!");
            }

            Matrix A1 = new(2, 2);

            A1[0, 0] = A[1, 1] / d;
            A1[0, 1] = -A[0, 1] / d;
            A1[1, 0] = -A[1, 0] / d;
            A1[1, 1] = A[0, 0] / d;

            return A1;
        }

        /// <summary>
        /// Обращение матрицы 3-го порядка.
        /// </summary>
        /// <param name="A">
        /// Исходная матрица.
        /// </param>
        /// <returns>
        /// Матрица, обратная к исходной.
        /// </returns>
        public static Matrix Inv3(Matrix A)
        {
            if (A.RowsCount != 3 || A.ColumnsCount != 3)
            {
                throw new ArgumentOutOfRangeException("Inv3", "Размеры матрицы должны быть 3 на 3!");
            }

            double d = Det3(A);

            if (Math.Abs(d) < Double.Epsilon)
            {
                throw new Exception("Inv3: Матрица близка к вырожденной!");
            }

            Matrix A1 = new(3, 3);

            A1[0, 0] = (A[1, 1] * A[2, 2] - A[2, 1] * A[1, 2]) / d;
            A1[0, 1] = (A[2, 1] * A[0, 2] - A[2, 2] * A[0, 1]) / d;
            A1[0, 2] = (A[0, 1] * A[1, 2] - A[1, 1] * A[0, 2]) / d;

            A1[1, 0] = (A[2, 0] * A[1, 2] - A[1, 0] * A[2, 2]) / d;
            A1[1, 1] = (A[0, 0] * A[2, 2] - A[2, 0] * A[0, 2]) / d;
            A1[1, 2] = (A[1, 0] * A[0, 2] - A[0, 0] * A[1, 2]) / d;

            A1[2, 0] = (A[1, 0] * A[2, 1] - A[2, 0] * A[1, 1]) / d;
            A1[2, 1] = (A[2, 0] * A[0, 1] - A[0, 0] * A[2, 1]) / d;
            A1[2, 2] = (A[0, 0] * A[1, 1] - A[1, 0] * A[0, 1]) / d;

            return A1;
        }

        /// <summary>
        /// Норма матрицы: интегральная, евклидова или равномерная.
        /// </summary>
        /// <param name="normType">
        /// Тип нормы - строка из списка: "integral", "euclidian", "uniform".
        /// </param>
        /// <returns>
        /// Значение нормы.
        /// </returns>
        public double Norm(string normType)
        {
            double normValue = 0.0;

            if (normType == "integral")
            {
                for (int i = 0; i < RowsCount; i++)
                {
                    for (int j = 0; j < ColumnsCount; j++)
                    {
                        normValue += Math.Abs(Entries[i, j]);
                    }
                }
            }

            if (normType == "euclidian")
            {
                for (int i = 0; i < RowsCount; i++)
                {
                    for (int j = 0; j < ColumnsCount; j++)
                    {
                        normValue += Math.Pow(Entries[i, j], 2);
                    }
                }

                normValue = Math.Sqrt(normValue);
            }

            if (normType == "uniform")
            {
                double currentEntry;

                for (int i = 0; i < RowsCount; i++)
                {
                    for (int j = 0; j < ColumnsCount; j++)
                    {
                        currentEntry = Math.Abs(Entries[i, j]);

                        if (currentEntry > normValue)
                        {
                            normValue = currentEntry;
                        }
                    }
                }
            }
            
            return normValue;
        }
                
        /// <summary>
        /// Псевдообращение матрицы-строки или матрицы-столбца.
        /// </summary>
        /// <param name="vector">
        /// Исходная матрица: вектор-строка или вектор-столбец.
        /// </param>
        /// <returns>
        /// Псевдообратная матрица (столбец или строка).
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Если матрица не является ни строкой ни столбцом, выдаётся сообщение об ошибке.
        /// </exception>
        public static Matrix PseudoInversionVector(Matrix vector)
        {
            int m = vector.RowsCount;
            int n = vector.ColumnsCount;

            if (m != 1 && n != 1)
            {
                throw new ArgumentOutOfRangeException("PseudoInversionVector", "Размеры входной матрицы не корректны!");
            }

            //double norm2 = DotSquare(vector);
            double norm2 = vector.DotSquare();

            if (norm2 <= double.Epsilon)
            {
                return new Matrix(n, m);
            }

            return Transpose(vector) / norm2;
        }

        /// <summary>
        /// Псевдообращение матрицы по алгоритму Гревилля.
        /// </summary>
        /// <param name="matrix">
        /// Исходная матрица.
        /// </param>
        /// <returns>
        /// Матрица, псевдообратная к исходной.
        /// </returns>
        public static Matrix PseudoInversionGreville(Matrix matrix)
        {
            /* Greville T.N.E.
             1. The pseudoinverse of a rectangular matrix and its applications to the solution of systems of linear equations. SIAM Rev., 1 (1959), 38—43.
             2. Some applications of the pseudoinverse of a matrix. SIAM Rev., 2 (1960), 15—22.
             3. Note on the generalized inverse of a matrix product. SIAM Rev., 8 (1966), 518—521. Erratum 9 (1967) 7.
             */
        
            int m = matrix.RowsCount;
            int n = matrix.ColumnsCount;
            
            if (m == 1 || n==1)
            {
                return PseudoInversionVector(matrix);                 
            }

            bool mt = false;

            if (m < n)
            {
                matrix.Transpose();
                //m = matrix.RowsCount;
                n = matrix.ColumnsCount;

                mt = true; // Исходная матрица транспонирована.
            }

            Matrix c0 = ExtractColumn(matrix, 0);
            Matrix p = PseudoInversionVector(c0);

            Matrix t;

            double n2q;

            for (int k = 1; k < n; k++)
            {
                Matrix product = c0 * p;

                Matrix c1 = ExtractColumn(matrix, k);
                Matrix q = (Eye(product) - product) * c1;
                n2q = q.DotSquare();

                if (n2q > double.Epsilon)
                {
                    t = Transpose(q / n2q);
                }
                else
                {
                   Matrix a = p * c1;
                   t = Transpose(Transpose(p) * a / (1 + DotSquare(a)));
                }

                Matrix ct = c1 * t;
                                
                p = VerticalConcatenation(p * (Eye(ct) - ct), t);
                c0 = HorizontalConcatenation(c0, c1);
            }

            if (mt) // Если исходная матрица была транспонирована...
            {
                p.Transpose();
                matrix.Transpose();
            }

            return p;
        }

    }
}
