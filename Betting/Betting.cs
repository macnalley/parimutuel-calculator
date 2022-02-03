//Methods for calling bets
public static class Betting
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

    private static string[] SplitIntoArgs(string input)
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
    
}