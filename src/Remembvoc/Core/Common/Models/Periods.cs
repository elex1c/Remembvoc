namespace Remembvoc.Core.Common.Models;

public static class Periods
{
    private static readonly int[] periods = [1440, 2880, 4320, 10080, 20160, 40320];

    public static int GetPeriod(int period)
    {
        if (period < 0 || period >= periods.Length)
        {
            return periods[0];
        }
        return periods[period];
    }
}