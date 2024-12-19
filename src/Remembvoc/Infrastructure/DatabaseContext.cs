using Microsoft.EntityFrameworkCore;
using Remembvoc.Core.Common.Interfaces;
using Remembvoc.Core.Common.Models;

namespace Remembvoc.Infrastructure;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DbSet<WordEntity> Words { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<PriorityEntity> Priorities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WordEntity>()
            .HasIndex(e => e.Phrase)
            .IsUnique();
        
        modelBuilder.Entity<LanguageEntity>()
            .HasIndex(e => e.ShortForm)
            .IsUnique();

        modelBuilder.Entity<PriorityEntity>()
            .HasOne(b => b.WordEntity)
            .WithOne(a => a.PriorityEntity)
            .HasForeignKey<PriorityEntity>(b => b.Id);
    }
}