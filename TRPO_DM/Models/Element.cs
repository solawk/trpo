using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TRPO_DM.Interfaces;

namespace TRPO_DM.Models
{
    public class Element : IElementData
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public string ImageURI { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public Category Category { get; set; }
    }
}
