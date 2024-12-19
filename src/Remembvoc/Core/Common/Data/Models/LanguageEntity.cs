using System.ComponentModel.DataAnnotations;

namespace Remembvoc.Core.Common.Models;

public class LanguageEntity
{
    public int Id { get; set; }
    [Required]
    public string ShortForm { get; set; }
    public List<WordEntity> Words { get; set; }
}