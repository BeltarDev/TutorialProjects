// See https://aka.ms/new-console-template for more information

using Humanizer;

Console.WriteLine("Quantities:");
HumanizeQuantities();

Console.WriteLine("\nDate/Time Manipulation:");
HumanizeDates();

static void HumanizeQuantities()
{
    Console.WriteLine("case".ToQuantity(0));
    Console.WriteLine("case".ToQuantity(1));
    Console.WriteLine("case".ToQuantity(12));
}

static void HumanizeDates()
{
    Console.WriteLine(DateTime.UtcNow.AddHours(-48).Humanize());
    Console.WriteLine(DateTime.UtcNow.AddHours(+5).Humanize());
    Console.WriteLine(TimeSpan.FromDays(3).Humanize());
    Console.WriteLine(TimeSpan.FromDays(22).Humanize());
}