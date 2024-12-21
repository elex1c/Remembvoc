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

    public async Task<List<WordEntity>> GetAllWithPrioritiesAsync()
    {
        return await _context.Words.Include(p => p.Priority)
            .ToListAsync();
    }
    
    public async Task<WordEntity?> GetWordByNameAsync(string word)
    {
        return await _context.Words.FirstOrDefaultAsync(w => w.Phrase == word); 
    }

    public async Task<List<WordEntity>> GetWordsForRevisingAsync(int elementsPerPage, int pageNumber)
    {
        return await _context.Priorities.Include(p => p.Word)
            .Where(p => p.MinutesToRepeat <= 0)
            .Select(p => p.Word)
            .OrderBy(x => x.Id)
            .Skip(pageNumber * elementsPerPage - elementsPerPage)
            .Take(elementsPerPage)
            .ToListAsync();
    }
}