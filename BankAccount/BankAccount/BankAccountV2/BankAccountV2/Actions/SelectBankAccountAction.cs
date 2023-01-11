using BankAccountV2.Classes;

namespace BankAccountV2.Actions;

internal class SelectBankAccountAction : BankAccountAction
{
    public override string? Name { get; set; } = "select";

    public override string? Description { get; set; } = "select \t\t\t~This keyword is used to select an existing bankaccount";

    public SelectBankAccountAction(AppState appState) : base(appState)
    {
    }

    public override bool CanExecute() => AppState.AllBankAccounts.Any();

    public override void Execute()
    {
        var listAccounts = AccountOverview(AppState.AllBankAccounts);
        Console.WriteLine(listAccounts);
        var name = AskTextFromUser("Select an account by name:", 4);
        var accountFoundByName = AppState.AllBankAccounts.FirstOrDefault(b => b.Owner == name);

        if (accountFoundByName is not null)
        {
            AppState.SelectedBankAccount = accountFoundByName;
        }
    }

    public static string AccountOverview(List<BankAccount> givenAccount)
    {
        var report = new System.Text.StringBuilder();

        report.AppendLine("Name\tDate of Creation\tType Account\tBalance");

        foreach (var item in givenAccount)
        {
            report.AppendLine($"{item.Owner}\t{item.DateCreated}\t{item.Balance:C}\t\t{item.GetType().Name}");
        }


        return report.ToString();
    }
}
