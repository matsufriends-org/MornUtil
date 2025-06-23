using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MornUtil.Editor
{
    /// <summary>
    /// BindAnimatorClip用のCustomPropertyDrawer
    /// </summary>
    [CustomPropertyDrawer(typeof(BindAnimatorClip))]
    internal class BindAnimatorClipPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            var animatorProp = property.FindPropertyRelative("_animator");
            var clipNameProp = property.FindPropertyRelative("_clipName");
            
            // ラベルを描画
            var rect = EditorGUI.PrefixLabel(position, label);
            
            // インデントをリセット
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            // Animatorとクリップを1行で表示
            var animatorWidth = rect.width * 0.5f;
            var clipWidth = rect.width * 0.5f;
            
            // Animatorフィールド
            var animatorRect = new Rect(rect.x, rect.y, animatorWidth, rect.height);
            var newAnimator = EditorGUI.ObjectField(animatorRect, animatorProp.objectReferenceValue, typeof(Animator), true) as Animator;
            
            if (newAnimator != animatorProp.objectReferenceValue)
            {
                animatorProp.objectReferenceValue = newAnimator;
                clipNameProp.stringValue = string.Empty; // Animatorが変更されたらクリップ名をリセット
            }
            
            // AnimationClip選択フィールド
            var clipRect = new Rect(rect.x + animatorWidth, rect.y, clipWidth, rect.height);
            DrawClipSelector(clipRect, animatorProp.objectReferenceValue as Animator, clipNameProp);
            
            // インデントを復元
            EditorGUI.indentLevel = indent;
            
            EditorGUI.EndProperty();
        }
        
        private void DrawClipSelector(Rect rect, Animator animator, SerializedProperty clipNameProp)
        {
            if (animator?.runtimeAnimatorController == null)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.Popup(rect, 0, new string[] { "Animatorを設定" });
                EditorGUI.EndDisabledGroup();
                return;
            }
            
            var clips = GetAnimationClips(animator);
            if (clips.Count == 0)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.Popup(rect, 0, new string[] { "クリップなし" });
                EditorGUI.EndDisabledGroup();
                return;
            }
            
            var clipNames = clips.Select(c => c?.name ?? "null").ToArray();
            var currentClipName = clipNameProp.stringValue;
            var currentIndex = System.Array.IndexOf(clipNames, currentClipName);
            
            // 現在の選択が無効な場合は0にリセット
            if (currentIndex < 0)
            {
                currentIndex = 0;
                clipNameProp.stringValue = clipNames[0];
            }
            
            var newIndex = EditorGUI.Popup(rect, currentIndex, clipNames);
            if (newIndex != currentIndex && newIndex >= 0 && newIndex < clipNames.Length)
            {
                clipNameProp.stringValue = clipNames[newIndex];
            }
        }
        
        private List<AnimationClip> GetAnimationClips(Animator animator)
        {
            var clips = new List<AnimationClip>();
            
            if (animator?.runtimeAnimatorController == null)
                return clips;
                
            var allClips = animator.runtimeAnimatorController.animationClips;
            if (allClips != null)
            {
                // 重複を除去
                var uniqueClips = allClips.Where(c => c != null).GroupBy(c => c.name).Select(g => g.First());
                clips.AddRange(uniqueClips);
            }
            
            return clips.OrderBy(c => c.name).ToList();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
    }
}