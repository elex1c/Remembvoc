namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface ISentenceGen
{
    public Task<string?> GenerateSentence(string word, string language);
}