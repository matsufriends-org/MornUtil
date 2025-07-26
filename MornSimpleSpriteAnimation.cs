using System;
using UnityEngine;

namespace MornUtil
{
    public sealed class MornSimpleSpriteAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private float _duration = 0.2f;
        private int _nextIndex;
        private float _nextChangeTime;

        private void Reset()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_sprites == null || _sprites.Length == 0 || _renderer == null)
            {
                return;
            }

            if (Time.time >= _nextChangeTime)
            {
                _renderer.sprite = _sprites[_nextIndex];
                _nextIndex = (_nextIndex + 1) % _sprites.Length;
                _nextChangeTime = Time.time + _duration;
            }
        }
    }
}