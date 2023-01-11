using System;

namespace Classes;

public class LineOfCreditAccount : BankAccount
{
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit = 0) : base(name, initialBalance, -creditLimit)
    {

    }

    public override void PerformMonthEndTransactions()
    {
        if (Balance < 0)
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.Now, "charge monthly interest");
        }
    }
    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
            return new Transaction(-20, DateTime.Now, "Apply overdraft fee");
        else
            return default;
    }
}