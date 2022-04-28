namespace ParimutuelCalculator;

public class Race
{  
    public double TakePercentage { get; set; }
    public DateTime Date { get; set; }
    public int NumOfHorses { get; set; }
    public List<Horse> Horses { get; set; }
    public List<Bet> Bets { get; set; }
    public int LastBetId { get; set; }
    public double WinTotal { get; set; }
    public double PlaceTotal { get; set; }
    public double ShowTotal { get; set; }
    public int WinHorse { get; set; }
    public int PlaceHorse { get; set; }
    public int ShowHorse { get; set; }    
    
    public Race()
    {
        // This sets the percentage of all bets that the track collects as a fee.
        // The default is 10%
        TakePercentage = 0.1;
        NumOfHorses = 20;
        Date = DateTime.Today;
        InstantiateHorses();
        Bets = new List<Bet>();
        LastBetId = 0;
        WinTotal = 0;
        PlaceTotal = 0;
        ShowTotal = 0;
    }

    public void CalculateWinPayouts(int horse)
    {
        // Calculates the amount of money bet on all horses to win
        CalculatePool(BetType.win);
        CalculateHorsePool(BetType.win);
    
        // The amount kept as a fee by the house
        double take = WinTotal * TakePercentage;

        // The total amount of money bet to place on the winning and placing horses
        double winHorseTotal = CalculateHorseTotal(Bets, BetType.place, horse);

        // The remaining amount to be paid out to correct bets after paying the house 
        // and paying back the winning bets' initial wager
        double purse = WinTotal - take - winHorseTotal;

        // The amount paid per $2 bet to winning bettors
        double payout = purse / WinTotal;

        // Rounds the payout down to nearest 10 cents
        payout = Math.Floor(payout * 10) / 10;

        // Guarantees a payout from the house if odds are low or negative
        if (payout < 0.10) 
            { payout = 0.10; }

        Bets.Where(bet => bet.Horse == WinHorse && bet.BetType == BetType.win)
            .ToList()
            .ForEach(bet => bet.SetAmountOwed(payout));
    }

    public void CalculatePlacePayouts(int winHorse, int placeHorse)
    {
        // The amount of money bet on all horses to place
        CalculatePool(BetType.place);
        CalculateHorsePool(BetType.place);
    
        // The amount kept as a fee by the house
        double take = PlaceTotal * TakePercentage;

        // The total amount of money bet to place on the winning and placing horses
        double winHorseTotal = CalculateHorseTotal(Bets, BetType.place, winHorse);
        double placeHorseTotal = CalculateHorseTotal(Bets, BetType.place, placeHorse);

        // The total bet on both horses
        double horseTotal = winHorseTotal + placeHorseTotal;

        // The remaining amount to be paid out to correct bets on each horse after paying the house 
        // and paying back the placing bets' initial wager. The purse is split in two, as the money
        // is disbursed evenly to the bettors of both horses
        double purse = (PlaceTotal - take - horseTotal) / 2;

        // The amount paid per $2 bet to bettors of the winning and placing horse
        double winPayout = (purse / winHorseTotal);
        double placePayout = (purse / placeHorseTotal);

        // Rounds the payout down to nearest 10 cents
        winPayout = Math.Floor(winPayout * 10) / 10;
        placePayout = Math.Floor(placePayout * 10) / 10;

        // Guarantees a payout from the house if odds are low or negative
        if (winPayout < 0.10)
            { winPayout = 0.10; }
        if (placePayout < 0.10) 
            { placePayout = 0.10; }

        Bets.Where(bet => bet.Horse == WinHorse && bet.BetType == BetType.place)
            .ToList()
            .ForEach(bet => bet.SetAmountOwed(winPayout));

        Bets.Where(bet => bet.Horse == PlaceHorse && bet.BetType == BetType.place)
            .ToList()
            .ForEach(bet => bet.SetAmountOwed(placePayout));
    }

    public void CalculateShowPayouts(int winHorse, int placeHorse, int showHorse)
    {
        // The amount of money bet on all horses to show
        CalculatePool(BetType.show);
        CalculateHorsePool(BetType.show);
    
        // The amount kept as a fee by the house
        double take = ShowTotal * TakePercentage;

        // The total amount of money bet to place on the winning, placing, and showing horses
        double winHorseTotal = CalculateHorseTotal(Bets, BetType.show, winHorse);
        double placeHorseTotal = CalculateHorseTotal(Bets, BetType.show, placeHorse);
        double showHorseTotal = CalculateHorseTotal(Bets, BetType.show, showHorse);

        double horseTotal = winHorseTotal + placeHorseTotal + showHorseTotal;

        // The remaining amount to be paid out to correct bets on each horse after paying the house 
        // and paying back the showing bets' initial wagers. The purse is split in three, as the money
        // is disbursed evenly to the bettors of all three horses
        double purse = (ShowTotal - take - horseTotal) / 3;

        // The amount paid per $2 bet to bettors of the winning, placing, and showing horses
        double winPayout = (purse / winHorseTotal);
        double placePayout = (purse / placeHorseTotal);
        double showPayout =  (purse / showHorseTotal);

        // Rounds the payout down to nearest 10 cents
        winPayout = Math.Floor(winPayout * 10) / 10;
        placePayout = Math.Floor(placePayout * 10) / 10;
        showPayout = Math.Floor(showPayout * 10) / 10;

        // Guarantees a payout from the house if odds are low or negative
        if (winPayout < 0.10)
            { winPayout = 0.10; }
        if (placePayout < 0.10) 
            { placePayout = 0.10; }
        if (showPayout < 0.10)
            { showPayout = 0.10; }

        Bets.Where(bet => bet.Horse == WinHorse && bet.BetType == BetType.show)
            .ToList()
            .ForEach(bet => bet.SetAmountOwed(winPayout));

        Bets.Where(bet => bet.Horse == PlaceHorse && bet.BetType == BetType.show)
            .ToList()
            .ForEach(bet => bet.SetAmountOwed(placePayout));

        Bets.Where(bet => bet.Horse == ShowHorse && bet.BetType == BetType.show)
            .ToList()
            .ForEach(bet => bet.SetAmountOwed(showPayout));   
    }
    
    // Calculates the total amount wagered on a given horse and bet type. 
    //E.g., the total amount placed by all bettors to win on horse 1. 
    private double CalculateHorseTotal(List<Bet> betsList, BetType betType, int horse)
    {
        double total = (from bet in betsList
                        where bet.BetType == betType && bet.Horse == horse
                        select bet.Amount).Sum();

        return total;
    }

    // Calculates the total wagered on a particular kind of bet. 
    // E.g., total amount wagered on horses to win.
    private double CalculateBetTotal(List<Bet> betsList, BetType betType)
    {
        double total = (from bet in betsList
                        where bet.BetType == betType
                        select bet.Amount).Sum();

        return total;
    }

    private void InstantiateHorses()
    {
        Horses = new List<Horse>();
        
        for (int i = 1; i <= NumOfHorses; i++)
        {
            var horse = new Horse(i);
            Horses.Add(horse);
        }
    }

    // Calculates the the total amount wagered by all bets on a bet type.
    public void CalculatePool(BetType betType)
    {
        double pool = Bets.Where(bet => bet.BetType == betType)
                          .Select(bet => bet.Amount)
                          .Sum();

        switch (betType)
        {
            case BetType.win:
                WinTotal = pool;
                break;
            case BetType.place:
                PlaceTotal = pool;
                break;
            case BetType.show:
                ShowTotal = pool;
                break;
            default:
                break;
        }
                
    }

    // Calculates the amount wagered on each horse by all bets of a given bet type.
    public void CalculateHorsePool(BetType betType)
    {   
        foreach (var horse in Horses)
        {
            double pool = Bets.Where(bet => bet.Horse == horse.HorseNumber &&
                                            bet.BetType == betType)
                              .Select(bet => bet.Amount)
                              .Sum(); 

            switch (betType)
            {
                case BetType.win:
                    horse.WinPool = pool;
                    break;
                case BetType.place:
                    horse.PlacePool = pool;
                    break;
                case BetType.show:
                    horse.ShowPool = pool;
                    break;
                default:
                    break;              
            }
        }
    }

}