using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
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
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(255);
                
            builder
                .Property(x => x.DifficultyLevel)
                .IsRequired();

            builder
                .Property(x => x.Visibility)
                .IsRequired();


            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder
                .HasMany(x => x.Ingredients)
                .WithOne()
                .HasForeignKey(x => x.RecipeId);

            builder
                .HasMany(x => x.Steps)
                .WithOne()
                .HasForeignKey(x => x.RecipeId);


            builder.ToTable("Recipes");
        }
    }
}