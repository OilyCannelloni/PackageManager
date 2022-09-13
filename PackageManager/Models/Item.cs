using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PackageManager.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Package")]
        public int PackageID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(50)")]
        public string Address { get; set; }

        public float Mass { get; set; }
    }
}
