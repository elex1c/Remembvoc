using Microsoft.EntityFrameworkCore;
using Remembvoc.Core.Common.Interfaces;
using Remembvoc.Core.Common.Models;
using Remembvoc.Infrastructure;
using Remembvoc.Models;

namespace Remembvoc.Core.Common.Services;

public class WordService : IWordService, IDisposable
{
    private readonly DatabaseContext _context = new();
    
    public Word? GetWord(string word)
    {
        var wordEntity = _context.Words.Include(w => w.LanguageEntity)
            .Include(w => w.PriorityEntity)
            .FirstOrDefault(x => x.Phrase == word.ToLower());

        if (wordEntity is null) return null;
        
        return (Word)wordEntity;
    }
    
    public List<Word> GetWordsForRevising(int elementsPerPage, int pageNumber)
    {
        return _context.Priorities.Include(p => p.WordEntity)
            .Where(p => p.MinutesToRepeat <= 0)
            .Select(p => p.WordEntity)
            .OrderBy(x => x.Id)
            .Skip(pageNumber * elementsPerPage - elementsPerPage)
            .Take(elementsPerPage)
            .Select(x => (Word)x)
            .ToList();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}