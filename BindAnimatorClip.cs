using System;
using UnityEngine;

namespace MornUtil
{
    [Serializable]
    public class BindAnimatorClip
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _clipName;
        public Animator Animator => _animator;
        public AnimationClip Clip
        {
            get
            {
                if (_animator == null || _animator.runtimeAnimatorController == null || string.IsNullOrEmpty(_clipName))
                {
                    return null;
                }

                var clips = _animator.runtimeAnimatorController.animationClips;
                foreach (var clip in clips)
                {
                    if (clip != null && clip.name == _clipName)
                    {
                        return clip;
                    }
                }

                return null;
            }
        }
        public bool IsValid => Clip != null;

        public BindAnimatorClip()
        {
        }

        public BindAnimatorClip(Animator animator, string clipName = "")
        {
            _animator = animator;
            _clipName = clipName;
        }

        public void Play(float transition = 0f)
        {
            if (_animator == null || string.IsNullOrEmpty(_clipName))
            {
                return;
            }

            var clip = Clip;
            if (clip != null)
            {
                _animator.MornPlay(clip, transition);
            }
        }

        public void ApplyImmediate(float normalizedTime = 1f)
        {
            if (_animator == null || string.IsNullOrEmpty(_clipName))
            {
                return;
            }

            var clip = Clip;
            if (clip != null)
            {
                _animator.MornApplyImmediate(clip, normalizedTime);
            }
        }
    }
}