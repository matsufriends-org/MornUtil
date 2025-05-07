﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MornUtil
{
    public static class MornEnumerableEx
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(_ => Guid.NewGuid());
        }

        public static T MinBy<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        {
            var enumerable = source as T[] ?? source.ToArray();
            var min = enumerable.Min(selector);
            return enumerable.First(_ => Equals(selector.Invoke(_), min));
        }

        public static T MaxBy<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        {
            var enumerable = source as T[] ?? source.ToArray();
            var max = enumerable.Max(selector);
            return enumerable.First(_ => EqualityComparer<TResult>.Default.Equals(selector.Invoke(_), max));
        }
    }
}