public enum BetType
{
    win,
    place,
    show
}

public static class BetTypeMethods
{
    public static BetType ParseBetType(string bet)
    {        
        switch (bet.ToLower())
        {
            case "win":
                return BetType.win;
            case "place":
                return BetType.place;
            case "show":
                return BetType.show;
            default:
                return BetType.win;
        }       
    }
}