namespace Remembvoc.ApplicationCore.Common.Models.DTOs;

public class Word
{
    public int Id { get; set; }
    public string Phrase { get; set; }
    public string Translation { get; set; }
    public Language Language { get; set; }
}