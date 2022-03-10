namespace ParimutuelCalculator;

public class Horse
{
    public int HorseNumber { get; set; }
    public int WinBets { get; set; }
    public int PlaceBets { get; set; }
    public int ShowBets { get; set; }

    public Horse(int horseNumber)
    {
        HorseNumber = HorseNumber;
        WinBets = 0;
        PlaceBets = 0;
        ShowBets = 0;
    }
}

