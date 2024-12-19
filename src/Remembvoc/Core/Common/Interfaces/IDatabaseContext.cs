using Microsoft.EntityFrameworkCore;
using Remembvoc.Core.Common.Models;

namespace Remembvoc.Core.Common.Interfaces;

public interface IDatabaseContext
{
    public DbSet<WordEntity> Words { get; }
    public DbSet<LanguageEntity> Languages { get; }
    public DbSet<PriorityEntity> Priorities { get; }
}