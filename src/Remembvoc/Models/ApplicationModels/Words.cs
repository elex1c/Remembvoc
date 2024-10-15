using System.ComponentModel.DataAnnotations;

namespace Remembvoc.Models.ApplicationModels;

public class Words
{
    public int Id { get; set; }
    [Required]
    public string Phrase { get; set; }
    [Required]
    public string Translation { get; set; }
    public int LanguageId { get; set; }
    public Languages Language { get; set; }
}