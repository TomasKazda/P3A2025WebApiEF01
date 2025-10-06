using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P3A2025WebApiEF01.Models
{
    public class Ingredients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int? CatId { get; set; }
        [ForeignKey(nameof(Ingredients.CatId))]
        public Category? Category { get; set; }
    }
}
