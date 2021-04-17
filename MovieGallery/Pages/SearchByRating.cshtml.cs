using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MovieGallery.Model.Movies;
using MovieGallery.Model.Shows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace MovieGallery.Pages
{
    public class SearchByRatingModel : PageModel
    {
        private readonly ILogger<SearchByRatingModel> _logger;

        public SearchByRatingModel(ILogger<SearchByRatingModel> logger)
        {
            _logger = logger;
        }
        public bool InitialCheckList { get; set; }

        public bool SearchCompleted { get; set; }

        public Movies MoviesResult { get; set; }

        public Shows ShowsResult { get; set; }

       public List<string> RatingsList { get; set; }

        public void OnGet()
        {
            try
            {
                ReadDataFiles();
                InitialCheckList = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured on OnGet Search By Rating {ex}");
            }
        }

        public void OnPost()
        {
            try
            {
                var Rating = Request.Form["rating"];
                ReadDataFiles();

                if (string.IsNullOrEmpty(Rating))
                    InitialCheckList = true;
                else
                {
                    var result = MoviesResult.Items.Where(x => x.ImDbRating == Rating).ToList();
                    MoviesResult = new Movies()
                    {
                        Items = result
                    };

                    var resultShow = ShowsResult.Items.Where(x => x.ImDbRating == Rating).ToList();
                    ShowsResult = new Shows()
                    {
                        Items = resultShow
                    };

                    if (MoviesResult.Items.Count > 0 || ShowsResult.Items.Count > 0)
                    {
                        SearchCompleted = true;
                    }
                    else
                    {
                        SearchCompleted = false;
                        MoviesResult = new Movies();
                        ShowsResult = new Shows();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured on OnPost Search Rating: {ex}");
            }
        }

        private void ReadDataFiles()
        {
            using (WebClient webClient = new WebClient())
            {
                string moviesJson = webClient.DownloadString("https://imdb-api.com/en/API/Top250Movies/k_81ggrpaf");
                MoviesResult = Movies.FromJson(moviesJson);
            }
            using (WebClient webClient = new WebClient())
            {
                string showsJson = webClient.DownloadString("https://imdb-api.com/en/API/Top250TVs/k_81ggrpaf");
                ShowsResult = Shows.FromJson(showsJson);
            }
            if (MoviesResult.Items.Count == 0 && ShowsResult.Items.Count == 0)
            {
                ReadLocalFiles(); //since apis have limit of 100 reads per day
            }
            RatingsList = new List<string>();
            foreach (var i in MoviesResult.Items)
            {
                RatingsList.Add(i.ImDbRating);
            }
            RatingsList= RatingsList.Distinct().ToList();
            RatingsList = RatingsList.OrderBy(x => x).ToList();
        }
        private void ReadLocalFiles()
        {
            using (StreamReader r = new StreamReader("Movies.json"))
            {
                string json = r.ReadToEnd();
                MoviesResult = JsonConvert.DeserializeObject<Movies>(json);
            }

            using (StreamReader r = new StreamReader("Shows.json"))
            {
                string json = r.ReadToEnd();
                ShowsResult = JsonConvert.DeserializeObject<Shows>(json);
            }
        }
    }
}
