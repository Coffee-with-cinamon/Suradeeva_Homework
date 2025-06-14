using System;

namespace ExponentialCalculator
{
    class Program
    {
        const double PrecisionThreshold = 0.000001;
        const double MinXValue = -1.0;
        const double MaxXValue = 1.0;

        static void Main()
        {
            double x = ReadInputValue();
            double exponential = CalculateExponential(x);
            Console.WriteLine($"e^x = {exponential:F6}");
        }

        static double ReadInputValue()
        {
            Console.WriteLine($"Введите x в диапазоне [{MinXValue}; {MaxXValue}]:");

            while (true)
            {
                double input = Convert.ToDouble(Console.ReadLine());

                if (input >= MinXValue && input <= MaxXValue)
                {
                    return input;
                }

                Console.WriteLine($"x должен быть в диапазоне [{MinXValue}; {MaxXValue}]. Повторите ввод:");
            }
        }

        static double CalculateExponential(double x)
        {
            double result = 1.0;
            double term;
            int n = 1;

            do
            {
                term = CalculateTerm(x, n);
                result += term;
                n++;
            }
            while (Math.Abs(term) >= PrecisionThreshold);

            return result;
        }

        static double CalculateTerm(double x, int n)
        {
            return Math.Pow(x, n) / Factorial(n);
        }

        static double Factorial(int number)
        {
            if (number == 0) return 1;

            double factorial = 1;
            for (int i = 1; i <= number; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
    }
}