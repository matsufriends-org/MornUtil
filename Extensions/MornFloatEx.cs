using UnityEngine;

namespace MornUtil
{
    public static class MornFloatEx
    {
        public static bool AsProbability(this float value)
        {
            return Random.Range(0, 1f) <= value;
        }
    }
}