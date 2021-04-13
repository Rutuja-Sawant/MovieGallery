using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace MovieGallery.Pages
{
    public class LaureatesModel : PageModel
    {
        private readonly ILogger<LaureatesModel> _logger;

        public LaureatesModel(ILogger<LaureatesModel> logger) => _logger = logger;

        public void OnGet()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string response = webClient.DownloadString("https://themodernilluminati.azurewebsites.net/ExposeApi?country=IN");
                    var laureates = Laureates.FromJson(response);
                    List<MovieGallery.Laureate> laureate = laureates.NobleLaureates;
                    ViewData["Laureates"] = laureate;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured on calling Nobel Laureates get endpoint {ex}");
            }
        }
    }
}