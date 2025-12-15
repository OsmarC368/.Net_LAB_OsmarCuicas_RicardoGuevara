using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
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
            .Property(x => x.Lastname)
            .IsRequired()
            .HasMaxLength(255);

        builder
			.Property(x => x.Email)
			.IsRequired()
			.HasMaxLength(255); 

        builder.HasIndex(x => x.Email).IsUnique();       

		builder
			.Property(x => x.Password)
			.IsRequired()
			.HasMaxLength(255);


        builder.HasOne(x => x.UserType)
            .WithMany()
            .HasForeignKey(x => x.UserTypeID)
            .IsRequired();

        builder.HasMany(x => x.Recipes)
            .WithOne()
            .HasForeignKey(x => x.UserIdR);

        builder.ToTable("Users");
    }
}