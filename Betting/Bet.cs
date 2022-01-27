public class Bet
{
    public string Name { get; set; }
    public int Horse { get; set; }
    public double Amount { get; set; }
    public BetType BetType { get; set; }

    public Bet(string name, int horse, double amount, BetType betType)
    {
        Name = name;
        Horse = horse;
        Amount = amount;
        BetType = betType;
    }

}