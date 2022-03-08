namespace ParimutuelCalculator;

public class Horse
{
    public int RaceOfDay { get; set; }
    public int HorseNumber { get; set; }
    public int WinBets { get; set; }
    public int PlaceBets { get; set; }
    public int ShowBets { get; set; }

    public Horse(int raceOfDay, int horseNumber)
    {
        RaceOfDay = raceOfDay;
        HorseNumber = HorseNumber;
    }
}

