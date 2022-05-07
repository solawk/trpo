namespace TRPO_MVC.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }

        public int? ParentID { get; set; }

        public string Name { get; set; }
    }
}
