using System.Diagnostics;
using System.Text.Json;
using TRPO_MVC.Models;

namespace TRPO_MVC.Services
{
    public class ElementService
    {
        private HttpClient httpClient { get; }

        public ElementService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<List<ElementModel>> GetAllElements()
        {
            using HttpResponseMessage response = await httpClient.GetAsync("api/elements");

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            var elements = JsonSerializer.Deserialize<List<ElementModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            foreach (var e in elements)
            {
                e.ParsedData = JsonSerializer.Deserialize<Dictionary<string, string>>(e.Data);
            }

            return elements;
        }
    }
}
