using System;

class SubstringCounter
{
    static void Main()
    {
        string mainString = ReadInputString("Введите основную строку S:");
        string substring = ReadInputString("Введите подстроку S1 для поиска:");

        int occurrenceCount = CountSubstringOccurrences(mainString, substring);

        DisplayResult(substring, occurrenceCount);
    }

    static string ReadInputString(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine();
    }

    static int CountSubstringOccurrences(string text, string pattern)
    {
        if (string.IsNullOrEmpty(pattern))
            return 0;

        int count = 0;
        int position = 0;

        while ((position = text.IndexOf(pattern, position, StringComparison.Ordinal)) != -1)
        {
            position += pattern.Length;
            count++;
        }

        return count;
    }

    static void DisplayResult(string substring, int count)
    {
        Console.WriteLine($"Количество вхождений '{substring}' в строку: {count}");
    }
}