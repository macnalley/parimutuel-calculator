//Methods for calling bets
public static class BettingMethods
{
    public static Bet PlaceBet()
    {
        Console.WriteLine("Enter bettor name, horse number, bet type (optional), and amount (optional) in that order.\n" +
                            "Note: Default bet type is win, and default bet is $2.00.");
        string[] args = SplitIntoArgs(Console.ReadLine());

        switch (args.Length)
        {
            case 2:
                return CreateBet(args[0], args[1]);
            case 3:
                return CreateBet(args[0], args[1], args[2]);
            case 4:
                return CreateBet(args[0], args[1], args[2], args[3]);
            default:
                IO.InvalidInput();
                return PlaceBet();
        }
    }

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

    private static Bet CreateBet(string name, string horse)
    {
        int horseInt;
        if (!int.TryParse(horse, out horseInt))
            {
                IO.InvalidInput();
                return PlaceBet();
            }
        else return new Bet(name, horseInt);
    }

    private static Bet CreateBet(string name, string horse, string amountOrBet)
    {
        int horseInt;
        if (!int.TryParse(horse, out horseInt))
            {
                IO.InvalidInput();
                return PlaceBet();
            }
        
        double amountDouble;
        if (!double.TryParse(amountOrBet, out amountDouble))
        {
            BetType bet = new BetType();
            if (!IsValidBetType(amountOrBet))
            {
                IO.InvalidInput();
                return PlaceBet();
            }
            else 
            {
                bet.GetBetType(amountOrBet);
                return new Bet(name, horseInt, bet);
            }         
        }

        else return new Bet(name, horseInt, amountDouble);
    }

    private static Bet CreateBet(string name, string horse, string bet, string amount)
    {
        int horseInt;
        if (!int.TryParse(horse, out horseInt))
        {
            IO.InvalidInput();
            return PlaceBet();
        }
        
        double amountDouble;
        if (!double.TryParse(amount, out amountDouble))
        {
            IO.InvalidInput();
            return PlaceBet();
        }

        BetType betType = new BetType();
        if (!IsValidBetType(bet))
        {
            IO.InvalidInput();
            return PlaceBet();
        }
        else 
        {
            betType.GetBetType(bet);
            return new Bet(name, horseInt, amountDouble, betType);
        } 
    }

    private static bool IsValidBetType(string bet)
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

}