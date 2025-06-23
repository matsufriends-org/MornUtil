using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MornUtil
{
    public static class MornAnimatorEx
    {
        public static void MornPlay(this Animator animator, AnimationClip clip, float transition = 0)
        {
            animator.CrossFadeInFixedTime(clip.name, transition);
        }

        public async static UniTask MornPlayAsync(this Animator animator, AnimationClip clip, float transition = 0,
            CancellationToken ct = default)
        {
            animator.CrossFadeInFixedTime(clip.name, transition);

            // まずクリップが再生されるまで待機
            await UniTask.WaitUntil(
                () =>
                {
                    var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                    return stateInfo.IsName(clip.name) && stateInfo.normalizedTime > 0f;
                },
                cancellationToken: ct);
            // その後、クリップの終了まで待機
            await UniTask.WaitUntil(
                () =>
                {
                    var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                    return !stateInfo.IsName(clip.name) || stateInfo.normalizedTime >= 1f;
                },
                cancellationToken: ct);
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