using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using MauiApp4.Models;

namespace MauiApp4.Services
{
    public class MovieService
    {
        private readonly HttpClient _client = new HttpClient();
        private const string apiKey = "966c4f4f";

        public async Task<Movie> GetMovieAsync(string title)
        {
            var response = await _client.GetStringAsync($"http://www.omdbapi.com/?t={title}&apikey={apiKey}");
            return JsonSerializer.Deserialize<Movie>(response);
        }
    }
}
