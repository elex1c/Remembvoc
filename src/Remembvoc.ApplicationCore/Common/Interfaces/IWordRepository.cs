using Remembvoc.ApplicationCore.Common.Models.Entities;

namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IWordRepository : IRepository<WordEntity>
{
    public Task AddWordAsync(WordEntity wordEntity);
    public Task DeleteWordAsync(WordEntity wordEntity);
    public Task<List<WordEntity>> GetAllWithPrioritiesAsync();
    public Task<WordEntity?> GetWordByNameAsync(string word);
}