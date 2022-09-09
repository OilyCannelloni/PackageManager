using System.ComponentModel.DataAnnotations;

namespace PackageManager.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public DateTime CreationDate { get; set; }  
        public string? City { get; set; }
        public bool IsSealed { get; set; }
        public DateTime? SealDate { get; set; }
    }
}
