List<Bet> betsList = new List<Bet>();

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
            betsList.Add(Betting.PlaceBet());
            break;
        case ConsoleKey.D2:
            IO.NotImplemented();
            break;
        case ConsoleKey.D3:
            IO.NotImplemented();
            break;
        case ConsoleKey.Escape:
            Environment.Exit(0);
            break;
        default:
            IO.InvalidInput(); 
            break;      
    }
}