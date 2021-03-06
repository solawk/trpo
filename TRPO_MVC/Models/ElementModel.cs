namespace TRPO_MVC.Models
{
    public class ElementModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Data { get; set; }

        public string ImageURI { get; set; }

        public CategoryModel Category { get; set; }

        public Dictionary<string, object> ParsedData { get; set; }
    }
}
