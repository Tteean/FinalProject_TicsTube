using FinalProject_Service.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Services.Implementations
{
    public class IMDBService: IIMDBService
    {
        private readonly string apiKey = "7f09d2e0";
        private readonly HttpClient _httpClient;

        public IMDBService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetIMDbRatingAsync(string movieTitle)
        {
            string url = $"https://www.omdbapi.com/?t={movieTitle}&apikey={apiKey}";
            string response = await _httpClient.GetStringAsync(url);

            JObject json = JObject.Parse(response);
            return json["imdbRating"]?.ToString() ?? "no rating";
        }
    }
}

