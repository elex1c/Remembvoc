using Microsoft.EntityFrameworkCore;
using Remembvoc.Infrastructure.Data.ModelsDTO;

namespace Remembvoc.Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DbSet<WordDTO> Words { get; set; }
    public DbSet<LanguageDTO> Languages { get; set; }
    public DbSet<PriorityDTO> Priorities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WordDTO>()
            .HasIndex(w => w.Phrase)
            .IsUnique();

        modelBuilder.Entity<WordDTO>()
            .HasOne(w => w.Language)
            .WithMany(l => l.Words)
            .HasForeignKey(w => w.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PriorityDTO>()
            .HasOne(p => p.Word)
            .WithOne(w => w.Priority)
            .HasForeignKey<PriorityDTO>(p => p.WordId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}