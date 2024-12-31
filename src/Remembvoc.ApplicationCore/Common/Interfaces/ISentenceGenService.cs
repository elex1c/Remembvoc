namespace Remembvoc.ApplicationCore.Common.Interfaces;

public interface ISentenceGenService
{
    public Task<string?> GenerateAsync(string phrase, string language);
}