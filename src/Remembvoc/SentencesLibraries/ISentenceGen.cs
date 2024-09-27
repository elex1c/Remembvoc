using Remembvoc.Models;

namespace Remembvoc.SentencesLibraries;

public interface ISentenceGen
{
    public Task<string?> Generate(string word, Languages language);
}