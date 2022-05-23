using System.Diagnostics;
using System.Text.Json;
using TRPO_DM.Interfaces;
using TRPO_DM.Models;
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
            parseElementModel(e);

            return e;
        }

        public async Task<ElementModel> ReadElement(int elementID)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("api/elements/" + elementID.ToString());

            response.EnsureSuccessStatusCode();

            string receivedContent = await response.Content.ReadAsStringAsync();

            var e = JsonSerializer.Deserialize<ElementModel>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            parseElementModel(e);

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
            parseElementModel(e);

            return e;
        }

        public async Task<ElementModel> DeleteElement(int elementID)
        {
            Debug.WriteLine(elementID);
            using HttpResponseMessage response = await httpClient.DeleteAsync("api/elements/" + elementID.ToString());

            response.EnsureSuccessStatusCode();

            string receivedContent = await response.Content.ReadAsStringAsync();

            var e = JsonSerializer.Deserialize<ElementModel>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            parseElementModel(e);

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
                parseElementModel(e);
            }

            return elements;
        }

        public async Task<List<ElementModel>> SearchElements(string? name, int? categoryID, string? data)
        {
            // Генерация списка фильтров
            List<Filter> filters = new List<Filter>();

            if (name != null)
            {
                filters.Add(new Filter(Filter.FilterType.Name, "", name, Filter.Predicate.Equals));
                Debug.WriteLine("Searching by name: " + name);
            }

            if (categoryID != null)
            {
                filters.Add(new Filter(Filter.FilterType.Category, "", categoryID, Filter.Predicate.Equals));
                Debug.WriteLine("Searching by category: " + categoryID);
            }

            if (data != null)
            {
                var dataDictionary = parseFilterData(data);

                foreach (var d in dataDictionary)
                {
                    Filter.Predicate predicate = Filter.Predicate.Equals;

                    if (d.Value.predicate < 0) predicate = Filter.Predicate.LesserThen;
                    else if (d.Value.predicate > 0) predicate = Filter.Predicate.GreaterThan;

                    filters.Add(new Filter(Filter.FilterType.Data, d.Key, d.Value.value, predicate));

                    Debug.WriteLine("Searching by data property: " + d.Key + " ... " + d.Value.value);
                }
            }

            // Отправка фильтров
            string filtersJson = Newtonsoft.Json.JsonConvert.SerializeObject(filters);
            HttpContent filtersContent = new StringContent(filtersJson, System.Text.Encoding.UTF8, "application/json");

            Debug.WriteLine("Content:");
            Debug.WriteLine(filtersJson);

            // Получение найденных элементов
            using HttpResponseMessage response = await httpClient.PostAsync("api/elements/search", filtersContent);
            response.EnsureSuccessStatusCode();
            string receivedContent = await response.Content.ReadAsStringAsync();
            var elements = JsonSerializer.Deserialize<List<ElementModel>>(receivedContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            foreach (var e in elements)
            {
                parseElementModel(e);
            }

            return elements;
        }

        private void parseElementModel(ElementModel e)
        {
            e.ParsedData = parseData(e.Data);
        }

        private Dictionary<string, object> parseData(string d)
        {
            return JsonSerializer.Deserialize<Dictionary<string, object>>(d);
        }

        private Dictionary<string, FilterDataModel> parseFilterData(string d)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, FilterDataModel>>(d);
        }
    }
}
