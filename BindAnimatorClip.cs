using System;
using UnityEngine;

namespace MornUtil
{
    /// <summary>
    /// AnimatorからAnimationClipを選択するためのバインドクラス
    /// </summary>
    [Serializable]
    public class BindAnimatorClip
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _clipName;
        /// <summary>
        /// バインドされたAnimator
        /// </summary>
        public Animator Animator
        {
            get => _animator;
            set
            {
                if (_animator != value)
                {
                    _animator = value;
                    // Animatorが変更されたらクリップ名をリセット
                    _clipName = string.Empty;
                }
            }
        }
        /// <summary>
        /// 選択されたクリップ名
        /// </summary>
        public string ClipName
        {
            get => _clipName;
            set => _clipName = value;
        }
        /// <summary>
        /// 選択されたAnimationClip
        /// </summary>
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
        /// <summary>
        /// 有効なバインドかどうか
        /// </summary>
        public bool IsValid => Clip != null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BindAnimatorClip()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BindAnimatorClip(Animator animator, string clipName = "")
        {
            _animator = animator;
            _clipName = clipName;
        }

        /// <summary>
        /// 文字列表現
        /// </summary>
        public override string ToString()
        {
            if (_animator == null)
                return "Animator: None";
            if (string.IsNullOrEmpty(_clipName))
                return $"Animator: {_animator.name}, Clip: None";
            return $"Animator: {_animator.name}, Clip: {_clipName}";
        }
    }
}