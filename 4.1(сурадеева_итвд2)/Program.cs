using System;

class SpiralMatrixTraversal
{
    static void Main()
    {
        int matrixSize = ReadOddNumber();
        int[,] matrix = CreateSquareMatrix(matrixSize);

        PrintMatrix(matrix);
        Console.WriteLine("\nОбход по спирали:");
        TraverseSpirally(matrix);
    }

    static int ReadOddNumber()
    {
        int number;
        do
        {
            Console.Write("Введите нечетное N: ");
            number = Convert.ToInt32(Console.ReadLine());

            if (number % 2 == 0)
                Console.WriteLine("N должно быть нечетным, введите другое N");
        } while (number % 2 == 0);

        return number;
    }

    static int[,] CreateSquareMatrix(int size)
    {
        int[,] matrix = new int[size, size];
        int value = 1;

        for (int row = 0; row < size; row++)
            for (int col = 0; col < size; col++)
                matrix[row, col] = value++;

        return matrix;
    }

    static void PrintMatrix(int[,] matrix)
    {
        int size = matrix.GetLength(0);
        Console.WriteLine("Массив:");

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
                Console.Write($"{matrix[row, col],3} ");
            Console.WriteLine();
        }
    }

    static void TraverseSpirally(int[,] matrix)
    {
        int size = matrix.GetLength(0);
        int center = size / 2;
        int x = center, y = center;

        Console.Write(matrix[y, x] + " ");

        for (int step = 1; step < size; step++)
        {
            MoveAndPrint(ref x, ref y, step, matrix, Direction.Right);
            MoveAndPrint(ref x, ref y, step, matrix, Direction.Down);

            step++;
            if (step >= size) break;

            MoveAndPrint(ref x, ref y, step, matrix, Direction.Left);
            MoveAndPrint(ref x, ref y, step, matrix, Direction.Up);
        }
    }

    static void MoveAndPrint(ref int x, ref int y, int steps, int[,] matrix, Direction direction)
    {
        for (int i = 0; i < steps; i++)
        {
            switch (direction)
            {
                case Direction.Right: x++; break;
                case Direction.Down: y++; break;
                case Direction.Left: x--; break;
                case Direction.Up: y--; break;
            }
            Console.Write(matrix[y, x] + " ");
        }
    }

    enum Direction { Right, Down, Left, Up }
}