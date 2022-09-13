using System.ComponentModel.DataAnnotations;

namespace PackageManager.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? City { get; set; }
        public bool IsSealed { get; set; }
        public DateTime? SealDate { get; set; }
        public virtual ICollection<Item>? Items { get; set; } = null;
    }
}
