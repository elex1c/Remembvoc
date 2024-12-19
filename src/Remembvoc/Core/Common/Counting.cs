using Remembvoc.Core.Common.Models;

namespace Remembvoc.RepetitionAlgorithm
{
    public static class Counting
    {
        public static void CountCheckTime(PriorityEntity priorityEntity)
        {
            DateTime now = DateTime.Now;
            int minutesSinceLastCheck = (int)Math.Round((now - priorityEntity.LastCheck).TotalMinutes);
            
            priorityEntity.MinutesToRepeat -= minutesSinceLastCheck;
            priorityEntity.LastCheck = now;
        }

        public static void DefaultSet(PriorityEntity priorityEntity, int periodTime)
        {
            priorityEntity.Period = 1;
            priorityEntity.MinutesToRepeat = periodTime;
            priorityEntity.LastCheck = DateTime.Now;
            priorityEntity.Points = 1;
        }
        
        public static void CountPoints(PriorityEntity priorityEntity, bool isSuccess)
        {
            if (isSuccess)
            {
                priorityEntity.Period += 1;
                priorityEntity.MinutesToRepeat = Periods.GetPeriod(priorityEntity.Period - 1);
            }
            else 
            {
                if (priorityEntity.Period > 1) priorityEntity.Period -= 1;
                priorityEntity.MinutesToRepeat = (int)Math.Round(Periods.GetPeriod(priorityEntity.Period - 1) * 0.65);
            }
        }
    }
}
