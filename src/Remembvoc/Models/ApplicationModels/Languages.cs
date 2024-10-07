using System.ComponentModel.DataAnnotations;

namespace Remembvoc.Models.ApplicationModels;

public class Languages
{
    public int Id { get; set; }
    [Required]
    public string ShortForm { get; set; }
    public List<Words> Words { get; set; }
}