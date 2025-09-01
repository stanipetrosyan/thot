using UnityEditor;
using UnityEngine;

namespace thot.DS.Adapters {
    public static class Assets {
        
        public static T CreateAsset<T>(string path, string assetName) where T : ScriptableObject {
            var asset = LoadAsset<T>(path, assetName);

            if (!asset) {
                asset = ScriptableObject.CreateInstance<T>();
            }

            var fullPath = $"{path}/{assetName}.asset";
            AssetDatabase.CreateAsset(asset, fullPath);

            return asset;
        }

        public static T LoadAsset<T>(string path, string assetName) where T : ScriptableObject {
            return AssetDatabase.LoadAssetAtPath<T>($"{path}/{assetName}.asset");
        }
    }
}