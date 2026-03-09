using HoroscopeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HoroscopeChallenge.Infrastructure.Persistence;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<HoroscopeQuery> HoroscopeQueries => Set<HoroscopeQuery>();
    public DbSet<HoroscopeCache> HoroscopeCaches => Set<HoroscopeCache>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        UserConfiguration(modelBuilder);
        HoroscopeQueryConfiguration(modelBuilder);
        HoroscopeCacheConfiguration(modelBuilder);
    }

    private static void UserConfiguration(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<User>();

        entity.HasKey(x => x.Id);

        entity.HasIndex(x => x.Username).IsUnique();
        entity.HasIndex(x => x.Email).IsUnique();

        entity.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);

        entity.Property(x => x.PasswordHash)
            .IsRequired();
    }

    private static void HoroscopeQueryConfiguration(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<HoroscopeQuery>();

        entity.HasKey(x => x.Id);

        entity.HasOne(x => x.User)
            .WithMany(x => x.Queries)
            .HasForeignKey(x => x.UserId);

        entity.Property(x => x.Sign)
            .IsRequired()
            .HasMaxLength(20);

        entity.Property(x => x.Horoscope)
            .IsRequired();
    }

    private static void HoroscopeCacheConfiguration(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<HoroscopeCache>();

        entity.HasKey(x => x.Id);

        entity.HasIndex(x => new { x.Sign, x.Date })
            .IsUnique();

        entity.Property(x => x.Sign)
            .IsRequired()
            .HasMaxLength(20);

        entity.Property(x => x.Horoscope)
            .IsRequired();
    }
}