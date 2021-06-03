using System;
using System.Threading.Tasks;

namespace MultMatrix
{
    public class Matrix
    {
        private static Matrix _instance;

        private Matrix() { }

        public static Matrix GetInstance()
        {
            if (_instance == null)
                _instance = new Matrix();
            return _instance;
        }


        public int[,] GetMatrixFromConsole(string name)
        {
            Console.Write($"Кiлькiсть рядкiв матрицi: {name} ");
            var n = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.Write($"Кiлькiсть стовпцiв матрицi: {name} ");
            var m = int.Parse(Console.ReadLine() ?? string.Empty);

            var rnd = new Random();
            var matrix = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = rnd.Next(1, 10);
                }
            }

            return matrix;
        }

        public int[,] ParallelMatrixMultiplication(int[,] matrixA, int[,] matrixB)
        {
            int tLength = matrixA.GetLength(0);

            if (matrixA.ColumnsCount() != matrixB.RowsCount())
                throw new Exception("Множення не можливе!");

            var matrixC = new int[matrixA.RowsCount(), matrixB.ColumnsCount()];

            Parallel.For(0, tLength, delegate (int i)
            {
                for (int j = 0; j < tLength; j++)
                {
                    int tmp = 0;

                    for (int k = 0; k < tLength; k++) tmp += matrixA[i, k] * matrixB[k, j];

                    matrixC[i, j] = tmp;
                }
            });

            return matrixC;
        }

        public int[,] ParallelMatrixMultiplication2(int[,] matrixA, int[,] matrixB)
        {
            int tLength = matrixA.GetLength(0);

            if (matrixA.ColumnsCount() != matrixB.RowsCount())
                throw new Exception("Множення не можливе!");

            var matrixC = new int[matrixA.RowsCount(), matrixB.ColumnsCount()];

            Parallel.For(0, tLength, i =>
                Parallel.For(0, tLength, j =>
            {
                int tmp = 0;

                Parallel.For(0, tLength, k => tmp += matrixA[i, k] * matrixB[k, j]);

                matrixC[i, j] = tmp;
            }));

            return matrixC;
        }
        public int[,] MatrixMultiplication(int[,] matrixA, int[,] matrixB)
        {
            if (matrixA.ColumnsCount() != matrixB.RowsCount())
                throw new Exception("Множення не можливе!");

            var matrixC = new int[matrixA.RowsCount(), matrixB.ColumnsCount()];

            for (var i = 0; i < matrixA.RowsCount(); i++)
            {
                for (var j = 0; j < matrixB.ColumnsCount(); j++)
                {
                    matrixC[i, j] = 0;

                    for (var k = 0; k < matrixA.ColumnsCount(); k++) matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }

            return matrixC;
        }

        public void PrintMatrix(int[,] matrix)
        {
            for (var i = 0; i < matrix.RowsCount(); i++)
            {
                for (var j = 0; j < matrix.ColumnsCount(); j++)
                {
                    Console.Write(matrix[i, j].ToString().PadLeft(4));
                }

                Console.WriteLine();
            }
        }
    }

    static class MatrixExt
    {
        public static int RowsCount(this int[,] matrix) => matrix.GetUpperBound(0) + 1;

        public static int ColumnsCount(this int[,] matrix) => matrix.GetUpperBound(1) + 1;
    }
}
