using Classes;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;

/*
var account = new BankAccount("Jacco", 1000);

Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} at {account.DateCreated}");

account.MakeWithdrawal(500, DateTime.Now, "Rent Payment");
Console.WriteLine($"Current balance is: {account.Balance}");
account.MakeDeposit(100, DateTime.Now, "Friend paid back");
Console.WriteLine($"Current balance is: {account.Balance}");

Console.WriteLine($"Transaction was made at {account.GetTransactionDate2(0)}.");
Console.WriteLine($"Transaction was made at {account.GetTransactionDate2(1)}.");

Console.WriteLine(account.GetAccountHistory());

var giftCard = new GiftCardAccount("gift card", 100, 50);

Console.WriteLine(
    $"Account {giftCard.Number} was created for {giftCard.Owner} with {giftCard.Balance} at {giftCard.DateCreated}");
giftCard.MakeWithdrawal( 20, DateTime.Now, "get expensive coffee");
giftCard.MakeWithdrawal(50, DateTime.Now, "buy groceries");
giftCard.PerformMonthEndTransactions();
// can make additional deposits:
giftCard.MakeDeposit(27.50m, DateTime.Now, "add some additional spending money");
Console.WriteLine(giftCard.GetAccountHistory());

var savings = new InterestEarningAccount("savings account", 10000);
Console.WriteLine(
    $"Account {savings.Number} was created for {savings.Owner} with {savings.Balance} at {savings.DateCreated}");
savings.MakeDeposit(750, DateTime.Now, "save some money");
savings.MakeDeposit(1250, DateTime.Now, "Add more savings");
savings.MakeWithdrawal(250, DateTime.Now, "Needed to pay monthly bills");
savings.PerformMonthEndTransactions();
Console.WriteLine(savings.GetAccountHistory());

var lineOfCredit = new LineOfCreditAccount("line of credit", 0, 2000);
// How much is too much to borrow?
lineOfCredit.MakeWithdrawal(1000m, DateTime.Now, "Take out monthly advance");
lineOfCredit.MakeDeposit(50m, DateTime.Now, "Pay back small amount");
lineOfCredit.MakeWithdrawal(5000m, DateTime.Now, "Emergency funds for repairs");
lineOfCredit.MakeDeposit(150m, DateTime.Now, "Partial restoration on repairs");
lineOfCredit.PerformMonthEndTransactions();
Console.WriteLine(lineOfCredit.GetAccountHistory());
*/

namespace BankAccount;

class Program
{
    public static void Main(string[] args)
    {
        var allowedActions = new List<string>();
        var bankAccounts = new List<Classes.BankAccount>();
        Classes.BankAccount? selectedBankAccount = null;

        // NOTE: REMOVE THIS IN PRODUCTION!!!!!!!
        var testAccount1 = new Classes.BankAccount("Jacco", 12345);
        bankAccounts.Add(testAccount1);
        selectedBankAccount = testAccount1;

        const string create = $"{nameof(create)} \t\t~This keyword is used to create a bankaccount";
        const string select = $"{nameof(select)} \t\t~This keyword is used to select an existing bankaccount";
        const string transact = $"{nameof(transact)}\t~This keyword is used to transact with selected account";
        const string list = $"{nameof(list)}\t\t~This keyword is used to list all transactions from selected account";
        const string quit = $"{nameof(quit)} \t\t~This keyword is used to quit the application";

        while (true)
        {
            // vraag user wat te doen?
            Console.WriteLine("Welcome to GoldmanSucks. Tell us what you would like to do. ");
            Console.WriteLine("The options are:");

            Console.WriteLine(create);
            allowedActions.Add(nameof(create));

            Console.WriteLine(quit);
            allowedActions.Add(nameof(quit));

            // if there are bank accounts created then be able to select it
            if (bankAccounts.Any())
            {
                allowedActions.Add(nameof(select));
                Console.WriteLine(select);
            }

            // if selected account is not null
            // then list other options that are able to execute with selected account
            if (selectedBankAccount is not null)
            {
                allowedActions.Add(nameof(transact));
                allowedActions.Add(nameof(list));
                Console.WriteLine(transact);
                Console.WriteLine(list);
            }

            Console.Write("Select option:");
            var userAction = Console.ReadLine();
            var userActionLower = userAction.ToLower();

            if (!allowedActions.Contains(userActionLower))
            {
                Console.WriteLine("Unauthorized Action, eh uh");
                continue;
            }

            // DONE: validate if given action is allowed CHECK

            // DONE: use nameof keyword for user actions CHECK
            switch (userActionLower)
            {
                case nameof(create):
                    {
                        var newBankAccount = CreateBankAccountForUser();
                        bankAccounts.Add(newBankAccount);
                        break;
                    }
                case nameof(select):
                    {
                        // option to select certain account
                        var newSelectedBankAccount = SelectAccount(bankAccounts);

                        // save state of selected account (store selected account in variable)
                        selectedBankAccount = newSelectedBankAccount;

                        Console.WriteLine("Bank account found!");
                        break;
                    }
                case nameof(transact):
                    {
                        DoWithdrawOrDeposit(selectedBankAccount);

                        break;
                    }
                case nameof(list):

                    {
                        Console.WriteLine($"Number of bank accounts: {bankAccounts.Count}"); //list transactions
                        break;
                    }
                case "q":
                case nameof(quit):
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("That's invalid input, see options.");
                        break;
                    }
            }

            Console.WriteLine();
        }
    }

    private static void DoWithdrawOrDeposit(Classes.BankAccount selectedBankAccount)
    {
        while (true)
        {
            Console.WriteLine($"Current account {selectedBankAccount.Owner} has balance of {selectedBankAccount.Balance}");
            Console.WriteLine("Would you like to do a ~withdraw~ action or ~deposit~?");

            var userName = selectedBankAccount.Owner;
            var withdrawOrDeposit = Console.ReadLine();

            switch (withdrawOrDeposit?.ToLower())
            {
                case "withdraw":
                    {
                        var withdraw = RequestWithdrawal(userName);
                        Console.WriteLine("What is the reason for withdrawal?");
                        Console.Write("Reason:");
                        var reason = Console.ReadLine();

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
                        var deposit = RequestDeposit(userName);
                        Console.WriteLine("What is the reason for deposit?");
                        Console.Write("Reason:");
                        var reason = Console.ReadLine();

                        try
                        {
                            selectedBankAccount.MakeDeposit(deposit, DateTime.Now, reason);
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
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

    private static Classes.BankAccount CreateBankAccountForUser()
    {
        Console.WriteLine("Hello Ser, so you would like to create a BankAccount at GoldmanSucks?");

        var name = AskNameFromUser();
        Console.WriteLine($"Welcome {name} please proceed.");

        var deposit = RequestDeposit(name);

        var bankAccount = new Classes.BankAccount(name, deposit);

        Console.WriteLine($"Account {bankAccount.Number} was created for {bankAccount.Owner} with {bankAccount.Balance} at {bankAccount.DateCreated}");

        return bankAccount;
    }

    private static string AskNameFromUser()
    {
        var firstTry = true;


        while (true)
        {
            if (firstTry)
            {
                Console.Write("What is your Name?: ");
                firstTry = false;
            }
            else
            {
                Console.Write("Try again:");
            }

            var name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name) && name.Length > 3 && name.All(char.IsLetterOrDigit) &&
                name.Take(5).All(x => !char.IsDigit(x)) && char.IsUpper(name[0]))
            {
                return name;
            }
            else
            {
                Console.WriteLine("Sorry, invalid input. Name should consist out of a Letters + numbers, not just empty characters or numbers and with a minimum length of 4 and first letter should be uppercase");
            }
        }
    }

    private static decimal RequestDeposit(string name)
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

            if (!string.IsNullOrWhiteSpace(readLine) && decimal.TryParse(readLine, out var requestDeposit))
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
    private static string AccountOverview(IReadOnlyCollection<Classes.BankAccount> givenAccount)
    {
        var report = new System.Text.StringBuilder();

        report.AppendLine("Name\tDate of Creation\tBalance");

        foreach (var item in givenAccount)
        {
            report.AppendLine($"{item.Owner}\t{item.DateCreated}\t{item.Balance}");
        }

        return report.ToString();
    }
    //todo: Alter method to handle wrong inputs on select. CHECK

    private static Classes.BankAccount SelectAccount(IReadOnlyCollection<Classes.BankAccount> allBankAccounts)
    {
        Console.WriteLine("These are your current accounts:");

        var currentAccountOverview = AccountOverview(allBankAccounts);
        Console.WriteLine(currentAccountOverview);

        var firstTry = true;

        while (true)
        {
            if (firstTry)
            {
                Console.Write("Select an account by name:");
                firstTry = false;
            }
            else
            {
                Console.Write("Account not found, please provide correct name:");
            }

            var nameByUserInput = Console.ReadLine();
            // todo: validate user input (or use AskNameFromUser?) 

            var accountFoundByName = allBankAccounts.FirstOrDefault(b => b.Owner == nameByUserInput);

            if (accountFoundByName is not null)
            {
                return accountFoundByName;
            }
        }
    }
}