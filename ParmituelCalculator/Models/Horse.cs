namespace ParimutuelCalculator;

public class Horse
{
    public int HorseNumber { get; set; }
    public double WinPool { get; set; }
    public double PlacePool { get; set; }
    public double ShowPool { get; set; }
    public double Odds { get; set; }

    public Horse(int horseNumber)
    {
        HorseNumber = horseNumber;
        WinPool = 0;
        PlacePool = 0;
        ShowPool = 0;
    }

    public double CalculateOdds(double betTotal, BetType betType)
    {
        double horsePool;

        switch (betType)
        {
            case BetType.win:
                horsePool = WinPool;
                break;
            case BetType.place:
                horsePool = PlacePool / 2;
                betTotal = betTotal / 2;
                break;
            case BetType.show:
                horsePool = ShowPool / 3;
                betTotal = betTotal / 3;
                break;
            default:
                horsePool = 0;
                break;
    }

        if (horsePool == 0)
        {
            horsePool = 2.00;
            betTotal += 2.00;
        }

        double odds = (betTotal - horsePool) / horsePool;

        if (odds < 1)
            odds = 1;

        return odds;
    }
}

