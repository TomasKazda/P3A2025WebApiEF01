using Microsoft.EntityFrameworkCore;
using P3A2025WebApiEF01.Models;

namespace P3A2025WebApiEF01.Data
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Ingredients> Ingredients { get; set; } = null!;
        public DbSet<Component> Components { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData( new Category { CatId = 1, Name = "Vegetables" },
                                                    new Category { CatId = 2, Name = "Fruits" },
                                                    new Category { CatId = 3, Name = "Alcohol" },
                                                    new Category { CatId = 4, Name = "Dairy" },
                                                    new Category { CatId = 5, Name = "Syrup"} );
        }
    }
}



