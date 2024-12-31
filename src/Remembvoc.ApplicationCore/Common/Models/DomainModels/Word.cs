namespace Remembvoc.ApplicationCore.Common.Models.DomainModels;

public class Word
{
    public int Id { get; set; }
    public string Phrase { get; set; }
    public string Translation { get; set; }
    public Language Language { get; set; }
}