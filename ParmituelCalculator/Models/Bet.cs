using System.ComponentModel.DataAnnotations;

namespace ParimutuelCalculator;

public class Bet
{  
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required, Range(1, 20)]  
    public int Horse { get; set; }
    
    public BetType BetType { get; set; }
    
    [Required, Range(2.00, 9999.99)]
    public double Amount { get; set; }
    
    public double AmountOwed { get; set; }


    public Bet()
    {
        Amount = 2.00;
        BetType = BetType.win;
        AmountOwed = 0;
    }

    
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

    public void CreateBet(string[] args)
    {
        Name = args[0];
        Horse = int.Parse(args[1]);
        
        if (args.Count() == 3)
        {
            if (BettingMethods.IsDouble(args[2]))
            {
                Amount = double.Parse(args[2]);
            }
            else BetType = BetTypeMethods.ParseBetType(args[2]);
        }

        if (args.Count() == 4)
        {
            Amount = double.Parse(args[3]);
            BetType = BetTypeMethods.ParseBetType(args[3]);
        }

    }

    public override string ToString()
    {
        return $"{Name}: ${Amount} to {BetType} on horse {Horse}";
    }

    public void SetAmountOwed(double payout)
    {
        AmountOwed = (Amount * payout) + Amount; 
    }
}