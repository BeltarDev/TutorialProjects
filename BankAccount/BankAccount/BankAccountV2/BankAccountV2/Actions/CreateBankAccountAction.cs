using System.Globalization;
using BankAccountV2.Classes;

namespace BankAccountV2.Actions;

internal class CreateBankAccountAction : BankAccountAction
{
    public override string? Name { get; set; } = "create";
    public override string? Description { get; set; } = "create \t\t\t~This keyword is used to create a bankaccount";

    public CreateBankAccountAction(AppState appState) : base(appState)
    {
    }

    public override void Execute()
    {
        var name = AskTextFromUser("Please insert name:", 4);

        Console.Write("Please insert balance:");
        var balanceText = Console.ReadLine();
        decimal.TryParse(balanceText, NumberStyles.Any, CultureInfo.InvariantCulture, out var balance);
        if (balance < 0)
        {
            Console.WriteLine("Negative Value, retry");
        }
        else
        {
            Console.WriteLine("Select type of Account, options are:");
            Console.WriteLine("'BankAccount' or 'BA' for regular account ");
            Console.WriteLine("'GiftCardAccount' or 'GCA' or 'gift' for Gift Card account");
            Console.WriteLine("'InterestEarningAccount' or 'IEA' or 'savings' for a savings account");
            Console.WriteLine("'LineOfCreditAccount' or 'LOCA' or 'credit' for a Line of Credit account");

            while (true)
            {
                Console.Write("Select option:");
                var typeAccount = Console.ReadLine();

                switch (typeAccount.ToLowerInvariant())
                {
                    case "ba":
                    case "bankaccount":
                        {
                            var newBankAccount = new BankAccount(name, balance);
                            Console.WriteLine($"New bank account created (V2) for '{name}' with balance '{balance}'");
                            AppState.AllBankAccounts.Add(newBankAccount);
                            return;
                        }
                    case "gift":
                    case "gca":
                    case "giftcardaccount":
                        {
                            var newGCAccount = new GiftCardAccount(name, balance);
                            Console.WriteLine(
                                $"New Gift Card Account created (V2) for '{name}' with balance '{balance}'");
                            AppState.AllBankAccounts.Add(newGCAccount);
                            return;
                        }
                    case "savings":
                    case "iea":
                    case "interestearningaccount":
                        {
                            var newIEAccount = new InterestEarningAccount(name, balance);
                            Console.WriteLine(
                                $"New Interest Earning account created (V2) for '{name}' with balance '{balance}'");
                            AppState.AllBankAccounts.Add(newIEAccount);
                            return;
                        }
                    case "credit":
                    case "loca":
                    case "lineOfcreditaccount":
                        {
                            var newLoCAccount = new LineOfCreditAccount(name, balance);
                            Console.WriteLine(
                                $"New Line of Credit Account created (V2) for '{name}' with balance '{balance}'");
                            AppState.AllBankAccounts.Add(newLoCAccount);
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input, try again:");
                            continue;
                        }
                }
            }
        }
    }
}
