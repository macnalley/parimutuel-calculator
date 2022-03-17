using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ParimutuelCalculator;
using ParimutuelData;

namespace MyApp.Namespace
{
    public class PayoutsModel : PageModel
    {
        private readonly IParimutuelData parimutuelData;

        [BindProperty]
        public Race Race { get; set; }

        public PayoutsModel(IParimutuelData parimutuelData)
        {
            this.parimutuelData = parimutuelData;
        }

        public void OnGet()
        {
            Race = new Race();
        }

        public IActionResult OnPost()
        {
            if (!AreWinnersValid())
                return Page();
            else
            {

                Race.Bets = parimutuelData.GetRace().Bets;
                
                foreach(var bet in Race.Bets)
                    { bet.AmountOwed = 0; }
                
                Race.CalculateWinPayouts(Race.WinHorse);
                Race.CalculatePlacePayouts(Race.WinHorse, Race.PlaceHorse);
                Race.CalculateShowPayouts(Race.WinHorse, Race.PlaceHorse, Race.ShowHorse);

                return Page();
            }
        }

        private bool AreWinnersValid()
        {
            if 
            (
                Race.WinHorse == Race.PlaceHorse || 
                Race.WinHorse == Race.ShowHorse || 
                Race.PlaceHorse == Race.ShowHorse ||
                Race.WinHorse == null || Race.WinHorse <= 0 || Race.WinHorse > Race.NumOfHorses ||
                Race.PlaceHorse == null || Race.PlaceHorse <= 0 || Race.PlaceHorse > Race.NumOfHorses ||
                Race.ShowHorse == null || Race.ShowHorse <= 0 || Race.ShowHorse > Race.NumOfHorses
            )
                return false;
            else return true;
        }
    }
}
