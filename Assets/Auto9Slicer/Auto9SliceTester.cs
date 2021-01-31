using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Auto9Slicer
{
    [CreateAssetMenu(menuName = "Auto 9Slice/Tester", fileName = nameof(Auto9SliceTester))]
    public class Auto9SliceTester : ScriptableObject
    {
        public SliceOptions Options => options;
        [SerializeField] private SliceOptions options = new SliceOptions();

        public void Run()
        {
            var directoryPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));
            if (directoryPath == null) throw new Exception($"directoryPath == null");

            var fullDirectoryPath = Path.Combine(Path.GetDirectoryName(Application.dataPath) ?? "", directoryPath);
            var targets = Directory.GetFiles(fullDirectoryPath)
                .Select(Path.GetFileName)
                .Where(x => x.EndsWith(".png") || x.EndsWith(".jpg") || x.EndsWith(".jpeg"))
                .Select(x => Path.Combine(directoryPath, x))
                .Select(x => (Path: x, Texture: AssetDatabase.LoadAssetAtPath<Texture2D>(x)))
                .Where(x => x.Item2 != null)
                .ToArray();

            foreach (var target in targets)
            {
                var importer = AssetImporter.GetAtPath(target.Path);
                if (importer is TextureImporter textureImporter)
                {
                    if (textureImporter.spriteBorder != Vector4.zero) continue;

                    var fullPath = Path.Combine(Path.GetDirectoryName(Application.dataPath) ?? "", target.Path);
                    Debug.Log($"Auto 9Slice {Path.GetFileName(target.Path)}");

                    // importerのreadable設定に依らずに読み込むために直接読む
                    var targetTexture = new Texture2D(2, 2);
                    targetTexture.LoadImage(File.ReadAllBytes(fullPath));

                    var slicedTexture = Slicer.Slice(targetTexture, Options);
                    textureImporter.textureType = TextureImporterType.Sprite;
                    textureImporter.spriteBorder = slicedTexture.Border.ToVector4();
                    if (fullPath.EndsWith(".png")) File.WriteAllBytes(fullPath, slicedTexture.Texture.EncodeToPNG());
                    if (fullPath.EndsWith(".jpg")) File.WriteAllBytes(fullPath, slicedTexture.Texture.EncodeToJPG());
                    if (fullPath.EndsWith(".jpeg")) File.WriteAllBytes(fullPath, slicedTexture.Texture.EncodeToJPG());
                }
            }

            AssetDatabase.Refresh();
        }
    }

    [CustomEditor(typeof(Auto9SliceTester))]
    public class Auto9SliceTesterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Run")) ((Auto9SliceTester) target).Run();
        }
    }
}