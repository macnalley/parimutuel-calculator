namespace ParimutuelCalculator;

//Methods for calling bets
public static class BettingMethods
{
    public static string[] SplitIntoArgs(string input)
    {
        string[] args;
        
        if (input.Contains(','))
        {
            args = input.Split(',');
        } 
        else 
        {
            args = input.Split(' ');
        }

        for (int i = 0; i < args.Length; i++)
        {
            args[i] = args[i].Trim().Trim(',');
        }

        return args;
    }

    public static bool IsValidBetType(string bet)
    {
        if (bet.ToLower() == "win" || 
            bet.ToLower() == "place" || 
            bet.ToLower() == "show")
            { return true; }
        else return false;
    }
    
    public static void ShowOdds(List<Bet> bets)
    {
        if (AreNoBets(bets))
            { return; }

        Console.WriteLine("Odds");

        int numHorses = FindHighestHorseNumber(bets);
        
        for (int i = 1; i <= numHorses; i++)
        {
            double odds = CalculateOdds(bets, i);
            string oddsString = DoubleToFraction(odds);

            Console.WriteLine($"Horse {i}: {oddsString}");
        }

        Console.ReadLine();

    }

    private static double CalculateOdds(List<Bet> bets, int horseNumber)
    {
        // Calculates the total amount wagered on bets to win
        double winTotal = (from bet in bets
                           where bet.BetType == BetType.win
                           select bet.Amount).Sum();

        // Calculates the total amount wagered on this horse to win 
        double horseTotal = (from bet in bets
                             where bet.Horse == horseNumber &&
                                   bet.BetType == BetType.win
                             select bet.Amount).Sum();

        // If there are no bets are the current horse, the program
        // imagines an additional minimum bet to create odds.
        if (horseTotal == 0)
        { 
            horseTotal = 2.00; 
            winTotal += 2.00;        
        }

        // The available amount to pay to winners
        double winnings = winTotal - horseTotal;

        double ratio = winnings / horseTotal;

        // Guarantees a minimum return of money
        if (ratio < 1)
            { ratio = 1; }

        return ratio;
    }

    private static int FindHighestHorseNumber(List<Bet> bets)
    {
        int highestNumber = bets[0].Horse;

        for (int i = 1; i < bets.Count; i++)
        {
            if (bets[i].Horse > bets[i - 1].Horse)
                { highestNumber = bets[i].Horse; }     
        }

        return highestNumber;
    }

    private static string DoubleToFraction(double odds)
    {
        if (odds < 5)
        {
            double roundedOdds = Math.Round(odds * 2) / 2;
            if (roundedOdds % 1 == 0)
                { return $"{roundedOdds}"; }
            else 
            {
                double numerator = roundedOdds * 2;
                return $"{numerator}/2";
            }
            
        }
        else 
        {
            double roundedOdds = Math.Round(odds);
            return $"{roundedOdds}";
        }
    }

    public static bool IsInt(string str)
    {
        int x;
        return int.TryParse(str, out x);
    }

    public static bool IsDouble(string str)
    {
        double x;
        return double.TryParse(str, out x);
    }

    public static bool IsValidBet(string[] args)
    {
        if (args.Count() == 2 && 
            BettingMethods.IsInt(args[1]))
            { return true; }
        
        else if (args.Count() == 3 &&
            BettingMethods.IsInt(args[1]) &&
            BettingMethods.IsValidBetType(args[2]))
            { return true; }

        else if (args.Count() == 3 &&
            BettingMethods.IsInt(args[1]) &&
            BettingMethods.IsDouble(args[2]) &&
            Double.Parse(args[2]) >= 2.00)
            { return true; }

        else if (args.Count() == 4 &&
            BettingMethods.IsInt(args[1]) &&
            BettingMethods.IsValidBetType(args[2]) &&
            BettingMethods.IsDouble(args[3]) &&
            Double.Parse(args[3]) >= 2.00)
            { return true; }

        else return false;

    }

    public static void DisplayBets(List<Bet> betsList)
    {        
        Console.Clear();
        Console.WriteLine("Bets");

        for (int i = 1; i <= betsList.Count(); i++)
        {
            Console.WriteLine($"{i}. {betsList[i - 1].ToString()}");
        }
    }

    public static bool AreNoBets(List<Bet> betsList)
    {
        if (betsList.Count() == 0)
        {
            Console.Clear();
            Console.WriteLine("No bets placed on this race.");
            Console.ReadLine();
            Console.Clear();
            return true;
        }
        else return false;
    }

    public static bool AreWinners(List<Bet> betsList, int winHorse, int placeHorse, int showHorse)
    {
        Bet? winWinner = (from bet in betsList 
                          where bet.Horse == winHorse
                          select bet).FirstOrDefault();

        Bet? placeWinner = (from bet in betsList 
                            where bet.Horse == placeHorse
                            select bet).FirstOrDefault();

        Bet? showWinner = (from bet in betsList 
                           where bet.Horse == showHorse
                           select bet).FirstOrDefault();
        
        if (winWinner != null || placeWinner != null || showWinner != null)
            { return true; }
        else return false;
    }
}