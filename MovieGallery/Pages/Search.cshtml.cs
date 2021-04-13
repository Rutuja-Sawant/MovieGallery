using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MovieGallery.Model.Movies;
using MovieGallery.Model.Shows;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

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