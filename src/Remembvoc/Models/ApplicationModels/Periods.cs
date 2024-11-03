namespace Remembvoc.Models.ApplicationModels;

public static class Periods
{
    private static readonly int[] values = [1440, 2880, 4320, 10080, 20160, 40320];

    public static int GetPeriod(int period)
    {
        if (period < 0 || period >= values.Length)
        {
            return values[0];
        }
        return values[period];
    }
}