Console.Clear();
Console.WriteLine(@"/---------------------------------------------------\");
Console.WriteLine(@"|   _          _     _        ______      _     _   |");
Console.WriteLine(@"|  | |        | |   ( )       | ___ \    | |   | |  |");
Console.WriteLine(@"|  | |     ___| |_  |/ ___    | |_/ / ___| |_  | |  |");
Console.WriteLine(@"|  | |    / _ \ __|   / __|   | ___ \/ _ \ __| | |  |");
Console.WriteLine(@"|  | |___|  __/ |_    \__ \   | |_/ /  __/ |_  |_|  |");
Console.WriteLine(@"|  \_____/\___|\__|   |___/   \____/ \___|\__| (_)  |");
Console.WriteLine(@"|                                                   |");
Console.WriteLine(@"\---------------------------------------------------/");

Console.ReadLine();
Console.Clear();

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
            Console.Clear();
            Console.WriteLine("Bet added.\n");
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

    Console.Clear();
    race.CalculateWinPayouts(betsList, winHorse);
    race.CalculatePlacePayouts(betsList, winHorse, showHorse);
    race.CalculateShowPayouts(betsList, winHorse, placeHorse, showHorse);
    Console.ReadLine();
    Console.Clear();

    bool IsPayoutMenuInputValid()
    {
        if (args.Length != 3)
        { 
            IO.InvalidInput();
            return false; 
        }
        if (!int.TryParse(args[0], out winHorse))
        { 
            IO.InvalidInput();
            return false; 
        }
        if (!int.TryParse(args[1], out placeHorse))
        { 
            IO.InvalidInput();
            return false; 
        }
        if (!int.TryParse(args[2], out showHorse))
        { 
            IO.InvalidInput();
            return false; 
        }
        else return true;
    }
}