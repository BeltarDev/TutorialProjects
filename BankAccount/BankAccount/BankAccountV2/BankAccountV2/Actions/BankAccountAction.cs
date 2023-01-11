namespace BankAccountV2.Actions;

internal class BankAccountAction
{
    protected readonly AppState AppState;

    public virtual string? Name { get; set; }
    public virtual string? Description { get; set; }

    public BankAccountAction(AppState appState)
    {
        AppState = appState;
    }

    public virtual bool CanExecute()
    {
        return true;
    }

    public virtual void Execute()
    {
        throw new NotImplementedException();
    }

    public string AskTextFromUser(string question, int minLength)
    {
        var firstTry = true;

        while (true)
        {
            if (firstTry)
            {
                Console.Write(question);
                firstTry = false;
            }
            else
            {
                Console.Write("Invalid input, try again:");
            }
            var name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                continue;
            }

            if (name.Length < minLength)
            {
                Console.Write($"Min length is {minLength}");
            }

            return name;
        }
    }

    public string AskTextFromUser(string question)
    {
        return AskTextFromUser(question, 0);
    }

    public string AskTextFromUser()
    {
        return AskTextFromUser("Please insert value:", 0);
    }
}
