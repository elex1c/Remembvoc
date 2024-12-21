using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IWordRepository : IRepository<WordEntity>
{
    public Task<List<WordEntity>> GetAllWithPrioritiesAsync();
    public Task<WordEntity?> GetWordByNameAsync(string word);
    public Task<List<WordEntity>> GetWordsForRevisingAsync(int elementsPerPage, int pageNumber);
}