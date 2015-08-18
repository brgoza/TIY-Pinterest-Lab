using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixChallenge
{
    class Program
    {
        public static int MaxProduct = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("How many rows/cols? ");
            int size = int.Parse(Console.ReadLine());
            int[,] matrix = GenerateMatrix(size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    MaxProduct = Math.Max(MaxProduct, HorizontalProdcuct(i, j, matrix));
                    MaxProduct = Math.Max(MaxProduct, VerticalProduct(i, j, matrix));
                    MaxProduct = Math.Max(MaxProduct, DownRightProduct(i, j, matrix));
                    MaxProduct = Math.Max(MaxProduct, UpRightProduct(i, j, matrix));
                }
            }
        }

        public static int HorizontalProdcuct(int x, int y, int[,] matrix)
        {
            var result = 1;
            for (int i = 0; i < 4; i++)
            {
                result = result * matrix[x + i, y];
            }
            return result;
        }

        public static int VerticalProduct(int x, int y, int[,] matrix)
        {

            var result = 1;
            for (int i = 0; i < 4; i++)
            {
                if (y + i < matrix.GetLength(0))
                    result = result * matrix[x, y + i];
            }
            return result;
        }

        public static int DownRightProduct(int x, int y, int[,] matrix)
        {

            var result = 1;
            for (int i = 0; i < 4; i++)
            {
                result = result * matrix[x + i, y + i];
            }
            return result;
        }

        public static int UpRightProduct(int x, int y, int[,] matrix)
        {

            var result = 1;
            for (int i = 0; i < 4; i++)
            {
                result = result * matrix[x + i, y - i];
            }
            return result;
        }

        public static int[,] GenerateMatrix(int size)
        {
            Random rnd = new Random();
            int[,] matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rnd.Next(0, 100);
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            return matrix;
        }
    }
}
