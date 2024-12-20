using System.ComponentModel.DataAnnotations;

namespace Remembvoc.Infrastructure.Data.ModelsDTO;

public class WordDTO
{
    public int Id { get; set; }
    [Required]
    public string Phrase { get; set; }
    [Required]
    public string Translation { get; set; }
    
    public int LanguageId { set; get; }
    public virtual LanguageDTO Language { set; get; }
    
    public virtual PriorityDTO Priority { set; get; }
}