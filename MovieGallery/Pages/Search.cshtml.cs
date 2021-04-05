using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieGallery.Model;
using MovieGallery.Movie.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace MovieGallery.Pages
{
    public class SearchModel : PageModel
    {
        public bool InitialCheckList { get; set; }
        public string Query { get; set; }
        public bool SearchCompleted { get; set; }
        public bool CorrectYear { get; set; }

        public Movies MoviesResult = new Movies();
        public Movies MoviesJson { get; set; }

        public Shows ShowsResult = new Shows();
        public Shows ShowsJson { get; set; }

        public void OnGet(string SearchbyYear)
        {
            string currentYear = DateTime.Now.Year.ToString();
            using (StreamReader r = new StreamReader("Movies.json"))
            {
                string json = r.ReadToEnd();
                MoviesJson = JsonConvert.DeserializeObject<Movies>(json);
            }

            using (StreamReader r = new StreamReader("Shows.json"))
            {
                string json = r.ReadToEnd();
                ShowsJson = JsonConvert.DeserializeObject<Shows>(json);
            }

            if (SearchbyYear == null)
            {
                InitialCheckList = true;
                MoviesResult = MoviesJson;
                ShowsResult = ShowsJson;
            }
            else
            {
                Query = SearchbyYear;
                long inputYear = Convert.ToInt64(Query);
                if (inputYear > 100 && inputYear <= Convert.ToInt64(currentYear))
                {
                    var result = MoviesJson.Items.Where(x => x.Year == inputYear).ToList();
                    MoviesResult = new Movies()
                    {
                        Items = result
                    };

                    var resultShow = ShowsJson.Items.Where(x => x.Year == inputYear).ToList();
                    ShowsResult = new Shows()
                    {
                        Items = resultShow
                    };

                    if (MoviesResult.Items.Count > 0 || ShowsResult.Items.Count > 0)
                    {
                        SearchCompleted = true;
                        CorrectYear = true;
                    }
                    else
                    {
                        SearchCompleted = false;
                        CorrectYear = true;
                        MoviesResult = new Movies();
                        ShowsResult = new Shows();
                    }
                }
                else
                {
                    CorrectYear = false;
                }
            }
        }
    }
}