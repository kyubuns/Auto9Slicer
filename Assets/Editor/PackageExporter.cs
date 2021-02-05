using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Development
{
    public static class PackageExporter
    {
        private const string Target = "Assets/Auto9Slicer";

        [MenuItem("Export/Package")]
        public static void Export()
        {
            var packageText = AssetDatabase.LoadAssetAtPath<TextAsset>(Path.Combine(Target, "package.json"));
            var package = JsonUtility.FromJson<PackageJson>(packageText.text);

            var basePath = Path.Combine(Application.dataPath, "Auto9Slicer");
            Directory.Move(Path.Combine(basePath, "Samples~/Demo"), Path.Combine(basePath, "Demo"));
            File.Move(Path.Combine(basePath, "Samples~/Demo.meta"), Path.Combine(basePath, "Demo.meta"));
            AssetDatabase.Refresh(ImportAssetOptions.Default);

            var outputPath = Path.Combine(Path.GetDirectoryName(Application.dataPath) ?? "", $"{package.displayName}_v{package.version}.unitypackage");
            AssetDatabase.ExportPackage(new[] { Target }, outputPath, ExportPackageOptions.Recurse);
            Debug.LogFormat("ExportPackage {0}", outputPath);
        }
    }

    [Serializable]
    public class PackageJson
    {
        public string displayName;
        public string version;
    }
}