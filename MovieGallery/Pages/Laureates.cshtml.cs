using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieGallery.Pages
{
    public class LaureatesModel : PageModel
    {
        public void OnGet()
        {
            using (var webClient = new System.Net.WebClient())
            {
                IDictionary<long, QuickType.Laureates> allNobels = new Dictionary<long, QuickType.Laureates>();
                string nobelJSON = webClient.DownloadString("http://api.nobelprize.org/v1/laureate.json");
                QuickType.Laureates nobel = QuickType.Laureates.FromJson(nobelJSON);
                List<QuickType.Laureate> laureate1 = nobel.NobleLaureates;
                ViewData["Laureates"] = laureate1;
            }
        }
    }
}
