using Remembvoc.ApplicationCore.Common.Interfaces;

namespace Remembvoc.ApplicationCore.Common.Services;

public class SentenceGenService : ISentenceGenService
{
    private readonly ISentenceGen _sentenceGenerator;

    public SentenceGenService(ISentenceGen sentenceGenerator)
    {
        _sentenceGenerator = sentenceGenerator;
    }
    
    public async Task<string?> GenerateAsync(string phrase, string language)
    {
        return await _sentenceGenerator.GenerateSentence(phrase, language);;
    }
}