using System;

namespace MornUtil
{
    public static class MornTimeSpan
    {
        public static TimeSpan ToTimeSpanAsSeconds(this float seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }
    }
}