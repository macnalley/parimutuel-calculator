using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParimutuelCalculator;
using ParimutuelData;

namespace MyApp.Namespace
{
    public class OddsModel : PageModel
    {
        private readonly IParimutuelData parimutuelData;

        public Race Race { get; set; }
        
        public OddsModel(IParimutuelData parimutuelData)
        {
            this.parimutuelData = parimutuelData;
        }
        
        public void OnGet()
        {
            Race = parimutuelData.GetRace();
            Race.CalculatePool(BetType.win);
            Race.CalculatePool(BetType.place);
            Race.CalculatePool(BetType.show);
            Race.CalculateHorsePool(BetType.win);
        }
    }
}
