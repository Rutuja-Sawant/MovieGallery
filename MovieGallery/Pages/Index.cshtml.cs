using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MovieGallery.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // using var webClient = new WebClient())
            //{
            //webClient.DownloadString("")
            //var welcome = Welcome.FromJson(jsonString);
            //}
        }
    }
}