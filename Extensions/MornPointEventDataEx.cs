﻿using UnityEngine.EventSystems;

namespace MornUtil
{
    public static class MornPointEventDataEx
    {
        public static bool IsLeftClick(this PointerEventData pointerEventData)
        {
            return pointerEventData.pointerId == -1;
        }

        public static bool IsRightClick(this PointerEventData pointerEventData)
        {
            return pointerEventData.pointerId == -2;
        }

        public static bool IsMiddleClick(this PointerEventData pointerEventData)
        {
            return pointerEventData.pointerId == -3;
        }
    }
}