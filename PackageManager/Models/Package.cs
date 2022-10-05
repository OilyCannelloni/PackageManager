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

        [Display(Name = "Seal Status")]
        public bool IsSealed { get; set; }

        [Display(Name = "Seal Date")]
        public DateTime? SealDate { get; set; }
        public virtual List<Item> Items { get; set; } = new List<Item>();
    }
}
