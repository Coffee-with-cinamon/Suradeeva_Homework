using System;

namespace MathFunctions
{
    public class Calculator
    {
        public static void Main()
        {
            const int factorialNumber = 5;
            const int fibonacciNumber = 7;

            DisplayFactorialResults(factorialNumber);
            DisplayFibonacciResults(fibonacciNumber);
        }

        public static void DisplayFactorialResults(int number)
        {
            Console.WriteLine($"Факториал {number} (рекурсивно): {CalculateFactorialRecursive(number)}");
            Console.WriteLine($"Факториал {number} (итеративно): {CalculateFactorialIterative(number)}");
        }

        public static void DisplayFibonacciResults(int number)
        {
            Console.WriteLine($"Фибоначчи {number} (рекурсивно): {CalculateFibonacciRecursive(number)}");
            Console.WriteLine($"Фибоначчи {number} (итеративно): {CalculateFibonacciIterative(number)}");
        }

        private static int CalculateFactorialRecursive(int n)
        {
            return n <= 1 ? 1 : n * CalculateFactorialRecursive(n - 1);
        }

        private static int CalculateFactorialIterative(int n)
        {
            var result = 1;
            for (var i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        private static int CalculateFibonacciRecursive(int n)
        {
            return n <= 1 ? n : CalculateFibonacciRecursive(n - 1) + CalculateFibonacciRecursive(n - 2);
        }

        private static int CalculateFibonacciIterative(int n)
        {
            if (n <= 1) return n;

            var previous = 0;
            var current = 1;

            for (var i = 2; i <= n; i++)
            {
                (previous, current) = (current, previous + current);
            }

            return current;
        }
    }
}