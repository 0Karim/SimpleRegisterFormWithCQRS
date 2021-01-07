using CleanArch.Application.Common.Interfaces;
using CleanArch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Persistence
{
    public class AppDbContext : DbContext , IDbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Governate> Governate { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<AddressInfo> AddressInfo { get; set; }

        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<City>().HasData
                (
                new City() { Id = 1, CityName = "Cairo"},
                new City() { Id = 2, CityName = "Alex" },
                new City() { Id = 3, CityName = "Assuit" }
                );

            modelBuilder.Entity<Governate>().HasData
                (
                new Governate() { Id = 1,  GovName= "Egypt" },
                new Governate() { Id = 2, GovName = "Saudi" },
                new Governate() { Id = 3, GovName = "France" }
                );



            base.OnModelCreating(modelBuilder);
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "Server=.;Database=CQRSDB;Trusted_Connection=True;MultipleActiveResultSets=true";
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
