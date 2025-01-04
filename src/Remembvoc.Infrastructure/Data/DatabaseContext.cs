using Microsoft.EntityFrameworkCore;
using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DbSet<WordEntity> Words { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<PriorityEntity> Priorities { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WordEntity>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.Phrase).IsRequired();
            entity.Property(w => w.Translation).IsRequired();

            entity.HasOne(w => w.Language)
                .WithMany(l => l.Words)
                .HasForeignKey(w => w.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(w => w.Priority)
                .WithOne(p => p.Word)
                .HasForeignKey<PriorityEntity>(p => p.WordId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<PriorityEntity>(entity =>
        {
            entity.HasKey(p => p.WordId);
            entity.Property(p => p.Points).IsRequired();
            entity.Property(p => p.LastCheck).IsRequired();
            entity.Property(p => p.MinutesToRepeat).IsRequired();
            entity.Property(p => p.Period).IsRequired();
        });
        
        modelBuilder.Entity<LanguageEntity>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Name).IsRequired();

            entity.HasMany(l => l.Words)
                .WithOne(w => w.Language)
                .HasForeignKey(w => w.LanguageId);
        });

        base.OnModelCreating(modelBuilder);
    }
}