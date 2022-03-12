using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRPO_DM.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("ParentCategory")]
        public int? ParentID { get; set; }

        public string Name { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual List<Element> Elements { get; set; }
    }
}
