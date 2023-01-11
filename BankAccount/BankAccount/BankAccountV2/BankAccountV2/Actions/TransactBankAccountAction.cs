using System.Reflection.Metadata.Ecma335;

namespace BankAccountV2.Actions;

internal class TransactBankAccountAction : BankAccountAction
{
    public override string? Name { get; set; } = "transact";
    public override string? Description { get; set; } = "transact \t\t~This keyword is used to transact from selected bankaccount";

    public TransactBankAccountAction(AppState appState) : base(appState)
    {
    }

    public override bool CanExecute() => AppState.SelectedBankAccount != null;

    public override void Execute()
    {
        while (true)
        {
            var selectedBankAccount = AppState.SelectedBankAccount;
            if (selectedBankAccount is null)
            {
                throw new ArgumentNullException("");
            }

            Console.WriteLine(
                $"Current account {selectedBankAccount.Owner} has balance of {selectedBankAccount.Balance}");
            Console.WriteLine("Would you like to do a ~withdraw~ action or ~deposit~? or ~quit~");


            var withdrawOrDeposit = Console.ReadLine();
            switch (withdrawOrDeposit?.ToLower())
            {
                case "withdraw":
                    {
                        var withdraw = RequestWithdrawal(selectedBankAccount.Owner);
                        Console.WriteLine("What is the reason for withdrawal?");
                        Console.Write("Reason:");
                        var reason = Console.ReadLine() ?? throw new ArgumentNullException("");

                        try
                        {
                            selectedBankAccount.MakeWithdrawal(withdraw, DateTime.Now, reason);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        continue;
                    }
                case "deposit":
                    {
                        var deposit = RequestDeposit(selectedBankAccount.Owner);
                        Console.WriteLine("What is the reason for deposit?");
                        Console.Write("Reason: ");
                        var reason = Console.ReadLine();
                        if (reason is not null )
                        {
                            try
                            {
                                selectedBankAccount.MakeDeposit(deposit, DateTime.Now, reason);
                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        else
                        {
                            Console.Write("invalid reason");
                            return;
                        }
                        

                        continue;
                    }
                case "q":
                case "quit":
                    {
                        return;
                    }
                default:
                    Console.WriteLine("Invalid input. See options.");
                    continue;
            }
        }
    }


    public static decimal RequestDeposit(string name)
    {
        Console.WriteLine($"{name}, deposit funds it is.");

        var firstTry = true;

        while (true)
        {
            if (firstTry)
            {
                Console.Write("Deposit Capital: ");
                firstTry = false;
            }
            else
            {
                Console.Write("Try again:");
            }

            var readLine = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(readLine) && decimal.TryParse(readLine, out var requestDeposit) && requestDeposit > 0)
            {
                // good value
                Console.WriteLine($"{name}, you will deposit {requestDeposit}");
                return requestDeposit;
            }
            else
            {
                // bad value
                Console.WriteLine($"Sorry, invalid input. Value should be a valid number and greater than 0");
            }
        }
    }

    private static decimal RequestWithdrawal(string name)
    {
        Console.WriteLine($"So {name} you will perform a withdrawal.");

        var firstTry = true;

        while (true)
        {
            if (firstTry)
            {
                Console.Write("Money to withdraw: ");
                firstTry = false;
            }
            else
            {
                Console.Write("Try again:");
            }

            var readLine = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(readLine) && decimal.TryParse(readLine, out var requestWithdrawal) && requestWithdrawal > 0)
            {
                // good value
                Console.WriteLine($"{name}, you will withdraw {requestWithdrawal}");
                return requestWithdrawal;
            }
            else
            {
                // bad value
                Console.WriteLine($"Sorry, invalid input. Value should be a valid number and greater than 0");
            }
        }
    }
}
