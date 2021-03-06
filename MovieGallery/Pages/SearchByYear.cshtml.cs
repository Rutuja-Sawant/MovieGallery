using System;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MovieGallery.Model.Movies;
using Newtonsoft.Json;

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
                year = (year == 0) ? 2021 : year; //if  year entered is null or invalid format, by default take year as 2021
                Movies response = new Movies();
                using (WebClient webClient = new WebClient())
                {
                    string moviesJson = webClient.DownloadString("https://imdb-api.com/en/API/Top250Movies/k_81ggrpaf");
                    response = Movies.FromJson(moviesJson);
                }

                if(response.Items.Count==0) //since api has limit of 100 reads/day
                {
                    using (StreamReader r = new StreamReader("Movies.json")) //read local copy
                    {
                        string json = r.ReadToEnd();
                        response = JsonConvert.DeserializeObject<Movies>(json);
                    }
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
