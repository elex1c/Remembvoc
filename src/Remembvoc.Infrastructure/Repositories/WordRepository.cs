using Microsoft.EntityFrameworkCore;
using Remembvoc.ApplicationCore.Common.Interfaces;
using Remembvoc.Infrastructure.Data;
using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.Infrastructure.Repositories;

public class WordRepository : IWordRepository
{
    private readonly DatabaseContext _context;

    public WordRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<WordEntity>> GetAllAsync()
    {
        return await _context.Words.
            AsNoTracking()
            .ToListAsync();
    }

    public async Task<WordEntity> AddWordAsync(WordEntity wordEntity)
    {
        var entity = await _context.Words.AddAsync(wordEntity);
        await _context.SaveChangesAsync();
        
        _context.Entry(entity.Entity).State = EntityState.Detached;
        
        return entity.Entity;
    }

    public async Task DeleteWordAsync(WordEntity wordEntity)
    {
        _context.Words.Remove(wordEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<WordEntity>> GetAllWithPrioritiesAsync()
    {
        return await _context.Words.AsNoTracking()
            .Include(w => w.Priority)
            .ToListAsync();
    }
    
    public async Task<WordEntity?> GetWordByNameAsync(string word)
    {
        return await _context.Words.AsNoTracking()
            .FirstOrDefaultAsync(w => w.Phrase == word); 
    }
}