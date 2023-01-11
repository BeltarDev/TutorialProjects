using Newtonsoft.Json;

namespace BankAccountV2.Classes;

public class Transaction
{
    public decimal Amount { get; }
    public DateTime Date { get; }
    [JsonProperty]
    public string Notes { get; set; }

    //public string Recipient { get; set; }

    public Transaction(decimal amount, DateTime date, string note)
    {
        Amount = amount;
        Date = date;
        Notes = note;
        
    }
}

