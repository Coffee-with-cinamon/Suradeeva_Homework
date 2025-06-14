using System;
using System.Linq;

namespace ComplexNumberSystem
{
    public struct Complex : IEquatable<Complex>, IFormattable
    {
        public readonly double Real;
        public readonly double Imaginary;

        public static readonly Complex Zero = new Complex(0, 0);
        public static readonly Complex One = new Complex(1, 0);
        public static readonly Complex ImaginaryOne = new Complex(0, 1);

        public Complex(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public double Magnitude => Math.Sqrt(Real * Real + Imaginary * Imaginary);
        public double Phase => Math.Atan2(Imaginary, Real);

        public static Complex FromPolarCoordinates(double magnitude, double phase)
        {
            return new Complex(
                magnitude * Math.Cos(phase),
                magnitude * Math.Sin(phase));
        }

        public Complex Conjugate() => new Complex(Real, -Imaginary);

        public static Complex operator +(Complex left, Complex right)
        {
            return new Complex(left.Real + right.Real, left.Imaginary + right.Imaginary);
        }

        public static Complex operator -(Complex left, Complex right)
        {
            return new Complex(left.Real - right.Real, left.Imaginary - right.Imaginary);
        }

        public static Complex operator *(Complex left, Complex right)
        {
            return new Complex(
                left.Real * right.Real - left.Imaginary * right.Imaginary,
                left.Real * right.Imaginary + left.Imaginary * right.Real);
        }

        public static Complex operator /(Complex dividend, Complex divisor)
        {
            double denominator = divisor.Real * divisor.Real + divisor.Imaginary * divisor.Imaginary;
            return new Complex(
                (dividend.Real * divisor.Real + dividend.Imaginary * divisor.Imaginary) / denominator,
                (dividend.Imaginary * divisor.Real - dividend.Real * divisor.Imaginary) / denominator);
        }

        public Complex Pow(int exponent)
        {
            return FromPolarCoordinates(
                Math.Pow(Magnitude, exponent),
                Phase * exponent);
        }

        public Complex[] NthRoots(int degree)
        {
            if (degree <= 0)
                throw new ArgumentOutOfRangeException(nameof(degree), "Degree must be positive");

            double magnitude = Math.Pow(Magnitude, 1.0 / degree);
            Complex[] roots = new Complex[degree];

            for (int k = 0; k < degree; k++)
            {
                double angle = (Phase + 2 * Math.PI * k) / degree;
                roots[k] = FromPolarCoordinates(magnitude, angle);
            }

            return roots;
        }

        public bool Equals(Complex other)
        {
            return Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary);
        }

        public override bool Equals(object obj)
        {
            return obj is Complex other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Imaginary);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (Imaginary == 0) return Real.ToString(format, formatProvider);
            if (Real == 0) return $"{Imaginary.ToString(format, formatProvider)}i";

            string sign = Imaginary > 0 ? "+" : "-";
            return $"{Real.ToString(format, formatProvider)} {sign} {Math.Abs(Imaginary).ToString(format, formatProvider)}i";
        }

        public override string ToString()
        {
            return ToString("G", null);
        }

        public static bool operator ==(Complex left, Complex right) => left.Equals(right);
        public static bool operator !=(Complex left, Complex right) => !left.Equals(right);
    }

    class Program
    {
        static void Main()
        {
            Complex z1 = new Complex(3, 4);
            Complex z2 = new Complex(1, -2);
            Console.WriteLine($"z1 = {z1}");
            Console.WriteLine($"z2 = {z2}");
            Console.WriteLine($"z1 + z2 = {z1 + z2}");
            Console.WriteLine($"z1 * z2 = {z1 * z2}");
            Console.WriteLine($"z1 / z2 = {z1 / z2}");
            Console.WriteLine($"|z1| = {z1.Magnitude}");
            Console.WriteLine($"Фаза z1 = {z1.Phase} рад");
            Console.WriteLine($"z1^3 = {z1.Pow(3)}");

            Console.WriteLine("Кубические корни из z1:");
            foreach (var root in z1.NthRoots(3))
            {
                Console.WriteLine(root);
            }
        }
    }
}