using System;
using System.Diagnostics;

namespace SharedTools
{
    public static class Performance
    {
        public static TimeSpan TestExecutionDuration(Action action, int iterations)
        {
            Stopwatch watch = Stopwatch.StartNew();
            for (int x = 1; x <= iterations; x++) action();
            watch.Stop();
            return new TimeSpan(watch.ElapsedTicks / iterations);
        }
    }
}
