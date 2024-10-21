/*
using System;
using System.Collections.Generic;
using System.Linq;

class GoldBar
{
    public int Number { get; set; }  // Номер злитка
    public int Weight { get; set; }   // Вага злитка
    public int Value { get; set; }    // Ціна злитка (можна задати, наприклад, вагу як ціну)

    public GoldBar(int number, int weight, int value)
    {
        Number = number;
        Weight = weight;
        Value = value;
    }
}

class Program
{
    static void Main()
    {
        Random random = new Random();
        int k = random.Next(5, 15); // Кількість злитків (випадкове число від 5 до 15)
        int W = random.Next(10, 50); // Максимальна вага, яку може взяти грабіжник

        List<GoldBar> goldBars = new List<GoldBar>();

        // Генерація випадкових злитків
        for (int i = 0; i < k; i++)
        {
            int weight = random.Next(1, 20); // Випадкова вага злитка
            int value = random.Next(1, 100); 
            goldBars.Add(new GoldBar(i + 1, weight, value));
        }

        // Виведення інформації про злитки
        Console.WriteLine("Gold Bars:");
        foreach (var bar in goldBars)
        {
            Console.WriteLine($"Number: {bar.Number}, Weight: {bar.Weight}, Value: {bar.Value}");
        }

        Console.WriteLine($"\nMaximum weight the robber can take: {W}");

        // Знаходження оптимального набору злитків
        var result = Knapsack(goldBars, W);
        
        // Вивід результатів
        Console.WriteLine("\nGold Bars taken:");
        foreach (var number in result)
        {
            Console.WriteLine($"Gold Bar Number: {number}");
        }

        // Сортування злитків по відношенню ваги до ціни
        var sortedGoldBars = goldBars.OrderBy(bar => (double)bar.Weight / bar.Value).ToList();

        // Виведення відсортованих злитків
        Console.WriteLine("\nSorted Gold Bars by Weight to Value Ratio:");
        foreach (var bar in sortedGoldBars)
        {
            Console.WriteLine($"Number: {bar.Number}, Weight: {bar.Weight}, Value: {bar.Value}, Ratio: {(double)bar.Weight / bar.Value:F2}");
        }
    }

    static List<int> Knapsack(List<GoldBar> goldBars, int W)
    {
        int n = goldBars.Count;
        int[,] dp = new int[n + 1, W + 1];

        // Заповнення таблиці динамічного програмування
        for (int i = 1; i <= n; i++)
        {
            for (int w = 1; w <= W; w++)
            {
                if (goldBars[i - 1].Weight <= w)
                {
                    dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - goldBars[i - 1].Weight] + goldBars[i - 1].Value);
                }
                else
                {
                    dp[i, w] = dp[i - 1, w];
                }
            }
        }

        // Відстеження злитків, які були взяті
        List<int> selectedBars = new List<int>();
        int remainingWeight = W;

        for (int i = n; i > 0 && remainingWeight > 0; i--)
        {
            if (dp[i, remainingWeight] != dp[i - 1, remainingWeight])
            {
                selectedBars.Add(goldBars[i - 1].Number);
                remainingWeight -= goldBars[i - 1].Weight;
            }
        }

        selectedBars.Reverse(); // Для зручності виводу
        return selectedBars;
    }
}
*/
