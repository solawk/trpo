using TRPO_DM.Interfaces;

namespace TRPO_DM.ViewModels
{
    public class CategoryVM : ICategoryData
    {
        public int ID { get; set; }

        public int? ParentID { get; set; }

        public string Name { get; set; }
    }
}
