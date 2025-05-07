﻿using System;
using Random = UnityEngine.Random;

namespace MornUtil
{
    public static class MornArrayEx
    {
        public static bool Contains<T>(this T[] array, T value)
        {
            return Array.IndexOf(array, value) >= 0;
        }

        public static T GetRandomValue<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
    }
}