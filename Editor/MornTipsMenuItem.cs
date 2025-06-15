using UnityEditor;

namespace MornUtil
{
    internal static class MornTipsMenuItem
    {
        [MenuItem("Tools/MornTips/Tips表示")]
        private static void ShowTips()
        {
            MornTipsDrawer.TipsEnabled = true;
            MornTipsDrawer.TipsEditMode = false;
        }

        [MenuItem("Tools/MornTips/Tips非表示")]
        private static void HideTips()
        {
            MornTipsDrawer.TipsEnabled = false;
            MornTipsDrawer.TipsEditMode = false;
        }

        [MenuItem("Tools/MornTips/Tips変更")]
        private static void ChangeTips()
        {
            MornTipsDrawer.TipsEnabled = true;
            MornTipsDrawer.TipsEditMode = true;
        }
    }
}