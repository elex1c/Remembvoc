namespace Remembvoc.ApplicationCore.Common.Models.Entities;

public class PriorityEntity
{
    public int WordId { get; set; }
    public double Points { get; set; }
    public DateTime LastCheck { get; set; }
    public int MinutesToRepeat { get; set; }
    public int Period { get; set; }
    
    public virtual WordEntity Word { get; set; }
}