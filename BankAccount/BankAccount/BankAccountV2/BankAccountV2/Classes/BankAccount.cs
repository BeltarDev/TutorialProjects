using Newtonsoft.Json;

namespace BankAccountV2.Classes;

public class BankAccount
{
    public static int AccountNumberSeed = 1234657890;

    [JsonProperty] public string Number { get; private set; } = null!;
    [JsonProperty] public string Owner { get; private set; } = null!;
    [JsonProperty] public decimal Balance { get; private set; } = 0;


        [JsonProperty]
    public List<Transaction> AllTransactions { get; private set; } = new();

    [JsonIgnore]
    public decimal BalanceOld
    {
        get
        {
            decimal balance = 0;
            foreach (var item in AllTransactions)
            {
                balance += item.Amount;
            }
            return balance;
        }
    }

    public DateTime DateCreated { get; set; }
    
    private readonly decimal _minimumBalance;

    [JsonConstructor]
    protected BankAccount()
    {

    }

    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0){}
    
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        DateCreated = DateTime.Now;
        Number = AccountNumberSeed.ToString();
        AccountNumberSeed++;

        Owner = name;
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
    }
    
    public virtual void PerformMonthEndTransactions()
    {

    }

    public DateTime GetTransactionDate(int n)
    {
        if (n > (AllTransactions.Count-1) )
        {
            throw new ArgumentOutOfRangeException(nameof(n), "Transaction number does not exist.");
        }
        else if (n < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "Provide valid index");

        }

        return AllTransactions[n].Date;
    }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }

        var deposit = new Transaction(amount, date, note);
        AllTransactions.Add(deposit);

        Balance += amount;


        // balance has changed
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
        }
        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction withdrawal = new(-amount, date, note);
        AllTransactions.Add(withdrawal);
        if (overdraftTransaction != null)
            AllTransactions.Add(overdraftTransaction);

        Balance -= amount;
        // balance has changed
    }

    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("Insufficient funds for this withdrawal");
        }
        else
        {
            return default;
        }
    }

    public string GetAccountHistory()
    {
        var report = new System.Text.StringBuilder();
        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in AllTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }
        return report.ToString();
    }
}
