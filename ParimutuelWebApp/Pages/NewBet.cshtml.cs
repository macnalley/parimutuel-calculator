using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ParimutuelCalculator;
using ParimutuelData;

namespace MyApp.Namespace
{
    public class NewBetModel : PageModel
    {
        private readonly IParimutuelData parimutuelData; 
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Bet Bet { get; set; }
        public IEnumerable<SelectListItem> BetTypes { get; set; }

        public NewBetModel(IHtmlHelper htmlHelper, IParimutuelData parimutuelData)
        {
            this.htmlHelper = htmlHelper;
            this.parimutuelData = parimutuelData;
        }

        public void OnGet()
        {
            Bet = new Bet();
            BetTypes = htmlHelper.GetEnumSelectList<BetType>();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                parimutuelData.GetRace().Bets.Add(Bet);
                return RedirectToPage("./Bets");
            }
            else
            {
                BetTypes = htmlHelper.GetEnumSelectList<BetType>();
                return Page();
            }
            
            
        }
    }
}
