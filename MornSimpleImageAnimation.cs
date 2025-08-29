using MornEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MornUtil
{
    public sealed class MornSimpleImageAnimation : MonoBehaviour
    {
        [SerializeField] private Image _renderer;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private bool _isLoop = true;
        [SerializeField, ShowIf(nameof(_isLoop))] private bool _isPingPong;
        [SerializeField] private float _duration = 0.2f;
        private int _nextIndex;
        private float _nextChangeTime;

        private void Reset()
        {
            _renderer = GetComponent<Image>();
        }

        private void Update()
        {
            if (_sprites == null || _sprites.Length == 0 || _renderer == null)
            {
                return;
            }

            if (_isLoop == false && _nextIndex >= _sprites.Length)
            {
                return;
            }

            if (Time.time >= _nextChangeTime)
            {
                if (_isPingPong)
                {
                    var length = _sprites.Length;
                    var pingPongIndex = _nextIndex % (2 * length - 2);
                    if (pingPongIndex >= length)
                    {
                        pingPongIndex = 2 * length - 2 - pingPongIndex;
                    }

                    _renderer.sprite = _sprites[pingPongIndex];
                }
                else
                {
                    _renderer.sprite = _sprites[_nextIndex % _sprites.Length];
                }

                _nextIndex += 1;
                _nextChangeTime = Time.time + _duration;
            }
        }
    }
}