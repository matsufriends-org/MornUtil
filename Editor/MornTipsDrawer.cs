using UnityEditor;
using UnityEngine;

namespace MornUtil
{
    [CustomPropertyDrawer(typeof(MornTips))]
    internal class MornTipsDrawer : PropertyDrawer
    {
        private const string TipsEnabledKey = "MornUtil.TipsEnabled";
        private const string TipsEditModeKey = "MornUtil.TipsEditMode";
        public static bool TipsEnabled
        {
            get => EditorPrefs.GetBool(TipsEnabledKey, true);
            set => EditorPrefs.SetBool(TipsEnabledKey, value);
        }
        public static bool TipsEditMode
        {
            get => EditorPrefs.GetBool(TipsEditModeKey, false);
            set => EditorPrefs.SetBool(TipsEditModeKey, value);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!TipsEnabled)
            {
                return;
            }

            var messageProperty = property.FindPropertyRelative("_message");
            if (messageProperty != null)
            {
                if (TipsEditMode)
                {
                    // 編集モード: TextAreaとして表示
                    EditorGUI.BeginProperty(position, label, property);
                    EditorGUI.BeginChangeCheck();
                    messageProperty.stringValue = EditorGUI.TextArea(position, messageProperty.stringValue);
                    if (EditorGUI.EndChangeCheck())
                    {
                        property.serializedObject.ApplyModifiedProperties();
                    }

                    EditorGUI.EndProperty();
                }
                else if (!string.IsNullOrEmpty(messageProperty.stringValue))
                {
                    // 通常モード: HelpBoxとして表示
                    EditorGUI.HelpBox(position, messageProperty.stringValue, MessageType.Info);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!TipsEnabled)
            {
                return 0f;
            }

            var messageProperty = property.FindPropertyRelative("_message");
            if (messageProperty != null)
            {
                if (TipsEditMode)
                {
                    // 編集モード: TextAreaの高さ
                    var content = new GUIContent(messageProperty.stringValue);
                    var minHeight = EditorGUIUtility.singleLineHeight * 2;
                    var textHeight = GUI.skin.textArea.CalcHeight(content, EditorGUIUtility.currentViewWidth - 25f);
                    return Mathf.Max(minHeight, textHeight) + 4f;
                }

                if (!string.IsNullOrEmpty(messageProperty.stringValue))
                {
                    // 通常モード: HelpBoxの高さ
                    var content = new GUIContent(messageProperty.stringValue);
                    var style = GUI.skin.GetStyle("helpbox");
                    return style.CalcHeight(content, EditorGUIUtility.currentViewWidth - 25f) + 4f;
                }
            }

            return 0f;
        }
    }
}