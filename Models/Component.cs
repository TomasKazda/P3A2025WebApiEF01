using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P3A2025WebApiEF01.Models
{
    [PrimaryKey(nameof(RecipeId), nameof(IngredientId))]
    [Index(nameof(CLnk), IsUnique = true)]
    public class Component
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CLnk { get; set; }

        public int RecipeId { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;

        public int IngredientId { get; set; }
        [ForeignKey(nameof(IngredientId))]
        public Ingredients Ingredient { get; set; } = null!;

        public decimal Quantity { get; set; }
        public string Unit { get; set; } = null!;
    }
}
