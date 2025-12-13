using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
			.UseIdentityColumn(); 

		builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(255);

        builder.ToTable("Ingredients");
    }
}