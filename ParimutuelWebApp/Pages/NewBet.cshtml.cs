using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ParimutuelCalculator;

namespace MyApp.Namespace
{
    public class NewBetModel : PageModel
    {
        private readonly IHtmlHelper htmlHelper;

        public List<Bet> BetsList { get; set; } 

        [BindProperty]
        public Bet Bet { get; set; }
        public IEnumerable<SelectListItem> BetTypes { get; set; }

        public NewBetModel(IHtmlHelper htmlHelper, List<Bet> BetsList)
        {
            this.htmlHelper = htmlHelper;
            this.BetsList = BetsList;
        }

        public void OnGet()
        {
            Bet = new Bet();
            BetTypes = htmlHelper.GetEnumSelectList<BetType>();
        }

        public void OnPost()
        {
            BetsList.Add(Bet);
        }
    }
}
