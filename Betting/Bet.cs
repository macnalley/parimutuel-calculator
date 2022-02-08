public class Bet
{
    public string Name { get; set; }
    public int Horse { get; set; }
    public double Amount { get; set; }
    public BetType BetType { get; set; }
    public double AmountOwed { get; set; }

    public Bet(string name, int horse)
    {
        Name = name;
        Horse = horse;
        Amount = 2.00;
        BetType = BetType.win;
    }

    public Bet(string name, int horse, double amount)
    {
        Name = name;
        Horse = horse;
        Amount = amount;
        BetType = BetType.win;
    }

    public Bet(string name, int horse, BetType betType)
    {
        Name = name;
        Horse = horse;
        Amount = 2.00;
        BetType = betType;

    }

    public Bet(string name, int horse, double amount, BetType betType)
    {
        Name = name;
        Horse = horse;
        Amount = amount;
        BetType = betType;
    }

}

public enum BetType
{
    win,
    place,
    show
}

public static class BetTypeMethods
{
    public static void GetBetType(this BetType betType, string bet)
    {        
        switch (bet.ToLower())
        {
            case "win":
                betType = BetType.win;
                break;
            case "place":
                betType = BetType.show;
                break;
            case "show":
                betType = BetType.show;
                break;
            default:
                break;
        }       
    }
}