using System.ComponentModel.DataAnnotations;

namespace PackageManager.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DataType? CreationDate { get; set; }  
        public string? City { get; set; }
        public bool IsSealed { get; set; }

        [DataType(DataType.Date)]
        public DataType? SealDate { get; set; }
    }
}
