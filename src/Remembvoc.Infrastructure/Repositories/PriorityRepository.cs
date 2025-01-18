using Microsoft.EntityFrameworkCore;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.ApplicationCore.Common.Models.Entities;
using Remembvoc.Infrastructure.Data;

namespace Remembvoc.Infrastructure.Repositories;

public class PriorityRepository : IPriorityRepository
{
    private readonly DatabaseContext _context;

    public PriorityRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PriorityEntity priority)
    {
        await _context.AddAsync(priority);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<PriorityEntity>> GetAllAsync()
    {
        return await _context.Priorities
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PriorityEntity?> GetPriorityByIdAsync(int id)
    {
        return await _context.Priorities.AsNoTracking()
            .FirstOrDefaultAsync(p => p.WordId == id);
    }

    public async Task UpdatePrioritiesAsync(IEnumerable<PriorityEntity> priorities)
    {
        _context.Priorities.UpdateRange(priorities);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateSinglePriorityAsync(PriorityEntity priority)
    {
        var existingEntity = _context.Priorities.Local
            .FirstOrDefault(p => p.WordId == priority.WordId);
    
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).State = EntityState.Detached;
        }

        _context.Priorities.Update(priority);
        await _context.SaveChangesAsync();
    }
}