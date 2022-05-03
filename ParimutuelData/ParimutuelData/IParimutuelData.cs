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
        race = new Race()
        {
            // LastBetId = 7
        };

        race.Bets = new List<Bet>()
        {
            // new Bet{ Id = 1, Name = "Mack", Horse = 1, BetType = BetType.win, Amount = 2.00 },
            // new Bet{ Id = 2, Name = "Matilda", Horse = 2, BetType = BetType.place, Amount = 3.00 },
            // new Bet{ Id = 3, Name = "Boo Kitty", Horse = 3, BetType = BetType.show, Amount = 4.00 },
            // new Bet{ Id = 4, Name = "Spike", Horse = 4, BetType = BetType.win, Amount = 5.00 },
            // new Bet{ Id = 5, Name = "Sunshine", Horse = 5, BetType = BetType.place, Amount = 6.00 },
            // new Bet{ Id = 6, Name = "Dixie", Horse = 1, BetType = BetType.show, Amount = 7.00 },
            // new Bet{ Id = 7, Name = "Giuseppe", Horse = 2, BetType = BetType.win, Amount = 8.00 }
        };
    }

    public Race GetRace()
    {
        return race;
    }
}
