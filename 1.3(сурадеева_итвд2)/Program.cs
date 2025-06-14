using System;

class QuadraticEquationSolver
{
    static void Main()
    {
        var coefficients = ReadCoefficients();
        SolveQuadraticEquation(coefficients.a, coefficients.b, coefficients.c);
    }

    static (double a, double b, double c) ReadCoefficients()
    {
        Console.WriteLine("Введите коэффициенты квадратного уравнения ax^2 + bx + c = 0");

        Console.Write("a: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("b: ");
        double b = Convert.ToDouble(Console.ReadLine());

        Console.Write("c: ");
        double c = Convert.ToDouble(Console.ReadLine());

        return (a, b, c);
    }

    static void SolveQuadraticEquation(double a, double b, double c)
    {
        if (a == 0)
        {
            // Линейное уравнение bx + c = 0
            if (b == 0)
            {
                Console.WriteLine("Уравнение не имеет решения (a = 0 и b = 0)");
                return;
            }
            Console.WriteLine($"Уравнение линейное, x = {-c / b}");
            return;
        }

        double discriminant = CalculateDiscriminant(a, b, c);
        Console.WriteLine($"Дискриминант = {discriminant}");

        if (discriminant < 0)
        {
            Console.WriteLine("Действительных корней нет");
        }
        else if (discriminant == 0)
        {
            double root = -b / (2 * a);
            Console.WriteLine($"Один действительный корень: x = {root}");
        }
        else
        {
            (double root1, double root2) = CalculateRoots(a, b, discriminant);
            Console.WriteLine($"Два действительных корня: x1 = {root1}, x2 = {root2}");
        }
    }

    static double CalculateDiscriminant(double a, double b, double c)
    {
        return b * b - 4 * a * c;
    }

    static (double, double) CalculateRoots(double a, double b, double discriminant)
    {
        double sqrtDiscriminant = Math.Sqrt(discriminant);
        double root1 = (-b + sqrtDiscriminant) / (2 * a);
        double root2 = (-b - sqrtDiscriminant) / (2 * a);
        return (root1, root2);
    }
}