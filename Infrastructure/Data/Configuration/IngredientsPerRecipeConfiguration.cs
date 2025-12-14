using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class IngredientsPerRecipeConfiguration : IEntityTypeConfiguration<IngredientsPerRecipe>
    {
        public void Configure(EntityTypeBuilder<IngredientsPerRecipe> builder)
        {
            builder
                .HasKey(x => x.id);

            builder 
                .Property(x => x.id)
                .UseIdentityColumn();

            builder
                .HasOne(x => x.Recipe)
                .WithMany()
                .HasForeignKey(x => x.RecipeId)
                .IsRequired();

            builder
                .HasOne(x => x.Ingredient)
                .WithMany()
                .HasForeignKey(x => x.IngredientId)
                .IsRequired();

            builder
                .Property(x => x.amount)
                .IsRequired();

            builder.ToTable("IngredientsPerRecipe");
        }
    }

}