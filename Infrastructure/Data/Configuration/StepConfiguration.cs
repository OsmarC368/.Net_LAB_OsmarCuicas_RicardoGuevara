using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class StepConfiguration : IEntityTypeConfiguration<Step>, IStepConfiguration
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.imageURL)
                .IsRequired(false);

            builder.Property(x => x.Duration)
                .IsRequired();

            builder.HasOne(x => x.RecipeS)
                .WithMany()
                .HasForeignKey(x => x.RecipeIdS)
                .IsRequired();

            builder.ToTable("Steps");
        }
    }

    internal interface IStepConfiguration
    {
    }
}