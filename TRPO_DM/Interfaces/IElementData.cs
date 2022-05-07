namespace TRPO_DM.Interfaces
{
    public interface IElementData
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public string ImageURI { get; set; }

        public int CategoryID { get; set; }
    }
}
