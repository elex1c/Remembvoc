namespace Remembvoc.ApplicationCore.Common.Models.DTOs;

public class Priority
{
    public int Id { get; set; }
    public double Points { get; set; }
    public DateTime LastCheck { get; set; }
    public int MinutesToRepeat { get; set; }
    public int Period { get; set; }
    
    public void CountCheckTime()
    {
        DateTime now = DateTime.Now;
        int minutesSinceLastCheck = (int)Math.Round((now - LastCheck).TotalMinutes);
            
        MinutesToRepeat -= minutesSinceLastCheck;
        LastCheck = now;
    }
}