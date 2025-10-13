using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P3A2025WebApiEF01.Models
{
    //[PrimaryKey(nameof(RecipeId), nameof(IngredientId))]
    [Index(nameof(RecipeId), nameof(IngredientId), IsUnique = true)]
    public class Component
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CLnk { get; set; }
        
        public int RecipeId { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;

        public int IngredientId { get; set; }

        [ForeignKey(nameof(IngredientId))]
        public required Ingredients Ingredient { get; set; }

        public decimal? Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
