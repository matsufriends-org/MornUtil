using UnityEngine;

namespace MornUtil
{
    public static class MornAnimatorEx
    {
        public static void MornPlay(this Animator animator, AnimationClip clip, float transition = 0)
        {
            animator.CrossFadeInFixedTime(clip.name, transition);
        }

        public static void MornApplyImmediate(this Animator animator, AnimationClip clip, float normalizedTime = 1f)
        {
            // 対象のクリップを最後まで一瞬で再生する
            if (animator == null || clip == null)
            {
                return;
            }

            animator.Play(clip.name, -1, normalizedTime);
            animator.Update(0f);
            animator.StopPlayback();
        }
    }
}