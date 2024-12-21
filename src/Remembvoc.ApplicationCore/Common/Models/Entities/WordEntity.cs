using System.ComponentModel.DataAnnotations;

namespace Remembvoc.ApplicationCore.Common.Models.Entities;

public class WordEntity
{
    public int Id { get; set; }
    [Required]
    public string Phrase { get; set; }
    [Required]
    public string Translation { get; set; }
    
    public int LanguageId { set; get; }
    public virtual LanguageEntity Language { set; get; }
    
    public virtual PriorityEntity Priority { set; get; }
}