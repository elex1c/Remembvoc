using Remembvoc.ApplicationCore.Common.Utilities;

namespace Remembvoc.ApplicationCore.Common.Models.DomainModels;

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
    
    public void CountPoints(bool isTranslationSuccessful)
    {
        if (isTranslationSuccessful)
        {
            Period += 1;
            MinutesToRepeat = Periods.GetPeriod(Period - 1);
        }
        else 
        {
            if (Period > 1) Period -= 1;
            MinutesToRepeat = (int)Math.Round(Periods.GetPeriod(Period - 1) * 0.65);
        }
    }

    public void DefaultSet(int periodTime)
    {
        Period = 1;
        MinutesToRepeat = periodTime;
        LastCheck = DateTime.Now;
        Points = 1;
    }
}