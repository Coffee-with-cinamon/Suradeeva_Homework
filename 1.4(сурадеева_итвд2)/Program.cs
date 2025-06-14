using System;

class BitwiseMajorityVoter
{
    static void Main()
    {
        var numbers = ReadThreeNumbers();
        int result = CalculateMajorityBits(numbers.a, numbers.b, numbers.c);
        Console.WriteLine($"Результат: {result}");
    }

    static (int a, int b, int c) ReadThreeNumbers()
    {
        Console.WriteLine("Введите три целых числа:");

        Console.Write("Первое: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Второе: ");
        int b = int.Parse(Console.ReadLine());

        Console.Write("Третье: ");
        int c = int.Parse(Console.ReadLine());

        return (a, b, c);
    }

    static int CalculateMajorityBits(int a, int b, int c)
    {
        int result = 0;
        const int bitsInInt = sizeof(int) * 8;

        for (int bitPosition = 0; bitPosition < bitsInInt; bitPosition++)
        {
            if (HasMajorityOnBit(a, b, c, bitPosition))
            {
                result = SetBit(result, bitPosition);
            }
        }

        return result;
    }

    static bool HasMajorityOnBit(int a, int b, int c, int bitPosition)
    {
        int bitCount = GetBit(a, bitPosition)
                     + GetBit(b, bitPosition)
                     + GetBit(c, bitPosition);

        return bitCount >= 2;
    }

    static int GetBit(int number, int bitPosition)
    {
        return (number >> bitPosition) & 1;
    }

    static int SetBit(int number, int bitPosition)
    {
        return number | (1 << bitPosition);
    }
}