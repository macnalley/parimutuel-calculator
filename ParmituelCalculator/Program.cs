using ParimutuelCalculator;

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
Console.WriteLine("\nPress \"Enter\" to continue.");

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
    Console.WriteLine("Type a number to choose an action or press \"Escape\" to exit:\n" +
        "1. Place bet.\n" + 
        "2. Display odds.\n" +
        "3. Calculate payouts.\n" +
        "4. Display all current bets.\n" +
        "5. Start a new race.");
    
    switch (Console.ReadKey(true).Key)
    { 
        case ConsoleKey.D1:
            Console.Clear();
            BetMenu();
            break;
        case ConsoleKey.D2:
            Console.Clear();
            BettingMethods.ShowOdds(betsList);
            Console.Clear();
            break;
        case ConsoleKey.D3:
            Console.Clear();
            PayoutMenu();
            Console.Clear();
            break;
        case ConsoleKey.D4:
            BetDisplayMenu();
            break;
        case ConsoleKey.D5:
            NewRaceMenu();
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

void BetDisplayMenu()
{
    if (BettingMethods.AreNoBets(betsList))
            { return; }

    Console.Clear();
    Console.WriteLine("Bets");

    for (int i = 1; i <= betsList.Count(); i++)
    {
        Console.WriteLine($"{i}. {betsList[i - 1].ToString()}");
    }
    
    Console.WriteLine("\nTo remove a bet, type the bet number and hit \"Enter.\" Else, hit enter to return.");
    string input = Console.ReadLine().Trim().Trim('.');

    if (BettingMethods.IsInt(input))
        { DeleteBetMenu(input); }

    Console.Clear();
}

void DeleteBetMenu(string str)
{
    int betNumber = int.Parse(str);
    Bet selectedBet = betsList[betNumber - 1];

    Console.Clear();
    Console.WriteLine($"{selectedBet}");    
    Console.WriteLine("\nType \"DELETE\" and hit enter to confirm.");
    string input = Console.ReadLine();
    
    if (input == "DELETE")
    {
        Bet betToRemove = betsList[betNumber - 1];
        betsList.Remove(betToRemove);
        Console.Clear();
        Console.WriteLine("Bet deleted.");
    }
    else
    {
        Console.WriteLine("Not confirmed.");
        Console.ReadLine();
        Console.Clear();
    }

}

void PayoutMenu()
{    
    if (BettingMethods.AreNoBets(betsList))
        { return; }

    int winHorse = 0;
    int placeHorse = 0;
    int showHorse = 0;
    string[] args;

    do 
    {
        Console.WriteLine("Enter the numbers of the first, second, and third place horses.\n" +
            "Or type \"Back\" to return");
        
        string input = Console.ReadLine();
        
        if (input.ToLower() == "back")
            { return; }
        
        args = BettingMethods.SplitIntoArgs(input);
    } 
    while (!IsPayoutMenuInputValid());

    Console.Clear();

    if (!BettingMethods.AreWinners(betsList, winHorse, placeHorse, showHorse))
        { Console.WriteLine("No winners."); }
    else 
    {
        race.Bets = betsList;
        Console.WriteLine("Payouts");
        race.CalculateWinPayouts(winHorse);
        race.CalculatePlacePayouts(winHorse, placeHorse);
        race.CalculateShowPayouts(winHorse, placeHorse, showHorse);
    }
    
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

void NewRaceMenu()
{
    Console.Clear();
    Console.WriteLine("Will clear all current bets.\n" +
        "Type \"CONFIRM\" to confirm, or hit \"Enter\" to return.");

    string input = Console.ReadLine();

    if (input == "CONFIRM")
        {
            Console.Clear();
            betsList = new List<Bet>();
            Console.WriteLine("New race started. All bets cleared.");
            Console.WriteLine();
            Console.ReadLine();
        }
    else
    {
        Console.WriteLine("Not confirmed.");
        Console.ReadLine();
        Console.Clear();
    }
}