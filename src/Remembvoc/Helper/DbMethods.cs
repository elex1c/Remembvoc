using Microsoft.EntityFrameworkCore;
using Remembvoc.Models.ApplicationModels;

namespace Remembvoc.Helper;

public static class DbMethods
{
    public static void UpdateTimeInPriorities()
    {
        var context = new DatabaseContext();
        
        var priorities = context.Priorities.ToList();

        foreach (var priority in priorities)
            RepetitionAlgorithm.Counting.CountCheckTime(priority);

        context.SaveChanges();
    }

    public static List<Words> GetWordsForRevising(int elementsPerPage, int pageNumber)
    {
        var context = new DatabaseContext();
        
        return context.Priorities.Include(p => p.Words)
            .Where(p => p.MinutesToRepeat <= 0)
            .Select(p => p.Words)
            .OrderBy(x => x.Id)
            .Skip(pageNumber * elementsPerPage - elementsPerPage)
            .Take(elementsPerPage)
            .ToList();
    }

    public static Words? GetWordElement(string word)
    {
        var context = new DatabaseContext();

        return context.Words.FirstOrDefault(x => x.Phrase == word.ToLower());
    }
}