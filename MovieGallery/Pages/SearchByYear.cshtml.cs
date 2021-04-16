using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MovieGallery.Model.Movies;

namespace MovieGallery.Pages
{
    public class SearchByYearModel : PageModel
    {
        private readonly ILogger<SearchByYearModel> _logger;

        public SearchByYearModel(ILogger<SearchByYearModel> logger)
        {
            _logger = logger;
        }

        //Returns  JSON response containing movies in that year
        public JsonResult OnGet(long year)
        {  
            try
            {
                year = (year == 0) ? 1994 : year;//if  year entered is null, by default take year=1994
                Movies response = new Movies();
                using (WebClient webClient = new WebClient())
                {
                    string moviesJson = webClient.DownloadString("https://imdb-api.com/en/API/Top250Movies/k_81ggrpaf");
                    response = Movies.FromJson(moviesJson);
                }
                var result = response.Items.Where(x => x.Year == year).ToList();
                response = new Movies()
                {
                    Items = result
                };
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured on OnGet Search by year {year}: {ex}");
                throw ex;
            }
        }
    }
}
