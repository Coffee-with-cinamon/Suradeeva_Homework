using System;

class IntegerDivisionCalculator
{
    static void Main()
    {
        (int dividend, int divisor) = ReadInputNumbers();

        if (divisor == 0)
        {
            Console.WriteLine("На ноль делить нельзя");
            return;
        }

        var result = CalculateDivision(dividend, divisor);

        Console.WriteLine($"Целая часть: {result.quotient}, остаток: {result.remainder}");
    }

    static (int dividend, int divisor) ReadInputNumbers()
    {
        Console.Write("Введите делимое: ");
        int dividend = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите делитель: ");
        int divisor = Convert.ToInt32(Console.ReadLine());

        return (dividend, divisor);
    }

    static (int quotient, int remainder) CalculateDivision(int dividend, int divisor)
    {
        int sign = DetermineResultSign(dividend, divisor);

        dividend = Math.Abs(dividend);
        divisor = Math.Abs(divisor);

        int quotient = 0;
        while (dividend >= divisor)
        {
            dividend -= divisor;
            quotient++;
        }

        return (sign * quotient, dividend);
    }

    static int DetermineResultSign(int a, int b)
    {
        return (a < 0 && b > 0) || (a > 0 && b < 0) ? -1 : 1;
    }
}
