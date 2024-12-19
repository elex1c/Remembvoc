using Microsoft.EntityFrameworkCore;
using Remembvoc.Core.Common.Models;
using Remembvoc.Infrastructure;

namespace Remembvoc.Helper;

public static class DbMethods
{
    public static void UpdateTimeInPriorities()
    {
        using var context = new DatabaseContext();

        var priorities = context.Priorities.ToList();

        foreach (var priority in priorities)
            RepetitionAlgorithm.Counting.CountCheckTime(priority);

        context.SaveChanges();
    }

    public static List<WordEntity> GetWordsForRevising(int elementsPerPage, int pageNumber)
    {
        using var context = new DatabaseContext();
        
        return context.Priorities.Include(p => p.WordEntity)
            .Where(p => p.MinutesToRepeat <= 0)
            .Select(p => p.WordEntity)
            .OrderBy(x => x.Id)
            .Skip(pageNumber * elementsPerPage - elementsPerPage)
            .Take(elementsPerPage)
            .ToList();
    }

    public static WordEntity? GetWordElement(string word)
    {
        using var context = new DatabaseContext();

        return context.Words.Include(w => w.LanguageEntity)
            .Include(w => w.PriorityEntity)
            .FirstOrDefault(x => x.Phrase == word.ToLower());
    }
}