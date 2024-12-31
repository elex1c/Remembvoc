using Remembvoc.ApplicationCore.Common.Events;
using Remembvoc.ApplicationCore.Common.Models.DomainModels;
using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface IWordService
{
    public Task<WordValidationResponse> AddWordAsync(string word, string language, string translation);
    public Task DeleteWordAsync(string word);
    public Task<List<Word>> GetAllAsync();
    public Task GetAndSendUpdatedDataAsync();
    public Task<Word?> GetWordByNameAsync(string word);
    public Task<int> CountWordsForRevisingAsync();
    public Task<int> CountWordsForWordList();
    public event EventHandler<WordsListUpdatedEvent> WordListUpdated;
}