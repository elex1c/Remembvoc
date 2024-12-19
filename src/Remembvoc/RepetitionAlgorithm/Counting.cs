using Remembvoc.Models.ApplicationModels;

namespace Remembvoc.RepetitionAlgorithm
{
    public static class Counting
    {
        public static void CountCheckTime(Priorities priority)
        {
            DateTime now = DateTime.Now;
            int minutesSinceLastCheck = (int)Math.Round((now - priority.LastCheck).TotalMinutes);
            
            priority.MinutesToRepeat -= minutesSinceLastCheck;
            priority.LastCheck = now;
        }

        public static void DefaultSet(Priorities priority, int periodTime)
        {
            priority.Period = 1;
            priority.MinutesToRepeat = periodTime;
            priority.LastCheck = DateTime.Now;
            priority.Points = 1;
        }
        
        public static void CountPoints(Priorities priority, bool isSuccess)
        {
            if (isSuccess)
            {
                priority.Period += 1;
                priority.MinutesToRepeat = Periods.GetPeriod(priority.Period - 1);
            }
            else 
            {
                if (priority.Period > 1) priority.Period -= 1;
                priority.MinutesToRepeat = (int)Math.Round(Periods.GetPeriod(priority.Period - 1) * 0.65);
            }
        }
    }
}
