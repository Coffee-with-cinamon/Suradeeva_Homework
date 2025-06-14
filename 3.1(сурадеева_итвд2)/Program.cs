using System;

namespace BinaryNumberOperations
{
    public class BinaryCalculator
    {
        private const int DefaultBitLength = 32;

        public static void Main()
        {
            var (firstNumber, secondNumber) = ReadInputNumbers();

            DisplayBinaryRepresentation("Первое число", firstNumber);
            DisplayBinaryRepresentation("Второе число", secondNumber);

            CalculateAndDisplaySum(firstNumber, secondNumber);
        }

        private static (int First, int Second) ReadInputNumbers()
        {
            Console.Write("Введите первое число: ");
            int first = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Введите второе число: ");
            int second = int.Parse(Console.ReadLine() ?? "0");

            return (first, second);
        }

        private static void DisplayBinaryRepresentation(string label, int number)
        {
            string binary = ConvertToPaddedBinary(number);
            Console.WriteLine($"{label} в двоичном виде: {binary}");
        }

        private static void CalculateAndDisplaySum(int a, int b)
        {
            int sum = a + b;
            string binarySum = ConvertToPaddedBinary(sum);

            Console.WriteLine($"Сумма в двоичном виде:        {binarySum}");
            Console.WriteLine($"Сумма в десятичном виде: {sum}");
        }

        private static string ConvertToPaddedBinary(int number, int bitLength = DefaultBitLength)
        {
            return Convert.ToString(number, 2).PadLeft(bitLength, '0');
        }
    }
}