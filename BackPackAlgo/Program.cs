using System;
using System.Collections.Generic;
using System.Linq;

class Sand
{
    public int Number { get; set; }  // Номер піску
    public double Weight { get; set; } // Вага піску
    public double Value { get; set; }  // Ціна піску
    public bool CanTakeFractionally { get; set; } // Можливість брати частинами

    public double ValuePerWeight => Value / Weight; // Ціна за одиницю ваги

    public Sand(int number, double weight, double value, bool canTakeFractionally)
    {
        Number = number;
        Weight = weight;
        Value = value;
        CanTakeFractionally = canTakeFractionally;
    }
}

class Program
{
    static void Main()
    {
        Random random = new Random();
        int k = random.Next(5, 15); // Кількість предметів (випадкове число від 5 до 15)
        double W = random.Next(10, 50); // Максимальна вага, яку може взяти грабіжник

        List<Sand> sands = new List<Sand>();

        // Генерація випадкових пісків
        for (int i = 0; i < k; i++)
        {
            double weight = random.Next(1, 20); // Випадкова вага піску
            double value = random.Next(1, 100); // Випадкова ціна піску
            bool canTakeFractionally = random.Next(0, 2) == 1; // Випадкове значення для можливості брати частинами
            sands.Add(new Sand(i + 1, weight, value, canTakeFractionally));
        }

        // Виведення інформації про пісок
        Console.WriteLine("Gold Sands:");
        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine("Number | Weight | Value | Value/Weight | Can Take Fractionally");
        Console.WriteLine("----------------------------------------------------------");
        foreach (var sand in sands)
        {
            Console.WriteLine($"{sand.Number,6} | {sand.Weight,6} | {sand.Value,5} | {sand.ValuePerWeight:F2} | {sand.CanTakeFractionally}");
        }
        Console.WriteLine("----------------------------------------------------------");

        Console.WriteLine($"\nMaximum weight the robber can take: {W}");

        // Знаходження оптимального набору пісків
        var result = FractionalKnapsack(sands, W, out var selectedSands);
        
        // Вивід результатів
        Console.WriteLine($"\nMaximum value the robber can take: {result:F2}");

        // Вивід вибраних пісків
        Console.WriteLine("\nGold Sands taken:");
        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine("Number | Weight Taken | Fraction of Sand Taken");
        Console.WriteLine("----------------------------------------------------------");
        foreach (var sand in selectedSands)
        {
            Console.WriteLine($"{sand.Number,6} | {sand.Weight,12} | {sand.CanTakeFractionally}");
        }
        Console.WriteLine("----------------------------------------------------------");
    }

    static double FractionalKnapsack(List<Sand> sands, double W, out List<Sand> selectedSands)
    {
        // Сортуємо піски за ціною за одиницю ваги у спадному порядку
        var sortedSands = sands.OrderByDescending(s => s.ValuePerWeight).ToList();
        selectedSands = new List<Sand>();
        double totalValue = 0.0;

        foreach (var sand in sortedSands)
        {
            if (W == 0) break; // Якщо вага рюкзака досягнута, виходимо з циклу

            if (sand.CanTakeFractionally) // Якщо можна брати частинами
            {
                // Якщо поточний предмет повністю вміщується в рюкзак
                if (sand.Weight <= W)
                {
                    W -= sand.Weight;
                    totalValue += sand.Value;
                    selectedSands.Add(new Sand(sand.Number, sand.Weight, sand.Value, true));
                }
                else // В іншому випадку беремо частину предмета
                {
                    totalValue += sand.ValuePerWeight * W; // Додаємо цінність частини
                    // Додаємо частину до вибраних
                    selectedSands.Add(new Sand(sand.Number, W, sand.ValuePerWeight * W, true)); 
                    W = 0; // Рюкзак заповнений
                }
            }
            else // Якщо не можна брати частинами
            {
                // Якщо поточний предмет повністю вміщується в рюкзак
                if (sand.Weight <= W)
                {
                    W -= sand.Weight;
                    totalValue += sand.Value;
                    selectedSands.Add(new Sand(sand.Number, sand.Weight, sand.Value, false));
                }
            }
        }

        return totalValue;
    }
}
