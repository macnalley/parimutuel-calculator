// Instantiates a list of bets to hold bets as they are placed
// and a race object to contain informtaion about odds and payouts
List<Bet> betsList = new List<Bet>();
Race race = new Race();

// A loop to run a main menu that can be closed with the escape key.
while (true)
    { RunMainMenu(); }


void RunMainMenu()
{
    Console.WriteLine("Type a number to choose an action or Press \"Escape\" to exit:\n" +
        "1. Place bet.\n" + 
        "2. Display odds.\n" +
        "3. Calculate payouts.\n");
    
    switch (Console.ReadKey(true).Key)
    { 
        case ConsoleKey.D1:
            betsList.Add(BettingMethods.PlaceBet());
            break;
        case ConsoleKey.D2:
            IO.NotImplemented();
            break;
        case ConsoleKey.D3:
            PayoutMenu();
            break;
        case ConsoleKey.Escape:
            Environment.Exit(0);
            break;
        default:
            IO.InvalidInput(); 
            break;      
    }
}

void PayoutMenu()
{
    int winHorse = 0;
    int placeHorse = 0;
    int showHorse = 0;
    string[] args;

    do 
    {
        Console.WriteLine("Enter the numbers of the first, second, and third place horses.");
        string input = Console.ReadLine();
        args = BettingMethods.SplitIntoArgs(input);
    } 
    while (!IsPayoutMenuInputValid());

    race.CalculateWinPayouts(betsList, winHorse);
    race.CalculatePlacePayouts(betsList, winHorse, showHorse);
    race.CalculateShowPayouts(betsList, winHorse, placeHorse, showHorse);

    bool IsPayoutMenuInputValid()
    {
        if (args.Length != 3)
            { return false; }
        if (!int.TryParse(args[0], out winHorse))
            { return false; }
        if (!int.TryParse(args[1], out placeHorse))
            { return false; }
        if (!int.TryParse(args[2], out showHorse))
            { return false; }
        else return true;
    }
}