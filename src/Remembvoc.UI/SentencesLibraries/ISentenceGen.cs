using Remembvoc.UI.Models;

namespace Remembvoc.UI.SentencesLibraries;

public interface ISentenceGen
{
    public Task<string?> GenerateSentence(string word, string language);
}