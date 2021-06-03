using System;
using System.Diagnostics;

namespace MultMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            var matrixA = Matrix.GetInstance().GetMatrixFromConsole("A");
            var matrixB = Matrix.GetInstance().GetMatrixFromConsole("B");

            //Console.WriteLine("Матриця A:");
            //Matrix.getInstance().PrintMatrix(matrixA);
            //Console.WriteLine("Матриця B:");
            //Matrix.getInstance().PrintMatrix(matrixB);

            sw.Start();
            var matrixC1 = Matrix.GetInstance().ParallelMatrixMultiplication(matrixA, matrixB);
            sw.Stop();
            Console.WriteLine($"Parallel: {sw.Elapsed}");

            sw.Restart();
            var matrixC2 = Matrix.GetInstance().ParallelMatrixMultiplication2(matrixA, matrixB);
            sw.Stop();
            Console.WriteLine($"Parallel2: {sw.Elapsed}");

            sw.Restart();
            var matrixC3 = Matrix.GetInstance().MatrixMultiplication(matrixA, matrixB);
            sw.Stop();
            Console.WriteLine($"Standart: {sw.Elapsed}");

            //Console.WriteLine("Матриця C:");
            //Matrix.getInstance().PrintMatrix(matrixC1);

            Console.ReadKey();
        }
    }
}
