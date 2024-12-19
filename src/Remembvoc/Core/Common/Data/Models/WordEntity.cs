using System.ComponentModel.DataAnnotations;

namespace Remembvoc.Core.Common.Models;

public class WordEntity
{
    public int Id { get; set; }
    [Required]
    public string Phrase { get; set; }
    [Required]
    public string Translation { get; set; }
    public int LanguageId { get; set; }
    public LanguageEntity LanguageEntity { get; set; }
    public virtual PriorityEntity PriorityEntity { get; set; }
}