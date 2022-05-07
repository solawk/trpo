using System.Diagnostics;
using System.Text.Json;
using TRPO_DM.Interfaces;
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

        public async Task<ElementModel> CreateElement(IElementData element)
        {
            string elementJson = JsonSerializer.Serialize(element);
            HttpContent elementContent = new StringContent(elementJson, System.Text.Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await httpClient.PutAsync("api/elements", elementContent);

            response.EnsureSuccessStatusCode();

            string receivedContent = await response.Content.ReadAsStringAsync();

            var e = JsonSerializer.Deserialize<ElementModel>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            parseData(e);

            return e;
        }

        public async Task<ElementModel> ReadElement(int elementID)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("api/elements/" + elementID.ToString());

            response.EnsureSuccessStatusCode();

            string receivedContent = await response.Content.ReadAsStringAsync();

            var e = JsonSerializer.Deserialize<ElementModel>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            parseData(e);

            return e;
        }

        public async Task<ElementModel> UpdateElement(IElementData element)
        {
            string elementJson = JsonSerializer.Serialize(element);
            HttpContent elementContent = new StringContent(elementJson, System.Text.Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await httpClient.PatchAsync("api/elements", elementContent);

            response.EnsureSuccessStatusCode();

            string receivedContent = await response.Content.ReadAsStringAsync();

            var e = JsonSerializer.Deserialize<ElementModel>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            parseData(e);

            return e;
        }

        public async Task<ElementModel> DeleteElement(int elementID)
        {
            Debug.WriteLine(elementID);
            using HttpResponseMessage response = await httpClient.DeleteAsync("api/elements/" + elementID.ToString());

            response.EnsureSuccessStatusCode();

            string receivedContent = await response.Content.ReadAsStringAsync();

            var e = JsonSerializer.Deserialize<ElementModel>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            parseData(e);

            return e;
        }

        public async Task<List<ElementModel>> GetAllElements()
        {
            using HttpResponseMessage response = await httpClient.GetAsync("api/elements");

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            var elements = JsonSerializer.Deserialize<List<ElementModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            foreach (var e in elements)
            {
                parseData(e);
            }

            return elements;
        }

        private void parseData(ElementModel e)
        {
            e.ParsedData = JsonSerializer.Deserialize<Dictionary<string, object>>(e.Data);
        }
    }
}
