using TRPO_DM.Interfaces;

namespace TRPO_MVC.Models.Out
{
    public class CategoryOutModel : ICategoryData
    {
        public int ID { get; set; }

        public int? ParentID { get; set; }

        public string Name { get; set; }
    }
}
