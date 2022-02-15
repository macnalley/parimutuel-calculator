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

// betsList.Add(new Bet("Mack", 1, 2.00, BetType.win));
// betsList.Add(new Bet("Mack", 2, 3.00, BetType.win));
// betsList.Add(new Bet("Mack", 3, 20.00, BetType.show));
// betsList.Add(new Bet("Mack", 4, 2.00, BetType.win));
// betsList.Add(new Bet("Mack", 5, 2.00, BetType.win));
// betsList.Add(new Bet("Mack", 6, 2.00, BetType.win));


// A loop to run a main menu that can be closed with the escape key.
while (true)
    { RunMainMenu(); }


void RunMainMenu()
{
    Console.WriteLine("Type a number to choose an action or Press \"Escape\" to exit:\n" +
        "1. Place bet.\n" + 
        "2. Display odds.\n" +
        "3. Calculate payouts.\n" +
        "4. Display all current bets");
    
    switch (Console.ReadKey(true).Key)
    { 
        case ConsoleKey.D1:
            Console.Clear();
            BetMenu();
            break;
        case ConsoleKey.D2:
            Console.Clear();
            BettingMethods.ShowOdds(betsList);
            Console.ReadLine();
            Console.Clear();
            break;
        case ConsoleKey.D3:
            PayoutMenu();
            break;
        case ConsoleKey.D4:
            BettingMethods.DisplayBets(betsList);
            break;
        case ConsoleKey.Escape:
            Environment.Exit(0);
            break;
        default:
            IO.InvalidInput(); 
            break;      
    }
}

void BetMenu()
{   
    string[] args;

    do
    {
        Console.WriteLine("Enter bettor name, horse number, bet type (optional), and amount (optional), in that order,\n" +
                            "or type \"Back\" to return.\n" +
                            "Note: Default bet type is win, and default bet is $2.00.");
        
        string input = Console.ReadLine();
        
        if (input.ToLower() == "back")
            {
                Console.Clear();
                break;
            }
        
        else 
        {   
            args = BettingMethods.SplitIntoArgs(input);

            if (!BettingMethods.IsValidBet(args))
            { 
                    Console.Clear();
                    Console.WriteLine("Invalid Bet"); 
            }

            if (BettingMethods.IsValidBet(args))
            { 
                Bet bet = new Bet();
                bet.CreateBet(args);
                betsList.Add(bet);
                Console.Clear();
                Console.WriteLine("Bet added.\n");
            }
        }
    }
    while (!BettingMethods.IsValidBet(args));

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