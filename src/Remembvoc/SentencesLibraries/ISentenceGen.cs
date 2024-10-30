using Remembvoc.Models;

namespace Remembvoc.SentencesLibraries;

public interface ISentenceGen
{
    public Task<string?> GenerateSentence(string word, Languages language);
}