using System;
using System.Collections.Generic;

namespace Classes;

public class BankAccount
{
    public string Number { get; }
    public string Owner { get; set; }
    public decimal Balance { get; private set; } = 0;

    public decimal BalanceOld
    {
        get
        {
            decimal balance = 0;
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
            }
            return balance;
        }
    }

    public DateTime DateCreated { get; }
    private static int accountNumberSeed = 1234657890; 
    
    private readonly decimal _minimumBalance;

    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0){}
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        DateCreated = DateTime.Now;
        Number = accountNumberSeed.ToString();
        accountNumberSeed++;

        Owner = name;
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
    }

    private List<Transaction> allTransactions = new List<Transaction>();

    public virtual void PerformMonthEndTransactions()
    {

    }

    public DateTime GetTransactionDate(int n)
    {
        if (n > (allTransactions.Count-1) )
        {
            throw new ArgumentOutOfRangeException(nameof(n), "Transaction number does not exist.");
        }
        else if (n < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "Provide valid index");

        }

        return allTransactions[n].Date;
    }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }

        var deposit = new Transaction(amount, date, note);
        allTransactions.Add(deposit);

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
        allTransactions.Add(withdrawal);
        if (overdraftTransaction != null)
            allTransactions.Add(overdraftTransaction);

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
        foreach (var item in allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }
        return report.ToString();
    }

}