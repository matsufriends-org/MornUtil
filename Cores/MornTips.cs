using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MornUtil
{
    [Serializable]
    public class MornTips
    {
        [SerializeField] private string _message;

        public MornTips(string message)
        {
            _message = message;
        }

        public MornTips()
        {
            _message = "Tipsが未設定です。";
        }
    }
}