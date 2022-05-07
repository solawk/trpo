using TRPO_DM.Interfaces;

namespace TRPO_API.DataModels
{
    public class ElementDM : IElementData
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public string ImageURI { get; set; }

        public int CategoryID { get; set; }
    }
}
