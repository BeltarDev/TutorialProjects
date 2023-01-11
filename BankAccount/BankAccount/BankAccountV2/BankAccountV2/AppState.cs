using BankAccountV2.Classes;

namespace BankAccountV2;

internal class AppState
{
    public List<BankAccount> AllBankAccounts { get; set; } = new();
    public BankAccount? SelectedBankAccount { get; set; }

    public static AppState LoadOrCreateAppState()
    {
        var bankAccounts = AppStateHelper.ReadState();
        
        if (bankAccounts is not null)
        {
            return new AppState
            {
                AllBankAccounts = bankAccounts
            };
        }

        return new AppState(); 
    }

}

