using Remembvoc.ApplicationCore.Common.Validation.ValidationResponses;

namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface ITranslationService<TTranslationResponse> 
    where TTranslationResponse : ITranslationResponse
{
    public Task<TTranslationResponse> GenerateSentenceAsync(string word);
    public Task<CheckTranslationResponse> CheckTranslationAsync(string userInput);
}