using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MovieGallery.Model.Movies;
using MovieGallery.Model.Shows;
using System;
using System.Linq;
using System.Net;

namespace MovieGallery.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ILogger<SearchModel> _logger;

        public SearchModel(ILogger<SearchModel> logger)
        {
            _logger = logger;
        }

        public bool InitialCheckList { get; set; }

        public bool SearchCompleted { get; set; }

        public Movies MoviesResult { get; set; }

        public Shows ShowsResult { get; set; }

        public void OnGet()
        {
            try
            {
                ReadDataFiles();
                InitialCheckList = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured on OnGet Search {ex}");
            }
        }

        public void OnPost(long? SearchbyYear)
        {
            try
            {
                ReadDataFiles();

                if (SearchbyYear == null)
                    InitialCheckList = true;
                else
                {
                    var result = MoviesResult.Items.Where(x => x.Year == SearchbyYear).ToList();
                    MoviesResult = new Movies()
                    {
                        Items = result
                    };

                    var resultShow = ShowsResult.Items.Where(x => x.Year == SearchbyYear).ToList();
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
                _logger.LogError($"Exception occured on OnPost Search for year {SearchbyYear}: {ex}");
            }
        }

        private void ReadDataFiles()
        {
            using (WebClient webClient=new WebClient())
            {
                string moviesJson = webClient.DownloadString("https://imdb-api.com/en/API/Top250Movies/k_81ggrpaf");
                MoviesResult = Movies.FromJson(moviesJson);
            }
            using (WebClient webClient = new WebClient())
            {
                string showsJson = webClient.DownloadString("https://imdb-api.com/en/API/Top250TVs/k_81ggrpaf");
                ShowsResult = Shows.FromJson(showsJson);
            }
        }
    }
}