using System.Globalization;
using System.Xml;
using BankAccountV2.Classes;

namespace BankAccountV2.Actions;

class ListBankAccountAction : BankAccountAction
{
    public override string? Name { get; set; } = "list";

    public override string? Description { get; set; } =
        "'list' \t\t\t~This keyword is used to list transactions from selected bankaccount";

    public ListBankAccountAction(AppState appState) : base(appState)
    {
    }
    public override bool CanExecute() => AppState.SelectedBankAccount != null;
    public override void Execute()
    {
        var account = AppState.SelectedBankAccount;
        Console.WriteLine($"List all TX's for current bankaccount: {account.Owner} with balance {account.Balance}");

        var report = new System.Text.StringBuilder();
        report.AppendLine("Date\t\tAmount\t\tNote");
        foreach (var item in account.AllTransactions)
        {
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t\t{item.Notes}");
        }

        var fullReport = report.ToString();
        Console.WriteLine(fullReport);
        Console.WriteLine($"{DateTime.Now}\t{account.Balance:C}\tCurrent Balance");
        //var output = String.Format(new NumberFormatInfo(),account.Balance);
    }
}
