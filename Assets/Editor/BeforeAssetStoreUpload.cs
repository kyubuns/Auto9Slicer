using System.IO;
using UnityEditor;
using UnityEngine;

namespace Development
{
    public static class BeforeAssetStoreUpload
    {
        private const string Target = "Assets/Auto9Slicer";

        [MenuItem("Asset Store Tools/**Before Upload**")]
        public static void Run()
        {
            Debug.Log("Start");
            AssetDatabase.DeleteAsset("Assets/Auto9Slicer/LICENSE.md");
            AssetDatabase.DeleteAsset("Assets/Auto9Slicer/package.json");

            var basePath = Path.Combine(Application.dataPath, "Auto9Slicer");
            Directory.Move(Path.Combine(basePath, "Samples~/Demo"), Path.Combine(basePath, "Demo"));
            File.Move(Path.Combine(basePath, "Samples~/Demo.meta"), Path.Combine(basePath, "Demo.meta"));

            AssetDatabase.Refresh(ImportAssetOptions.Default);
            Debug.Log("Finish");
        }
    }
}