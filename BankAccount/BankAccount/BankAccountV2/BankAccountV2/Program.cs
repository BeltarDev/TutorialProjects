using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Net.NetworkInformation;
using BankAccountV2;
using BankAccountV2.Actions;
using BankAccountV2.Classes;
using Newtonsoft.Json;
using System.Reflection;

namespace BankAccountV2;

internal class Program
{
    private static void Main()
    {
        var appState = AppState.LoadOrCreateAppState();
        CultureInfo.CurrentCulture = new CultureInfo("nl-NL");

        Console.WriteLine("CurrentCulture is {0}.", new decimal(123).ToString(""));
        Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.Name);
        Console.WriteLine("CurrentCultureSymbol is {0}.", CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol);
        Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalDigits);
        Console.WriteLine("CurrentCulture is {0}.", CultureInfo.CurrentCulture.NumberFormat.IsReadOnly);

        RegionInfo[] regions = { new RegionInfo("NL"), new RegionInfo("nl-NL"),new RegionInfo("nl-BE") };
        PropertyInfo[] props = typeof(RegionInfo).GetProperties(BindingFlags.Instance | BindingFlags.Public);

        Console.WriteLine("{0,-30}{1,18}{2,18}{3,18}\n",
            "RegionInfo Property", "EU", "nl-NL", "nl-EU");
        foreach (var prop in props)
        {
            Console.Write("{0,-30}", prop.Name);
            foreach (var region in regions)
                Console.Write("{0,18}", prop.GetValue(region, null));

            Console.WriteLine();
        }




        var bankAccountActionTypes = typeof(BankAccountAction).Assembly
            .GetTypes()
            .Where(x => typeof(BankAccountAction).IsAssignableFrom(x) && x != typeof(BankAccountAction))
            .ToArray();

        while (true)
        {
            Console.WriteLine("Welcome to GoldmanStacks. Tell us what you would like to do. ");
            Console.WriteLine("The options are:");

            var allowedActions = new List<BankAccountAction>();

            foreach (var bankAccountActionType in bankAccountActionTypes)
            {
                var instance = (BankAccountAction)Activator.CreateInstance(bankAccountActionType, appState)!;

                if (!instance.CanExecute())
                {
                    continue;
                }

                allowedActions.Add(instance);
                Console.WriteLine(instance.Description);
            }

            Console.Write("Select option:");
            var userAction = Console.ReadLine();

            // stop app in case of empty input or q or quit
            if (string.IsNullOrWhiteSpace(userAction) ||
                userAction.Equals("q", StringComparison.OrdinalIgnoreCase) ||
                userAction.Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                AppStateHelper.SaveState(appState.AllBankAccounts);
                Environment.Exit(0);
                return;
            }

            var selectedBankAccountAction =
                allowedActions.FirstOrDefault(x => x.Name.Equals(userAction, StringComparison.OrdinalIgnoreCase));

            if (selectedBankAccountAction is null)
            {
                Console.WriteLine("Unauthorized Action, eh uh");
                continue;
            }

            selectedBankAccountAction.Execute();
        }
    }
}
