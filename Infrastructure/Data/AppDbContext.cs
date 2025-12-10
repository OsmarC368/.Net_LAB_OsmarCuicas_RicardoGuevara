using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Configurations;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserType> userTypes{ get; set; }
        public DbSet<Measure> measures{ get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserTypeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeasureConfiguration).Assembly);
        }

    }
}