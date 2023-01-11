namespace BankAccountV2.Actions;

internal class SelectBankAccountActionLoop : BankAccountAction
{
    public override string? Name { get; set; } = "select endless";
    public override string? Description { get; set; } = "select endless \t\t~This keyword is used to select an existing bankaccount";

    public SelectBankAccountActionLoop(AppState appState) : base(appState)
    {
    }
    public override bool CanExecute() => AppState.AllBankAccounts.Any();
    public override void Execute()
    {
        while (AppState.SelectedBankAccount == null)
        {
            new SelectBankAccountAction(AppState).Execute();
        }

    }
}
