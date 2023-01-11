using BankAccountV2.Classes;

namespace BankAccountV2.Actions;

internal class PerformMonthEndTransactionBankAccountAction : BankAccountAction
{
    public override string? Name { get; set; } = "met";
    public override string? Description { get; set; } = $"'met' \t\t\t~This keyword is used to perform month end transactions.";

    public PerformMonthEndTransactionBankAccountAction(AppState appState) : base(appState)
    {
    }

    public override bool CanExecute() => AppState.SelectedBankAccount is LineOfCreditAccount or InterestEarningAccount or GiftCardAccount;

    public override void Execute()
    {
        AppState.SelectedBankAccount!.PerformMonthEndTransactions();
    }
}
