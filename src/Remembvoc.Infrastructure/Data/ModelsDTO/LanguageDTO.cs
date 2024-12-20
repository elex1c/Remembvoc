namespace Remembvoc.Infrastructure.Data.ModelsDTO;

public class LanguageDTO
{
    public int Id { get; set; }
    public string LanguageName { get; set; }
    
    public virtual ICollection<WordDTO> Words { get; set; }
}