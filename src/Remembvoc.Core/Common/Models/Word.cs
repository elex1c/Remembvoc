namespace Remembvoc.Core.Common.Models;

public class Word
{
    public int Id { get; set; }
    public string Phrase { get; set; }
    public string Translation { get; set; }
    public int LanguageId { set; get; }
}