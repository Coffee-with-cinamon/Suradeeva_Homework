using System;

class PrimeNumberFinder
{
    static void Main()
    {
        Console.Write("Введите верхнюю границу (n): ");
        int upperBound = int.Parse(Console.ReadLine());

        Console.WriteLine($"Простые числа до {upperBound}:");
        FindPrimesUpTo(upperBound);
    }

    static void FindPrimesUpTo(int limit)
    {
        for (int candidate = 2; candidate <= limit; candidate++)
        {
            if (IsPrime(candidate))
            {
                Console.WriteLine(candidate);
            }
        }
    }

    static bool IsPrime(int number)
    {
        if (number < 2) return false;

        int maxDivisor = (int)Math.Sqrt(number);
        for (int divisor = 2; divisor <= maxDivisor; divisor++)
        {
            if (number % divisor == 0)
            {
                return false;
            }
        }
        return true;
    }
}