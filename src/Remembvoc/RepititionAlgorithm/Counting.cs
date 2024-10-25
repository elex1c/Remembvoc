using Remembvoc.Models.ApplicationModels;

namespace Remembvoc.RepititionAlgorithm
{
    public class Counting
    {
        public static void CountCheckTime(Priorities priority)
        {
            DateTime now = DateTime.Now;
            int minutesSinceLastCheck = (int)Math.Round((now - priority.LastCheck).TotalMinutes);
            
            priority.MinutesToRepeat -= minutesSinceLastCheck;
            priority.LastCheck = now;
        }

        public static void CountPoints(Priorities priority, bool isSuccess, int currentPeriodTime)
        {
            if (isSuccess)
            {
                priority.Period += 1;
                priority.MinutesToRepeat = currentPeriodTime;
            }
            else 
            {
                if (priority.Period > 1) priority.Period -= 1;
                priority.MinutesToRepeat = (int)Math.Round((double)currentPeriodTime * 0.65);
            }
        }
    }
}
