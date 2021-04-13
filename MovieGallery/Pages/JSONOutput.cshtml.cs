using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MovieGallery.Pages
{
    public class JSONOutputModel : PageModel
    {
        private readonly ILogger<JSONOutputModel> _logger;

        public JSONOutputModel(ILogger<JSONOutputModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        //public Movies GetData(long year)
        //{
        //    try
        //    {
        //        Movies MoviesResult = new Movies();

        //        using (StreamReader r = new StreamReader("Movies.json"))
        //        {
        //            string json = r.ReadToEnd();
        //            MoviesResult = JsonConvert.DeserializeObject<Movies>(json);
        //            var result = MoviesResult.Items.Where(x => x.Year == year).ToList();
        //            MoviesResult = new Movies()
        //            {
        //                Items = result
        //            };
        //        }

        //        return MoviesResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured on OnGet Search {ex}");
        //        throw ex;
        //    }
        //}
    }
}