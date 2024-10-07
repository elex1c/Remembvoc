using Microsoft.EntityFrameworkCore;
using Remembvoc.Models.ApplicationModels;

namespace Remembvoc;

public class DatabaseContext : DbContext
{
    public DbSet<Words> Words { get; set; }
    public DbSet<Languages> Languages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Words>()
            .HasIndex(e => e.Phrase)
            .IsUnique();
        
        modelBuilder.Entity<Languages>()
            .HasIndex(e => e.ShortForm)
            .IsUnique();
    }
}