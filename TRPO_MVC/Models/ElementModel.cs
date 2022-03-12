namespace TRPO_MVC.Models
{
    public class ElementModel
    {
        public int ID { get; set; }

        public string Data { get; set; }

        public Dictionary<string, string> ParsedData { get; set; }
    }
}
