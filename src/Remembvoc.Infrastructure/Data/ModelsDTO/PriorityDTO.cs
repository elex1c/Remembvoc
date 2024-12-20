namespace Remembvoc.Infrastructure.Data.ModelsDTO;

public class PriorityDTO
{
    public int Id { get; set; }
    public double Points { get; set; }
    public DateTime LastCheck { get; set; }
    public int MinutesToRepeat { get; set; }
    public int Period { get; set; }

    public int WordId { get; set; }
    public virtual WordDTO Word { get; set; }
}