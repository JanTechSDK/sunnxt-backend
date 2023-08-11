using Microsoft.EntityFrameworkCore;
using SunNxtBackend.Models;

namespace SunNxtBackend
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AgeRange> AgeRanges { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeRange>().HasKey(s => s.Id);
            modelBuilder.Entity<AgeRange>().Property(s => s.AgeRangeName).HasMaxLength(128).IsRequired();

            modelBuilder.Entity<Country>().HasKey(s => s.Id);
            modelBuilder.Entity<Country>().Property(s => s.CountryName).HasMaxLength(128).IsRequired();

            modelBuilder.Entity<State>().HasKey(s => s.Id);
            modelBuilder.Entity<State>().Property(s => s.StateName).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<State>().HasOne(c => c.Country)
                                        .WithMany(s => s.States)
                                        .HasForeignKey(c => c.CountryId);

            modelBuilder.Entity<City>().HasKey(s => s.Id);
            modelBuilder.Entity<City>().Property(s => s.CityName).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<City>().HasOne(s => s.State)
                                        .WithMany(c=>c.Cities)
                                        .HasForeignKey(c => c.StateId);

            modelBuilder.Entity<AppUser>().HasKey(s => s.Id);
            modelBuilder.Entity<AppUser>().Property(s => s.MobileNumber).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<AppUser>().Property(s => s.FullName).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<AppUser>().Property(s => s.Gender).HasMaxLength(50);
            
            modelBuilder.Entity<AppUser>().HasOne(a => a.AgeRange)
                                            .WithMany(ar => ar.AppUsers)
                                            .HasForeignKey(s => s.AgeRangeId);
            modelBuilder.Entity<AppUser>().HasOne(a => a.Country)
                                           .WithMany(ar => ar.AppUsers)
                                           .HasForeignKey(s => s.CountryId);
            modelBuilder.Entity<AppUser>().HasOne(a => a.State)
                                           .WithMany(ar => ar.AppUsers)
                                           .HasForeignKey(s => s.StateId);
            modelBuilder.Entity<AppUser>().HasOne(a => a.City)
                                           .WithMany(ar => ar.AppUsers)
                                           .HasForeignKey(s => s.CityId);

        }

    }
}
