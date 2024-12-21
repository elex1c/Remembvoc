using System.ComponentModel.DataAnnotations;

namespace Remembvoc.ApplicationCore.Common.Models.Entities;

public class LanguageEntity
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    
    public virtual ICollection<WordEntity> Words { get; set; }
}