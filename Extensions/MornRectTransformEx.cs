﻿using UnityEngine;

namespace MornUtil
{
    public static class MornRectTransformEx
    {
        public static Vector2 CenterPosition(this RectTransform rect)
        {
            var result = (Vector2)rect.position;
            var dif = Vector2.one * 0.5f - rect.pivot;
            result += dif * rect.rect.size * rect.transform.lossyScale;
            return result;
        }
    }
}