using System.Diagnostics;
using System.Text.Json;
using TRPO_DM.Interfaces;
using TRPO_MVC.Models;

namespace TRPO_MVC.Services
{
    public class CategoryService
    {
        private HttpClient httpClient { get; }

        public CategoryService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<CategoryModel> CreateCategory(ICategoryData category)
        {
            string categoryJson = JsonSerializer.Serialize(category);
            HttpContent categoryContent = new StringContent(categoryJson, System.Text.Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await httpClient.PutAsync("api/categories", categoryContent);

            response.EnsureSuccessStatusCode();

            string receivedContent = await response.Content.ReadAsStringAsync();

            var c = JsonSerializer.Deserialize<CategoryModel>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return c;
        }

        public async Task<CategoryModel> ReadCategory(int categoryID)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("api/categories/" + categoryID.ToString());

            response.EnsureSuccessStatusCode();

            string receivedContent = await response.Content.ReadAsStringAsync();

            var c = JsonSerializer.Deserialize<CategoryModel>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return c;
        }

        public async Task<CategoryModel> UpdateCategory(ICategoryData category)
        {
            string categoryJson = JsonSerializer.Serialize(category);
            Debug.WriteLine(categoryJson);
            HttpContent categoryContent = new StringContent(categoryJson, System.Text.Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await httpClient.PatchAsync("api/categories", categoryContent);

            response.EnsureSuccessStatusCode();

            string receivedContent = await response.Content.ReadAsStringAsync();

            var c = JsonSerializer.Deserialize<CategoryModel>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return c;
        }

        public async Task<CategoryModel> DeleteCategory(int categoryID)
        {
            Debug.WriteLine(categoryID);
            using HttpResponseMessage response = await httpClient.DeleteAsync("api/categories/" + categoryID.ToString());

            response.EnsureSuccessStatusCode();

            string receivedContent = await response.Content.ReadAsStringAsync();

            var c = JsonSerializer.Deserialize<CategoryModel>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return c;
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            using HttpResponseMessage response = await httpClient.GetAsync("api/categories");

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            var categories = JsonSerializer.Deserialize<List<CategoryModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return categories;
        }
    }
}
