using System;
using System.IO;

class Program
{
    const string FileName = "expenses.txt";

    static void Main()
    {
        int choice;
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nЧто ты хочешь сделать сейчас?");
            Console.WriteLine("1. Добавить новую трату");
            Console.WriteLine("2. Посмотреть историю");
            Console.WriteLine("3. Узнать общую сумму");
            Console.WriteLine("0. Выход");
            Console.ResetColor();

            while (!int.TryParse(Console.ReadLine(), out choice))
                Console.WriteLine("Введи число");

            switch (choice)
            {
                case 1: AddExpense(); break;
                case 2: ShowHistory(); break;
                case 3: ShowTotal(); break;
                case 0: return;
                default: Console.WriteLine("Такого выбора нет"); break;
            }
        }
    }

    static void AddExpense()
    {
        Console.WriteLine("Сколько потратил сегодня?");
        if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
        {
            File.AppendAllText(FileName, $"{DateTime.Now:dd.MM.yyyy} - {amount}{Environment.NewLine}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Сегодняшние траты: {amount}₽");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("Некорректная сумма");
        }
    }

    static void ShowHistory()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("История трат:");
        Console.ResetColor();

        if (!File.Exists(FileName))
        {
            Console.WriteLine("Записей пока нет");
            return;
        }

        var lines = File.ReadAllLines(FileName);
        for (int i = 0; i < lines.Length; i++)
            Console.WriteLine($"{i + 1}. {lines[i]}");
    }

    static void ShowTotal()
    {
        if (!File.Exists(FileName))
        {
            Console.WriteLine("Записей пока нет");
            return;
        }

        int total = 0;
        foreach (var line in File.ReadAllLines(FileName))
        {
            var parts = line.Split('-');
            if (parts.Length >= 2 && int.TryParse(parts[1].Trim().Replace("₽", ""), out int amount))
                total += amount;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Общая сумма трат: {total:N0}₽");
        Console.ResetColor();
    }
}
