using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRPO_DM.Models
{
    public class Element
    {
        [Key]
        public int ID { get; set; }

        public string Data { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}
