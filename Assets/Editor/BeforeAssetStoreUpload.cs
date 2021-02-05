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
            AssetDatabase.DeleteAsset("Assets/Auto9Slicer/README.md");
            AssetDatabase.DeleteAsset("Assets/Auto9Slicer/package.json");
            Debug.Log("Finish");
        }
    }
}