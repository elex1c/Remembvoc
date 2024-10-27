using Microsoft.EntityFrameworkCore;
using Remembvoc.Models.ApplicationModels;

namespace Remembvoc.Helper;

public static class DbMethods
{
    public static void UpdateTimeInPriorities(DatabaseContext context)
    {
        var priorities = context.Priorities.ToList();

        foreach (var priority in priorities)
            RepetitionAlgorithm.Counting.CountCheckTime(priority);

        context.SaveChanges();
    }

    public static List<Words> GetWordsForRevising(int elementsPerPage, int pageNumber, DatabaseContext context)
    {
        return context.Priorities.Include(p => p.Words)
            .Where(p => p.MinutesToRepeat <= 0)
            .Select(p => p.Words)
            .OrderBy(x => x.Id)
            .Skip(pageNumber * elementsPerPage - elementsPerPage)
            .Take(elementsPerPage)
            .ToList();
    }
}