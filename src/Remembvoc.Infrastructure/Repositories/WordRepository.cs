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
        return await _context.Words.ToListAsync();
    }

    public async Task AddWordAsync(WordEntity wordEntity)
    {
        await _context.Words.AddAsync(wordEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWordAsync(WordEntity wordEntity)
    {
        _context.Words.Remove(wordEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<WordEntity>> GetAllWithPrioritiesAsync()
    {
        return await _context.Words.Include(p => p.Priority)
            .ToListAsync();
    }
    
    public async Task<WordEntity?> GetWordByNameAsync(string word)
    {
        return await _context.Words.FirstOrDefaultAsync(w => w.Phrase == word); 
    }
}