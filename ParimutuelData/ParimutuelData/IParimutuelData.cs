using ParimutuelCalculator;

namespace ParimutuelData;
public interface IParimutuelData
{
    Race GetRace();
}

public class InMemoryRaces : IParimutuelData
{
    public Race race;

    public InMemoryRaces()
    {
        race = new Race();

        race.Bets = new List<Bet>()
        {
            new Bet{ Name = "Mack", Horse = 1, BetType = BetType.win, Amount = 2.00 },
            new Bet{ Name = "Matilda", Horse = 2, BetType = BetType.place, Amount = 3.00 },
            new Bet{ Name = "Boo Kitty", Horse = 3, BetType = BetType.show, Amount = 4.00 },
            new Bet{ Name = "Spike", Horse = 4, BetType = BetType.win, Amount = 5.00 },
            new Bet{ Name = "Sunshine", Horse = 5, BetType = BetType.place, Amount = 6.00 },
            new Bet{ Name = "Dixie", Horse = 1, BetType = BetType.show, Amount = 7.00 },
            new Bet{ Name = "Sourcream", Horse = 2, BetType = BetType.win, Amount = 8.00 }
        };
    }

    public Race GetRace()
    {
        return race;
    }
}
