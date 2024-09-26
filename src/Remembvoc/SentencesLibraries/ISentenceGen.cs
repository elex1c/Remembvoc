using Remembvoc.Models;

namespace Remembvoc.SentencesLibraries;

public interface ISentenceGen
{
    public string API_KEY { get; set; }
    public string Generate(string word, Languages language);
}