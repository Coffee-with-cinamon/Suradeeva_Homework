using System;

class TriangleSimilarityChecker
{
    static void Main()
    {
        var triangle1 = ReadTriangle("первого");
        var triangle2 = ReadTriangle("второго");

        var orderedSides1 = OrderTriangleSides(triangle1);
        var orderedSides2 = OrderTriangleSides(triangle2);

        bool areSimilar = CheckTrianglesSimilarity(orderedSides1, orderedSides2);

        Console.WriteLine(areSimilar ? "Треугольники подобны" : "Треугольники не подобны");
    }

    static (int a, int b, int c) ReadTriangle(string triangleName)
    {
        Console.WriteLine($"Введите стороны треугольника {triangleName}:");

        Console.Write("Сторона a: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Сторона b: ");
        int b = int.Parse(Console.ReadLine());

        Console.Write("Сторона c: ");
        int c = int.Parse(Console.ReadLine());

        return (a, b, c);
    }

    static (int max, int min, int mid) OrderTriangleSides((int a, int b, int c) triangle)
    {
        int max = Math.Max(triangle.a, Math.Max(triangle.b, triangle.c));
        int min = Math.Min(triangle.a, Math.Min(triangle.b, triangle.c));
        int mid = triangle.a + triangle.b + triangle.c - max - min;

        return (max, min, mid);
    }

    static bool CheckTrianglesSimilarity(
        (int max1, int min1, int mid1) triangle1,
        (int max2, int min2, int mid2) triangle2)
    {
        double ratioMax = (double)triangle1.max1 / triangle2.max2;
        double ratioMin = (double)triangle1.min1 / triangle2.min2;
        double ratioMid = (double)triangle1.mid1 / triangle2.mid2;

        const double tolerance = 1e-10;
        return Math.Abs(ratioMax - ratioMin) < tolerance &&
               Math.Abs(ratioMin - ratioMid) < tolerance;
    }
}