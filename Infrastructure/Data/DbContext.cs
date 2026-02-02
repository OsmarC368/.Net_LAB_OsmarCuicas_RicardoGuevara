using Core.Entities;
using Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Measure> Measure { get; set; }
    public DbSet<UserType> UserType { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Step> Steps { get; set; }
    public DbSet<IngredientsPerRecipe> IngredientsPerRecipes { get; set; }
    public DbSet<StepUser> StepUsers { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(IngredientConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(MeasureConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(UserTypeConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(RecipeConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(StepConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(IngredientsPerRecipeConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(StepUserConfiguration).Assembly);
	}

}