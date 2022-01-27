List<Bet> betsList = new List<Bet>();

string userInput;

do
{
    Console.WriteLine("Type a number and hit \"Enter\" to choose an action or Press \"Escape\" to exit:\n" +
        "1. Place bet.\n" + 
        "2. Display odds.\n" +
        "3. Calculate payouts.\n");

    userInput = Console.ReadLine();

    switch (userInput)
    { 
        case "1":
            PlaceBet();
            break;
        case "2":
            NotImplemented();
            break;
        case "3":
            NotImplemented();
            break;
        default:
            InvalidInput(); 
            break;      
    }

} while (Console.ReadKey().Key != ConsoleKey.Escape);

void PlaceBet()
{
    Console.WriteLine("Enter bettor name, horse number, bet type, and amount in that order.");
    userInput = Console.ReadLine();
    if (userInput != null)
    {
        string[] args = userInput.Split(' ');
        if (args.Length == 4)
        {
            string name = args[0];
            
            int horse;
            if (!int.TryParse(args[1], out horse))
                {
                    InvalidInput();
                    PlaceBet();
                    return;
                }
            
            BetType betType = BetType.place;
            switch (args[2].ToLower())
            {
                case "win":
                    betType = BetType.win;
                    break;
                case "place":
                    betType = BetType.place;
                    break;
                case "show":
                    betType = BetType.show;
                    break;
                default:
                    InvalidInput();
                    PlaceBet();
                    break;
            }
            
            double amount;
            if (!double.TryParse(args[3], out amount))
                {
                    InvalidInput();
                    PlaceBet();
                    return;
                }
            
            betsList.Add(new Bet(name, horse, amount, betType));       
        }
        else
        {
            InvalidInput();
            PlaceBet();
        }
    }
    else 
    {
        InvalidInput();
        PlaceBet();
        return;
    }
}

void NotImplemented()
{
    Console.WriteLine("Functionality not implemented.");
}

void InvalidInput()
{
    Console.WriteLine("Invalid input.");
}
