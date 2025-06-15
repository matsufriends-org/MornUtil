using System.IO;
using UnityEditor;
using UnityEngine;

namespace MornUtil
{
    internal sealed class MornSimpleImageGeneratorWindow : EditorWindow
    {
        private Color _fillColor = Color.white;
        private int _width = 512;
        private int _height = 512;
        private string _fileName = "GeneratedImage";
        private string _savePath = "";

        [MenuItem("Tools/MornUtil/Simple Image Generator")]
        private static void Open()
        {
            var window = GetWindow<MornSimpleImageGeneratorWindow>();
            window.titleContent = new GUIContent("Simple Image Generator");
            window.minSize = new Vector2(400, 250);
        }

        private void OnGUI()
        {
            GUILayout.Label("簡単な画像生成ツール", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 色の設定
            _fillColor = EditorGUILayout.ColorField("塗りつぶす色", _fillColor);
            EditorGUILayout.Space();

            // サイズの設定
            EditorGUILayout.LabelField("画像サイズ", EditorStyles.boldLabel);
            _width = EditorGUILayout.IntField("幅 (Width)", _width);
            _height = EditorGUILayout.IntField("高さ (Height)", _height);

            // サイズの制限
            _width = Mathf.Clamp(_width, 1, 4096);
            _height = Mathf.Clamp(_height, 1, 4096);

            EditorGUILayout.Space();

            // ファイル名
            _fileName = EditorGUILayout.TextField("ファイル名", _fileName);

            EditorGUILayout.Space();

            // 保存パスの選択
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("保存先: " + (_savePath == "" ? "未選択" : _savePath));
            if (GUILayout.Button("選択", GUILayout.Width(60)))
            {
                var selectedPath = EditorUtility.SaveFilePanel(
                    "画像の保存先を選択",
                    Application.dataPath,
                    _fileName,
                    "png"
                );

                if (!string.IsNullOrEmpty(selectedPath))
                {
                    _savePath = selectedPath;
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20);

            // 生成ボタン
            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_savePath));
            if (GUILayout.Button("画像を生成", GUILayout.Height(30)))
            {
                GenerateImage();
            }
            EditorGUI.EndDisabledGroup();

            if (string.IsNullOrEmpty(_savePath))
            {
                EditorGUILayout.HelpBox("保存先を選択してください", MessageType.Info);
            }
        }

        private void GenerateImage()
        {
            // テクスチャの作成
            var texture = new Texture2D(_width, _height, TextureFormat.RGBA32, false);
            
            // 色で塗りつぶす
            var pixels = new Color[_width * _height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = _fillColor;
            }
            texture.SetPixels(pixels);
            texture.Apply();

            // PNGにエンコード
            byte[] pngData = texture.EncodeToPNG();

            // ファイルに保存
            try
            {
                File.WriteAllBytes(_savePath, pngData);
                
                // Unityプロジェクト内の場合、アセットをリフレッシュ
                if (_savePath.StartsWith(Application.dataPath))
                {
                    AssetDatabase.Refresh();
                }

                Debug.Log($"画像を生成しました: {_savePath}");
                EditorUtility.DisplayDialog("成功", $"画像を生成しました。\n{_savePath}", "OK");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"画像の保存に失敗しました: {e.Message}");
                EditorUtility.DisplayDialog("エラー", $"画像の保存に失敗しました。\n{e.Message}", "OK");
            }
            finally
            {
                // テクスチャのクリーンアップ
                DestroyImmediate(texture);
            }
        }
    }
}