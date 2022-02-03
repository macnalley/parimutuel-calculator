List<Bet> betsList = new List<Bet>();

while (true)
    RunMainMenu();

void PlaceBet()
{
    Console.WriteLine("Enter bettor name, horse number, bet type, and amount in that order.");
    string userInput = Console.ReadLine();
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

void RunMainMenu()
{
    Console.WriteLine("Type a number to choose an action or Press \"Escape\" to exit:\n" +
        "1. Place bet.\n" + 
        "2. Display odds.\n" +
        "3. Calculate payouts.\n");
    
    switch (Console.ReadKey(true).Key)
    { 
        case ConsoleKey.D1:
            PlaceBet();
            break;
        case ConsoleKey.D2:
            NotImplemented();
            break;
        case ConsoleKey.D3:
            NotImplemented();
            break;
        case ConsoleKey.Escape:
            Environment.Exit(0);
            break;
        default:
            InvalidInput(); 
            break;      
    }
}