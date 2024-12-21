using Microsoft.EntityFrameworkCore;
using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.Infrastructure.Data;

public class DatabaseContext : DbContext
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
            .HasIndex(w => w.Phrase)
            .IsUnique();

        modelBuilder.Entity<WordEntity>()
            .HasOne(w => w.Language)
            .WithMany(l => l.Words)
            .HasForeignKey(w => w.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PriorityEntity>()
            .HasOne(p => p.Word)
            .WithOne(w => w.Priority)
            .HasForeignKey<PriorityEntity>(p => p.WordId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}