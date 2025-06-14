using System;

class CircleDrawer
{
    const int DefaultRadius = 6;

    static void Main()
    {
        int radius = DefaultRadius;
        DrawCircle(radius);
    }

    static void DrawCircle(int circleRadius)
    {
        int diameter = circleRadius * 2 + 1;

        for (int row = 0; row < diameter; row++)
        {
            for (int col = 0; col < diameter; col++)
            {
                char pixel = ShouldDrawPixel(col, row, circleRadius) ? '*' : ' ';
                Console.Write(pixel);
            }
            Console.WriteLine();
        }
    }

    static bool ShouldDrawPixel(int x, int y, int radius)
    {
        int center = radius;
        int dx = x - center;
        int dy = y - center;
        int distanceSquared = dx * dx + dy * dy;

        int outerBound = radius * radius + radius;
        int innerBound = radius * radius - radius;

        return distanceSquared <= outerBound && distanceSquared >= innerBound;
    }
}