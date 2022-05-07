using TRPO_DM.Interfaces;

namespace TRPO_API.DataModels
{
    public class CategoryDM : ICategoryData
    {
        public int ID { get; set; }

        public int? ParentID { get; set; }

        public string Name { get; set; }
    }
}
