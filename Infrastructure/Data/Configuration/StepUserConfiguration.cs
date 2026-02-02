using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class StepUserConfiguration : IEntityTypeConfiguration<StepUser>
    {
        public void Configure(EntityTypeBuilder<StepUser> builder)
        {
            builder
                .HasKey(x => x.id);

            builder
                .Property(x => x.id)
                .UseIdentityColumn(); 

            builder
                .Property(x => x.completed)
                .IsRequired();

            builder
                .Property(x => x.comment)
                .HasMaxLength(255);

            
            builder.HasOne(x => x.StepSUR)
                .WithMany()
                .HasForeignKey(x => x.stepSURID)
                .IsRequired();

            builder.HasOne(x => x.UserSUR)
                .WithMany()
                .HasForeignKey(x => x.userSURID)
                .IsRequired();

            builder.ToTable("StepUser");
        }
    }
}