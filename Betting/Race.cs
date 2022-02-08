public class Race
{
    public double TakePercentage { get; set; }
    public double WinTotal { get; set; }
    public double PlaceTotal { get; set; }
    public double ShowTotal { get; set; }
    
    public Race()
    {
        // This sets the percentage of all bets that the track collects as a fee.
        // The default is 10%
        TakePercentage = 0.1;
    }

    public void CalculateWinPayouts(List<Bet> betsList, int horse)
    {
        // The amount of money bet on all horses to win
        WinTotal = CalculateBetTotal(betsList, BetType.win);
    
        // The amount kept as a fee by the house
        double take = WinTotal * TakePercentage;

        // The total amount of money bet to win on the winning horse
        double horseTotal = CalculateHorseTotal(betsList, BetType.win, horse);

        // The remaining amount to be paid out to correct bets after paying the house 
        // and paying back the winning bets' initial wager
        double purse = WinTotal - take - horseTotal;

        // The amount paid per $2 bet to winning bettors
        double payout = purse / horseTotal * 2.00;

        // Rounds the payout down to nearest 10 cents
        payout = Math.Floor(payout * 10) / 10;

        // Guarantees a payout from the house if odds are low or negative
        if (payout < 0.10) 
            { payout = 0.10; }

        foreach (Bet bet in betsList)
        {
            if (bet.Horse == horse)
            {
                double amountOwed = bet.Amount * (payout / 2.00) + bet.Amount; 
                Console.WriteLine($"{ bet.Name } is owed ${ amountOwed }0");
            }
        }
    }

    public void CalculatePlacePayouts(List<Bet> betsList, int winHorse, int placeHorse)
    {
        // The amount of money bet on all horses to place
        PlaceTotal = CalculateBetTotal(betsList, BetType.place);
    
        // The amount kept as a fee by the house
        double take = PlaceTotal * TakePercentage;

        // The total amount of money bet to place on the winning and placing horses
        double winHorseTotal = CalculateHorseTotal(betsList, BetType.place, winHorse);
        double placeHorseTotal = CalculateHorseTotal(betsList, BetType.place, placeHorse);

        double horseTotal = winHorseTotal + placeHorseTotal;

        // The remaining amount to be paid out to correct bets on each horse after paying the house 
        // and paying back the placing bets' initial wager. The purse is split in two, as the money
        // is disbursed evenly to the bettors of both horses
        double purse = (PlaceTotal - take - horseTotal) / 2;

        // The amount paid per $2 bet to bettors of the winning and placing horse
        double winPayout = (purse / winHorseTotal * 2.00);
        double placePayout = (purse / placeHorseTotal * 2.00);

        // Rounds the payout down to nearest 10 cents
        winPayout = Math.Floor(winPayout * 10) / 10;
        placePayout = Math.Floor(placePayout * 10) / 10;

        // Guarantees a payout from the house if odds are low or negative
        if (winPayout < 0.10)
            { winPayout = 0.10; }
        if (placePayout < 0.10) 
            { placePayout = 0.10; }

        foreach (Bet bet in betsList)
        {
            if (bet.Horse == winHorse)
            {
                double amountOwed = bet.Amount * (winPayout / 2.00) + bet.Amount; 
                if (amountOwed > 0)
                    { Console.WriteLine($"{ bet.Name } is owed ${ amountOwed }0"); }
            }
        }
        foreach (Bet bet in betsList)
        {
            if (bet.Horse == placeHorse)
            {
                double amountOwed = bet.Amount * (placePayout / 2.00) + bet.Amount; 
                if (amountOwed > 0)
                    { Console.WriteLine($"{ bet.Name } is owed ${ amountOwed }0"); }
            }
        }
    }

    public void CalculateShowPayouts(List<Bet> betsList, int winHorse, int placeHorse, int showHorse)
    {
        // The amount of money bet on all horses to show
        ShowTotal = CalculateBetTotal(betsList, BetType.show);
    
        // The amount kept as a fee by the house
        double take = ShowTotal * TakePercentage;

        // The total amount of money bet to place on the winning, placing, and showing horses
        double winHorseTotal = CalculateHorseTotal(betsList, BetType.place, winHorse);
        double placeHorseTotal = CalculateHorseTotal(betsList, BetType.place, placeHorse);
        double showHorseTotal = CalculateHorseTotal(betsList, BetType.place, showHorse);

        double horseTotal = winHorseTotal + placeHorseTotal + showHorseTotal;

        // The remaining amount to be paid out to correct bets on each horse after paying the house 
        // and paying back the showing bets' initial wagers. The purse is split in three, as the money
        // is disbursed evenly to the bettors of all three horses
        double purse = (ShowTotal - take - horseTotal) / 3;

        // The amount paid per $2 bet to bettors of the winning, placing, and showing horses
        double winPayout = (purse / winHorseTotal * 2.00);
        double placePayout = (purse / placeHorseTotal * 2.00);
        double showPayout =  (purse / showHorseTotal * 2.00);

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

        foreach (Bet bet in betsList)
        {
            if (bet.Horse == winHorse)
            {
                double amountOwed = bet.Amount * (winPayout / 2.00) + bet.Amount; 
                if (amountOwed > 0)
                { Console.WriteLine($"{ bet.Name } is owed ${ amountOwed }0"); }
            }
        }
        foreach (Bet bet in betsList)
        {
            if (bet.Horse == placeHorse)
            {
                double amountOwed = bet.Amount * (placePayout / 2.00) + bet.Amount; 
                if (amountOwed > 0)
                { Console.WriteLine($"{ bet.Name } is owed ${ amountOwed }0"); }
            }
        }
        foreach (Bet bet in betsList)
        {
            if (bet.Horse == showHorse)
            {
                double amountOwed = bet.Amount * (showPayout / 2.00) + bet.Amount; 
                if (amountOwed > 0)
                    { Console.WriteLine($"{ bet.Name } is owed ${ amountOwed }0"); }
            }
        }
    }
    
    // Calculates the total amount wagered on a given horse and bet type. 
    //E.g., the total amount placed by all bettors to win on horse 1. 
    private double CalculateHorseTotal(List<Bet> betsList, BetType betType, int horse)
    {
        double total = 0;

        foreach (Bet bet in betsList)
        {
            if (bet.BetType == betType && bet.Horse == horse)
                { total += bet.Amount; }
        }

        return total;
    }

    // Calculates the total wagered on a particular kind of bet. 
    // E.g., total amount wagered on horses to win.
    private double CalculateBetTotal(List<Bet> betsList, BetType betType)
    {
        double total = 0;

        foreach (Bet bet in betsList)
        {
            if (bet.BetType == betType)
                { total += bet.Amount; }
        }

        return total;
    }
}