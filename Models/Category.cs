using System.ComponentModel.DataAnnotations;

namespace P3A2025WebApiEF01.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }

}
