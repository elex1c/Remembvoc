namespace Remembvoc.Core.Common.Models;

public class Priority
{
    public int Id { get; set; }
    public double Points { get; set; }
    public DateTime LastCheck { get; set; }
    public int MinutesToRepeat { get; set; }
    public int Period { get; set; }
    public virtual Word Words { get; set; }
}