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
        int numHorses = FindHighestHorseNumber(bets);
        
        for (int i = 1; i <= numHorses; i++)
        {
            double odds = CalculateOdds(bets, i);
            string oddsString = DoubleToFraction(odds);

            Console.WriteLine($"Horse {i}: {oddsString}");
        }
    }

    private static double CalculateOdds(List<Bet> bets, int horseNumber)
    {
        // Calculates the total amount wagered on bets to win
        double winTotal = 0;
        foreach (Bet bet in bets)
        {
            if (bet.BetType == BetType.win)
                { winTotal += bet.Amount; }
        }

        // Calculates the total amount wagered on this horse to win 
        double horseTotal = 0;
        foreach (Bet bet in bets)
        {
            if (bet.Horse == horseNumber && bet.BetType == BetType.win)
                { horseTotal += bet.Amount; }
        }

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
            BettingMethods.IsDouble(args[2]))
            { return true; }

        else if (args.Count() == 4 &&
            BettingMethods.IsInt(args[1]) &&
            BettingMethods.IsValidBetType(args[2]) &&
            BettingMethods.IsDouble(args[3]))
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

        Console.ReadLine();
        Console.Clear();

    }

}