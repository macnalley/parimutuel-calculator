using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParimutuelCalculator;
using ParimutuelData;

namespace MyApp.Namespace
{
    public class BetsModel : PageModel
    {
        private readonly IParimutuelData parimutuelData;
        
        public IEnumerable<Bet> Bets { get; set; }

        public BetsModel(IParimutuelData parimutuelData)
        {
            this.parimutuelData = parimutuelData;
        }
        
        public void OnGet()
        {
            Bets = parimutuelData.GetRace().Bets; 
        }
    }
}
