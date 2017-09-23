using System;
using System.Collections.Generic;
using System.Linq;
 
public class Program
{
    static void Main(string[] args)
    {
//        var data = new int[] {1, 2, 4, 5, 17, 18};
//        var data = new int[] {3, 4, 5, 6, 7, 17, 18};
        var data = new int[] {2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14, 15, 16, 17, 19, 20, 21, 24};
        FoodService.GetBestSubscriptionPlan(data);
    }
}
 
public class FoodService
{
    // For demonstration, lets define some symbols to represent the meal subscriptions:
    // x means day with subscription
    // o means day without subscription
 
    // For example, for subscription days 1, 2, 4, 5, 17, 18, the string representation would be
    // Days 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18
    //       x  x  o  x  x  o  o  o  o  o  o  o  o  o  o  o  x  x
 
    // Now we will discuss the basic strategy to find the best solution
    // Possible services:
    // 1 day subscription   -  $6.00 (package 1)
    // 5 days subscription  - $27.99 (package 2)
    // 20 days subscription - $99.99 (package 3)
 
    // Here we will apply greedy algorithm from the cheapest package to single meal package.
    // But here we have one issue, for the below cases (20 days):
 
    // Days 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20
    //       x  x  x  x  x  x  x  x  x  o  x  x  x  x  x  x  x  x  x  o (Greedy result: $103.98)
    //       x  x  x  x  x  x  x  x  o  x  x  x  x  x  o  x  x  x  x  x (Greedy result: $101.97)
 
    // Above examples are case where if we subscribe 20 days meal ($99.99) but is cheaper then the result came out with greedy algorithm. 
    // This is a better strategy that rather order more meals than needed but with lower cost.
 
    // To handle this special scenarios, the algorithm should be (the word 'assign' means scan & replace x with o):
    // 1. Convert the inputs to string representation with symbol x & o.
    // 2. Try to assign consecutive 20-days with x with package 3. 
    // 3. Try to assign consecutive 20-days with x or o with package 3.
    // 4. Try to assign consecutive 5-days with x with package 2.
    // 5. Assign remaining days with x with package 1.
 
    private static string _dailyMeals;
 
    public static void GetBestSubscriptionPlan(int[] inputs)
    {
        // 1. Convert the inputs to string representation with symbol x & o.
 
        var max = inputs.Max();
        var days = new bool[max];
 
        foreach (var i in inputs)
            days[i - 1] = true;
 
        _dailyMeals = string.Join("", days.Select(x => x ? 'x' : 'o'));
 
        // 2. Try to assign consecutive 20-days with x with package 3.
 
        var mealSubscription = new MealSubscription();
 
        while (true)
        {
            var index = _dailyMeals.IndexOf(new string('x', 20), StringComparison.Ordinal);
            if (index == -1) break;
 
            mealSubscription.Price += 99.99;
            mealSubscription.Package20Days.Add($"{index+1}-{index+20}");
            UpdateDailyMeals(index, 20);
        }
 
        // 3. Try to assign consecutive 20-days with x or o with package 3.
 
        for (var i = 0; i < _dailyMeals.Length; i++)
        {
            var start = i;
            var end = start + 19;
            if (end >= _dailyMeals.Length) break;
 
            if (_dailyMeals[start] == 'x' && _dailyMeals[end] == 'x')
            {
                var subDailyMeals = _dailyMeals.Substring(start, 20);
 
                if (GetBestPriceWithPackage1And2(subDailyMeals) > 99.9)
                {
                    mealSubscription.Price += 99.99;
                    mealSubscription.Package20Days.Add($"{start+1}-{end+1}");
                    UpdateDailyMeals(start, 20);
                }
            }
        }
 
        // 4. Try to assign consecutive 5-days with x with package 2.
 
        while (true)
        {
            var index = _dailyMeals.IndexOf("xxxxx", StringComparison.Ordinal);
            if (index == -1) break;
 
            mealSubscription.Price += 27.99;
            mealSubscription.Package5Days.Add($"{index+1}-{index+5}");
            UpdateDailyMeals(index, 5);
        }
 
        // 5. Assign remaining days with x with package 1.
 
        for (var i = 0; i < _dailyMeals.Length; i++)
        {
            if (_dailyMeals[i] != 'x') continue;
 
            mealSubscription.Price += 6;
            mealSubscription.Package1Day.Add($"{i+1}");
            UpdateDailyMeals(i, 1);
        }
 
 
        // 6. Print result
 
        Console.WriteLine($"Days: {string.Join(",", inputs)}");
        Console.WriteLine($"Price: ${mealSubscription.Price: #.##}");
        Console.WriteLine($"1 Day Subscription   : {string.Join(",", mealSubscription.Package1Day)}");
        Console.WriteLine($"5 Days Subscription  : {string.Join(",", mealSubscription.Package5Days)}");
        Console.WriteLine($"20 Days Subscription : {string.Join(",", mealSubscription.Package20Days)}");
    }
 
    private static double GetBestPriceWithPackage1And2(string meals)
    {
        double price = 0;
 
        while (true)
        {
            var index = meals.IndexOf("xxxxx", StringComparison.Ordinal);
            if (index == -1) break;
 
            price += 27.99;
            meals = meals.Substring(0, index) + 
                    new string('o', 5) + 
                    meals.Substring(index + 5);
        }
 
        price += 6 * meals.Count(x => x == 'x');
        return price;
    }
 
    private static void UpdateDailyMeals(int startIndex, int length)
    {
        _dailyMeals = _dailyMeals.Substring(0, startIndex) + 
                     new string('o', length) + 
                     _dailyMeals.Substring(startIndex + length);
    }
 
    private class MealSubscription
    {
        public List<string> Package1Day { get; set; } = new List<string>();
        public List<string> Package5Days { get; set; } = new List<string>();
        public List<string> Package20Days { get; set; } = new List<string>();
        public double Price { get; set; } = 0;
    }
    
}